using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using SoftObject.TrainConcept.Libraries;
using SoftObject.UtilityLibrary;
using SoftObject.UtilityLibrary.Win32;
using Microsoft.Win32;
using SoftObject.TrainConcept.ClientServer;
using Microsoft.Web.Administration;
using IISManager;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Adapter;
using SoftObject.TrainConcept.Forms;


namespace SoftObject.TrainConcept
{

	/// <summary>
	/// Summary description for AppHandler.
	/// </summary>
	public class AppHandler
    {
        #region Imports for ProcessHandling
        [DllImport("kernel32.dll")]
		static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, 
										 IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
										 bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, 
										 string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo,
										 out PROCESS_INFORMATION lpProcessInformation);

		public struct PROCESS_INFORMATION
		{
			public IntPtr hProcess;
			public IntPtr hThread;
			public uint dwProcessId;
			public uint dwThreadId;
		}

		public struct STARTUPINFO
		{
			public uint cb;
			public string lpReserved;
			public string lpDesktop;
			public string lpTitle;
			public uint dwX;
			public uint dwY;
			public uint dwXSize;
			public uint dwYSize;
			public uint dwXCountChars;
			public uint dwYCountChars;
			public uint dwFillAttribute;
			public uint dwFlags;
			public short wShowWindow;
			public short cbReserved2;
			public IntPtr lpReserved2;
			public IntPtr hStdInput;
			public IntPtr hStdOutput;
			public IntPtr hStdError;
		}

		public struct SECURITY_ATTRIBUTES
		{
			public int length;
			public IntPtr lpSecurityDescriptor;
			public bool bInheritHandle;
        }

        //OPEN EVENT
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenEvent(UInt32
            dwDesiredAccess, Boolean bInheritHandle, String lpName);

        #endregion

        private String	m_language="de";
        private string m_privateDataFolder;
        private string m_serverName;
        private Mutex  m_mutex;
        private string m_loginName;
        private string m_loginPwd;
        private int    m_iMaxOnlineUsers;
        private string m_imgCertificationHeader;
        private StreamWriter m_streamWriter = null;
        private string m_contentEditMediaFolder = "";

        #region Getter & Setter
        public string WebtrainServer { get; private set; } = "";
        public IDEManagerBridge IdeManager { get; private set; } = null;
        public ContentManagerBridge ContentManager { get; private set; } = null;
        public FrmMain MainForm { get; private set; } = null;
        public UserManagerBridge UserManager { get; private set; } = null;
        public LibraryManagerBridge LibManager { get; private set; } = null;
        public LearnmapManagerBridge MapManager { get; private set; } = null;
        public CTSServerManagerBridge CtsServerManager { get; private set; } = null;
        public CTSServerManagerBridge CtsVPNServerManager { get; private set; } = null;
        public CTSClientManagerBridge CtsClientManager { get; private set; } = null;
        public NoticeManagerBridge NoticeManager { get; private set; } = null;
        public TestResultManagerBridge TestResultManager { get; private set; } = null;
        public ClassroomManagerBridge ClassManager { get; private set; } = null;
        public string WorkingFolder { get; private set; }
        public  string ExeFolder { get; private set; }
        public  string HelpFolder { get; private set; }
        public  bool IsServer { get; private set; } = false;
        public  bool IsClient { get; private set; } = false;
        public bool IsSingle { get; private set; } = false;
        public int LoginType { get; private set; } = 0;
        public  string ServerName
		{
			get
			{
				return m_serverName;
			}
			set
			{
				string sName=value.ToLower();
				ProfileHandler.WriteProfileString("SYSTEM","ServerName",sName);
                m_serverName=sName;

                if (IsClient)
                {
                    ContentFolder = ProfileHandler.GetProfileString("SYSTEM", "ContentFolder", "");
                    if (ContentFolder.Length == 0)
                        if (VirtualDirName.IndexOf(':') >= 0)
                            ContentFolder = "http://" + m_serverName + VirtualDirName + "/Content/" + m_language;
                        else
                            ContentFolder = "http://" + m_serverName + "/" + VirtualDirName + "/Content/" + m_language;
                }
            }
		}

		public  int PortNr { get; private set; }
        public  string VirtualDirName { get; private set; }
        public  string UserFileName { get; private set; }
        public string ClassesFolder { get; private set; }
        public  string ContentFolder { get; private set; }
        public  ProfileHandler ProfileHandler { get; private set; } = null;
        public string Language
		{
			get
			{
				return m_language;
			}

			set
			{
				ProfileHandler.WriteProfileString("SYSTEM","Language",value);
			}
		}
		public  LanguageHandler LanguageHandler { get; private set; } = null;
        public  string LibsFolder { get; private set; }
        public  string MapsFolder { get; private set; }
        public  string LanguageFolder { get; private set; }
        public  int UseType { get; private set; } = 0;
        public int KeepAliveTimer { get; private set; }
        public  bool CtsConsoleOn { get; private set; }
        public int TimeOutStandard { get; private set; }
        public  int TimeOutTransfer { get; private set; }
        public string ResourceName { get; private set; } = "SoftObject.TrainConcept.res.webtrain.resources_softobject";
        public  bool IsEditContent { get; private set; }
        public System.Drawing.Font DefaultFont
		{
			get
			{
				if (String.Compare(Language,"zh_cn",StringComparison.OrdinalIgnoreCase)==0)
					return new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				else
					return new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			}
		}

        public System.Drawing.Font ContentModeButtonFont
        {
            get
            {
                if (String.Compare(Language, "zh_cn", StringComparison.OrdinalIgnoreCase) == 0)
                    return new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                else
                    return new System.Drawing.Font("Tahoma", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            }
        }

		public string VersionString
		{
			get 
			{
				Assembly assembly = Assembly.GetCallingAssembly();
				AssemblyName assemblyname = assembly.GetName();
				Version assemblyver = assemblyname.Version;
				return assemblyver.ToString();
			}
        }

        public PicturePoolHandler UserAvatars { get; private set; } = null;
        public UserProgressInfoManager UserProgressInfoMgr { get; private set; } = null;

        public string LoginName
        {
            get 
            {
                return m_loginName; 
            }
            set 
            {
                ProfileHandler.WriteProfileString("SYSTEM", "LoginName", value);
                m_loginName = value; 
            }
        }

        public string LoginPassword
        {
            get 
            {
                return m_loginPwd; 
            }
            set 
            {
                string strCodedPwd = "";
                EncodePassword(value, ref strCodedPwd);
                ProfileHandler.WriteProfileString("SYSTEM", "LoginPassword", strCodedPwd);
                m_loginPwd = value; 
            }
        }

        public TCDongleHandler DongleHandler { get; private set; } = null;
        public int DongleId { get; private set; } = -1;
        public int MaxOnlineUsers
        {
            get 
            {
                if (m_iMaxOnlineUsers > 0)
                    return m_iMaxOnlineUsers;
                else
                    return UserManager.GetUserCount();
            }
        }

        public string ImgCertificationHeader
        {
            get 
            {
                return m_imgCertificationHeader;
            }
            set 
            {
                m_imgCertificationHeader = value;
                ProfileHandler.WriteProfileString("CERTIFICATION", "HeaderImage", m_imgCertificationHeader);
            }
        }

        public string LicenseId { get; set; }
        public string ImportExportFolder { get; private set; }
        public string ReportsFolder { get; private set; }
        public string ContentEditMediaFolder
        {
            get { return m_contentEditMediaFolder; }
            set 
            {
                m_contentEditMediaFolder = value;
                ProfileHandler.WriteProfileString("SYSTEM", "ContentEditMediaFolder", m_contentEditMediaFolder);
            }
        }

        public Dictionary<int, string> Templates { get; } = new Dictionary<int, string>();
        public Dictionary<int, string> PageActionTextItems { get; } = new Dictionary<int, string>();
        public string ConfigAdminInfo { get; private set; } = "";
        public FrmCTSServerConsole CTSServerConsole { get; private set; } = null;
        public FrmCTSServerConsole CTSVPNServerConsole { get; private set; } = null;

        #endregion

        public AppHandler()
        {

        }

        void InitBridges()
        {
            try
            {
                RegistryKey key = GetRegistrySoftwareKey(@"SoftObject\TrainConcept");
                if (key != null)
                {
                    object val = key.GetValue("Logfile");
                    if (val != null)
                    {
                        m_streamWriter = new StreamWriter((string)val);
                        m_streamWriter.AutoFlush = true;
                        m_streamWriter.WriteLine("Start");
                    }
                } 
                
                
               EnsureBrowserEmulationEnabled();
               
               MainForm =new FrmMain();
	           UserManager=new UserManagerBridge(new DefaultUserManager());
	           IdeManager=new IDEManagerBridge(new DefaultIDEManager());
	           ContentManager = new ContentManagerBridge(new DefaultContentManager());
	           LibManager=new LibraryManagerBridge(new DefaultLibraryManager());
	           MapManager = new LearnmapManagerBridge(new DefaultLearnmapManager());
	           NoticeManager = new NoticeManagerBridge(new DefaultNoticeManager());
	           TestResultManager = new TestResultManagerBridge(new DefaultTestResultManager());
	           ProfileHandler = new ProfileHandler();
	           LanguageHandler = new LanguageHandler();
	           UserAvatars = new PicturePoolHandler();
	           DongleHandler = new TCDongleHandler();
               UserProgressInfoMgr = new UserProgressInfoManager();
               ClassManager = new ClassroomManagerBridge(new DefaultClassroomManager());
            }
            catch (System.Exception ex)
            {
               if (m_streamWriter != null)
                   m_streamWriter.WriteLine(ex.Message);
            }
        }

        public void Initialize()
        {
            //DummyProc(@"D:\Tintenherz.pptx");
            //bool bRes=FileAndDirectoryHelpers.IsValidFileName("Was ist das Periodensystem! - Aufbau und System.mpg",true);
            //bRes = FileAndDirectoryHelpers.IsValidFileName("Aufbau und System.mpg", true);
            //Console.WriteLine(bRes.ToString());

            //if (args.Length > 0)
            //    MessageBox.Show(String.Format("Arguments: {0}",args[0]) );

            //StartAutoUpdate();

            InitBridges();

			// Path to exe
            ExeFolder = System.Windows.Forms.Application.ExecutablePath.Substring(0, System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\'));

			// Path to ini file
			ProfileHandler.SetFileName(ExeFolder+@"\TrainConcept.ini");

            m_privateDataFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\SoftObject\WebTrain";
            if (!Directory.Exists(m_privateDataFolder))
                Directory.CreateDirectory(m_privateDataFolder);

            // set working folder
            WorkingFolder = ProfileHandler.GetProfileString("SYSTEM", "WorkingFolder", m_privateDataFolder);

            WebtrainServer = ProfileHandler.GetProfileString("SYSTEM", "WebtrainServer", "http://webtrain.softobject.at/");

			// read language and language-folder
			m_language=ProfileHandler.GetProfileString("SYSTEM","Language","de");
			LanguageFolder=ProfileHandler.GetProfileString("SYSTEM","LanguageFolder",ExeFolder);

            string strClientType = ProfileHandler.GetProfileString("SYSTEM", "ClientType", "Socket");
            string strServerType = ProfileHandler.GetProfileString("SYSTEM", "ServerType", "Socket");
            CtsServerManager = new CTSServerManagerBridge(new CTSSocketServerManager());
            CtsVPNServerManager = new CTSServerManagerBridge(new CTSSocketServerManager());
            CtsClientManager = new CTSClientManagerBridge(new CTSSocketClientManager());

            CTSServerConsole = new FrmCTSServerConsole();
            CTSVPNServerConsole = new FrmCTSServerConsole();
            CtsClientManager.SetAdapter(new CTSClientAdapterImpl());
            CtsServerManager.SetAdapter(new CTSServerAdapterImpl(CTSServerConsole));
            CtsVPNServerManager.SetAdapter(new CTSServerAdapterImpl(CTSVPNServerConsole));

            MapManager.SetAdapter(new LearnmapAdapterImpl());
            NoticeManager.SetAdapter(new NoticeAdapterImpl());
            UserManager.SetAdapter(new UserAdapterImpl());
            ClassManager.SetAdapter(new ClassroomAdapterImpl());

            DongleHandler.Init();

            UseType = ProfileHandler.GetProfileInt("SYSTEM", "UseType", 1);

            IsServer = UseType == 2;
            IsClient = UseType == 1;
            IsSingle = UseType == 0;

            IsEditContent = ProfileHandler.GetProfileInt("SYSTEM", "EditContent", 0) == 1 || IsServer || IsSingle;
            m_contentEditMediaFolder = ProfileHandler.GetProfileString("SYSTEM", "ContentEditMediaFolder", WorkingFolder);

            m_imgCertificationHeader = ProfileHandler.GetProfileString("CERTIFICATION", "HeaderImage","");
            LicenseId = ProfileHandler.GetProfileString("SYSTEM", "LicenseId", "-1");

            ConfigAdminInfo = ProfileHandler.GetProfileString("SYSTEM", "AdminInfo", "");

            // check RunOnce entry in registry
            CheckRunOnStart();

            // check another instance running
			if (IsServer && OtherInstanceRunning())
			{
				//yes -> shut down Launcher and we're done
                CloseLauncher();
				return;
			}

            // check valid License
            if (!IsClient && !CheckLicense())
            {
                //yes -> shut down Launcher and we're done
                CloseLauncher();
                return;
            }

            // set language
			if (!LanguageHandler.SetLanguage(LanguageFolder,"language",m_language))
			{
				// not found -> shot down Launcher and we're done
                CloseLauncher();
                MessageBox.Show("Missing Language File!","WebTrain",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}

			// set culture to appr. language
			string sName=Thread.CurrentThread.CurrentCulture.Name;
			switch(m_language)
			{
				case "de":		if (sName.IndexOf("de")!=0)
									Thread.CurrentThread.CurrentCulture = new CultureInfo("de-AT");break;
				case "et":		if (sName.IndexOf("et")!=0)
									Thread.CurrentThread.CurrentCulture = new CultureInfo("et-EE");break;
				case "en_GB":	if (sName.IndexOf("en")!=0)
									Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");break;
				case "es_ES":	if (sName.IndexOf("es")!=0)
									Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");break;
				case "fr_FR":	if (sName.IndexOf("fr")!=0)
									Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");break;
				case "it_IT":	if (sName.IndexOf("it")!=0)
									Thread.CurrentThread.CurrentCulture = new CultureInfo("it-IT");break;
				case "zh_CN":	if (sName.IndexOf("zh")!=0)
								{
								   Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
								   Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
								}
								break;

			}
			
			KeepAliveTimer=ProfileHandler.GetProfileInt("SYSTEM","KeepAliveTimer",30000);

			CtsConsoleOn=(ProfileHandler.GetProfileInt("SYSTEM","CtsConsoleOn",(IsServer ? 1:0))==1);

			TimeOutStandard=ProfileHandler.GetProfileInt("SYSTEM","TimeOutStandard",30000);
			TimeOutTransfer=ProfileHandler.GetProfileInt("SYSTEM","TimeOutTransfer",180000);

			LoginType=ProfileHandler.GetProfileInt("SYSTEM","LoginType",0);
			m_serverName=ProfileHandler.GetProfileString("SYSTEM","ServerName","");

            if (IsClient)
            {
                if (m_serverName.Length > 0)
                {
                    m_serverName = m_serverName.ToLower();
                    int pos = m_serverName.IndexOf("http://");
                    if (pos >= 0)
                        m_serverName = m_serverName.Substring(pos + 7);
                }
                else
                    m_serverName = AppCommunication.AutoServerName;
            }

            VirtualDirName = ProfileHandler.GetProfileString("SYSTEM", "IISVirtualDirectory", "TrainConceptServer");
            PortNr = ProfileHandler.GetProfileInt("SYSTEM", "PortNr", 1000);

            // open UserList
			string userListName=ProfileHandler.GetProfileString("SYSTEM","UserListName","users.xml");
			UserFileName = WorkingFolder+'\\'+userListName;
			if (System.IO.File.Exists(UserFileName))
				UserManager.Open(UserFileName);
            else
            {
                if (!Directory.Exists(WorkingFolder))
                    Directory.CreateDirectory(WorkingFolder);
            }

            // open NoticeManager
            NoticeManager.Open(WorkingFolder + "\\Notices.xml", WorkingFolder);

			ContentFolder=ProfileHandler.GetProfileString("SYSTEM","ContentFolder","");
			if (ContentFolder.Length==0)
			{
				if (IsClient && m_serverName.Length>0)
				{
					if (VirtualDirName.IndexOf(':')>=0)
						ContentFolder = "http://"+m_serverName+VirtualDirName+"/Content/"+m_language;
					else
						ContentFolder = "http://"+m_serverName+"/"+VirtualDirName+"/Content/"+m_language;
				}
				else
					ContentFolder = WorkingFolder+@"\Contents\Content\"+m_language;
			}
			else
				ContentFolder = ContentFolder+@"\"+m_language;

			LibsFolder=ProfileHandler.GetProfileString("SYSTEM","LibrariesFolder","");
			if (LibsFolder.Length==0)
                LibsFolder=WorkingFolder+@"\Contents\Libraries\"+m_language;
			else
				LibsFolder=LibsFolder+@"\"+m_language;

			MapsFolder=ProfileHandler.GetProfileString("SYSTEM","LearnmapsFolder","");
			if (MapsFolder.Length==0)
				MapsFolder=WorkingFolder+@"\Learnmaps\"+m_language;
			else
				MapsFolder=MapsFolder+@"\"+m_language;

            ClassesFolder = ProfileHandler.GetProfileString("SYSTEM", "ClassesFolder", "");
            if (ClassesFolder.Length == 0)
                ClassesFolder = WorkingFolder + @"\Classes\" + m_language;
            else
                ClassesFolder = ClassesFolder + @"\" + m_language;

            ImportExportFolder = ProfileHandler.GetProfileString("SYSTEM", "ImportExportFolder", "");
            if (ImportExportFolder.Length == 0)
                ImportExportFolder = WorkingFolder + "\\ImportExport";

            ReportsFolder = ProfileHandler.GetProfileString("SYSTEM", "ReportsFolder", "");
            if (ReportsFolder.Length == 0)
                ReportsFolder = ImportExportFolder;

            HelpFolder=ProfileHandler.GetProfileString("SYSTEM","HelpFolder",WorkingFolder);

            m_loginName = ProfileHandler.GetProfileString("SYSTEM", "LoginName", "");
            m_loginPwd = "";
            string strEncodedPwd = ProfileHandler.GetProfileString("SYSTEM", "LoginPassword", "");
            if (strEncodedPwd.Length > 0)
            {
                string strDecodedPwd = "";
                DecodePassword(strEncodedPwd, ref strDecodedPwd);
                m_loginPwd = strDecodedPwd;
            }

#if(SKIN_EMCO)
			m_resourceName="SoftObject.TrainConcept.res.trainconcept.resources_emco";
#elif(SKIN_WEBTRAIN)
			ResourceName="SoftObject.TrainConcept.res.webtrain.resources_softobject";
#else
			m_resourceName="SoftObject.TrainConcept.res.trainconcept.resources_emco";
#endif
            TestResultManager.Open(WorkingFolder + "\\tests.dat");
            UserProgressInfoMgr.Open(WorkingFolder + "\\userprogressinfo.db");

            UserAvatars.Initialize(WorkingFolder + "\\avatars");

           
            //string text="";
			//EncodeDate(new DateTime(2003,11,1),ref text);

            //m_userProgressInfoMgr.AddUserProgressInfo("schueler1", "map1", "working1", 0, 0);

			if(!MainForm.Initialize())
			    return;

            bool bEditTemplatesFound=InitializeEditorTemplates();
            if (!bEditTemplatesFound && (IsServer && IsEditContent))
                IsEditContent = false;

            CloseLauncher();

            if (IsServer && VirtualDirName.Length>0)
            {
                if (!CheckVirtualDirectory1() && !CheckVirtualDirectory2())
                {
                    string sErr = String.Format("Couldn't open Virtual Directory: ''{0}''.\nIs IIS running?", "Default Web Site" + '/' + VirtualDirName);
                    MessageBox.Show(sErr,"WebTrain",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            if (IsSingle)
            {
                Directory.CreateDirectory(WorkingFolder + @"\Contents\Content\" + m_language);
                Directory.CreateDirectory(WorkingFolder + @"\Contents\Libraries\" + m_language);
                Directory.CreateDirectory(WorkingFolder + @"\Learnmaps\" + m_language);
                Directory.CreateDirectory(WorkingFolder + @"\Classes\" + m_language);
                Directory.CreateDirectory(WorkingFolder + @"\ImportExport");
            }


            //TestZipfileSave();

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();

            System.Windows.Forms.Application.Run(MainForm);
		}


        private  bool InitializeEditorTemplates()
        {
            var profHnd = new ProfileHandler(ExeFolder + @"\templates.cfg");
            var aKeys = profHnd.GetAllKeys("TEMPLATES");
            if (aKeys.Count > 0)
            {
                var rh = new ResourceHandler("SoftObject.Trainconcept.res.webtrain.resources_EditorTemplates", MainForm.GetType().Assembly);
                int i = 0;
                foreach (var e in aKeys)
                {
                    Templates.Add(i, e.Key);
                    PageActionTextItems.Add(i++, e.Value);
                }
                return true;
            }
            return false;
        }

		public  bool IsMultiLicence()
		{
			try
			{
				RegistryKey key=GetRegistrySoftwareKey(@"Microsoft\Windows");
			    if (key!=null)
			    {
				    object val= key.GetValue("OBIVersion");
				    if (val!=null)
				    {
					    // Mehrplatzversion?
					    if (((string)val)=="080675")
						    return true;
				    }
			    }
			}
			catch (System.Exception)
			{

			}
			return false;
		}

		public bool IsDongleFree()
		{
			try
			{
                RegistryKey key = GetRegistrySoftwareKey(@"SoftObject\TrainConcept");
			    if (key!=null)
			    {
				    object val= key.GetValue("FreeDongle");
				    if (val!=null)
				    {
					    // Dongle free code OK?
					    if (((string)val)=="04041963")
						    return true;
				    }
			    }
			}
			catch (System.Exception)
			{
				
			}

			return false;
		}

		public bool GetTimeLimit(ref DateTime dt)
		{
			try
			{
                RegistryKey key = GetRegistryKey(@"Software\Microsoft\Windows\STLInfo");
		        if (key!=null)
		        {
			        object val= key.GetValue("STLVersion");
			        if (val!=null)
			        {
				        string sDat=(string)val;
				        if (!DecodeDate(sDat,ref dt))
					        return false;
				        return true;
			        }
		        }
			}
			catch (System.Exception)
			{
				
			}
			return false;
		}
		
		public bool SetTimeLimit(DateTime dt)
		{
			try
			{
                RegistryKey key = GetRegistryKey(@"Software\Microsoft\Windows\STLInfo");
                if (key != null)
                {
                    object val = key.GetValue("STLVersion");
                    if (val != null)
                    {
                        string test = "";
                        EncodeDate(dt, ref test);
                        key.SetValue("STLVersion", test);
                        return true;
                    }
                }
			}
			catch (System.Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
			return false;
		}

		public bool GetTimeLimitId(ref int id)
		{
			try
			{
                RegistryKey key = GetRegistryKey(@"Software\Microsoft\Windows\STLInfo");
                if (key != null)
			    {
				    object val= key.GetValue("STLVersionId");
				    if (val!=null)
				    {
					    string sId=(string)val;
					    id = Int32.Parse(sId);
					    return true;
				    }
			    }
			}
			catch (System.Exception)
			{
				
			}
			return false;
		}

		public bool IsTimeLimited()
		{
			int id=0;
			return GetTimeLimitId(ref id);
		}

		public bool IsTimeLimitationExceeded(int licenseID,int days)
		{
			DateTime start=new DateTime();
			int	id=0;

			if (!GetTimeLimitId(ref id) || id!=licenseID)
				return false;

			if (!GetTimeLimit(ref start))
				return false;

			TimeSpan ts=new TimeSpan(days+1,0,0,0);
			DateTime now=DateTime.Now;

			if (now>(start+ts))
				return true;

			return false;
		}

        public int GetLicenceRestDays()
        {
            if (!IsTimeLimited())
                return -1;

            int licTypeVal = 0;
            DongleHandler.GetLicenceTypeValue(ref licTypeVal);

            DateTime start = DateTime.Now;
            GetTimeLimit(ref start);

            TimeSpan ts = new TimeSpan(licTypeVal, 0, 0, 0);
            DateTime end = start + ts;
            TimeSpan diff = end - DateTime.Now;

            return diff.Days;
        }


		public static bool DecodeDate(string str,ref DateTime date)
		{
			string [] aTokens=str.Split(new char[] {' '},8);

			if (aTokens.Length!=8)
				return false;

			char [] aChars = new Char[8];
			int  i;
			for(i=0;i<aTokens.Length;++i)
			{
				byte b=Convert.ToByte(aTokens[i]);
				b ^= (byte)(((i*13)^0x86)%255);
				aChars[i]=Convert.ToChar(b);
			}
			string sDate=new string(aChars);
			date=new DateTime(Int32.Parse(sDate.Substring(4,4)),Int32.Parse(sDate.Substring(2,2)),Int32.Parse(sDate.Substring(0,2)));
			return true;
		}

		public static void EncodeDate(DateTime date,ref string str)
		{
			string l_str=date.ToString("ddMMyyyy");
			for(int i=0;i<l_str.Length;++i)
			{
				byte b=Convert.ToByte(Convert.ToByte(l_str[i]) ^ ((i*13)^0x86)%255);
				str+=String.Format("{0} ",b);
			}
		}

		public bool OtherInstanceRunning()
		{
			try
			{
				string mutexName = System.Windows.Forms.Application.ProductName + "_MultistartPrevent";
				m_mutex = new Mutex(false,mutexName);
				if (m_mutex.WaitOne(0,true))
					return false;
			}
			catch (System.Exception /*ex*/)
			{
                return true;
			}
			return true;
		}

		private bool CheckRunOnStart()
		{
			try
			{
                RegistryKey key = GetRegistrySoftwareKey(@"SoftObject\TrainConcept");
			    if (key!=null)
			    {
                    CloseLauncher();
				    object val= key.GetValue("RunOnce");
				    if (val!=null)
				    {
                        //MessageBox.Show("RunOnce found!");
                        string strCmd = (string)val;
                        try
                        {
                            strCmd.Trim();
                            if (strCmd.Length > 0)
                            {
                                STARTUPINFO si = new STARTUPINFO();
                                PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                                if (!CreateProcess(null, strCmd, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi))
                                    return false;
                                Thread.Sleep(5000);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            return false;
                        }

                        key.DeleteValue("RunOnce");
				    }

                    val = key.GetValue("RunAlways");
                    if (val != null)
                    {
                        string strCmd = (string)val;
                        try
                        {
	                        strCmd.Trim();
	                        if (strCmd.Length > 0)
	                        {
	                            STARTUPINFO si = new STARTUPINFO();
	                            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
	                            if (!CreateProcess(null, strCmd, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi))
	                                return false;
	                            Thread.Sleep(5000);
	                        }
                        }
                        catch (System.Exception ex)
                        {
                            return false;
                        }
                    }

                    val = key.GetValue("RunAlwaysClientOnly");
                    if (val != null && IsClient)
                    {
                        string strCmd = (string)val;
                        try
                        {
                            strCmd.Trim();
                            if (strCmd.Length > 0)
                            {
                                STARTUPINFO si = new STARTUPINFO();
                                PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                                if (!CreateProcess(null, strCmd, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi))
                                    return false;
                                Thread.Sleep(5000);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            return false;
                        }
                    }

			    }
			}
			catch (System.Exception e)
			{
                CloseLauncher();
                MessageBox.Show(e.ToString(), "WebTrain", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return false;
		}

		public static int GetOSArchitecture()
		{
            return (Environment.Is64BitOperatingSystem ? 64 : 32);
		}

		public static string getOSInfo()
		{
			//Get Operating system information.
			OperatingSystem os = Environment.OSVersion;
			//Get version information about the os.
			Version vs = os.Version;

			//Variable to hold our return value
			string operatingSystem = "";

			if (os.Platform == PlatformID.Win32Windows)
			{
				//This is a pre-NT version of Windows
				switch (vs.Minor)
				{
					case 0:
						operatingSystem = "95";
						break;
					case 10:
						if (vs.Revision.ToString() == "2222A")
							operatingSystem = "98SE";
						else
							operatingSystem = "98";
						break;
					case 90:
						operatingSystem = "Me";
						break;
					default:
						break;
				}
			}
			else if (os.Platform == PlatformID.Win32NT)
			{
				switch (vs.Major)
				{
					case 3:
						operatingSystem = "NT 3.51";
						break;
					case 4:
						operatingSystem = "NT 4.0";
						break;
					case 5:
						if (vs.Minor == 0)
							operatingSystem = "2000";
						else
							operatingSystem = "XP";
						break;
					case 6:
						if (vs.Minor == 0)
							operatingSystem = "Vista";
						else
							operatingSystem = "7";
						break;
					default:
						break;
				}
			}
			//Make sure we actually got something in our OS check
			//We don't want to just return " Service Pack 2" or " 32-bit"
			//That information is useless without the OS version.
			if (operatingSystem != "")
			{
				//Got something.  Let's prepend "Windows" and get more info.
				operatingSystem = "Windows " + operatingSystem;
//				//See if there's a service pack installed.
//				if (os.ServicePack != "")
//				{
//					//Append it to the OS name.  i.e. "Windows XP Service Pack 3"
//					operatingSystem += " " + os.ServicePack;
//				}
//				//Append the OS architecture.  i.e. "Windows XP Service Pack 3 32-bit"
				operatingSystem += " " + GetOSArchitecture().ToString() + "-bit";
			}
			//Return the information we've gathered.
			return operatingSystem;
		}

        public static RegistryKey GetRegistrySoftwareKey(string strKey)
        {
            RegistryKey key = null;
            if (AppHandler.GetOSArchitecture() == 64)
            {
                RegistryKey localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                key = localMachine64.OpenSubKey(@"Software\Wow6432Node\" + strKey, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            }
            else
                key = Registry.LocalMachine.OpenSubKey(@"Software\"+strKey, true);
            return key;
        }

        public static RegistryKey GetRegistryKey(string strKey)
        {
            RegistryKey key = null;
            if (AppHandler.GetOSArchitecture() == 64)
            {
                RegistryKey localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                key = localMachine64.OpenSubKey(strKey,RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            }
            else
                key = Registry.LocalMachine.OpenSubKey(strKey, true);
            return key;
        }


        private bool CheckVirtualDirectory1()
        {
            ServerManager srvManager = new ServerManager();
            if (srvManager != null)
            {
                String strVirDirName = "/" + VirtualDirName;
                Microsoft.Web.Administration.Application defApp = null;
                try
                {
	                Site defaultSite = srvManager.Sites["Default Web Site"];
	                defApp = defaultSite.Applications[0];
                }
                catch (System.Exception /*ex*/)
                {
                    //string sErr = String.Format("Couldn't open Virtual Directory: ''{0}''.\nIs IIS running?", "Default Web Site"+strVirDirName, ex.ToString());
                    //MessageBox.Show(sErr,"WebTrain",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }

                                
                if (defApp != null)
                {
                    VirtualDirectoryCollection dVirDirs = defApp.VirtualDirectories;
                    bool bFound = false;
                    foreach (var vd in dVirDirs)
                        if (vd.Path == strVirDirName)
                        {
                            string strPhysPath = m_privateDataFolder;
                            int iFound = ContentFolder.IndexOf(@"\Contents");
                            if (iFound >= 0)
                                strPhysPath = ContentFolder.Substring(0, iFound);

                            strPhysPath +=  @"\Contents";
                            if (String.Compare(vd.PhysicalPath, strPhysPath, true) != 0)
                            {
                                vd.PhysicalPath = strPhysPath;
                                srvManager.CommitChanges();
                            }
                            bFound = true;
                            break;
                        }

                    if (!bFound)
                    {
                        try
                        {
                            VirtualDirectory vd = defApp.VirtualDirectories.Add(strVirDirName, ExeFolder);
                            string sText = vd.ToString();
                            srvManager.CommitChanges();
                        }
                        catch (System.Exception /*ex*/)
                        {
                            //Console.WriteLine("Couldn't create VirtualDirectory(Name: {0}) because {1}", strVirDirName, ex.ToString());
                            return false;
                        }
                        return true;
                    }
                    //ShowVirtualDirs();
                    return true;
                }
            }
            return false;
        }

        /*
        private static void ShowVirtualDirs()
        {
            ServerManager srvManager = new ServerManager();
            if (srvManager != null)
            {
                Site defaultSite = srvManager.Sites["Default Web Site"];
                foreach (Microsoft.Web.Administration.Application app in defaultSite.Applications)
                {
                    Console.WriteLine("Found application with the following path: {0}", app.Path);
                    Console.WriteLine("Virtual Directories:");
                    if (app.VirtualDirectories.Count > 0)
                    {
                        foreach (Microsoft.Web.Administration.VirtualDirectory vdir in app.VirtualDirectories)
                        {
                            Console.WriteLine(
                                "  Virtual Directory: {0}", vdir.Path);
                            Console.WriteLine(
                                "   |-PhysicalPath = {0}", vdir.PhysicalPath);
                            Console.WriteLine(
                                "   |-LogonMethod  = {0}", vdir.LogonMethod);
                            Console.WriteLine(
                                "   +-UserName     = {0}\r\n", vdir.UserName);
                        }
                    }
                }
            }
        }*/

        private void CloseLauncher()
        {
            IntPtr hLauncher = WindowsAPI.FindWindow("LauncherWndClass", "LauncherWnd");
            if (hLauncher != null)
                WindowsAPI.SendMessage(hLauncher, (int)Msg.WM_CLOSE, 0, 0);
        }

        private bool CheckVirtualDirectory2()
        {
            try
            {
	            string[] websites = IISWebsite.ExistedWebsites;
	            if (websites.Length > 0)
	            {
	                IISWebsite webSite = IISWebsite.OpenWebsite(websites[0]);
	                IISWebVirturalDir virtDir = webSite.Root;
	                if (!virtDir.ExistVirtualDir(VirtualDirName))
	                    virtDir.CreateSubVirtualDir(VirtualDirName, @"C:\Program Files (x86)\TrainConcept");
	            }
            }
            catch (System.Exception /*ex*/)
            {
                return false;
            }
            return true;
        }

        private static void EncodePassword(string strPassword, ref string strCoded)
        {
            strCoded = "";
            for (int i = 0; i < strPassword.Length; ++i)
            {
                byte b = Convert.ToByte(Convert.ToByte(strPassword[i]) ^ ((i * 13) ^ 0x86) % 255);
                strCoded += String.Format("{0} ", b);
            }
        }

        private static bool DecodePassword(string strCoded, ref string strPassword)
        {
            string[] aTokens = strCoded.Split(new char[] { ' ' });

            char[] aChars = new Char[aTokens.Length];
            int i;
            for (i = 0; i < aTokens.Length; ++i)
            {
                byte b = Convert.ToByte(aTokens[i]);
                b ^= (byte)(((i * 13) ^ 0x86) % 255);
                aChars[i] = Convert.ToChar(b);
            }
            strPassword = new string(aChars);
            return true;
        }

        private bool CheckLicense()
        {
            // check if license file exists
            var myFiles = Directory.GetFiles(ExeFolder, "*.*", System.IO.SearchOption.AllDirectories).Where(s => s.EndsWith(".lic", StringComparison.OrdinalIgnoreCase));
            String[] strFiles = myFiles.ToArray();
            if (strFiles.Length == 0)
            {
                CloseLauncher();
                string txt = LanguageHandler.GetText("ERROR", "Missing license file!");
                string cap = LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // check type of license file
            bool bIsValid = true;
            short grpId = 0, licType = 0;
            int licTypeVal = 0;
            int iDongleId = 0;
            if (DongleHandler.ReadLicenceFile(strFiles[0], ref iDongleId) && iDongleId >= 1000)
            {
                DongleId = iDongleId;
                DongleHandler.GetLicence(ref grpId, ref licType);

                //#define	TCL_GRP_SINGLE						0x0
                //#define	TCL_GRP_MULTI						0x1
                //#define	TCL_GRP_NETWARE						0x2
                if ((grpId == 0 || grpId == 1) && UseType != 0)
                {
                    bIsValid = false;
                }
                else if (grpId == 2 && UseType != 2)
                {
                    bIsValid = false;
                }
            }
            else
                bIsValid = false;

            if (!bIsValid)
            {
                CloseLauncher();
                string txt = LanguageHandler.GetText("ERROR", "invalid License file!");
                string cap = LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Zeitbeschränkte Version?
            DateTime dt = new DateTime();
            int iLicenseId = 0;
            if (GetTimeLimit(ref dt) && GetTimeLimitId(ref iLicenseId))
            {

                //licType:	
                //TCL_TYPE_UNLIMITED 		 			0x0
                //TCL_TYPE_USERSINSTALLEDCOUNTER		0x1
                //TCL_TYPE_STARTCOUNTER		 		    0x2
                //TCL_TYPE_TIMERCOUNTER		 		    0x4
                //TCL_TYPE_USERSONLINECOUNTER  		    0x8
                //TCL_TYPE_RESTARTTIMERCOUNTER  		0x10
                if (licType == 0)
                {
                    RegistryKey key = GetRegistryKey(@"Software\Microsoft\Windows");
                    if (key != null)
                        key.DeleteSubKey("STLInfo");
                }
                else if (licType != 4 && licType != 16) // Keine Zeitlimitierung in der Lizenz 
                {
                    bIsValid = false;
                }

                // unlimitiert bzw. Limit abgelaufen?
                if (IsTimeLimitationExceeded(iLicenseId, (DateTime.Now - dt).Days))
                {
                    if (licType==16)
                    {
                        CloseLauncher();
                        DongleHandler.GetLicenceTypeValue(ref licTypeVal);
                        SetTimeLimit(DateTime.Now);
                        string txt = String.Format(LanguageHandler.GetText("MESSAGE", "new time limit set to {0} days"), licTypeVal);
                        string cap = LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        CloseLauncher();
                        string txt = LanguageHandler.GetText("ERROR", "License time limit exceeded on {0}!");
                        string cap = LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(String.Format(txt,String.Format("Timelimit: {0}", dt.ToString())), cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else
            {
                if (licType == 4) // zeitlimitierung! Gültig nur mit Neuinstallation
                {
                    CloseLauncher();
                    string txt = LanguageHandler.GetText("ERROR", "invalid License file!");
                    string cap = LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (licType == 8)
                {
                    m_iMaxOnlineUsers = licTypeVal;
                }
            }

            LicenseId = DongleId.ToString();
            
            // Stand-Alone-Version?
            if (!IsClient && !IsServer)
            {
                // Mehrfachlizenz?
                if (IsMultiLicence())
                {
                    RegistryKey key = GetRegistrySoftwareKey(@"SoftObject\TrainConcept");
                    if (key == null)
                    {
                        CloseLauncher();
                        string txt = LanguageHandler.GetText("ERROR", "Invalid_Installation");
                        string cap = LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string counter = (string)key.GetValue("InstallationCounter");
                    // Anzahl am Dongle verringern!
                    if (counter != null)
                    {
                        int cnt = Int32.Parse(counter);
                        DongleHandler.SetLicenceTypeValue(cnt - 1);

                        // Key entgültig löschen -> Wird nur das 1.mal durchgeführt
                        key.DeleteValue("InstallationCounter");
                    }
                }
                else
                {
                }
            }
            else if (IsServer)
            {
            }
            return true;
        }

   
        public void EnsureBrowserEmulationEnabled(string exename = "trainconcept", bool uninstall = false)
        {
            try
            {
                if (Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true)==null)
                {
                    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl",RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true)==null)
                        Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree);
                }

                using (
                    var rk = Registry.CurrentUser.OpenSubKey(
                            @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true)
                )
                {
                    if (!uninstall)
                    {
                        var value = rk.GetValue(exename + ".exe");
                        if (value == null)
                        {
                            rk.SetValue(exename+".exe", (uint)11001, RegistryValueKind.DWord);
                            rk.SetValue(exename+".vshost.exe", (uint)11001, RegistryValueKind.DWord);
                        }
                    }
                    else
                        rk.DeleteValue(exename);
                }
            }
            catch
            {
                MessageBox.Show("Error creating necessary Registry entries\nMissing User rights?");
            }
        }


        public string GetDocumentsFolder(string libName)
        {
            if (IsClient || IsSingle)
            {
                string strDocsDir = WorkingFolder + "\\docs\\";
                if (!Directory.Exists(strDocsDir))
                    Directory.CreateDirectory(strDocsDir);
                string strSubDir = strDocsDir + "\\" + libName;
                if (!Directory.Exists(strSubDir))
                    Directory.CreateDirectory(strSubDir);
                return strSubDir;
            }
            return ContentFolder + @"\" + LibManager.GetFileName(libName).ToLower() + @"\docs";
        }
       
        /*
        private static void DummyProc(string fileName)
        {
            string data = "#TRANSFERSTART";
            byte[] aTypeAndSize = new byte[5];
            aTypeAndSize[0] = 0;

            byte[] aLength = BitConverter.GetBytes(data.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(aLength);
            Array.Copy(aLength, 0, aTypeAndSize, 1, aLength.Length);

            int iTest = BitConverter.ToInt32(aTypeAndSize, 1);
            Console.WriteLine(iTest.ToString());

            iTest = BitConverter.ToInt32(aLength, 0);
            Console.WriteLine(iTest.ToString());

            string strFileOut = @"D:\Tintenherz_copy.pptx";

            if (File.Exists(fileName))
            {
                Stream inStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Stream outStream = File.Create(strFileOut);
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);

                int iRead = 0;
                while (inStream.Position < inStream.Length)
                {
                    Byte[] buf = new Byte[1024];
                    int cnt = inStream.Read(buf, 0, 1024);
                    if (cnt > 0)
                    {
                        outStream.Write(buf, 0, cnt);
                        iRead += cnt;
                        Console.WriteLine(String.Format("{0},{1}", cnt, iRead));
                    }
                }
            }
        }*/
    }
}


;