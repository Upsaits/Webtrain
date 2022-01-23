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
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.ClientServer
{
    #region ConnectionStateServer
    // State object for reading client data asynchronously
    public class ConnectionStateServer
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int cBufferSize = 1048576;
        // Receive buffer.
        public byte[] buffer = new byte[cBufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
        public int dataLength = 0;
        public byte type = 0;
        public int actDataLength = 0;
        public string m_commandString="";
        public ManualResetEvent receiveTypeAndLengthDone = new ManualResetEvent(false);
        public ManualResetEvent receiveDataDone = new ManualResetEvent(false);
        public ManualResetEvent clientDone = new ManualResetEvent(false);
        public string name;
        public int id;
        public readonly Semaphore semaphor = new Semaphore(1, 1);
        public ConnectionStateServer(Socket _workSocket, int _id)
        {
            workSocket = _workSocket;
            id = _id;
        }

        public void Reset()
        {
            type = 0;
            dataLength = 0;
            actDataLength = 0;
            m_commandString = "";
            sb.Clear();
        }
    }
    #endregion

    public class CTSSocketServerManager : ICTSServerManager
    {
        #region members
        private ICTSServerAdapter m_adapter = null;
        private bool m_isClosing = false;
        private CTSServerManagerBridge m_parent = null;
        private IPEndPoint m_localEndPoint=null;
        private Socket m_listener;
        private ConcurrentDictionary<ConnectionStateServer, string> m_dUsers = new ConcurrentDictionary<ConnectionStateServer, string>();
        private AutoResetEvent m_jobDone;
        private CTSClientTransferData m_transferData;

        // static members
        private static object criticalObjConnection = new object();
        private static int m_actConnectionId = 0;
        private static ManualResetEvent m_acceptDone = new ManualResetEvent(false);
        private static ManualResetEvent m_serverDone = new ManualResetEvent(false);
        private static ManualResetEvent m_evtHeaderSent = new ManualResetEvent(false);
        private static ManualResetEvent m_evtDataSent = new ManualResetEvent(false);
        private static CTSDataFilter[] m_dataFilter = new CTSDataFilter[1000];
        //private string m_commandString;
        public const int cFileSendBufferSize = 262144; // 256 kB
        private Thread m_connectThread = null;
        #endregion

        public CTSSocketServerManager() 
        {
        }

        public void AddMessage(string sMessage)
        {
            m_adapter.AddConsoleMessage(sMessage);
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

                foreach (var u in m_dUsers.Keys)
                    Disconnect(u.id);

                m_dUsers.Clear();

                try
                {
                    m_serverDone.Set();
                    Thread.Sleep(100);
                    //m_listener.Shutdown(SocketShutdown.Both);
                    m_connectThread.Abort();
                    m_listener.Close();
                }
                catch (Exception /*ex*/)
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

        public int GetUsersOnline()
        {
            return m_dUsers.Count;
        }

        public void DisconnectUser(string userName)
        {
            ForceDisconnect(userName);
        }

        public void SendClassMessage(int roomId, string toUserName, string message)
        {
            ConnectionStateServer user = GetConnectedUser(toUserName);
            if (user!=null)
                SendString(user.id, String.Format("#RECEIVECLASSMESSAGE{{{0}|{1}|{2}}}", roomId.ToString(), m_adapter.GetUserName(), message));
        }

        public void SendNoticeWorkoutState(string strTitle, int iWorkedOutState, string toUserName)
        {
            ConnectionStateServer user = GetConnectedUser(toUserName);
            if (user != null)
                SendString(user.id, String.Format("#SENDNOTICEWORKOUTSTATE{{{0}|{1}}}", strTitle, iWorkedOutState.ToString()));
        }


        public void SendNotice(string toUserName,string noticeTitle)
        {
            ConnectionStateServer user = GetConnectedUser(toUserName);
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
                        SendFile(user.id,"NOTICES", ni.title, 1, 1, filePath);
                        SendString(user.id, String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}}}", 1, 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName),ni.ModificationDateString));
                    }
                }
            }
        }

        public void SendTransferRequest(string toUserName, string typeName, string name)
        {
            ConnectionStateServer user = GetConnectedUser(toUserName);
            if (user != null)
                SendString(user.id, String.Format("#TRANSFERREQUEST{{{0}|{1}}}", typeName, name));
        }

        public void SendLogout(string toUserName)
        {
            ConnectionStateServer user = GetConnectedUser(toUserName);
            if (user != null)
            {
                SendString(user.id, String.Format("#LOGOUT{{{0}|{1}}}", m_adapter.GetUserName(), 0)); // Server-user abmelden
                SendString(user.id, String.Format("#LOGOUT{{{0}|{1}}}", user.name, 0)); // User abmelden
            }

        }

        public void SendLogoutAllUsers()
        {
            // Alle User ausloggen !
            foreach (var u in m_dUsers.Keys)
            {
                SendString(u.id, String.Format("#LOGOUT{{{0}|{1}}}", m_adapter.GetUserName(), 0)); // Server-user abmelden
                SendString(u.id, String.Format("#LOGOUT{{{0}|{1}}}", u.name, 0)); // User abmelden
            }
        }

        public void SetAdapter(ICTSServerAdapter adapter)
        {
            m_adapter = adapter;
            m_adapter.SetParentCTSServerManager(this);
        }

        public ICTSServerAdapter GetAdapter()
        {
            return m_adapter;
        }

        public IPAddress GetIPAddress()
        {
            if (m_localEndPoint != null)
                return m_localEndPoint.Address;
            return null;
        }
        #endregion

        #region connection methods
        /// <summary>
        /// Connect the current instance of command client to the server.This method throws ServerNotFoundException on failure.
        /// Run this method and handle the 'ConnectingSuccessed' and 'ConnectingFailed' to get the connection state.
        /// </summary>
        private bool ConnectServer(int portId)
        {
            IPAddress ipAdr = SelectIPAddress();
            if (ipAdr == null)
            {
                AddMessage(String.Format("couldn't find IP Adress"));
                return false;
            }

            m_localEndPoint = new IPEndPoint(ipAdr, portId);
            m_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            m_serverDone.Reset();
            
            try
            {
                String strTxt = String.Format("Try to connect to {0}", m_localEndPoint.ToString());
                AddMessage(strTxt);

                m_listener.Bind(m_localEndPoint);
                m_listener.Listen(100);

                ThreadStart connectThreadStart = new ThreadStart(ConnectThreadStarter);
                m_connectThread = new Thread(connectThreadStart);
                m_connectThread.Priority = ThreadPriority.BelowNormal;
                m_connectThread.Start();
            }
            catch (Exception /*ex*/)
            {
                AddMessage(String.Format("couldn't connect to {0}", m_localEndPoint.ToString()));
                return false;
            }

            AddMessage(String.Format("successfully connected to {0}", m_localEndPoint.ToString()));
            CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.ConnectionInfo, m_localEndPoint.ToString(),true);
            m_parent.FireEvent(gea);

            return true;
        }

        private void ConnectThreadStarter()
        {
            while (true)
            {
                //Set the event to nonsignaled state.
                m_acceptDone.Reset();

                // Start an asynchronous socket to listen for connections.
                m_listener.BeginAccept(new AsyncCallback(AsyncCallback_Accept), m_listener);

                AddMessage("Waiting for connection..");
                // Wait until a connection is made before continuing.
                int id = WaitHandle.WaitAny(new WaitHandle[] { m_acceptDone, m_serverDone });
                if (id == 1)
                {
                    break;
                }
            }
            AddMessage("Shutdown initiated");
        }

        private void ForceDisconnect(int connId)
        {
            Disconnect(connId);
        }

        private void ForceDisconnect(string userName)
        {
            ConnectionStateServer user = GetConnectedUser(userName);
            if (user != null)
                Disconnect(user.id);
        }

        private void Disconnect(int connId)
        {
            lock (criticalObjConnection)
            {
                ConnectionStateServer user = GetConnectedUser(connId);
                if (user != null)
                {
                    string userName = user.name;
                    if (userName != null && user.name.IndexOf('@') > 0)
                        userName = user.name.Substring(0, user.name.IndexOf('@'));


                    // Benutzer überhaupt angemeldet?
                    if (userName != null && userName.Length > 0 && !m_isClosing)
                    {
                        // Benutzer an allen anderen abmelden
                        foreach (var u in m_dUsers.Keys)
                        {
                            if (u.name != null && u.name != userName)
                                SendString(u.id, String.Format("#LOGOUT{{{0}|{1}}}", userName, 0));
                        }

                        CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.Logout, user.workSocket.RemoteEndPoint.ToString(), userName, "", "");
                        m_parent.FireEvent(gea);
                    }

                    String strTxt = String.Format("connection[{0}]: Disconnected", connId);
                    AddMessage(strTxt);

                    user.clientDone.Set();

                    string trash; // Concurrent dictionaries make things weird
                    m_dUsers.TryRemove(user, out trash);
                    m_dataFilter[user.id] = null;
                }
            }
        }

        private ConnectionStateServer GetConnectedUser(int iId)
        {
            try
            {
                var u = m_dUsers.Keys.Single(o => o.id == iId);
                return u;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        private ConnectionStateServer GetConnectedUser(EndPoint endPoint)
        {
            try
            {
                var u = m_dUsers.Keys.Single(o => (o.workSocket.RemoteEndPoint == endPoint));
                return u;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        private ConnectionStateServer GetConnectedUser(string strUserName)
        {
            try
            {
                var u = m_dUsers.Keys.Single(o => o.name == strUserName);
                return u;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        private string GetConnectedUserName(int iId)
        {
            try
            {
                var u = m_dUsers.Keys.Single(o => o.id == iId);
                if (u != null)
                    return u.name;
                return "";
            }
            catch (Exception /*ex*/)
            {
                return "";
            }
        }

        private IPAddress SelectIPAddress()
        {
            return m_adapter.SelectIPAddress();
        }

        private bool IsUserAlreadyOnline(string userName)
        {
            try
            {
                var u = m_dUsers.Keys.Where(o => o.name == userName).Single();
                return u != null;
            }
            catch (Exception /*ex*/)
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
            ConnectionStateServer user = GetConnectedUser(handler.RemoteEndPoint);

            String strTxt = String.Format("accept connection with {0}", handler.RemoteEndPoint.ToString());
            AddMessage(strTxt);

            if (user == null)
            {
                // Create the state object.
                user = new ConnectionStateServer(handler, ++m_actConnectionId);
                m_dUsers.TryAdd(user, String.Empty);
                m_dataFilter[user.id] = new CTSDataFilter();

                strTxt = String.Format("connection added with {0}", handler.RemoteEndPoint.ToString());
                AddMessage(strTxt);

                user.clientDone.Reset();

                while (!(user.workSocket.Poll(0, SelectMode.SelectRead) && user.workSocket.Available == 0))
                {
                    user.m_commandString = "";

                    if (user.workSocket.Available > 0)
                    {
                        try
                        {
                            if (user.dataLength > 0)
                            {
                                user.receiveDataDone.Reset();
                                user.workSocket.BeginReceive(user.buffer, user.actDataLength, user.dataLength - user.actDataLength, 0, new AsyncCallback(ACB_ReceiveData), user);
                                user.receiveDataDone.WaitOne();
                            }
                            else
                            {
                                // Begin receiving the data from the remote device.
                                user.receiveTypeAndLengthDone.Reset();
                                user.workSocket.BeginReceive(user.buffer, 0, 5, 0, new AsyncCallback(ACB_ReceiveTypeAndLength), user);
                                user.receiveTypeAndLengthDone.WaitOne();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            strTxt = String.Format("Connection[{0}]: Unexpected Error={1}", user.id, ex.Message);
                            AddMessage(strTxt);
                            user.clientDone.Set();
                        }

                        int id = WaitHandle.WaitAny(new WaitHandle[] { user.clientDone, user.receiveTypeAndLengthDone, user.receiveDataDone });
                        if (id == 0)
                            break;
                    }
                    Thread.Sleep(100);
                }

                Disconnect(user.id);
            }
        }

        #endregion

        #region sending methods

        private void Send(ConnectionStateServer user, Socket handler, String data)
        {
            byte[] aTypeAndSize = new byte[5];
            aTypeAndSize[0] = 0;

            // Convert the string data to byte data using UTF8 encoding.
            byte[] byteData = Encoding.Unicode.GetBytes(data);

            byte[] aLength = BitConverter.GetBytes(byteData.Length);
            Array.Copy(aLength, 0, aTypeAndSize, 1, aLength.Length);

            m_evtHeaderSent.Reset();
            handler.BeginSend(aTypeAndSize, 0, 5, 0, new AsyncCallback(ACB_SendTypeAndLength), handler);
            m_evtHeaderSent.WaitOne();

            // Begin sending the data to the remote device.
            m_evtDataSent.Reset();
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(ACB_SendData), handler);
            m_evtDataSent.WaitOne();
        }

        private void Send(ConnectionStateServer user, Socket handler, Byte[] aData, int iDataLength)
        {
            byte[] aTypeAndSize = new byte[5];
            aTypeAndSize[0] = 1;
            byte[] aLength = BitConverter.GetBytes(iDataLength);
            //Array.Reverse(aLength);
            Array.Copy(aLength, 0, aTypeAndSize, 1, aLength.Length);

            m_evtHeaderSent.Reset();
            handler.BeginSend(aTypeAndSize, 0, 5, 0, new AsyncCallback(ACB_SendTypeAndLength), handler);
            m_evtHeaderSent.WaitOne();

            // Begin sending the data to the remote device.
            m_evtDataSent.Reset();
            handler.BeginSend(aData, 0, iDataLength, 0, new AsyncCallback(ACB_SendData), handler);
            m_evtDataSent.WaitOne();
        }

        private void ACB_SendTypeAndLength(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                string strMsg = String.Format("ConnId-{0}: Sent {1} bytes to client.", handler.Handle, bytesSent);
                AddMessage(strMsg);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            m_evtHeaderSent.Set();
        }

        private void ACB_SendData(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                string strMsg = String.Format("ConnId-{0}: Sent {1} bytes to client.", handler.Handle, bytesSent);
                AddMessage(strMsg);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            m_evtDataSent.Set();
        }
       
        private bool SendString(int connId, string sData)
        {
            ConnectionStateServer user = GetConnectedUser(connId);
            if (user==null || user.workSocket == null || !user.workSocket.Connected)
                return false;
            try
            {
                user.semaphor.WaitOne();
                Send(user, user.workSocket, sData);
                user.semaphor.Release();

                String strTxt = String.Format("connection[{0}]--> {1}", connId, sData);
                AddMessage(strTxt);
            }
            catch (Exception /*ex*/)
            {
                user.semaphor.Release();
                ForceDisconnect(connId);
            }
            return true;
        }

        private bool SendBytes(int connId, Byte[] aData, int iDataLength)
        {
            try
            {
                ConnectionStateServer user = GetConnectedUser(connId);
                if (user != null)
                {
                    user.semaphor.WaitOne();
                    Send(user, user.workSocket, aData, iDataLength);
                    user.semaphor.Release();

                    String strTxt = String.Format("connection[{0}]--> Bytes{1}", connId, iDataLength);
                    AddMessage(strTxt);
                    return true;
                }
                else
                {
                    ForceDisconnect(connId);
                }
            }
            catch (Exception ex)
            {
                String strTxt = String.Format("connection[{0}]: Error={0}", ex.Message);
                AddMessage(strTxt);
                ForceDisconnect(connId);
            }
            return false;
        }

        private void SendToAll(string message, IEnumerable<ConnectionStateServer> users = null)
        {
            if (users == null)
            {
                foreach (var u in m_dUsers.Keys)
                {
                    SendString(u.id,message);
                }
            }
            else
            {
                foreach (var u in m_dUsers.Keys.Where(users.Contains))
                {
                    SendString(u.id, message);
                }
            }
        }


        private void SendFile(int connId, string typeName, string name, int cnt, int id, string fileName)
        {
            if (!File.Exists(fileName)) 
                return;

            FileInfo fi = new FileInfo(fileName);

            SendString(connId, String.Format("#TRANSFERSTART{{{0}|{1}|{2}|{3}|{4}|{5}}}", typeName, name, cnt, id, fi.Name, fi.Length));

            int iRead = 0;
            Stream inStream = File.Open(fileName, FileMode.Open,FileAccess.Read,FileShare.Read);
            while (inStream.Position < inStream.Length)
            {
                Byte[] buf = new Byte[cFileSendBufferSize];
                int cntRead = inStream.Read(buf, 0, cFileSendBufferSize);
                if (cntRead > 0)
                {
                    if (!SendBytes(connId, buf, cntRead))
                        break;
                    iRead += cntRead;
                    Thread.Sleep(20);
                    //Console.WriteLine(String.Format("Daten zum Client gesendet:{0} ,Gesamtdaten: {1}", cntRead, iRead));
                }
            }
            inStream.Close();

            SendString(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, cnt, id));
        }
        #endregion

        #region receiving methods

        private void ACB_ReceiveTypeAndLength(IAsyncResult ar)
        {
            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            ConnectionStateServer user = (ConnectionStateServer)ar.AsyncState;
            try
            {
                if (user != null && user.workSocket.Connected)
                {
                    // Read data from the remote device.
                    int bytesRead = user.workSocket.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        user.type = user.buffer[0];
                        user.dataLength = BitConverter.ToInt32(user.buffer, 1);
                        if (user.type > 1 || (user.dataLength < 0 || user.dataLength > cFileSendBufferSize))
                        {
                            m_adapter.AddConsoleMessage("Error: unknown Type-id!");
                        }
                        else
                        {
                            string sTxt = String.Format("received datalength (Type={0},Length={1})", user.type, user.dataLength);
                            m_adapter.AddConsoleMessage(sTxt);
                            user.actDataLength = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_adapter.AddConsoleMessage(String.Format("ReceiveTypeAndLength: Error={0}", ex.Message));
            }
            finally
            {
                user.receiveTypeAndLengthDone.Set();
            }
        }

        private void ACB_ReceiveData(IAsyncResult ar)
        {
            ConnectionStateServer user = (ConnectionStateServer)ar.AsyncState;
            try
            {
                Socket client = user.workSocket;
                if (user != null && client.Connected)
                {
                    // Read data from the remote device.
                    int bytesRead = client.EndReceive(ar);

                    string sTxt = String.Format("received data (Type={0},Length={0},Received={1},ActualData={2})", user.type, user.dataLength, bytesRead, user.actDataLength);
                    m_adapter.AddConsoleMessage(sTxt);
                    user.actDataLength += bytesRead;

                    if (user.actDataLength < user.dataLength)
                    {
                        sTxt = String.Format("some more data (ActualData={0})", user.type, user.dataLength, bytesRead, user.actDataLength);
                        m_adapter.AddConsoleMessage(sTxt);
                    }
                    else
                    {
                        //state.dataLength = bytesRead;
                        sTxt = String.Format("received all data (Type={0},Length={1},ActualData={2})", user.type, user.dataLength, user.actDataLength);
                        m_adapter.AddConsoleMessage(sTxt);
                        ProcessMessage(user);
                        user.dataLength = 0;
                        user.type = 0;
                        user.actDataLength = 0;
                    }
                }
            }
            catch (System.Exception ex)
            {
                m_adapter.AddConsoleMessage(String.Format("ReceiveData: Error={0}", ex.Message));
            }
            finally
            {
                user.receiveDataDone.Set();
            }

        }

        private void ProcessMessage(ConnectionStateServer user)
        {
            if (user.type == 0)
            {
                user.sb.AppendFormat("{0}", Encoding.Unicode.GetString(user.buffer, 0, user.dataLength));
                if (user.sb.Length > 0 &&
                    m_dataFilter[user.id].AnalyseData(user.sb.ToString(), out user.m_commandString) &&
                    user.m_commandString.Length > 0)
                {
                    int cmdStartFound = user.m_commandString.IndexOf('#');
                    if (cmdStartFound >= 0)
                    {
                        AddMessage(String.Format("++++ ProcessMessage({0})", user.m_commandString));
                        CheckReceivedCommand(user.id, ref user.m_commandString);
                    }
                    user.sb.Clear();
                }
            }
            else if (user.type == 1)
            {
                if (m_transferData.IsActive)
                    m_transferData.DoTransfer(user.buffer, user.dataLength);
            }
        }

        private void CheckReceivedCommand(int connId, ref string commandString)
        {
            string dataString = "";  // Zunächst keine Daten im Stream

            // Prüfen ob irgendwo im Stream Kommandos vorkommen
            if (commandString.IndexOf("#TRANSFERSTART") != 0 &&
                commandString.IndexOf("#TRANSFEREND") != 0 &&
                commandString.IndexOf("#LOGIN") != 0 &&
                commandString.IndexOf("#TRANSFERREQUEST") != 0 &&
                commandString.IndexOf("#SENDCLASSMESSAGE") != 0 &&
                commandString.IndexOf("#ASKTESTPERMISSION") != 0 &&
                commandString.IndexOf("#ASKCHANGEPASSWORD") != 0 &&
                commandString.IndexOf("#ASKMAPPROGRESS") != 0 &&
                commandString.IndexOf("#ASKWORKPROGRESS") != 0 &&
                commandString.IndexOf("#SENDTESTRESULT") != 0 &&
                commandString.IndexOf("#KEEPALIVE") != 0 &&
                commandString.IndexOf("#SENDUSERPROGRESSINFO") != 0 &&
                commandString.IndexOf("#SENDNOTICEWORKOUTSTATE") != 0 &&
                commandString.IndexOf("#SENDNOTICE") != 0)
            {
                // Daten vor dem Kommando?
                int found = 0;
                if ((found = commandString.IndexOf("#TRANSFER")) > 0)
                {
                    dataString = commandString.Substring(0, found);
                    commandString = commandString.Substring(found);
                }
                // Keine Kommandos gefunden?
                else if (found < 0)
                {
                    dataString = commandString;
                    commandString = "";
                }
                // Nur Kommando 
                else
                {
                    dataString = "";
                }
            }

            if (commandString.Length > 0)
                if (!DoCommand(connId, ref commandString))
                    return;

            if (commandString.Length > 0)
                CheckReceivedCommand(connId, ref commandString);
        }

        private bool DoCommand(int connId, ref string commandString)
        {
            string cmd;
            int found = commandString.IndexOf("}", StringComparison.Ordinal);
            if (found >= 0 && found <= (commandString.Length - 1))
            {
                cmd = commandString.Substring(0, found + 1);
                commandString = commandString.Substring(found + 1);
            }
            else
                return false;

            //Trace.WriteLine("-->" + cmd);
            String strTxt = String.Format("connection[{0}]<-- {1}", connId, cmd);
            AddMessage(strTxt);

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
                        DoAskTestPermission(connId, aTokens[0], aTokens[1],aTokens[2]);
                    else if (aCommands[0] == "#ASKMAPPROGRESS")
                        DoAskMapProgress(connId, aTokens[0], aTokens[1]);
                    else if (aCommands[0] == "#ASKWORKPROGRESS")
                        DoAskWorkProgress(connId, aTokens[0], aTokens[1], aTokens[2]);
                    else if (aCommands[0] == "#ASKCHANGEPASSWORD")
                        DoAskChangePassword(connId, aTokens[0], aTokens[1]);
                    else if (aCommands[0] == "#SENDTESTRESULT")
                        DoSendTestResult(connId, aTokens[0]);
                    else if (aCommands[0] == "#KEEPALIVE")
                        DoKeepAlive(connId, aTokens[0]);
                    else if (aCommands[0] == "#SENDUSERPROGRESSINFO")
                        DoSendUserProgressInfo(connId, aTokens[0], aTokens[1], Int32.Parse(aTokens[2]), ushort.Parse(aTokens[3]));
                    else if (aCommands[0] == "#SENDNOTICEWORKOUTSTATE")
                        DoSendNoticeWorkoutState(connId, aTokens[0], aTokens[1], Int32.Parse(aTokens[2]));
                    else if (aCommands[0] == "#SENDNOTICE")
                        DoSendNotice(connId, Int32.Parse(aTokens[0]), Int32.Parse(aTokens[1]), aTokens[2], aTokens[3], Int32.Parse(aTokens[4]), aTokens[5], Int32.Parse(aTokens[6]), aTokens[7], aTokens[8]);
                    else if (aCommands[0] == "#TRANSFERSTART")
                        DoTransferStart(connId, aTokens[0], aTokens[1], Int32.Parse(aTokens[2]), Int32.Parse(aTokens[3]), aTokens[4], Int32.Parse(aTokens[5]));
                    else if (aCommands[0] == "#TRANSFEREND")
                        DoTransferEnd(connId, aTokens[0], aTokens[1], Int32.Parse(aTokens[2]), Int32.Parse(aTokens[3]));

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
                    strUserName = String.Format("admin@{0}", m_dUsers.Count + 1);

                if (!IsUserAlreadyOnline(strUserName))
                {
                    ConnectionStateServer user = GetConnectedUser(connId);
                    string strTarget = user.workSocket.RemoteEndPoint.ToString();
                    CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.Login, strTarget, strUserName, strPassword, language);

                    m_jobDone.Reset();

                    m_parent.FireEvent(gea);

                    if (!m_jobDone.WaitOne(m_adapter.GetTimeoutStandard()))
                        return;

                    if (gea.ReturnValue == 0)
                    {
                        AddMessage(String.Format("REGISTERSTUDENT({0})", strUserName));
                        // Login ok senden
                        SendString(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, 0));

                        user.name = strUserName;

                        // Dem Neuen User alle anderen mitteilen
                        foreach (var u in m_dUsers.Keys)
                        {
                            if (u.name != null && u.name != userName)
                                SendString(u.id, String.Format("#LOGIN{{{0}|{1}}}", userName, 0));
                        }

                        // Neuer Verbindung Server mitteilen
                        SendString(connId, String.Format("#LOGIN{{{0}|{1}}}", m_adapter.GetUserName(), 0));
                    }
                    else
                        SendString(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, gea.ReturnValue));
                }
                else
                    SendString(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, 3));
            }
            catch (Exception e)
            {
                SendString(connId, String.Format("#LOGIN{{{0}|{1}}}", userName, 5));
                String strTxt = String.Format("connection[{0}]-Error{1}", connId, e.Message);
                AddMessage(strTxt);
            }
        }

        private void DoSendClassMessage(int connId, int roomId, string toUserName, string message)
        {
            if (m_adapter.GetUserName() == toUserName)
            {
                ReceiveClassMessageDelegate delReceiveClassMessage = m_adapter.GetDelegateReceiveClassMessage();
                string userName = GetConnectedUserName(connId);
                delReceiveClassMessage(roomId, userName, message);
            }
            else
            {
                ConnectionStateServer user = GetConnectedUser(toUserName);
                if (user != null)
                    SendString(user.id, String.Format("#RECEIVECLASSMESSAGE{{{0}|{1}|{2}}}", roomId.ToString(), toUserName, message));
            }
        }

        private void DoTransferRequest(int connId, string typeName, string name)
        {
            AddMessage(String.Format("+++++ DoTransferRequest(connId:{0},typename: {1}, name: {2})", connId, typeName, name));
            if (typeName == "USERLIST")
            {
                SendFile(connId, typeName, name, 1, 1, m_adapter.GetUserFileName());
            }
            else if (typeName == "CLASSES")
            {
                string userName = GetConnectedUserName(connId);
                if (userName.IndexOf('@') > 0)
                    userName = userName.Substring(0, userName.IndexOf('@'));

                if (name == "*")
                {
                    string fileName = m_adapter.GetClassFileName();
                    AddMessage(String.Format("+++++ Sendfile({0})", fileName));
                    SendFile(connId, typeName, name, 1, 1, fileName);
                }
            }
            else if (typeName == "LEARNMAPS")
            {
                string userName = GetConnectedUserName(connId);
                if (userName.IndexOf('@') > 0)
                    userName = userName.Substring(0, userName.IndexOf('@'));

                int mapsSent = 0;
                int mapId = 1;
                int mapCnt = m_adapter.GetMapCnt();
                if (name == "*")
                {
                    // send all learnmaps
                    for (int i = 0; i < mapCnt; ++i)
                    {
                        string fileName = m_adapter.GetMapFileName(i);
                        SendFile(connId, typeName, name, mapCnt, mapId, fileName);
                        ++mapsSent;
                        ++mapId;
                    }
                }
                else
                {
                    int mapsFound = 0;
                    for (int i = 0; i < mapCnt; ++i)
                    {
                        if (userName == "admin" || m_adapter.MapHasUser(i, userName))
                            ++mapsFound;
                    }

                    for (int i = 0; i < mapCnt; ++i)
                    {
                        if (userName == "admin" || m_adapter.MapHasUser(i, userName))
                        {
                            string fileName = m_adapter.GetMapFileName(i);
                            SendFile(connId, typeName, name, mapsFound, mapId, fileName);
                            ++mapsSent;
                            ++mapId;
                        }
                    }
                }

                if (mapsSent == 0)
                    SendString(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
            }
            else if (typeName == "LIBRARIES")
            {
                int libsSent = 0;
                int libId = 1;
                if (name == "*")
                {
                    // send all libraries
                    List<string> aLibNames = new List<string>();
                    int iLibCnt = m_adapter.GetAllLibraryNames(ref aLibNames);
                    foreach (var l in aLibNames)
                    {
                        SendFile(connId, "LIBRARIES", name, iLibCnt, libId, m_adapter.GetLibraryFileName(l));
                        ++libsSent;
                        ++libId;
                    }
                    if (libsSent == 0)
                        SendString(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
                }
                else
                {
                    if (m_adapter.HasLibrary(name))
                        SendFile(connId, "LIBRARIES", name, 1, 1, m_adapter.GetLibraryFileName(name));
                    else
                        SendString(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
                }
            }
            else if (typeName == "DOCUMENTS")
            {
                string userName = GetConnectedUserName(connId);

                int iCount = 0;
                var dDocs = new SortedList<string, Dictionary<string, string>>();
                if (m_adapter.GetDocumentsFileNames(userName, ref dDocs))
                {
                    foreach (var d in dDocs)
                        iCount += d.Value.Count;

                    int id = 0;
                    foreach (var d in dDocs)
                        foreach (var f in d.Value)
                        {
                            SendFile(connId, typeName, d.Key, iCount, ++id, f.Value);
                        }
                }

                if (iCount == 0)
                    SendString(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
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
                            SendString(connId, String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}}}", nic.Count, i + 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName), ni.ModificationDateString));
                            ++iSent;
                        }

                        if (ni.title == name)
                            break;
                    }
                }

                if (iSent == 0)
                    SendString(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
            }
        }

        private void DoAskTestPermission(int connId, string userName, string mapTitle,string testName)
        {
            SendString(connId, String.Format("#TESTPERMISSION{{{0}|{1}|{2}|{3}}}", userName, mapTitle,testName,(m_adapter.AskTestPermission(userName, mapTitle,testName)) ? 1 : 0));
        }

        private void DoAskMapProgress(int connId, string userName, string mapTitle)
        {
            SendString(connId, String.Format("#MAPPROGRESS{{{0}|{1}|{2}}}", userName, mapTitle, m_adapter.AskMapProgress(userName, mapTitle)));
        }

        private void DoAskWorkProgress(int connId, string userName, string mapTitle, string workTitle)
        {
            if (workTitle == "all")
                SendString(connId, String.Format("#WORKPROGRESS{{{0}|{1}|{2}|{3}}}", userName, mapTitle, workTitle, m_adapter.AskWorkProgress(userName, mapTitle)));
            else
                SendString(connId, String.Format("#WORKPROGRESS{{{0}|{1}|{2}|{3}}}", userName, mapTitle, workTitle, m_adapter.AskWorkProgress(userName, mapTitle, workTitle)));
        }

        private void DoAskChangePassword(int connId, string userName, string passWord)
        {
            SendString(connId, String.Format("#PASSWORDCHANGED{{{0}|{1}|{2}}}", userName, passWord, (m_adapter.AskChangePassword(userName, passWord)) ? 1 : 0));
        }

        private void DoSendTestResult(int connId, string sResult)
        {
            m_adapter.SetTestResult(sResult);
        }

        private void DoSendUserProgressInfo(int connId, string sUsername, string workingTitle, int iRegionId, ushort iRegionVal)
        {
            m_adapter.AddUserProgressInfo(sUsername, workingTitle, iRegionId, iRegionVal);
        }

        private void DoSendNoticeWorkoutState(int connId, string sUserName, string strNoticeTitle,int iWorkoutState)
        {
            m_adapter.SetNoticeWorkoutState(sUserName,strNoticeTitle,iWorkoutState);
        }

        private void DoSendNotice(int connId, int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle, int iWorkoutState, string strFileName, string strModificationDate)
        {
            m_adapter.AddNotice(iCount, iId, userName, contentPath, iPageId, strTitle, iWorkoutState, strFileName, strModificationDate);
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


        private void DoTransferStart(int connId, string typeName, string name, int cnt, int id, string fileName, int fileSize)
        {
            ConnectionStateServer user = GetConnectedUser(connId);
            if (user != null)
            {
                if (typeName == "NOTICES")
                {
                    string strFilePath = m_adapter.GetNoticesDirectory(user.name) + "\\cache";
                    if (strFilePath.Length > 0)
                    {
                        if (!Directory.Exists(strFilePath))
                            Directory.CreateDirectory(strFilePath);
                        m_transferData = new CTSClientTransferData(typeName, name, cnt, id,
                                             String.Format("{0}\\{1}", strFilePath, fileName), fileSize);
                    }
                }

                m_transferData.Start();
                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.TransferStart, typeName, name, m_transferData);
                m_parent.FireEvent(gea);
            }
        }

        private void DoTransferEnd(int connId, string typeName, string name, int cnt, int id)
        {
            if (m_transferData != null)
            {
                m_transferData.PackageId = id;
                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.TransferEnd, m_transferData.TypeName, m_transferData.Name, m_transferData);
                m_parent.FireEvent(gea);

                if (m_jobDone != null)
                    if (cnt == 0 || m_transferData.IsLastPackage())
                        m_jobDone.Set();

                m_transferData.Stop();
                m_transferData = null;
            }
            else
            {
                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.TransferEnd, typeName, name, null);
                m_parent.FireEvent(gea);

                if (m_jobDone != null)
                    m_jobDone.Set();
            }
        }

        #endregion

    }
}
