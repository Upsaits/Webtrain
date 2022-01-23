using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTreeList.Nodes;
using Forms;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Controls;
using SoftObject.TrainConcept.Interfaces;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for FrmEditContent.
	/// </summary>
    public partial class FrmEditContent : XtraForm, IContentNavigationCtrl
	{
        private enum ImgZoomType {X,Y,CalcWidth,CalcHeight};


		/// <summary>
		/// Required designer variable.
		/// </summary>
		public enum EditMode {Overview,Content,Questions};
        private EditMode editMode = EditMode.Overview;
		private string	m_strSelectedOverview="";
		private PageItem m_actPage=null;
		private QuestionItem m_actQuestion=null;
        private int m_iActPageId = 0;
        private int m_iActQuestionId = 0;
        private ContentBrowser m_contentBrowser = null;
        private ResourceHandler rh=null;
        private Point m_dragPoint = Point.Empty;
        private XtraTabPage m_dragPage = null;
        private bool m_bIsActive = false;
        private bool m_bIgnoreUpdate = false;
        private DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1;
        private DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup2;
        private AppHandler AppHandler = Program.AppHandler;
        private AppHelpers AppHelpers = Program.AppHelpers;

        public int ActQuestionId
        {
            get { return m_iActQuestionId; }
            set { m_iActQuestionId = value; }
        }

        public int ActPageId
        {
            get { return m_iActPageId; }
            set { m_iActPageId = value; }
        }

        public EditMode ActualEditMode
        {
            get { return editMode; }
            set { editMode = value; }
        }

        public string SelectedOverview
        {
            get { return m_strSelectedOverview; }
            set 
            { 
                m_strSelectedOverview = value;
                txtSelectedOverview.Text = m_strSelectedOverview;
            }
        }

		public FrmEditContent()
		{
            InitGallery1();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            rh = new ResourceHandler(AppHandler.ResourceName, GetType().Assembly);

            AppHandler.LibManager.LibraryManagerEvent += (object sender, ref LibraryManagerEventArgs ea) =>
            {
                if (ea.Command==LibraryManagerEventArgs.CommandType.ChangeManagement) 
                    xContentTree1.FillTree();
            };

			Initialize();
		}

        private void InitGallery1()
        {
            galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            galleryItemGroup2 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();

            var aGallery1 = new List<GalleryItem>();
            var aGallery2 = new List<GalleryItem>();
            for (int i = 0; i < AppHandler.Templates.Count; ++i)
                aGallery1.Add(new GalleryItem());
            aGallery2.AddRange(aGallery1);
            galleryItemGroup1.Caption = "Vorlagen";
            galleryItemGroup2.Caption = "Vorlagen";

            int id = 0;
            foreach (var t in AppHandler.Templates)
            {
                aGallery1[id].Caption = t.Value;
                aGallery1[id].Description = t.Value + "_f.htm";
                aGallery1[id].Hint = t.Value + "_f.htm";
                aGallery1[id].HoverImageIndex = id;
                aGallery1[id].ImageIndex = id;

                aGallery2[id].Caption = t.Value;
                aGallery2[id].Description = t.Value + "_f.htm";
                aGallery2[id].ImageIndex = id;
                ++id;
            }
            galleryItemGroup1.Items.AddRange(aGallery1.ToArray());
            galleryItemGroup2.Items.AddRange(aGallery2.ToArray());
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
                m_bIsActive = false;
                AppHandler.MainForm.CloseBar.Visible = false;
                AppHandler.MainForm.NoticeBar.Visible = false;
            }
            else
            {
                m_bIsActive = true;
                if (editMode == EditMode.Questions)
                {
                    AppHandler.MainForm.LibOverview.ContentTree.LearnmapName = "";
                    AppHandler.MainForm.LibOverview.ContentTree.UseType = ContentTreeViewList.UseType.QuestionContent;
                    AppHandler.MainForm.LibOverview.UpdateView();
                    AppHandler.MainForm.LibOverviewBar.Visible = true;
                    AppHandler.MainForm.LibOverview.NavigationPanel.Visible = false;
                    AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = true;
                }
                else if (editMode == EditMode.Content)
                {
                    AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = false;
                    AppHandler.MainForm.LibOverviewBar.Visible = false;
                }

                AppHandler.MainForm.CloseBar.Visible = true;
            }
        }

        public void InitGalleries()
        {
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gddTemplates)).BeginInit();
            this.SuspendLayout();

            this.ribbonGalleryBarItemTemplates.Gallery.Groups.Add(galleryItemGroup1);
            this.gddTemplates.Gallery.Groups.Add(galleryItemGroup2);

             var rhTemplates = new ResourceHandler("SoftObject.Trainconcept.res.webtrain.resources_EditorTemplates", GetType().Assembly);
             foreach (var e in AppHandler.Templates)
             {
                 var bmp = rhTemplates.GetBitmap(e.Value);
                 var bmpSmall = rhTemplates.GetBitmap(e.Value + "_small");
                 if (bmp!=null && bmpSmall!=null)
                 {
                     this.imageList1.Images.Add(e.Value, bmpSmall);
                     this.imageList2.Images.Add(e.Value, bmp);
                 }
             }

            this.ribbonGalleryBarItemTemplates.Gallery.Images = this.imageList1;
            this.ribbonGalleryBarItemTemplates.Gallery.HoverImages = this.imageList2;
            this.gddTemplates.Gallery.Images = this.imageList2;

            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gddTemplates)).EndInit();
            this.ResumeLayout();
        }

        public void Initialize()
		{
            InitGalleries();

			xContentTree1.FillTree();
			xContentTree1.PopulateColumns();
			xContentTree1.BestFitColumns();
			xContentTree1.CollapseAll();
            xContentTree1.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            SwitchEditMode(EditMode.Overview);
		}

		private void SwitchEditMode(EditMode tEdit)
		{
			if (tEdit==EditMode.Overview)
			{
				panOverview.Dock = DockStyle.Fill;
				panOverview.Visible=true;
				panContent.Visible =false;
				panQuestions.Visible =false;
				btnOverview.Enabled=false;
				btnContent.Enabled=false;
				btnQuestions.Enabled=false;
                btnImport.Enabled = true;
                btnExport.Enabled = false;
                btnTest.Visible = false;
                btnAdd.Visible = false;
                btnDel.Visible = false;
                btnSettings.Visible = true;

				xContentTree1.Visible=true;
				xContentTree1.UseType = ContentTreeViewList.UseType.FullContent;
				xContentTree1.Dock = DockStyle.Top;
                
                ribbonControl1.Dock = DockStyle.None;
                ribbonControl1.Visible = false;
                xtraTabCtrl_Content.Visible = false;
				xtraTabCtrl_Content.Dock=DockStyle.None;
				panContentBrowser.Visible=false;
                panContentBrowser.Dock = DockStyle.None;
				editMode = tEdit;

				UpdateOverviewMode();
			}
			else if (tEdit==EditMode.Content)
			{
				panContent.Dock = DockStyle.Fill;
				panContent.Visible= true;
				panOverview.Visible=false;
				panQuestions.Visible =false;
				btnOverview.Enabled=true;
				btnContent.Enabled=false;
				btnQuestions.Enabled=true;
                btnTest.Visible = false;
                btnImport.Enabled = true;
                btnExport.Enabled = false;
                btnAdd.Visible = true;
                btnDel.Visible = false;
                btnSettings.Visible = false;
				xContentTree1.Visible=false;
				xContentTree1.Dock = DockStyle.None;
                ribbonControl1.Visible = true;
                ribbonControl1.Dock = DockStyle.Top;
                xtraTabCtrl_Content.Visible = true;
				xtraTabCtrl_Content.Dock=DockStyle.Fill;
                panContentBrowser.Visible = true;
                panContentBrowser.Dock = DockStyle.Top;
				editMode = tEdit;

				UpdateContentMode(0);				
			}
			else // Questions
			{
				panQuestions.Dock = DockStyle.Fill;
				panQuestions.Visible= true;
				panOverview.Visible=false;
				panContent.Visible =false;
				btnOverview.Enabled=true;
				btnContent.Enabled=true;
				btnQuestions.Enabled=false;
                btnImport.Enabled = true;
                btnExport.Enabled = false;
                btnTest.Visible = true;
                btnTest.Enabled = false;
                btnAdd.Visible = true;
                btnDel.Visible = false;
                btnSettings.Visible = false;
                xContentTree1.Visible = false;
				xContentTree1.Dock = DockStyle.None;
                ribbonControl1.Visible = false;
                ribbonControl1.Dock = DockStyle.None;
                xtraTabCtrl_Questions.Visible = true;
				xtraTabCtrl_Questions.Dock=DockStyle.Fill;
                panContentBrowser.Visible = true;
                panContentBrowser.Dock = DockStyle.Top;
				editMode = tEdit;

                UpdateQuestionMode();
			}
		}

		private void UpdateOverviewMode()
		{
            if (m_strSelectedOverview.Length > 0)
            {
                xContentTree1.SelectItem(m_strSelectedOverview);
                btnOverviewDelete.Enabled = true;
                btnContent.Enabled = true;
                btnQuestions.Enabled = true;
            }

            AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = false;
            AppHandler.MainForm.LibOverviewBar.Visible = false;
		}
	
		private void UpdateContentMode(int iActivePageId)
		{
            if (m_contentBrowser != null)
                m_contentBrowser.Dispose();

            m_contentBrowser = new ContentBrowser(this.axWebBrowser1, this, null, m_strSelectedOverview, null,true);

            try
            {
                if (xtraTabCtrl_Content.TabPages.Count > 0)
                    xtraTabCtrl_Content.TabPages.Clear();
            }
            catch (System.ArgumentOutOfRangeException /*ex*/)
            {
            }

            while (ribbonPageGroupNotices.ItemLinks.Count > 0)
                this.ribbonControl1.Items.Remove(((BarButtonItemLink)ribbonPageGroupNotices.ItemLinks[0]).Item);
            this.ribbonPageGroupNotices.ItemLinks.Clear();

            int pageCnt=0;
			if (AppHandler.LibManager.GetPageCnt(m_strSelectedOverview,out pageCnt))
			{
				for (int i=0;i<pageCnt;++i)
				{
					PageItem page=AppHandler.LibManager.GetPage(m_strSelectedOverview,i);
					if (page!=null)
					{
						var tabPage=new XtraTabPage();
						tabPage.Text = String.Format("Seite {0}",i+1);
						ContentEditTabPage client = new ContentEditTabPage(m_strSelectedOverview,i,new ShowPageCallback(ShowPage),
						    (b) =>
						    {
						        btnDelAction.Enabled = b;
                            }, strGetActionFullPath);
						client.Dock = DockStyle.Fill;
						tabPage.Controls.Add(client);
                        if (client.Page.isLocal)
                        {
                            tabPage.Appearance.Header.ForeColor = System.Drawing.Color.Red;
                            tabPage.Appearance.Header.Options.UseForeColor = true;
                        }

						xtraTabCtrl_Content.TabPages.Add(tabPage);
					}
				}

                if (pageCnt == 0)
                {
                    this.panContentBrowser.Visible = false;
                    this.panContentBrowser.Dock = DockStyle.None;
                }
                else
                {
                    this.panContentBrowser.Visible = true;
                    this.panContentBrowser.Dock = DockStyle.Top;

                    xtraTabCtrl_Content.SelectedTabPageIndex = (iActivePageId >= 0) ? iActivePageId : m_iActPageId;
                }
            }

            AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = false;
            AppHandler.MainForm.LibOverviewBar.Visible = false;

            btnAdd.Text = "Neue Seite";
            btnDel.Text = "Seite Löschen";
            btnDel.Visible = pageCnt > 0;
            m_contentBrowser.IsActive = pageCnt > 0;
        }

		private void UpdateQuestionMode(bool bFromLibOverview=false)
		{
            if (m_bIgnoreUpdate)
                return;

            if (m_contentBrowser != null)
                m_contentBrowser.Dispose();
            m_contentBrowser = new ContentBrowser(this.axWebBrowser1, this, null, m_strSelectedOverview, 
                (bIsEnabled) =>
                                {
                                    btnTest.Enabled = bIsEnabled;
                                },true);
            m_contentBrowser.UseType = ContentBrowser.ContentUseType.Question;

            m_bIgnoreUpdate = true;

            try
            {
	            if (xtraTabCtrl_Questions.TabPages.Count>0)
				    xtraTabCtrl_Questions.TabPages.Clear();
            }
            catch (System.ArgumentOutOfRangeException /*ex*/)
            {
            }

			int questionCnt=0;
			if (AppHandler.LibManager.GetQuestionCnt(m_strSelectedOverview,out questionCnt))
            {
				for (int i=0;i<questionCnt;++i)
				{
					var tabPage=new XtraTabPage();
					tabPage.Text = String.Format("Frage {0}",i+1);
					QuestionEditTabPage client = new QuestionEditTabPage(m_strSelectedOverview,i,ShowPage);
					client.Dock = DockStyle.Fill;
					tabPage.Controls.Add(client);
                    if (client.Question!=null && client.Question.isLocal)
                    {
                        tabPage.Appearance.Header.ForeColor = Color.Red;
                        tabPage.Appearance.Header.Options.UseForeColor = true;
                    }
					xtraTabCtrl_Questions.TabPages.Add(tabPage);
				}

                if (!bFromLibOverview)
                {
                    AppHandler.MainForm.LibOverview.ContentTree.LearnmapName = "";
                    AppHandler.MainForm.LibOverview.ContentTree.UseType = ContentTreeViewList.UseType.QuestionContent;
                    AppHandler.MainForm.LibOverview.UpdateView();
                    AppHandler.MainForm.LibOverviewBar.Visible = true;
                    AppHandler.MainForm.LibOverview.NavigationPanel.Visible = false;

                    AppHandler.MainForm.ToolBar.Items["bLibOverview"].Visible = true;
                }
            }


            btnAdd.Text = "Neue Frage";
            btnDel.Text = "Frage Löschen";
            btnDel.Visible = questionCnt > 0;
            btnTest.Visible = questionCnt > 0;

            m_contentBrowser.IsActive = questionCnt > 0;
            m_bIgnoreUpdate = false;
        }

		protected override void OnResize(EventArgs ea)
		{
			base.OnResize(ea);

			if (xContentTree1!=null)
				xContentTree1.Size = new Size(ClientSize.Width,ClientSize.Height/2);
			if (xtraTabCtrl_Content!=null)
				xtraTabCtrl_Content.Size = new Size(ClientSize.Width,ClientSize.Height);
		}

        private void ShowPage(object parentPage,bool bUpdateTabPage=false)
        {
            if (editMode == EditMode.Content)
            {
                if (parentPage != null)
                {
                    if (xtraTabCtrl_Content.SelectedTabPageIndex == m_iActPageId)
                    {
                        var tabPage = (parentPage as ContentEditTabPage);
                        if (bUpdateTabPage)
                            UpdateContentEditTabPage(tabPage);

                        AppHandler.MainForm.LibOverview.UpdateView();
                        m_contentBrowser.ShowPage();
                    }
                }
            }
            else if (editMode == EditMode.Questions)
            {
                if (parentPage != null)
                {
                    var tabPage = (parentPage as QuestionEditTabPage);
                    if (bUpdateTabPage)
                        UpdateQuestionEditTabPage(tabPage);

                    AppHandler.MainForm.LibOverview.UpdateView();
                    m_contentBrowser.SetQuestion(m_actQuestion);
                    m_contentBrowser.ShowPage();
                }
            }
        }

        public bool OnLibOverviewFocusNodeChange(string strNewPath, int iSubId)
        {
            if (!m_bIsActive)
                return false;

            if (editMode == EditMode.Content)
            {
                if (strNewPath != m_strSelectedOverview)
                {
                    xContentTree1.SelectItem(strNewPath);
                }
            }
            else if (editMode == EditMode.Questions)
            {
                if (strNewPath == m_strSelectedOverview)
                {
                    if (iSubId != xtraTabCtrl_Questions.SelectedTabPageIndex)
                        xtraTabCtrl_Questions.SelectedTabPageIndex = iSubId;
                }
                else
                {
                    xContentTree1.SelectItem(strNewPath);
                    SelectedOverview = strNewPath;
                    UpdateQuestionMode(true);
                }
            }

            return true;
        }

        private void UpdateContentEditTabPage(ContentEditTabPage page)
        {
            GalleryItem selItem = this.ribbonGalleryBarItemTemplates.Gallery.Groups[0].Items[page.SelectedTemplateId];
            this.ribbonGalleryBarItemTemplates.Gallery.SetItemCheck(selItem, true);
            this.ribbonGalleryBarItemTemplates.Gallery.MakeVisible(selItem);

            selItem = this.ribbonGalleryBarItemTemplates.GalleryDropDown.Gallery.Groups[0].Items[page.SelectedTemplateId];
            this.ribbonGalleryBarItemTemplates.GalleryDropDown.Gallery.SetItemCheck(selItem, true);
            this.ribbonGalleryBarItemTemplates.GalleryDropDown.Gallery.MakeVisible(selItem);

            //transparentFrameControl1.Hide();

            if (page.Page.PageActions != null)
            {
                var cnt = page.Page.PageActions.Count(p => p is ImageActionItem);
                if (cnt >= 1)
                {
                    barEditHeight.Enabled = true;
                    barEditWidth.Enabled = true;
                    barButtonLeft.Enabled = true;
                    barButtonRight.Enabled = true;
                    barButtonUp.Enabled = true;
                    barButtonDown.Enabled = true;
                    barSelectImageId.Enabled = false;

                    ImageActionItem imgItem = (ImageActionItem)page.Page.PageActions.Where(p => p is ImageActionItem).First();
                    barEditWidth.EditValue = ZoomImageItem(imgItem, 0, ImgZoomType.CalcWidth);
                    barEditHeight.EditValue = ZoomImageItem(imgItem, 0, ImgZoomType.CalcHeight);

                    barEditHeight.Enabled = !barCheckProp.Checked;

                    //bool bHasGallery = page.Page.PageActions.Where(p => (p is ImageActionItem) && p.id=="img1").Count()>1;
                    bool bHasGallery = m_contentBrowser.HasImageGallery;
                    if (!bHasGallery && cnt > 1)
                    {
                        barSelectImageId.Enabled = true;
                        repositoryItemSpinEdit1.MaxValue = cnt;
                        barSelectImageId.EditValue = (int)1;
                    }

                    //if (cnt == 1)
                    //    ShowTransparentFrame(imgItem);

                }
                else
                {
                    barEditHeight.Enabled = false;
                    barEditWidth.Enabled = false;
                    barButtonLeft.Enabled = false;
                    barButtonRight.Enabled = false;
                    barButtonUp.Enabled = false;
                    barButtonDown.Enabled = false;
                }

                ribbonPageGroupNotices.ItemLinks.Clear();
                ribbonPageGroupPDF.ItemLinks.Clear();
                ribbonPageGroupPPT.ItemLinks.Clear();
                ribbonPageGroupxAPI.ItemLinks.Clear();

                foreach (var p in page.Page.PageActions)
                    if (p is DocumentActionItem)
                    {
                        var docAction = p as DocumentActionItem;
                        if (docAction.typeId == 0)
                            AddDocumentItemToRibbon(p.id, ribbonPageGroupNotices);
                        else if (docAction.typeId == 1)
                            AddDocumentItemToRibbon(p.id, ribbonPageGroupPDF);
                        else if (docAction.typeId == 2)
                            AddDocumentItemToRibbon(p.id, ribbonPageGroupPPT);
                        else if (docAction.typeId == 3)
                            AddDocumentItemToRibbon(p.id, ribbonPageGroupxAPI);
                    }
            }
            else
            {
                barEditHeight.Enabled = false;
                barEditWidth.Enabled = false;
                barButtonLeft.Enabled = false;
                barButtonRight.Enabled = false;
                barButtonUp.Enabled = false;
                barButtonDown.Enabled = false;
                ribbonPageGroupNotices.ItemLinks.Clear();
                ribbonPageGroupPDF.ItemLinks.Clear();
            }
        }

        /*
        private void ShowTransparentFrame(ImageActionItem imgItem)
        {
            var ptOffest = new System.Drawing.Size(120,100);
            var imgPos = new System.Drawing.Point(ImageActionItem.DefaultImgPosX + imgItem.left,
                                                  ImageActionItem.DefaultImgPosY + imgItem.top);
            imgPos = imgPos + ptOffest;
            this.transparentFrameControl1.Initialize(imgPos.X, imgPos.Y, imgItem.width, imgItem.height, imgItem);
            this.transparentFrameControl1.Show();
            this.transparentFrameControl1.BringToFront();
            int id=panContentBrowser.Controls.GetChildIndex(transparentFrameControl1);
            Console.WriteLine(id.ToString());
            this.transparentFrameControl1.FrameChanged += frmImgFrame1_FrameChanged;
        }

        void frmImgFrame1_FrameChanged(object sender, ref TransparentFrameEventArgs ea)
        {
            if (m_actPage != null && m_actPage.PageActions != null)
                for (int i = 0; i < m_actPage.PageActions.Count(); ++i)
                {
                    var a = m_actPage.PageActions[i];
                    if (a is ImageActionItem)
                    {
                        var imgItem = a as ImageActionItem;
                        if (imgItem == (ea.Context as ImageActionItem))
                        {
                            imgItem.left += ea.DeltaPos.X;
                            imgItem.top += ea.DeltaPos.Y;
                            AppHandler.LibManager.SetPageAction(m_actPage, a, i);
                            AppHandler.LibManager.SetModified(SelectedOverview);
                            m_contentBrowser.DHTMLParser.SetDivAt("image1", ImageActionItem.DefaultImgPosX + imgItem.left, ImageActionItem.DefaultImgPosY + imgItem.top);
                        }
                    }
                }
        }
        */
        private void UpdateQuestionEditTabPage(QuestionEditTabPage page)
        {
            if (m_actQuestion != null)
            {
                if (m_actQuestion.type == "MultipleChoice")
                {
                    btnTest.Enabled = m_actQuestion.question.Length > 0 && m_actQuestion.Answers != null &&
                                      m_actQuestion.Answers.Length > 0;
                }
                else if (m_actQuestion.type == "Completion")
                {
                    btnTest.Enabled = false;
                }
            }
        }

        private int ZoomImageItem(ImageActionItem imgItem, int iTrackbarValue, ImgZoomType eZoomType)
        {
            Size szAct = new Size(imgItem.width, imgItem.height);
            Size szOrig = new Size(imgItem.origwidth > 0 ? imgItem.origwidth : szAct.Width, imgItem.origheight > 0 ? imgItem.origheight : szAct.Height);

            if (eZoomType == ImgZoomType.X)
            {
                double dScale = 1 + ((double)(iTrackbarValue - 50) / (double)100);
                imgItem.width = (int)(szOrig.Width * dScale);
            }
            else if (eZoomType == ImgZoomType.Y)
            {
                double dScale = 1 + ((double)(iTrackbarValue - 50) / (double)100);
                imgItem.height = (int)(szOrig.Height * dScale);
            }
            else if (eZoomType == ImgZoomType.CalcWidth)
            {
                double scale = ((double)szAct.Width / (double)szOrig.Width) - 1;
                return 50 + (int)(scale * 100);
            }
            else if (eZoomType == ImgZoomType.CalcHeight)
            {
                double scale = ((double)szAct.Height / (double)szOrig.Height) - 1;
                return 50 + (int)(scale * 100);
            }

            return 0;
        }

        private bool SelectFileForContent(string subDir, ref string _strFileName, ref Size szOrigSize)
        {
            bool bFileFound = (_strFileName.Length > 0 && File.Exists(_strFileName));
            if (!bFileFound)
            {
                if (opFileDlgContent.ShowDialog() == DialogResult.OK)
                {
                    _strFileName = opFileDlgContent.FileName;

                    if (!FileAndDirectoryHelpers.IsValidFileName(Path.GetFileName(_strFileName), true))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Filename_contains_invalid_chars", "Der Dateiname enthält ungültige Zeichen!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK);
                        return false;
                    }

                    if (subDir == "pictures")
                    {
                        Bitmap img = new Bitmap(_strFileName);
                        szOrigSize.Width = img.Width;
                        szOrigSize.Height = img.Height;
                    }
                    bFileFound = true;
                }
            }

            if (bFileFound)
            {
                string strDirName = this.m_contentBrowser.ContentFolder + "\\" + subDir;
                string strNewfilePath = strDirName + "\\" + Path.GetFileName(_strFileName);
                string strFileName = Path.GetFileName(_strFileName);
                bool doIt = false;

                if (_strFileName.ToLower() != strNewfilePath.ToLower())
                {
                    doIt = true;
                    if (File.Exists(strNewfilePath))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "the_file_X_already_exists", "Die Datei {0} existiert bereits. Bestehende Überschreiben?");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        String sTxt = String.Format(txt, strFileName);
                        doIt = (MessageBox.Show(sTxt, cap, MessageBoxButtons.YesNo) == DialogResult.Yes);
                    }
                }

                if (doIt)
                {
                    try
                    {
                        if (!Directory.Exists(strDirName))
                            Directory.CreateDirectory(strDirName);

                        File.Copy(_strFileName, strNewfilePath, true);
                        _strFileName = strFileName;
                        return true;
                    }
                    catch (System.Exception ex)
                    {
                        string stxt = ex.Message;
                        return false;
                    }
                }
                _strFileName = strFileName;
                return true;
            }
            return false;
        }

        private void AddDocumentItemToRibbon(string title,RibbonPageGroup ribbonPageGroup)
        {
            BarButtonItem btnItem = new BarButtonItem();
            btnItem.Caption = title;
            btnItem.Name = title;
            btnItem.ButtonStyle = BarButtonStyle.Check;
            btnItem.ItemDoubleClick += barbtnItemRibbon_ItemDoubleClick;
            btnItem.DownChanged += barbtnItemRibbon_DownChanged;

            if (ribbonPageGroup == ribbonPageGroupNotices)
                btnItem.LargeGlyph = rh.GetIcon("notes2").ToBitmap();
            else if (ribbonPageGroup==ribbonPageGroupPDF)
                btnItem.LargeGlyph = rh.GetIcon("file_pdf").ToBitmap();
            else if (ribbonPageGroup==ribbonPageGroupPPT)
                btnItem.LargeGlyph = rh.GetIcon("file_pptx").ToBitmap();
            else if (ribbonPageGroup == ribbonPageGroupxAPI)
                btnItem.LargeGlyph = rh.GetIcon("file_xapi").ToBitmap();

            ribbonControl1.Items.Add(btnItem);
            ribbonPageGroup.ItemLinks.Add(btnItem);
        }

        private void AddNewDocument(string strFilePath)
        {
            var tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
            DocumentActionItem docItem = null;
            if (File.Exists(strFilePath))
            {
                string strExt = System.IO.Path.GetExtension(strFilePath).ToLower();
                string strFileName = System.IO.Path.GetFileName(strFilePath);
                if (strExt == ".rtf" || strExt == ".pdf" || strExt == ".pptx")
                {
                    if (strExt.ToLower() == ".rtf" && tabPage.HasDocumentPageAction(0))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "only_one_task_allowed", "Pro Seite ist nur eine Aufgabe möglich, und die ist bereits definiert!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK);
                        return;
                    }


                    var dlg = new FrmNewNotice(false);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (tabPage.HasDocumentPageAction(dlg.Title))
                        {
                            string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "document_already_exists", "Ein Dokument mit diesem Title ist bereits definiert!");
                            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            MessageBox.Show(txt, cap, MessageBoxButtons.OK);
                            return;
                        }
                        else
                            docItem = new DocumentActionItem(dlg.Title, strFileName, strExt == ".rtf" ? 0 : (strExt==".pdf" ? 1 : 2), true);
                    }
                }
                else
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "invalid_file_extension", "unbekannter Dateityp!");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.YesNo);
                }

                if (docItem != null)
                {
                    opFileDlgDocuments.InitialDirectory = AppHandler.ContentEditMediaFolder;
                    opFileDlgDocuments.FilterIndex = 0;

                    Size strSize = new Size(0, 0);
                    if (SelectFileForContent("docs", ref strFilePath, ref strSize))
                    {
                        tabPage.AddPageAction(docItem);
                        switch (docItem.typeId)
                        {
                            case 0: AddDocumentItemToRibbon(docItem.id, ribbonPageGroupNotices); break;
                            case 1: AddDocumentItemToRibbon(docItem.id, ribbonPageGroupPDF); break;
                            case 2: AddDocumentItemToRibbon(docItem.id, ribbonPageGroupPPT); break;
                            case 3: AddDocumentItemToRibbon(docItem.id, ribbonPageGroupxAPI); break;
                            default: break;
                        }
                       }
                }
            }
        }

        private void AddNewContent(bool bPrev)
        {
            XContentTree.NodeType eNodeType;
            string strTitle;
            if (xContentTree1.AnalyseContentNode(out eNodeType, out strTitle))
            {
                string strPath = xContentTree1.ContentList.GetPath(xContentTree1.FocusedNode.Id);
                string strTxt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_really_want_to_insert_x", "Möchten Sie wirklich {0} einfügen?");
                string strCap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                string strText = "";

                switch (eNodeType)
                {
                    case XContentTree.NodeType.Library: strText = String.Format(strTxt, String.Format("die Bibliothek '{0}'", txtOverviewTitle.Text)); break;
                    case XContentTree.NodeType.Book: strText = String.Format(strTxt, String.Format("das Buch '{0}'", txtOverviewTitle.Text)); break;
                    case XContentTree.NodeType.Chapter: strText = String.Format(strTxt, String.Format("das Kapitel '{0}'", txtOverviewTitle.Text)); break;
                    case XContentTree.NodeType.Point: strText = String.Format(strTxt, String.Format("den Punkt '{0}'", txtOverviewTitle.Text)); break;
                }

                if (MessageBox.Show(strText, strCap, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool bDoIt=true;
                    if (eNodeType == XContentTree.NodeType.Point)
                        AppHandler.LibManager.AddPoint(strPath, txtOverviewTitle.Text, bPrev);
                    else if (eNodeType == XContentTree.NodeType.Chapter)
                    {
                        AppHandler.LibManager.AddChapter(strPath, txtOverviewTitle.Text, bPrev);
                        AppHandler.LibManager.AddPoint(strPath+@"\\Punkt1", "Punkt 1", bPrev);
                    }
                    else if (eNodeType == XContentTree.NodeType.Book)
                        AppHandler.LibManager.AddBook(strPath, txtOverviewTitle.Text, bPrev);
                    else if (eNodeType == XContentTree.NodeType.Library)
                    {
                        if (AppHandler.LibManager.GetLibrary(txtOverviewTitle.Text) != null)
                        {
                            MessageBox.Show(String.Format("Die Bibliothek '{0}' existiert bereits", txtOverviewTitle.Text));
                            bDoIt = false;
                        }
                        else
                        {
                            AppHelpers.CreateLibrary(txtOverviewTitle.Text, bPrev);
                            strPath = txtOverviewTitle.Text;
                        }
                    }

                    if (bDoIt)
                    {
                        AppHandler.LibManager.Reload(strPath);
                        xContentTree1.FillTree();
                        xContentTree1.SelectItem(strPath);
                    }
                }
            }
        }

        private string strGetActionFullPath(string strFileName)
        {
            string strResult = m_contentBrowser.ContentFolder + '/' + strFileName;
            strResult = strResult.Replace("//", "\\");
            strResult = strResult.Replace('/', '\\');
            return strResult;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////
        // implement IContentNavigationCtrl
        public void SetPageId(int iPageId)
        {
        }

        public int GetPageId()
        {
            return this.m_iActPageId + 1;
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

        public void AddDocument(string strTitle,int typeId)
        {
        }

        public void ClearDocuments()
        {
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // Form delegates
        private void FrmEditContent_Closed(object sender, System.EventArgs e)
		{
			AppHandler.ContentManager.ContentEditorClosed();
		}

        ///////////////////////////////////////////////////////////////////////////////////////////////
        // button delegates
        private void btnOverview_Click(object sender, System.EventArgs e)
		{
			this.SwitchEditMode(EditMode.Overview);
		}

		private void btnContent_Click(object sender, System.EventArgs e)
		{
			this.SwitchEditMode(EditMode.Content);
		}

		private void btnQuestions_Click(object sender, System.EventArgs e)
		{
			this.SwitchEditMode(EditMode.Questions);
		}

        private void btnOverviewInsertPrev_Click(object sender, System.EventArgs e)
        {
            if (this.txtOverviewTitle.Text.Length>0)
                AddNewContent(false);
        }

        private void btnOverviewDelete_Click(object sender, System.EventArgs e)
        {
            XContentTree.NodeType eNodeType;
            string strTitle;
            if (xContentTree1.AnalyseContentNode(out eNodeType, out strTitle))
            {
                string strTxt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_really_want_to_delete", "Möchten Sie wirklich {0} löschen?");
                string strCap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                string strText = "";

                switch (eNodeType)
                {
                    case XContentTree.NodeType.Library: strText = String.Format(strTxt, String.Format("die gesamte Bibliothek '{0}'", strTitle)); break;
                    case XContentTree.NodeType.Book: strText = String.Format(strTxt, String.Format("das gesamte Buch '{0}'", strTitle)); break;
                    case XContentTree.NodeType.Chapter: strText = String.Format(strTxt, String.Format("das gesamte Kapitel '{0}'", strTitle)); break;
                    case XContentTree.NodeType.Point: strText = String.Format(strTxt, String.Format("den gesamten Punkt '{0}'", strTitle)); break;
                }

                if (MessageBox.Show(strText, strCap, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string sPath = xContentTree1.ContentList.GetPath(xContentTree1.FocusedNode.Id);
                    bool bResult = true;
                    bool bOnlyOneLeft = false;
                    if (eNodeType== XContentTree.NodeType.Point)
                    {
                        bResult = AppHandler.LibManager.DeletePoint(sPath);
                        bOnlyOneLeft = AppHandler.LibManager.GetPointCnt(sPath) == 1;
                    }
                    else if (eNodeType==XContentTree.NodeType.Chapter)
                    {
                        bResult = AppHandler.LibManager.DeleteChapter(sPath);
                        bOnlyOneLeft = AppHandler.LibManager.GetChapterCnt(sPath) == 1;
                    }
                    else if (eNodeType == XContentTree.NodeType.Book)
                    {
                        bResult = AppHandler.LibManager.DeleteBook(sPath);
                        bOnlyOneLeft = AppHandler.LibManager.GetBookCnt(sPath) == 1;
                    }

                    if (bResult)
                    {
                        if (eNodeType == XContentTree.NodeType.Library)
                        {
                            AppHelpers.DestroyLibrary(strTitle);
                        }
                        else
                        {
                            AppHandler.LibManager.Reload(sPath);
                        }

                        xContentTree1.FillTree();
                        xContentTree1.SelectItem(sPath);
                        btnOverviewDelete.Enabled = !bOnlyOneLeft;
                    }
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (editMode == EditMode.Questions)
            {
                if (m_strSelectedOverview.Length > 0)
                {
                    var newQu=new QuestionItem("qtempl_a_frm.htm", "MultipleChoice", "question?", 0, true, true, true);
                    int iQuCnt;
                    AppHandler.LibManager.AddQuestion(m_strSelectedOverview, newQu);
                    AppHandler.LibManager.GetQuestionCnt(m_strSelectedOverview, out iQuCnt);

                    ImageActionItem imgItem = new ImageActionItem("img1", "Templates/img/question.gif", 0, 0, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, true);

                    AppHandler.LibManager.AddQuestionAction(newQu, imgItem);
                    AppHandler.LibManager.SetQuestion(m_strSelectedOverview, newQu, iQuCnt-1);
                    AppHandler.LibManager.SetModified(m_strSelectedOverview);

                    UpdateQuestionMode();
                    xtraTabCtrl_Questions.SelectedTabPageIndex = iQuCnt - 1;
                }
            }
            else if (editMode == EditMode.Content)
            {
                if (m_strSelectedOverview.Length > 0)
                {
                    PageItem pi = new PageItem();
                    if (ContentEditTabPage.CreatePageItem(-1, ref pi))
                    {
                        AppHandler.LibManager.AddPage(m_strSelectedOverview, pi);
                        UpdateContentMode(-1);
                    }
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DevExpress.XtraTab.XtraTabControl tabCtrl = null;
            if (editMode == EditMode.Content && xtraTabCtrl_Content.SelectedTabPage != null)
            {
                tabCtrl = xtraTabCtrl_Content;
            }
            else if (editMode == EditMode.Questions && xtraTabCtrl_Questions.SelectedTabPage != null)
            {
                tabCtrl = xtraTabCtrl_Questions;
            }

            if (tabCtrl.SelectedTabPage != null)
            {
                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_really_want_to_delete_x", "Möchten Sie wirklich {0} löschen?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                String sTxt = String.Format(txt, tabCtrl.SelectedTabPage.Text);
                bool doIt = (MessageBox.Show(sTxt, cap, MessageBoxButtons.YesNo) == DialogResult.Yes);

                if (doIt)
                {
                    if (editMode == EditMode.Questions)
                    {
                        AppHandler.LibManager.DeleteQuestion(m_strSelectedOverview, tabCtrl.SelectedTabPageIndex);
                        UpdateQuestionMode();
                    }
                    else if (editMode == EditMode.Content)
                    {
                        AppHandler.LibManager.DeletePage(m_strSelectedOverview, tabCtrl.SelectedTabPageIndex);
                        UpdateContentMode(-1);
                    }
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            bool isRight = m_contentBrowser.CheckActualQuestion();
            string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "The_answer_is", "Die Antwort ist {0}!");
            String sTxt = String.Format(txt, isRight ? "richtig" : "falsch");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            MessageBox.Show(sTxt, cap, MessageBoxButtons.OK);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool doIt=true;
            if (editMode == EditMode.Questions && xtraTabCtrl_Questions.SelectedTabPage != null)
            {
                var tabPage = xtraTabCtrl_Questions.SelectedTabPage.Controls[0] as QuestionEditTabPage;
                doIt = tabPage.SaveQuestion();
                m_contentBrowser.ShowPage();
            }

            if (doIt)
                AppHandler.LibManager.Reload(m_strSelectedOverview);
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Folder für Inhalte bestimmen
            string strTemplatePath = AppHandler.ContentFolder + '\\' + AppHandler.LibManager.GetFileName(m_strSelectedOverview) + "\\Templates\\";
            string strTemplateFilename = Utilities.GetTemplateName(m_actPage.templateName) + ".htm";
            var editor = new ContentEditPageEditor(strTemplatePath, strTemplateFilename, m_actPage);
            bool? bRes = editor.Show();
            if (bRes.HasValue)
            {

            }
            var pt = AppHandler.LibManager.GetPoint(m_strSelectedOverview);
            AppHandler.LibManager.SetPage(m_strSelectedOverview, m_actPage, m_iActPageId);
            AppHandler.LibManager.SetModified(m_strSelectedOverview);
            UpdateContentMode(m_iActPageId);

            if (xtraTabCtrl_Content.SelectedTabPage != null)
                ShowPage(xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage);
        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            if (xtraTabCtrl_Questions.SelectedTabPage != null)
            {
                var tabPage = xtraTabCtrl_Questions.SelectedTabPage.Controls[0] as QuestionEditTabPage;
                tabPage.AddQuestion();
            }
        }

        private void btnDelQuestion_Click(object sender, EventArgs e)
        {
            if (xtraTabCtrl_Questions.SelectedTabPage != null)
            {
                var tabPage = xtraTabCtrl_Questions.SelectedTabPage.Controls[0] as QuestionEditTabPage;
                tabPage.DeleteQuestion();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////

        private void xContentTree1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
            if (e.OldNode == null)
            {
                SelectedOverview = xContentTree1.GetFirstPoint();
                xContentTree1.SelectItem(m_strSelectedOverview);
                return;
            }

            XContentTree.NodeType eNodeType;
            string strTitle;
            int iPageCnt;
            if (xContentTree1.AnalyseContentNode(out eNodeType, out strTitle))
            {
                string strPath = xContentTree1.ContentList.GetPath(e.Node.Id);
                if (editMode == EditMode.Overview)
                {
                    if (eNodeType == XContentTree.NodeType.Point)
                    {
                        txtOverviewType.Text = "Punkt";
                        txtOverviewTitle.Text = strTitle;
                        btnOverviewDelete.Enabled = AppHandler.LibManager.GetPointCnt(strPath) > 1;
                        btnContent.Enabled = true;
                        btnQuestions.Enabled = true;
                        btnExport.Enabled = false;
                    }
                    else if (eNodeType == XContentTree.NodeType.Chapter)
                    {
                        txtOverviewType.Text = "Kapitel";
                        txtOverviewTitle.Text = strTitle;
                        btnOverviewDelete.Enabled = AppHandler.LibManager.GetChapterCnt(strPath) > 1;
                        btnContent.Enabled = false;
                        btnQuestions.Enabled = false;
                        btnExport.Enabled = false;
                    }
                    else if (eNodeType == XContentTree.NodeType.Book)
                    {
                        txtOverviewType.Text = "Buch";
                        txtOverviewTitle.Text = strTitle;
                        btnOverviewDelete.Enabled = AppHandler.LibManager.GetBookCnt(strPath) > 1;
                        btnContent.Enabled = false;
                        btnQuestions.Enabled = false;
                        btnExport.Enabled = false;
                    }
                    else if (eNodeType == XContentTree.NodeType.Library)
                    {
                        txtOverviewType.Text = "Bibliothek";
                        txtOverviewTitle.Text = strTitle;
                        btnOverviewDelete.Enabled = true;
                        btnContent.Enabled = false;
                        btnQuestions.Enabled = false;
                        btnExport.Enabled = true;
                    }

                    string[] aTitles;
                    Utilities.SplitPath(strPath, out aTitles);
                    LibraryItem lib = AppHandler.LibManager.GetLibrary(aTitles[0]);
                    txtVersion.Text = lib.version;
                    AppHandler.LibManager.GetPageCnt(strPath, out iPageCnt);
                    txtOvwPageCount.Text = iPageCnt.ToString();
                    SelectedOverview = strPath;
                }
            }
        }

        private void xContentTree1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (btnDel.Enabled)
                    btnOverviewDelete_Click(this, new EventArgs());
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
            if (e.Page != null && e.Page.Controls != null && e.Page.Controls[0] != null)
            {
                var page = e.Page.Controls[0] as ContentEditTabPage;
                if (page!=null && page.SelectedTemplateId >= 0)
                {
                    m_actPage = page.Page;
                    m_iActPageId = page.PageId;
                    UpdateContentEditTabPage(page);
                    ShowPage(page);
                    btnDuplicate.Enabled = true;
                }
            }
		}

		private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
            if (e.Page != null && e.Page.Controls != null && e.Page.Controls[0] != null)
            {
                var page = e.Page.Controls[0] as QuestionEditTabPage;
                if (page != null)
                {
                    m_actQuestion = page.Question;
                    m_iActQuestionId = page.QuestionId;
                    UpdateQuestionEditTabPage(page);
                    ShowPage(page);
                    btnDuplicateQu.Enabled = true;
                }
            }
		}

        private void rgbiTemplates_GalleryInitDropDownGallery(object sender, InplaceGalleryEventArgs e)
        {
            e.PopupGallery.SynchWithInRibbonGallery = true;
        }

        private void ribbonGalleryBarItemTemplates_GalleryPopupClose(object sender, InplaceGalleryEventArgs e)
        {
            List<GalleryItem> l = e.PopupGallery.GetCheckedItems();
            if (l.Count == 1)
            {
                GalleryItem selItem = this.ribbonGalleryBarItemTemplates.Gallery.GetItemByCaption(l[0].Caption);

                var tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
                if (tabPage.SelectedTemplateId != selItem.ImageIndex)
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_really_want_to_change_template");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        selItem = this.ribbonGalleryBarItemTemplates.Gallery.Groups[0].Items[selItem.ImageIndex];
                        this.ribbonGalleryBarItemTemplates.Gallery.SetItemCheck(selItem, true);
                        this.ribbonGalleryBarItemTemplates.Gallery.MakeVisible(selItem);
                        tabPage.SelectedTemplateId = selItem.ImageIndex;
                        m_actPage = tabPage.Page;
                    }
                }
            }
        }

        private void ribbonGalleryBarItemTemplates_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            if (xtraTabCtrl_Content.SelectedTabPage != null)
            {
                var tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
                if (e.Item.Checked)
                {
                    if (tabPage.SelectedTemplateId != e.Item.ImageIndex)
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_really_want_to_change_template");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            GalleryItem selItem = e.Gallery.Groups[0].Items[tabPage.SelectedTemplateId];
                            e.Gallery.SetItemCheck(selItem, true);
                            e.Gallery.MakeVisible(selItem);
                        }
                        else
                            tabPage.SelectedTemplateId = e.Item.ImageIndex;
                    }
                }
            }
        }

        private void ribbonControl1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && xtraTabCtrl_Content.SelectedTabPage != null)
            {
                RibbonControl ribbon = sender as RibbonControl;
                if (ribbon.SelectedPage == ribbonPageDocuments)
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void ribbonControl1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "only_single_file_allowed", "Sie können Dateien nur einzeln hinzufügen");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.YesNo);
            }
            else
            {
                AddNewDocument(files[0]);
            }
        }


        private void barButtonLeft_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (editMode == EditMode.Content)
            {
                ImageActionItem imgItem;
                if (barSelectImageId.Enabled)
                    imgItem = m_contentBrowser.GetImageItem(Convert.ToInt32(barSelectImageId.EditValue)-1);
                else
                    imgItem = m_contentBrowser.GetImageItem(0);

                imgItem.left -= 5;
                m_contentBrowser.ReplaceImageRegion(imgItem);
                for (int i = 0; i < m_actPage.PageActions.Count(); ++i)
                    if (m_actPage.PageActions[i].id == imgItem.id)
                    {
                        AppHandler.LibManager.SetPageAction(m_actPage, m_actPage.PageActions[i], i);
                        AppHandler.LibManager.SetModified(SelectedOverview);
                    }
                //this.transparentFrameControl1.MoveByDelta(-5, 0);
            }
        }

        private void barButtonRight_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (editMode == EditMode.Content)
            {
                ImageActionItem imgItem;
                if (barSelectImageId.Enabled)
                    imgItem = m_contentBrowser.GetImageItem(Convert.ToInt32(barSelectImageId.EditValue) - 1);
                else
                    imgItem = m_contentBrowser.GetImageItem(0);

                imgItem.left += 5;
                m_contentBrowser.ReplaceImageRegion(imgItem);
                for (int i = 0; i < m_actPage.PageActions.Count(); ++i)
                    if (m_actPage.PageActions[i].id == imgItem.id)
                    {
                        AppHandler.LibManager.SetPageAction(m_actPage, m_actPage.PageActions[i], i);
                        AppHandler.LibManager.SetModified(SelectedOverview);
                    }
                //this.transparentFrameControl1.MoveByDelta(5, 0);
            }
        }

        private void barButtonUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (editMode == EditMode.Content)
            {
                ImageActionItem imgItem;
                if (barSelectImageId.Enabled)
                    imgItem = m_contentBrowser.GetImageItem(Convert.ToInt32(barSelectImageId.EditValue) - 1);
                else
                    imgItem = m_contentBrowser.GetImageItem(0);

                imgItem.top -= 5;
                m_contentBrowser.ReplaceImageRegion(imgItem);
                for (int i = 0; i < m_actPage.PageActions.Count(); ++i)
                    if (m_actPage.PageActions[i].id == imgItem.id)
                    {
                        AppHandler.LibManager.SetPageAction(m_actPage, m_actPage.PageActions[i], i);
                        AppHandler.LibManager.SetModified(SelectedOverview);
                    }

                //this.transparentFrameControl1.MoveByDelta(0, -5);
            }

        }

        private void barButtonDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (editMode == EditMode.Content)
            {
                ImageActionItem imgItem;
                if (barSelectImageId.Enabled)
                    imgItem = m_contentBrowser.GetImageItem(Convert.ToInt32(barSelectImageId.EditValue) - 1);
                else
                    imgItem = m_contentBrowser.GetImageItem(0);

                imgItem.top += 5;
                m_contentBrowser.ReplaceImageRegion(imgItem);
                for (int i = 0; i < m_actPage.PageActions.Count(); ++i)
                    if (m_actPage.PageActions[i].id == imgItem.id)
                    {
                        AppHandler.LibManager.SetPageAction(m_actPage, m_actPage.PageActions[i], i);
                        AppHandler.LibManager.SetModified(SelectedOverview);
                    }
                            
                //this.transparentFrameControl1.MoveByDelta(0, 5);
            }

        }

        private void repositoryItemZoomTrackBar1_EditValueChanged(object sender, EventArgs e)
        {
            if (editMode == EditMode.Content)
            {
                var trackBarCtrl = sender as DevExpress.XtraEditors.TrackBarControl;
                ImageActionItem imgItem;
                if (barSelectImageId.Enabled)
                    imgItem = m_contentBrowser.GetImageItem(Convert.ToInt32(barSelectImageId.EditValue) - 1);
                else
                    imgItem = m_contentBrowser.GetImageItem(0);

                ZoomImageItem(imgItem, trackBarCtrl.Value, ImgZoomType.X);
                if (barCheckProp.Checked)
                {
                    ZoomImageItem(imgItem, trackBarCtrl.Value, ImgZoomType.X);
                    ZoomImageItem(imgItem, trackBarCtrl.Value, ImgZoomType.Y);
                    barEditHeight.EditValue = trackBarCtrl.Value;
                }

                m_contentBrowser.ChangeImageRegion(imgItem);

                for (int i = 0; i < m_actPage.PageActions.Count(); ++i)
                    if (m_actPage.PageActions[i].id == imgItem.id)
                    {
                        AppHandler.LibManager.SetPageAction(m_actPage, m_actPage.PageActions[i], i);
                        AppHandler.LibManager.SetModified(SelectedOverview);
                    }
            }
        }

        private void repositoryItemZoomTrackBar2_EditValueChanged(object sender, EventArgs e)
        {
            if (editMode == EditMode.Content)
            {
                var trackBarCtrl = sender as DevExpress.XtraEditors.TrackBarControl;
                ImageActionItem imgItem;
                if (barSelectImageId.Enabled)
                    imgItem = m_contentBrowser.GetImageItem(Convert.ToInt32(barSelectImageId.EditValue) - 1);
                else
                    imgItem = m_contentBrowser.GetImageItem(0);

                ZoomImageItem(imgItem, trackBarCtrl.Value, ImgZoomType.Y);
                m_contentBrowser.ChangeImageRegion(imgItem);

                for (int i = 0; i < m_actPage.PageActions.Count(); ++i)
                    if (m_actPage.PageActions[i].id == imgItem.id)
                    {
                        AppHandler.LibManager.SetPageAction(m_actPage, m_actPage.PageActions[i], i);
                        AppHandler.LibManager.SetModified(SelectedOverview);
                    }
            }
        }

        private void barChooseQuestionImage_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            if (xtraTabCtrl_Questions.SelectedTabPage != null)
            {
                string txt;
                string cap;
                QuestionEditTabPage tabPage;
                switch (e.Index)
                {
                    case 0: // set standard picture
                        txt = AppHandler.LanguageHandler.GetText("MESSAGE", "add_standard_question_image", "Standardbild (Fragezeichen) zur Frage setzen?");
                        cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            tabPage = xtraTabCtrl_Questions.SelectedTabPage.Controls[0] as QuestionEditTabPage;
                            ImageActionItem imgItem = new ImageActionItem("img1", "Templates/img/question.gif", 0, 0, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, true);
                            tabPage.SetQuestionImage(imgItem);
                            m_contentBrowser.ShowPage();
                        }
                        break;
                    case 1: // delete standard picture
                        txt = AppHandler.LanguageHandler.GetText("MESSAGE", "delete_question_image", "Aktuelles Bild zur Frage wirklich löschen?");
                        cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            tabPage = xtraTabCtrl_Questions.SelectedTabPage.Controls[0] as QuestionEditTabPage;
                            tabPage.SetQuestionImage(null);
                        }
                        break;
                    case 2: // set custom picture
                        tabPage = xtraTabCtrl_Questions.SelectedTabPage.Controls[0] as QuestionEditTabPage;
                        bool doIt = true;
                        if (tabPage.HasQuestionImage())
                        {
                            txt = AppHandler.LanguageHandler.GetText("MESSAGE", "question_image_already_set", "Die Frage hat bereits ein Bild. Bestehendes Überschreiben?");
                            cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            doIt = (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo) == DialogResult.Yes);
                        }

                        if (doIt)
                        {
                            opFileDlgContent.InitialDirectory = AppHandler.ContentEditMediaFolder;
                            opFileDlgContent.Filter = "Alle Dateien (*.*)|*.*|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                            opFileDlgContent.FilterIndex = 0;
                            string strFileName = "";
                            Size szOrigSize = new Size(0, 0);
                            if (SelectFileForContent("pictures", ref strFileName, ref szOrigSize))
                            {
                                double dRatio = (double)szOrigSize.Width / (double)szOrigSize.Height;
                                Size szNew = new Size(ImageActionItem.DefaultImgWidthQu, (int)((double)ImageActionItem.DefaultImgWidthQu / dRatio));
                                ImageActionItem imgItem = new ImageActionItem("img1", "pictures//" + strFileName, 0, 0, szNew.Width, szNew.Height, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, true);
                                tabPage.SetQuestionImage(imgItem);
                            }
                        }
                        break;
                }
            }
        }

        private void barNewActionItems_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            if (xtraTabCtrl_Content.SelectedTabPage != null)
            {
                ContentEditTabPage tabPage;
                string strFileName;
                Size szOrigSize;
                switch (e.Index)
                {
                    case 0: // add imageactionitem
                        opFileDlgContent.InitialDirectory = AppHandler.ContentEditMediaFolder;
                        opFileDlgContent.Filter = "Alle Dateien (*.*)|*.*|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                        opFileDlgContent.FilterIndex = 1;
                        strFileName = "";
                        szOrigSize = new Size(0, 0);
                        if (SelectFileForContent("pictures", ref strFileName, ref szOrigSize))
                        {
                            bool bUseGallery=m_contentBrowser.HasImageGallery;
                            if (!bUseGallery && m_contentBrowser.GetImageCount()==1)
                            {
                                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "do_you_want_to_create_a_gallery", "Wollen sie eine Bildergallerie erstellen?");
                                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    bUseGallery = true;
                            }

                            string strItemId = "img1";
                            if (!bUseGallery)
                                strItemId = String.Format("img{0}", m_contentBrowser.GetImageCount() + 1);

                            tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
                            //double dRatio = (double)szOrigSize.Width / (double)szOrigSize.Height;
                            //Size szNew = new Size(ImageActionItem.DefaultImgWidth, (int)((double)ImageActionItem.DefaultImgWidth / dRatio));
                            //ImageActionItem imgItem = new ImageActionItem(strItemId, "pictures//" + strFileName, 0, 0, szNew.Width, szNew.Height, ImageActionItem.DefaultImgWidth, ImageActionItem.DefaultImgHeight, true);
                            ImageActionItem imgItem = new ImageActionItem(strItemId, "pictures//" + strFileName, 0, 0, szOrigSize.Width, szOrigSize.Height, szOrigSize.Width, szOrigSize.Height, true);
                            tabPage.AddPageAction(imgItem);
                            UpdateContentEditTabPage(tabPage);
                            m_actPage = tabPage.Page;
                        }
                        break;
                    case 1: // add animationactionitem
                        opFileDlgContent.InitialDirectory = AppHandler.ContentEditMediaFolder;
                        opFileDlgContent.Filter = "Alle Dateien (*.*)|*.*|Flash Animation Files (*.swf)|*.swf";
                        opFileDlgContent.FilterIndex = 1;
                        strFileName = "";
                        szOrigSize = new Size(0, 0);
                        if (SelectFileForContent("animations", ref strFileName, ref szOrigSize))
                        {
                            tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
                            double dRatio = (double)szOrigSize.Width / (double)szOrigSize.Height;
                            AnimationActionItem aniItem = new AnimationActionItem("ani1", "animations//" + strFileName, "", true, true);
                            tabPage.AddPageAction(aniItem);
                            UpdateContentEditTabPage(tabPage);
                            m_actPage = tabPage.Page;
                        }
                        break;
                    case 2: // add SimulationActionItem
                        opFileDlgContent.InitialDirectory = AppHandler.ContentEditMediaFolder;
                        opFileDlgContent.Filter = "Alle Dateien (*.*)|*.*|CNC Simulations-Dateien(*.nc)|*.nc|All files (*.*)|*.*";
                        opFileDlgContent.FilterIndex = 1;
                        strFileName = "";
                        szOrigSize = new Size(0, 0);
                        if (SelectFileForContent("simulations", ref strFileName, ref szOrigSize))
                        {
                            tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
                            SimulationActionItem simItem = new SimulationActionItem("sim1", "simulations//" + strFileName,"",false, false, true);
                            tabPage.AddPageAction(simItem);
                            UpdateContentEditTabPage(tabPage);
                            m_actPage = tabPage.Page;
                        }
                        break;
                    case 3: // add videoactionitem
                        opFileDlgContent.InitialDirectory = AppHandler.ContentEditMediaFolder;
                        opFileDlgContent.Filter = "Alle Dateien (*.*)|*.*|Video Dateien(*.avi;*.mpg;*.mpeg;*.wmv)|*.avi;*.mpg;*.mpeg;*.wmv|All files (*.*)|*.*";
                        opFileDlgContent.FilterIndex = 1;
                        strFileName = "";
                        szOrigSize = new Size(0, 0);
                        if (SelectFileForContent("videos", ref strFileName, ref szOrigSize))
                        {
                            tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
                            VideoActionItem vidItem = new VideoActionItem("vid1", "videos//" + strFileName, false, true);
                            tabPage.AddPageAction(vidItem);
                            UpdateContentEditTabPage(tabPage);
                            m_actPage = tabPage.Page;
                        }
                        break;
                    case 4: // add Keyword action
                        {
                            var frm = new XFrmEditKeywordAction(m_strSelectedOverview);
                            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
                                KeywordActionItem keywordItem = new KeywordActionItem(frm.KeyId,frm.KeyText,frm.KeyDescription,frm.IsTooltip,frm.IsGlossary,false);
                                tabPage.AddPageAction(keywordItem);
                                if (frm.IsTooltip)
                                {
                                    bool bFound = false;
                                    for (int a = 0; a < m_actPage.PageActions.Count(); ++a)
                                        if (m_actPage.PageActions[a] is TextActionItem)
                                        {
                                            var txtAction = (m_actPage.PageActions[a] as TextActionItem);
                                            string strText = txtAction.text;
                                            if (strText.IndexOf(frm.KeyText) >= 0 && (txtAction.id == "text1" || txtAction.id=="text2"))
                                            {
                                                txtAction.text = strText.Replace(frm.KeyText, String.Format("{{{{*{0}*}}}}", frm.KeyId));
                                                bFound = true;
                                            }
                                        }

                                    if (bFound)
                                    {
                                        AppHandler.LibManager.SetPage(m_strSelectedOverview, m_actPage, m_iActPageId);
                                        AppHandler.LibManager.SetModified(m_strSelectedOverview);
                                    }
                                }
                                UpdateContentEditTabPage(tabPage);
                                m_actPage = tabPage.Page;
                            }
                        }
                        break;
                }
            }
        }

        private void barbtnItemRibbon_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            if (m_actPage != null && m_actPage.PageActions != null)
            {
                foreach (var p in m_actPage.PageActions)
                    if (p is DocumentActionItem)
                    {
                        var docAction = p as DocumentActionItem;
                        if (docAction.id == e.Item.Name)
                            AppHelpers.ShowDocument(m_strSelectedOverview,docAction,true);
                    }
            }
        }
        
        private void barbtnItemRibbon_DownChanged(object sender, ItemClickEventArgs e)
        {
            bool bIsOneDown=false;
            foreach(var  i in ribbonControl1.Items)
            {
                if (i is BarButtonItem && ((BarButtonItem)i).Down)
                {
                    bIsOneDown = true;
                    break;
                }
            }
            this.barButtonItemDelDoc.Enabled = bIsOneDown;
        }

        private void barbtnItemAddDoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            opFileDlgDocuments.InitialDirectory = AppHandler.ContentEditMediaFolder;
            opFileDlgDocuments.FilterIndex = 0;
            if (opFileDlgDocuments.ShowDialog() == DialogResult.OK)
                AddNewDocument(opFileDlgDocuments.FileName);
        }

        private void barbtnItemDelDoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabCtrl_Content.SelectedTabPage != null)
            {
                var tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;

                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "do_you_really_want_to_delete_all_selected_documents", "Wollen sie wirklich alle ausgewählten Dokumente entfernen?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                bool bIsDone = false;
                while (!bIsDone)
                {
                    bool bHasFound = false;
                    foreach (var i in ribbonControl1.Items)
                    {
                        if ((i is BarButtonItem) && ((BarButtonItem)i).Down)
                        {
                            this.ribbonPageGroupNotices.ItemLinks.Remove((BarItem)i);
                            this.ribbonPageGroupPDF.ItemLinks.Remove((BarItem)i);
                            ribbonControl1.Items.Remove((BarItem)i);
                            tabPage.DelDocumentPageAction(((BarItem)i).Name);
                            bHasFound = true;
                            break;
                        }
                    }
                    if (!bHasFound)
                        bIsDone = true;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (m_strSelectedOverview.Length > 0)
            {
                string[] aTitles = null;
                if (Utilities.SplitPath(m_strSelectedOverview, out aTitles))
                {
                    AppHelpers.ExportLibrary(aTitles[0]);
                }
            }

        }
    
	    private void btnImport_Click(object sender, EventArgs e)
        {
            if (editMode == EditMode.Overview)
            {
                var frm = new XFrmImportContentModule();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //xContentTree1.FillTree();
                    //xContentTree1.SelectItem(SelectedOverview);
                }
            }
            else if (editMode == EditMode.Content)
            {
                var frm = new XFrmImportQuestions();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var qu in frm.Questions)
                    {
                        qu.templateName = "qtempl_a_frm.htm";
                        qu.type = "MultipleChoice";

                        int iQuCnt;
                        AppHandler.LibManager.AddQuestion(m_strSelectedOverview, qu);
                        AppHandler.LibManager.GetQuestionCnt(m_strSelectedOverview, out iQuCnt);

                        ImageActionItem imgItem = new ImageActionItem("img1", "Templates/img/question.gif", 0, 0, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, true);

                        AppHandler.LibManager.AddQuestionAction(qu, imgItem);
                        AppHandler.LibManager.SetQuestion(m_strSelectedOverview, qu, iQuCnt - 1);
                        AppHandler.LibManager.SetModified(m_strSelectedOverview);
                    }
                    UpdateQuestionMode();
                }
            }

            else if (editMode == EditMode.Questions)
            {
                var frm = new XFrmImportQuestions();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    foreach (var qu in frm.Questions)
                    {
                        qu.templateName="qtempl_a_frm.htm";
                        qu.type="MultipleChoice";

                        int iQuCnt;
                        AppHandler.LibManager.AddQuestion(m_strSelectedOverview,qu);
                        AppHandler.LibManager.GetQuestionCnt(m_strSelectedOverview, out iQuCnt);

                        ImageActionItem imgItem = new ImageActionItem("img1", "Templates/img/question.gif", 0, 0, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, ImageActionItem.DefaultImgWidthQu, ImageActionItem.DefaultImgHeightQu, true);

                        AppHandler.LibManager.AddQuestionAction(qu, imgItem);
                        AppHandler.LibManager.SetQuestion(m_strSelectedOverview, qu, iQuCnt - 1);
                        AppHandler.LibManager.SetModified(m_strSelectedOverview);
                    }
                    UpdateQuestionMode();
                }
            }
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            var frm = new XFrmSelectContent();
            frm.SelectedPath = this.m_strSelectedOverview;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strTargetPath = frm.SelectedPath;
                AppHandler.LibManager.AddPage(strTargetPath, m_actPage);
                AppHandler.LibManager.SetModified(strTargetPath);
                AppHandler.LibManager.Reload(strTargetPath);
                UpdateContentMode(-1);
            }
        }

        private void txtOverviewTitle_TextChanged(object sender, EventArgs e)
        {
            this.btnOverviewInsertPrev.Enabled = (sender as TextBox).Text.Length > 0;
        }


        private void xContentTree1_ShownEditor(object sender, EventArgs e)
        {
            /*
            bool bIsClassMap = false;
            DevExpress.XtraTreeList.TreeList tree = sender as DevExpress.XtraTreeList.TreeList;
            if (!((tree.FocusedColumn.FieldName == "UserList" && !bIsClassMap) ||
                  (tree.FocusedColumn.FieldName == "ClassList" && bIsClassMap) ||
                   tree.FocusedColumn.FieldName == "Color") ||
                  tree.FocusedNode.ParentNode != null)*/
            DevExpress.XtraTreeList.TreeList tr = sender as DevExpress.XtraTreeList.TreeList;
            tr.ActiveEditor.Enabled = true;
            tr.ActiveEditor.Font = tr.Font;
        }

        private void xContentTree1_ShowingEditor(object sender, CancelEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tr = sender as DevExpress.XtraTreeList.TreeList;
            XContentTree.NodeType eNodeType;
            string strTitle;
            if (xContentTree1.AnalyseContentNode(out eNodeType, out strTitle))
            {
                if (eNodeType == XContentTree.NodeType.Library)
                    e.Cancel = true;
            }
            else
                e.Cancel = true;
        }

        private void xContentTree1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Valid)
            {
                //var tree = sender as DevExpress.XtraTreeList.TreeList;
                XContentTree.NodeType eNodeType;
                string strTitle;
                if (xContentTree1.AnalyseContentNode(out eNodeType, out strTitle))
                {
                    string strPath = xContentTree1.ContentList.GetPath(xContentTree1.FocusedNode.Id);
                    string strTxt = AppHandler.LanguageHandler.GetText("MESSAGE", "do_you_really_want_to_rename", "Möchten Sie wirklich {0} umbenennen?");
                    string strCap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    string strText = "";

                    switch (eNodeType)
                    {
                        case XContentTree.NodeType.Library: strText = String.Format(strTxt, String.Format("die Bibliothek '{0}'", strTitle)); break;
                        case XContentTree.NodeType.Book: strText = String.Format(strTxt, String.Format("das Buch '{0}'", strTitle)); break;
                        case XContentTree.NodeType.Chapter: strText = String.Format(strTxt, String.Format("das Kapitel '{0}'", strTitle)); break;
                        case XContentTree.NodeType.Point: strText = String.Format(strTxt, String.Format("den Punkt '{0}'", strTitle)); break;
                    }

                    bool bCanChange = true;
                    if (MessageBox.Show(strText, strCap, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (eNodeType == XContentTree.NodeType.Point)
                            bCanChange = AppHandler.LibManager.SetPointTitle(strPath, e.Value.ToString());
                        else if (eNodeType == XContentTree.NodeType.Chapter)
                            bCanChange = AppHandler.LibManager.SetChapterTitle(strPath, e.Value.ToString());
                        else if (eNodeType == XContentTree.NodeType.Book)
                            bCanChange = AppHandler.LibManager.SetBookTitle(strPath, e.Value.ToString());
                        else
                            bCanChange = AppHandler.LibManager.SetLibraryTitle(strPath, e.Value.ToString());

                        if (!bCanChange)
                        {
                            strTxt = AppHandler.LanguageHandler.GetText("MESSAGE", "sorry_X_already_exists", "Sorry, {0} ist bereits vorhanden!");
                            strCap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            strText = "";

                            switch (eNodeType)
                            {
                                case XContentTree.NodeType.Library: strText = String.Format(strTxt, String.Format("Die Bibliothek '{0}'", e.Value.ToString())); break;
                                case XContentTree.NodeType.Book: strText = String.Format(strTxt, String.Format("Das Buch '{0}'", e.Value.ToString())); break;
                                case XContentTree.NodeType.Chapter: strText = String.Format(strTxt, String.Format("Das Kapitel '{0}'", e.Value.ToString())); break;
                                case XContentTree.NodeType.Point: strText = String.Format(strTxt, String.Format("Der Punkt '{0}'", e.Value.ToString())); break;
                            }
                            MessageBox.Show(strText, strCap, MessageBoxButtons.OK);
                        }
                    }
                    else
                        bCanChange = false;

                    if (bCanChange)
                    {
                        List<KeyValuePair<string, string>> dMaps;
                        if (AppHandler.MapManager.FindMapsByWorking(strPath, out dMaps) > 0)
                        {
                            strTxt = AppHandler.LanguageHandler.GetText("MESSAGE", "maps_will_be_deleted", "Eine oder mehrere Lernmappen verweisen auf diese Änderung\n Sie werden in den Mappen gelöscht!");
                            strCap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            if (MessageBox.Show(strTxt, strCap, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                foreach (var m in dMaps)
                                {
                                    AppHandler.MapManager.DeleteWorking(m.Key, m.Value);
                                    AppHandler.MapManager.Save(m.Key);
                                }
                            }

                        }

                        AppHandler.LibManager.SetModified(strPath);
                        AppHandler.LibManager.Reload(strPath);

                        if (eNodeType == XContentTree.NodeType.Point && strPath == m_strSelectedOverview)
                        {
                            string[] aTitles;
                            Utilities.SplitPath(strPath, out aTitles);
                            aTitles[3] = e.Value.ToString();
                            string strNewPath = Utilities.MergePath(aTitles);
                            SelectedOverview = strNewPath;
                        }
                    }
                    else
                        e.Value = strTitle;
                }
            }
        }


        private void xContentTree1_Leave(object sender, EventArgs e)
        {
            xContentTree1.CloseEditor();
        }

        private void xContentTree1_DragDrop(object sender, DragEventArgs e)
        {
            var dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            if (dragNode != null)
            {
                string sParentPath = xContentTree1.ContentList.GetPath(dragNode.ParentNode.Id);
                AppHandler.LibManager.Reload(sParentPath);
            }

        }

        private void xtraTabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            XtraTabControl c = sender as XtraTabControl;
            m_dragPoint = new Point(e.X, e.Y);
            XtraTabHitInfo hi = c.CalcHitInfo(m_dragPoint);
            m_dragPage = hi.Page;
            if (hi.Page == null)
                m_dragPoint = Point.Empty;
        }

        private void xtraTabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if ((m_dragPoint != Point.Empty) && ((Math.Abs(e.X - m_dragPoint.X) > SystemInformation.DragSize.Width) || (Math.Abs(e.Y - m_dragPoint.Y) > SystemInformation.DragSize.Height)))
                    xtraTabCtrl_Content.DoDragDrop(sender, DragDropEffects.Move);
        }

        private void xtraTabControl1_DragDrop(object sender, DragEventArgs e)
        {
            XtraTabControl c = sender as XtraTabControl;
            if (c == null)
                return;
            XtraTabHitInfo hi = c.CalcHitInfo(c.PointToClient(new Point(e.X, e.Y)));

            int p1 = c.TabPages.IndexOf(m_dragPage);
            int p2 = c.TabPages.IndexOf(hi.Page);

            AppHandler.LibManager.MovePage(m_strSelectedOverview, p1, p2 - p1);
            UpdateContentMode(-1);
        }

        private void xtraTabControl1_DragOver(object sender, DragEventArgs e)
        {
            XtraTabControl c = sender as XtraTabControl;
            if (c == null)
                return;

            XtraTabHitInfo hi = c.CalcHitInfo(c.PointToClient(new Point(e.X, e.Y)));
            if (hi.Page != null)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            /*
            var frm = new XFrmContentEditSettings();
            frm.ShowDialog();*/
        }

        private void axWebBrowser1_SizeChanged(object sender, EventArgs e)
        {
            /*
            if (transparentFrameControl1.Visible )
            {
                axWebBrowser1.SendToBack();
                transparentFrameControl1.BringToFront();
                transparentFrameControl1.Refresh();
            }*/
        }


        private void btnDuplicateQu_Click(object sender, EventArgs e)
        {
            var frm = new XFrmSelectContent();
            frm.SelectedPath = this.m_strSelectedOverview;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strTargetPath = frm.SelectedPath;
                AppHandler.LibManager.AddQuestion(strTargetPath, m_actQuestion);
                AppHandler.LibManager.SetModified(strTargetPath);
                AppHandler.LibManager.Reload(strTargetPath);
                UpdateContentMode(-1);
            }
        }

        private void btnDelAction_Click(object sender, EventArgs e)
        {
            var tabPage = xtraTabCtrl_Content.SelectedTabPage.Controls[0] as ContentEditTabPage;
            if (tabPage != null)
                tabPage.DeleteSelectedAction();
        }

        private void barCheckItem1_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            var chk = sender as BarCheckItem;
            barEditHeight.Enabled = !chk.Checked;
        }

    }
}
