using System;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SoftObject.TrainConcept.Libraries;
using System.Linq;
using SoftObject.SOComponents.Forms;
using Timer = System.Threading.Timer;

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

    #region ConnectionState
    // State object for receiving data from remote device.
    public class ConnectionStateClient
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1048576;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
        public int  dataLength = 0;
        public byte type = 0;
        public int  actDataLength = 0;
        public ManualResetEvent m_receiveTypeAndLengthDone = new ManualResetEvent(false);
        public ManualResetEvent m_receiveDataDone = new ManualResetEvent(false);

        public bool IsValid() {return (type>=0 && type<=1);}
    }
    #endregion

    /// <summary>
    /// Implementation for ClientManager based on .NET Sockets.
	/// </summary>
    public class CTSSocketClientManager : ICTSClientManager
    {
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
            if (CommandReceived != null)
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
            if (CommandSent != null)
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
            if (CommandFailed != null)
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
            if (ServerDisconnected != null)
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
            if (DisconnectedFromServer != null)
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
            if (ConnectingFailed != null)
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
            if (NetworkDead != null)
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
            if (NetworkAlived != null)
                NetworkAlived(this, e);
        }
        #endregion

        #region members
        private CTSClientManagerBridge m_parent;
        private CTSClientTransferData m_transferData;
        private AutoResetEvent m_jobDone=null;
        private AutoResetEvent m_workProgressDone=null;
        private AutoResetEvent m_mapProgressDone=null;
        private string m_commandString = "";
        private string m_requestString = "";
        private CTSDataFilter m_dataFilter = new CTSDataFilter();
        private Timer m_keepAliveTimer;
        ICTSClientAdapter m_adapter;

        private Socket clientSocket;
		private NetworkStream networkStream;
		private IPEndPoint m_serverEP;
        private Thread m_connectThread = null;

        //This Semaphor is to protect the critical section from concurrent access of sender threads.
        private readonly Semaphore semaphor = new Semaphore(1, 1);
        public const int cFileSendBufferSize = 262144;
        #endregion

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
		        
		/// <summary>
		/// Cretaes a command client instance.
		/// </summary>
		/// <param name="server">The remote server to connect.</param>
		/// <param name="netName">The string that will send to the server and then to other clients, to identify this client to all clients.</param>
		public CTSSocketClientManager()
		{
		}

        private void AddMessage(String strTxt)
        {
            m_adapter.AddConsoleMessage(strTxt);
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

        #region ICTSClientManager implementation
        public bool Create(string ipAddr, int portId)
        {
            try
            {
	            m_serverEP = new IPEndPoint(IPAddress.Parse(ipAddr), portId);
                System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
                return ConnectToServer();
            }
            catch (System.Exception /*ex*/)
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
            bool bResult = false;
            int iXPos, iYPos;

#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationEmpty();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
            frmToastNotification.Location = new Point(iXPos,iYPos);
#endif
            frmToastNotification.Start(String.Format("Anmelden"),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_jobDone.Reset();

                        SendCommand(String.Format("#LOGIN{{{0}|{1}}}",  userName, passWord));

                        if (m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });
            return bResult;
        }

        public bool SendLogin(string language, string userName, string passWord, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            bool bResult = false;
            int iXPos, iYPos;

#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationEmpty();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
            frmToastNotification.Location = new Point(iXPos, iYPos);
#endif
            frmToastNotification.Start(String.Format("Anmelden"),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_jobDone.Reset();

                        SendCommand(String.Format("#LOGIN{{{0}|{1}|{2}}}", language, userName, passWord));

                        if (m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });
            return bResult;
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
            bool bResult = false;
            int iXPos, iYPos;
#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationSmall();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
            frmToastNotification.Location = new Point(iXPos, iYPos);
#endif
            frmToastNotification.Start(String.Format("Lade {0}", name),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_jobDone.Reset();

                        SendCommand(String.Format("#TRANSFERREQUEST{{{0}|{1}}}", typeName, name));

                        if (m_jobDone.WaitOne(m_adapter.GetTimeoutTransfer(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });

            return bResult;
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
            bool bResult = false;
            int iXPos, iYPos;
#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationEmpty();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
            frmToastNotification.Location = new Point(iXPos, iYPos);
#endif
            frmToastNotification.Start(String.Format("Klassennachricht"),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_jobDone.Reset();

                        SendCommand(String.Format("#SENDCLASSMESSAGE{{{0}|{1}|{2}}}", roomId, toUserName, message));

                        if (m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });

            return bResult;
        }

        public void AskTestPermission(string userName, string mapTitle, string testName)
        {
            if (!CheckConnection())
                return;

            SendCommand(String.Format("#ASKTESTPERMISSION({0}|{1}|{2})", userName, mapTitle, testName));
        }

        public bool AskTestPermission(string userName, string mapTitle, string testName, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            bool bResult = false;
            int iXPos, iYPos;
#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationEmpty();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
            frmToastNotification.Location = new Point(iXPos, iYPos);
#endif
            frmToastNotification.Start(String.Format("Testerlaubnis holen"),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_jobDone.Reset();

                        SendCommand(String.Format("#ASKTESTPERMISSION{{{0}|{1}|{2}}}", userName,mapTitle,testName));

                        if (m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });

            return bResult;
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
                if (m_jobDone!=null)
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

        public void SendUserProgressInfo(string userName, string workingName, int iRegionId, ushort iRegionValue)
        {
            if (!CheckConnection())
                return;

            string sOut = String.Format("#SENDUSERPROGRESSINFO{{{0}|{1}|{2}|{3}}}", userName, workingName,iRegionId, iRegionValue);

            SendCommand(sOut);
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
                    SendString(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}}}", 1, 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName),ni.ModificationDateString));
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
                    SendString(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}}}", 1, 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName),ni.ModificationDateString));
                }
            }

            return true;
        }

        public void SendNoticeWorkoutState(string userName,string strTitle, int iWorkedOutState)
        {
            if (!CheckConnection())
                return;

            string sOut = String.Format("#SENDNOTICEWORKOUTSTATE{{{0}|{1}|{2}}}", userName, strTitle, iWorkedOutState);

            SendCommand(sOut);
        }


        public void AskMapProgress(string userName, string mapTitle)
        {
            if (!CheckConnection())
                return;

            SendCommand(String.Format("#ASKMAPPROGRESS{{{0}|{1}}}", userName, mapTitle));
        }

        public bool AskMapProgress(string userName, string mapTitle, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_mapProgressDone = jobDone;
            bool bResult = false;
            int iXPos, iYPos;

#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationEmpty();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
            frmToastNotification.Location = new Point(iXPos, iYPos);
#endif
            frmToastNotification.Start(String.Format("Mappenfortschritt holen"),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_mapProgressDone.Reset();

                        SendCommand(String.Format("#ASKMAPPROGRESS{{{0}|{1}}}", userName, mapTitle));

                        if (m_mapProgressDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });

            return bResult;
        }

        public void AskWorkProgress(string userName, string mapTitle, string workTitle)
        {
            if (!CheckConnection())
                return;

            SendCommand(String.Format("#ASKWORKPROGRESS{{{0}|{1}|{2}}}", userName, mapTitle, workTitle));
        }

        public bool AskWorkProgress(string userName, string mapTitle, string workTitle, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_workProgressDone = jobDone;
            bool bResult = false;
            int iXPos, iYPos;
#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationEmpty();
            m_adapter.GetCircProgressLocation(out iXPos, out iYPos);
#endif
            frmToastNotification.Start(String.Format("Ausarbeitungsfortschritt holen"),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_workProgressDone.Reset();

                        SendCommand(String.Format("#ASKWORKPROGRESS{{{0}|{1}|{2}}}", userName, mapTitle, workTitle));

                        if (m_workProgressDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });

            return bResult;
        }


        public void AskChangePassword(string userName, string passWord)
        {
            if (!CheckConnection())
                return;

            SendCommand(String.Format("#ASKCHANGEPASSWORD{{{0}|{1}}}", userName, passWord));
        }

        public bool AskChangePassword(string userName, string passWord, ref AutoResetEvent jobDone)
        {
            if (!CheckConnection())
                return false;

            m_jobDone = jobDone;
            m_requestString = userName;
            bool bResult = false;

#if(!__USEFORMS__)
            var frmToastNotification = new XFrmLongProcessToastNotification();
#else
            var frmToastNotification = new XFrmLongProcessToastNotificationEmpty();
#endif
            frmToastNotification.Start(String.Format("Anfrage auf Passwortänderung"),
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        m_jobDone.Reset();

                        SendCommand(String.Format("#ASKCHANGEPASSWORD{{{0}|{1}}}", userName, passWord));

                        if (m_jobDone.WaitOne(m_adapter.GetTimeoutStandard(), false))
                        {
                            e.Result = "OK";
                            bResult = true;
                        }
                        else
                        {
                        }
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });
            m_requestString = "";
            return bResult;
        }

 
        public void SetAdapter(ICTSClientAdapter adapter)
        {
            m_adapter = adapter;
        }

        public ICTSClientAdapter GetAdapter()
        {
            return m_adapter;
        }
        #endregion

        #region Connection methods

        /// <summary>
        /// Connect the current instance of command client to the server.This method throws ServerNotFoundException on failur.Run this method and handle the 'ConnectingSuccessed' and 'ConnectingFailed' to get the connection state.
        /// </summary>
        private bool ConnectToServer()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(m_serverEP);

                networkStream = new NetworkStream(this.clientSocket);

                ThreadStart connectThreadStart = new ThreadStart(ConnectThreadStarter);
                m_connectThread = new Thread(connectThreadStart);
                m_connectThread.Priority = ThreadPriority.BelowNormal;
                m_connectThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CTSClientEventArgs ea1 = new CTSClientEventArgs(CTSClientEventArgs.CommandType.ConnectionInfo, false);
                m_parent.FireEvent(ref ea1);
                return false;
            }

            CTSClientEventArgs ea2 = new CTSClientEventArgs(CTSClientEventArgs.CommandType.ConnectionInfo, true);
            m_parent.FireEvent(ref ea2);
            return true;

        }

        private void ConnectThreadStarter()
        {
            ConnectionStateClient state = new ConnectionStateClient();
            while (!(clientSocket.Poll(0, SelectMode.SelectRead) && clientSocket.Available == 0))
            {
                // Check to see if this NetworkStream is readable.
                if (this.networkStream.CanRead)
                {
                    // Create the state object.
                    state.workSocket = clientSocket;

                    if (state.dataLength > 0)
                    {
                        state.m_receiveDataDone.Reset();
                        clientSocket.BeginReceive(state.buffer, state.actDataLength, state.dataLength - state.actDataLength, 0, new AsyncCallback(ACB_ReceiveData), state);
                        state.m_receiveDataDone.WaitOne();
                    }
                    else
                    {
                        // Begin receiving the data from the remote device.
                        state.m_receiveTypeAndLengthDone.Reset();
                        clientSocket.BeginReceive(state.buffer, 0, 5, 0, new AsyncCallback(ACB_ReceiveTypeAndLength), state);
                        state.m_receiveTypeAndLengthDone.WaitOne();
                    }
                }
                Thread.Sleep(100);
            }
            this.OnServerDisconnected(new EventArgs());
            this.Disconnect();
        }

        /// <summary>
        /// Disconnect the client from the server and returns true if the client had been disconnected from the server.
        /// </summary>
        /// <returns>True if the client had been disconnected from the server,otherwise false.</returns>
        public bool Disconnect()
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                try
                {
                    if (m_jobDone != null)
                        m_jobDone.Set();
                    m_connectThread.Abort();
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
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

        private void NetworkChange_NetworkAvailabilityChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            if (!e.IsAvailable)
            {
                this.OnNetworkDead(new EventArgs());
                this.OnDisconnectedFromServer(new EventArgs());
            }
            else
                this.OnNetworkAlived(new EventArgs());
        }

        #endregion

        #region Sending methods

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

        private void bwSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && (e.Result != null && (bool)e.Result))
                OnCommandSent(new EventArgs());
            else
                this.OnCommandFailed(new EventArgs());

            GC.Collect();
        }

        private void bwSender_DoWork(object sender, DoWorkEventArgs e)
        {
            string cmd = (string)e.Argument;
            e.Result = SendString(cmd);
        }

        private void Send(string data)
        {
            byte[] aTypeAndSize = new byte[5];
            aTypeAndSize[0] = 0;

            // Convert the string data to byte data using UTF8 encoding.
            byte[] byteData = Encoding.Unicode.GetBytes(data);

            byte[] aLength = BitConverter.GetBytes(byteData.Length);
            //Array.Reverse(aLength);
            Array.Copy(aLength, 0, aTypeAndSize, 1, aLength.Length);

            clientSocket.BeginSend(aTypeAndSize, 0, 5,0, new AsyncCallback(ACB_SendTypeAndLength),clientSocket);

            // Begin sending the data to the remote device.
            clientSocket.BeginSend(byteData, 0, byteData.Length,0, new AsyncCallback(ACB_SendData), clientSocket);
        }


        private void Send(Byte[] aData, int iDataLength)
        {
            byte[] aTypeAndSize = new byte[5];
            aTypeAndSize[0] = 1;
            byte[] aLength = BitConverter.GetBytes(iDataLength);
            //Array.Reverse(aLength);
            Array.Copy(aLength, 0, aTypeAndSize, 1, aLength.Length);

            clientSocket.BeginSend(aTypeAndSize, 0, 5, 0, new AsyncCallback(ACB_SendTypeAndLength), clientSocket);

            // Begin sending the data to the remote device.
            clientSocket.BeginSend(aData, 0, iDataLength, 0, new AsyncCallback(ACB_SendData), clientSocket);
        }

        private void ACB_SendData(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                string sTxt = String.Format("Sent {0} bytes to Server.", bytesSent);
                m_adapter.AddConsoleMessage(sTxt);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private void ACB_SendTypeAndLength(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                string sTxt = String.Format("Sent {0} bytes to Server.", bytesSent);
                m_adapter.AddConsoleMessage(sTxt);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private bool SendString(string sData)
        {
            if (!clientSocket.Connected)
                return false;

            try
            {
                semaphor.WaitOne();
                Send(sData);
                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }

        }

        private bool SendBytes(Byte[] aData,int iDataLength)
        {
            if (!clientSocket.Connected)
                return false;
            try
            {
                semaphor.WaitOne();
                Send(aData,iDataLength);
                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }

        }
        #endregion

        #region Receiving methods


        /// <summary>
        /// Receiving Section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void StartReceive(object sender , DoWorkEventArgs e)
		{
            ConnectionStateClient state = new ConnectionStateClient();
            while (!(clientSocket.Poll(0, SelectMode.SelectRead) && clientSocket.Available == 0))
			{
                // Check to see if this NetworkStream is readable.
                if(this.networkStream.CanRead)
                {
                    // Create the state object.
                    state.workSocket = clientSocket;

                    if (state.dataLength>0)
                    {
                        state.m_receiveDataDone.Reset();
                        clientSocket.BeginReceive(state.buffer, state.actDataLength, state.dataLength-state.actDataLength, 0, new AsyncCallback(ACB_ReceiveData), state);
                        state.m_receiveDataDone.WaitOne();
                    }
                    else
                    {
                        // Begin receiving the data from the remote device.
                        state.m_receiveTypeAndLengthDone.Reset();
                        clientSocket.BeginReceive(state.buffer, 0, 5, 0, new AsyncCallback(ACB_ReceiveTypeAndLength), state);
                        state.m_receiveTypeAndLengthDone.WaitOne();
                    }
                }
            }
            this.OnServerDisconnected(new EventArgs());
            this.Disconnect();
        }

        private void ACB_ReceiveTypeAndLength(IAsyncResult ar)
        {
            ConnectionStateClient state = (ConnectionStateClient)ar.AsyncState;
            try 
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                Socket client = state.workSocket;
                if (client.Connected)
                {
                    // Read data from the remote device.
                    int bytesRead = client.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        state.type = state.buffer[0];
                        state.dataLength = BitConverter.ToInt32(state.buffer, 1);
                        if (state.type > 1 || (state.dataLength < 0 || state.dataLength > cFileSendBufferSize))
                        {
                            m_adapter.AddConsoleMessage("Error: unknown Type-id!");
                        }
                        else
                        {
                            string sTxt = String.Format("received datalength (Type={0},Length={1})", state.type, state.dataLength);
                            m_adapter.AddConsoleMessage(sTxt);
                            state.actDataLength = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_adapter.AddConsoleMessage(String.Format("ReceiveTypeAndLength: Error={0}",ex.Message));
            }
            finally
            {
                state.m_receiveTypeAndLengthDone.Set();
            }
        }

        private void ACB_ReceiveData(IAsyncResult ar)
        {
           ConnectionStateClient state = (ConnectionStateClient)ar.AsyncState;
           try
            {
	            Socket client = state.workSocket;
                if (client.Connected)
                {
                    // Read data from the remote device.
                    int bytesRead = client.EndReceive(ar);
                    if (bytesRead>0)
                    {
                        string sTxt = String.Format("received data (Type={0},Length={0},Received={1},ActualData={2})", state.type, state.dataLength, bytesRead, state.actDataLength);
                        m_adapter.AddConsoleMessage(sTxt);
                        state.actDataLength += bytesRead;

                        if (state.actDataLength < state.dataLength)
                        {
                            sTxt = String.Format("some more data (ActualData={0})", state.type, state.dataLength, bytesRead, state.actDataLength);
                            m_adapter.AddConsoleMessage(sTxt);
                        }
                        else
                        {
                            //state.dataLength = bytesRead;
                            sTxt = String.Format("received all data (Type={0},Length={1},ActualData={2})", state.type, state.dataLength, state.actDataLength);
                            m_adapter.AddConsoleMessage(sTxt);
                            ProcessMessage(state);
                            state.dataLength = 0;
                            state.type = 0;
                            state.actDataLength = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_adapter.AddConsoleMessage(String.Format("ReceiveData: Error={0}", ex.Message));
            }
            finally
            {
                state.m_receiveDataDone.Set();
            }
       }

        private void ProcessMessage(ConnectionStateClient state)
        {
            if (state.type == 0)
            {
                state.sb.AppendFormat("{0}", Encoding.Unicode.GetString(state.buffer, 0, state.dataLength));
                if (state.sb.Length > 0 &&
                    m_dataFilter.AnalyseData(state.sb.ToString(), out m_commandString) &&
                    m_commandString.Length > 0)
                {
                    int cmdStartFound = m_commandString.IndexOf('#');
                    if (cmdStartFound >= 0)
                        CheckReceivedCommand(ref m_commandString);
                    this.OnCommandReceived(new EventArgs());
                    state.sb.Clear();
                }
            }
            else if (state.type==1)
            {
                if (m_transferData.IsActive)
                {
                    m_transferData.DoTransfer(state.buffer, state.dataLength);
                    string sTxt = String.Format("id: {0}, PercDone: {1}", m_transferData.PackageId, m_transferData.PercDone);
                    m_adapter.AddConsoleMessage(sTxt);
                    CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.TransferProgress, m_transferData.TypeName, m_transferData.Name, m_transferData);
                    m_parent.FireEvent(ref gea);
                }
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

		private void DoTestPermission(string userName,string mapName, string testName,int retValue)
		{
			CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.TestPermission,userName,mapName,testName,retValue);
			m_parent.FireEvent(ref gea);

			if (m_jobDone!=null)
				m_jobDone.Set();
		}

        private void DoMapProgress(string userName, string mapName, int retValue)
        {
            CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.MapProgress, userName, mapName, retValue);
            m_parent.FireEvent(ref gea);

            if (m_mapProgressDone != null)
                m_mapProgressDone.Set();
        }

        private void DoWorkProgress(string userName, string mapName, string workName, string retValues)
        {
            if (workName == "all")
            {
                CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.WorkProgress, userName, mapName, workName, retValues);
                m_parent.FireEvent(ref gea);
            }
            else
            {
                int retValue = Int32.Parse(retValues);
                CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.WorkProgress, userName, mapName, workName, retValue);
                m_parent.FireEvent(ref gea);
            }

            if (m_workProgressDone != null)
                m_workProgressDone.Set();
        }

        private void DoPasswordChanged(string userName, string password, int retValue)
        {
            CTSClientEventArgs gea = new CTSClientEventArgs(CTSClientEventArgs.CommandType.PasswordChanged, userName, retValue);
            m_parent.FireEvent(ref gea);

            // Nur bei diesem User-Login Anfrage beenden
            if (userName == m_requestString)
            {
                if (m_jobDone != null)
                    m_jobDone.Set();
            }
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
            else if (typeName == "CLASSES")
            {
                if (!Directory.Exists(m_adapter.GetClassesFolderPath()))
                    Directory.CreateDirectory(m_adapter.GetClassesFolderPath());
                m_transferData = new CTSClientTransferData(typeName, name, m_adapter.GetClassesFolderPath()+@"\classes.xml", fileSize);
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
                            SendString(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}}}", nic.Count, i + 1, ni.userName, ni.contentPath, ni.pageId, ni.title, ni.workedOutState, Path.GetFileName(ni.fileName),ni.ModificationDateString));
                            ++iSent;
                        }

                        if (ni.title == name)
                            break;
                    }
                }

                if (iSent == 0)
                {
                    SendString(String.Format("#SENDNOTICE{{{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}}}", 0, 0, userName, "", 0, "", 0, "",""));
                    SendString(String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, 0, 0));
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

        private void DoSendNotice(int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle, int iWorkedOut, string strFileName,string strModificationDate)
        {
            m_adapter.AddNotice(iCount, iId, userName, contentPath, iPageId, strTitle, iWorkedOut, strFileName, strModificationDate);
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

			Trace.WriteLine("<--"+cmd);
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
						DoTestPermission(aTokens[0],aTokens[1],aTokens[2],Int32.Parse(aTokens[3]));
                    else if (aCommands[0] == "#MAPPROGRESS")
                        DoMapProgress(aTokens[0], aTokens[1], Int32.Parse(aTokens[2]));
                    else if (aCommands[0] == "#WORKPROGRESS")
                        DoWorkProgress(aTokens[0], aTokens[1], aTokens[2], aTokens[3]);
                    else if (aCommands[0] == "#PASSWORDCHANGED")
                        DoPasswordChanged(aTokens[0], aTokens[1], Int32.Parse(aTokens[2]));
                    else if (aCommands[0] == "#TRANSFERREQUEST")
                        DoTransferRequest(aTokens[0], aTokens[1]);
                    else if (aCommands[0] == "#SENDNOTICEWORKOUTSTATE")
                        DoSendNoticeWorkoutState(aTokens[0],Int32.Parse(aTokens[1]));
                    else if (aCommands[0] == "#SENDNOTICE")
                        DoSendNotice(Int32.Parse(aTokens[0]), Int32.Parse(aTokens[1]), aTokens[2], aTokens[3], Int32.Parse(aTokens[4]), aTokens[5], Int32.Parse(aTokens[6]), aTokens[7], aTokens[8]);
					else
						return false;
					return true;
				}
			}
			return false;
		}


        private void CheckReceivedCommand(ref string commandString)
		{
			string dataString="";  // Zunächst keine Daten im Stream

			// Prüfen ob irgendwo im Stream Kommandos vorkommen
			if (commandString.IndexOf("#TRANSFERSTART")!=0 &&
				commandString.IndexOf("#TRANSFEREND")!=0 &&
				commandString.IndexOf("#LOGIN")!=0 &&
				commandString.IndexOf("#LOGOUT")!=0 &&
				commandString.IndexOf("#RECEIVECLASSMESSAGE")!=0 &&
                commandString.IndexOf("#SENDNOTICEWORKOUTSTATE") != 0 &&
                commandString.IndexOf("#SENDNOTICE") != 0 &&
				commandString.IndexOf("#TESTPERMISSION")!=0 &&
				commandString.IndexOf("#MAPPROGRESS")!=0 &&
                commandString.IndexOf("#PASSWORDCHANGED") != 0 &&
				commandString.IndexOf("#WORKPROGRESS")!=0)
			{
				// Daten vor dem Kommando?
				int found=0;
				if ((found=commandString.IndexOf("#TRANSFER"))>0)
				{
					dataString = commandString.Substring(0,found);
					commandString = commandString.Substring(found);
				}
				// Keine Kommandos gefunden?
				else if (found<0)
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

			if (commandString.Length>0)
				if (!DoCommand(ref commandString))
					return;

			if (commandString.Length>0)
				CheckReceivedCommand(ref commandString);
		}


        private void SendFile(string typeName, string name, int cnt, int id, string fileName)
        {
            if (!File.Exists(fileName)) 
                return;
            System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

            SendString(String.Format("#TRANSFERSTART{{{0}|{1}|{2}|{3}|{4}|{5}}}", typeName, name, cnt, id, fi.Name, fi.Length));

            int iRead = 0;
            Stream inStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            while (inStream.Position < inStream.Length)
            {
                Byte[] buf = new Byte[cFileSendBufferSize];
                int cntRead = inStream.Read(buf, 0, cFileSendBufferSize);
                if (cntRead > 0)
                {
                    if (!SendBytes(buf, cntRead))
                        break;
                    iRead += cntRead;
                    Thread.Sleep(20);
                    //Console.WriteLine(String.Format("Daten zum Server gesendet:{0} ,Gesamtdaten: {1}", cntRead, iRead));
                }
            }
            inStream.Close();


            SendString(String.Format("#TRANSFEREND{{{0}|{1}|{2}|{3}}}", typeName, name, cnt, id));
        }


       #endregion
   }
}
