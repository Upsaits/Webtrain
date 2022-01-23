using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.XtraPrinting.Native;
using SoftObject.TrainConcept.Libraries;
using Ionic.Zip;
using TCWebUpdate.Repositories;

namespace TCWebUpdate
{
    public partial class ContentEditorPage : System.Web.UI.Page
    {
        private static string m_actLibrary = "";
        private static ContentTreeViewList m_treeViewList = null;
        private static DataTable m_nodesTable = null;
        private static LibraryManagerBridge m_libMan = null;
        private static LearnmapManagerBridge m_mapMan = null;
        private static MainMaster m_masterPage = null;
        private static RootMaster m_rootPage = null;
        private static WebtrainPackageRepository m_webtrainPackageRepo = WebtrainPackageRepository.Instance;
        private static ServerInfoRepository m_serverInfoRepo = ServerInfoRepository.Instance;

        private static Dictionary<int, List<WebtrainPackageInfo>> m_dPackageInfos =
            new Dictionary<int, List<WebtrainPackageInfo>>();

        private static QuestionCollection m_aQuestions = null;
        private static bool m_bWasInitialized = false;
        private static int m_iActQuId;
        private static WorkoutInfoItem m_activeWorkout = null;
        private static TreeViewNode m_activeQuestionNode = null;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            bool bIsAuthenticated = (Session["EMail"] != null);
            if (!IsPostBack)
            {
                m_treeViewList = null;
                m_nodesTable = null;

                m_masterPage = (MainMaster)Page.Master;
                m_rootPage = (RootMaster)m_masterPage.Master;

                m_libMan = m_masterPage.LibManager;
                m_mapMan = m_masterPage.MapManager;

                if (bIsAuthenticated)
                    SetupNavigationBar();

                if (!m_bWasInitialized)
                {
                    //MainMaster.OnExam += m_masterPage_OnExam;
                    m_bWasInitialized = true;
                }
                
                ASPxLabel[] aTest = new ASPxLabel[] {lblAnswer1};
            }

            if (!bIsAuthenticated)
            {
                m_masterPage = (MainMaster)Page.Master;
                m_masterPage.ShowPanels(false);

                m_rootPage.ShowMenu(false);
                CallbackPanel1.Visible = false;
            }
            else
            {
                m_masterPage = (MainMaster) Page.Master;
                m_masterPage.NavigationBar.ItemClick += NavigationBar_ItemClick;
                m_masterPage.ShowPanels(true,MainMaster.PanelType.ContentBrowser);
                m_rootPage.SetCampusName("Metzentrum Attnang-Puchheim");

            }
        }

        /*
        void m_masterPage_OnExam(object sender, ExamEventArgs e)
        {
            if (e.ExamStarted)
            {
                //QuestionPanel.Visible = false;
                //ExamPanel.Visible = true;
                chkExamAnswer1.Checked = true;
                lblExamAnswer1.Text = "Test";
            }
            else
            {
                //QuestionPanel.Visible = true;
                //ExamPanel.Visible = false;
            }
        }*/

        private void SetupNavigationBar()
        {
            m_masterPage.NavigationBar.Groups.Clear();
            m_masterPage.NavigationBar.ItemClick += NavigationBar_ItemClick;

#if DEBUG
            String[] aServerInfos = m_serverInfoRepo.GetServerInfos(2);
#else
            String[] aServerInfos = m_serverInfoRepo.GetServerInfos(1);
#endif
            foreach (var s in aServerInfos)
            {
                string[] strTokens = s.Split(',');
                if (strTokens.Length == 4)
                {
                    /* see: TCWebUpdate/ServerInfoRepository.cs
                    string strResult = r["locationId"].ToString() + ',' +      [0]
                                       r["name"].ToString() + ',' +   [1]
                                       r["licenseId"].ToString() + ',' +     [2]
                                       r["ipadress"].ToString() + ',' +       [3]
                      */
                }

                int licId = Convert.ToInt32(strTokens[2]);
                var aWPI = m_webtrainPackageRepo.GetAllPackageInfos(licId);
                if (aWPI.Length>0)
                {
                    m_dPackageInfos[licId] = new List<WebtrainPackageInfo>(aWPI);
                    NavBarGroup grp = new NavBarGroup(strTokens[1], strTokens[2]);
                    foreach (var wpi in m_dPackageInfos[licId])
                    {
                        if (wpi.PackageType=="Library" && wpi.Title != "Auswahlverfahren")
                        {
                            var navItem = new NavBarItem(wpi.Title, wpi.Title + '[' + wpi.ContentFilename + ']');
                            grp.Items.Add(navItem);
                        }
                    }

                    m_masterPage.NavigationBar.Groups.Add(grp);
                }

            }
        }

        private void ExtractLibrary(int licenseId, string strTitle)
        {
            string strFilename = "";
            foreach (var wpi in m_dPackageInfos[licenseId])
            {
                if (wpi.Title == strTitle)
                {
                    string strPackagesDir = HttpContext.Current.Server.MapPath(String.Format("~/packages/{0}/",licenseId));
                    string strJsonTempDir = strPackagesDir + Guid.NewGuid().ToString();
                    using (Ionic.Zip.ZipFile zipfile = new ZipFile(strPackagesDir + wpi.ContentFilename,
                        System.Text.Encoding.GetEncoding(850)))
                        foreach (ZipEntry e in zipfile)
                        {
                            if (Path.GetExtension(e.FileName) == ".xml")
                            {
                                e.Extract(strJsonTempDir);
                                strFilename = e.FileName;
                                break;
                            }
                        }

                    if (strFilename.Length > 0)
                    {
                        treeView.Nodes.Clear();
                        SetActualLibrary(strJsonTempDir + @"\" + strFilename);
                    }
                }
            }
        }

        private void SetActualLibrary(string strNewFile)
        {
            if(m_actLibrary.Length>0)
                m_libMan.Close(0);
            m_libMan.Open(MapPath("~/App_Data/"), strNewFile);
            m_treeViewList = new ContentTreeViewList(m_libMan, m_mapMan, ContentTreeViewList.UseType.QuestionContent);
            m_treeViewList.Fill();
            m_actLibrary = strNewFile;
            m_nodesTable = GetDataTable_VirtualMode();
            treeView.RefreshVirtualTree();
        }


        protected void treeView_VirtualModeCreateChildren(object source, TreeViewVirtualModeCreateChildrenEventArgs e)
        {
            if (m_nodesTable != null)
            {
                List<TreeViewVirtualNode> children = new List<TreeViewVirtualNode>();

                if (e.NodeName == null)
                {
                    for (int i = 0; i < m_nodesTable.Rows.Count; i++)
                    {
                        string parentName = "-1";
                        if (m_nodesTable.Rows[i]["ParentID"].ToString() == parentName)
                        {
                            TreeViewVirtualNode child = new TreeViewVirtualNode(m_nodesTable.Rows[i]["ID"].ToString(), m_nodesTable.Rows[i]["Title"].ToString());
                            children.Add(child);
                            child.IsLeaf = !(bool)m_nodesTable.Rows[i]["HasChilds"];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < m_nodesTable.Rows.Count; i++)
                    {
                        string parentName = e.NodeName;
                        if (m_nodesTable.Rows[i]["ParentID"].ToString() == parentName)
                        {
                            TreeViewVirtualNode child = new TreeViewVirtualNode(m_nodesTable.Rows[i]["ID"].ToString(), m_nodesTable.Rows[i]["Title"].ToString());
                            children.Add(child);
                            child.IsLeaf = !(bool)m_nodesTable.Rows[i]["HasChilds"];
                        }
                    }
                }
                e.Children = children;
            }
        }

        private DataTable GetDataTable_VirtualMode()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("ParentID", typeof(int));
            dt.Columns.Add("HasChilds", typeof(bool));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID"] };

            foreach (ContentTreeViewRecord r in m_treeViewList)
            {
                Debug.WriteLine(r.ID);
                Debug.WriteLine(r.Title);
                Debug.WriteLine(r.ParentID);
                Debug.WriteLine(r.GetQuestionId());
                dt.Rows.Add(r.ID, r.Title, r.ParentID, r.GetType() != ContentTreeViewRecordType.Question);
            }

            return dt;
        }


        void NavigationBar_ItemClick(object source, NavBarItemEventArgs e)
        {
            var nb= source as ASPxNavBar;
            if (nb != null) 
                nb.SelectedItem = e.Item;

            ExtractLibrary(Convert.ToInt32(e.Item.Group.Name), e.Item.Text);

            var aQuestions = new QuestionCollection();
            m_libMan.GetQuestions(m_treeViewList.GetPath(0),ref aQuestions,true,false);
            btnExamStart.Visible = aQuestions.Count > 0;
        }

        protected void CallbackPanel1_OnCallback(object sender, CallbackEventArgsBase e)
        {

        }

        protected void TreeView1_OnNodeClick(object source, TreeViewNodeEventArgs e)
        {
            treeView.SelectedNode = e.Node;
            chkAnswer1.Checked = true;
            int id = Convert.ToInt32(e.Node.Name);
            ContentTreeViewRecord r = (ContentTreeViewRecord) m_treeViewList[id];
            if (r.GetType()== ContentTreeViewRecordType.Question && r.ParentID >= 0)
            {
                var parentPath = m_treeViewList.GetPath(r.ParentID);
                //Debug.WriteLine(parentPath);
                var quItem = m_libMan.GetQuestion(parentPath, r.GetQuestionId());
                if (quItem != null)
                {
                    ShowQuestion(quItem,
                        null,
                        new ASPxLabel[]
                        {
                            lblAnswer1, lblAnswer2, lblAnswer3,
                            lblAnswer4, lblAnswer5, lblAnswer6,
                            lblAnswer7, lblAnswer8, lblAnswer9
                        },
                        new ASPxCheckBox[]
                        {
                            chkAnswer1, chkAnswer2, chkAnswer3,
                            chkAnswer4, chkAnswer5, chkAnswer6,
                            chkAnswer7, chkAnswer8, chkAnswer9
                        });
                }
            }

            btnExamStart.Enabled = (r.GetType() != ContentTreeViewRecordType.Question);
            btnSave.Visible = (r.GetType() == ContentTreeViewRecordType.Question);
            btnExport.Visible = (r.GetType() == ContentTreeViewRecordType.Question);
        }

        protected void treeView_Unload(object sender, EventArgs e)
        {

        }

        protected void treeView_Load(object sender, EventArgs e)
        {

        }

        protected void btnExamStart_OnClick(object sender, EventArgs e)
        {
            if (!btnExamStart.Checked)
            {
                QuestionPanel.Visible = false;
                ExamPanel.Visible = true;
                btnExamStart.Checked = true;

                btnWorkout.Visible = true;
                btnNextQuestion.Visible = true;

                if (treeView.SelectedNode != null)
                {
                    var node = treeView.SelectedNode;
                    int id = Convert.ToInt32(node.Name);
                    ContentTreeViewRecord r = (ContentTreeViewRecord)m_treeViewList[id];
                    if (r.ID >= 0)
                    {
                        var path = m_treeViewList.GetPath(r.ID);

                        // Frage anzeigen
                        m_aQuestions = new QuestionCollection();
                        m_libMan.GetQuestions(path, ref m_aQuestions, true, false);
                        m_iActQuId = 0;
                        m_activeQuestionNode = node;
                        ShowActualExamQuestion();
                    }
                }
            }
            else
            {
                QuestionPanel.Visible = true;
                ExamPanel.Visible = false;
                btnExamStart.Checked = false;
                btnWorkout.Visible = false;
                btnNextQuestion.Visible = false;

                ShowQuestion(null,
                    null,
                    new ASPxLabel[]
                    {
                        lblAnswer1, lblAnswer2, lblAnswer3,
                        lblAnswer4, lblAnswer5, lblAnswer6,
                        lblAnswer7, lblAnswer8, lblAnswer9
                    },
                    new ASPxCheckBox[]
                    {
                        chkAnswer1, chkAnswer2, chkAnswer3,
                        chkAnswer4, chkAnswer5, chkAnswer6,
                        chkAnswer7, chkAnswer8, chkAnswer9
                    });
            }
        }

        private void ShowActualExamQuestion()
        {
            lblExamProgress.Text = String.Format("{0}/{1}",m_iActQuId+1,m_aQuestions.Count);

            if (m_aQuestions.Count>0 && m_iActQuId >= 0 && m_iActQuId < m_aQuestions.Count)
            {
                var qu = m_aQuestions.Item(m_iActQuId);
                m_activeWorkout = new WorkoutInfoItem(qu, m_aQuestions.GetPath(qu), m_iActQuId);

                ShowQuestion(m_aQuestions.Item(m_iActQuId),
                    lblExamQuestion,
                    new ASPxLabel[]
                    {
                        lblExamAnswer1, lblExamAnswer2, lblExamAnswer3,
                        lblExamAnswer4, lblExamAnswer5, lblExamAnswer6,
                        lblExamAnswer7, lblExamAnswer8, lblExamAnswer9
                    },
                    new ASPxCheckBox[]
                    {
                        chkExamAnswer1, chkExamAnswer2, chkExamAnswer3,
                        chkExamAnswer4, chkExamAnswer5, chkExamAnswer6,
                        chkExamAnswer7, chkExamAnswer8, chkExamAnswer9
                    });

            }
        }
        
        private void ShowQuestion(QuestionItem quItem,
            ASPxLabel lblQu,
            ASPxLabel[] aLabels, ASPxCheckBox[] aChkBoxes)
        {
            foreach (var lbl in aLabels)
                lbl.Text = "";
            foreach (var chk in aChkBoxes)
            {
                chk.Visible = false;
                chk.Checked = false;
            }

            if (lblQu != null)
                lblQu.Text = quItem.question;
            if (quItem!=null && quItem.Answers != null)
            {
                BitArray corrMask = new BitArray(new int[] { quItem.correctAnswerMask });
                for (int i = 0; i < quItem.Answers.Length; ++i)
                {
                    aLabels[i].Text = quItem.Answers[i];
                    if (lblQu==null)
                        aChkBoxes[i].Checked = corrMask[i];
                    aChkBoxes[i].Visible = true;
                }
            }
        }
        
        private void GetActualSelection(ref WorkoutInfoItem workout, ASPxCheckBox[] aCheckBoxes)
        {
            if (workout != null && workout.Question.Answers != null)
            {
                BitArray actBits = new BitArray(new int[] { 0 });
                for (int i = 0; i < workout.Question.Answers.Length; ++i)
                {
                    bool isChecked = aCheckBoxes[i].Checked;
                    actBits.Set(i, isChecked);
                    workout.ActCorrMask = actBits;
                }
            }
        }

        protected void clbCallback_OnCallback(object source, CallbackEventArgs e)
        {
            if (e.Parameter == "btnWorkout")
            {
                GetActualSelection(ref m_activeWorkout,new ASPxCheckBox[]
                {
                    chkExamAnswer1, chkExamAnswer2, chkExamAnswer3,
                    chkExamAnswer4, chkExamAnswer5, chkExamAnswer6,
                    chkExamAnswer7, chkExamAnswer8, chkExamAnswer9
                });

                if (!m_activeWorkout.HasAnswer())
                {
                    e.Result = string.Format("{0};{1};{2}", "1", "0","Keine Antwort eingegeben!");
                }
                else
                {
                    m_activeWorkout.Workout();
                    if (m_activeWorkout.IsRight)
                    {
                        e.Result = string.Format("{0};{1};{2}", "1", "0", "Das ist richtig!");
                        if (m_iActQuId < (m_aQuestions.Count - 1))
                            btnNextQuestion.Visible = true;
                        else
                            btnNextQuestion.Visible = false;
                    }
                    else
                    {
                        e.Result = string.Format("{0};{1};{2}", "1", "0", "Das ist falsch!");
                    }
                }
            }
            else if (e.Parameter == "btnSave")
            {
                if (treeView.SelectedNode != null)
                {
                    var node = treeView.SelectedNode;
                    int id = Convert.ToInt32(node.Name);
                    ContentTreeViewRecord r = (ContentTreeViewRecord) m_treeViewList[id];
                    if (r.ID >= 0)
                    {
                        var parentPath = m_treeViewList.GetPath(r.ParentID);
                        var quItem = m_libMan.GetQuestion(parentPath, r.GetQuestionId());
                        if (quItem != null)
                        {
                            WorkoutInfoItem workout = new WorkoutInfoItem(quItem, parentPath, r.GetQuestionId());
                            GetActualSelection(ref workout, new ASPxCheckBox[]
                            {
                                chkAnswer1, chkAnswer2, chkAnswer3,
                                chkAnswer4, chkAnswer5, chkAnswer6,
                                chkAnswer7, chkAnswer8, chkAnswer9
                            });

                            var newQue = (QuestionItem) workout.Question.Clone();
                            newQue.correctAnswerMask = 0;
                            for (int i=0;i<workout.ActCorrMask.Count;++i)
                                if (workout.ActCorrMask[i])
                                    newQue.correctAnswerMask += (1 << i);

                            if (newQue.correctAnswerMask != workout.Question.correctAnswerMask)
                            {
                                m_libMan.SetQuestion(workout.Path, newQue, workout.Id);
                                m_libMan.Reload(workout.Path);
                            }
                        }
                    }
                }
            }
        }

        protected void btnNextQuestion_OnClick(object sender, EventArgs e)
        {
            m_iActQuId = Math.Min(m_aQuestions.Count-1,m_iActQuId+1);

            ShowActualExamQuestion();
        }

        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            var lib = m_libMan.GetLibrary(0);
            string strFilepath = m_libMan.GetFilePath(lib.title);
            string strFilename=Path.GetFileName(strFilepath);

            try
            {
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.AppendHeader("Content-Disposition", String.Format("attachment; filename={0}",strFilename));
                Response.TransmitFile(strFilepath);
                Response.Flush();
                Response.End();
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }
    }
}