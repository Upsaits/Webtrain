using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Interfaces;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmWorkout.
	/// </summary>
    public abstract partial class ContentWorkoutControl : ActivateableUserControl, IContentNavigationCtrl
	{
		protected string m_work;
		protected FrmContent parentContent=null;
        protected ContentBrowser contentBrowser;
		protected WorkoutInfoItemCollection aWorkouts=null;
		protected QuestionPool questionPool=null;
		protected bool isActive=false;
		protected WorkoutInfoItem activeWorkout=null;
		protected bool isExaming;
		protected int  questionCnt;
		protected bool randomChoose=true;
		protected int  trialCnt=1;
		protected int  successLevel=85;
        protected string questionnaire = "";
	    protected bool testAlwaysAllowed = false;
        protected TestType eTestType = TestType.Final;
        private ResourceHandler rh = null;
        private AppHandler AppHandler = Program.AppHandler;

        #region Properties
        public FrmContent ParentContent
        {
            get { return parentContent; }
        }

        public string Work
        {
            get
            {
                return m_work;
            }
        }

        public bool IsActive
		{
			get
			{
				return isActive;
			}
		}

        public bool HasQuestions
        {
            get 
            {
                return questionPool!=null;
            }
        }

	    public bool IsTestAlwaysAllowed
	    {
	        get { return testAlwaysAllowed; }
	    }

        protected DevExpress.XtraTab.XtraTabControl TabControl
        {
            get { return xtraTabControl1; }
        }

	    #endregion

        abstract protected void GetAllQuestions(ref QuestionCollection aQuestions);

        public ContentWorkoutControl()
		{
			InitializeComponent();
		}

		public ContentWorkoutControl(FrmContent _parentContent,string _work,bool _isExaming,
						  int _questionCnt)
		{
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

            xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            xtraTabControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;

            //xtraTabControl1.AppearancePage.Header.BackColor = Color.Aqua;
            xtraTabControl1.AppearancePage.HeaderActive.BackColor = Color.DarkGray;

            DevExpress.XtraTab.Registrator.PaintStyleCollection.DefaultPaintStyles.Add(new CustomFlatViewInfoRegistrator());
            xtraTabControl1.PaintStyleName = "MyFlat";

			imageList1.Images.Add(rh.GetBitmap("workoutquestion"));
			imageList1.Images.Add(rh.GetBitmap("false"));
			imageList1.Images.Add(rh.GetBitmap("true"));

			m_work = _work;
			isExaming = _isExaming;
			parentContent = _parentContent;
			questionCnt = _questionCnt;

			Text = m_work;

            contentBrowser = new ContentBrowser(axWebBrowser1, this, AppHandler.MainForm.LibOverview, m_work,
                (bIsEnabled) =>
                {
                    if (!activeWorkout.IsWorkedOut)
                        parentContent.CtrlBar.BtnWorkout.Enabled = bIsEnabled;
                });
            contentBrowser.UseType = ContentBrowser.ContentUseType.Question;
            contentBrowser.OnContentChange += contentBrowser_OnContentChange;

            CreateQuestionPool();
		}

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        void contentBrowser_OnContentChange(object sender, ref ContentBrowserEventArgs ea)
        {
        }

	    protected void CreateQuestionPool()
        {
            QuestionCollection aQuestions = new QuestionCollection();

            GetAllQuestions(ref aQuestions);

            if (aQuestions.Count > 0)
            {
                questionPool = new QuestionPool(aQuestions, isExaming);
                aWorkouts = new WorkoutInfoItemCollection();

                ChooseQuestions(!randomChoose);
            }
        }


		protected override void OnResize(EventArgs ea)
		{
			base.OnResize(ea);
		}

		// ShowPage
		// bestimmte HTML-Seite laden
		public void ShowPage()
		{
			if (xtraTabControl1.SelectedTabPageIndex>=0)
            {
                if (isExaming)
                {
                    if (!activeWorkout.IsWorkedOut && activeWorkout.Question.type == "MultipleChoice")
                        parentContent.CtrlBar.BtnWorkout.Enabled = true;
                    else
                        parentContent.CtrlBar.BtnWorkout.Enabled = false;
                }
                else if (!aWorkouts.AreAllWorkedOut())
                {
                    parentContent.CtrlBar.BtnBack.Enabled = true;
                    parentContent.CtrlBar.BtnForward.Enabled = true;
                    parentContent.CtrlBar.BtnWorkout.Enabled = true;
                }
                parentContent.CtrlBar.BtnSolution.Enabled = false;
                contentBrowser.SetQuestion(activeWorkout.Question);
                contentBrowser.ShowPage();
			}
		}


		protected void ChooseQuestions(bool all)
		{
			xtraTabControl1.TabPages.Clear();
			aWorkouts.Clear();

			QuestionCollection	l_aQuestions = new QuestionCollection();

			if (all)
				questionPool.ChooseAll(l_aQuestions);
			else
				questionPool.Choose(l_aQuestions,questionCnt);

			for(int i=0;i<l_aQuestions.Count;++i)
			{
				QuestionItem que=l_aQuestions.Item(i);

				aWorkouts.Add(new WorkoutInfoItem(que,l_aQuestions.GetPath(que),l_aQuestions.GetId(que)));
			
				string sQuestion=AppHandler.LanguageHandler.GetText("FORMS","Question","Frage");
				string txt,name;
				txt = String.Format("{0} {1}",sQuestion,i+1);
				name= String.Format("tabPage{0}",i);

			    var page = new DevExpress.XtraTab.XtraTabPage();
			    page.Text = txt;
			    page.ImageIndex = 0;
				xtraTabControl1.TabPages.Add(page);
			}

			AfterChoosing();
		}

		protected virtual void AfterChoosing()
		{
			xtraTabControl1.SelectedTabPageIndex = 0;
			activeWorkout = aWorkouts.Item(0);
			parentContent.SetChartControlMaxValue(aWorkouts.Count);
		}


        public override void SetActive(bool bIsOn)
		{
            base.SetActive(bIsOn);
			isActive = bIsOn;
			SetActiveChanged();
		}

		protected virtual void SetActiveChanged()
		{
			if (isActive)
			{
				parentContent.CtrlBar.BtnWorkout.Click += new EventHandler(OnBtnWorkout);
				parentContent.ShowChartControl(true);

				if (aWorkouts!=null && aWorkouts.Count>0)
				{
					int workedOut=0,right=0,wrong=0;
					aWorkouts.GetWorkoutResult(ref workedOut,ref right,ref wrong);
					parentContent.SetChartControlValues(workedOut,right,wrong);

                    parentContent.CtrlBar.BtnWorkout.Enabled = false;
                    parentContent.CtrlBar.BtnSolution.Enabled = false;
					
                    ShowPage();
				}
				else
				{
					parentContent.CtrlBar.BtnWorkout.Enabled=false;
					parentContent.CtrlBar.BtnSolution.Enabled=false;
					parentContent.CtrlBar.BtnChoose.Enabled=false;
				}
			}
			else
			{
                axWebBrowser1.Stop();
                parentContent.CtrlBar.BtnWorkout.Click -= new EventHandler(OnBtnWorkout);
				parentContent.ShowChartControl(false);
			}
		}

		protected virtual void DoWorkout()
		{
            if (activeWorkout == null)
                return;

			string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Do_you_want_to_workout_all_questions","Wollen sie alle Fragen ausarbeiten?");
			string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Attention","Achtung");
			if (!isExaming && MessageBox.Show(txt,cap,MessageBoxButtons.YesNo)!=DialogResult.Yes)
				return;

            bool bWasEdited;
            contentBrowser.GetActualSelection(activeWorkout,out bWasEdited);

			if (isExaming)
			{
				if (!activeWorkout.HasAnswer())
				{
					string txt1=AppHandler.LanguageHandler.GetText("MESSAGE","No_answer","Keine Antwort eingegeben!");
					string cap1=AppHandler.LanguageHandler.GetText("SYSTEM","Attention","Achtung");
					MessageBox.Show(txt1,cap1,MessageBoxButtons.OK);
					return;
				}

			}
			else
			{
				for(int i=0;i<aWorkouts.Count;++i)
					if (!aWorkouts.Item(i).WasEdited)
					{
						string txt1=AppHandler.LanguageHandler.GetText("MESSAGE","No_answer_at_question","Bei Frage {0} wurde keine Antwort eingegeben!");
						string cap1=AppHandler.LanguageHandler.GetText("SYSTEM","Attention","Achtung");
						string txt2=String.Format(txt1,i+1);
						MessageBox.Show(txt2,cap1,MessageBoxButtons.OK);
						return;
					}
			}

			parentContent.CtrlBar.BtnWorkout.Enabled = false;
            parentContent.CtrlBar.BtnBack.Enabled = false;
            parentContent.CtrlBar.BtnForward.Enabled = false;

            contentBrowser.DisableAnswers();

			if (isExaming)
			{
				activeWorkout.Workout();
				xtraTabControl1.SelectedTabPage.ImageIndex = activeWorkout.IsRight ? 2:1;
			}
			else
			{
				aWorkouts.WorkoutAll();
                for (int i = 0; i < aWorkouts.Count; ++i)
                {
                    xtraTabControl1.TabPages[i].ImageIndex = aWorkouts.Item(i).IsRight ? 2 : 1;
                    xtraTabControl1.TabPages[i].PageEnabled = true;
                }
			}

			int workedOut=0,right=0,wrong=0;
			aWorkouts.GetWorkoutResult(ref workedOut,ref right,ref wrong);

            if (isExaming)
            {
                aWorkouts.GetWorkoutResult(ref workedOut, ref right, ref wrong, activeWorkout.Path);
                AppHelpers.AddUserProgressInfo(AppHandler.MainForm.ActualUserName, activeWorkout.Path, (int)UserProgressInfoManager.RegionType.Examing, HelperMacros.MAKEWORD(2, (byte)workedOut));
                Debug.WriteLine(String.Format("UserProgressInfo sent for {0}, Region=Examing, Path = {1}, QuestionNr={2} ", AppHandler.MainForm.ActualUserName, activeWorkout.Path, activeWorkout.Id));
            }

			parentContent.SetChartControlValues(workedOut,right,wrong);
		}


		private void OnBtnWorkout(object sender, System.EventArgs e)
		{
			DoWorkout();
		}

	    private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex >= 0)
            {
                var id = xtraTabControl1.SelectedTabPageIndex;
                if (aWorkouts.Item(id).IsWorkedOut)
                {
                    if (aWorkouts.Item(id).IsRight)
                        e.Page.BackColor = System.Drawing.Color.YellowGreen;
                    else
                        e.Page.BackColor = System.Drawing.Color.Maroon;
                }

                if (this.activeWorkout != null)
                {
                    if (!activeWorkout.IsWorkedOut)
                    {
                        bool bWasEdited = false;
                        if (!isExaming && !activeWorkout.IsWorkedOut)
                            contentBrowser.GetActualSelection(activeWorkout, out bWasEdited);

                        if (bWasEdited)
                        {
                            e.PrevPage.PageEnabled = false;
                        }
                    }

                    activeWorkout = aWorkouts.Item(id);
                    ShowPage();
                }
            }
        }

        #region IContentNavigationCtrl implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPageId"></param>
        public void SetPageId(int iPageId)
        {
        }

        public int GetPageId()
        {
            return 1;
        }

        public void SetMaxPages(int iPages)
        {
        }

        public void SetVideo(string strVideoPath)
        {
        }

        public void SetAnimation(string strAnimPath1, string strAnimPath2)
        {
        }

        public void SetGrafic(int iImgId, int iImgCnt)
        {
        }

        public void SetSimulation(bool bIsMill, string strSimulPath1, string strSimulPath2)
        {
        }

        public void AddDocument(string strTitle, int typeId)
        {
        }

        public void ClearDocuments()
        {
        }
        #endregion


    }
}
