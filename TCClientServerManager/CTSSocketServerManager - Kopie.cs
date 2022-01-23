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

namespace SoftObject.TrainConcept.ClientServer
{
    public class CTSSocketServerManager : ICTSServerManager
    {
        // State object for reading client data asynchronously
        private class StateObject
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            // Received data string.
            public StringBuilder sb = new StringBuilder();
            public string Name = String.Empty;
            public int Id { get; set; }
        }

        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private static CTSDataFilter[] m_dataFilter = new CTSDataFilter[1000];
        private static AutoResetEvent[] m_aRTSEvent = new AutoResetEvent[1000];
        private static string m_commandString = String.Empty;
        private ICTSServerAdapter m_adapter = null;
        private bool m_isClosing = false;
        private CTSServerManagerBridge m_parent = null;

        private ConcurrentDictionary<StateObject, string> m_dUsers = new ConcurrentDictionary<StateObject, string>();

        public CTSSocketServerManager() 
        {
        }

          
        public void Create(int portId, ref AutoResetEvent jobDone)
        {
            using (BackgroundWorker bwConnector = new BackgroundWorker())
            {
                bwConnector.DoWork += bwConnector_DoWork;
                bwConnector.RunWorkerCompleted += bwConnector_RunWorkerCompleted;
                bwConnector.RunWorkerAsync();
            }

        }

        public void SetParent(object parent)
        {
            m_parent = (CTSServerManagerBridge)parent;
        }

        public void Close()
        {
            /*
            if (ipdaemon1 != null)
            {
                m_isClosing = true;

                // Alle User bezüglich des Logouts benachrichtigen!
                SendToAll(String.Format("LOGOUT{{{0}|{1}}}", m_adapter.GetUserName(), 0));

                for (int i = ipdaemon1.ConnectionCount; i > 0; --i)
                    ipdaemon1.Disconnect(i);
                m_dUsers.Clear();

                ipdaemon1.Listening = false;
                ipdaemon1 = null;
                m_isClosing = false;

                m_adapter.ShowServerConsole(false);
                //if (m_ctsServerConsole!=null)
                //    m_ctsServerConsole.Close();
            }*/
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

        public void AddMessage(string sMessage)
        {
            m_adapter.AddConsoleMessage(sMessage);
        }

        public void SendClassMessage(int roomId, string toUserName, string message)
        {
            StateObject user = GetConnectedUser(toUserName);
            SendData(user.Id, String.Format("#RECEIVECLASSMESSAGE{{{0}|{1}|{2}}}", roomId.ToString(), m_adapter.GetUserName(), message));
        }

        public void SetAdapter(ICTSServerAdapter adapter)
        {
            m_adapter = adapter;
        }


        private void bwConnector_DoWork(object sender, DoWorkEventArgs e)
        {
            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".

            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, portId);

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    //allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.
                    //allDone.WaitOne();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void bwConnector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (!((bool)e.Result))
            //    OnConnectingFailed(new EventArgs());
            //else
            //    OnConnectingSuccessed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
        }

        private void CheckReceivedCommand(int connId, ref string commandString)
        {
            if (commandString.Length > 0)
                if (!DoCommand(connId, ref commandString))
                    return;

            if (commandString.Length > 0)
                CheckReceivedCommand(connId, ref commandString);
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

            Trace.WriteLine("-->" + cmd);
            AddMessage("-->" + cmd);

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
                if (m_adapter.IsCampus())
                {
                    strUserName = String.Format("CampusUser{0}", m_dUsers.Count + 1);
                    strPassword = strUserName;
                }

                if (!IsUserAlreadyOnline(strUserName))
                {
                    StateObject user = GetConnectedUser(connId);
                    string strTarget = user.workSocket.RemoteEndPoint.ToString();
                    CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.Login, strTarget, strUserName, strPassword, language);
                    m_parent.FireEvent(ref gea);

                    if (gea.ReturnValue == 0)
                    {
                        if (m_adapter.IsCampus())
                        {
                            // Login ok senden
                            SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, 0));
                        }
                        else
                        {
                            // Login ok senden
                            SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, 0));

                            // Alle User bezüglich des Logins benachrichtigen!
                            SendToAll(String.Format("#LOGIN{{{0}|{1}}}", strUserName, 0));
                        }

                        user.Name = strUserName;

                        /*
                        // Dem Neuen User alle anderen mitteilen
                        IDictionaryEnumerator iter = m_dUsers.GetEnumerator();
                        while (iter.MoveNext())
                        {
                            DictionaryEntry item = (DictionaryEntry)iter.Current;
                            if (((string)item.Value) != strUserName)
                                SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", (string)item.Value, 0));
                        }*/

                        // Neuer Verbindung Server mitteilen
                        SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", m_adapter.GetUserName(), 0));
                    }
                    else
                        SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, gea.ReturnValue));
                }
                else
                    SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", strUserName, 3));
            }
            catch (nsoftware.IPWorks.IPWorksException e)
            {
                string sMsg = "IPEXCEXPTION: " + e.Message;
                AddMessage(sMsg);
                SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", userName, 5));
            }
            catch (Exception e)
            {
                string sMsg = "EXCEXPTION: " + e.Message;
                AddMessage(sMsg);
                SendData(connId, String.Format("#LOGIN{{{0}|{1}}}", userName, 5));
            }
        }

        private void DoSendClassMessage(int connId, int roomId, string toUserName, string message)
        {
            if (m_adapter.GetUserName() == toUserName)
            {
                //AppHandler.MainForm.ReceiveClassMessage(roomId,(string)m_dUsers[connId],message);
                ReceiveClassMessageDelegate delReceiveClassMessage = m_adapter.GetDelegateReceiveClassMessage();
                delReceiveClassMessage(roomId, toUserName, message);
            }
            else
            {
                StateObject user = GetConnectedUser(toUserName);
                SendData(user.Id, String.Format("#RECEIVECLASSMESSAGE{{{0}|{1}|{2}}}", roomId.ToString(), toUserName, message));
            }
        }

        private void DoTransferRequest(int connId, string typeName, string name)
        {
            if (typeName == "USERLIST")
                SendFile(connId, typeName, name, 1, 1, m_adapter.GetUserFileName());
            else if (typeName == "LEARNMAPS")
            {
                int mapCnt = 0;
                for (int i = 0; i < m_adapter.GetMapCnt(); ++i)
                {
                    if (m_adapter.MapHasUser(i, GetConnectedUser(connId).Name))
                        ++mapCnt;
                }

                int mapsSent = 0;
                int mapId = 1;
                for (int i = 0; i < m_adapter.GetMapCnt(); ++i)
                {
                    if (m_adapter.MapHasUser(i, GetConnectedUser(connId).Name))
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


        private bool SendData(int connId, string sData)
        {
            StateObject user = GetConnectedUser(connId);
            if (user.workSocket == null || !user.workSocket.Connected)
                return false;

            int dataToSend = sData.Length;
            try
            {
                Byte[] aData = System.Text.UTF8Encoding.UTF8.GetBytes(sData);

                Send(user.workSocket,aData);

                Trace.WriteLine("-->" + String.Format("ConnId[{0}]:{1}", connId, sData));
                AddMessage("-->" + String.Format("ConnId[{0}]:{1}", connId, sData));
            }
            catch (Exception e)
            {
                ForceDisconnect(connId);
            }
            return true;
        }

        private void SendToAll(string message, ICollection<StateObject> users = null)
        {
            if (users == null)
            {
                foreach (var u in m_dUsers.Keys)
                {
                    Send(u.workSocket,message);
                }
            }
            else
            {
                foreach (var u in m_dUsers.Keys.Where(users.Contains))
                {
                    Send(u.workSocket, message);
                }
            }
        }

        private void ForceDisconnect(int connId)
        {
            StateObject user = GetConnectedUser(connId);
            if (user.workSocket.Connected)
                user.workSocket.Disconnect(true);
        }

        private void ForceDisconnect(string userName)
        {
            StateObject user = GetConnectedUser(userName);
            if (user.workSocket.Connected)
                user.workSocket.Disconnect(true);
        }


        private void Disconnect(int connId)
        {
            StateObject user = GetConnectedUser(connId);
            string userName = user.Name;

            // Benutzer überhaupt angemeldet?
            if (userName != null && userName.Length > 0)
            {
                /*
                // Benutzer an allen anderen abmelden
                IDictionaryEnumerator iter = m_dUsers.GetEnumerator();
                while (iter.MoveNext())
                {
                    DictionaryEntry item = (DictionaryEntry)iter.Current;
                    string sUser = (string)item.Value;
                    if (sUser != null && sUser.Length > 0 && sUser != userName)
                        SendData((int)item.Key, String.Format("#LOGOUT{{{0}|{1}}}", userName, 0));
                }*/

                CTSServerEventArgs gea = new CTSServerEventArgs(CTSServerEventArgs.CommandType.Logout, userName);
                m_parent.FireEvent(ref gea);
                string strOut;
                m_dUsers.TryRemove(user, out strOut);
            }
        }


        private bool SendData(int connId, Byte[] aData)
        {
            int dataToSend = aData.Length;
            try
            {
                StateObject user = GetConnectedUser(connId);
                Send(user.workSocket, aData);
                Trace.WriteLine("-->" + String.Format("ConnId[{0}]:{1}", connId, aData.ToString()));
                AddMessage("-->" + String.Format("ConnId[{0}]:{1}", connId, aData.ToString()));
            }
            catch (Exception e)
            {
                ForceDisconnect(connId);
            }
            return true;
        }

        private void SendFile(int connId, string typeName, string name, int cnt, int id, string fileName)
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

            // Ein bischen Zeit lassen zum 
            //Thread.Sleep(2000);

            SendData(connId, String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, cnt, id));
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


        private void AcceptCallback(IAsyncResult ar) 
        {
            // Signal the main thread to continue.
            //allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject user=GetConnectedUser(handler.RemoteEndPoint);
            if (user == null)
            {
                // Create the state object.
                user = new StateObject { workSocket = handler, Id = m_dUsers.Count + 1 };
                m_dUsers.TryAdd(user, String.Empty);
                m_dataFilter[user.Id] = new CTSDataFilter();
                m_aRTSEvent[user.Id] = new AutoResetEvent(false);
            }

            var bwReceiver = new BackgroundWorker();
            bwReceiver.WorkerSupportsCancellation = true;
            bwReceiver.DoWork += StartReceive;
            bwReceiver.RunWorkerAsync(user.Id);
        }

        public void ReadCallback(IAsyncResult ar) 
        {
            String content = String.Empty;
        
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                byte[] l_aBytes = new Byte[bytesRead];
                Array.Copy(state.buffer, l_aBytes, bytesRead);
                // There  might be more data, so store the data received so far.
                //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                if (m_dataFilter[state.Id].AnalyseData(l_aBytes, ref m_commandString))
                    CheckReceivedCommand(state.Id, ref m_commandString);
            }
        }

        private void StartReceive(object sender, DoWorkEventArgs e)
        {
            StateObject user = GetConnectedUser((int)e.Argument);
            while (user.workSocket.Connected)
            {
                user.workSocket.BeginReceive(user.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), user);
            }
        }

        private void Send(Socket handler, String data) 
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void Send(Socket handler, Byte [] aData)
        {
            // Begin sending the data to the remote device.
            handler.BeginSend(aData, 0, aData.Length, 0, new AsyncCallback(SendCallback), handler);
        }


        private void SendCallback(IAsyncResult ar) 
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

        private StateObject GetConnectedUser(int iId)
        {
            var u = m_dUsers.Keys.Where(o => o.Id == iId).Single();
            return u;
        }

        private StateObject GetConnectedUser(EndPoint endPoint)
        {
            try
            {
	            var u = m_dUsers.Keys.Where(o => o.workSocket.RemoteEndPoint== endPoint).Single();
	            return u;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private StateObject GetConnectedUser(string strUserName)
        {
            var u = m_dUsers.Keys.Where(o => o.Name == strUserName).Single();
            return u;
        }


    }
}
