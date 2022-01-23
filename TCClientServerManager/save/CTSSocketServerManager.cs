using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.ClientServer
{
    public class CTSSocketServerManager : ICTSServerManager
    {
        // State object for reading client data asynchronously
        private class ConnectionState
        {
            // Size of receive buffer.
            public const int BufferSize = 1024;
            // Client  socket.
            private Socket workSocket;
            // Receive buffer.
            private byte[] buffer=null;
            // username 
            private string name;
            // connection-Id
            private int id;
            // Thread-Sync events
            private ManualResetEvent clientDone;
            private ManualResetEvent receiveDone;

            public Socket WorkSocket
            {
                get { return workSocket; }
            }

            public byte[] Buffer
            {
                get { return buffer; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public int Id
            {
                get { return id; }
                set { id = value; } 
            }

            public ManualResetEvent ClientDone 
            { 
                get { return clientDone; } 
            }
            
            public ManualResetEvent ReceiveDone 
            { 
                get { return receiveDone; } 
            }

            public ConnectionState(Socket workSocket,int id)
            {
                this.workSocket = workSocket;
                buffer = new byte[BufferSize];
                clientDone = new ManualResetEvent(false);
                receiveDone = new ManualResetEvent(false);
                this.id = id;
            }
        }

        private ICTSServerAdapter m_adapter = null;
        private bool m_isClosing = false;
        private CTSServerManagerBridge m_parent = null;
        private AutoResetEvent m_connectionDone = new AutoResetEvent(false);
        private AutoResetEvent m_connectionFailed = new AutoResetEvent(false);
        private IPEndPoint m_localEndPoint=null;
        private Socket m_listener;
        private ConcurrentDictionary<ConnectionState, string> m_dUsers = new ConcurrentDictionary<ConnectionState, string>();
        private AutoResetEvent m_jobDone;
        private CTSClientTransferData m_transferData;

        // static members
        private static object criticalObjConnection = new object();
        private static int m_actConnectionId = 0;
        private static ManualResetEvent m_acceptDone = new ManualResetEvent(false);
        private static ManualResetEvent m_serverDone = new ManualResetEvent(false);
        private static CTSDataFilter[] m_dataFilter = new CTSDataFilter[1000];
        private static string m_commandString = String.Empty;

        private static string[] m_aStrCommands = {  "#TRANSFERSTART",
                                                    "#TRANSFEREND",
                                                    "#LOGIN",
                                                    "#TRANSFERREQUEST",
                                                    "#SENDCLASSMESSAGE",
                                                    "#ASKTESTPERMISSION",
                                                    "#SENDTESTRESULT",
                                                    "#KEEPALIVE",
                                                    "#SENDUSERPROGRESSINFO",
                                                    "#SENDNOTICE"};


        public CTSSocketServerManager() 
        {
        }

        #region ICTSServerManager implementation
        public bool Create(int portId, ref AutoResetEvent jobDone)
        {
            m_jobDone = jobDone;
            return ConnectServer(portId);
        }

        public void SetParent(object parent)
        {
            m_parent = (CTSServerManagerBridge)parent;
        }

        public void Close()
        {
            if (m_listener != null)
            {
                m_isClosing = true;

                // Alle User ausloggen !
                foreach (var u in m_dUsers.Keys)
                {
                    SendData(u.Id, String.Format("#LOGOUT{{{0}|{1}}}", m_adapter.GetUserName(), 0));
                    SendData(u.Id, String.Format("#LOGOUT{{{0}|{1}}}", u.Name, 0));
                }

                foreach (var u in m_dUsers.Keys)
                    Disconnect(u.Id);

                m_dUsers.Clear();

                try
                {
                    m_serverDone.Set();
                    Thread.Sleep(500);
                    //m_listener.Shutdown(SocketShutdown.Both);
                    m_listener.Close();
                }
                catch (System.Exception ex)
                {
                	
                }
                finally
                {
                    m_listener = null;
                }

                m_isClosing = false;

            }
        }

        public bool IsRunning()
        {
            return true;
        }

        public void AbortTransfer()
        {
            if (m_transferData != null)
            {
                m_transferData.Stop();
                if (m_jobDone != null)
                    m_jobDone.Set();
            }
        }

        public int GetUsersOnline()
        {
            return m_dUsers.Count;
        }

        public void DisconnectUser(string userName)
        {
            ForceDisconnect(userName);
        }

        public void AddMessage(string sMessage)
        {
            m_adapter.AddConsoleMessage(sMessage);
        }

        public void AddMessage(string strTarget,string sMessage)
        {
            lock (this)
            {
                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.Message, strTarget, sMessage);
                m_parent.FireEvent(gea);
            }
        }

        public void SendClassMessage(int roomId, string toUserName, string message)
        {
            ConnectionState user = GetConnectedUser(toUserName);
            if (user!=null)
                SendData(user.Id, String.Format("#RECEIVECLASSMESSAGE{{{0}|{1}|{2}}}", roomId.ToString(), m_adapter.GetUserName(), message));
        }

        public void SendNoticeWorkoutState(string strTitle, int iWorkedOutState, string toUserName)
        {
            ConnectionState user = GetConnectedUser(toUserName);
            if (user != null)
                SendData(user.Id, String.Format("#SENDNOTICEWORKOUTSTATE{{{0}|{1}}}", strTitle, iWorkedOutState.ToString()));
        }


        public void SendNotice(string toUserName,string noticeTitle)
        {
            ConnectionState user = GetConnectedUser(toUserName);
            if (user != null)
            {
                var aNotices = m_adapter.GetNotices(toUserName) as NoticeItemCollection;
                var ni = aNotices.FirstOrDefault(x => x.title == noticeTitle);
                if (ni != null)
                {
                    m_adapter.StartNoticeTransfer(toUserName, ni.title);
                    string dirName = String.Format("{0}\\notices\\", m_adapter.GetNoticesDirectory(toUserName), toUserName);
                    string filePath = dirName + '\\' + ni.fileName;
                    ni.dirtyFlag = 0;
                    if (File.Exists(filePath))
                    {
                        SendFile(user.Id,"NOTICES", ni.title, 1, 1, filePath);
                        SendData(user.Id,String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}}}", 1, 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName)));
                    }
                }
            }
        }

        public void SendTransferRequest(string toUserName, string typeName, string name)
        {
            ConnectionState user = GetConnectedUser(toUserName);
            if (user != null)
                SendData(user.Id, String.Format("#TRANSFERREQUEST{{{0}|{1}}}", typeName, name));
        }

        public void SetAdapter(ICTSServerAdapter adapter)
        {
            m_adapter = adapter;
        }

        public ICTSServerAdapter GetAdapter()
        {
            return m_adapter;
        }

        public IPAddress GetIPAddress()
        {
            return m_localEndPoint!=null ? m_localEndPoint.Address:null;
        }

        #endregion



        /// <summary>
        /// Connect the current instance of command client to the server.This method throws ServerNotFoundException on failur.Run this method and handle the 'ConnectingSuccessed' and 'ConnectingFailed' to get the connection state.
        /// </summary>
        public bool ConnectServer(int portId)
        {
            IPAddress ipAdr = GetIPAddress(Dns.GetHostName());
            if (ipAdr == null)
            {
                AddMessage(String.Format("couldn't find IP Adress"));
                return false;
            }

            m_localEndPoint = new IPEndPoint(ipAdr, portId);
            m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            m_serverDone.Reset();
            m_connectionDone.Reset();
            m_connectionFailed.Reset();

            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.WorkerSupportsCancellation = false;
                bw.DoWork += bwConnector_DoWork;
                bw.RunWorkerCompleted += bwConnector_RunWorkerCompleted;
                bw.RunWorkerAsync();
            }

            int id = WaitHandle.WaitAny(new WaitHandle[] { m_connectionDone, m_connectionFailed }, 30000);
            if (id > 0)
                AddMessage(String.Format("couldn't connect to {0}", m_localEndPoint.ToString()));
            else
                AddMessage(String.Format("successfully connected to {0}", m_localEndPoint.ToString()));

            CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.ConnectionInfo, m_localEndPoint.ToString(), id==0);
            m_parent.FireEvent(gea);

            return true;
        }

        private void bwConnector_DoWork(object sender, DoWorkEventArgs evtArgs)
        {
            try
            {
                AddMessage("Server",String.Format("Try to connect to {0}", m_localEndPoint.ToString()));
                m_listener.Bind(m_localEndPoint);
                m_listener.Listen(100);
                evtArgs.Result = true;
                m_connectionDone.Set();
                using (BackgroundWorker bw = new BackgroundWorker())
                {
                    bw.WorkerSupportsCancellation = true;
                    bw.DoWork += bwAcceptor_StartConnect;
                    bw.RunWorkerCompleted += bwAcceptor_RunWorkerCompleted;
                    bw.RunWorkerAsync();
                }
            }
            catch (Exception e)
            {
                evtArgs.Result = false;
                m_connectionFailed.Set();
            }
        }

        private void bwConnector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && e.Result!=null)
            {
                if ((bool)e.Result == true)
                {

                }
                else
                {

                }
            }
            ((BackgroundWorker)sender).Dispose();
        }

        private void bwAcceptor_StartConnect(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                //Set the event to nonsignaled state.
                m_acceptDone.Reset();

                // Start an asynchronous socket to listen for connections.
                m_listener.BeginAccept(new AsyncCallback(AsyncCallback_Accept), m_listener);

                AddMessage("Server","Waiting for connection..");
                // Wait until a connection is made before continuing.
                int id = WaitHandle.WaitAny(new WaitHandle[] { m_acceptDone, m_serverDone });
                if (id == 1)
                {
                    break;
                }
            }
            AddMessage("Server","Shutdown initiated");
        }

        private void bwAcceptor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && e.Result!=null)
            {
                if ((bool)e.Result == true)
                {

                }
                else
                {

                }
            }
            ((BackgroundWorker)sender).Dispose();
        }

        private void CheckReceivedCommand(int connId, ref string commandString)
        {
            string dataString = "";  // Daten vor dem Kommando im Stream

            // Prüfen ob irgendwo im Stream Kommandos vorkommen
            bool bFound=ParseCommandString(ref dataString, ref commandString);
            if (dataString.Length > 0)
            {
                if (m_transferData != null)
                {
                    m_transferData.DoTransfer(dataString);
                    CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.TransferProgress, m_transferData.TypeName, m_transferData.Name, m_transferData);
                    m_parent.FireEvent(gea);
                }
                else
                {
                    commandString = dataString;
                    AddMessage(String.Format("connection[{0}]", connId), "<-- remained Command string: " + commandString);
                    return;
                }

                if (bFound)
                    CheckReceivedCommand(connId, ref commandString);

            }

            if (bFound)
                if (!DoCommand(connId, ref commandString))
                    return;

            if (commandString.Length > 0)
                CheckReceivedCommand(connId, ref commandString);
        }

        private bool ParseCommandString(ref string dataString,ref string commandString)
        {
            // Prüfen ob irgendwo im String Kommandos vorkommen
            foreach (string s in m_aStrCommands)
            {
                int iFound=commandString.IndexOf(s); // 1.Kommando suchen
                if (iFound >= 0)
                {
                    // Nur Kommando 
                    if (iFound == 0)
                    {
                        dataString = "";
                        return true;
                    }
                    // Daten vor dem Kommando?
                    else if (iFound > 0)
                    {
                        dataString = commandString.Substring(0, iFound);
                        commandString = commandString.Substring(iFound);
                        return true;
                    }
                }
            }
            // Keine Kommandos gefunden?
            dataString = commandString;
            commandString = "";
            return false;
        }

        private bool DoCommand(int connId, ref string commandString)
        {
            string cmd;
            int found = commandString.IndexOf("}");
            if (found >= 0 && found <= (commandString.Length - 1))
            {
                cmd = commandString.Substring(0, found + 1);
                commandString = commandString.Substring(found + 1);
            }
            else
                return false;

            //Trace.WriteLine("-->" + cmd);
            AddMessage(String.Format("connection[{0}]",connId),"<--" + cmd);

            string[] aCommands = cmd.Split('{');
            if (aCommands.Length > 1)
            {
                char[] trimChars = { '}' };
                aCommands[1] = aCommands[1].TrimEnd(trimChars);
                string[] aTokens = aCommands[1].Split('|');
                if (aCommands[0].Length > 0)
                {
                    if (aCommands[0] == "#LOGIN")
                    {
                        if (aTokens.Length == 2)
                            DoLogin(connId, aTokens[0], aTokens[1], "");
                        else if (aTokens.Length == 3)
                            DoLogin(connId, aTokens[0], aTokens[1], aTokens[2]);
                        else
                            return false;
                    }
                    else if (aCommands[0] == "#TRANSFERREQUEST")
                        DoTransferRequest(connId, aTokens[0], aTokens[1]);
                    else if (aCommands[0] == "#SENDCLASSMESSAGE")
                        DoSendClassMessage(connId, Int32.Parse(aTokens[0]), aTokens[1], aTokens[2]);
                    else if (aCommands[0] == "#ASKTESTPERMISSION")
                        DoAskTestPermission(connId, aTokens[0], aTokens[1]);
                    else if (aCommands[0] == "#SENDTESTRESULT")
                        DoSendTestResult(connId, aTokens[0]);
                    else if (aCommands[0] == "#KEEPALIVE")
                        DoKeepAlive(connId, aTokens[0]);
                    else if (aCommands[0] == "#SENDUSERPROGRESSINFO")
                        DoSendUserProgressInfo(connId, aTokens[0], aTokens[1], Int32.Parse(aTokens[2]), Int32.Parse(aTokens[3]));
                    else if (aCommands[0] == "#SENDNOTICE")
                        DoSendNotice(connId, Int32.Parse(aTokens[0]), Int32.Parse(aTokens[1]), aTokens[2], aTokens[3], Int32.Parse(aTokens[4]), aTokens[5], Int32.Parse(aTokens[6]), aTokens[7]);
                    else if (aCommands[0] == "#TRANSFERSTART")
                        DoTransferStart(connId,aTokens[0], aTokens[1], Int32.Parse(aTokens[2]), Int32.Parse(aTokens[3]), aTokens[4], Int32.Parse(aTokens[5]));
                    else if (aCommands[0] == "#TRANSFEREND")
                        DoTransferEnd(connId,aTokens[0], aTokens[1], Int32.Parse(aTokens[2]), Int32.Parse(aTokens[3]));

                    else
                        return false;
                    return true;
                }
            }
            return false;
        }

        private void DoLogin(int connId, string userName, string password, string language)
        {
            try
            {
                string strUserName = userName;
                string strPassword = password;

                if (userName == "admin")
                {
                    strUserName = String.Format("admin@{0}", m_dUsers.Count + 1);
                }
                /*
                if (m_adapter.IsCampus())
                {
                    strUserName = String.Format("CampusUser{0}", m_dUsers.Count + 1);
                    strPassword = strUserName;
                }*/

                if (!IsUserAlreadyOnline(strUserName))
                {
                    ConnectionState user = GetConnectedUser(connId);
                    string strTarget = user.WorkSocket.RemoteEndPoint.ToString();
                    CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.Login, strTarget, strUserName, strPassword, language);

                    m_jobDone.Reset();

                    m_parent.FireEvent(gea);

                    if (!m_jobDone.WaitOne(10000))
                        return;

                    if (gea.ReturnValue == 0)
                    {
                        AddMessage(String.Format("REGISTERSTUDENT({0})", strUserName));
                        // Login ok senden
                        SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, 0));

                        /*
                        if (!m_adapter.IsCampus())
                        {
                            // Alle User bezüglich des Logins benachrichtigen!
                            SendToAll(String.Format("#LOGIN{{{0}|{1}}}", strUserName, 0), m_dUsers.Keys.Where(o => o.Name != userName));
                        }*/

                        user.Name = strUserName;

                        // Dem Neuen User alle anderen mitteilen
                        foreach (var u in m_dUsers.Keys)
                        {
                            if (u.Name != null && u.Name != userName)
                                SendData(u.Id, String.Format("#LOGIN{{{0}|{1}}}", userName, 0));
                        }

                        // Neuer Verbindung Server mitteilen
                        SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", m_adapter.GetUserName(), 0));
                        //SendData(connId, String.Format("#TRANSFERREQUEST{{{0}|{1}|{2}}}", "NOTICELIST", "Notices", userName));
                    }
                    else
                        SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, gea.ReturnValue));
                }
                else
                    SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, 3));
            }
            catch (Exception e)
            {
                AddMessage(String.Format("connection[{0}]-Error: ", connId), e.Message);
                SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", userName, 5));
            }
        }

        private void DoSendClassMessage(int connId, int roomId, string toUserName, string message)
        {
            if (m_adapter.GetUserName() == toUserName)
            {
                //AppHandler.MainForm.ReceiveClassMessage(roomId,(string)m_dUsers[connId],message);
                ReceiveClassMessageDelegate delReceiveClassMessage = m_adapter.GetDelegateReceiveClassMessage();
                string userName = GetConnectedUserName(connId);
                delReceiveClassMessage(roomId, userName, message);
            }
            else
            {
                ConnectionState user = GetConnectedUser(toUserName);
                if (user!=null)
                    SendData(user.Id, String.Format("#RECEIVECLASSMESSAGE{{{0}|{1}|{2}}}", roomId.ToString(), toUserName, message));
            }
        }

        private void DoTransferRequest(int connId, string typeName, string name)
        {
            if (typeName == "USERLIST")
                SendFile(connId, typeName, name, 1, 1, m_adapter.GetUserFileName());
            else if (typeName == "LEARNMAPS")
            {
                string userName = GetConnectedUserName(connId);
                if (userName.IndexOf('@') > 0)
                    userName = userName.Substring(0, userName.IndexOf('@'));

                int mapCnt = 0;
                for (int i = 0; i < m_adapter.GetMapCnt(); ++i)
                {
                    if (userName == "admin" || m_adapter.MapHasUser(i, userName))
                        ++mapCnt;
                }

                int mapsSent = 0;
                int mapId = 1;
                for (int i = 0; i < m_adapter.GetMapCnt(); ++i)
                {
                    if (userName == "admin" || m_adapter.MapHasUser(i, userName))
                    {
                        string fileName = m_adapter.GetMapFileName(i);
                        SendFile(connId, typeName, name, mapCnt, mapId, fileName);
                        ++mapsSent;
                        ++mapId;
                    }
                }

                if (mapsSent == 0)
                    SendData(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
            }
            else if (typeName == "LIBRARIES")
            {
                if (m_adapter.HasLibrary(name))
                    SendFile(connId, "LIBRARIES", name, 1, 1, m_adapter.GetLibraryFileName(name));
                else
                    SendData(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
            }
            else if (typeName == "DOCUMENTS")
            {
                int id=0;
                Dictionary<string,string> dFiles=new Dictionary<string,string>();
                if (m_adapter.GetDocumentsFileNames(ref dFiles))
                {
                    foreach (var d in dFiles)
                    {
                        string[] aFiles = System.IO.Directory.GetFiles(d.Value, "*.*");
                        foreach(var f in aFiles)
                            SendFile(connId, typeName, d.Key, aFiles.Length, ++id, f);
                        id = 0;
                    }
                }

                if (id==0)
                    SendData(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
            }
            else if (typeName == "NOTICES")
            {
                string userName = GetConnectedUserName(connId);
                if (userName.IndexOf('@') > 0)
                    userName = userName.Substring(0, userName.IndexOf('@'));

                int iSent = 0;
                NoticeItemCollection nic = m_adapter.GetNotices(userName) as NoticeItemCollection;
                for (int i = 0; i < nic.Count; ++i)
                {
                    NoticeItem ni = nic[i];
                    if (name == "*" || ni.title == name)
                    {
                        m_adapter.StartNoticeTransfer(userName, ni.title);
                        string dirName = String.Format("{0}\\notices\\", m_adapter.GetNoticesDirectory(userName), userName);
                        string filePath = dirName + '\\' + ni.fileName;
                        ni.dirtyFlag = 0;
                        if (File.Exists(filePath))
                        {
                            SendFile(connId, "NOTICES", name, nic.Count, i + 1, filePath);
                            SendData(connId, String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}}}", nic.Count, i + 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName)));
                            ++iSent;
                        }

                        if (ni.title == name)
                            break;
                    }
                }
               
                if (iSent == 0)
                    SendData(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
            }
        }

        private void DoAskTestPermission(int connId, string userName, string mapTitle)
        {
            SendData(connId, String.Format("#TESTPERMISSION{{{0}|{1}|{2}}}", userName, mapTitle, (m_adapter.AskTestPermission(userName, mapTitle)) ? 1 : 0));
        }

        private void DoSendTestResult(int connId, string sResult)
        {
            m_adapter.SetTestResult(sResult);
        }

        private void DoSendUserProgressInfo(int connId, string sUsername, string workingTitle,int iRegionId,int iRegionVal)
        {
            m_adapter.AddUserProgressInfo(sUsername, workingTitle,iRegionId, iRegionVal);
        }

        private void DoSendNotice(int connId, int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle, int iWorkoutState, string strFileName)
        {
            m_adapter.AddNotice(iCount, iId, userName, contentPath, iPageId, strTitle, iWorkoutState, strFileName);
        }

        private void DoKeepAlive(int connId, string userName)
        {
            if (!m_isClosing && !IsUserAlreadyOnline(userName))
            {
                string passWord = "";
                string fullName = "";
                int userId = m_adapter.GetUserInfo(userName, ref passWord, ref fullName);
                if (userId >= 0)
                    DoLogin(connId, userName, passWord, "");
            }
        }


        private void DoTransferStart(int connId,string typeName, string name, int cnt, int id, string fileName , int fileSize)
        {
            ConnectionState user = GetConnectedUser(connId);
            if (user != null)
            {
                if (typeName == "NOTICES")
                {
                    string strFilePath = m_adapter.GetNoticesDirectory(user.Name)+"\\cache";
                    if (strFilePath.Length>0)
                    {
                        if (!Directory.Exists(strFilePath))
                            Directory.CreateDirectory(strFilePath);
                        m_transferData = new CTSClientTransferData(typeName, name, cnt, id,
                                             String.Format("{0}\\{1}", strFilePath, fileName), fileSize);
                    }
                }

                m_transferData.Start();
                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.TransferStart,typeName,name, m_transferData);
                m_parent.FireEvent(gea);
            }
        }

        private void DoTransferEnd(int connId, string typeName, string name, int cnt, int id)
        {
            if (m_transferData != null)
            {
                m_transferData.PackageId = id;
                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.TransferEnd, m_transferData.TypeName, m_transferData.Name,m_transferData);
                m_parent.FireEvent(gea);

                if (m_jobDone != null)
                    if (cnt == 0 || m_transferData.IsLastPackage())
                        m_jobDone.Set();

                m_transferData.Stop();
                m_transferData = null;
            }
            else
            {
                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.TransferEnd, typeName,name,null);
                m_parent.FireEvent(gea);

                if (m_jobDone != null)
                    m_jobDone.Set();
            }
        }



        private bool SendData(int connId, string sData)
        {
            ConnectionState user = GetConnectedUser(connId);
            if (user.WorkSocket == null || !user.WorkSocket.Connected)
                return false;

            int dataToSend = sData.Length;
            try
            {
                Byte[] aData = System.Text.UTF8Encoding.UTF8.GetBytes(sData);

                Send(user.WorkSocket,aData);

                AddMessage(String.Format("connection[{0}]", connId), "-->" + sData);
            }
            catch (Exception e)
            {
                ForceDisconnect(connId);
            }
            return true;
        }

        private void SendToAll(string message, IEnumerable<ConnectionState> users = null)
        {
            if (users == null)
            {
                foreach (var u in m_dUsers.Keys)
                {
                    SendData(u.Id,message);
                }
            }
            else
            {
                foreach (var u in m_dUsers.Keys.Where(users.Contains))
                {
                    SendData(u.Id, message);
                }
            }
        }

        private void ForceDisconnect(int connId)
        {
            Disconnect(connId);
        }

        private void ForceDisconnect(string userName)
        {
            ConnectionState user = GetConnectedUser(userName);
            if (user!=null)
                Disconnect(user.Id);
        }

        private void Disconnect(int connId)
        {
            lock (criticalObjConnection)
            {
                ConnectionState user = GetConnectedUser(connId);
                if (user != null)
                {
                    string userName = user.Name;
                    if (userName!=null && user.Name.IndexOf('@') > 0)
                        userName = user.Name.Substring(0, user.Name.IndexOf('@'));


                    // Benutzer überhaupt angemeldet?
                    if (userName!=null && userName.Length>0 && !m_isClosing)
                    {
                        // Benutzer an allen anderen abmelden
                        foreach (var u in m_dUsers.Keys)
                        {
                            if (u.Name != null && u.Name != userName)
                                SendData(u.Id, String.Format("#LOGOUT{{{0}|{1}}}", userName, 0));
                        }

                        CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.Logout,user.WorkSocket.RemoteEndPoint.ToString(),userName,"","");
                        m_parent.FireEvent(gea);
                    }

                    AddMessage(String.Format("connection[{0}]: ", connId), "Disconnected");
                    
                    user.ClientDone.Set();

                    string trash; // Concurrent dictionaries make things weird
                    m_dUsers.TryRemove(user, out trash);
                    m_dataFilter[user.Id] = null;
                }
            }
        }


        private bool SendData(int connId, Byte[] aData)
        {
            int dataToSend = aData.Length;
            try
            {
                ConnectionState user = GetConnectedUser(connId);

                Send(user.WorkSocket, aData);

                AddMessage(String.Format("ConnId[{0}]", connId), "-->" + aData.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                ForceDisconnect(connId);
            }
            return true;
        }

        private void SendFile(int connId, string typeName, string name, int cnt, int id, string fileName)
        {
            if (File.Exists(fileName))
            {
                System.IO.StreamReader reader = new StreamReader(fileName);
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

                SendData(connId, String.Format("#TRANSFERSTART{{{0}|{1}|{2}|{3}|{4}|{5}}}", typeName, name, cnt, id, fi.Name, reader.BaseStream.Length));

                char[] aBuf = new char[reader.BaseStream.Length + 1];
                int bytesRead = 0;
                while ((bytesRead = reader.Read(aBuf, 0, aBuf.Length)) > 0)
                {
                    Byte[] byteData = System.Text.UTF8Encoding.UTF8.GetBytes(aBuf, 0, bytesRead);
                    SendData(connId, byteData);
                }

                SendData(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, cnt, id));
                reader.Close();
            }
        }


        private bool IsUserAlreadyOnline(string userName)
        {
            try
            {
                var u = m_dUsers.Keys.Where(o => o.Name == userName).Single();
                return u != null;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }


        private void AsyncCallback_Accept(IAsyncResult ar) 
        {
            if (m_serverDone.WaitOne(0))
                return;

            // Signal the main thread to continue.
            m_acceptDone.Set();

            // Get the socket that handles the client request.
            Socket handler = m_listener.EndAccept(ar);
            ConnectionState user=GetConnectedUser(handler.RemoteEndPoint);
            AddMessage("Server",String.Format("accept connection with {0}",handler.RemoteEndPoint.ToString()));
            if (user == null)
            {
                // Create the state object.
                user = new ConnectionState(handler, ++m_actConnectionId);
                m_dUsers.TryAdd(user, String.Empty);
                m_dataFilter[user.Id] = new CTSDataFilter();

                AddMessage("Server", String.Format("connection added with {0}", handler.RemoteEndPoint.ToString()));

                user.ClientDone.Reset();

                while (true)
                {
                    user.ReceiveDone.Reset();

                    AddMessage(String.Format("Connection[{0}]",user.Id),"wait for data");
                    user.WorkSocket.BeginReceive(user.Buffer, 0, ConnectionState.BufferSize, 0, new AsyncCallback(AsyncCallBack_Read), user);

                    int id = WaitHandle.WaitAny(new WaitHandle[] { user.ReceiveDone, user.ClientDone });
                    if (id == 1)
                        break;
                }

                Disconnect(user.Id);
            }
        }

        public void AsyncCallBack_Read(IAsyncResult ar) 
        {
            ConnectionState user = (ConnectionState)ar.AsyncState;
            try
            {
	            String content = String.Empty;
	            // Retrieve the state object and the handler socket
	            // from the asynchronous state object.
	            Socket handler = user.WorkSocket;
	
	            // Read data from the client socket. 
	            int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    AddMessage(String.Format("Connection[{0}]", user.Id), String.Format("{0} bytes read on {1}", bytesRead, handler.RemoteEndPoint.ToString()));

                    byte[] l_aBytes = new Byte[bytesRead];
                    Array.Copy(user.Buffer, l_aBytes, bytesRead);
                    // There  might be more data, so store the data received so far.
                    //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //m_commandString = "#TRANSFEREND{NOTICES|*|5|4}#SENDNOTICE{}";
                    if (m_dataFilter[user.Id]!=null && m_dataFilter[user.Id].AnalyseData(l_aBytes, ref m_commandString))
                        CheckReceivedCommand(user.Id, ref m_commandString);
                    user.ReceiveDone.Set();
                }
                else
                    user.ClientDone.Set();
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                AddMessage(String.Format("Connection[{0}]", user.Id), ex.Message);
                user.ClientDone.Set();
            }
        }


        private void Send(Socket handler, String data) 
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(AsyncCallBack_Send), handler);
        }

        private void Send(Socket handler, Byte [] aData)
        {
            // Begin sending the data to the remote device.
            handler.BeginSend(aData, 0, aData.Length, 0, new AsyncCallback(AsyncCallBack_Send), handler);
        }


        private void AsyncCallBack_Send(IAsyncResult ar) 
        {
            try 
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket) ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private ConnectionState GetConnectedUser(int iId)
        {
	        try
	        {
		        var u = m_dUsers.Keys.Where(o => o.Id == iId).Single();
                return u;
	        }
	        catch (System.Exception ex)
	        {
	        	return null;
	        }
        }

        private ConnectionState GetConnectedUser(EndPoint endPoint)
        {
            try
            {
                var u = m_dUsers.Keys.Where(o => (o.WorkSocket.RemoteEndPoint == endPoint)).Single();
                return u;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private ConnectionState GetConnectedUser(string strUserName)
        {
            try
            {
	            var u = m_dUsers.Keys.Where(o => o.Name == strUserName).Single();
	            return u;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private string GetConnectedUserName(int iId)
        {
            try
            {
                var u = m_dUsers.Keys.Where(o => o.Id == iId).Single();
                if (u!=null)
                    return u.Name;
                return "";
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }

        public static IPAddress GetIPAddress(string hostname)
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(hostname);

            var iCnt = host.AddressList.Where(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            if (iCnt.Count() > 1)
            {
                FrmSelectServer frm = new FrmSelectServer();
                frm.ShowDialog();
                return frm.SelIPAddress;
            }

            foreach (IPAddress ip in host.AddressList)
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip;
            return null;
        }
    }
}
