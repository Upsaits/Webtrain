using System;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
    /// Zusammendfassende Beschreibung für ContentExamingControl.
	/// </summary>
	public class ContentExamingControl : ContentWorkoutControl
	{
        private AppHandler AppHandler = Program.AppHandler;
		public ContentExamingControl(FrmContent _parentContent,string _work) : base(_parentContent,_work,true,10)
		{
		}

        protected override void GetAllQuestions(ref QuestionCollection aQuestions)
        {
            if (randomChoose)
            {
                bool bFound = false;
                if (parentContent.RecentProgress.Count > 0)
                    foreach (var w in parentContent.RecentProgress)
                    {
                        AppHandler.LibManager.GetQuestions(w.Value.Item1, ref aQuestions, true, false);
                        if (w.Value.Item1 == m_work)
                            bFound = true;
                    }
                if (!bFound)
                    AppHandler.LibManager.GetQuestions(m_work, ref aQuestions, true, false);
            }
        }        

		protected override void AfterChoosing()
		{
			base.AfterChoosing();
			parentContent.CtrlBar.BtnChoose.Enabled = false;
		}

		protected override void SetActiveChanged()
		{
			if (isActive)
            {
				parentContent.CtrlBar.BtnSolution.Click += new EventHandler(OnBtnSolution);
				parentContent.CtrlBar.BtnSolution.MouseUp += new System.Windows.Forms.MouseEventHandler(OnBtnSolutionMouseUp);
				parentContent.CtrlBar.BtnSolution.MouseDown += new System.Windows.Forms.MouseEventHandler(OnBtnSolutionMouseDown);
				parentContent.CtrlBar.BtnChoose.Click += new EventHandler(OnBtnChoose);
			}
			else
			{
				parentContent.CtrlBar.BtnSolution.Click -= new EventHandler(OnBtnSolution);
				parentContent.CtrlBar.BtnSolution.MouseUp += new System.Windows.Forms.MouseEventHandler(OnBtnSolutionMouseUp);
				parentContent.CtrlBar.BtnSolution.MouseDown += new System.Windows.Forms.MouseEventHandler(OnBtnSolutionMouseDown);
				parentContent.CtrlBar.BtnChoose.Click -= new EventHandler(OnBtnChoose);
			}
			
			base.SetActiveChanged();
		}

		protected override void DoWorkout()
		{
			base.DoWorkout();

			if (activeWorkout.IsWorkedOut && !activeWorkout.IsRight)
				parentContent.CtrlBar.BtnSolution.Enabled = true;

			if (aWorkouts.IsWorkedOut())
				parentContent.CtrlBar.BtnChoose.Enabled = true;
		}

		private void OnBtnSolution(object sender, System.EventArgs e)
        {
            contentBrowser.ShowAnswer(activeWorkout,true);
        }

		private void OnBtnChoose(object sender, System.EventArgs e)
		{
            parentContent.CtrlBar.BtnSolution.Enabled = false;
			if (questionPool.IsEmpty)
				questionPool.Reset();
			ChooseQuestions(false);
		}

		private void OnBtnSolutionMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            contentBrowser.ShowAnswer(activeWorkout, false);
		}

		private void OnBtnSolutionMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            contentBrowser.ShowAnswer(activeWorkout, true);
		}
	}
}
