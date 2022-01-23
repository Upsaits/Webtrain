using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList.Nodes;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Controls;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmLMEditContentNew : XtraForm
    {
        private enum EditMode { Workings, Tests };
        private enum MoveDirection { Up = -1, Down = 1 };
        private string mapTitle = "";
        private ResourceHandler rh = null;
        private readonly ContentBrowser contentBrowser;
        private readonly LearnmapBuilder learnmapBuilder;
        private EditMode actualEditMode = EditMode.Workings;
        private ListViewItem m_lastMouseOverItem = null;
        private int totalTestQuCnt=0;
        private bool ignoreUpdate = false;
        private int activeTestId = 0;
        private const string c_contentTitle="Inhalt: ";
        private const string c_testTitle="Test: ";
        private AppHandler AppHandler = Program.AppHandler;
        public ContentBrowser ContentBrowser
        {
            get
            {
                return contentBrowser;
            }
        }

        public string MapTitle
        {
            get
            {
                return mapTitle;
            }
        }

        public FrmLMEditContentNew()
        {
            InitializeComponent();
        }

		public FrmLMEditContentNew(string _mapTitle)
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			this.Icon = rh.GetIcon("main");
			this.btnWorkings.Text = AppHandler.LanguageHandler.GetText("FORMS","Contents","Inhalte");
			this.btnTest.Text = AppHandler.LanguageHandler.GetText("FORMS","Tests","Tests");

            mapTitle = _mapTitle;

            bool bIsClassMap = false;
            AppHandler.MapManager.GetUsage(mapTitle, ref bIsClassMap);
            this.rbgMapUsage.EditValue = bIsClassMap ? 1 : 0;

            bool bShowProgressContentOrientated = true;
            AppHandler.MapManager.GetProgressOrientation(mapTitle, ref bShowProgressContentOrientated);
            this.chkShowProgressQuestionOrientated.Checked = !bShowProgressContentOrientated;

            learnmapBuilder = new LearnmapBuilder(mapTitle, AddWorking, AddQuestion);

            contentBrowser = new ContentBrowser(axWebBrowser1, AppHandler.MainForm.LibOverview, AppHandler.MainForm.LibOverview, "",
                (bIsEnabled) => { btnTest.Enabled = bIsEnabled; });
            contentBrowser.OnContentChange += OnContentBrowser;

            AppHandler.MapManager.LearnmapManagerEvent += new OnLearnmapManagerHandler(FrmLMEditContentNew_LearnmapEvent);

            Initialize();
		}

		public void Initialize()
		{
			string sWorkings=AppHandler.LanguageHandler.GetText("FORMS","Workings","Bearbeitungen");
			string sPages=AppHandler.LanguageHandler.GetText("FORMS","Pages","Seiten");
            string sQuestions = AppHandler.LanguageHandler.GetText("FORMS", "Questions", "Fragen");
            string sFromContent = AppHandler.LanguageHandler.GetText("FORMS", "From_content", "Aus Inhalt");
			string sQuestion=AppHandler.LanguageHandler.GetText("FORMS","Question","Frage");

            lvwWorkings.Columns.Add(sWorkings, lvwWorkings.Size.Width * 2 / 3, HorizontalAlignment.Left);
            lvwWorkings.Columns.Add(sPages, lvwWorkings.Size.Width / 6, HorizontalAlignment.Left);
            lvwWorkings.Columns.Add(sQuestions, lvwWorkings.Size.Width / 6, HorizontalAlignment.Left);

            UpdateWorkings();

            lvwQuestions.Columns.Add(sFromContent, lvwQuestions.Size.Width / 2, HorizontalAlignment.Left);
            lvwQuestions.Columns.Add(sQuestion, lvwQuestions.Size.Width / 2, HorizontalAlignment.Left);

		    UpdateTests();
           
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
                AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = false;
                AppHandler.MainForm.LibOverview.BtnBack.Click -= OnBtnBack;
                AppHandler.MainForm.LibOverview.BtnForward.Click -= OnBtnForward;
                AppHandler.MainForm.LibOverviewBar.Visible = false;
                AppHandler.MainForm.LibOverviewBar.Update();
                AppHandler.MainForm.CloseBar.Visible = false;
            }
            else
            {
                AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = true;
                AppHandler.MainForm.LibOverview.ContentTree.LearnmapName = this.mapTitle;
                AppHandler.MainForm.LibOverview.ContentTree.UseType = ContentTreeViewList.UseType.FullContent;
                AppHandler.MainForm.LibOverview.UpdateView();
                AppHandler.MainForm.LibOverview.NavigationPanel.Visible = true;
                AppHandler.MainForm.LibOverview.BtnBack.Click += OnBtnBack;
                AppHandler.MainForm.LibOverview.BtnForward.Click += OnBtnForward;
                AppHandler.MainForm.LibOverviewBar.Visible = true;
                AppHandler.MainForm.LibOverviewBar.Update();
                AppHandler.MainForm.CloseBar.Visible = true;
            }
        }

		private void SwitchEditMode(EditMode tEdit)
		{
			if (tEdit==EditMode.Workings)
			{
				this.panWorkingBar.Dock = DockStyle.Fill;
				this.panWorkingBar.Visible=true;
				this.btnTest.Visible=true;
				this.panTestBar.Visible =false;
				this.btnWorkings.Visible=false;
                this.rbgMapUsage.Visible = true;

                AppHandler.MainForm.LibOverview.ContentTree.UseType = ContentTreeViewList.UseType.FullContent;
                AppHandler.MainForm.LibOverview.ContentTree.FillTree();
                ContentBrowser.UseType = TrainConcept.ContentBrowser.ContentUseType.LearningPage;

                if (lvwWorkings.Items.Count > 0)
                {
                    lvwWorkings.HideSelection = false;
                    lvwWorkings.Select();
                    SelectWorking(0);
                }
			}
			else
			{
                this.panWorkingBar.Visible = false;
                this.btnTest.Visible = false;
                this.panTestBar.Visible = true;
                this.panTestBar.Dock = DockStyle.Fill;
                this.btnWorkings.Visible = true;
                this.rbgMapUsage.Visible = false;

                AppHandler.MainForm.LibOverview.ContentTree.UseType = ContentTreeViewList.UseType.QuestionContent;
                AppHandler.MainForm.LibOverview.ContentTree.FillTree();
                ContentBrowser.UseType = TrainConcept.ContentBrowser.ContentUseType.Question;
			}

            actualEditMode = tEdit;
		}

        private void SelectWorking(int id)
        {
            lvwWorkings.Items[id].Focused = true;
            lvwWorkings.Items[id].Selected = true;
            this.Invoke((MethodInvoker)delegate
            {
                lvwWorkings_DoubleClick(lvwWorkings, new EventArgs());
            });
        }

        protected override void OnResize(EventArgs ea)
		{
			base.OnResize(ea);

			this.axWebBrowser1.Size = new Size(ClientSize.Width,ClientSize.Height/2);

			if (lvwWorkings.Columns.Count>0)
			{
				lvwWorkings.Columns[0].Width = ClientSize.Width * 2/3;
				lvwWorkings.Columns[1].Width = ClientSize.Width / 6;
                lvwWorkings.Columns[1].Width = ClientSize.Width / 6;
            }

			if (lvwQuestions.Columns.Count>0)
			{
				lvwQuestions.Columns[0].Width = panel1.ClientSize.Width/2;
				lvwQuestions.Columns[1].Width = panel1.ClientSize.Width/2;
			}
		}

		private TreeListNode GetDragNode(IDataObject data)
		{
			return data.GetData(typeof(TreeListNode)) as DevExpress.XtraTreeList.Nodes.TreeListNode;
		}

        private void UpdateWorkings()
        {
            lvwWorkings.Items.Clear();
            string[] aWorkings = null;
            string[] aTests = null;
            if (AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings,false) &&
                AppHandler.MapManager.GetWorkingTests(mapTitle, ref aTests))
                for(int i=0;i<aWorkings.Length;++i)
                {
                    if (aWorkings[i].Length > 0)
                        AddWorking(aWorkings[i]);
                    else
                        AddTest(aTests[i]);
                }
        }
        
		private void AddWorking(string working)
		{
			int pageCnt;
            int quCnt;
			bool isOk = AppHandler.LibManager.GetPageCnt(working,out pageCnt);
            AppHandler.LibManager.GetQuestionCnt(working, out quCnt);
            ListViewItem lvi = new ListViewItem(c_contentTitle + working);
			lvi.SubItems.Add(pageCnt.ToString());
            lvi.SubItems.Add(quCnt.ToString());

            // Creates wrapper to provide SuperTooltip control access to the node object
            ListViewSuperTooltipProvider sp = new ListViewSuperTooltipProvider(lvi);
            lvi.Tag = sp;
            // Assign the wrapper to SuperTooltip control together with information
            // about what to display on Super Tooltip for this node.
            superTooltip1.SetSuperTooltip(sp,
                new DevComponents.DotNetBar.SuperTooltipInfo(lvi.Text, "",
                "Doppelklicken Sie mit der linken Maustaste um den Inhalt anzuzeigen,\n\r",
                null, null, DevComponents.DotNetBar.eTooltipColor.Lemon));

            lvwWorkings.Items.Add(lvi);

            int testQuCnt = 0;
            if (AppHandler.LibManager.GetQuestionCnt(working, out testQuCnt, true))
                totalTestQuCnt += testQuCnt;

            axWebBrowser1.Visible = lvwWorkings.Items.Count > 0;
            //lvwWorkings.HideSelection = false;
            //lvwWorkings.Select();
            //SelectWorking(lvwWorkings.Items.Count - 1);

		}

        private void AddTest(string test)
        {
            ListViewItem lvi = new ListViewItem(c_testTitle +test);
            lvi.SubItems.Add("");

            // Creates wrapper to provide SuperTooltip control access to the node object
            ListViewSuperTooltipProvider sp = new ListViewSuperTooltipProvider(lvi);
            lvi.Tag = sp;
            // Assign the wrapper to SuperTooltip control together with information
            // about what to display on Super Tooltip for this node.
            superTooltip1.SetSuperTooltip(sp,
                new DevComponents.DotNetBar.SuperTooltipInfo(lvi.Text, "",
                    "Der Test wird an dieser Stelle ausgeführt\n\r",
                    null, null, DevComponents.DotNetBar.eTooltipColor.Lemon));

            lvwWorkings.Items.Add(lvi);
        }

        private void AddQuestion(string strPath,int quId,string strQuestion)
        {
            var lvi = new ListViewItem();
            lvi.Text = strPath;
            var lvsi= new ListViewItem.ListViewSubItem();
            lvsi.Tag = quId;
            lvsi.Text = strQuestion;
            lvi.SubItems.Add(lvsi);

            // Creates wrapper to provide SuperTooltip control access to the node object
            var sp = new ListViewSuperTooltipProvider(lvi);
            lvi.Tag = sp;
            // Assign the wrapper to SuperTooltip control together with information
            // about what to display on Super Tooltip for this node.
            superTooltip2.SetSuperTooltip(sp,
                new DevComponents.DotNetBar.SuperTooltipInfo("Frage: " + lvsi.Text, "",
                "Doppelklicken Sie mit der linken Maustaste um die Frage anzuzeigen,\n\r",
                null, null, DevComponents.DotNetBar.eTooltipColor.Lemon));

            this.lvwQuestions.Items.Add(lvi);

            if (panTestAdjustment.QuestionCount < this.lvwQuestions.Items.Count)
                panTestAdjustment.QuestionCountText = lvwQuestions.Items.Count.ToString();
        }

        private void InitTooltips()
        {
            // Load SuperTooltip information for each node...
            foreach (ListViewItem i in lvwWorkings.Items)
            {
                // Creates wrapper to provide SuperTooltip control access to the node object
                ListViewSuperTooltipProvider sp = new ListViewSuperTooltipProvider(i);
                i.Tag = sp;
                // Assign the wrapper to SuperTooltip control together with information
                // about what to display on Super Tooltip for this node.
                superTooltip1.SetSuperTooltip(sp,
                    new DevComponents.DotNetBar.SuperTooltipInfo("Inhalt: " + i.Text, "",
                    "Doppelklicken Sie mit der linken Maustaste um den Inhalt anzuzeigen,\n\r",
                    null, null, DevComponents.DotNetBar.eTooltipColor.Lemon));
            }

            // Load SuperTooltip information for each node...
            foreach (ListViewItem i in lvwQuestions.Items)
            {
                // Creates wrapper to provide SuperTooltip control access to the node object
                ListViewSuperTooltipProvider sp = new ListViewSuperTooltipProvider(i);
                i.Tag = sp;
                // Assign the wrapper to SuperTooltip control together with information
                // about what to display on Super Tooltip for this node.
                superTooltip2.SetSuperTooltip(sp,
                    new DevComponents.DotNetBar.SuperTooltipInfo("Frage: " + i.SubItems[1].Text, "",
                    "Doppelklicken Sie mit der linken Maustaste um die Frage anzuzeigen,\n\r",
                    null, null, DevComponents.DotNetBar.eTooltipColor.Lemon));
            }

        }

        private void ShowListViewTooltip(ListViewItem item)
        {
            if (item == null)
                return;

            var sp = item.Tag as ListViewSuperTooltipProvider;
            sp.Show();
            m_lastMouseOverItem = item;
        }

        private void HideListViewTooltip()
        {
            if (m_lastMouseOverItem != null)
            {
                ListViewSuperTooltipProvider sp = m_lastMouseOverItem.Tag as ListViewSuperTooltipProvider;
                sp.Hide();
                m_lastMouseOverItem = null;
            }
        }

        private static void MoveListViewItems(ListView sender, MoveDirection direction)
        {
            int dir = (int)direction;
            int opp = dir * -1;

            bool valid = sender.SelectedItems.Count > 0 &&
                            ((direction == MoveDirection.Down && (sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1))
                        || (direction == MoveDirection.Up && (sender.SelectedItems[0].Index > 0)));

            if (valid)
            {
                foreach (ListViewItem item in sender.SelectedItems)
                {
                    int index = item.Index + dir;
                    sender.Items.RemoveAt(item.Index);
                    sender.Items.Insert(index, item);

                    sender.Items[index + opp].SubItems[1].Text = (index + opp).ToString();
                    item.SubItems[1].Text = (index).ToString();
                }
            }
        }

        private void OnBtnBack(object sender, System.EventArgs e)
        {
            contentBrowser.OnBtnBack();
        }

        private void OnBtnForward(object sender, System.EventArgs e)
        {
            contentBrowser.OnBtnForward();
        }

        private void OnContentBrowser(object sender, ref ContentBrowserEventArgs ea)
        {
        }

        public bool OnLibOverviewFocusNodeChange(string strNewPath, int iSubId)
        {
            if (actualEditMode == EditMode.Workings)
            {
                lvwWorkings.SelectedItems.Clear();
                try
                {
                    var result = FindWorkItem(strNewPath);
                    if (result != null)
                    {
                        lvwWorkings.FocusedItem = result;
                        result.Selected = true;
                        lvwWorkings.Select();
                        System.Threading.Thread.Sleep(250);
                    }
                }
                catch (System.Exception /*ex*/)
                {

                }

                if (actualEditMode == EditMode.Workings)
                {
                    ContentBrowser.SetWork(strNewPath);
                    ContentBrowser.ShowPage();
                }
            }
            else
            {
                lvwQuestions.SelectedItems.Clear();
                try
                {
                    ListViewItem result = null;
                    foreach(ListViewItem lvi in lvwQuestions.Items)
                    {
                        if (lvi.Text == strNewPath && (int)lvi.SubItems[1].Tag == iSubId)
                        {
                            result = lvi;
                            break;
                        }
                    }

                    if (result != null)
                    {
                        lvwQuestions.FocusedItem = result;
                        result.Selected = true;
                        lvwQuestions.Select();
                        System.Threading.Thread.Sleep(250);
                    }
                }
                catch (System.Exception /*ex*/)
                {

                }

                if (actualEditMode == EditMode.Tests)
                {
                    QuestionItem que = AppHandler.LibManager.GetQuestion(strNewPath,iSubId);
                    if (que!=null)
                    {
                        ContentBrowser.SetWork(strNewPath);
                        ContentBrowser.SetQuestion(que);
                        ContentBrowser.ShowPage();
                    }
                }
            }
            return true;
        }


        private void UpdateTests()
        {
            try
            {
                while(tabCtrlTests.TabPages.Count>1)
                    tabCtrlTests.TabPages.RemoveAt(tabCtrlTests.TabPages.Count-1);
            }
            catch (System.ArgumentOutOfRangeException /*ex*/)
            {
            }

            TestItem ti;
            int i = 0;
            do
            {
                ti = AppHandler.MapManager.GetTest(mapTitle, i);
                if (i>0 && ti != null)
                {
                    var tabPage = new XtraTabPage();
                    tabPage.Text = ti.title;
                    tabPage.Size = new System.Drawing.Size(826, 88);
                    tabPage.Controls.Add(panTestTabPage);
                    tabCtrlTests.TabPages.Add(tabPage);
                }
                ++i;
            } while (ti != null);

            activeTestId = 0;
            SetActiveTestInfo();
        }

        private void SetActiveTestInfo()
        {
            var ti = AppHandler.MapManager.GetTest(mapTitle, activeTestId);
            if (ti != null)
            {
                bool randomChoose = false;
                int questionCnt = 0;
                int trialCnt = 0;
                int successLevel = 0;
                bool testAlwaysAllowed = false;
                ti = AppHandler.MapManager.GetTest(mapTitle, activeTestId);
                if (ti != null)
                {
                    randomChoose = ti.randomChoose;
                    questionCnt = ti.questionCount;
                    trialCnt = ti.trialCount;
                    successLevel = ti.successLevel;
                    testAlwaysAllowed = ti.testAlwaysAllowed;
                }

                if (randomChoose && (totalTestQuCnt < questionCnt))
                    questionCnt = totalTestQuCnt;

                panTestAdjustment.LearnmapTitle = mapTitle;
                panTestAdjustment.TestId = activeTestId;
                panTestAdjustment.QuestionCountText = questionCnt.ToString();
                panTestAdjustment.SuccessLevelText = successLevel.ToString();
                panTestAdjustment.TrialCntText = trialCnt.ToString();
                panTestAdjustment.TestAlwaysAllowed = testAlwaysAllowed;

                TestQuestionItem[] aItems = null;
                lvwQuestions.Items.Clear();
                if (AppHandler.MapManager.GetTestQuestions(mapTitle, activeTestId, ref aItems))
                    foreach (var t in aItems)
                    {
                        QuestionItem que = AppHandler.LibManager.GetQuestion(t.contentPath, t.questionId);
                        if (que != null)
                            AddQuestion(t.contentPath, t.questionId, que.question);
                    }

                panTestAdjustment.ListViewQuestions = lvwQuestions;

                if (randomChoose)
                    panTestAdjustment.QuestionChooseChecked = true;
                else
                    panTestAdjustment.QuestionSelectChecked = true;

                tabCtrlTests.TabPages[activeTestId].Controls.Clear();
                tabCtrlTests.TabPages[activeTestId].Controls.Add(this.panTestTabPage);
                tabCtrlTests.Update();
            }
        }


        private void CreateNewTest(string strTestname)
        {
            int iTestCnt = AppHandler.MapManager.GetTestCount(mapTitle);
            var ti = new TestItem();
            if (AppHandler.MapManager.SetTest(mapTitle, iTestCnt, strTestname, ti.randomChoose, ti.questionCount,
                ti.trialCount, ti.successLevel, ti.questionnare, ti.testAlwaysAllowed, TestType.Intermediate))
                UpdateTests();
        }

        private void DeleteTest(int iTestId)
        {
            var ti = AppHandler.MapManager.GetTest(mapTitle, iTestId);
            if (ti != null)
            {
                AppHandler.MapManager.DeleteTest(mapTitle, iTestId);
                UpdateTests();
            }
        }

        private ListViewItem FindWorkItem(string strWork)
        {
            string strSearchTxt = c_contentTitle + strWork;
            var result = lvwWorkings.Items.Cast<ListViewItem>().Single(x => x.Text == strSearchTxt);
            return result;
        }

        private ListViewItem FindTestItem(string strWork)
        {
            string strSearchTxt = c_testTitle + strWork;
            var result = lvwWorkings.Items.Cast<ListViewItem>().Single(x => x.Text == strSearchTxt);
            return result;
        }

        private void lvwWorkings_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedIndices.Count > 0)
            {
                ListViewItem lvi = lv.SelectedItems[0];
                if (lvi != null && lvi.Text.Contains(c_contentTitle))
                {
                    contentBrowser.SetWork(lvi.Text.Substring(c_contentTitle.Length));
                    contentBrowser.ShowPage();
                }
            }
        }


        private void lvwWorkings_DragDrop(object sender, DragEventArgs e)
        {
            ListView lv = sender as ListView;
            TreeListNode node = GetDragNode(e.Data);
            if (node != null)
            {
                string dragString = AppHandler.MainForm.LibOverview.ContentTree.ContentList.GetPath(node.Id);

                string[] aTitles = null;
                Utilities.SplitPath(dragString, out aTitles);

                var lib = AppHandler.LibManager.GetLibrary(aTitles[0]);

                if (aTitles.Length == 4)
                {
                    this.learnmapBuilder.AddPoint(dragString,lib.version);
                }
                else if (aTitles.Length == 3)
                {
                    this.learnmapBuilder.AddChapter(dragString, lib.version);
                }
                else if (aTitles.Length == 2)
                {
                    this.learnmapBuilder.AddBook(dragString, lib.version);
                }
                else if (aTitles.Length == 1)
                {
                    this.learnmapBuilder.AddLibrary(dragString, lib.version);
                }
            }
        }

        private void lvwWorkings_DragEnter(object sender, DragEventArgs e)
        {
            TreeListNode node = GetDragNode(e.Data);
            if (node != null)
                e.Effect = DragDropEffects.Copy;
        }

        private void lvwWorkings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ListView lv = (ListView)sender;
                if (lv.SelectedIndices.Count > 0)
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_delete_selected_items", "Wollen Sie alle markierten Einträge löschen?");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ignoreUpdate = true;
                        for (int i = lv.SelectedIndices.Count; i > 0; --i)
                        {
                            ListViewItem lvi = lv.SelectedItems[i - 1];
                            lv.Items.Remove(lvi);

                            if (lvi.Text.Contains(c_contentTitle))
                            {
                                string work=lvi.Text.Substring(c_contentTitle.Length);
                                AppHandler.MapManager.DeleteWorking(mapTitle, work);
                                AppHandler.UserProgressInfoMgr.DeleteProgressInfoOfWork(work);

                                int testQuCnt = 0;
                                if (AppHandler.LibManager.GetQuestionCnt(work, out testQuCnt, true))
                                    totalTestQuCnt -= testQuCnt;

                                axWebBrowser1.Visible = lvwWorkings.Items.Count > 0;
                            }
                            else
                            {
                                string strTestname=lvi.Text.Substring(c_testTitle.Length);
                                AppHandler.MapManager.DeleteWorking(mapTitle, "",strTestname);
                                var aTRIs = new TestResultItemCollection();
                                AppHandler.TestResultManager.Find(ref aTRIs, AppHandler.MainForm.ActualUserName, mapTitle,strTestname);
                                if (aTRIs.Count > 0)
                                    for (int j = 0; j < aTRIs.Count; ++j)
                                        AppHandler.TestResultManager.Delete(AppHandler.MainForm.ActualUserName, mapTitle, strTestname, j);
                                AppHandler.MainForm.ControlCenter.UpdateWorkoutLearnmapState(mapTitle);
                            }
                        }

                        AppHandler.MapManager.Save(mapTitle);
                        AppHandler.ContentManager.CloseLearnmap(mapTitle);
                        Activate();
                        ignoreUpdate = false;
                    }
                }
            }
        }

        private void lvwWorkings_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem nodeAt = lvwWorkings.GetItemAt(e.X, e.Y);
            if (nodeAt != m_lastMouseOverItem)
            {
                HideListViewTooltip();
                if (nodeAt != null)
                {
                    m_lastMouseOverItem = nodeAt;
                    // Delayed display
                    tooltipDisplayDelay.Start();
                }
            }
        }

        private void lvwWorkings_MouseLeave(object sender, EventArgs e)
        {
            // Hide tooltip when mouse leaves tree control
            HideListViewTooltip();
            tooltipDisplayDelay.Stop();
        }

        private void lvwWorkings_MouseDown(object sender, MouseEventArgs e)
        {
            // Hide tooltip if any is visible...
            HideListViewTooltip();
        }

        private void lvwWorkings_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lv = sender as ListView;
            int iCnt = AppHandler.MapManager.GetTestCount(this.mapTitle);
            if (lv != null && iCnt > 0 && lv.SelectedIndices.Count==1 && lv.Items.Count>1)
            {
                var lvi = lv.SelectedItems[0];
                if (lvi.Text.Contains(c_contentTitle))
                {
                    btnInsertTest.Enabled = true;
                    return;
                }
            }

            btnInsertTest.Enabled = false;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (totalTestQuCnt == 0)
            {
                string txt=AppHandler.LanguageHandler.GetText("MESSAGE","no_questions_in_this_learnmap","Diese Mappe enthält keine Testfragen!");
                string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
                MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            SwitchEditMode(EditMode.Tests);
        }

        private void btnWorkings_Click(object sender, EventArgs e)
        {
            if (actualEditMode == EditMode.Tests)
            {
                if (panTestAdjustment.QuestionSelectChecked)
                {
                    if (this.lvwQuestions.Items.Count<panTestAdjustment.QuestionCount)
                    {
                        string txt=AppHandler.LanguageHandler.GetText("MESSAGE","QuestionCount_not_achieved","Fragenanzahl noch nicht erreicht!");
                        string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
                        MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                panTestAdjustment.SaveTestValues();
            }
            SwitchEditMode(EditMode.Workings);
        }


        private void lvwQuestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ListView lv = (ListView)sender;
                if (lv.SelectedIndices.Count > 0)
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_delete_selected_items", "Wollen Sie alle markierten Einträge löschen?");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = lv.SelectedIndices.Count; i > 0; --i)
                        {
                            ListViewItem lvi = lv.SelectedItems[i - 1];
                            AppHandler.MapManager.DeleteTestQuestion(mapTitle,0, lvi.Text, (int)lvi.SubItems[1].Tag);
                            lv.Items.Remove(lvi);
                        }

                        AppHandler.MapManager.Save(mapTitle);
                        AppHandler.ContentManager.CloseLearnmap(mapTitle);
                    }
                }
            }
        }

        private void lvwQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lvwQuestions_DoubleClick(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedIndices.Count > 0)
            {
                ListViewItem lvi = lv.SelectedItems[0];
                if (lvi != null)
                {
                    var lvsi = lvi.SubItems[1];
                    AppHandler.MainForm.LibOverview.SetActualPoint(lvi.Text, (int)lvsi.Tag);
                }
            }
        }

        private void lvwQuestions_DragDrop(object sender, DragEventArgs e)
        {
            ListView lv = sender as ListView;
            TreeListNode node = GetDragNode(e.Data);
            if (node != null)
            {
                string contentPath = AppHandler.MainForm.LibOverview.ContentTree.ContentList.GetPath(node.ParentNode.Id);
                ContentTreeViewRecord rec=(ContentTreeViewRecord)AppHandler.MainForm.LibOverview.ContentTree.ContentList[node.Id];
                if (rec!=null)
                    this.learnmapBuilder.AddTestQuestion(contentPath,activeTestId,rec.GetQuestionId());
            }
        }

        private void lvwQuestions_DragEnter(object sender, DragEventArgs e)
        {
            TreeListNode node = GetDragNode(e.Data);
            if (node != null)
            {
                string contentPath = AppHandler.MainForm.LibOverview.ContentTree.ContentList.GetPath(node.ParentNode.Id);
                ContentTreeViewRecord rec=(ContentTreeViewRecord)AppHandler.MainForm.LibOverview.ContentTree.ContentList[node.Id];
                if (rec != null && rec.GetType()== ContentTreeViewRecordType.Question)
                {
                    e.Effect = DragDropEffects.Copy;
                }

            }
        }

        private void lvwQuestions_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem nodeAt = lvwQuestions.GetItemAt(e.X, e.Y);
            if (nodeAt != m_lastMouseOverItem)
            {
                HideListViewTooltip();
                if (nodeAt != null)
                {
                    m_lastMouseOverItem = nodeAt;
                    // Delayed display
                    tooltipDisplayDelay.Start();
                }
            }
        }

        private void lvwQuestions_MouseLeave(object sender, EventArgs e)
        {
            // Hide tooltip when mouse leaves tree control
            HideListViewTooltip();
            tooltipDisplayDelay.Stop();
        }

        private void lvwQuestions_MouseDown(object sender, MouseEventArgs e)
        {
            // Hide tooltip if any is visible...
            HideListViewTooltip();
        }

        private void rbnMapUsage_EditValueChanged(object sender, EventArgs e)
        {
            if (learnmapBuilder != null)
            {
                bool bIsClassMap = (int)this.rbgMapUsage.EditValue != 0;
                this.learnmapBuilder.SetUsage(bIsClassMap);
                if (!bIsClassMap)
                {
                    string[] astrClassNames;
                    AppHandler.ClassManager.GetClassNames(out astrClassNames);
                    if (astrClassNames != null)
                        foreach (string strClass in astrClassNames)
                            if (AppHandler.ClassManager.HasLearnmap(strClass, mapTitle))
                                AppHelpers.RemoveMapFromClass(strClass, mapTitle);
                }
                else
                {
                    AppHandler.MapManager.SetUsers(mapTitle, null);
                    AppHandler.MapManager.Save(mapTitle);
                }
            }
        }

        private void rbgMapUsage_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (learnmapBuilder != null)
            {
                bool bWasClassMap = false;
                AppHandler.MapManager.GetUsage(mapTitle, ref bWasClassMap);
                string txt;
                if (bWasClassMap)
                    txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_switch_to_usermap", "Bei diesem Vorgang werden alle aktuellen Klassen von der Mappe abgemeldet\nWollen Sie wirklich wechseln?");
                else
                    txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_switch_to_classmap", "Bei diesem Vorgang werden alle aktuellen Benutzer von der Mappe abgemeldet\nWollen Sie wirklich wechseln?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private void tooltipDisplayDelay_Tick(object sender, EventArgs e)
        {
            tooltipDisplayDelay.Stop();
            if (m_lastMouseOverItem != null)
                ShowListViewTooltip(m_lastMouseOverItem);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lvwWorkings.SelectedIndices.Count > 0)
            {
                ListViewItem lvi = lvwWorkings.SelectedItems[0];
                if (lvi != null)
                {
                    if (lvi.Text.Contains(c_testTitle))
                    {
                        if ((lvi.Index - 2) >= 0)
                        {
                            var lviPrev = lvwWorkings.Items[lvi.Index - 2];
                            if (lviPrev.Text.Contains(c_testTitle))
                            {
                                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Its_not_allowed_to_have_consecutive_questions", "Aufeinander folgende Tests sind nicht erlaubt!");
                                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    else if (lvi.Index == (lvwWorkings.Items.Count - 1) &&
                             lvwWorkings.Items[lvwWorkings.Items.Count - 2].Text.Contains(c_testTitle))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE",
                            "tests_must_not_be_at_the_end",
                            "Zwischentests sind als Endtest nicht erlaubt!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    AppHandler.MapManager.MoveWorking(mapTitle, lvi.Index, -1);
                    MoveListViewItems(lvwWorkings, MoveDirection.Up);
                    AppHandler.ContentManager.CloseLearnmap(mapTitle);
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lvwWorkings.SelectedIndices.Count > 0)
            {
                bool bIgnore = false;
                ListViewItem lvi = lvwWorkings.SelectedItems[0];
                if (lvi != null)
                {
                    if (lvi.Text.Contains(c_testTitle))
                    {
                        if ((lvi.Index + 2) < lvwWorkings.Items.Count)
                        {
                            var lviNext = lvwWorkings.Items[lvi.Index + 2];
                            if (lviNext.Text.Contains(c_testTitle))
                            {
                                string txt = AppHandler.LanguageHandler.GetText("MESSAGE",
                                    "Its_not_allowed_to_have_consecutive_tests",
                                    "Aufeinander folgende Tests sind nicht erlaubt!");
                                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                bIgnore = true;
                            }
                        }
                        else
                        {
                            string txt = AppHandler.LanguageHandler.GetText("MESSAGE",
                                "tests_must_not_be_at_the_end",
                                "Zwischentests sind als Endtest nicht erlaubt!");
                            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            bIgnore = true;
                        }
                    }
                    else if (lvi.Index == (lvwWorkings.Items.Count - 1) &&
                             lvwWorkings.Items[lvwWorkings.Items.Count - 1].Text.Contains(c_testTitle))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE",
                            "tests_must_not_be_at_the_end",
                            "Zwischentests sind als Endtest nicht erlaubt!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        bIgnore = true;
                    }


                    if (!bIgnore)
                    {
                        AppHandler.MapManager.MoveWorking(mapTitle, lvi.Index, +1);
                        MoveListViewItems(lvwWorkings, MoveDirection.Down);
                        AppHandler.ContentManager.CloseLearnmap(mapTitle);
                    }
                }
            }
        }

        private void FrmLMEditContentNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            AfterActivation(false);
            AppHandler.ContentManager.LearnmapEditorClosed(mapTitle);
        }

        private void FrmLMEditContentNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.actualEditMode == EditMode.Tests)
            {
                if (panTestAdjustment.QuestionSelectChecked)
                {
                    if (this.lvwQuestions.Items.Count < panTestAdjustment.QuestionCount)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "QuestionCount_not_achieved", "Fragenanzahl noch nicht erreicht!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    if (panTestAdjustment.QuestionCount > totalTestQuCnt)
                    {
                        //string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_delete_selected_items", "Wollen Sie alle markierten Einträge löschen?");
                        //string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        //if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        AppHandler.ContentManager.LearnmapEditorClosed(mapTitle);
                        return;
                    }
                }
                panTestAdjustment.SaveTestValues();
            }
        }

        private void FrmLMEditContentNew_LearnmapEvent(object sender, ref LearnmapManagerEventArgs ea)
        {
            if (ignoreUpdate)
                return;
            if (ea.MapName == mapTitle && ea.Command == LearnmapManagerEventArgs.CommandType.DeleteWorking)
                UpdateWorkings();
        }

        private void btnNewTest_Click(object sender, EventArgs e)
        {
            var dlg=new FrmNewTest(mapTitle);
            if (dlg.ShowDialog() == DialogResult.OK)
                CreateNewTest(dlg.Title);
        }

        private void btnInsertTest_Click(object sender, EventArgs e)
        {
            if (lvwWorkings.SelectedIndices.Count == 1)
            {
                ListViewItem lvi = lvwWorkings.SelectedItems[0];
                if (lvi != null)
                {
                    var frm = new XFrmChooseTest(mapTitle);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        AppHandler.MapManager.AddWorking(mapTitle, "", frm.Testname,"");
                        AppHandler.MapManager.MoveWorking(mapTitle, lvwWorkings.Items.Count, lvi.Index - lvwWorkings.Items.Count);
                        UpdateWorkings();
                    }
                }
            }
        }

        private void btnDeleteTest_Click(object sender, EventArgs e)
        {
            string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_delete_selected_test", "Wollen Sie den ausgewählten Test löschen?");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteTest(activeTestId);
            }
        }

        private void tabCtrlTests1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (tabCtrlTests.SelectedTabPageIndex >= 0)
            {
                activeTestId = tabCtrlTests.SelectedTabPageIndex;
                SetActiveTestInfo();
                btnDeleteTest.Enabled= (activeTestId > 0);
            }
        }

        private void chkShowProgressQuestionOrientated_CheckedChanged(object sender, EventArgs e)
        {
            if (learnmapBuilder != null)
            {
                bool bIsContentOrientated = !chkShowProgressQuestionOrientated.Checked;
                learnmapBuilder.SetProgressOrientation(bIsContentOrientated);
            }
        }

        private void FrmLMEditContentNew_Load(object sender, EventArgs e)
        {
            SwitchEditMode(EditMode.Workings);
        }



    }
}
