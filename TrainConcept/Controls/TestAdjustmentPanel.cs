using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
    public partial class TestAdjustmentPanel : UserControl
    {
        private ListViewEx lvwQuestions=null;
        private string mapTitle = "";
        private int testId = -1;
        private AppHandler AppHandler = Program.AppHandler;

        public string TrialCntText { set { spnTrialCnt.Text = value; } }
        public int TrialCnt { get { return (int) spnTrialCnt.Value; } }
        public string SuccessLevelText { set { spnSuccessLevel.Text = value; } }
        public int SuccessLevel { get { return (int)spnSuccessLevel.Value; } }
        public string QuestionCountText { set { spnQuCnt.Text = value; } }
        public int QuestionCount { get { return Int32.Parse(this.spnQuCnt.Text); } }

        public bool TestAlwaysAllowed
        {
            set { chkTestAlwaysAllowed.Checked = value; }
            get { return chkTestAlwaysAllowed.Checked; }
        }

        public bool QuestionChooseChecked
        {
            set { rbnQuChoose.Checked = value; }
            get { return rbnQuChoose.Checked; }
        }

        public bool QuestionSelectChecked 
        { 
            set { rbnQuSelect.Checked = value; }
            get { return rbnQuSelect.Checked; }
        }

        public bool IsRandomChoose { get { return rbnQuChoose.Checked; } }

        public ListViewEx ListViewQuestions
        {
            set { lvwQuestions = value; }
        }

        public string LearnmapTitle
        {
            set { mapTitle = value; }
        }

        public int TestId
        {
            set { testId = value; }
        }

        public TestAdjustmentPanel()
        {
   
            InitializeComponent();

            this.label4.Text = AppHandler.LanguageHandler.GetText("FORMS", "Trialcount", "Anzahl Versuche") + ':';
            this.label2.Text = AppHandler.LanguageHandler.GetText("FORMS", "Successlevel", "Erfolgsgrenze") + ':';
            this.label1.Text = AppHandler.LanguageHandler.GetText("FORMS", "Questioncount", "Fragenanzahl") + ':';
            this.rbnQuChoose.Text = AppHandler.LanguageHandler.GetText("FORMS", "Choose_questions_randomized", "Fragen zufällig wählen");
            this.rbnQuSelect.Text = AppHandler.LanguageHandler.GetText("FORMS", "Use_questionlist", "Fragenliste verwenden");

        }

        public void SaveTestValues()
        {
            if (mapTitle.Length > 0/* && testId >= 0*/)
            {
                var ti = AppHandler.MapManager.GetTest(mapTitle, testId);
                if (ti != null)
                {
                    AppHandler.MapManager.SetTest(mapTitle, testId, ti.title,IsRandomChoose,QuestionCount,TrialCnt,SuccessLevel,"",TestAlwaysAllowed, testId == 0 ? TestType.Final : TestType.Intermediate);
                    AppHandler.MapManager.Save(mapTitle);
                    AppHandler.TestResultManager.FireEvent(EventArgs.Empty);
                }
            }
        }

        private void rbnQuChoose_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnQuChoose.Checked)
            {
                if (lvwQuestions != null)
                    lvwQuestions.Enabled = false;
                SaveTestValues();
            }
        }

        private void rbnQuSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnQuSelect.Checked)
            {
                if (lvwQuestions != null)
                {
                    lvwQuestions.Enabled = true;
                    if (lvwQuestions.Items.Count > 0)
                    {
                        if (lvwQuestions.Items.Count > Int32.Parse(this.spnQuCnt.Text))
                            spnQuCnt.Text = lvwQuestions.Items.Count.ToString();
                    }
                }
                SaveTestValues();
            }
        }

        private void spnQuCnt_EditValueChanged(object sender, EventArgs e)
        {
            SaveTestValues();
        }

        private void spnSuccessLevel_EditValueChanged(object sender, EventArgs e)
        {
            SaveTestValues();
        }

        private void spnTrialCnt_EditValueChanged(object sender, EventArgs e)
        {
            SaveTestValues();
        }

        private void chkTestAlwaysAllowed_CheckedChanged(object sender, EventArgs e)
        {
            SaveTestValues();
        }

    }
}
