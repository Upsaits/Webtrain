using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Interfaces;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Zusammendfassende Beschreibung für LibraryOverview.
	/// </summary>
	public partial class LibraryOverview : UserControl, IContentNavigationCtrl
	{
		private ResourceHandler rh=null;
        private int pageId = 0;
        private int maxPages = 0;
        private AppHandler AppHandler = Program.AppHandler;
        public int PageId
        {
            get { return pageId; }
            set
            {
                pageId = value;
                if (pageId == 0)
                {
                    btnForward.Enabled = false;
                    btnBack.Enabled = false;
                }
                else if (pageId == 1)
                {
                    btnBack.Enabled = false;
                    if (maxPages == 1)
                        btnForward.Enabled = false;
                    else
                        btnForward.Enabled = true;
                }
                else if (pageId == maxPages)
                {
                    btnBack.Enabled = true;
                    btnForward.Enabled = false;
                }
                else
                {
                    btnForward.Enabled = true;
                    btnBack.Enabled = true;
                }
                //label1.Text = pageId.ToString() + "/" + maxPages.ToString();
            }

        }

        public int MaxPages
        {
            get { return maxPages; }
            set
            {
                maxPages = value;
                if (maxPages > 0)
                    PageId = 1;
                else
                    PageId = 0;
            }
        }

        public Panel NavigationPanel
        {
            get { return panel1; }
        }

        public XContentTree ContentTree
        {
            get { return contentTreeView1; }
        }

        public SimpleButton BtnPrevPage
        {
            get { return btnPrevPage; }
        }
        
	    public SimpleButton BtnNextPage
	    {
	        get { return btnNextPage; }
	    }

        public SimpleButton BtnForward
        {
            get { return btnForward; }
        }

	    public SimpleButton BtnBack
        {
            get { return btnBack; }
        }
        
        public void SetPageId(int iPageId)
        {
            PageId = iPageId;
        }

        public int GetPageId()
        {
            return PageId;
        }

        public void SetMaxPages(int iPages)
        {
            MaxPages = iPages;
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

        public void AddDocument(string strTitle,int typeId)
        {
        }

        public void ClearDocuments()
        {
        }
        
		public LibraryOverview()
		{
            rh = new ResourceHandler(AppHandler.ResourceName, GetType().Assembly);

			InitializeComponent();

            SetupButtons();

			contentTreeView1.FillTree();
			contentTreeView1.PopulateColumns();
		}

        private void SetupButtons()
        {
            this.SuspendLayout();

            lBtnImages.Images.Add(rh.GetBitmap("left"));
            lBtnImages.Images.Add(rh.GetBitmap("right"));

            //btnBack.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Back","Zurück");
            btnBack.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            btnBack.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            btnBack.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR", "BackDescription", "zurück blättern");
            btnBack.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            btnBack.LookAndFeel.UseDefaultLookAndFeel = false;
            //btnBack.Font = AppHandler.DefaultFont;

            //btnForward.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Forward","Vorwärts");
            btnForward.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            btnForward.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            btnForward.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR", "ForwardDescription", "vorwärts blättern");
            btnForward.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            btnForward.LookAndFeel.UseDefaultLookAndFeel = false;
            //btnForward.Font = AppHandler.DefaultFont;
            this.ResumeLayout();
        }
        
        public void SetActualPoint(string path,int iSubId=0)
		{
            if (!contentTreeView1.IsItemSelected(path))
			    contentTreeView1.SelectItem(path,iSubId);
            UpdateCheckState(path);
		}

        public void UpdateCheckState(string path,int iPageCntWorkedOut=-1)
        {
            int iCheckedState = AppHelpers.GetMapProgressOfWork(AppHandler.MainForm.ActualUserName, path, iPageCntWorkedOut);
            contentTreeView1.CheckItem(path, iCheckedState);
        }

		public void UpdateView()
		{
			contentTreeView1.FillTree();
			contentTreeView1.PopulateColumns();

            Form frmActive = AppHandler.MainForm.ActiveMdiChild;
            if (frmActive is FrmContent)
            {
                FrmContent frmContent = frmActive as FrmContent;
                if (frmContent.ActiveView is ContentLearningControl)
                {
                    this.contentTreeView1.SelectItem((frmContent.ActiveView as ContentLearningControl).Work);
                }
            }
            else if (frmActive is FrmEditContent)
            {
                FrmEditContent frmEditContent = frmActive as FrmEditContent;
                if (frmEditContent.ActualEditMode == FrmEditContent.EditMode.Content)
                {
                    this.contentTreeView1.SelectItem(frmEditContent.SelectedOverview);
                }
                else if (frmEditContent.ActualEditMode == FrmEditContent.EditMode.Questions)
                {
                    int questionCnt = 0;
                    if (AppHandler.LibManager.GetQuestionCnt(frmEditContent.SelectedOverview, out questionCnt) && questionCnt>0)
                        this.contentTreeView1.SelectItem(frmEditContent.SelectedOverview,frmEditContent.ActQuestionId+1);
                    else
                        this.contentTreeView1.SelectItem(frmEditContent.SelectedOverview);
                }

            }
		}

        public void CheckPageButtons()
        {
            string strPath = ContentTree.GetItemPath(ContentTree.FocusedNode);
            if (strPath.Length>0)
            {
                string strNextPointPath = ContentTree.GetNextPoint(strPath);
                btnNextPage.Enabled = strNextPointPath.Length > 0;

                string strPrevPointPath = ContentTree.GetPrevPoint(strPath);
                btnPrevPage.Enabled = strPrevPointPath.Length > 0;
            }
        }
        
        private void contentTreeView1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.OldNode == null)
                return;

            if (NavigationPanel.Visible)
                CheckPageButtons();
        }

        private void contentTreeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
        }

        private void contentTreeView1_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.OldNode == null)
                return;

            int iSubId = 0;
            XContentTree tv = sender as XContentTree;
            string strNewPath = tv.GetItemPath(e.Node, ref iSubId);
            string strOldPath = tv.GetItemPath(e.OldNode);
            if (strNewPath.Length > 0)
            {
                string[] aTitles = null;
                Utilities.SplitPath(strNewPath, out aTitles);
                if (aTitles.Length == 4)
                {
                    Form frmActive = AppHandler.MainForm.ActiveMdiChild;
                    if (frmActive is FrmContent)
                    {
                        FrmContent frmContent = frmActive as FrmContent;
                        if (!frmContent.OnLibOverviewFocusNodeChange(strNewPath))
                            e.CanFocus=false;
                    }
                    else if (frmActive is FrmLMEditContentNew)
                    {
                        FrmLMEditContentNew frmContentNew = frmActive as FrmLMEditContentNew;
                        if (!frmContentNew.OnLibOverviewFocusNodeChange(strNewPath, iSubId))
                            e.CanFocus = false;
                    }
                    else if (frmActive is FrmEditContent)
                    {
                        FrmEditContent frmEditContent = frmActive as FrmEditContent;
                        if (!frmEditContent.OnLibOverviewFocusNodeChange(strNewPath, iSubId))
                            e.CanFocus = false;
                    }

                    if (NavigationPanel.Visible)
                        CheckPageButtons();
                }
            }

        }

    }
}
