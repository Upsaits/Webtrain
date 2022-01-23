using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using SoftObject.TrainConcept.Libraries;

namespace TCWebUpdate
{
    /*
    public class ExamEventArgs : EventArgs
    {
        private readonly int iQuId;
        private readonly bool bShow;

        public ExamEventArgs(bool bStarted,int iQuId=-1)
        {
            this.iQuId = iQuId;
            this.bShow = bStarted;
        }

        public int QuestionId { get { return iQuId; } }
        public bool ExamStarted { get { return bShow; } }
    }*/


    public partial class MainMaster : System.Web.UI.MasterPage
    {
        //public static event EventHandler<ExamEventArgs> OnExam;
        public enum PanelType{ ContentBrowser, UserProfile };

        private static LibraryManagerBridge m_libMan = new LibraryManagerBridge(new DefaultLibraryManager());
        private static LearnmapManagerBridge m_mapMan = new LearnmapManagerBridge(new DefaultLearnmapManager());

        public SoftObject.TrainConcept.Libraries.LibraryManagerBridge LibManager { get { return m_libMan; }}
        public SoftObject.TrainConcept.Libraries.LearnmapManagerBridge MapManager{ get { return m_mapMan; } } 
        
        public ASPxNavBar NavigationBar
        {
            get { return NavBar1; }
        }

        protected void Page_Load(object sender, EventArgs e) 
        {
        }

        public void ShowPanels(bool bShow, PanelType panelType=PanelType.ContentBrowser)
        {
            if (panelType==PanelType.UserProfile)
            {
                //NavigationBar.Groups.Clear();
                //var grp = new NavBarGroup("Daten", "Daten");
                //var navItem = new NavBarItem("Persönliche Daten");
                //grp.Items.Add(navItem);
                //navItem = new NavBarItem("Sozialanamnese");
                //grp.Items.Add(navItem);
                //NavigationBar.Groups.Add(grp);
            }
            this.LeftPane.Visible = bShow;
        }

        /*
        protected void btnStartExam_OnClick(object sender, EventArgs e)
        {
            if (!btnStartExam.Checked)
            {
                btnStartExam.Checked = true;
                if (OnShowQuestion != null)
                    OnShowQuestion(this, new ShowQuestionEventArgs(true,0));
            }
            else
            {
                btnStartExam.Checked = false;
                if (OnShowQuestion != null)
                    OnShowQuestion(this, new ShowQuestionEventArgs(false));
            }
        }*/

        /*
        protected void btnStartExam_OnClick(object sender, EventArgs e)
        {
            var ctrl1 = MainContent.FindControl("CallbackPanel1");
            if (ctrl1 != null)
            {
                ASPxCallbackPanel pan = ctrl1 as ASPxCallbackPanel;
                var ctrl2 = ctrl1.FindControl("PanelContent1");
                if (ctrl2 != null)
                {
                    var panQu = ctrl2.FindControl("QuestionPanel");
                    var panExam = ctrl2.FindControl("ExamPanel");
                    if (panQu != null && panExam != null)
                    {
                        if (!btnStartExam.Checked)
                        {
                            panQu.Visible = false;
                            panExam.Visible = true;
                            btnStartExam.Checked = true;
                            if (OnExam!=null)
                                OnExam.Invoke(this, new ExamEventArgs(true,0));
                        }
                        else
                        {
                            panQu.Visible = true;
                            panExam.Visible = false;
                            btnStartExam.Checked = false;
                            if (OnExam!=null)
                                OnExam.Invoke(this, new ExamEventArgs(false));
                        }
                    }
                }
            }
        } */
    }
}