using System;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SoftObject.TrainConcept.Libraries;
using System.Collections.Generic;
using System.Linq;

namespace SoftObject.TrainConcept.ClientServer
{
    #region delegates
    /// <summary>
	/// Occurs when a command received from the server.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">The information about the received command.</param>
	public delegate void CommandReceivedEventHandler(object sender , EventArgs e);
    	/// <summary>
	/// Occurs when a command had been sent to the the remote server Successfully.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">EventArgs.</param>
	public delegate void CommandSentEventHandler(object sender , EventArgs e);
    	/// <summary>
	/// Occurs when a command sending action had been failed.This is because disconnection or sending exception.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">EventArgs.</param>
	public delegate void CommandSendingFailedEventHandler(object sender , EventArgs e);	/// <summary>
	/// Occurs when the network had been failed.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">EventArgs.</param>
	public delegate void NetworkDeadEventHandler(object sender , EventArgs e);
    /// <summary>
	/// Occurs when this client disconnected from the server.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">EventArgs.</param>
	public delegate void DisconnectedEventHandler(object sender , EventArgs e);
	/// <summary>
	/// Occurs when the network had been started to work.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">EventArgs.</param>
	public delegate void NetworkAlivedEventHandler(object sender , EventArgs e);
	/// <summary>
	/// Occurs when this client connected to the remote server Successfully.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">EventArgs.</param>
	public delegate void ConnectingSuccessedEventHandler(object sender , EventArgs e);
	/// <summary>
	/// Occurs when this client failed on connecting to server.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">EventArgs.</param>
	public delegate void ConnectingFailedEventHandler(object sender , EventArgs e);
    /// <summary>
    /// Occurs when this client disconnected from the server.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">EventArgs.</param>
    public delegate void ServerDisconnectedEventHandler(object sender, EventArgs e);

    #endregion

    /// <summary>
    /// Implementation for ClientManager based on .NET Sockets.
	/// </summary>
    public class CTSSocketClientManager : ICTSClientManager
    {
        private CTSClientManagerBridge m_parent;
        private CTSClientTransferData m_transferData;
        private AutoResetEvent m_jobDone;
		private string m_commandString="";
		private CTSDataFilter m_dataFilter=new CTSDataFilter();
		private AutoResetEvent m_connectionDone=new AutoResetEvent(false);
        private AutoResetEvent m_connectionFailed = new AutoResetEvent(false);
        private Timer m_keepAliveTimer;
        ICTSClientAdapter m_adapter;

        private Socket clientSocket;
		private NetworkStream networkStream;
		private BackgroundWorker bwReceiver;
		private IPEndPoint m_serverEP;

        //This Semaphor is to protect the critical section from concurrent access of sender threads.
        private Semaphore semaphor = new Semaphore(1, 1);
        
		#region getter&setter
		/// <summary>
		/// [Gets] The value that specifies the current client is connected or not.
		/// </summary>
		public bool Connected
		{
			get
			{
				if ( clientSocket != null )
					return clientSocket.Connected;
				else
					return false;
			}
		}
		/// <summary>
		/// [Gets] The IP address of the remote server.If this client is disconnected,this property returns IPAddress.None.
		/// </summary>
		public IPAddress ServerIP
		{
			get
			{
				if ( Connected )
					return m_serverEP.Address;
				else
					return IPAddress.None;
			}
		}

		/// <summary>
		/// [Gets] The comunication port of the remote server.If this client is disconnected,this property returns -1.
		/// </summary>
		public int ServerPort
		{
			get
			{
				if ( Connected )
					return m_serverEP.Port;
				else
					return -1;
			}
		}
		/// <summary>
		/// [Gets] The IP address of this client.If this client is disconnected,this property returns IPAddress.None.
		/// </summary>
		public IPAddress IP
		{
			get
			{
				if ( Connected )
					return ( (IPEndPoint)clientSocket.LocalEndPoint ).Address;
				else
					return IPAddress.None;
			}
		}
		/// <summary>
		/// [Gets] The comunication port of this client.If this client is disconnected,this property returns -1.
		/// </summary>
		public int Port
		{
			get
			{
				if ( Connected )
					return ( (IPEndPoint)clientSocket.LocalEndPoint ).Port;
				else
					return -1;
			}
		}
		#endregion

        private static string[] m_aStrCommands = {  "#TRANSFERSTART",
                                                    "#TRANSFEREND",
                                                    "#LOGIN",
				                                    "#LOGOUT",
				                                    "#RECEIVECLASSMESSAGE",
                                                    "#SENDNOTICE",
                                                    "#SENDNOTICEWORKOUTSTATE",
                                                    "#TESTPERMISSION" };
        
		/// <summary>
		/// Cretaes a command client instance.
		/// </summary>
		/// <param name="server">The remote server to connect.</param>
		/// <param name="netName">The string that will send to the server and then to other clients, to identify this client to all clients.</param>
		public CTSSocketClientManager()
		{
		}

        #region ICTSClientManager implementation
        public bool Create(string ipAddr, int portId)
        {
            try
            {
	            m_serverEP = new IPEndPoint(IPAddress.Parse(ipAddr), portId);
                System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
                return ConnectToServer();
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public void SetParent(object parent)
        {
            m_parent = (CTSClientManagerBridge)parent;
        }

        public void Close()
        {
            Disconnect();
            if (m_keepAliveTimer != null)
            {
                m_keepAliveTimer.Dispose();
                m_keepAliveTimer = null;
            }
            System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAvailabilityChanged;
        }

        public void StartKeepAlive()
        {
            int timer = m_adapter.GetKeepAliveTime();
            if (timer > 0)
                m_keepAliveTimer = new Timer(KeepAlive, this, 0, timer);
        }

        public void SendLogin(string userName, string passWord)
        {
            if (!CheckConnection())
                return;
            SendCommand(String.Format("#LOGIN{{{0}|{1}}}", userName, passWord));
        }

        public bool SendLogin(string userName, string passWord, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            SendCommand(String.Format("#LOGIN{{{0}|{1}}}", userName, passWord));
            if (!m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
            {
                return false;
            }
            return true;
        }

        public bool SendLogin(string language, string userName, string passWord, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            SendCommand(String.Format("#LOGIN{{{0}|{1}|{2}}}", language, userName, passWord));

            if (!m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                return false;
            return true;
        }

        public void SendTransferRequest(string typeName, string name)
        {
            if (!CheckConnection())
                return;

            SendCommand(String.Format("#TRANSFERREQUEST{{{0}|{1}}}", typeName, name));
        }

        public bool SendTransferRequest(string typeName, string name, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            SendCommand(String.Format("#TRANSFERREQUEST{{{0}|{1}}}", typeName, name));

            if (!m_jobDone.WaitOne(m_adapter.GetTimeoutTransfer(), false))
            {
                return false;
            }
            return true;
        }

        public void SendClassMessage(int roomId, string toUserName, string message)
        {
            if (!CheckConnection())
                return;

            SendCommand(String.Format("#SENDCLASSMESSAGE{{{0}|{1}|{2}}}", roomId, toUserName, message));
        }


        public bool SendClassMessage(int roomId, string toUserName, string message, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            SendCommand(String.Format("#SENDCLASSMESSAGE{{{0}|{1}|{2}}}", roomId, toUserName, message));

            if (!m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
            {
                return false;
            }
            return true;
        }

        public void AskTestPermission(string userName, string mapTitle)
        {
            if (!CheckConnection())
                return;

            SendCommand(String.Format("#ASKTESTPERMISSION({0}|{1})", userName, mapTitle));
        }

        public bool AskTestPermission(string userName, string mapTitle, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            SendCommand(String.Format("#ASKTESTPERMISSION{{{0}|{1}}}", userName, mapTitle));

            if (!m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
            {
                return false;
            }
            return true;
        }

        public bool IsRunning()
        {
            return Connected;
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

        public void SendTestResult(TestResultItem testResult)
        {
            if (!CheckConnection())
                return;

            string sOut = String.Format("#SENDTESTRESULT{{{0}}}", testResult);

            SendCommand(sOut);
        }

        public bool SendTestResult(TestResultItem testResult, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            string sOut = String.Format("#SENDTESTRESULT{{{0}}}", testResult);

            SendData(sOut);

            return true;
        }

        public void SendUserProgressInfo(string userName, string workingName, int iRegionId, int iRegionValue)
        {
            if (!CheckConnection())
                return;

            string sOut = String.Format("#SENDUSERPROGRESSINFO{{{0}|{1}|{2}|{3}}}", userName, workingName,iRegionId, iRegionValue);

            SendCommand(sOut);
        }

        public bool SendUserProgressInfo(string userName, string workingName, int iRegionId, int iRegionValue, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            string sOut = String.Format("#SENDUSERPROGRESSINFO{{{0}|{1}|{2}|{3}}}", userName, workingName, iRegionId, iRegionValue);

            SendData(sOut);

            return true;
        }

        public void SendNotice(string noticeTitle)
        {
            if (!CheckConnection())
                return;

            string userName = m_adapter.GetUserName();
            var aNotices = m_adapter.GetNotices(userName) as NoticeItemCollection;
            var ni = aNotices.FirstOrDefault(x => x.title == noticeTitle);
            if (ni != null)
            {
                ni.dirtyFlag = 0;
                m_adapter.StartNoticeTransfer(userName, ni.title);
                string dirName = String.Format("{0}\\notices\\", m_adapter.GetNoticesDirectory(userName), userName);
                string filePath = dirName + '\\' + ni.fileName;
                if (File.Exists(filePath))
                {
                    SendFile("NOTICES", ni.title, 1, 1, filePath);
                    SendData(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}}}", 1, 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName)));
                }
            }
        }

        public bool SendNotice(string noticeTitle, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_jobDone.Reset();

            string userName = m_adapter.GetUserName();
            var aNotices = m_adapter.GetNotices(userName) as NoticeItemCollection;
            var ni = aNotices.FirstOrDefault(x => x.title == noticeTitle);
            if (ni != null)
            {
                m_adapter.StartNoticeTransfer(userName, ni.title);
                string dirName = String.Format("{0}\\notices\\", m_adapter.GetNoticesDirectory(userName), userName);
                string filePath = dirName + '\\' + ni.fileName;
                if (File.Exists(filePath))
                {
                    SendFile("NOTICES", ni.title, 1, 1, filePath);
                    SendData(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}}}", 1, 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName)));
                }
            }

            return true;
        }


        public void SetAdapter(ICTSClientAdapter adapter)
        {
            m_adapter = adapter;
        }
        #endregion

		#region Private Methods

        private bool SendData(string sData)
        {
            try
            {
                semaphor.WaitOne();
                Byte[] aData = System.Text.UTF8Encoding.UTF8.GetBytes(sData);
                this.networkStream.Write(aData, 0, aData.Length);
                this.networkStream.Flush();
                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }

        }

        private bool SendData(Byte[] aData)
        {
            try
            {
                semaphor.WaitOne();
                this.networkStream.Write(aData, 0, aData.Length);
                this.networkStream.Flush();
                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }

        }

        private bool CheckConnection()
        {
            if (!Connected)
            {
                try
                {
                    ConnectToServer();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return Connected;
        }

        private void SendKeepAlive()
        {
            SendCommand(String.Format("#KEEPALIVE{{{0}}}", m_adapter.GetUserName()));
        }

        private void KeepAlive(object state)
        {
            if (Connected)
              SendKeepAlive();
        }

		private void NetworkChange_NetworkAvailabilityChanged(object sender , System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
		{
			if ( !e.IsAvailable )
			{
				this.OnNetworkDead(new EventArgs());
				this.OnDisconnectedFromServer(new EventArgs());
			}
			else
				this.OnNetworkAlived(new EventArgs());
		}

		private void StartReceive(object sender , DoWorkEventArgs e)
		{
			while ( this.clientSocket.Connected )
			{
                // Check to see if this NetworkStream is readable.
                if(this.networkStream.CanRead)
                {
                    byte[] aData = new byte[1024];
                    StringBuilder sData = new StringBuilder();
                    int bytesRead = 0;

                    // Incoming message may be larger than the buffer size.
                    do
                    {
                         bytesRead = networkStream.Read(aData, 0, aData.Length);
					     sData.AppendFormat("{0}", System.Text.UTF8Encoding.UTF8.GetString(aData, 0, bytesRead));
                    }
                    while(networkStream.DataAvailable);

			        if (m_dataFilter.AnalyseData(System.Text.UTF8Encoding.UTF8.GetBytes(sData.ToString()),ref m_commandString))
			        {
				        int cmdStartFound= m_commandString.IndexOf('#');
				        if (cmdStartFound>=0)
				        {
					        if((m_commandString.Length-cmdStartFound)<5)
							        return;
				        }
				        CheckReceivedCommand(ref m_commandString);
                        this.OnCommandReceived(new EventArgs());
				        //Thread.Sleep(100);
			        }
			    }
            }
            this.OnServerDisconnected(new EventArgs());
            this.Disconnect();
        }

		private void bwSender_RunWorkerCompleted(object sender , RunWorkerCompletedEventArgs e)
		{
			if ( !e.Cancelled && e.Error == null && (e.Result!=null && (bool)e.Result))
				OnCommandSent(new EventArgs());
			else
				this.OnCommandFailed(new EventArgs());

			//( (BackgroundWorker)sender ).Dispose();
			GC.Collect();
		}

		private void bwSender_DoWork(object sender , DoWorkEventArgs e)
		{
			string cmd = (string)e.Argument;
			e.Result = SendData(cmd);
        }

        private void bwConnector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!((bool)e.Result))
                OnConnectingFailed(new EventArgs());
            else
                OnConnectingSuccessed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
        }

        private void bwConnector_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(m_serverEP);
                e.Result = true;
                m_connectionDone.Set();

                networkStream = new NetworkStream(this.clientSocket);

                bwReceiver = new BackgroundWorker();
                bwReceiver.WorkerSupportsCancellation = true;
                bwReceiver.DoWork += StartReceive;
                bwReceiver.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                e.Result = false;
                m_connectionFailed.Set();
            }
        }

		private void DoLogin(string userName,int retValue)
		{
			CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.Login,userName,retValue);
			m_parent.FireEvent(ref gea);
				
			// Nur bei diesem User-Login Anfrage beenden
			if (userName == m_adapter.GetUserName())
			{
				if (m_jobDone!=null)
					m_jobDone.Set();
			}
		}

		private void DoTestPermission(string userName,string mapName, int retValue)
		{
			CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.TestPermission,userName,mapName,retValue);
			m_parent.FireEvent(ref gea);

			if (m_jobDone!=null)
				m_jobDone.Set();
		}

		private void DoLogout(string userName,int retValue)
		{
			CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.Logout,userName,retValue);
			m_parent.FireEvent(ref gea);
		}

		private void DoTransferStart(string typeName,string name,int cnt,int id,string fileName,int fileSize)
		{
			if (typeName=="USERLIST")
			{
				m_transferData = new CTSClientTransferData(typeName,name,m_adapter.GetUserFileName(),fileSize);
			}
			else if (typeName=="LEARNMAPS")
			{
				if (!Directory.Exists(m_adapter.GetMapsFolderPath()))
					Directory.CreateDirectory(m_adapter.GetMapsFolderPath());
				m_transferData = new CTSClientTransferData(typeName,name,cnt,id,
                                     String.Format("{0}\\{1}", m_adapter.GetMapsFolderPath(), fileName), fileSize);
			}
			else if (typeName=="LIBRARIES")
			{
				if (!Directory.Exists(m_adapter.GetLibsFolderPath()))
					Directory.CreateDirectory(m_adapter.GetLibsFolderPath());
				m_transferData = new CTSClientTransferData(typeName,name,cnt,id,
                                     String.Format("{0}\\{1}", m_adapter.GetLibsFolderPath(), fileName), fileSize);
			}
            else if (typeName == "DOCUMENTS")
            {
                string strSubDir = m_adapter.GetDocumentsFolderPath(name);
                m_transferData = new CTSClientTransferData(typeName, name, cnt, id,
                                     String.Format("{0}\\{1}", strSubDir, fileName), fileSize);
            }
            else if (typeName == "NOTICES")
            {
                string strFilePath=m_adapter.GetNoticesDirectory(m_adapter.GetUserName())+"\\cache";
                if (strFilePath.Length > 0)
                {
                    if (!Directory.Exists(strFilePath))
                        Directory.CreateDirectory(strFilePath);
                    m_transferData = new CTSClientTransferData(typeName, name, cnt, id,
                                         String.Format("{0}\\{1}", strFilePath, fileName), fileSize);
                }
            }

            if (m_transferData != null)
            {
                m_transferData.Start();
                CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.TransferStart,m_transferData.TypeName,m_transferData.Name,m_transferData);
                m_parent.FireEvent(ref gea);
            }
		}

		private void DoTransferEnd(string typeName,string name,int cnt,int id)
		{
			if (m_transferData!=null)
			{
				m_transferData.PackageId = id;
                CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.TransferEnd, m_transferData.TypeName, m_transferData.Name,m_transferData);
				m_parent.FireEvent(ref gea);

				if (m_jobDone!=null)
					if (cnt==0 || m_transferData.IsLastPackage())
						m_jobDone.Set();

				m_transferData.Stop();
				m_transferData = null;
			}
			else
			{
                CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.TransferEnd, typeName, name, null);
                m_parent.FireEvent(ref gea);

				if (m_jobDone!=null)
					m_jobDone.Set();
			}
		}

        private void DoTransferRequest(string typeName, string name)
        {
            if (typeName == "NOTICELIST")
            {
                int iSent = 0;
                string userName = m_adapter.GetUserName();
                var nic = m_adapter.GetNotices(userName) as NoticeItemCollection;  
                for(int i=0;i<nic.Count;++i)
                {
                    NoticeItem ni = nic[i];
                    if (name=="*" || ni.title == name)
                    {
                        m_adapter.StartNoticeTransfer(userName, ni.title);
                        string dirName = String.Format("{0}\\notices\\", m_adapter.GetNoticesDirectory(userName), userName);
                        string filePath = dirName + '\\' + ni.fileName;
                        if (File.Exists(filePath))
                        {
                            SendFile("NOTICES", name, nic.Count, i + 1, filePath);
                            SendData(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}}}", nic.Count, i + 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName)));
                            ++iSent;
                        }

                        if (ni.title == name)
                            break;
                    }
                }

                if (iSent == 0)
                {
                    SendData(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}}}", 0, 0, userName, "", 0, "",0, ""));
                    SendData(String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
                }
            }
        }

		private void DoReceiveClassMessage(int roomId,string fromUserName,string message)
		{
			CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.ReceiveClassMesssage,fromUserName,roomId,message);
			m_parent.FireEvent(ref gea);
		}

        private void DoSendNoticeWorkoutState(string strTitle, int iWorkedOutState)
        {
            m_adapter.SetNoticeWorkoutState(strTitle, iWorkedOutState);
        }

        private void DoSendNotice(int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle, int iWorkedOut, string strFileName)
        {
            m_adapter.AddNotice(iCount, iId, userName, contentPath, iPageId, strTitle, iWorkedOut, strFileName);
        }

		private bool DoCommand(ref string commandString)
		{
			string cmd;
			int found=commandString.IndexOf("}");
			if (found>=0 && found<=(commandString.Length-1))
			{
				cmd=commandString.Substring(0,found+1);
				commandString = commandString.Substring(found+1);
			}
			else
				return false;

			Trace.WriteLine("-->"+cmd);
			string[] aCommands = cmd.Split('{');
			if (aCommands.Length>1)
			{
				char[] trimChars={'}'};
				aCommands[1] = aCommands[1].TrimEnd(trimChars);
				string[] aTokens = aCommands[1].Split('|');
				if (aCommands[0].Length>0)
				{
					if (aCommands[0] == "#LOGIN")
						DoLogin(aTokens[0],Int32.Parse(aTokens[1]));
					else if (aCommands[0] == "#LOGOUT")
						DoLogout(aTokens[0],Int32.Parse(aTokens[1]));
					else if (aCommands[0] == "#TRANSFERSTART")
						DoTransferStart(aTokens[0],aTokens[1],Int32.Parse(aTokens[2]),Int32.Parse(aTokens[3]),aTokens[4],Int32.Parse(aTokens[5]));
					else if (aCommands[0] == "#TRANSFEREND")
						DoTransferEnd(aTokens[0],aTokens[1],Int32.Parse(aTokens[2]),Int32.Parse(aTokens[3]));
					else if (aCommands[0] == "#RECEIVECLASSMESSAGE")
						DoReceiveClassMessage(Int32.Parse(aTokens[0]),aTokens[1],aTokens[2]);
					else if (aCommands[0] == "#TESTPERMISSION")
						DoTestPermission(aTokens[0],aTokens[1],Int32.Parse(aTokens[2]));
                    else if (aCommands[0] == "#TRANSFERREQUEST")
                        DoTransferRequest(aTokens[0], aTokens[1]);
                    else if (aCommands[0] == "#SENDNOTICEWORKOUTSTATE")
                        DoSendNoticeWorkoutState(aTokens[0],Int32.Parse(aTokens[1]));
                    else if (aCommands[0] == "#SENDNOTICE")
                        DoSendNotice(Int32.Parse(aTokens[0]), Int32.Parse(aTokens[1]), aTokens[2], aTokens[3], Int32.Parse(aTokens[4]), aTokens[5], Int32.Parse(aTokens[6]), aTokens[7]);
					else
						return false;
					return true;
				}
			}
			return false;
		}

        private void CheckReceivedCommand(ref string commandString)
		{
            string dataString = "";  // Daten vor dem Kommando im Stream

            // Prüfen ob irgendwo im Stream Kommandos vorkommen
            bool bFound = ParseCommandString(ref dataString, ref commandString);
            if (dataString.Length > 0)
            {
                if (m_transferData != null)
                {
                    m_transferData.DoTransfer(dataString);
                    CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.TransferProgress, m_transferData.TypeName, m_transferData.Name, m_transferData);
                    m_parent.FireEvent(ref gea);

                    if (bFound)
                        CheckReceivedCommand(ref commandString);
                }
                else
                {
                    commandString = dataString;
                    return;
                }
            }

            if (bFound)
                if (!DoCommand(ref commandString))
                    return;

            if (commandString.Length > 0)
                CheckReceivedCommand(ref commandString);
		}

        private bool ParseCommandString(ref string dataString, ref string commandString)
        {
            // Prüfen ob irgendwo im String Kommandos vorkommen
            foreach (string s in m_aStrCommands)
            {
                int iFound = commandString.IndexOf(s); // 1.Kommando suchen
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

        private void SendFile(string typeName, string name, int cnt, int id, string fileName)
        {
            if (File.Exists(fileName))
            {
                System.IO.StreamReader reader = new StreamReader(fileName);
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

                SendData(String.Format("#TRANSFERSTART{{{0}|{1}|{2}|{3}|{4}|{5}}}", typeName, name, cnt, id, fi.Name, reader.BaseStream.Length));

                char[] aBuf = new char[reader.BaseStream.Length + 1];
                int bytesRead = 0;
                while ((bytesRead = reader.Read(aBuf, 0, aBuf.Length)) > 0)
                {
                    Byte[] byteData = System.Text.UTF8Encoding.UTF8.GetBytes(aBuf, 0, bytesRead);
                    SendData(byteData);
                }

                // Ein bischen Zeit lassen zum 
                //Thread.Sleep(1000);

                SendData(String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, cnt, id));
                reader.Close();
            }
        }


       #endregion

		#region Public Methods
        /// <summary>
		/// Connect the current instance of command client to the server.This method throws ServerNotFoundException on failur.Run this method and handle the 'ConnectingSuccessed' and 'ConnectingFailed' to get the connection state.
		/// </summary>
        public bool ConnectToServer()
		{
            try
            {
	            m_connectionDone.Reset();
                m_connectionFailed.Reset();

	            using (BackgroundWorker bwConnector = new BackgroundWorker())
	            {
	                bwConnector.DoWork += bwConnector_DoWork;
	                bwConnector.RunWorkerCompleted += bwConnector_RunWorkerCompleted;
	                bwConnector.RunWorkerAsync();
	            }

                int id = WaitHandle.WaitAny(new WaitHandle[] { m_connectionDone, m_connectionFailed }, 30000);
                if (id > 0)
                {
                    CTSClientEventArgs ea1 = new CTSClientEventArgs(CTSClientEventArgs.CommandType.ConnectionInfo, false);
                    m_parent.FireEvent(ref ea1);
                    return false;
                }

                CTSClientEventArgs ea2 = new CTSClientEventArgs(CTSClientEventArgs.CommandType.ConnectionInfo, true);
                m_parent.FireEvent(ref ea2);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
		}


		/// <summary>
		/// Sends a command to the server if the connection is alive.
		/// </summary>
		/// <param name="cmd">The command to send.</param>
		public void SendCommand(string cmd)
		{
            if (clientSocket != null && clientSocket.Connected)
            {
                using (BackgroundWorker bwSender = new BackgroundWorker())
                {
                    bwSender.DoWork += bwSender_DoWork;
                    bwSender.RunWorkerCompleted += bwSender_RunWorkerCompleted;
                    bwSender.WorkerSupportsCancellation = true;
                    bwSender.RunWorkerAsync(cmd);
                }
            }
            else
            {
                OnCommandFailed(new EventArgs());
                if (m_jobDone != null)
                    m_jobDone.Set();
            }
		}

		/// <summary>
		/// Disconnect the client from the server and returns true if the client had been disconnected from the server.
		/// </summary>
		/// <returns>True if the client had been disconnected from the server,otherwise false.</returns>
		public bool Disconnect()
		{
			if (clientSocket != null && clientSocket.Connected )
			{
				try
				{
					clientSocket.Shutdown(SocketShutdown.Both);
					clientSocket.Close();
                    if (bwReceiver!=null)
					    bwReceiver.CancelAsync();
					OnDisconnectedFromServer(new EventArgs());
					return true;
				}
				catch
				{
					return false;
				}

			}
			else
				return true;
		}

		#endregion


		#region Events
		/// <summary>
		/// Occurs when a command received from a remote client.
		/// </summary>
		public event CommandReceivedEventHandler CommandReceived;
		/// <summary>
		/// Occurs when a command received from a remote client.
		/// </summary>
		/// <param name="e">The received command.</param>
		protected virtual void OnCommandReceived(EventArgs e)
		{
			if ( CommandReceived != null )
                CommandReceived(this, e);
		}

		/// <summary>
		/// Occurs when a command had been sent to the the remote server Successfully.
		/// </summary>
		public event CommandSentEventHandler CommandSent;
		/// <summary>
		/// Occurs when a command had been sent to the the remote server Successfully.
		/// </summary>
		/// <param name="e">The sent command.</param>
		protected virtual void OnCommandSent(EventArgs e)
		{
			if ( CommandSent != null )
                CommandSent(this, e);
		}

		/// <summary>
		/// Occurs when a command sending action had been failed.This is because disconnection or sending exception.
		/// </summary>
		public event CommandSendingFailedEventHandler CommandFailed;
		/// <summary>
		/// Occurs when a command sending action had been failed.This is because disconnection or sending exception.
		/// </summary>
		/// <param name="e">The sent command.</param>
		protected virtual void OnCommandFailed(EventArgs e)
		{
			if ( CommandFailed != null )
                CommandFailed(this, e);
		}


		/// <summary>
		/// Occurs when the server disconnected.
		/// </summary>
		public event ServerDisconnectedEventHandler ServerDisconnected;
		/// <summary>
		/// Occurs when the server disconnected.
		/// </summary>
		/// <param name="e">Server information.</param>
		protected virtual void OnServerDisconnected(EventArgs e)
		{
			if ( ServerDisconnected != null )
                ServerDisconnected(this, e);
		}

		/// <summary>
		/// Occurs when this client disconnected from the remote server.
		/// </summary>
		public event DisconnectedEventHandler DisconnectedFromServer;
		/// <summary>
		/// Occurs when this client disconnected from the remote server.
		/// </summary>
		/// <param name="e">EventArgs.</param>
		protected virtual void OnDisconnectedFromServer(EventArgs e)
		{
			if ( DisconnectedFromServer != null )
                DisconnectedFromServer(this, e);
		}

		/// <summary>
		/// Occurs when this client connected to the remote server Successfully.
		/// </summary>
		public event ConnectingSuccessedEventHandler ConnectingSuccessed;
		/// <summary>
		/// Occurs when this client connected to the remote server Successfully.
		/// </summary>
		/// <param name="e">EventArgs.</param>
		protected virtual void OnConnectingSuccessed(EventArgs e)
		{
            if (ConnectingSuccessed != null)
                ConnectingSuccessed(this, e);
		}

		/// <summary>
		/// Occurs when this client failed on connecting to server.
		/// </summary>
		public event ConnectingFailedEventHandler ConnectingFailed;
		/// <summary>
		/// Occurs when this client failed on connecting to server.
		/// </summary>
		/// <param name="e">EventArgs.</param>
		protected virtual void OnConnectingFailed(EventArgs e)
		{
			if ( ConnectingFailed != null )
                ConnectingFailed(this, e);
		}

		/// <summary>
		/// Occurs when the network had been failed.
		/// </summary>
		public event NetworkDeadEventHandler NetworkDead;
		/// <summary>
		/// Occurs when the network had been failed.
		/// </summary>
		/// <param name="e">EventArgs.</param>
		protected virtual void OnNetworkDead(EventArgs e)
		{
			if ( NetworkDead != null )
                NetworkDead(this, e);
		}

		/// <summary>
		/// Occurs when the network had been started to work.
		/// </summary>
		public event NetworkAlivedEventHandler NetworkAlived;
		/// <summary>
		/// Occurs when the network had been started to work.
		/// </summary>
		/// <param name="e">EventArgs.</param>
		protected virtual void OnNetworkAlived(EventArgs e)
		{
			if ( NetworkAlived != null )
				NetworkAlived(this , e);
		}
		#endregion
    }
}
