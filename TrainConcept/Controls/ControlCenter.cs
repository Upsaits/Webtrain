using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraRichEdit.Commands;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for ControlCenter.
	/// </summary>
	public partial class ControlCenter : UserControl
	{
		public  ExplorerBar m_sideBar;
		private ExplorerBarGroupItem m_workoutPanel=null;
		private ExplorerBarGroupItem m_editPanel=null;
		private ExplorerBarGroupItem m_systemPanel=null;
		private ExplorerBarGroupItem m_ctsPanel=null;
		private ExplorerBarGroupItem m_chatPanel=null;
		private ExplorerBarGroupItem m_classPanel=null;

		//private Hashtable m_dActButtons=new Hashtable();
        private Dictionary<string,ButtonItem> m_dActButtons= new Dictionary<string,ButtonItem>();
		private ResourceHandler rh=null;
        private string m_activePanel=null;
        private AppHandler AppHandler = Program.AppHandler;
        public ControlCenter()
		{
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			InitializeSideBar();

            AppHandler.MapManager.LearnmapManagerEvent += ControlCenter_LearnmapEvent;
            AppHandler.ClassManager.ClassroomManagerEvent += ControlCenter_ClassroomEvent;
            AppHandler.UserManager.UserManagerEvent += UserManager_UserManagerEvent;
		}


		private void InitializeSideBar()
		{
			m_sideBar = (ExplorerBar) AppHandler.IdeManager.CreateExplorerBar("sbControlCenter",imageList1,imageList1);
			m_sideBar.TabIndex = 1;
			m_sideBar.TabStop = false;
			
			Controls.Add(m_sideBar);
			Name = "ControlCenter";

            m_sideBar.ExpandedChange += SideBar_ExpandedChange;
            m_sideBar.SizeChanged += m_sideBar_SizeChanged;
            m_sideBar.BackStyle.BackColor = System.Drawing.SystemColors.Control;
        }

        public void SetConnectionState(bool bIsOn)
        {
            if (bIsOn)
                m_sideBar.BackStyle.BackColor = System.Drawing.SystemColors.HotTrack;
            else
                m_sideBar.BackStyle.BackColor = System.Drawing.SystemColors.Control;
            m_sideBar.RecalcLayout();
        }

		public void SetupSideBar(bool isAdmin, bool isServer, bool isTeacher)
		{
            imageList1.Images.Add(rh.GetBitmap("learnmap"));            //0
            imageList1.Images.Add(rh.GetBitmap("Student"));             //1
            imageList1.Images.Add(rh.GetBitmap("learnmapeditor"));      //2
            imageList1.Images.Add(rh.GetBitmap("mapdistribute"));       //3
            imageList1.Images.Add(rh.GetBitmap("useradministration"));  //4
            imageList1.Images.Add(rh.GetBitmap("Chatroom"));            //5
            imageList1.Images.Add(rh.GetBitmap("dongle"));              //6
            imageList1.Images.Add(rh.GetBitmap("Updates"));             //7
            imageList1.Images.Add(rh.GetBitmap("Upgrates"));            //8
            imageList1.Images.Add(rh.GetBitmap("language"));            //9
            imageList1.Images.Add(rh.GetBitmap("lib"));                 //10
            imageList1.Images.Add(rh.GetBitmap("learnmapeditor"));      //11
            imageList1.Images.Add(rh.GetBitmap("classroom"));           //12
            imageList1.Images.Add(rh.GetBitmap("Assistant"));           //13
            imageList1.Images.Add(rh.GetBitmap("learnmap_done"));       //14
            imageList1.Images.Add(rh.GetBitmap("learnmap_undone"));     //15
            imageList1.Images.Add(rh.GetBitmap("settings"));            //16
            imageList1.Images.Add(rh.GetBitmap("learnmapserver"));      //17

            m_sideBar.Groups.Clear();

			string woLearnmapPanelTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","WorkoutLearnmapPanelTitle","Lernmappen");
			string woLearnmapPanelDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","WorkoutLearnmapPanelDescription","Durcharbeiten der Lernmappen");
			string edLearnmapPanelTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","EditLearnmapPanelTitle","Lernmappeneditor");
			string edLearnmapPanelDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","EditLearnmapPanelDescription","Bearbeiten der Lernmappen");
			string sysPanelTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","SystemPanelTitle","System");
			string sysPanelDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","SystemPanelDescription","Systemeinstellungen");
			string ctsPanelTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","CTSPanelTitle","CTS");
			string ctsPanelDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","CTSPanelDescription","Schüler-Lehrer-Betrieb");
			string studentBtnTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","StudentBtnTitle","Schüler");
			string studentBtnDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","StudentBtnDescription","Aktuelle Schülerliste");
			string classroomPanelTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","ClassroomPanelTitle","Klassenräume");
			string classroomPanelDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","ClassroomPanelDescription","Klassenräume");
			string classroomBtnTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","ClassroomBtnTitle","Klasse");
			string classroomBtnDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","ClassroomBtnDescription","Klassenraum");
			string chatroomPanelTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","ChatroomPanelTitle","Chaträume");
			string chatroomPanelDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","ChatroomPanelDescription","Chaträume");
			string chatroomBtnTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","ChatroomBtnTitle","Chatraum");
			string chatroomBtnDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","ChatroomBtnDescription","Chatraum");
			string userManagerBtnTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","UserManagerBtnTitle","Benutzerverwaltung");
			string userManagerBtnDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","UserManagerBtnDescription","Benutzerliste verwalten");
			string lmDistributeBtnTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapDistributeBtnTitle","Lernmappenverteiler");
			string lmDistributeBtnDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapDistributeBtnDescription","Verteilen der Lernmappen");
			string lmDongleStateTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapDongleStateTitle","Lizenz Status");
			string lmDongleStateDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapDongleStateDescription","Lizenz Status anzeigen");
			string lmUpdatesTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapUpdatesTitle","Updates");
			string lmUpdatesDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapUpdatesDescription","Aktuelle Updates suchen");
			string lmUpgradesTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapUpgradesTitle","Webtrainmodule");
			string lmUpgradesDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapUpgradesDescription","Aktuelle Upgrades suchen");
			string lmDealerUpdateTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","DealerUpdateTitle","Händler-Updates");
			string lmDealerUpdateDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","DealerUpdateDescription","Händler-Updates");
			string lmLanguageTitle=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapLanguageTitle","Sprachauswahl");
			string lmLanguageDescr=AppHandler.LanguageHandler.GetText("SIDEBAR","LearnmapLanguageDescription","Sprachauswahl");
            string lmSetupAssistantTitle = AppHandler.LanguageHandler.GetText("SIDEBAR", "SetupAssistantTitle", "Einrichtungsassistent");
            string lmSetupAssistantDescr = AppHandler.LanguageHandler.GetText("SIDEBAR", "SetupAssistantDescription", "Assistent zur Einrichtung von TrainConcept");
			string sNewMap = AppHandler.LanguageHandler.GetText("FORMS","New_map","Neue Lernmappe");
            string sNewClass = AppHandler.LanguageHandler.GetText("FORMS", "New_class", "Neue Klasse");

            //-----Setup learnmap workout Panel
            m_workoutPanel = (ExplorerBarGroupItem) AppHandler.IdeManager.AddExplorerBarGroup(m_sideBar,"WorkoutLearnmapPanel",woLearnmapPanelTitle,woLearnmapPanelDescr,0);
            UpdateLearnmapWorkings();
            m_workoutPanel.Expanded = true;
            //------------------------------------------------------

            //-----Setup learnmap edit Panel
            if (isAdmin)
			{
				m_editPanel = (ExplorerBarGroupItem) AppHandler.IdeManager.AddExplorerBarGroup(m_sideBar,"EditLearnmapPanel",edLearnmapPanelTitle,edLearnmapPanelDescr,2);
				AppHandler.IdeManager.AddExplorerBarButton(m_editPanel,"NewLearnmapItem",sNewMap,sNewMap,-1);

                for (int i = 0; i < AppHandler.MapManager.GetMapCnt(); ++i)
                {
                    string title;
                    AppHandler.MapManager.GetTitle(i, out title);
                    if (m_editPanel != null)
                    {
                        var map = AppHandler.MapManager.GetItem(title);
                        AddEditLearnmap(title, map.isServerMap);
                    }
                       
                }
            }

            //---------Setup system Panel
            m_systemPanel = (ExplorerBarGroupItem)AppHandler.IdeManager.AddExplorerBarGroup(m_sideBar, "SystemPanel", sysPanelTitle, sysPanelDescr, 4);
            
            AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "SettingsItem", "Einstellungen", "Einstellungen bearbeiten", 16);

            if (AppHandler.IsEditContent)
				AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel,"EditContentItem","Inhalte bearbeiten","Inhalte bearbeiten",11);

            AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "UpdatesItem", lmUpdatesTitle, lmUpdatesDescr, 7);
            AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "LicenseStateItem", lmDongleStateTitle, lmDongleStateDescr, 6);

            if (isAdmin)
			{
                //AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "SelectLanguageItem", lmLanguageTitle, lmLanguageDescr, 9);
                AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "UpgradesItem", lmUpgradesTitle, lmUpgradesDescr, 8);

                // setup classes panel
                m_classPanel = (ExplorerBarGroupItem)AppHandler.IdeManager.AddExplorerBarGroup(m_sideBar, "ClassPanel", classroomPanelTitle, "", 12);
                if (isAdmin)
                    AppHandler.IdeManager.AddExplorerBarButton(m_classPanel, "NewClassItem", sNewClass, sNewClass, -1);
                string[] aClasses = null;
                if (AppHandler.ClassManager.GetClassNames(out aClasses)>0)
                    foreach(string classname in aClasses)
						AddClass(classname);

                // setup online panel
                if (isServer)
                {
                    m_ctsPanel = (ExplorerBarGroupItem)AppHandler.IdeManager.AddExplorerBarGroup(m_sideBar, "CTSPanel", ctsPanelTitle, ctsPanelDescr, 1);
                    AppHandler.IdeManager.AddExplorerBarButton(m_ctsPanel, "StudentList", studentBtnTitle, studentBtnDescr, 1);
                    AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "SetupSystemAssistantItem", lmSetupAssistantTitle, lmSetupAssistantDescr, 13);
                }

                AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "UserAdministrationItem", userManagerBtnTitle, userManagerBtnDescr, 4);
                AppHandler.IdeManager.AddExplorerBarButton(m_systemPanel, "LearnmapDistributionItem", lmDistributeBtnTitle, lmDistributeBtnDescr, 3);
			}

            // ------ setup chat panel
            m_chatPanel = (ExplorerBarGroupItem)AppHandler.IdeManager.AddExplorerBarGroup(m_sideBar, "ChatPanel", chatroomPanelTitle, chatroomPanelDescr, 5);
            for (int i = 0; i < 4; ++i)
            {
                string tit = String.Format("Chatroom{0}", i + 1);
                string txt1 = String.Format("{0} {1}", chatroomBtnTitle, i + 1);
                string txt2 = String.Format("{0} {1}", chatroomBtnDescr, i + 1);

                AppHandler.IdeManager.AddExplorerBarButton(m_chatPanel, tit, txt1, txt2, 5);
            }

		}

        void m_sideBar_SizeChanged(object sender, EventArgs e)
        {
            radialMenu1.Visible = false;
        }

		public bool HasWorkoutLearnmap(string title)
		{
			return AppHandler.IdeManager.HasExplorerBarButton(m_workoutPanel,title);
		}

		public void AddWorkoutLearnmap(string title)
        {
            int iIconId = 15;
            if (AppHelpers.IsTestAttended(AppHandler.MainForm.ActualUserName, title, Utilities.c_strDefaultFinalTest))
                iIconId = 14;
			AppHandler.IdeManager.AddExplorerBarButton(m_workoutPanel,title,title,title,iIconId);
			m_sideBar.RecalcLayout();
		}

        public void UpdateWorkoutLearnmapState(string title)
        {
            int iIconId = 15;
            if (AppHelpers.IsTestAttended(AppHandler.MainForm.ActualUserName, title, Utilities.c_strDefaultFinalTest))
                iIconId = 14;
            AppHandler.IdeManager.SetExplorerBarButtonImageId(m_workoutPanel, title, iIconId);
            m_sideBar.RecalcLayout();
            
        }


        public void AddEditLearnmap(string title, bool isServerMap = false)
		{
			object item=AppHandler.IdeManager.AddExplorerBarButton(m_editPanel,title,title,"",isServerMap ? 17: 2);
            if (item!=null)
                (item as ButtonItem).MouseHover += bntItem_MouseHover;// creates Radial-Menu 
            m_sideBar.RecalcLayout();
		}

        public void AddClass(string title)
        {
            object item = AppHandler.IdeManager.AddExplorerBarButton(m_classPanel, title, title, "", 12);
            if (item != null)
                (item as ButtonItem).MouseHover += bntItem_MouseHover;// creates Radial-Menu 
            m_sideBar.RecalcLayout();
        }

        public void RemoveClass(string title)
        {
            AppHandler.IdeManager.RemoveExplorerBarButton(m_classPanel, title);
            m_sideBar.RecalcLayout();
        }

        public void AddClassLearnmap(string title)
        {
            object item = AppHandler.IdeManager.AddExplorerBarButton(m_workoutPanel, title, title, "", 2);
            if (item != null)
                (item as ButtonItem).MouseHover += bntItem_MouseHover; // creates Radial-Menu 
            m_sideBar.RecalcLayout();
        }

		public void RemoveWorkoutLearnmap(string title)
		{
			AppHandler.IdeManager.RemoveExplorerBarButton(m_workoutPanel,title);
			m_sideBar.RecalcLayout();
		}

		public void RemoveEditLearnmap(string title)
		{
			AppHandler.IdeManager.RemoveExplorerBarButton(m_editPanel,title);
			m_sideBar.RecalcLayout();
		}

		public void CreateNewEditLearnmap()
		{
			FrmNewLearnmap dlg=new FrmNewLearnmap();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CreateEditLearnmap(dlg.Title);
                AppHandler.ContentManager.OpenLearnmapEditor(AppHandler.MainForm, dlg.Title);
            }
		}

        public void CreateEditLearnmap(string strTitle)
		{
			int newId=AppHandler.MapManager.GetMapCnt()+1;
			string fileName= String.Format("Learnmap{0}",newId);
			string filePath= AppHandler.MapsFolder+"\\"+fileName+".xml";
            if (AppHandler.MapManager.Create(filePath, strTitle,AppHandler.IsServer))
                AppHandler.MapManager.Save(strTitle);
		}

        public void DeleteEditLearnmap(string title)
        {
            string txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_delete_learnmap", "Wollen Sie die Lernmappe mit dem Titel {0}{1}{2} wirklich löschen?");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            string txt2 = String.Format(txt1, '"', title, '"');
            if (MessageBox.Show(txt2, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // close open maps
                if (AppHandler.ContentManager.HasLearnmap(title))
                    AppHandler.ContentManager.CloseLearnmap(title);
                // remove map from classrooms
                string[] astrClasses;
                AppHandler.ClassManager.GetClassNames(out astrClasses);
                if (astrClasses != null)
                {
                    foreach (var c in astrClasses)
                        if (AppHandler.ClassManager.HasLearnmap(c, title))
                            AppHandler.ClassManager.RemoveLearnmap(c, title);
                    AppHandler.ClassManager.Save();
                }

                // destroy map
                AppHandler.UserProgressInfoMgr.DeleteProgressInfoOfMap(title);
                AppHandler.MapManager.Destroy(title);
			}
		}

        public void CreateNewClassroom()
        {
            FrmNewClassroom dlg = new FrmNewClassroom();
            if (dlg.ShowDialog() == DialogResult.OK)
                CreateClassroom(dlg.Title);
        }

        public void CreateClassroom(string strTitle)
        {
            string filePath = AppHandler.ClassesFolder + "\\" + "classes.xml";
            AppHandler.ClassManager.CreateClassroom(strTitle);
            AppHandler.ClassManager.Save(filePath);
        }

        public void DeleteClassroom(string title)
        {
            string txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_delete_classroom", "Wollen Sie die Klasse mit dem Titel {0}{1}{2} wirklich löschen?");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            string txt2 = String.Format(txt1, '"', title, '"');
            if (MessageBox.Show(txt2, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (AppHandler.ContentManager.HasClassroom(title))
                    AppHandler.ContentManager.CloseClassroom(title);

                bool isClassMap=false;
                AppHandler.ClassManager.GetClassMapUsage(title, ref isClassMap);
                if (isClassMap)
                {
                    string[] aMaps;
                    AppHandler.ClassManager.GetLearnmapNames(title, out aMaps);
                    if (aMaps!=null)
                        foreach(string m in aMaps)
                            AppHandler.MapManager.SetUsage(m,false);
                }

                AppHandler.ClassManager.DeleteClassroom(title);
                AppHandler.ClassManager.Save();

            }
        }


        private void UpdateLearnmapWorkings()
        {
            string strUser = AppHandler.MainForm.ActualUserName;
            if (strUser.Length > 0)
            {
                bool isAdmin = false;
                bool isTeacher = false;
                AppHandler.UserManager.GetUserRights(strUser, ref isAdmin, ref isTeacher);

                for (int i = 0; i < AppHandler.MapManager.GetMapCnt(); ++i)
                {
                    string mapTitle;
                    AppHandler.MapManager.GetTitle(i, out mapTitle);
                    if (AppHandler.MapManager.HasUser(i, strUser) || isAdmin)
                    {
                        if (!HasWorkoutLearnmap(mapTitle))
                            AddWorkoutLearnmap(mapTitle);
                    }
                    else
                    {
                        if (HasWorkoutLearnmap(mapTitle))
                            RemoveWorkoutLearnmap(mapTitle);
                    }
                }
            }
        }

        private void SideBar_ExpandedChange(object sender, System.EventArgs e)
        {
            BaseItem item = sender as BaseItem;

            if (item == null)
                return;

            if (item is ExplorerBarGroupItem)
            {
                ExplorerBarGroupItem panel = item as ExplorerBarGroupItem;
                if (panel.Name == "EditLearnmapPanel" || panel.Name == "ClassPanel")
                    radialMenu1.Visible=false;
            }
        }

        private void ControlCenter_LearnmapEvent(object sender, ref LearnmapManagerEventArgs ea)
        {
            if (ea.Command == LearnmapManagerEventArgs.CommandType.Close)
            {
                AppHandler.ContentManager.CloseLearnmapEditor(ea.MapName);
                RemoveWorkoutLearnmap(ea.MapName);
                if (m_editPanel!=null)
                    RemoveEditLearnmap(ea.MapName);
            }
            else if (ea.Command == LearnmapManagerEventArgs.CommandType.Create)
            {
                if (m_editPanel != null)
                    AddEditLearnmap(ea.MapName);
                UpdateLearnmapWorkings();
            }
            else if (ea.Command == LearnmapManagerEventArgs.CommandType.AddUser ||
                     ea.Command == LearnmapManagerEventArgs.CommandType.DeleteUser ||
                     ea.Command == LearnmapManagerEventArgs.CommandType.ChangeUsage)
            {
                UpdateLearnmapWorkings();
            }
        }

        private void ControlCenter_ClassroomEvent(object sender, ref ClassroomManagerEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnClassroomManagerHandler(ControlCenter_ClassroomEvent), new object[] { sender, ea });
                return;
            }
            if (ea.Command == ClassroomManagerEventArgs.CommandType.Close)
            {
                AppHandler.ContentManager.CloseAllClassrooms();
            }
            else if (ea.Command == ClassroomManagerEventArgs.CommandType.Create)
            {
                if (m_classPanel != null)
                {
                    AddClass(ea.ClassName);
                    m_sideBar.RecalcLayout();
                }
            }
            if (ea.Command == ClassroomManagerEventArgs.CommandType.Destroy)
            {
                AppHandler.ContentManager.CloseClassroom(ea.ClassName);
                RemoveClass(ea.ClassName);
                m_sideBar.RecalcLayout();
            }
            else if (ea.Command == ClassroomManagerEventArgs.CommandType.AddLearnmap ||
                     ea.Command == ClassroomManagerEventArgs.CommandType.RemoveLearnmap ||
                     ea.Command == ClassroomManagerEventArgs.CommandType.ChangeUsage)
            {
                UpdateLearnmapWorkings();
            }
        }

        void UserManager_UserManagerEvent(object sender, ref UserManagerEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnUserManagerHandler(UserManager_UserManagerEvent), new object[] { sender, ea });
                return;
            }

            if (ea.Command == UserManagerEventArgs.CommandType.Create)
            {
            }
            else if (ea.Command == UserManagerEventArgs.CommandType.Destroy)
            {
                AppHandler.MapManager.DeleteUser(ea.UserName);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.radialMenu1.Visible = false;
            timer1.Enabled = false;
        }

        void bntItem_MouseHover(object sender, EventArgs e)
        {
            BaseItem item = sender as BaseItem;
            ExplorerBarGroupItem panel = (ExplorerBarGroupItem)item.Parent;
            ButtonItem btnItem = (ButtonItem)item;

            if (m_activePanel != null)
                m_dActButtons[m_activePanel] = null;

            m_dActButtons[panel.Name] = btnItem;

            m_activePanel = panel.Name;
            // set up radial menu for Edit-Learnmaps and Classes
            if (panel.Name == "EditLearnmapPanel" || panel.Name == "ClassPanel")
            {
                Rectangle r = item.Bounds;
                var pos = new Point(r.Right - this.radialMenu1.Size.Width,
                    r.Bottom - this.radialMenu1.Size.Height - 20);

                if (panel.Name == "EditLearnmapPanel")
                {
                    if (AppHandler.IsServer || btnItem.ImageIndex != 17)
                    {
                        this.radialMenu1.Location = pos;
                        this.radialMenu1.Visible = true;
                        this.timer1.Enabled = false;
                        this.timer1.Enabled = true;
                        this.radialMenu1.Items[2].Enabled = true;
                    }
                }
                else if (panel.Name == "ClassPanel")
                {
                    if (!AppHandler.UserManager.IsCentralManaged())
                    {
                        this.radialMenu1.Location = pos;
                        this.radialMenu1.Visible = true;
                        this.timer1.Enabled = false;
                        this.timer1.Enabled = true;
                        this.radialMenu1.Items[2].Enabled = false;
                    }
                }
            }

        }


        private void radialMenu1_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void radialMenu1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void radialMenu1_MenuOpened(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void radialMenu1_MenuClosed(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void radialMenu1_ItemClick(object sender, EventArgs e)
        {
            if ((sender as BaseItem).Name == "radMenuItemAccept")
            {
                if (m_activePanel != null)
                {
                    if (m_activePanel == "EditLearnmapPanel")
                        AppHandler.ContentManager.OpenLearnmapEditor(AppHandler.MainForm, m_dActButtons[m_activePanel].Name);
                    else
                        AppHandler.ContentManager.OpenClassroom(AppHandler.MainForm, m_dActButtons[m_activePanel].Name);
                }
            }
            else if ((sender as BaseItem).Name == "radMenuItemDelete")
            {
                if (m_activePanel != null)
                {
                    if (m_activePanel == "EditLearnmapPanel")
                        DeleteEditLearnmap(m_dActButtons[m_activePanel].Name);
                    else
                        DeleteClassroom(m_dActButtons[m_activePanel].Name);
                }
            }
            else
            {
                var frm = new FrmSelectServer();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string strMapTitle = m_dActButtons[m_activePanel].Name;
                    string[] aClasses;
                    string strClasses = "";
                    if (Program.AppHandler.ClassManager.GetClassNames(out aClasses) > 0)
                    {
                        var aResults = aClasses.Where(c => Program.AppHandler.ClassManager.HasLearnmap(c, strMapTitle));
                        if (aResults.Count() > 0)
                        {
                            var aFoundClasses = aResults.ToArray();
                            strClasses = aFoundClasses[0];
                            for (int i = 1; i < aFoundClasses.Count(); ++i)
                                if (i == (aFoundClasses.Count() - 1))
                                    strClasses += (',' + aFoundClasses[i]);
                                else
                                    strClasses += (',' + aFoundClasses[i] + ',');
                        }
                    }

                    if (strClasses.Length == 0)
                    {
                        string[] aUserNames=null;
                        if (!AppHandler.MapManager.GetUsers(strMapTitle, ref aUserNames))
                        {
                            string txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "Learnmap_is_not_yet_distributed", "Die Lernmappe {0} ist noch nicht verteilt und kann daher nicht gesendet werden!");
                            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            string txt2 = String.Format(txt1, strMapTitle);
                            MessageBox.Show(txt2, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    AppHelpers.ExportLearnmap(m_dActButtons[m_activePanel].Name,frm.SelectedServers);
                }
            }
        }
	}
}

