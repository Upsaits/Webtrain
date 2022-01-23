using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpreadsheet.Model.CopyOperation;
using SoftObject.SOComponents.Controls;
using SoftObject.SOComponents.Forms;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.ClientServer;
using SoftObject.TrainConcept.Controls;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        private class ClassMessage
        {
            public ClassMessage(int roomId, string userName, string message)
            {
                RoomId = roomId;
                UserName = userName;
                Message = message;
            }

            public int RoomId { get; set; }
            public string UserName { get; set; }
            public string Message { get; set; }
        };

        private ControlCenter m_ctrlCenter = null;
        private Bar m_ctrlCenterBar = null;
        private Bar m_libOverviewBar = null;
        private LibraryOverview m_libOverview = null;
        private Bar m_noticeBar = null;
        private SONotice m_notice = null;
        private string m_actUserName = "";
        private Bar m_menuBar;
        private Bar m_toolBar;
        private Bar m_closeBar;
        private ButtonItem m_actBtnItem = null;
        private AutoResetEvent m_jobDone = new AutoResetEvent(false);
        private int m_loginReturn = -1;
        private readonly Dictionary<string, bool> m_dUsersConnState = new Dictionary<string, bool>();
        private SOTaskbarNotifier m_taskNotifier;
        private ResourceHandler rh = null;
        private readonly ArrayList m_aMessages = new ArrayList();
        private readonly ConcurrentQueue<ClassMessage> m_aClassMsgs = new ConcurrentQueue<ClassMessage>();
        private readonly Queue[] m_aqChatroomMsgs = new Queue[4];
        private static bool stm_bFirstLoginTry = true;
        private static bool stm_bRegisterAs = false;
        private bool m_bWasAskedForOffline = false;
        private string m_lastTransferredFilename = "";
        private static IntPtr stm_toastId = (IntPtr) 0;
        private static bool stm_bUpdatesShown = false;
        private bool m_bLogoutActive = false;
        private AppHandler AppHandler = Program.AppHandler;

        #region Getter- and Setter

        public Bar ToolBar
        {
            get { return m_toolBar; }
        }

        public Bar CloseBar
        {
            get { return m_closeBar; }
        }

        public Bar MenuBar
        {
            get { return m_menuBar; }
        }

        public ControlCenter ControlCenter
        {
            get { return m_ctrlCenter; }
        }

        public Bar LibOverviewBar
        {
            get { return m_libOverviewBar; }
        }

        public LibraryOverview LibOverview
        {
            get { return m_libOverview; }
        }

        public Bar NoticeBar
        {
            get { return m_noticeBar; }
        }

        public SONotice Notice
        {
            get { return m_notice; }
        }

        public string ActualUserName
        {
            get { return m_actUserName; }
        }

        public AutoResetEvent JobDone
        {
            get { return JobDone; }
        }

        public Dictionary<string, bool> UsersConnState
        {
            get { return m_dUsersConnState; }
        }

        public SOTaskbarNotifier TaskbarNotifier
        {
            get { return m_taskNotifier; }
        }

        public System.Collections.ArrayList Messages
        {
            get { return m_aMessages; }
        }

        public Point CircularProgressLocation 
        {
            get
            {
                Rectangle r = dockSite3.RectangleToScreen(dockSite3.ClientRectangle);
                return new Point(r.Right-160,r.Bottom-50); 
            }
        }
    
        #endregion

		public FrmMain()
		{
			// Wird standardmäßig hier aufgerufen, hat aber in unserer
			// Anwendung den Nachteil, daß FrmMain() schon beim Konstruktor
			// initialisiert werden würde, daß wollen wir extra mit Initialize()
			// erledigen!
			//InitializeComponent();
		}

		//Initialisiert das Hauptfenster
		public bool Initialize()
		{
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);

			InitializeComponent();

			this.Icon = rh.GetIcon("main");
			this.Name = "MainForm";
			this.Text = AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");

			for(int i=0;i<m_aqChatroomMsgs.Length;++i)
				m_aqChatroomMsgs.SetValue(new Queue(),i);

            messageTimer.Start();
            transferTimer.Start();

            return true;
		}

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == (int)SoftObject.UtilityLibrary.Win32.Msg.WM_CLOSE)
        //        Application.Exit();
        //}

        private void TransferStart(string name)
        {
            lblUserName.Text = "";
        }

        private void TransferEnd(string strTypeName, string strName,CTSClientTransferData transferData)
        {
            if (strTypeName == "LIBRARIES" && strName == "*")
                AppHandler.LibManager.OpenFromImport(AppHandler.LibsFolder, transferData.FileName);
            else if (strTypeName == "LEARNMAPS" && strName == "*")
                AppHandler.MapManager.OpenFromImport(AppHandler.MapsFolder, transferData.FileName);
            else if (strTypeName == "CLASSES" && strName == "*")
            {
                //AppHandler.ClassManager.OpenFromImport(AppHandler.ClassesFolder, transferData.FileName);
                var clsMgr = AppHandler.ClassManager; // actual ClassManager
                string[] aActClasses;
                clsMgr.GetClassNames(out aActClasses); // get all actual classes

                var tempClsMgr = new DefaultClassroomManager(); // create new class manager
                tempClsMgr.Open(transferData.FileName);
                string[] aNewClasses;
                tempClsMgr.GetClassNames(out aNewClasses); // get new classes

                for (int i=0;i<aNewClasses.Length;++i) // run through all classes
                {
                    string strClass = aNewClasses[i];
                    if (clsMgr.GetClassId(strClass) >= 0) // if class is already there
                    {
                        string[] aNewMapNames;
                        if (tempClsMgr.GetLearnmapNames(strClass, out aNewMapNames) > 0) // add all learnmaps from new class
                            foreach (var m in aNewMapNames)
                                clsMgr.AddLearnmap(strClass, m);
                    }
                }

            }

            lblUserName.Text = "";
        }

        private void TransferProgress(string strFilePath, int val)
        {
            if (lblUserName.Visible && lblUserName.IsHandleCreated)
                this.BeginInvoke((MethodInvoker) delegate
                {
                    ToastNotification.UpdateToast(this, stm_toastId, Path.GetFileName(strFilePath));
                    lblUserName.Text = String.Format("{0}% übertragen", val);
                });
        }
        

		#region InitializeBars
		private void InitializeBars()
		{
			this.SuspendLayout();

			////////////////////////////////////
			//--- Menu-Bar
			////////////////////////////////////
            m_menuBar = this.dotNetBarManager1.Bars["barMainMenu"];
            string txt = AppHandler.LanguageHandler.GetText("MENU", "System", "&System");
            m_menuBar.Items["bSystem"].Text = txt;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Login_change_user", "&Anmelden");
            m_menuBar.Items["bSystem"].SubItems["bRegister"].Text = txt;
            m_menuBar.Items["bSystem"].SubItems["bRegister"].Click += IdmRegister_Click;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Login_as", "Anmelden a&ls");
            m_menuBar.Items["bSystem"].SubItems["bRegisterAs"].Text = txt;
            m_menuBar.Items["bSystem"].SubItems["bRegisterAs"].Click += IdmRegisterAs_Click;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Exit", "&Beenden");
            m_menuBar.Items["bSystem"].SubItems["bExit"].Text = txt;
            m_menuBar.Items["bSystem"].SubItems["bExit"].Click += IdmExit_Click;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Help", "&Hilfe");
            m_menuBar.Items["bSystem"].SubItems["bHelp"].Text = txt;
            m_menuBar.Items["bSystem"].SubItems["bHelp"].Click += IdmHelp_Click;
            
            txt = AppHandler.LanguageHandler.GetText("MENU", "About", "&Über..");
            m_menuBar.Items["bSystem"].SubItems["bAbout"].Text = txt;
            m_menuBar.Items["bSystem"].SubItems["bAbout"].Click += IdmAbout_Click;

            m_menuBar.Font = AppHandler.DefaultFont;
	
			////////////////////////////////////
			//--- Tool-Bar
			////////////////////////////////////
            m_toolBar = this.dotNetBarManager1.Bars["barToolBar"];
            txt = AppHandler.LanguageHandler.GetText("MENU", "Login", "&Anmelden");
            m_toolBar.Items["bRegister2"].Text = txt;
            m_toolBar.Items["bRegister2"].Click += new System.EventHandler(this.IdmRegister_Click);

            txt = AppHandler.LanguageHandler.GetText("MENU", "Print", "&Drucken");
            m_toolBar.Items["bPrint"].Text = txt;
			m_toolBar.Items["bPrint"].Enabled = false;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Notices", "&Notizen");
            m_toolBar.Items["bNoticeCenter"].Text = txt;
			m_toolBar.Items["bNoticeCenter"].Visible = false;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Glossary", "&Lexikon");
            m_toolBar.Items["bGlossary"].Text = txt;
			m_toolBar.Items["bGlossary"].Visible = false;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Messages", "&Nachrichten");
            m_toolBar.Items["bMessages"].Text = txt;
			m_toolBar.Items["bMessages"].Visible = false;

            txt = AppHandler.LanguageHandler.GetText("MENU", "Navigator", "&Navigator");
            m_toolBar.Items["bLibOverview"].Text= txt;
            m_toolBar.Items["bLibOverview"].Visible = false;
            m_toolBar.Items["bLibOverview"].Click += IdmLibOverview_Click;

            txt = AppHandler.LanguageHandler.GetText("MENU", "DINISOSim", "&DIN-ISO Sim");
            m_toolBar.Items["bStartSim"].Text= txt;
            m_toolBar.Items["bStartSim"].Visible = false;
            m_toolBar.Items["bStartSim"].Click += IdmStartSim_Click;
            m_toolBar.Font = AppHandler.DefaultFont;
            m_toolBar.Visible = true;

            m_closeBar = this.dotNetBarManager1.Bars["barCloseBar"];
            txt = AppHandler.LanguageHandler.GetText("MENU", "Close", "&Fenster schliessen");
            m_closeBar.Items["bClose"].Text = txt;
            m_closeBar.Items["bClose"].Click += IdmClose_Click;
            m_closeBar.Font = AppHandler.DefaultFont;
            m_closeBar.Visible = false;
            //m_closeBar.DockOffset = this.Size.Width - m_closeBar.Size.Width;
 
            
			////////////////////////////////////
			//--- Library-Overview
			////////////////////////////////////
            Bar newBar = this.dotNetBarManager1.Bars["barNavigatorBar"];
			LibraryOverview libView=new LibraryOverview();
            (newBar.Items["NavigatorItem"] as DockContainerItem).Control = libView;
            txt = AppHandler.LanguageHandler.GetText("SYSTEM", "NavigatorBar", "Inhalte");
            newBar.Text = txt;
            m_libOverviewBar = newBar;
			m_libOverview = libView;


            //////////////////////////////////////
            ////--- Notizen
            //////////////////////////////////////
            newBar = this.dotNetBarManager1.Bars["barNoticeBar"];
			SONotice notice=new SONotice();
            (newBar.Items["NoticeItem"] as DockContainerItem).Control = notice;
            txt = AppHandler.LanguageHandler.GetText("SYSTEM", "Notices", "Notizen");
            m_noticeBar = newBar;
			m_notice = notice;


            //////////////////////////////////////
            ////--- Control-Center-Bar
            //////////////////////////////////////
            newBar = this.dotNetBarManager1.Bars["barControlCenter"];
			ControlCenter control=new ControlCenter();
            (newBar.Items["ControlCenterItem"] as DockContainerItem).Control = control;
            txt = AppHandler.LanguageHandler.GetText("SYSTEM", "Controlcenter", "Kontrollzentrum");
            newBar.Text = txt;
			m_ctrlCenterBar = newBar;
			m_ctrlCenter = control;
			m_ctrlCenter.m_sideBar.ItemClick += new System.EventHandler(this.ControlCenter_ItemClick);


            //////////////////////////////////////
            ////--- Taskbar Notifier
            //////////////////////////////////////
			m_taskNotifier=new SOTaskbarNotifier();
			m_taskNotifier.SetBackgroundBitmap(rh.GetBitmap("Taskbar_MsgSkin"),Color.FromArgb(255,0,255));
			m_taskNotifier.SetCloseBitmap(rh.GetBitmap("Taskbar_MsgClose"),Color.FromArgb(255,0,255),new Point(150,8));
			m_taskNotifier.TitleRectangle=new Rectangle(40,9,90,25);
			m_taskNotifier.ContentRectangle=new Rectangle(8,41,153,68);
			m_taskNotifier.CloseClickable=true;
			m_taskNotifier.TitleClickable=true;
			m_taskNotifier.ContentClickable=true;
			m_taskNotifier.EnableSelectionRectangle=true;
			m_taskNotifier.KeepVisibleOnMousOver=true;	
			m_taskNotifier.ReShowOnMouseOver=true;

			this.ResumeLayout(false);
		}


		#endregion

        #region Login and Logout
		private void TryLoginAsClient()
		{
            // we are already trying to login
            if (bkgworkerClientLogin.IsBusy)
                return;

            // close ClientManager and attach 
            AppHandler.CtsClientManager.Close();
            AppHandler.CtsClientManager.OnCTSClientEvent += new OnCTSClientHandler(FrmMain_CTSClient);

            stm_toastId=ToastNotification.Show(this, "               Bitte warten...(Server wird kontaktiert)              ", null, 300000, eToastGlowColor.Blue, eToastPosition.BottomLeft);

            bkgworkerClientLogin.RunWorkerAsync(); // async connect
		}

		private string AfterTryLoginAsClient()
        {
            bool bSendOk = false;
            if (!stm_bRegisterAs)
            {
                if (AppHandler.CtsClientManager.IsRunning()) // Server found?
                {
                    // refresh Userdata
                    AppHandler.UserManager.Close();
                    bSendOk = AppHandler.CtsClientManager.SendTransferRequest("USERLIST", "Benutzerdaten", ref m_jobDone);
                    AppHandler.UserManager.Open(AppHandler.UserFileName);
                    Update();
                }
                else if (stm_bFirstLoginTry) // Server not found -> ask for offline mode
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Do_you_want_to_work_offline");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    m_bWasAskedForOffline = true;
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        m_bWasAskedForOffline = false;
                        ToastNotification.Close(this);
                        return "";
                    }
                }
            }

            string strUserName;
            string strPassword;

            // if normal register and (first try and loginname is ok)?
            if (!stm_bRegisterAs && 
                (stm_bFirstLoginTry && (AppHandler.LoginName.Length > 0)))
            {
                // check password
                string password = "";
                string fullName = "";
                int iImgId = 0;
                AppHandler.UserManager.GetUserInfo(AppHandler.LoginName, ref password, ref fullName, ref iImgId);
                if (String.Compare(AppHandler.LoginPassword, password, true) != 0)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Invalid_password");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    stm_bFirstLoginTry = false;
                    ToastNotification.Close(this);
                    return "";
                }
                strUserName = AppHandler.LoginName;
                strPassword = AppHandler.LoginPassword; // ok.
            }
            else
            {
                ToastNotification.UpdateToast(this, stm_toastId, "Bitte anmelden...");
                // register as OR not first login or invalid loginname

                // show registration dialog
                FrmRegistration dlg = new FrmRegistration(AppHandler.LoginName, AppHandler.LoginPassword);
                dlg.StartPosition = FormStartPosition.CenterScreen;
                DialogResult res=dlg.ShowDialog();

                if (res == DialogResult.Retry) // Server was checked and is connected
                {
                    // refresh user data
                    AppHandler.UserManager.Close();
                    bSendOk = AppHandler.CtsClientManager.SendTransferRequest("USERLIST", "Benutzerdaten", ref m_jobDone);
                    AppHandler.UserManager.Open(AppHandler.UserFileName);

                    dlg = new FrmRegistration(AppHandler.LoginName, AppHandler.LoginPassword);
                    dlg.StartPosition = FormStartPosition.CenterScreen;
                    res = dlg.ShowDialog();
                }
                // dialog ok but User data not found or client not running or username invalid
                else if (res==DialogResult.OK &&
                         ((!AppHandler.UserManager.IsOpen()) ||
                         ((!m_bWasAskedForOffline && !AppHandler.CtsClientManager.IsRunning()) || dlg.UserName.Length == 0)))
                {

                    // can not reach server
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ToastNotification.Close(this);
                    return "";
                }
                else if (res != DialogResult.OK) // cancel register dialog
                {
                    ToastNotification.Close(this);
                    return "";
                }

                strUserName = dlg.UserName; // everything is fine-> set user name and pwd
                strPassword = dlg.Password;
            }

            stm_bFirstLoginTry = true;
            stm_bRegisterAs = false;

            // send login to Server
            bSendOk = AppHandler.CtsClientManager.IsRunning() && AppHandler.CtsClientManager.SendLogin(strUserName, strPassword, ref m_jobDone);

			if (!bSendOk) // cannot reach server
			{
                stm_bFirstLoginTry = false;

                // load local libraries & learnmaps
				AppHelpers.LoadLearnmaps();
				AppHelpers.LoadLibraries();

				if (!AppHandler.UserManager.IsOpen()) // user data available?
				{
                    // Nein-> abbrechen
                    string txt =AppHandler.LanguageHandler.GetText("ERROR","Cant_reach_server");
					string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
					MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
                    ToastNotification.Close(this);
                    return "";
				}

                // check rights of logged in user
				bool isAdmin=false;
				bool isTeacher=false;
                if (!AppHandler.UserManager.GetUserRights(strUserName, ref isAdmin, ref isTeacher))
                {
                    ToastNotification.Close(this);

                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Unknown_user");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ToastNotification.Close(this);
                    return "";
                }
						
				// can work offline?
				if (!isAdmin && AppHandler.MapManager.GetMapCnt()==0)
				{
                    ToastNotification.Close(this);

                    // no -> exit
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
					string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
					MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
                    ToastNotification.Close(this);
                    return "";
				}
				else
				{
					// yes -> if not already asked, ask for offline mode
                    if (!m_bWasAskedForOffline)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Do_you_want_to_work_offline");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        m_bWasAskedForOffline = true;
                        if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            ToastNotification.Close(this);
                            return "";
                        }
                    }

                    LoginUser(strUserName); // ok -> Login offline
                    ToastNotification.Close(this);
                    return strUserName;
                }
            }

            lock (m_actUserName) // wait for completed communication with Client see FrmMain_CTSClient()
            {
                if (m_loginReturn > 0)
                {
                    if (m_loginReturn == 1)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Unknown_user");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        stm_bFirstLoginTry = false;
                    }
                    else if (m_loginReturn == 2)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Invalid_password");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        stm_bFirstLoginTry = false;
                    }
                    else if (m_loginReturn == 3)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "User_already_online");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (m_loginReturn == 4 || m_loginReturn == 5)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Too_many_users_online");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    m_actUserName = "";
                    m_loginReturn = 0;

                    ToastNotification.Close(this);
                    return "";
                }
            }

			Update();
			
			// delete all local learnmaps & libraries
			AppHelpers.DestroyAllLearnmaps();
            AppHelpers.DeleteLibraries();

            ToastNotification.UpdateToast(this,stm_toastId,"Bitte warten...(Lernmappen werden übertragen)");

			// get learnmaps from server
			string txt1 = AppHandler.LanguageHandler.GetText("MESSAGE","Learnmaps","Lernmappen");
			if (!AppHandler.CtsClientManager.SendTransferRequest("LEARNMAPS",txt1,ref m_jobDone))
			{
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ToastNotification.Close(this);
                return "";
			}

			Update();

            AppHelpers.LoadLearnmaps();

            // learnmaps received?
			if (AppHandler.MapManager.GetMapCnt()==0)
			{
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "No_learnmaps");
				string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
				MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);

				LogoutUser(); // no learnmaps, nothing to do -> Exit
                ToastNotification.Close(this); 
                return "";
			}
            

            // go through learnmaps and load missing libraries
            ToastNotification.UpdateToast(this, stm_toastId, "Bitte warten...(Bibliotheken werden übertragen)");

            var dLoadedLibs = new Dictionary<string, bool>();
            bool msgShown = false;
			for(int i=0;i<AppHandler.MapManager.GetMapCnt();++i)
			{
				if (AppHandler.MapManager.HasUser(i,m_actUserName))
				{
					string [] aWorkings=null;
					if(AppHandler.MapManager.GetWorkings(i,ref aWorkings))
					{
						for(int j=0;j<aWorkings.Length;++j)
						{
							if (AppHandler.LibManager.GetPoint(aWorkings[j])==null)
							{	
								string [] aTitles;
								Utilities.SplitPath(aWorkings[j],out aTitles);
                                string libTitle = aTitles[0];
                                if (dLoadedLibs.ContainsKey(libTitle))
								{
                                    if (!msgShown && !dLoadedLibs[libTitle])
									{
                                        ToastNotification.Close(this);

                                        string txt = AppHandler.LanguageHandler.GetText("WARNING", "not_all_learnmaps_could_be_assigned_correctly");
										string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
										MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Warning);
										msgShown=true;
									}
								}
								else
								{
                                    if (!AppHandler.CtsClientManager.SendTransferRequest("LIBRARIES", libTitle, ref m_jobDone))
									{
                                        if (!m_bLogoutActive)
                                        {
                                            string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                                            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                            MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        ToastNotification.Close(this);
                                        return "";
									}

                                    lock (m_lastTransferredFilename)
                                    {
                                        AppHandler.LibManager.Open(AppHandler.LibsFolder, m_lastTransferredFilename);
                                    }

                                    Update();

                                    if (AppHandler.LibManager.GetLibrary(libTitle) != null)
                                        dLoadedLibs[libTitle] = true;
                                    else
                                    {
                                        if (!dLoadedLibs.ContainsKey(libTitle))
                                        {
                                            string txt = AppHandler.LanguageHandler.GetText("ERROR", "Library_X_not_found_on_server", "Library {0} not found on Server!");
                                            txt = String.Format(txt, libTitle);
                                            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                            MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            dLoadedLibs[libTitle] = false;
                                        }
                                    }
								}
							}
						}
					}
				}
			}

            ToastNotification.UpdateToast(this,stm_toastId,"Bitte warten...(Notizen werden übertragen)");

            // Notizen vom Server holen
            if (!AppHandler.CtsClientManager.SendTransferRequest("NOTICES", "*", ref m_jobDone))
            {
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ToastNotification.Close(this);

                return "";
            }

            ToastNotification.UpdateToast(this,stm_toastId,"Bitte warten...(Dokumente werden übertragen)");

            txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "Documents", "Dokumente");
            if (!AppHandler.CtsClientManager.SendTransferRequest("DOCUMENTS", txt1, ref m_jobDone))
            {
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ToastNotification.Close(this);

                return "";
            }

            // log progress: Session Start
            // (LoByte = 1): if Client is running Server automatically should send request for notice-update
            AppHelpers.AddUserProgressInfo(strUserName, "", (int)UserProgressInfoManager.RegionType.Session, HelperMacros.MAKEWORD(1, 1));

            LoginUser(m_actUserName);

            ToastNotification.Close(this);
            return m_actUserName;
        }

        private void TryLoginAsStandAlone()
        {
            if (bkgWorkerStandAloneLogin.IsBusy)
                return;

            // close ClientManager and attach 
            if (!AppHandler.CtsClientManager.IsRunning())
            {
                AppHandler.CtsClientManager.Close();
                AppHandler.CtsClientManager.OnCTSClientEvent += new OnCTSClientHandler(FrmMain_CTSClient);
            }

            stm_toastId = ToastNotification.Show(this, "                    Bitte warten...(Server wird kontaktiert)                 ", null, 300000, eToastGlowColor.Blue, eToastPosition.BottomLeft);

            bkgWorkerStandAloneLogin.RunWorkerAsync();
        }

        private void AfterTryLoginAsStandAlone()
        {
            if (!AppHandler.CtsClientManager.IsRunning()) // Server found?
            {
                // Fehlermeldung: Brauche Verbindung zum Server!
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cannot_reach_server","Server nicht erreichbar");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                string strTemp = String.Format("{0} : {1}", txt, AppCommunication.AutoServerName);
                ToastNotification.Close(this);
                MessageBox.Show(strTemp, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // refresh Userdata
            AppHandler.UserManager.Close();
            bool bSendOk = AppHandler.CtsClientManager.SendTransferRequest("USERLIST", "Benutzerdaten", ref m_jobDone);
            if (!bSendOk) // cannot reach server
            {
                // Fehlermeldung: Brauche Verbindung zum Server!
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cannot_get_usersdata", "Teilnehmerliste nicht gefunden");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                string strTemp = String.Format("{0} : {1}", txt, AppCommunication.AutoServerName);
                ToastNotification.Close(this);
                MessageBox.Show(strTemp, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            AppHandler.UserManager.Open(AppHandler.UserFileName);
            Update();

            ToastNotification.UpdateToast(this, stm_toastId, "Bitte anmelden...");
            
            FrmRegistration dlg = new FrmRegistration(AppHandler.LoginName, AppHandler.LoginPassword);
            var eDlgResult = dlg.ShowDialog();
            string strUserName = dlg.UserName;
            string strPassword = dlg.Password;

            if (eDlgResult == DialogResult.Cancel)
            {
                ToastNotification.Close(this);
                return;
            }
            else if (eDlgResult == DialogResult.No)
            {
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Invalid_password");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                ToastNotification.Close(this);
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bSendOk = false;
            if (strUserName != null && strUserName.Length > 0)
            {
                // send login to Server
                bSendOk = AppHandler.CtsClientManager.SendLogin(strUserName, strPassword, ref m_jobDone);
            }

            if (!bSendOk) // cannot reach server
            {
                // Fehlermeldung: Brauche Verbindung zum Server!
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cannot_reach_server", "Server nicht erreichbar");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                string strTemp = String.Format("{0} : {1}", txt, AppCommunication.AutoServerName);
                MessageBox.Show(strTemp, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ToastNotification.Close(this);
                return;
            }

            lock (m_actUserName) // wait for completed communication with Client see FrmMain_CTSClient()
            {
                if (m_loginReturn > 0)
                {
                    if (m_loginReturn == 1)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Unknown_user");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (m_loginReturn == 2)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Invalid_password");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (m_loginReturn == 3)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "User_already_online");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (m_loginReturn == 4 || m_loginReturn == 5)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Too_many_users_online");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    m_actUserName = "";
                    m_loginReturn = 0;
                    ToastNotification.Close(this);
                    return;
                }
            }

            AppHelpers.LoadClasses();

            ToastNotification.UpdateToast(this, stm_toastId, "Bitte warten...(Klassen werden übertragen)");

            // get learnmaps from server
            if (!AppHandler.CtsClientManager.SendTransferRequest("CLASSES", "*", ref m_jobDone))
            {
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    ToastNotification.Close(this);
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }


            Update();
            
            AppHelpers.LoadLearnmaps();
            AppHelpers.DeleteAllServerMaps();

            ToastNotification.UpdateToast(this, stm_toastId, "Bitte warten...(Lernmappen werden übertragen)");

            // get learnmaps from server
            if (!AppHandler.CtsClientManager.SendTransferRequest("LEARNMAPS", "*", ref m_jobDone))
            {
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    ToastNotification.Close(this);
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            Update();

            // go through learnmaps and load libraries
            ToastNotification.UpdateToast(this, stm_toastId, "Bitte warten...(Bibliotheken werden übertragen)");

            if (!AppHandler.CtsClientManager.SendTransferRequest("LIBRARIES", "*", ref m_jobDone))
            {
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    ToastNotification.Close(this);
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            lock (m_lastTransferredFilename)
            {
                if (!AppHelpers.LoadLibraries())
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "libraries_not_found");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ToastNotification.Close(this);
                    return;
                }
            }

            ToastNotification.UpdateToast(this, stm_toastId, "Bitte warten...(Notizen werden übertragen)");

            // Notizen vom Server holen
            if (!AppHandler.CtsClientManager.SendTransferRequest("NOTICES", "*", ref m_jobDone))
            {
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    ToastNotification.Close(this);
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            ToastNotification.UpdateToast(this, stm_toastId, "Bitte warten...(Dokumente werden übertragen)");

            if (!AppHandler.CtsClientManager.SendTransferRequest("DOCUMENTS", "*", ref m_jobDone))
            {
                if (!m_bLogoutActive)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Cant_reach_server");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    ToastNotification.Close(this);
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            ToastNotification.Close(this);

            // log progress: Session Start
            // (LoByte = 1): if Client is running Server automatically should send request for notice-update
            AppHelpers.AddUserProgressInfo(strUserName, "", (int)UserProgressInfoManager.RegionType.Session, HelperMacros.MAKEWORD(1, 1));

            LoginUser(m_actUserName);

        }

        private string LoginAsServer()
        {
            if (!AppHandler.UserManager.IsOpen() || AppHandler.UserManager.GetUserCount()==0)
                CreateStandardUser();

            if (!AppHandler.ClassManager.IsOpen() || AppHandler.ClassManager.GetClassCount() == 0)
                CreateStandardClassroom();
                
                
            string strUserName;
            string strPassword;
            DialogResult eDlgResult = DialogResult.OK;

            if (stm_bFirstLoginTry && (AppHandler.LoginName.Length > 0 && AppHandler.LoginPassword.Length > 0))
            {
                string password = "";
                string fullName = "";
                int iImgId = 0;
                AppHandler.UserManager.GetUserInfo(AppHandler.LoginName, ref password, ref fullName, ref iImgId);
                if (String.Compare(AppHandler.LoginPassword, password, true) != 0)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "Invalid_password");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    stm_bFirstLoginTry = false;
                    return null;
                }

                strUserName = AppHandler.LoginName;
                strPassword = AppHandler.LoginPassword;
            }
            else
            {
                FrmRegistration dlg = new FrmRegistration(AppHandler.LoginName, AppHandler.LoginPassword);
                eDlgResult = dlg.ShowDialog();
                strUserName = dlg.UserName;
                strPassword = dlg.Password;
            }

            if (eDlgResult == DialogResult.OK)
            {
                AppHelpers.LoadLearnmaps();
                AppHelpers.LoadClasses();
                if (!AppHelpers.LoadLibraries())
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "libraries_not_found");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }

                stm_bFirstLoginTry = true;
                return strUserName;
            }
            else if (eDlgResult == DialogResult.No)
            {
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Invalid_password");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return null;

        }


        void TryLoginAsServer()
        {
            if (bkgworkerServerLogin.IsBusy)
                return;

            AppHandler.CtsServerManager.Close();
            AppHandler.CtsServerManager.OnCTSServerEvent += new OnCTSServerHandler(this.FrmMain_CTSServer);

            AppHandler.CtsVPNServerManager.Close();
            AppHandler.CtsVPNServerManager.OnCTSServerEvent += new OnCTSServerHandler(this.FrmMain_CTSServer);

            bkgworkerServerLogin.RunWorkerAsync();
        }

        void AfterTryLoginAsServer()
        {
            if (AppHandler.CtsServerManager.IsRunning())
            {
                //AppCommunication.SetIPAddress(AppHandler.CtsServerManager.GetIPAddress().ToString());
                AppHandler.CtsVPNServerManager.Create(AppHandler.PortNr, ref m_jobDone);
                if (!AppHandler.CtsVPNServerManager.IsRunning())
                    AppHandler.CtsVPNServerManager.GetAdapter().ShowConsole(false);

            }
            
            string strUserName = LoginAsServer();
            if (strUserName != null && strUserName.Length > 0)
            {
                m_menuBar.Items["bSystem"].SubItems["bRegisterAs"].Enabled = false;
                m_toolBar.Items["bStartSim"].Visible = true;
                m_toolBar.RecalcLayout();

                AppHelpers.LoadLearnmaps();
                AppHelpers.LoadClasses();
                if (!AppHelpers.LoadLibraries())
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "libraries_not_found");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                LoginUser(strUserName);

                BeginInvoke((Action)(() =>
                {
                    AppCommunication.GetTransferedLearnmaps();
                }));


                // log progress: Session Start
                AppHelpers.AddUserProgressInfo(strUserName, "", (int)UserProgressInfoManager.RegionType.Session, HelperMacros.MAKEWORD(1, 0));
            }
        }

		private void LoginUser(string userName)
		{
			m_actUserName=userName;
            String strTitle = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            if (AppHandler.IsServer)
            {
                this.Text = String.Format("{0}-Server({1}), angemeldet als {2}",strTitle,AppHandler.CtsServerManager.GetIPAddress().ToString(),m_actUserName);
            }
            else if (AppHandler.IsClient)
            {
                this.Text = String.Format("{0}-Client, angemeldet als {1}", strTitle, m_actUserName);
            }
            else
                this.Text = String.Format("{0}-Studio, angemeldet als {1}", strTitle, m_actUserName);

            SetUserConnectionState(m_actUserName,true);

			bool isAdmin=false;
			bool isTeacher=false;
			AppHandler.UserManager.GetUserRights(userName,ref isAdmin,ref isTeacher);

            string fullName="";
            string passWord="";
            int iImgId=0;
            AppHandler.UserManager.GetUserInfo(userName, ref passWord, ref fullName, ref iImgId);
            this.lblUserName.Text = String.Format(" '{0}' angemeldet als '{1}' ", fullName,m_actUserName);

			m_ctrlCenter.SetupSideBar(isAdmin,AppHandler.IsServer,isTeacher);
			m_ctrlCenterBar.Visible=true;

			m_toolBar.Items["bRegister2"].Enabled=true;
			m_menuBar.Items["bSystem"].SubItems["bRegister"].Enabled = true;
			m_toolBar.Items["bRegister2"].Text = AppHandler.LanguageHandler.GetText("SYSTEM","Logout","Abmelden");
			m_menuBar.Items["bSystem"].SubItems["bRegister"].Text = m_toolBar.Items["bRegister2"].Text;

			if (AppHandler.IsClient || AppHandler.IsServer)
			{
				m_toolBar.Items["bMessages"].Visible= true;
				((ButtonItem) m_toolBar.Items["bMessages"]).PopupContainerLoad += new System.EventHandler(this.OnMessagesPopupLoad);
				((ButtonItem) m_toolBar.Items["bMessages"]).Click += new System.EventHandler(this.OnMessagesClicked);
				m_toolBar.RecalcLayout();

				if (AppHandler.IsServer)
				{
					AppHandler.ContentManager.OpenCTSStudents(this,false);
				}
			}
		}


		private void LogoutUser()
		{
            // log progress: Session End
            AppHelpers.AddUserProgressInfo(m_actUserName, "", (int)UserProgressInfoManager.RegionType.Session, HelperMacros.MAKEWORD(0, 0));
            
            AppHandler.ContentManager.CloseAll();

			m_ctrlCenterBar.Hide();
			m_libOverviewBar.Hide();
			m_noticeBar.Hide();

			SetUserConnectionState(m_actUserName,false);

            if (AppHandler.IsClient || AppHandler.IsSingle)
            {
                AppHandler.CtsClientManager.Close();
                AppHandler.CtsClientManager.OnCTSClientEvent -= new OnCTSClientHandler(FrmMain_CTSClient);
            }
            else
            {
                // Alle User ausloggen !
                AppHandler.CtsServerManager.SendLogoutAllUsers();
                AppHandler.CtsServerManager.Close();
                AppHandler.CtsServerManager.OnCTSServerEvent -= new OnCTSServerHandler(this.FrmMain_CTSServer);

                AppHandler.CtsVPNServerManager.SendLogoutAllUsers();
                AppHandler.CtsVPNServerManager.Close();
                AppHandler.CtsVPNServerManager.OnCTSServerEvent -= new OnCTSServerHandler(this.FrmMain_CTSServer);
            }

            m_actUserName = "";
            this.lblUserName.Text = "";
            this.Text = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");

            m_bWasAskedForOffline = false;
            stm_bFirstLoginTry = true;
		}

		//Öffne Dialogbox Anmeldung
		private void TryToLogin()
        {
            // Client?
			if (AppHandler.IsClient)
			{
                TryLoginAsClient();
			}
            // ->Server
			else if (AppHandler.IsServer)
            {
                // Keine Userdaten vorhanden?
                if (!AppHandler.UserManager.IsOpen())
                {
                    // Standard-user anlegen
                    CreateStandardUser();
                    AppHandler.UserManager.Save(AppHandler.UserFileName);
                }
                // -> Userdaten vorhanden!
                else
                    UpdateUserList();

                TryLoginAsServer();
            }
            else
                TryLoginAsStandAlone();
        }
#endregion

        private void CreateStandardUser()
        {
            // Administrator anlegen
            AppHandler.UserManager.CreateUser("Administrator", "admin", "admin");
            AppHandler.UserManager.SetUserRights("admin", true, true);
            AppHandler.UserManager.Save(AppHandler.UserFileName);
            AppHandler.UserManager.Open(AppHandler.UserFileName);
            stm_bFirstLoginTry = false;
        }

        private void CreateStandardClassroom()
        {
            string filePath = AppHandler.ClassesFolder + "\\" + "classes.xml";
            AppHandler.ClassManager.Save(filePath);
        }


        public bool IsUserConnected(string strUser)
        {
            bool bResult = false;
            if (strUser == ActualUserName)
                bResult=true;
            else if (m_dUsersConnState.ContainsKey(strUser))
                bResult = m_dUsersConnState[strUser];
            return bResult;
        }

        private void SetUserConnectionState(string userName, bool isActive)
		{
            if (userName.Length > 0)
			{
                if (!m_dUsersConnState.ContainsKey(userName))
                    m_dUsersConnState.Add(userName, isActive);
                else
                    m_dUsersConnState[userName] = isActive;
			}
		}

        public bool IsAdminUserConnected()
        {
            var user=m_dUsersConnState.Where(s => s.Key.Contains("admin") && s.Value==true).Count();
            return (m_actUserName!="admin" ? (user>=1) : (user>=2));
        }
		
        public void UpdateUserList()
		{
            string[] aUsers;
            AppHandler.UserManager.GetUserNames(out aUsers);
            if (aUsers != null)
                for (int i = 0; i < aUsers.Length; ++i)
                    if (m_dUsersConnState.ContainsKey(aUsers[i]))
                        SetUserConnectionState(aUsers[i], false);

            for (int i = 0; i < 4; ++i)
			{
                FrmChatroom frmClass = (FrmChatroom)AppHandler.ContentManager.GetChatroom(i + 1);
                if (frmClass != null)
                    frmClass.UpdateUserList();
            }

            var frmActiveView = AppHandler.ContentManager.GetActiveChild();
            if (frmActiveView is XFrmUserEdit)
                (frmActiveView as XFrmUserEdit).UpdateUserList();
		}

        public void ShowNotice(string strTitle, string strFileName, string strDocFileName="", bool bCanDelete=false,SONotice.DeleteNoticeCallBack fnOnDeleteNotice=null)
        {
            Console.WriteLine(strDocFileName);
            int nId = AppHandler.NoticeManager.Find(ActualUserName, strTitle);
            if (nId >= 0)
            {
                NoticeBar.Visible = true;
                Notice.Load(nId,strFileName, strDocFileName,GetNoticeTitle, Utilities.GetSolutionFile, OnSaveNotice, GetNoticeWorkedOutState, DoNoticeSubmit,false, SetNoticeWorkedOutState, bCanDelete, fnOnDeleteNotice);

                if (GetNoticeWorkedOutState(nId) < 0)
                    NoticeBar.Text = "Notiz";
                else
                    NoticeBar.Text = "Aufgabe";

                if (NoticeBar.DockSide != eDockSide.None)
                {
                    NoticeBar.InitalFloatLocation = new Point(100, 100);
                    NoticeBar.DockSide = eDockSide.None;
                    NoticeBar.Location = new Point(Location.X + 200, Location.Y+200);
                }
            }
        }

        public void ShowDocumentAsRtf(string strTitle, string strFileName, bool bEditSolution = false)
        {
            NoticeBar.Visible = true;
            Notice.Load(-1,strFileName, "", iNoticeId => strTitle, Utilities.GetSolutionFile, null, null, null,false,null,false,null,bEditSolution);
            NoticeBar.Text = "Dokument";
                
            if (NoticeBar.DockSide != eDockSide.None)
            {
                NoticeBar.InitalFloatLocation = new Point(100, 100);
                NoticeBar.DockSide = eDockSide.None;
                NoticeBar.Location = new Point(Location.X + 200, Location.Y + 200);
            }
        }



        #region Dongle Checks

		public bool CheckServerUserCount(int cnt)
		{
			return AppHandler.MaxOnlineUsers >=cnt;
		}

        #endregion
        
        #region Delegates
        public void ReceiveClassMessage(int roomId, string fromUserName, string message)
        {
            m_aClassMsgs.Enqueue(new ClassMessage(roomId,fromUserName,message));
            var frmChat = AppHandler.ContentManager.GetChatroom(roomId);
            if (frmChat != null)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    ((FrmChatroom)frmChat).UpdateMsgs();
                });
           }
             
        }

        private void OnSaveNotice(string fileName)
        {
            string strFileName = Path.GetFileName(fileName);
            int nId = AppHandler.NoticeManager.Find(strFileName);
            if (nId >= 0)
            {
                var ni = AppHandler.NoticeManager.GetNotice(nId);
                if (ni != null)
                {
                    ni.ModificationDate = DateTime.Now;
                    //MessageBox.Show(String.Format("NoticeId={0}, Date={1}", nId, ni.ModificationDateString));
                    AppHandler.NoticeManager.SetDirty(nId, true);
                    if (AppHandler.IsClient || AppHandler.IsSingle)
                        AppHandler.CtsClientManager.SendNotice(ni.title);
                    else
                        AppHandler.CtsServerManager.SendNotice(ni.userName, ni.title);
                }
            }

        }

        private string GetNoticeTitle(int iNoticeId)
        {
            if (iNoticeId >= 0)
            {
                var ni = AppHandler.NoticeManager.GetNotice(iNoticeId);
                return ni.title;
            }
            return "";
        }

        private int GetNoticeWorkedOutState(int iNoticeId)
        {
            if (iNoticeId >= 0)
            {
                var ni = AppHandler.NoticeManager.GetNotice(iNoticeId);
                return ni.workedOutState;
            }
            return 0;
        }

        private void SetNoticeWorkedOutState(int iNoticeId, int iState)
        {
            if (iNoticeId >= 0)
            {
                var ni = AppHandler.NoticeManager.GetNotice(iNoticeId);
                if (ni.workedOutState != iState)
                {
                    ni.workedOutState = iState;
                    AppHandler.NoticeManager.Save();
                    if (AppHandler.IsServer)
                    {
                        AppHandler.CtsServerManager.SendNoticeWorkoutState(ni.title, ni.workedOutState, ni.userName);
                        AppHandler.CtsVPNServerManager.SendNoticeWorkoutState(ni.title, ni.workedOutState, ni.userName);
                    }
                    else
                    {
                        AppHandler.CtsClientManager.SendNoticeWorkoutState(ni.userName,ni.title, ni.workedOutState);
                    }
                }
            }
        }

        private bool DoNoticeSubmit(int iNoticeId,string strFilename, string strSolFilename,ref string strDiffResult)
        {
            bool bResult = false;

            RichEditControl richEditControl1=new RichEditControl();
            RichEditControl richEditControl2 = new RichEditControl();

            richEditControl1.LoadDocument(strFilename, DocumentFormat.Rtf);
            richEditControl2.LoadDocument(strSolFilename, DocumentFormat.Rtf);

            string strText1 = richEditControl1.Document.HtmlText;
            string strText2 = richEditControl2.Document.HtmlText;

            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(strText1, strText2);

            // Lets add a block expression to group blocks we care about (such as dates)
            diffHelper.AddBlockExpression(new Regex(@"[\d]{1,2}[\s]*(Jan|Feb)[\s]*[\d]{4}", RegexOptions.IgnoreCase));

            string strResult = diffHelper.Build(out var iFoundDiffCount, out var iFoundInsCount, out var iFoundDelCount, out var iFoundReplCount);
            
            if (iFoundDiffCount > 0 || iFoundReplCount > 0 || iFoundInsCount > 0 || iFoundDelCount > 0)
                bResult=false;
            //webBrowser1.DocumentText = strResult;
            else
                bResult = true;
            //webBrowser1.DocumentText = "<!DOCTYPE html><html><body><h1>Sie sind gleich</h1></body></html>";
            strDiffResult = strResult;
            return bResult;
        }

        #endregion

        #region Event Handling
        private void OnItemClick(object sender, System.EventArgs e)
        {
            if (m_toolBar.Items==null)
            {
                if (sender is ButtonItem)
                {
                    ButtonItem item = sender as ButtonItem;
                    switch (item.Name)
                    {
                	    case "bRegister":   IdmRegister_Click(sender, e); break;
                        case "bRegisterAs": IdmRegisterAs_Click(sender, e); break;
                        case "bExit":       IdmExit_Click(sender, e); break;
                        case "bHelp":       IdmHelp_Click(sender,e); break;
                        case "bAbout":      IdmAbout_Click(sender, e); break;
                        case "bLibOverview":IdmLibOverview_Click(sender, e); break;
                        case "bClose":      IdmClose_Click(sender, e); break;
                    }
                }
            }
        }
        
		private void IdmRegister_Click(object sender, System.EventArgs e)
		{
			if (m_actUserName.Length>0)
			{
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Do_you_really_want_to_logout", "Wollen sie sich wirklich abmelden?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    m_bLogoutActive = true;
                    LogoutUser();

                    txt = AppHandler.LanguageHandler.GetText("SYSTEM", "Login", "Anmelden");
                    m_toolBar.Items["bRegister2"].Text = txt;
                    m_menuBar.Items["bSystem"].SubItems["bRegister"].Text = txt;

                    m_menuBar.Items["bSystem"].SubItems["bRegisterAs"].Enabled = true;

                    if (AppHandler.IsClient || AppHandler.IsServer || AppHandler.IsSingle)
                    {
                        m_toolBar.Items["bMessages"].Visible = false;
                        ((ButtonItem)m_toolBar.Items["bMessages"]).PopupContainerLoad -= new System.EventHandler(this.OnMessagesPopupLoad);
                        ((ButtonItem)m_toolBar.Items["bMessages"]).Click -= new System.EventHandler(this.OnMessagesClicked);
                    }

                    m_toolBar.Items["bLibOverview"].Visible = false;
                    m_toolBar.Items["bStartSim"].Visible = false;
                    m_toolBar.RecalcLayout();
                    m_bLogoutActive = false;
                }
            }
			else
			{
                TryToLogin();
			}
		}

        private void IdmRegisterAs_Click(object sender, System.EventArgs e)
        {
            stm_bRegisterAs = true;
            TryToLogin();
        }


		private void IdmExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

		private void IdmHelp_Click(object sender, System.EventArgs e)
		{
			string sPath = AppHandler.HelpFolder+"\\tchelp_"+AppHandler.Language+".chm";
			System.Windows.Forms.Help.ShowHelp(this,sPath);
		}

        private void IdmUpdates_Click(object sender, EventArgs e)
        {
            string url = String.Format(AppHandler.WebtrainServer + "/update.aspx?Version={0}&Language={1}&UseType={2}&KeyId={3}", AppHandler.VersionString, AppHandler.Language, AppHandler.UseType, AppHandler.DongleId);
            AppHandler.ContentManager.OpenBrowser(this, "Updates", url, new Rectangle(0,50,900,630));
            this.Update();
        }

		private void IdmAbout_Click(object sender, System.EventArgs e)
		{
			FrmAbout frmAbout = new FrmAbout();
			frmAbout.ShowDialog();
		}

		private void IdmLibOverview_Click(object sender, System.EventArgs e)
		{
			this.LibOverview.UpdateView();
			m_libOverviewBar.Visible = true;
		}

		private void IdmStartSim_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo("CSLSimTest.exe");
			System.Diagnostics.Process.Start(si);
		}

        private void IdmClose_Click(object sender, System.EventArgs e)
        {
            Form frmActive = AppHandler.MainForm.ActiveMdiChild;
            if (frmActive is FrmContent)
            {
                FrmContent frmContent = frmActive as FrmContent;
                if (!(frmContent.ActiveView is ContentTestingControl))
                {
                    AppHandler.ContentManager.CloseLearnmap(frmContent.MapTitle);
                }
            }
            else if (frmActive is FrmLMEditContentNew)
            {
                FrmLMEditContentNew frmLMEdit = frmActive as FrmLMEditContentNew;
                AppHandler.ContentManager.CloseLearnmapEditor(frmLMEdit.MapTitle);
            }
            else if (frmActive is XFrmOnlineUsers)
                AppHandler.ContentManager.CloseCTSStudents();
            else if (frmActive is FrmChatroom)
            {
                FrmChatroom frmChatroom = frmActive as FrmChatroom;
                AppHandler.ContentManager.CloseChatroom(frmChatroom.RoomId);
            }
            else if (frmActive is FrmBrowser)
            {
                FrmBrowser frmBrowser = frmActive as FrmBrowser;
                AppHandler.ContentManager.CloseBrowser(frmBrowser.Title,frmBrowser.URL);
            }
            else if (frmActive is FrmEditContent)
                AppHandler.ContentManager.CloseContentEditor();
            else if (frmActive is XFrmUserEdit)
                AppHandler.ContentManager.CloseUserEditor();
            else if (frmActive is XFrmMapsEdit)
                AppHandler.ContentManager.CloseLearnmapDistributor();
            else if (frmActive is XFrmClassroomEdit)
            {
                XFrmClassroomEdit frmClass = frmActive as XFrmClassroomEdit;
                AppHandler.ContentManager.CloseClassroom(frmClass.ClassName);
            }

        }

		private void ControlCenter_ItemClick(object sender, System.EventArgs e)
		{
			BaseItem item=sender as BaseItem;

			if(item==null)
				return;

			if (m_actBtnItem!=null)
				m_actBtnItem.Checked=false;

			if (item is ButtonItem)
			{
				ExplorerBarGroupItem panel = (ExplorerBarGroupItem) item.Parent;
				ButtonItem btnItem = (ButtonItem) item;
				if (String.Compare(panel.Name,"WorkoutLearnmapPanel",true)==0)
				{
					AppHandler.ContentManager.OpenLearnmap(this,btnItem.Text);
				}
				if (String.Compare(panel.Name,"EditLearnmapPanel",true)==0)
				{
                    if (btnItem.Name == "NewLearnmapItem")
						ControlCenter.CreateNewEditLearnmap();
					else
						AppHandler.ContentManager.OpenLearnmapEditor(this,btnItem.Text);
				}
				else if (String.Compare(panel.Name,"CTSPanel",true)==0)
				{
					AppHandler.ContentManager.OpenCTSStudents(this,true);
				}
				else if (String.Compare(panel.Name,"SystemPanel",true)==0)
				{
                    if (String.Compare(btnItem.Name, "SettingsItem", true) == 0)
                    {
                        XFrmEditSettings dlg= new XFrmEditSettings();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                        }
                    }
                    else if (String.Compare(btnItem.Name, "UserAdministrationItem", true) == 0)
					{
						AppHandler.ContentManager.OpenUserEditor(this);
					}
                    else if (String.Compare(btnItem.Name, "LearnmapDistributionItem", true) == 0)
					{
						AppHandler.ContentManager.OpenLearnmapDistributor(this);
					}
                    else if (String.Compare(btnItem.Name, "LicenseStateItem", true) == 0)
					{
						FrmDongleState dlg = new FrmDongleState(AppHandler.DongleHandler);
						dlg.ShowDialog();
					}
					else if(String.Compare(btnItem.Name,"UpdatesItem",true)==0)
					{
						string url=String.Format(AppHandler.WebtrainServer+"/update.aspx?Version={0}&Language={1}&UseType={2}&KeyId={3}",AppHandler.VersionString,AppHandler.Language,AppHandler.UseType,AppHandler.DongleId);
						AppHandler.ContentManager.OpenBrowser(this,"Updates",url);
					}
					else if(String.Compare(btnItem.Name,"UpgradesItem",true)==0)
					{
                        string url=String.Format(AppHandler.WebtrainServer+"/webtrainpackages.aspx?Version={0}&Language={1}&UseType={2}&KeyId={3}", AppHandler.VersionString, AppHandler.Language, AppHandler.UseType, AppHandler.DongleId);
                        AppHandler.ContentManager.OpenBrowser(this, "Webtrain-Pakete", url);
                    }
					else if(String.Compare(btnItem.Name,"SelectLanguageItem",true)==0)
					{
						FrmChooseLanguage dlg = new FrmChooseLanguage();
						if (dlg.ShowDialog() == DialogResult.OK)
						{
							if (dlg.Language!=AppHandler.Language)
							{
								AppHandler.Language = dlg.Language;

								string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Change_Language","Zum Umstellen der Landessprache ist ein Neustart erforderlich. TrainConcept jetzt beenden?");
								string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
								if (MessageBox.Show(txt,cap,MessageBoxButtons.YesNo,MessageBoxIcon.Error) == DialogResult.Yes)
								{
									this.Close();
								}
							}
						}
					}
					else if (String.Compare(btnItem.Name,"EditContentItem",true)==0)
					{
						AppHandler.ContentManager.OpenContentEditor(this);
					}
                    else if (String.Compare(btnItem.Name, "SetupSystemAssistantItem", true) == 0)
                    {
                        FrmSetupWizard frm = new FrmSetupWizard();
                        frm.ShowDialog();
                    }
				}
				else if (String.Compare(panel.Name,"ChatPanel",true)==0)
				{
                    int roomId = panel.SubItems.IndexOf(btnItem.Name) + 1;
                    AppHandler.ContentManager.OpenChatroom(this, roomId, m_aqChatroomMsgs[roomId - 1]);
				}
				else if (String.Compare(panel.Name,"ClassPanel",true)==0)
				{
                    if (btnItem.Name == "NewClassItem")
						ControlCenter.CreateNewClassroom();
					else
                        AppHandler.ContentManager.OpenClassroom(this, btnItem.Text);
				}
				btnItem.Checked=true;
				m_actBtnItem=btnItem;
			}
			else if (item is ExplorerBarGroupItem)
			{
				ExplorerBarGroupItem panel = (ExplorerBarGroupItem) item;
				if (String.Compare(panel.Name,"WorkoutLearnmapPanel",true)==0)
					AppHandler.ContentManager.CloseAllLearnmapEditors();
			}
		}

		private void FrmMain_MdiChildActivate(object sender, System.EventArgs e)
		{
			AppHandler.ContentManager.SetActiveChild(this.ActiveMdiChild);
		}

		public void FrmMain_CTSServer(object sender,CTSServerEventArgs ea)
		{
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnCTSServerHandler(FrmMain_CTSServer), new object[] { sender, ea });
                return;
            }

            var ctsServer = sender as CTSServerManagerBridge;

			if (ea.Command==CTSServerEventArgs.CommandType.Login)
			{
                // check correct language 
				if (ea.Language.Length>0 && String.CompareOrdinal(ea.Language,AppHandler.Language)!=0)
				{
					ea.ReturnValue=6;
                    m_jobDone.Set();
                    return;
				}

                bool bIsAdmin=false;
                // admin can be logged in more than once -> extract admin name
                string userName = ea.UserName;
                if (ea.UserName.IndexOf('@')>0)
                {
                    userName = ea.UserName.Substring(0, ea.UserName.IndexOf('@'));
                    bIsAdmin=true;
                }

				string foundPassWord="";
				string fullName="";
                int iImgId = 0;
                int userId = AppHandler.UserManager.GetUserInfo(userName, ref foundPassWord, ref fullName, ref iImgId);
				if (userId<0)
				{
					ea.ReturnValue=1; // user not in the database
                    m_jobDone.Set();
					return;
				}

                if (!bIsAdmin && String.Compare(userName, m_actUserName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    ea.ReturnValue = 3; // user already connected	
                }
                else if (String.Compare(ea.Password, foundPassWord, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    ea.ReturnValue = 2; // user-Password not correct
                }
                else
                {
                    if (!bIsAdmin)
                    {
                        // Genügend Lizenzen vorhanden?
                        string sMsg = String.Format("CHECKUSERCOUNT({0})", AppHandler.CtsServerManager.GetUsersOnline() + 1);

                        if (ctsServer != null) 
                            ctsServer.GetAdapter().AddConsoleMessage(sMsg);

                        if (!CheckServerUserCount(AppHandler.CtsServerManager.GetUsersOnline() + 1))
                        {
                            ea.ReturnValue = 4; // Zu wenig Lizenzen!
                            m_jobDone.Set();
                            return;
                        }
                    }

                    XFrmOnlineUsers ctsStudents = (XFrmOnlineUsers)AppHandler.ContentManager.GetCTSStudents();
                    if (ctsStudents != null)
                    {
                        if (userName !="admin" || !IsAdminUserConnected())
                            ctsStudents.RegisterStudent(ea.Target, userName);
                    }

                    ea.ReturnValue = 0; // user is correct
                    m_jobDone.Set();
                 
                    SetUserConnectionState(ea.UserName, true);
                }
                m_jobDone.Set();
			}
			else if (ea.Command == CTSServerEventArgs.CommandType.Logout)
			{
				SetUserConnectionState(ea.UserName,false);

                XFrmOnlineUsers ctsStudents = (XFrmOnlineUsers)AppHandler.ContentManager.GetCTSStudents();
                if (ctsStudents != null)
                {
                    if (ea.UserName != "admin" || !IsAdminUserConnected())
                        ctsStudents.UnregisterStudent(ea.UserName);
                }
			}
            else if (ea.Command == CTSServerEventArgs.CommandType.Message)
            {
            }
            else if (ea.Command == CTSServerEventArgs.CommandType.ConnectionInfo)
            {
                ControlCenter.SetConnectionState(ea.IsOn);
            }
            else if (ea.Command == CTSServerEventArgs.CommandType.TransferStart)
            {
                if (ea.TransferData != null)
                    TransferStart(ea.TransferData.Name);
            }
            else if (ea.Command == CTSServerEventArgs.CommandType.TransferProgress)
            {
                if (ea.TransferData != null)
                    TransferProgress(ea.TransferData.FileName,(int)ea.TransferData.PercDone);
            }
            else if (ea.Command == CTSServerEventArgs.CommandType.TransferEnd)
            {
                TransferEnd(ea.TransferTypeName,ea.TransferName,ea.TransferData);
            }
        }

		private void FrmMain_CTSClient(object sender,CTSClientEventArgs ea)
		{
            if (this.InvokeRequired)
            {
                if (ea.Command == CTSClientEventArgs.CommandType.Login)
                {
                    lock (m_actUserName)
                    {
                        // admin can be logged in more than once -> extract admin name
                        string userName = ea.UserName;
                        if (ea.UserName.IndexOf('@') > 0)
                            userName = ea.UserName.Substring(0, ea.UserName.IndexOf('@'));

                        if (m_actUserName.Length == 0 || userName == m_actUserName)
                        {
                            m_actUserName = userName;
                            m_loginReturn = ea.ReturnValue;
                        }
                    }
                }
                else if (ea.Command == CTSClientEventArgs.CommandType.TransferEnd)
                {
                    if (ea.TransferTypeName == "LIBRARIES")
                    {
                        if (ea.TransferData != null)
                        {
                            lock (m_lastTransferredFilename)
                            {
                                m_lastTransferredFilename = ea.TransferData.FileName;
                            }
                        }
                    }
                    else if (ea.TransferTypeName == "NOTICES")
                    {
                        if (ea.TransferData == null)
                        {
                            var aNotices = new Libraries.NoticeItemCollection();
                            if (AppHandler.NoticeManager.Find(ref aNotices, ActualUserName) > 0)
                            {
                                foreach (var n in aNotices)
                                {
                                    if (n.workedOutState > 0 && n.workedOutState <= 6)
                                    {
                                        int nId = AppHandler.NoticeManager.Find(ActualUserName, n.title);
                                        if (nId >= 0)
                                            AppHandler.NoticeManager.DeleteNotice(nId);
                                    }
                                }

                            }
                        }
                    }
                }

                this.BeginInvoke(new OnCTSClientHandler(FrmMain_CTSClient), new object[] { sender, ea });
                return;
            }

			if (ea.Command == CTSClientEventArgs.CommandType.Login)
			{
                // admin can be logged in more than once -> extract admin name
                string userName = ea.UserName;
                if (ea.UserName.IndexOf('@') > 0)
                    userName = ea.UserName.Substring(0, ea.UserName.IndexOf('@'));

                if (userName != m_actUserName)
                {
                    SetUserConnectionState(userName, true);
                    // notify server about session start
                }
			}
			else if (ea.Command == CTSClientEventArgs.CommandType.Logout)
			{
                if (ea.UserName.Length > 0 && m_actUserName.Length>0)
                {
                    // admin can be logged in more than once -> extract admin name
                    string userName = ea.UserName;
                    if (ea.UserName.IndexOf('@') > 0)
                        userName = ea.UserName.Substring(0, ea.UserName.IndexOf('@'));

                    if (userName == m_actUserName && ea.ReturnValue == 0)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "logout_from_server", "Sie wurden vom Server abgemeldet!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IdmRegister_Click(this, EventArgs.Empty);
                    }
                    else
                    {
                        SetUserConnectionState(userName, false);
                        // notify server about session end
                        AppHelpers.AddUserProgressInfo(userName, "", (int)UserProgressInfoManager.RegionType.Session, HelperMacros.MAKEWORD(0, 0));
                    }
                }
			}
			else if (ea.Command == CTSClientEventArgs.CommandType.TransferStart)
			{
                if (ea.TransferData!=null)
				    TransferStart(ea.TransferData.Name);
			}
			else if (ea.Command == CTSClientEventArgs.CommandType.TransferProgress)
			{
                if (ea.TransferData != null)
                    TransferProgress(ea.TransferData.FileName, (int)ea.TransferData.PercDone);
			}
			else if (ea.Command == CTSClientEventArgs.CommandType.TransferEnd)
			{
			    if (ea.TransferData != null)
			        TransferEnd(ea.TransferTypeName, ea.TransferName, ea.TransferData);
			}
			else if (ea.Command == CTSClientEventArgs.CommandType.ReceiveClassMesssage)
			{
				Invoke(new ReceiveClassMessageDelegate(ReceiveClassMessage),new object[] {ea.RoomId,ea.UserName,ea.Message});
			}
            else if (ea.Command == CTSClientEventArgs.CommandType.ConnectionInfo)
            {
                ControlCenter.SetConnectionState(ea.IsOn);
            }
            else if (ea.Command == CTSClientEventArgs.CommandType.PasswordChanged)
            {

            }
        }
	
		private void OnMessagesPopupLoad(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;

			PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;

            
			ListBox lbox=new ListBox();
			lbox.BackColor = System.Drawing.Color.FloralWhite;
			lbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			lbox.Size = new System.Drawing.Size(420,70);
			lbox.SelectionMode = SelectionMode.MultiExtended;
			lbox.TabIndex = 2;
			lbox.KeyDown += MsgListBox_KeyDown;
            lbox.MouseDoubleClick += MsgListBox_MouseDoubleClick;


            for (int i = 0; i < m_aMessages.Count; ++i)
            {
                lbox.Items.Add((string)m_aMessages[i]);
            }

			container.Controls.Add(lbox);
										
			lbox.Location=container.ClientRectangle.Location;
			container.ClientSize=lbox.Size;
			item.AutoCollapseOnClick = false;
		}

        private void MsgListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListBox lst = sender as ListBox;
            if (lst.SelectedIndex >= 0)
            {
                AlertControl ctrl = new AlertControl();
                String strMessage = (string) m_aMessages[lst.SelectedIndex];
                //String strText = "<size=14>Size = 14<br>" +
                //            "<b>Bold</b> <i>Italic</i> <u>Underline</u><br>" +
                //            "<size=11>Size = 11<br>" +
                //            "<color=255, 0, 0>Sample Text</color></size>" +
                //            "<href=www.devexpress.com>Hyperlink</href>";
                //String strHotTrackedText = "Das ist ein Test";
                String strCaption = "<b>Nachricht eingetroffen!</b>";
                var info = new AlertInfo(strCaption,strMessage);
                ctrl.BeforeFormShow += ctrl_BeforeFormShow;
                ctrl.AllowHtmlText = true;
                ctrl.Show(this,info);
            }
        }
         
        void ctrl_BeforeFormShow(object sender, AlertFormEventArgs e)
        {
            var newPos = new Point(this.Location.X + Width - 200, this.Location.Y + Height - 300);
            //e.Location = this.Location + new Size(100, 100);
            e.Location = newPos;
            e.AlertForm.Width = 200;
            e.AlertForm.Height = 300;
        }

		private void OnMessagesClicked(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;
			item.Expanded = true;
		}

		private void MsgListBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				ListBox lst = sender as ListBox;
				for(int i=lst.SelectedIndices.Count;i>0;--i)
				{
					lst.Items.RemoveAt(i-1);
					m_aMessages.RemoveAt(i-1);
				}

				if (m_aMessages.Count==0)
					((ButtonItem) m_toolBar.Items["bMessages"]).ImageIndex = 5;
			}
		}

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitializeBars();

            if (AppHandler.CtsConsoleOn)
            {
                if (AppHandler.IsServer)
                {
                    AppHandler.CtsServerManager.GetAdapter().ShowConsole(false);
                    AppHandler.CtsVPNServerManager.GetAdapter().ShowConsole(false);
                }
                else
                    AppHandler.CtsClientManager.GetAdapter().ShowConsole(true);
            }

            Program.AppCommunication.StartWebtrainCommunication();
        }

        private void messageTimer_Tick(object sender, EventArgs e)
        {
            if (!this.m_aClassMsgs.IsEmpty)
            {
                ClassMessage sCM;
                m_aClassMsgs.TryDequeue(out sCM);

                string txt2 = String.Format("{0}: {1}", sCM.UserName, sCM.Message);
                m_aqChatroomMsgs[sCM.RoomId - 1].Enqueue(txt2);

                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Y_has_a_message_in_chatroom_X","{0} hat im Chatraum{1} eine Nachricht für Sie");
                string txt1 = String.Format(txt, sCM.UserName,sCM.RoomId);
                m_taskNotifier.Show("", txt1, 500, 3000, 500);

                string msg = String.Format("{0} {1}", DateTime.UtcNow.ToString(), txt1);
                m_aMessages.Add(msg);

                var frmChat = AppHandler.ContentManager.GetChatroom(sCM.RoomId);
                if (frmChat != null)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        ((FrmChatroom)frmChat).UpdateMsgs();
                    });
                }

            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void bkgworkerClientLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!m_bWasAskedForOffline)
            {
                if (AppCommunication.AutoServerNameChecked && AppCommunication.AutoServerName.Length > 0)
                    AppHandler.CtsClientManager.Create(AppCommunication.AutoServerName, AppHandler.PortNr);
                else if (AppHandler.ServerName.Length > 0)
                    AppHandler.CtsClientManager.Create(AppHandler.ServerName, AppHandler.PortNr);
            }
        }

        private void bkgworkerClientLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppHandler.MainForm.BeginInvoke((Action) (() =>
            {
                string strUserName = AfterTryLoginAsClient();
                if (strUserName != null && strUserName.Length > 0)
                {
                    AppHandler.CtsClientManager.StartKeepAlive();

                    m_menuBar.Items["bSystem"].SubItems["bRegisterAs"].Enabled = false;
                    m_toolBar.Items["bStartSim"].Visible = true;
                    m_toolBar.RecalcLayout();
                }
            }));
        }

        private void bkgworkerStandAloneLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            if (AppCommunication.AutoServerNameChecked && AppCommunication.AutoServerName.Length > 0)
                AppHandler.CtsClientManager.Create(AppCommunication.AutoServerName, AppHandler.PortNr);
            else if (AppHandler.ServerName.Length > 0)
                AppHandler.CtsClientManager.Create(AppHandler.ServerName, AppHandler.PortNr);
        }

        private void bkgworkerStandAloneLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppHandler.MainForm.BeginInvoke((Action)(() =>
            {
                AfterTryLoginAsStandAlone();
                if (m_actUserName != null && m_actUserName.Length > 0)
                    AppHandler.CtsClientManager.StartKeepAlive();
            }));
        }

        private void bkgworkerServerLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            AppHandler.CtsServerManager.Create(AppHandler.PortNr, ref m_jobDone);
        }

        private void bkgworkerServerLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppHandler.MainForm.BeginInvoke((Action)(() =>
            {
                AfterTryLoginAsServer();
            }));
        }


        private void connectionTimer_Tick(object sender, EventArgs e)
        {
            if (AppCommunication.AvailableUpdatesChecked)
            {
                //var rh = new SoftObject.UtilityLibrary.ResourceHandler(AppHandler.ResourceName, AppHandler.MainForm.GetType().Assembly);
                if (!AppCommunication.UpdatesAvailable)
                {
                    ((ButtonItem)m_menuBar.Items["bUpdates"]).Text = "keine neuen Updates";
                    ((ButtonItem)m_menuBar.Items["bUpdates"]).ForeColor = Color.DarkGreen;
                    ((ButtonItem)m_menuBar.Items["bUpdates"]).Image = rh.GetBitmap("true");
                }
                else
                {
                    if (!AppCommunication.AvailableUpdateFileChecked)
                    {
                        ((ButtonItem)m_menuBar.Items["bUpdates"]).Text = "Updates werden heruntergeladen";
                        ((ButtonItem)m_menuBar.Items["bUpdates"]).ForeColor = Color.Blue;
                        ((ButtonItem)m_menuBar.Items["bUpdates"]).Image = rh.GetBitmap("unchecked");
                        ((CircularProgressItem)m_menuBar.Items["cpUpdates"]).Start();
                    }
                    else if (!AppCommunication.UpdateFileAvailable)
                    {
                        ((ButtonItem)m_menuBar.Items["bUpdates"]).Text = "Updates verfügbar";
                        ((ButtonItem)m_menuBar.Items["bUpdates"]).ForeColor = Color.DarkRed;
                        ((ButtonItem)m_menuBar.Items["bUpdates"]).Image = rh.GetBitmap("false");
                        ((CircularProgressItem)m_menuBar.Items["cpUpdates"]).Stop();
                    }
                }

                if (AppCommunication.UpdateFileAvailable)
                {
                    Debug.WriteLine("Neue Version installieren");
                    ((ButtonItem)m_menuBar.Items["bUpdates"]).Text = "Updates können installiert werden";
                    ((ButtonItem)m_menuBar.Items["bUpdates"]).ForeColor = Color.DarkGreen;
                    ((ButtonItem)m_menuBar.Items["bUpdates"]).Image = rh.GetBitmap("checked");
                    ((CircularProgressItem)m_menuBar.Items["cpUpdates"]).Stop();


                    connectionTimer.Stop();

                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_start_update", "Update jetzt durchführen?");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Program.AppCommunication.StartAutoUpdate();
                    connectionTimer.Start();
                }

                if (!stm_bUpdatesShown)
                {
                    TaskbarNotifier.Show("", AppCommunication.AvailableUpdateMsg, 500, 10000, 500);
                    stm_bUpdatesShown = true;
                }
            }

            //AppCommunication.GetLaunchPackage();

        }

        private void transferTimer_Tick(object sender, EventArgs e)
        {
            if (AppCommunication.AvailableUpdatesChecked)
            {
                AppCommunication.GetTransferedLearnmaps();
                //AppCommunication.CheckUserList();
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool bDoIt = true;
            if (e.CloseReason != CloseReason.TaskManagerClosing)
            {
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "Do_you_want_to_exit_webtrain", "Wollen sie Webtrain wirklich beenden?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    e.Cancel = false;
                else
                {
                    bDoIt = false;
                    e.Cancel = true;
                }

            }
            else
                e.Cancel = false;

            if (bDoIt)
            {
                // log progress: Session end
                if (m_actUserName.Length > 0)
                    AppHelpers.AddUserProgressInfo(m_actUserName, "", (int)UserProgressInfoManager.RegionType.Session, HelperMacros.MAKEWORD(0, 0));
                if (AppHandler.IsClient || AppHandler.IsSingle)
                    AppHandler.CtsClientManager.Close();
                else
                {
                    AppHandler.CTSServerConsole.Close();
                    AppHandler.CTSVPNServerConsole.Close();
                    AppHandler.CtsServerManager.Close();
                    AppHandler.CtsVPNServerManager.Close();
                }

            }

        }
        #endregion


    }
}
        

