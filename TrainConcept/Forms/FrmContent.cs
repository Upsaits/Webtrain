using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using SoftObject.SOComponents.Controls;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.ClientServer;
using SoftObject.TrainConcept.Controls;
using SoftObject.TrainConcept.Libraries;
using SoftObject.UtilityLibrary;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for FrmContent.
	/// </summary>
	public partial class FrmContent : XtraForm
	{
        private enum ViewMode { Workings, Tests, Notices };

        /// Timer für Fortschrittsanzeigen
        private const int msCheckProgressTime = 5000;
		private string mapTitle=null;
		private int count = 0;
		private ContentControlBar ctrlBar= new ContentControlBar();
        private Dictionary<string, ContentViews> m_dContentViews = new Dictionary<string, ContentViews>();
		private ActivateableUserControl activeView=null;
		private int chartMaxValue=0;
		private int askTestPermissionReturn = 0;
        private int mapProgress = -1;
        private int selTestResultId = -1;
		private AutoResetEvent  jobDone = new AutoResetEvent(false);
		private ResourceHandler rh=null;
        private ContentOverviewControl frmContentOverview = null;
        private bool isActUserAdmin = false;
        private bool isActUserTeacher = false;
        private ViewMode eActiveView = ViewMode.Workings;
        private Dictionary<String, Tuple<string, int>> dRecentProgress = new Dictionary<String, Tuple<string, int>>();
        private string strWorkProgress;
        private const int cMAXTABS = 10;
        private bool forcedClose = false;
        private AppHandler AppHandler = Program.AppHandler;

        #region Properties

        public ContentControlBar CtrlBar
		{
			get { return ctrlBar; }
		}


		public string MapTitle
		{
			get { return mapTitle; }
		}

		public XtraTabControl TabControl
		{
			get { return xtraTabControl1; }
		}

		public ActivateableUserControl ActiveView
		{
			get { return activeView;}
		}

        public Dictionary<String, Tuple<String, int>> RecentProgress
        {
            get { return dRecentProgress; }
        }
        #endregion

        public FrmContent()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
        }

		public FrmContent(string _mapTitle)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.timerMapProgress.Enabled = true;
            this.timerMapProgress.Interval = msCheckProgressTime;

			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			mapTitle  = _mapTitle;

			SuspendLayout();
			this.Icon = rh.GetIcon("main_small");
			this.imageList1.Images.Add(rh.GetIcon("main_small"));

			this.btnTests.Text = AppHandler.LanguageHandler.GetText("FORMS","Tests","Tests");
			this.btnCertification.Text = AppHandler.LanguageHandler.GetText("FORMS","certification","Zertifikat");
			this.btnResult.Text = AppHandler.LanguageHandler.GetText("FORMS","Result","Ergebnis");

			this.ctrlBar.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ctrlBar.Location = new System.Drawing.Point(0, 293);
            this.ctrlBar.Size = new System.Drawing.Size(560, 64);
            this.ctrlBar.ChartControl = soChart1;
            this.ctrlBarPanel.Controls.Add(this.ctrlBar);
            this.ctrlBarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            ResumeLayout();

			ctrlBar.BtnLearning.Click += OnBtnLearning;
			ctrlBar.BtnExaming.Click += OnBtnExaming;
			ctrlBar.BtnTesting.Click += OnBtnTesting;
			ctrlBar.BtnNextPage.Click += OnBtnNextPage;
			ctrlBar.BtnPrevPage.Click += OnBtnPrevPage;

			AppHandler.CtsClientManager.OnCTSClientEvent += FrmContent_CTSClient;
			AppHandler.TestResultManager.OnChangedEvent += FrmContent_TestResultsChanged;
            AppHandler.NoticeManager.NoticeManagerEvent += NoticeManager_NoticeManagerEvent;
            AppHandler.UserProgressInfoMgr.UserProgressInfoManagerEvent += UserProgressInfoMgr_UserProgressInfoManagerEvent;

			CreateOverviewPage();

            RestoreRecentProgress();

            timerMapProgress.Start();

			OnResize(EventArgs.Empty);
		}

        public new void Close()
        {
            forcedClose = true;
            base.Close();
        }

        private void RestoreRecentProgress()
        {
            strWorkProgress = "";
            dRecentProgress.Clear();

            // connect to server
            if ((AppHandler.IsClient || AppHandler.IsSingle) && AppHandler.CtsClientManager.IsRunning())
                AppHandler.CtsClientManager.AskWorkProgress(AppHandler.MainForm.ActualUserName, mapTitle, "all");
            else
            {
                strWorkProgress = AppHelpers.GetWorkProgress(AppHandler.MainForm.ActualUserName, mapTitle);
                if (strWorkProgress.Length > 0)
                    AnalyseWorkProgress();
            }
        }

        private bool AnalyseWorkProgress()
        {
            if (strWorkProgress.IndexOf(',') > 0)
            {
                //analyse strWorkProgress
                int[] aWorkProgress = strWorkProgress.Split(',').Select(str => int.Parse(str)).ToArray();
                string[] aWorkings = null;
                if (AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings))
                {
                    if (aWorkings.Length == aWorkProgress.Length) // check if they are the same
                    {
                        for (int i = 0; i < aWorkings.Length; ++i)
                        {
                            if (aWorkProgress[i] > 0) // any Pages entered?
                            {
                                string[] aTitles;
                                Utilities.SplitPath(aWorkings[i], out aTitles);
                                if (!dRecentProgress.ContainsKey(aTitles[3]))
                                    dRecentProgress.Add(aTitles[3], new Tuple<string, int>(aWorkings[i], aWorkProgress[i]));
                                else
                                {
                                    dRecentProgress[aTitles[3]] = new Tuple<string,int>(aWorkings[i],aWorkProgress[i]);
                                }
                            }
                            AppHandler.MainForm.LibOverview.UpdateCheckState(aWorkings[i], aWorkProgress[i]);
                        }
                        return true;
                    }
                }
            }
            return false;
        }


        public new void Activate()
        {
            base.Activate();
            AfterActivation(true);
        }

        public void AfterActivation(bool bIsOn)
        {
            if (!bIsOn)
            {
                ClearActiveView();
                AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = false;
                AppHandler.MainForm.LibOverviewBar.Visible = false;
                //AppHandler.MainForm.NoticeBar.Visible = false;
                AppHandler.MainForm.CloseBar.Visible = false;
            }
            else
            {
                SetActiveView();
                string strPathPt = frmContentOverview.GetFirstPoint();
                AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = true;
                AppHandler.MainForm.LibOverview.ContentTree.LearnmapName = mapTitle;
                AppHandler.MainForm.LibOverview.ContentTree.UseType = ContentTreeViewList.UseType.LearnmapContent;
                AppHandler.MainForm.LibOverview.SetActualPoint(strPathPt);
                AppHandler.MainForm.LibOverview.UpdateView();
                AppHandler.MainForm.LibOverviewBar.Visible = true;
                AppHandler.MainForm.LibOverview.NavigationPanel.Visible = false;
                AppHandler.MainForm.CloseBar.Visible = true;

                AnalyseWorkProgress();
            }
        }

		private void CreateOverviewPage()
		{
			string txt=AppHandler.LanguageHandler.GetText("FORMS","Overview","Inhaltsübersicht");

            frmContentOverview = new ContentOverviewControl(mapTitle);
            frmContentOverview.Dock = DockStyle.Fill;
            AddTabPage(txt,frmContentOverview,imageList1,0,true); //Füge neuen TAB ein

			count++; //Zähle Anzahl TAB's mit
		}


        private void AddTabPage(string strTitle,Control clientControl,ImageList imglist, int imageId, bool isActive)
        {
            var page = new XtraTabPage();
            page.Text = strTitle;
            page.Controls.Add(clientControl);
            page.ImageIndex = 0;
            page.Image = imglist.Images[imageId];
            page.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl1.TabPages.Add(page);
            if (isActive)
                xtraTabControl1.SelectedTabPage = page;
        }


	    public bool IsPageAllowed(string work, ref string strTestname, ref int posAfterTest)
	    {

	        bool bIsTest;
	        int _posAfterTest = 1;
	        string strNextPointPath = frmContentOverview.GetPrevPoint(work, out bIsTest);
	        while (strNextPointPath.Length > 0 && !bIsTest)
	        {
                strNextPointPath = frmContentOverview.GetPrevPoint(strNextPointPath, out bIsTest);
	            ++_posAfterTest;
	        }

	        if (bIsTest)
	        {
	            strTestname = strNextPointPath;
	            posAfterTest = _posAfterTest;
                return AppHelpers.IsTestPassed(AppHandler.MainForm.ActualUserName, mapTitle, strNextPointPath);
	        }

	        strTestname = "";
	        posAfterTest = 0;
	        return true;
	    }

        public void SelectPage(string work)
        {
            // Suche bereits geöffneter Seite
            var page = FindPageByWork(work);
            if (page != null)
            {
                //page.Selected = true;
                if (page.Controls[0] is ContentLearningControl)
                    ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Learning;
                else if (page.Controls[0] is ContentExamingControl)
                    ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Examing;
                else
                    ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Testing;
            }
        }


		public void AddPage(string title, string work, string testname, bool isActive)
		{
			// Suche bereits geöffneter Seite
            var page = FindPageByWork(work);
			if (page!=null)
			{
				//page.Selected=true;
				if (page.Controls[0] is ContentLearningControl)
					ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Learning;
                else if (page.Controls[0] is ContentExamingControl)
					ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Examing;
				else
					ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Testing;
                xtraTabControl1.SelectedTabPage = page;
				return;
			}

            try
            {
                ContentViews views = new ContentViews();
                views.LearningView = new ContentLearningControl(this, work);
                views.ExamingView = new ContentExamingControl(this, work);
                views.TestingView = new ContentTestingControl(this, work);
                m_dContentViews[work] = views;

                Control viewToActivate = views.LearningView;
                if (testname.Length > 0)
                {
                    ctrlBar.BtnTesting.Checked = true;
                    views.TestingView.SetTest(testname);
                    AppHandler.TestResultManager.Start(AppHandler.MainForm.ActualUserName, mapTitle,testname, DateTime.Now);
                    viewToActivate = views.TestingView;
                }

                viewToActivate.Dock = DockStyle.Fill;
				AddTabPage(title,viewToActivate, imageList1, 0,isActive); //Füge neuen TAB ein
                count++; //Zähle Anzahl TAB's mit

                if (!dRecentProgress.ContainsKey(title))
                    dRecentProgress.Add(title, new Tuple<string, int>(work,1));

                if (xtraTabControl1.TabPages.Count > cMAXTABS)
                    RemovePageById(1);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
		}

        public void RemovePageById(int id)
        {
            var page = xtraTabControl1.TabPages[id];
            if (page != null)
            {
                string work = TabPageFindWork(page);
                xtraTabControl1.TabPages.Remove(page);
                if (page.Controls[0] is Form)
                    ((Form) page.Controls[0]).Close();
                m_dContentViews.Remove(work); 
                count--;
            }
        }


		public void RemovePage(string work)
		{
            var page = FindPageByWork(work);
			if (page!=null)
			{
                xtraTabControl1.TabPages.Remove(page);
                if (page.Controls[0] is Form)
                    ((Form) page.Controls[0]).Close();
                m_dContentViews.Remove(work);
				count--;
			}
		}

		public void ClearActiveView()
		{
			if (activeView is ContentLearningControl)
				((ContentLearningControl) activeView).SetActive(false);
			else if (activeView is ContentExamingControl)
				((ContentExamingControl) activeView).SetActive(false);
            else if (activeView is ContentTestingControl)
                ((ContentTestingControl) activeView).SetActive(false);
		}

		public void SetActiveView()
		{
			SetActiveView(activeView);
		}

		public void SetLearningView()
		{
            string work = TabPageFindWork(xtraTabControl1.SelectedTabPage);
            if (work.Length>0)
                SetActiveView(m_dContentViews[work].LearningView);
		}

        public void SetExamingView()
        {
            string work = TabPageFindWork(xtraTabControl1.SelectedTabPage);
            if (work.Length > 0)
                SetActiveView(m_dContentViews[work].ExamingView);
        }

        public bool CheckIntermediateTest(string strTestname="")
        {
            string work = TabPageFindWork(xtraTabControl1.SelectedTabPage);
            if (work.Length > 0)
            {
                var testView = m_dContentViews[work].TestingView;
                if (testView.TestId > 0)
                {
                    if (testView.IsWorkedOut)
                    {
                        bool bWasCorrect = testView.IsWorkedOutCorrect;
                        strTestname = testView.TestName;
                        testView.ResetTest();
                        if (bWasCorrect)
                        {
                            // Test erfolgreich gewesen -> weiter gehts mit true, sonst neuer Test
                            return AppHelpers.IsTestPassed(AppHandler.MainForm.ActualUserName, mapTitle, strTestname);
                        }
                        else
                        {
                            testView.SetTest(strTestname);
                            AppHandler.TestResultManager.Start(AppHandler.MainForm.ActualUserName, mapTitle, strTestname, DateTime.Now);
                            return false;
                        }
                    }
                    else
                    {
                        if (strTestname.Length > 0)
                        {
                            var aTRIs = new TestResultItemCollection();
                            AppHandler.TestResultManager.Find(out aTRIs, AppHandler.MainForm.ActualUserName, mapTitle,
                                strTestname, false);
                            if (aTRIs.Count > 0)
                                for (int i = 0; i < aTRIs.Count; ++i)
                                    AppHandler.TestResultManager.Delete(AppHandler.MainForm.ActualUserName, mapTitle, strTestname, i);
                            testView.ResetTest();
                        }
                    }
                }

                // Test starten
                if (strTestname.Length > 0)
                {
                    // todo: Test evt. auch direkt starten, nicht indirekt über Button
                    // Test starten
                    testView.SetTest(strTestname);
                    bool bWasEnabled = ctrlBar.BtnTesting.Enabled;
                    ctrlBar.BtnTesting.Enabled = true;
                    ctrlBar.BtnTesting.PerformClick();
                    ctrlBar.BtnTesting.Enabled = bWasEnabled;
                    return false; // intermediate Test wird ausgeführt
                }
            }

            return true;
        }

		private void SetActiveView(ActivateableUserControl newView)
		{
            if (newView!=null && newView != activeView)
            {
                string actualWork = "";
                UserProgressInfoManager.RegionType tRegion = UserProgressInfoManager.RegionType.Unknown;

                if (activeView != null)
                {
                    if (activeView is ContentLearningControl)
                    {
                        ((ContentLearningControl)activeView).SetActive(false);
                        actualWork = ((ContentLearningControl)activeView).Work;
                        tRegion = UserProgressInfoManager.RegionType.Learning;
                    }
                    else if (activeView is ContentExamingControl)
                    {
                        ((ContentExamingControl)activeView).SetActive(false);
                        actualWork = ((ContentExamingControl)activeView).Work;
                        tRegion = UserProgressInfoManager.RegionType.Examing;
                    }
                    else if (activeView is ContentTestingControl)
                    {
                        ((ContentTestingControl)activeView).SetActive(false);
                        actualWork = ((ContentTestingControl)activeView).Work;
                        tRegion = UserProgressInfoManager.RegionType.Testing;
                    }
                    else if (activeView is ContentOverviewControl)
                    {
                        ((ContentOverviewControl)activeView).SetActive(false);
                    }

                    if (tRegion != UserProgressInfoManager.RegionType.Unknown)
                        AppHelpers.AddUserProgressInfo(AppHandler.MainForm.ActualUserName, actualWork, (int)tRegion, HelperMacros.MAKEWORD(0, 0));
                }

                newView.Dock = DockStyle.Fill;
                xtraTabControl1.SelectedTabPage.Controls.Clear();
                xtraTabControl1.SelectedTabPage.Controls.Add(newView);

                actualWork = "";
                tRegion = UserProgressInfoManager.RegionType.Unknown;

                if (newView is ContentLearningControl)
                {
                    ((ContentLearningControl)newView).SetActive(true);
                    actualWork = ((ContentLearningControl)newView).Work;
                    tRegion = UserProgressInfoManager.RegionType.Learning;

                    ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Learning;

                    UpdateInfoLine(newView.Text);

                    CheckPageButtons();
                }
                else if (newView is ContentExamingControl)
                {
                    actualWork = ((ContentExamingControl)newView).Work;
                    tRegion = UserProgressInfoManager.RegionType.Examing;

                    ((ContentExamingControl)newView).SetActive(true);
                    ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Examing;

                    string sTxt = AppHandler.LanguageHandler.GetText("SYSTEM", "Examing_questions_for_learnmap", "Übungsfragen für Lernmappe");
                    UpdateInfoLine(sTxt + ": " + mapTitle);
                }
                else if (newView is ContentTestingControl)
                {
                    actualWork = ((ContentTestingControl)newView).Work;
                    tRegion = UserProgressInfoManager.RegionType.Testing;

                    ((ContentTestingControl)newView).SetActive(true);
                    ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Testing;

                    string sTest = ((ContentTestingControl) newView).GetTestDescription();
                    string sTxt = AppHandler.LanguageHandler.GetText("SYSTEM", "Test_questions_for_learnmap", "Testfragen für Lernmappe");
                    sTxt = String.Format(sTxt, sTest, mapTitle);
                    UpdateInfoLine(sTxt);
                }
                else if (newView is ContentOverviewControl)
                {
                    //((FrmContentOverview)newView).SetActive(true);
                }

                if (tRegion != UserProgressInfoManager.RegionType.Unknown)
                    AppHelpers.AddUserProgressInfo(AppHandler.MainForm.ActualUserName, actualWork, (int)tRegion, HelperMacros.MAKEWORD(1, 0));

                string strColor;
                AppHandler.MapManager.GetColor(mapTitle, out strColor);
                if (strColor.Length > 0)
                    this.topPanel.BackColor = XMapsTree.ToColor(strColor).Value;

                activeView = newView;
            }
            else
            {
                if (activeView is ContentLearningControl)
                    ((ContentLearningControl)activeView).SetActive(true);
            }
		}


		public void ShowChartControl(bool isVisible)
		{
			if (isVisible)
				soChart1.Show();
			else
				soChart1.Hide();
		}

		public void SetChartControlMaxValue(int _maxValue)
		{
			chartMaxValue=_maxValue;
			soChart1.PercentONE  = 0;
			soChart1.PercentTWO  = 0;
			soChart1.PercentTHREE= 0;
		}

		public void SetChartControlValues(int _val1,int _val2,int _val3)
		{
            soChart1.PercentONE = (chartMaxValue>0) ? _val1 * 100 / chartMaxValue:0;
            soChart1.PercentTWO = (chartMaxValue > 0) ? _val2 * 100 / chartMaxValue : 0;
            soChart1.PercentTHREE = (chartMaxValue > 0) ? _val3 * 100 / chartMaxValue : 0;
		}

		public void SetLearningPage(int pageId)
		{
			OnBtnLearning(this,EventArgs.Empty);
			if (activeView is ContentLearningControl)
				((ContentLearningControl) activeView).SetPage(pageId);
		}

		public void JumpToLearningPage(string fromWork,int fromPageId,int toPageId,bool withBackJump)
		{
            if (this.eActiveView != ViewMode.Workings)
                SwitchViewMode(ViewMode.Workings);

            if (activeView is ContentLearningControl)
                ((ContentLearningControl)activeView).UpdatePage();
            SetLearningPage(toPageId/* + 1*/);
			if (withBackJump && (activeView is ContentLearningControl))
			{
				((ContentLearningControl) activeView).JumpWork=fromWork;
				((ContentLearningControl) activeView).JumpPage=fromPageId;
			}
		}

        public bool TryJumpToContent(string strWork)
        {
            if (this.eActiveView != ViewMode.Workings)
                SwitchViewMode(ViewMode.Workings);
            return frmContentOverview.SelectItem(strWork);
        }

		public bool IsTestActive()
		{
            if (xtraTabControl1.SelectedTabPage != null && (xtraTabControl1.SelectedTabPage.Controls[0] is ContentTestingControl))
            {
                ContentTestingControl frm = xtraTabControl1.SelectedTabPage.Controls[0] as ContentTestingControl;
                if (frm!=null && !(frm.CanClose(true)))
                    return true;
            }
            return false;
        }

		private bool IsTestAllowed(string testName)
		{
		    if (testName == Utilities.c_strDefaultFinalTest)
		    {
                if ((AppHandler.IsClient || AppHandler.IsSingle) && AppHandler.CtsClientManager.IsRunning())
                {
                    // Server fragen
                    return (AppHandler.CtsClientManager.AskTestPermission(AppHandler.MainForm.ActualUserName, mapTitle,testName, ref jobDone));
                }
                return AppHelpers.IsTestInMapAllowed(AppHandler.MainForm.ActualUserName, mapTitle, testName);
		    }

		    return true;
		}

        public void CheckPageButtons()
        {
            if (xtraTabControl1.SelectedTabPage != null && xtraTabControl1.SelectedTabPage.Controls[0] != null)
            {
                if (xtraTabControl1.SelectedTabPage.Controls[0] is ContentLearningControl)
                {
                    ContentLearningControl frmLearning = xtraTabControl1.SelectedTabPage.Controls[0] as ContentLearningControl;
                    if (frmLearning != null)
                    {
                        string strNextPointPath = frmContentOverview.GetNextPoint(frmLearning.Work);
                        ctrlBar.ButtonNextPageEnabled = strNextPointPath.Length > 0;
                    }

                    string strPrevPointPath = frmContentOverview.GetPrevPoint(frmLearning.Work);
                    ctrlBar.ButtonPrevPageEnabled = strPrevPointPath.Length > 0;

                    ctrlBar.PageId = frmLearning.GetPageId();

                    // enable Testbutton only if Map was completed and questions are available
                    var frmTesting = m_dContentViews[frmLearning.Work].TestingView;
                    if (frmTesting != null)
                        ctrlBar.BtnTesting.Enabled = frmTesting.IsTestAlwaysAllowed || (frmTesting.HasQuestions && mapProgress==100);

                    // enable Exambutton only if questions are available
                    var frmExaming = m_dContentViews[frmLearning.Work].ExamingView;
                    if (frmExaming != null)
                        ctrlBar.BtnExaming.Enabled = frmExaming.HasQuestions;
                }
            }
        }



        private void SwitchViewMode(ViewMode eToViewMode)
        {
            if (eToViewMode == ViewMode.Tests)
            {
                xtraTabControl1.Hide();

                string txt = AppHandler.LanguageHandler.GetText("FORMS", "Testoverview_for_map", "Testübersicht für Lernmappe");
                UpdateInfoLine(txt + " :" + mapTitle);
                
                xTreeViewTests.FillData(mapTitle);
                xTreeViewTests.Show();

                btnTests.Text = AppHandler.LanguageHandler.GetText("FORMS", "Contents", "Inhalte");
                btnResult.Show();
                btnResult.Enabled = false;

                if (isActUserTeacher)
                {
                    this.btnDelete.Show();
                }

                if (!AppHandler.IsClient || AppHandler.IsSingle)
                {
                    btnCertification.Show();
                    btnCertification.Enabled = false;
                }

                eActiveView = ViewMode.Tests;
            }
            else if (eToViewMode == ViewMode.Workings)
            {
                xTreeViewTests.Hide();
                string txt = AppHandler.LanguageHandler.GetText("FORMS", "Contentoverview_for_map", "Inhaltsübersicht für Lernmappe");
                
                UpdateInfoLine(txt + " :" + mapTitle);

                xtraTabControl1.Show();

                btnTests.Text = AppHandler.LanguageHandler.GetText("FORMS", "Tests", "Tests");
                btnResult.Hide();
                btnCertification.Hide();
                btnDelete.Hide();

                if (AppHandler.MainForm.Notice != null)
                    AppHandler.MainForm.Notice.HideTopPanel();

                eActiveView = ViewMode.Workings;
            }
            else
            {
                xtraTabControl1.Hide();
                string txt = AppHandler.LanguageHandler.GetText("FORMS", "noticeoverview_for_map", "Notizenübersicht für Lernmappe");

                UpdateInfoLine(txt + " :" + mapTitle);

                xTreeViewTests.Hide();
                btnResult.Hide();
                btnCertification.Hide();
                btnDelete.Hide();

                xTreeViewNotices.FillData(mapTitle);
                xTreeViewNotices.Dock = DockStyle.Fill;
                xTreeViewNotices.Show();

                btnTests.Text = AppHandler.LanguageHandler.GetText("FORMS", "Contents", "Inhalte");
                btnResult.Hide();
                btnCertification.Hide();
                btnDelete.Hide();

                eActiveView = ViewMode.Notices;
            }
        }

        private void DeleteSelectedTestEntries()
        {
            if (xTreeViewTests.Selection.Count>0)
            {
                //if (isActUserTeacher)
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Delete_Test_Result", "Testergebnis(se) löschen?");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = this.xTreeViewTests.Selection.Count; i > 0; --i)
                        {
                            /*
                            var tri=AppHandler.TestResultManager.Get(mapTitle, this.xTreeViewTests.Selection[i - 1].Id);
                            if (!isActUserTeacher && tri.testName == Utilities.c_strDefaultFinalTest)
                            {
                                txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Finaltest_can_only_be_deleted_by_Teacher", "Sie dürfen keine Finaltests löschen!");
                                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else*/
                                AppHandler.TestResultManager.Delete(mapTitle, this.xTreeViewTests.Selection[i - 1].Id);
                        }

                        AppHandler.TestResultManager.Save();
                        AppHandler.TestResultManager.FireEvent(EventArgs.Empty);

                        AppHandler.MainForm.ControlCenter.UpdateWorkoutLearnmapState(mapTitle);
                    }
                }
            }
        }

        private void DeleteSelectedNotices()
        {
            if (this.xTreeViewNotices.Selection.Count > 0)
            {
                if (isActUserTeacher)
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Delete_Notices", "Selektierte Notiz(en) löschen?");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        NoticeTreeRecord[] aSelNotices;
                        if (this.xTreeViewNotices.GetSelectedNotices(out aSelNotices)>0)
                            foreach (var n in aSelNotices)
                            {
                                int iId=AppHandler.NoticeManager.Find(n.User, n.Title);
                                if (iId>=0)
                                    AppHandler.NoticeManager.DeleteNotice(iId);
                            }

                        AppHandler.NoticeManager.Save();
                    }
                }
            }
        }

		
		private void OnClosed(object sender, System.EventArgs e)
		{
            AfterActivation(false);
			SetActiveView(null);

			AppHandler.ContentManager.LearnmapClosed(Text);

			AppHandler.CtsClientManager.OnCTSClientEvent -= new OnCTSClientHandler(FrmContent_CTSClient);
			AppHandler.TestResultManager.OnChangedEvent -= new OnTestResultsChangedHandler(FrmContent_TestResultsChanged);
		}

		public void OnBtnLearning(object sender, System.EventArgs e)
		{
            // irgendwie kommt da von line:386 dieser Aufruf daher, abblocken wenn nicht von maus getriggert!
            if (sender != null && (sender is SORadioBmpBtn))
            {
                var btn = sender as SORadioBmpBtn;
                if (!btn.MouseInside)
                    return;
            }

            if (xtraTabControl1.SelectedTabPage != null && xtraTabControl1.SelectedTabPage.Controls != null && xtraTabControl1.SelectedTabPage.Controls[0] != null &&
                !(xtraTabControl1.SelectedTabPage.Controls[0] is ContentLearningControl))
			{
                ContentTestingControl frm = xtraTabControl1.SelectedTabPage.Controls[0] as ContentTestingControl;
                if (frm != null)
                {
                    if (!frm.CanClose(false))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "You_have_to_finish_the_test",
                            "Sie müssen den Test abschließen!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Testing;
                        ctrlBar.BtnTesting.Checked = true;
                        return;
                    }
                }

                SetLearningView();

                AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = true;
                AppHandler.MainForm.LibOverviewBar.Visible = true;
			}
		}

		private void OnBtnExaming(object sender, System.EventArgs e)
		{
            if (xtraTabControl1.SelectedTabPage != null && xtraTabControl1.SelectedTabPage.Controls != null && 
                xtraTabControl1.SelectedTabPage.Controls.Count > 0 &&  xtraTabControl1.SelectedTabPage.Controls[0] != null && 
                !(xtraTabControl1.SelectedTabPage.Controls[0] is ContentExamingControl))
			{
                ContentTestingControl frm = xtraTabControl1.SelectedTabPage.Controls[0] as ContentTestingControl;
                if (frm != null && !(frm.CanClose(false)) || !CheckIntermediateTest())
                {
					string txt=AppHandler.LanguageHandler.GetText("ERROR","You_have_to_finish_the_test","Sie müssen den Test abschließen!");
					string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
					MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
					ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Testing;
					ctrlBar.BtnTesting.Checked = true;
					return;
				}

			    SetExamingView();

                AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = true;
                AppHandler.MainForm.LibOverviewBar.Visible = true;

				this.WindowState = FormWindowState.Normal;
				this.WindowState = FormWindowState.Maximized;
			}
		}

		private void OnBtnTesting(object sender, System.EventArgs e)
		{
            if (xtraTabControl1.SelectedTabPage!=null && xtraTabControl1.SelectedTabPage.Controls!=null && 
                xtraTabControl1.SelectedTabPage.Controls.Count>0 &&  xtraTabControl1.SelectedTabPage.Controls[0]!=null &&
                !(xtraTabControl1.SelectedTabPage.Controls[0] is ContentTestingControl))
			{
                string work = TabPageFindWork(xtraTabControl1.SelectedTabPage);
                var testView = m_dContentViews[work].TestingView;
			    var ti = AppHandler.MapManager.GetTest(mapTitle, testView.TestId);
			    if (ti != null)
			    {
                    if (IsTestAllowed(ti.title))
                    {
                        if (work.Length > 0)
                            SetActiveView(m_dContentViews[work].TestingView);

                        AppHandler.TestResultManager.Start(AppHandler.MainForm.ActualUserName, mapTitle, ti.title, DateTime.Now);
                    }
                    else
                    {
                        string txt = AppHandler.LanguageHandler.GetText("ERROR", "Youre_not_allowed_to_perform_a_test", "Sie dürfen keinen Test absolvieren!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (xtraTabControl1.SelectedTabPage.Controls[0] is ContentLearningControl)
                        {
                            ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Learning;
                            ctrlBar.BtnLearning.Checked = true;
                        }
                        else
                        {
                            ctrlBar.WorkingMode = ContentControlBar.WorkingModeType.Examing;
                            ctrlBar.BtnExaming.Checked = true;
                        }
                    }
			    }
			}
		}

		private void OnBtnNextPage(object sender, System.EventArgs e)
		{
            if (xtraTabControl1.SelectedTabPage != null && xtraTabControl1.SelectedTabPage.Controls != null &&
                xtraTabControl1.SelectedTabPage.Controls.Count > 0 && xtraTabControl1.SelectedTabPage.Controls[0] != null &&
                xtraTabControl1.SelectedTabPage.Controls[0] is ContentLearningControl)
            {
                var view = (activeView as ContentLearningControl);
                if (view != null)
                {
                    bool bIsTest;
                    string strNextPointPath = frmContentOverview.GetNextPoint(view.Work, out bIsTest);
                    if (bIsTest)
                    {
                        bool bPassed = AppHelpers.IsTestPassed(AppHandler.MainForm.ActualUserName, mapTitle, strNextPointPath);
                        if (!bPassed && !CheckIntermediateTest(strNextPointPath))
                            return;
                        strNextPointPath = frmContentOverview.GetNextPoint(strNextPointPath, out bIsTest);
                    }

                    AppHandler.MainForm.NoticeBar.Visible = false;
                    if (strNextPointPath.Length > 0)
                        frmContentOverview.SelectItem(strNextPointPath);
                }
			}
		}

		private void OnBtnPrevPage(object sender, System.EventArgs e)
		{
            if (xtraTabControl1.SelectedTabPage != null && xtraTabControl1.SelectedTabPage.Controls != null &&
                xtraTabControl1.SelectedTabPage.Controls.Count > 0 && xtraTabControl1.SelectedTabPage.Controls[0] != null &&
                xtraTabControl1.SelectedTabPage.Controls[0] is ContentLearningControl)
			{
                ContentLearningControl frmLearning = activeView as ContentLearningControl;
			    if (frmLearning != null)
			    {
                    string strPrevPointPath = frmContentOverview.GetPrevPoint(frmLearning.Work);
                    if (strPrevPointPath.Length > 0)
                        frmContentOverview.SelectItem(strPrevPointPath);
                    AppHandler.MainForm.NoticeBar.Visible = false;
			    }
            }
		}

        public bool OnLibOverviewFocusNodeChange(string strNewPath)
        {
            if (!(ActiveView is ContentTestingControl))
            {
                if (!TryJumpToContent(strNewPath))
                {
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    string txt2 = AppHandler.LanguageHandler.GetText("MESSAGE", "Content_not_contained_in_learnmap", "Dieser Inhalt befindet sich nicht in der aktuellen Lernmappe");
                    MessageBox.Show(txt2, cap, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                return true;
            }
            return false;
        }


		private void tabControl1_ClosePressed(object sender, System.EventArgs e)
		{
		}

		private void btnTests_Click(object sender, System.EventArgs e)
		{
            SwitchViewMode((eActiveView!=ViewMode.Workings) ? ViewMode.Workings : ViewMode.Tests);
		}

		private void FrmContent_CTSClient(object sender,CTSClientEventArgs ea)
		{
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnCTSClientHandler(FrmContent_CTSClient), new object[] { sender, ea });
                return;
            }

            if (ea.Command == CTSClientEventArgs.CommandType.TestPermission)
			{
                if (ea.MapName == mapTitle)
                    askTestPermissionReturn = ea.ReturnValue;
			}
            else if (ea.Command == CTSClientEventArgs.CommandType.MapProgress)
            {
                if (ea.MapName == mapTitle)
                {
                    mapProgress = ea.ReturnValue;
                    UpdateInfoLine("", true);
                }
            }
            else if (ea.Command == CTSClientEventArgs.CommandType.WorkProgress)
            {
                if (ea.MapName == mapTitle)
                {
                    if (ea.WorkName == "all")
                    {
                        strWorkProgress = ea.ReturnValues;
                        AnalyseWorkProgress();
                    }
                    else
                    {
                    }
                }
            }
        }

		private void FrmContent_TestResultsChanged(object sender,EventArgs e)
		{
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnTestResultsChangedHandler(FrmContent_TestResultsChanged), sender, e);
                return;
            }

			if (eActiveView == ViewMode.Tests)
				xTreeViewTests.FillData(mapTitle);
		}

		private void testTreeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left) 
			{
				DevExpress.XtraTreeList.TreeListHitInfo hInfo = xTreeViewTests.CalcHitInfo(new Point(e.X, e.Y));
				if(hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell)
				{
					selTestResultId=hInfo.Node.Id;
					btnResult.Enabled=true;
					btnCertification.Enabled=true;
                    btnDelete.Enabled = selTestResultId >= 0 && isActUserTeacher;

					if (e.Clicks==2)
						btnResult_Click(this,System.EventArgs.Empty);
				}
			}
		}

        private void testTreeView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedTestEntries();
        }

        
		private void btnResult_Click(object sender, System.EventArgs e)
		{
			if (selTestResultId>=0)
			{
				var aTestResults=new TestResultItemCollection();
				if (isActUserTeacher)
					AppHandler.TestResultManager.Find(ref aTestResults,mapTitle);
				else
					AppHandler.TestResultManager.Find(ref aTestResults,AppHandler.MainForm.ActualUserName,mapTitle);

				if (aTestResults.Count>selTestResultId)
				{
					TestResultItem item = aTestResults.Item(selTestResultId);
					if(item.aTestQuestionResults!=null)
					{
						FrmTestReport frmRep = new FrmTestReport(item);
						frmRep.Show();
					}
				}
			}
		}

		private void btnCertification_Click(object sender, System.EventArgs e)
		{
			if (selTestResultId>=0)
			{
				TestResultItemCollection aTestResults=new TestResultItemCollection();
				if (isActUserTeacher)
					AppHandler.TestResultManager.Find(ref aTestResults,mapTitle);
				else
					AppHandler.TestResultManager.Find(ref aTestResults,AppHandler.MainForm.ActualUserName,mapTitle);

				if (aTestResults.Count>selTestResultId)
				{
					TestResultItem item = aTestResults.Item(selTestResultId);

					string strPassword="";
					string strFullName="";
                    int iImgId = 0;
					AppHandler.UserManager.GetUserInfo(item.userName,ref strPassword,ref strFullName,ref iImgId);

					FrmEditCertification dlg= new FrmEditCertification(strFullName,item.mapName);
					dlg.ShowDialog();
					if (dlg.DialogResult == DialogResult.OK)
					{
						FrmCertification frmCert=new FrmCertification(dlg.UserName,dlg.ContentTitle,dlg.SuccessLevel,dlg.TeacherName,
																	  dlg.Place,dlg.Time,dlg.LogoImage);
						frmCert.Show();
					}
				}
			}
		}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedTestEntries();
        }

        private void FrmContent_Load(object sender, EventArgs e)
        {
            isActUserAdmin = false;
            isActUserTeacher = false;
            AppHandler.UserManager.GetUserRights(AppHandler.MainForm.ActualUserName, ref isActUserAdmin, ref isActUserTeacher);
            //xtraTabControl1.CustomHeaderButtons.Add(new CustomHeaderButton(ButtonPredefines.DropDown));
            //xtraTabControl1.CustomHeaderButtons.Add(new CustomHeaderButton(ButtonPredefines.Close));
        }

        private void btnNotices_Click(object sender, EventArgs e)
        {
            SwitchViewMode(ViewMode.Notices);
        }

        private void xTreeViewNotices_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hInfo = xTreeViewNotices.CalcHitInfo(new Point(e.X, e.Y));
                if (hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell)
                {
                    int selId = hInfo.Node.Id;
                    if (e.Clicks == 2)
                    {
                        var n = (NoticeTreeRecord)this.xTreeViewNotices.GetDataRecordByNode(hInfo.Node);
                        string userName = n.User;
                        string title = n.Title;

                        int nId = AppHandler.NoticeManager.Find(n.User, n.Title);
                        if (nId >= 0)
                        {
                            NoticeItem ni = AppHandler.NoticeManager.GetNotice(nId);
                            string dirName = String.Format("{0}\\{1}\\notices\\", AppHandler.NoticeManager.GetNoticePath(), ni.userName);
                            string filePath = dirName + '\\' + ni.fileName;

                            AppHandler.MainForm.NoticeBar.Visible = true;
                            if (!AppHandler.IsServer)
                                AppHandler.MainForm.Notice.Load(nId,filePath, "",GetNoticeTitle, (string strFilename, ref string strSolFilename) => false, SaveNotice, GetNoticeWorkedOutState,null,false);
                            else
                                AppHandler.MainForm.Notice.Load(nId,filePath, "",GetNoticeTitle, (string strFilename, ref string strSolFilename) => false, SaveNotice, GetNoticeWorkedOutState, null,true, SetNoticeWorkedOutState, true, DeleteNotice);

                            if (ni.workedOutState<0)
                                AppHandler.MainForm.NoticeBar.Text = "Notiz";
                            else
                                AppHandler.MainForm.NoticeBar.Text = "Aufgabe";
                        }
                    }
                }
            }
        }

        private void SaveNotice(string fileName)
        {
            string strFileName = Path.GetFileName(fileName);
            int nId = AppHandler.NoticeManager.Find(strFileName);
            if (nId >= 0)
            {
                var ni = AppHandler.NoticeManager.GetNotice(nId);
                if (ni != null)
                {
                    AppHandler.NoticeManager.SetDirty(nId, true);
                    if (AppHandler.IsClient || AppHandler.IsSingle)
                        AppHandler.CtsClientManager.SendNotice(ni.title);
                    else
                    {
                        AppHandler.CtsServerManager.SendNotice(ni.userName, ni.title);
                        AppHandler.CtsVPNServerManager.SendNotice(ni.userName, ni.title);
                    }
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

        private void SetNoticeWorkedOutState(int iNoticeId,int iState)
        {
            if (iNoticeId >= 0)
            {
                var ni = AppHandler.NoticeManager.GetNotice(iNoticeId);
                if (ni.workedOutState != iState)
                {
                    ni.workedOutState = iState;
                    AppHandler.NoticeManager.Save();
                    AppHandler.CtsServerManager.SendNoticeWorkoutState(ni.title,ni.workedOutState,ni.userName);
                    AppHandler.CtsVPNServerManager.SendNoticeWorkoutState(ni.title, ni.workedOutState, ni.userName);
                }
            }
        }

        private void DeleteNotice(int iNoticeId)
        {
            if (iNoticeId >= 0)
            {
                AppHandler.NoticeManager.DeleteNotice(iNoticeId);
                AppHandler.NoticeManager.Save();
                AppHandler.MainForm.NoticeBar.Visible = false;
            }
        }

        private void UpdateInfoLine(string strText, bool bAndMapProgress=false)
        {
            if (strText.Length>0)
                label1.Text = strText;
            if (bAndMapProgress)
            {
                if (mapProgress>= 0)
                    progressBarControl1.Position = mapProgress;
            }
        }

        private XtraTabPage FindPageByWork(string work)
        {
            // Suche bereits geöffneter Seite
            foreach (XtraTabPage page in xtraTabControl1.TabPages)
            {
                if (TabPageContainsWork(page, work))
                    return page;
            }
            return null;
        }


        private bool TabPageContainsWork(XtraTabPage page, string work)
        {
            string l_work = "";
            if (page.Controls[0] is ContentLearningControl)
                l_work = ((ContentLearningControl) page.Controls[0]).Work;
            else if (page.Controls[0] is ContentWorkoutControl)
                l_work = ((ContentWorkoutControl) page.Controls[0]).Work;
            if (l_work.Length > 0 && l_work == work)
                return true;
            return false;
        }

        private string TabPageFindWork(XtraTabPage page)
        {
            string l_work = "";
            if (page.Controls[0] is ContentLearningControl)
                l_work = ((ContentLearningControl) page.Controls[0]).Work;
            else if (page.Controls[0] is ContentWorkoutControl)
                l_work = ((ContentWorkoutControl) page.Controls[0]).Work;
            return l_work;
        }

        private void FrmContent_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (AppHandler.MainForm.Notice!=null)
                AppHandler.MainForm.Notice.HideTopPanel();
        }

        private void NoticeManager_NoticeManagerEvent(object sender, ref NoticeManagerEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnNoticeManagerHandler(NoticeManager_NoticeManagerEvent), new object[] { sender, ea });
            }
            else
            {
                if (this.eActiveView == ViewMode.Notices)
                    xTreeViewNotices.FillData(mapTitle);
            }
        }

        private void xTreeViewNotices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedNotices();
        }

        private void UserProgressInfoMgr_UserProgressInfoManagerEvent(object sender, EventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnUserProgressInfoManagerHandler(UserProgressInfoMgr_UserProgressInfoManagerEvent), new object[] { sender, ea });
            }
            else
            {
                UpdateInfoLine("");
            }
        }

        /// <summary>
        /// Timer für Fortschrittsanzeigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerMapProgress_Tick(object sender, EventArgs e)
        {
            if ((AppHandler.IsClient || AppHandler.IsSingle) && AppHandler.CtsClientManager.IsRunning())
            {
                // Abfragen am Server absetzen
                AppHandler.CtsClientManager.AskMapProgress(AppHandler.MainForm.ActualUserName, mapTitle);
                AppHandler.CtsClientManager.AskWorkProgress(AppHandler.MainForm.ActualUserName, mapTitle, "all");
            }
            else
            {
                mapProgress = AppHelpers.GetMapProgress(AppHandler.MainForm.ActualUserName, mapTitle);
                UpdateInfoLine("", true);
                strWorkProgress = AppHelpers.GetWorkProgress(AppHandler.MainForm.ActualUserName, mapTitle);
                if (strWorkProgress.Length > 0)
                    AnalyseWorkProgress();
            }

            if (ActiveView is ContentOverviewControl)
            {

            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                ctrlBarPanel.Hide();
                buttonPanel.Show();

                soChart1.Hide();
                btnTests.Show();
                btnResult.Hide();
                btnCertification.Hide();
                btnDelete.Hide();

                string txt = AppHandler.LanguageHandler.GetText("FORMS", "Contentoverview_for_map", "Inhaltsübersicht für Lernmappe");
                UpdateInfoLine(txt + " :" + mapTitle, true);

                SetActiveView((ActivateableUserControl)xtraTabControl1.SelectedTabPage.Controls[0]);
            }
            else
            {
                ctrlBarPanel.Show();
                buttonPanel.Hide();

                SetActiveView((ActivateableUserControl)xtraTabControl1.SelectedTabPage.Controls[0]);
            }
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex > 0)
            {
                string work = TabPageFindWork(xtraTabControl1.SelectedTabPage);
                if (work.Length > 0)
                    RemovePage(work);
            }
        }

        private void xtraTabControl1_CustomHeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;
        }

        private void FrmContent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forcedClose &&
                (e.CloseReason == CloseReason.UserClosing && IsTestActive()))
            {
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "You_have_to_finish_the_test","Sie müssen den Test abschließen!");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

	}

	sealed class ContentViews
	{
        public ContentLearningControl LearningView { get; set; }
        public ContentExamingControl ExamingView { get; set; }
        public ContentTestingControl TestingView { get; set; }

        public ContentViews()
		{
		}
	}
}
