using System;
using System.Diagnostics;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmContentTesting.
	/// </summary>
	public class ContentTestingControl : ContentWorkoutControl
	{
	    private int testId=0;
	    private string testName = "";
	    private bool bLastTestSucceeded = false;
        private AppHandler AppHandler = Program.AppHandler;
        public int TestId
        {
            get { return testId; }
        }

	    public string TestName
        {
            get { return testName; }
        }

	    public bool IsWorkedOut
		{
			get { return aWorkouts.IsWorkedOut(); }
		}

        public bool IsWorkedOutCorrect
        {
            get { return bLastTestSucceeded; }
        }
			
		public ContentTestingControl(FrmContent _parentContent,string _work) : base(_parentContent,_work,false,10)
		{
		}

        protected override void GetAllQuestions(ref QuestionCollection aQuestions)
        {
            // get test type 
            if (AppHandler.MapManager.GetTest(parentContent.MapTitle, testId, ref randomChoose, ref questionCnt, ref trialCnt, 
                                              ref successLevel, ref questionnaire, ref testAlwaysAllowed,ref eTestType)
                && !randomChoose)
            {
                TestQuestionItem[] aItems = null;
                if (AppHandler.MapManager.GetTestQuestions(parentContent.MapTitle, testId, ref aItems))
                    for (int i = 0; i < aItems.Length; ++i)
                    {
                        QuestionItem que = AppHandler.LibManager.GetQuestion(aItems[i].contentPath, aItems[i].questionId);
                        if (que != null)
                            aQuestions.Add(que, aItems[i].contentPath, aItems[i].questionId);
                    }
            }

            if (randomChoose)
            {
                string[] aWorkings = null;
                AppHandler.MapManager.GetWorkings(parentContent.MapTitle, ref aWorkings);
                for (int i = 0; i < aWorkings.Length; ++i)
                    AppHandler.LibManager.GetQuestions(aWorkings[i], ref aQuestions, false, true);
            }

        }

		protected override void SetActiveChanged()
		{
    		base.SetActiveChanged();
		    if (isActive)
		    {
                AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = false;
                AppHandler.MainForm.LibOverviewBar.Visible = false;
                parentContent.CtrlBar.BtnBack.Click += BtnBack_Click;
                parentContent.CtrlBar.BtnForward.Click += BtnForward_Click;
		    }
            else
            {
                parentContent.CtrlBar.BtnBack.Click -= BtnBack_Click;
                parentContent.CtrlBar.BtnForward.Click -= BtnForward_Click;
            }
		}

        void BtnForward_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTabPageIndex < (TabControl.TabPages.Count-1))
                TabControl.SelectedTabPage = TabControl.TabPages[TabControl.SelectedTabPageIndex + 1];
        }

        void BtnBack_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTabPageIndex>0)
                TabControl.SelectedTabPage = TabControl.TabPages[TabControl.SelectedTabPageIndex - 1];
        }
			
		protected override void DoWorkout()
		{
			base.DoWorkout();

			if (aWorkouts.IsWorkedOut())
			{
                var ti = AppHandler.MapManager.GetTest(parentContent.MapTitle, testId);
				int newId = AppHandler.TestResultManager.End(AppHandler.MainForm.ActualUserName,parentContent.MapTitle,ti.title,DateTime.Now,aWorkouts);
			    Debug.Assert(newId >= 0);
			    if (newId >= 0)
			    {
                    TestResultItem tri = AppHandler.TestResultManager.Get(newId);

                    bLastTestSucceeded = ((int)tri.percRight)>=ti.successLevel;

                    if (AppHandler.IsClient || AppHandler.IsSingle)
                    {
                        AppHandler.CtsClientManager.SendTestResult(tri);
                        //AppHandler.TestResultManager.Delete(newId);
                    }
                    AppHandler.MainForm.ControlCenter.UpdateWorkoutLearnmapState(parentContent.MapTitle);

                    if (testId>0 && bLastTestSucceeded)
                        parentContent.SetLearningView();
                }
			}
		}

		public bool CanClose(bool bSwitchToLearnMode)
		{
            if (aWorkouts == null)
                return true;
            if (!aWorkouts.IsWorkedOut())
                return false;

            if (testId == 0)
            {
                if (!randomChoose)
                    questionPool.Reset();
                ChooseQuestions(!randomChoose);
            }
            if (bSwitchToLearnMode)
                parentContent.SetLearningView();

		    return true;
		}

	    public void SetTest(string strTestname)
	    {
	        for (int i = 0; i < AppHandler.MapManager.GetTestCount(parentContent.MapTitle); ++i)
	        {
	            if (AppHandler.MapManager.GetTest(parentContent.MapTitle, i).title == strTestname)
	            {
                    testId = i;
	                testName = strTestname;
                    testAlwaysAllowed = true;
	            }
	        }
	        CreateQuestionPool();
	    }

	    public string GetTestDescription()
	    {
            var ti = AppHandler.MapManager.GetTest(parentContent.MapTitle, testId);
            TestType tType;
            if (Utilities.Str2TestType(ti.type, out tType))
            {
                if (tType == TestType.Final)
                    return "Endtest";
                else
                {
                    return String.Format("Zwischentest({0})", ti.title);
                }
            }

            return "unbekannter Testtyp";
	    }

	    public void ResetTest()
	    {
            testId = 0;
	        testName = "";
	        bLastTestSucceeded = false;
            if (!randomChoose)
                questionPool.Reset();
            ChooseQuestions(!randomChoose);
	    }
	}
}
