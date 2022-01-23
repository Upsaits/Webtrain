using SoftObject.TrainConcept.Controls;
using SoftObject.SOComponents.Controls;

namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmEditContent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditContent));
            this.repositoryItemZoomTrackBar3 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panOverview = new System.Windows.Forms.Panel();
            this.btnOverviewInsertPrev = new System.Windows.Forms.Button();
            this.btnOverviewDelete = new System.Windows.Forms.Button();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtOvwPageCount = new System.Windows.Forms.TextBox();
            this.lblOvwPageCnt = new System.Windows.Forms.Label();
            this.txtOverviewType = new System.Windows.Forms.TextBox();
            this.txtOverviewTitle = new System.Windows.Forms.TextBox();
            this.lblOvwTitle = new System.Windows.Forms.Label();
            this.lblOvwTyp = new System.Windows.Forms.Label();
            this.panButtonBar = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnQuestions = new System.Windows.Forms.Button();
            this.btnContent = new System.Windows.Forms.Button();
            this.btnOverview = new System.Windows.Forms.Button();
            this.panContent = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnDelAction = new DevComponents.DotNetBar.ButtonX();
            this.btnDuplicate = new DevComponents.DotNetBar.ButtonX();
            this.btnNewAction = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenuNewAction = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barNewActionItems = new DevExpress.XtraBars.BarListItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonGalleryBarItemTemplates = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gddTemplates = new DevExpress.XtraBars.Ribbon.GalleryDropDown(this.components);
            this.barButtonLeft = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRight = new DevExpress.XtraBars.BarButtonItem();
            this.barMoveImage = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonUp = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDown = new DevExpress.XtraBars.BarButtonItem();
            this.barEditWidth = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.barEditHeight = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemZoomTrackBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.barChooseQuestionImage = new DevExpress.XtraBars.BarListItem();
            this.barButtonItemAddDoc = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelDoc = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSelectImageId = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barCheckProp = new DevExpress.XtraBars.BarCheckItem();
            this.ribbonPageTemplates = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupTemplates = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageDocuments = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupNotices = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupPDF = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupPPT = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupxAPI = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupControl = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGraphics = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupPosition = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.repositoryItemTrackBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.btnEdit = new DevComponents.DotNetBar.ButtonX();
            this.xtraTabCtrl_Content = new DevExpress.XtraTab.XtraTabControl();
            this.popupMenuSetImageQu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnSetImageQu = new DevExpress.XtraEditors.DropDownButton();
            this.panQuestions = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnDuplicateQu = new DevComponents.DotNetBar.ButtonX();
            this.btnDelQuestion = new DevComponents.DotNetBar.ButtonX();
            this.btnAddQuestion = new DevComponents.DotNetBar.ButtonX();
            this.xtraTabCtrl_Questions = new DevExpress.XtraTab.XtraTabControl();
            this.label3 = new System.Windows.Forms.Label();
            this.axWebBrowser1 = new SoftObject.SOComponents.Controls.WebBrowserEx();
            this.xContentTree1 = new SoftObject.TrainConcept.Controls.XContentTree();
            this.bbgFontStyle = new DevExpress.XtraBars.BarButtonGroup();
            this.opFileDlgContent = new System.Windows.Forms.OpenFileDialog();
            this.opFileDlgDocuments = new System.Windows.Forms.OpenFileDialog();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panContentBrowser = new System.Windows.Forms.Panel();
            this.transparentFrameControl1 = new SoftObject.SOComponents.Controls.TransparentFrameControl();
            this.txtSelectedOverview = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar3)).BeginInit();
            this.panOverview.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.panButtonBar.SuspendLayout();
            this.panContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuNewAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gddTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabCtrl_Content)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuSetImageQu)).BeginInit();
            this.panQuestions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabCtrl_Questions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xContentTree1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.panContentBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // repositoryItemZoomTrackBar3
            // 
            this.repositoryItemZoomTrackBar3.Name = "repositoryItemZoomTrackBar3";
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(796, 11);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panOverview
            // 
            this.panOverview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panOverview.Controls.Add(this.btnOverviewInsertPrev);
            this.panOverview.Controls.Add(this.btnOverviewDelete);
            this.panOverview.Controls.Add(this.grpProperties);
            this.panOverview.Location = new System.Drawing.Point(0, 307);
            this.panOverview.Name = "panOverview";
            this.panOverview.Size = new System.Drawing.Size(870, 160);
            this.panOverview.TabIndex = 2;
            // 
            // btnOverviewInsertPrev
            // 
            this.btnOverviewInsertPrev.Location = new System.Drawing.Point(710, 40);
            this.btnOverviewInsertPrev.Name = "btnOverviewInsertPrev";
            this.btnOverviewInsertPrev.Size = new System.Drawing.Size(138, 36);
            this.btnOverviewInsertPrev.TabIndex = 2;
            this.btnOverviewInsertPrev.Text = "Neu";
            this.btnOverviewInsertPrev.Click += new System.EventHandler(this.btnOverviewInsertPrev_Click);
            // 
            // btnOverviewDelete
            // 
            this.btnOverviewDelete.Location = new System.Drawing.Point(710, 4);
            this.btnOverviewDelete.Name = "btnOverviewDelete";
            this.btnOverviewDelete.Size = new System.Drawing.Size(138, 36);
            this.btnOverviewDelete.TabIndex = 1;
            this.btnOverviewDelete.Text = "Löschen";
            this.btnOverviewDelete.Click += new System.EventHandler(this.btnOverviewDelete_Click);
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this.txtVersion);
            this.grpProperties.Controls.Add(this.lblVersion);
            this.grpProperties.Controls.Add(this.txtOvwPageCount);
            this.grpProperties.Controls.Add(this.lblOvwPageCnt);
            this.grpProperties.Controls.Add(this.txtOverviewType);
            this.grpProperties.Controls.Add(this.txtOverviewTitle);
            this.grpProperties.Controls.Add(this.lblOvwTitle);
            this.grpProperties.Controls.Add(this.lblOvwTyp);
            this.grpProperties.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpProperties.Location = new System.Drawing.Point(26, 4);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(678, 143);
            this.grpProperties.TabIndex = 0;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Eigenschaften";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(322, 37);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(163, 27);
            this.txtVersion.TabIndex = 7;
            this.txtVersion.Text = "100.100.100.9999";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(237, 41);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(85, 30);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Version: ";
            // 
            // txtOvwPageCount
            // 
            this.txtOvwPageCount.Location = new System.Drawing.Point(136, 104);
            this.txtOvwPageCount.Name = "txtOvwPageCount";
            this.txtOvwPageCount.ReadOnly = true;
            this.txtOvwPageCount.Size = new System.Drawing.Size(91, 27);
            this.txtOvwPageCount.TabIndex = 5;
            // 
            // lblOvwPageCnt
            // 
            this.lblOvwPageCnt.Location = new System.Drawing.Point(14, 107);
            this.lblOvwPageCnt.Name = "lblOvwPageCnt";
            this.lblOvwPageCnt.Size = new System.Drawing.Size(124, 26);
            this.lblOvwPageCnt.TabIndex = 4;
            this.lblOvwPageCnt.Text = "Seitenanzahl:";
            // 
            // txtOverviewType
            // 
            this.txtOverviewType.Enabled = false;
            this.txtOverviewType.Location = new System.Drawing.Point(74, 36);
            this.txtOverviewType.Name = "txtOverviewType";
            this.txtOverviewType.Size = new System.Drawing.Size(153, 27);
            this.txtOverviewType.TabIndex = 3;
            // 
            // txtOverviewTitle
            // 
            this.txtOverviewTitle.Location = new System.Drawing.Point(74, 71);
            this.txtOverviewTitle.Name = "txtOverviewTitle";
            this.txtOverviewTitle.Size = new System.Drawing.Size(576, 27);
            this.txtOverviewTitle.TabIndex = 2;
            this.txtOverviewTitle.TextChanged += new System.EventHandler(this.txtOverviewTitle_TextChanged);
            // 
            // lblOvwTitle
            // 
            this.lblOvwTitle.Location = new System.Drawing.Point(14, 76);
            this.lblOvwTitle.Name = "lblOvwTitle";
            this.lblOvwTitle.Size = new System.Drawing.Size(64, 24);
            this.lblOvwTitle.TabIndex = 1;
            this.lblOvwTitle.Text = "Titel:";
            // 
            // lblOvwTyp
            // 
            this.lblOvwTyp.Location = new System.Drawing.Point(14, 41);
            this.lblOvwTyp.Name = "lblOvwTyp";
            this.lblOvwTyp.Size = new System.Drawing.Size(64, 25);
            this.lblOvwTyp.TabIndex = 0;
            this.lblOvwTyp.Text = "Typ:";
            // 
            // panButtonBar
            // 
            this.panButtonBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panButtonBar.Controls.Add(this.btnSettings);
            this.panButtonBar.Controls.Add(this.btnExport);
            this.panButtonBar.Controls.Add(this.btnImport);
            this.panButtonBar.Controls.Add(this.btnSave);
            this.panButtonBar.Controls.Add(this.btnTest);
            this.panButtonBar.Controls.Add(this.btnDel);
            this.panButtonBar.Controls.Add(this.btnAdd);
            this.panButtonBar.Controls.Add(this.btnQuestions);
            this.panButtonBar.Controls.Add(this.btnContent);
            this.panButtonBar.Controls.Add(this.btnOverview);
            this.panButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panButtonBar.Location = new System.Drawing.Point(0, 807);
            this.panButtonBar.Name = "panButtonBar";
            this.panButtonBar.Size = new System.Drawing.Size(796, 71);
            this.panButtonBar.TabIndex = 3;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(1112, 7);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(130, 57);
            this.btnSettings.TabIndex = 9;
            this.btnSettings.TabStop = false;
            this.btnSettings.Text = "Einstellungen";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Visible = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(986, 7);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 57);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(858, 7);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(120, 57);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(702, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 57);
            this.btnSave.TabIndex = 6;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(1117, 9);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(120, 57);
            this.btnTest.TabIndex = 5;
            this.btnTest.TabStop = false;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(573, 7);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(120, 57);
            this.btnDel.TabIndex = 4;
            this.btnDel.TabStop = false;
            this.btnDel.Text = "Seite Löschen";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(443, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 57);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Neue Seite";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnQuestions
            // 
            this.btnQuestions.Location = new System.Drawing.Point(288, 7);
            this.btnQuestions.Name = "btnQuestions";
            this.btnQuestions.Size = new System.Drawing.Size(120, 57);
            this.btnQuestions.TabIndex = 2;
            this.btnQuestions.Text = "Fragen";
            this.btnQuestions.Click += new System.EventHandler(this.btnQuestions_Click);
            // 
            // btnContent
            // 
            this.btnContent.Location = new System.Drawing.Point(157, 7);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(120, 57);
            this.btnContent.TabIndex = 1;
            this.btnContent.Text = "Inhalte";
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // btnOverview
            // 
            this.btnOverview.Location = new System.Drawing.Point(26, 7);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(120, 57);
            this.btnOverview.TabIndex = 0;
            this.btnOverview.Text = "Übersicht";
            this.btnOverview.Click += new System.EventHandler(this.btnOverview_Click);
            // 
            // panContent
            // 
            this.panContent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panContent.Controls.Add(this.splitContainer1);
            this.panContent.Controls.Add(this.ribbonControl1);
            this.panContent.Location = new System.Drawing.Point(0, 493);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(870, 546);
            this.panContent.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 166);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnDelAction);
            this.splitContainer1.Panel1.Controls.Add(this.btnDuplicate);
            this.splitContainer1.Panel1.Controls.Add(this.btnNewAction);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xtraTabCtrl_Content);
            this.splitContainer1.Size = new System.Drawing.Size(866, 376);
            this.splitContainer1.SplitterDistance = 90;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnDelAction
            // 
            this.btnDelAction.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelAction.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDelAction.Enabled = false;
            this.btnDelAction.Location = new System.Drawing.Point(13, 191);
            this.btnDelAction.Name = "btnDelAction";
            this.btnDelAction.Size = new System.Drawing.Size(109, 52);
            this.btnDelAction.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelAction.TabIndex = 13;
            this.btnDelAction.Text = "Aktion Löschen";
            this.btnDelAction.Click += new System.EventHandler(this.btnDelAction_Click);
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDuplicate.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDuplicate.Enabled = false;
            this.btnDuplicate.Location = new System.Drawing.Point(8, 129);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(120, 52);
            this.btnDuplicate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDuplicate.TabIndex = 12;
            this.btnDuplicate.Text = "Dupliziere Seite";
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // btnNewAction
            // 
            this.btnNewAction.Appearance.Options.UseTextOptions = true;
            this.btnNewAction.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnNewAction.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnNewAction.DropDownControl = this.popupMenuNewAction;
            this.btnNewAction.Location = new System.Drawing.Point(-3, 66);
            this.btnNewAction.Name = "btnNewAction";
            this.btnNewAction.Size = new System.Drawing.Size(145, 54);
            this.btnNewAction.TabIndex = 11;
            this.btnNewAction.Text = "Neue Aktion";
            // 
            // popupMenuNewAction
            // 
            this.popupMenuNewAction.ItemLinks.Add(this.barNewActionItems);
            this.popupMenuNewAction.Name = "popupMenuNewAction";
            this.popupMenuNewAction.Ribbon = this.ribbonControl1;
            // 
            // barNewActionItems
            // 
            this.barNewActionItems.Caption = "barListNewAction";
            this.barNewActionItems.Id = 26;
            this.barNewActionItems.Name = "barNewActionItems";
            this.barNewActionItems.Strings.AddRange(new object[] {
            "Neues Bild",
            "Neue Animation",
            "Neue Simulation",
            "Neues Video",
            "Neues Stichwort"});
            this.barNewActionItems.ListItemClick += new DevExpress.XtraBars.ListItemClickEventHandler(this.barNewActionItems_ListItemClick);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowDrop = true;
            this.ribbonControl1.ApplicationButtonText = null;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonGalleryBarItemTemplates,
            this.barButtonLeft,
            this.barButtonRight,
            this.barMoveImage,
            this.barButtonUp,
            this.barButtonDown,
            this.barEditWidth,
            this.barEditHeight,
            this.barChooseQuestionImage,
            this.barNewActionItems,
            this.barButtonItemAddDoc,
            this.barButtonItemDelDoc,
            this.barButtonItem1,
            this.barSelectImageId,
            this.barCheckProp});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 42;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageTemplates,
            this.ribbonPageDocuments,
            this.ribbonPageGraphics});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemTrackBar1,
            this.repositoryItemTrackBar2,
            this.repositoryItemZoomTrackBar1,
            this.repositoryItemZoomTrackBar2,
            this.repositoryItemSpinEdit1});
            this.ribbonControl1.Size = new System.Drawing.Size(866, 166);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ribbonControl1_DragDrop);
            this.ribbonControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ribbonControl1_DragEnter);
            // 
            // ribbonGalleryBarItemTemplates
            // 
            this.ribbonGalleryBarItemTemplates.Caption = "Vorlagen";
            // 
            // 
            // 
            this.ribbonGalleryBarItemTemplates.Gallery.AllowHoverImages = true;
            this.ribbonGalleryBarItemTemplates.Gallery.Appearance.ItemDescriptionAppearance.Normal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonGalleryBarItemTemplates.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseFont = true;
            this.ribbonGalleryBarItemTemplates.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseTextOptions = true;
            this.ribbonGalleryBarItemTemplates.Gallery.Appearance.ItemDescriptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.ribbonGalleryBarItemTemplates.Gallery.CheckDrawMode = DevExpress.XtraBars.Ribbon.Gallery.CheckDrawMode.ImageAndText;
            this.ribbonGalleryBarItemTemplates.Gallery.ColumnCount = 3;
            this.ribbonGalleryBarItemTemplates.Gallery.DistanceItemImageToText = 5;
            this.ribbonGalleryBarItemTemplates.Gallery.HoverImages = this.imageList2;
            this.ribbonGalleryBarItemTemplates.Gallery.HoverImageSize = new System.Drawing.Size(105, 150);
            this.ribbonGalleryBarItemTemplates.Gallery.Images = this.imageList1;
            this.ribbonGalleryBarItemTemplates.Gallery.ImageSize = new System.Drawing.Size(50, 80);
            this.ribbonGalleryBarItemTemplates.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleCheck;
            this.ribbonGalleryBarItemTemplates.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch;
            this.ribbonGalleryBarItemTemplates.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Left;
            this.ribbonGalleryBarItemTemplates.Gallery.ShowItemText = true;
            this.ribbonGalleryBarItemTemplates.GalleryDropDown = this.gddTemplates;
            this.ribbonGalleryBarItemTemplates.Id = 0;
            this.ribbonGalleryBarItemTemplates.ImageOptions.ImageIndex = 0;
            this.ribbonGalleryBarItemTemplates.Name = "ribbonGalleryBarItemTemplates";
            this.ribbonGalleryBarItemTemplates.GalleryItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.ribbonGalleryBarItemTemplates_GalleryItemClick);
            this.ribbonGalleryBarItemTemplates.GalleryInitDropDownGallery += new DevExpress.XtraBars.Ribbon.InplaceGalleryEventHandler(this.rgbiTemplates_GalleryInitDropDownGallery);
            this.ribbonGalleryBarItemTemplates.GalleryPopupClose += new DevExpress.XtraBars.Ribbon.InplaceGalleryEventHandler(this.ribbonGalleryBarItemTemplates_GalleryPopupClose);
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(180, 256);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(120, 170);
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // gddTemplates
            // 
            // 
            // 
            // 
            this.gddTemplates.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseTextOptions = true;
            this.gddTemplates.Gallery.Appearance.ItemDescriptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gddTemplates.Gallery.CheckDrawMode = DevExpress.XtraBars.Ribbon.Gallery.CheckDrawMode.ImageAndText;
            this.gddTemplates.Gallery.ColumnCount = 3;
            this.gddTemplates.Gallery.DistanceItemImageToText = 5;
            this.gddTemplates.Gallery.DrawImageBackground = false;
            this.gddTemplates.Gallery.Images = this.imageList2;
            this.gddTemplates.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleCheck;
            this.gddTemplates.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch;
            this.gddTemplates.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Left;
            this.gddTemplates.Gallery.ShowGroupCaption = false;
            this.gddTemplates.Gallery.ShowItemText = true;
            this.gddTemplates.Name = "gddTemplates";
            this.gddTemplates.Ribbon = this.ribbonControl1;
            // 
            // barButtonLeft
            // 
            this.barButtonLeft.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barButtonLeft.Caption = "←";
            this.barButtonLeft.CloseSubMenuOnClick = false;
            this.barButtonLeft.Id = 16;
            this.barButtonLeft.ItemAppearance.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.barButtonLeft.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonLeft.ItemAppearance.Normal.Options.UseBackColor = true;
            this.barButtonLeft.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonLeft.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left));
            this.barButtonLeft.Name = "barButtonLeft";
            this.barButtonLeft.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonLeft_ItemClick);
            // 
            // barButtonRight
            // 
            this.barButtonRight.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonRight.Caption = "→";
            this.barButtonRight.Id = 17;
            this.barButtonRight.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonRight.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonRight.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right));
            this.barButtonRight.Name = "barButtonRight";
            this.barButtonRight.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonRight_ItemClick);
            // 
            // barMoveImage
            // 
            this.barMoveImage.Caption = "MoveImage";
            this.barMoveImage.Id = 18;
            this.barMoveImage.ItemLinks.Add(this.barButtonLeft);
            this.barMoveImage.ItemLinks.Add(this.barButtonRight);
            this.barMoveImage.ItemLinks.Add(this.barButtonUp);
            this.barMoveImage.ItemLinks.Add(this.barButtonDown);
            this.barMoveImage.Name = "barMoveImage";
            // 
            // barButtonUp
            // 
            this.barButtonUp.Caption = "↑";
            this.barButtonUp.Id = 19;
            this.barButtonUp.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonUp.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonUp.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up));
            this.barButtonUp.Name = "barButtonUp";
            this.barButtonUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonUp_ItemClick);
            // 
            // barButtonDown
            // 
            this.barButtonDown.Caption = "↓";
            this.barButtonDown.Id = 20;
            this.barButtonDown.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonDown.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonDown.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down));
            this.barButtonDown.Name = "barButtonDown";
            this.barButtonDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDown_ItemClick);
            // 
            // barEditWidth
            // 
            this.barEditWidth.Caption = "Breite";
            this.barEditWidth.Edit = this.repositoryItemZoomTrackBar1;
            this.barEditWidth.EditValue = 50;
            this.barEditWidth.EditWidth = 150;
            this.barEditWidth.Id = 22;
            this.barEditWidth.Name = "barEditWidth";
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.LargeChange = 10;
            this.repositoryItemZoomTrackBar1.Maximum = 100;
            this.repositoryItemZoomTrackBar1.Middle = 50;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.EditValueChanged += new System.EventHandler(this.repositoryItemZoomTrackBar1_EditValueChanged);
            // 
            // barEditHeight
            // 
            this.barEditHeight.Caption = "Höhe ";
            this.barEditHeight.Edit = this.repositoryItemZoomTrackBar2;
            this.barEditHeight.EditValue = 50;
            this.barEditHeight.EditWidth = 150;
            this.barEditHeight.Id = 23;
            this.barEditHeight.Name = "barEditHeight";
            // 
            // repositoryItemZoomTrackBar2
            // 
            this.repositoryItemZoomTrackBar2.LargeChange = 10;
            this.repositoryItemZoomTrackBar2.Maximum = 100;
            this.repositoryItemZoomTrackBar2.Middle = 50;
            this.repositoryItemZoomTrackBar2.Name = "repositoryItemZoomTrackBar2";
            this.repositoryItemZoomTrackBar2.EditValueChanged += new System.EventHandler(this.repositoryItemZoomTrackBar2_EditValueChanged);
            // 
            // barChooseQuestionImage
            // 
            this.barChooseQuestionImage.Caption = "barListImageQu";
            this.barChooseQuestionImage.Id = 25;
            this.barChooseQuestionImage.Name = "barChooseQuestionImage";
            this.barChooseQuestionImage.Strings.AddRange(new object[] {
            "Standardbild setzen",
            "Standardbild löschen",
            "Bild wählen"});
            this.barChooseQuestionImage.ListItemClick += new DevExpress.XtraBars.ListItemClickEventHandler(this.barChooseQuestionImage_ListItemClick);
            // 
            // barButtonItemAddDoc
            // 
            this.barButtonItemAddDoc.Caption = "Hinzufügen";
            this.barButtonItemAddDoc.Id = 33;
            this.barButtonItemAddDoc.Name = "barButtonItemAddDoc";
            this.barButtonItemAddDoc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barbtnItemAddDoc_ItemClick);
            // 
            // barButtonItemDelDoc
            // 
            this.barButtonItemDelDoc.Caption = "Löschen";
            this.barButtonItemDelDoc.Id = 34;
            this.barButtonItemDelDoc.Name = "barButtonItemDelDoc";
            this.barButtonItemDelDoc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barbtnItemDelDoc_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 35;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSelectImageId
            // 
            this.barSelectImageId.Caption = "Bild-Id:";
            this.barSelectImageId.Edit = this.repositoryItemSpinEdit1;
            this.barSelectImageId.Id = 36;
            this.barSelectImageId.Name = "barSelectImageId";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.IsFloatValue = false;
            this.repositoryItemSpinEdit1.Mask.EditMask = "N00";
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // barCheckProp
            // 
            this.barCheckProp.BindableChecked = true;
            this.barCheckProp.Caption = "Proportional";
            this.barCheckProp.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.barCheckProp.Checked = true;
            this.barCheckProp.Id = 41;
            this.barCheckProp.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.barCheckProp.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.barCheckProp.Name = "barCheckProp";
            this.barCheckProp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.barCheckProp.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem1_CheckedChanged);
            // 
            // ribbonPageTemplates
            // 
            this.ribbonPageTemplates.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupTemplates});
            this.ribbonPageTemplates.Name = "ribbonPageTemplates";
            this.ribbonPageTemplates.Text = "Vorlagen";
            // 
            // ribbonPageGroupTemplates
            // 
            this.ribbonPageGroupTemplates.ItemLinks.Add(this.ribbonGalleryBarItemTemplates);
            this.ribbonPageGroupTemplates.Name = "ribbonPageGroupTemplates";
            this.ribbonPageGroupTemplates.ShowCaptionButton = false;
            this.ribbonPageGroupTemplates.Text = "SeitenTemplate";
            // 
            // ribbonPageDocuments
            // 
            this.ribbonPageDocuments.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupNotices,
            this.ribbonPageGroupPDF,
            this.ribbonPageGroupPPT,
            this.ribbonPageGroupxAPI,
            this.ribbonPageGroupControl});
            this.ribbonPageDocuments.Name = "ribbonPageDocuments";
            this.ribbonPageDocuments.Text = "Dokumente";
            // 
            // ribbonPageGroupNotices
            // 
            this.ribbonPageGroupNotices.Name = "ribbonPageGroupNotices";
            this.ribbonPageGroupNotices.Text = "Aufgabe (RTF)";
            // 
            // ribbonPageGroupPDF
            // 
            this.ribbonPageGroupPDF.Name = "ribbonPageGroupPDF";
            this.ribbonPageGroupPDF.Text = "Unterlagen (PDF)";
            // 
            // ribbonPageGroupPPT
            // 
            this.ribbonPageGroupPPT.Name = "ribbonPageGroupPPT";
            this.ribbonPageGroupPPT.Text = "PowerPoint (PPTX)";
            // 
            // ribbonPageGroupxAPI
            // 
            this.ribbonPageGroupxAPI.Name = "ribbonPageGroupxAPI";
            this.ribbonPageGroupxAPI.Text = "SCORM (xAPI)";
            // 
            // ribbonPageGroupControl
            // 
            this.ribbonPageGroupControl.AllowTextClipping = false;
            this.ribbonPageGroupControl.ItemLinks.Add(this.barButtonItemAddDoc);
            this.ribbonPageGroupControl.ItemLinks.Add(this.barButtonItemDelDoc);
            this.ribbonPageGroupControl.Name = "ribbonPageGroupControl";
            this.ribbonPageGroupControl.ShowCaptionButton = false;
            this.ribbonPageGroupControl.State = DevExpress.XtraBars.Ribbon.RibbonPageGroupState.Expanded;
            // 
            // ribbonPageGraphics
            // 
            this.ribbonPageGraphics.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupPosition});
            this.ribbonPageGraphics.Name = "ribbonPageGraphics";
            this.ribbonPageGraphics.Text = "Grafikeinstellung";
            // 
            // ribbonPageGroupPosition
            // 
            this.ribbonPageGroupPosition.ItemLinks.Add(this.barMoveImage);
            this.ribbonPageGroupPosition.ItemLinks.Add(this.barEditWidth);
            this.ribbonPageGroupPosition.ItemLinks.Add(this.barEditHeight);
            this.ribbonPageGroupPosition.ItemLinks.Add(this.barSelectImageId);
            this.ribbonPageGroupPosition.ItemLinks.Add(this.barCheckProp);
            this.ribbonPageGroupPosition.Name = "ribbonPageGroupPosition";
            this.ribbonPageGroupPosition.Text = "Eigenschaften";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemTrackBar1
            // 
            this.repositoryItemTrackBar1.LargeChange = 10;
            this.repositoryItemTrackBar1.Maximum = 100;
            this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
            this.repositoryItemTrackBar1.TickFrequency = 10;
            // 
            // repositoryItemTrackBar2
            // 
            this.repositoryItemTrackBar2.LargeChange = 10;
            this.repositoryItemTrackBar2.Maximum = 100;
            this.repositoryItemTrackBar2.Name = "repositoryItemTrackBar2";
            this.repositoryItemTrackBar2.TickFrequency = 10;
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnEdit.Location = new System.Drawing.Point(5, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(109, 53);
            this.btnEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "Text bearbeiten";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // xtraTabCtrl_Content
            // 
            this.xtraTabCtrl_Content.AllowDrop = true;
            this.xtraTabCtrl_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabCtrl_Content.Location = new System.Drawing.Point(0, 0);
            this.xtraTabCtrl_Content.Name = "xtraTabCtrl_Content";
            this.xtraTabCtrl_Content.Size = new System.Drawing.Size(772, 376);
            this.xtraTabCtrl_Content.TabIndex = 0;
            this.xtraTabCtrl_Content.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            this.xtraTabCtrl_Content.DragDrop += new System.Windows.Forms.DragEventHandler(this.xtraTabControl1_DragDrop);
            this.xtraTabCtrl_Content.DragOver += new System.Windows.Forms.DragEventHandler(this.xtraTabControl1_DragOver);
            this.xtraTabCtrl_Content.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabControl1_MouseDown);
            this.xtraTabCtrl_Content.MouseMove += new System.Windows.Forms.MouseEventHandler(this.xtraTabControl1_MouseMove);
            // 
            // popupMenuSetImageQu
            // 
            this.popupMenuSetImageQu.ItemLinks.Add(this.barChooseQuestionImage);
            this.popupMenuSetImageQu.Name = "popupMenuSetImageQu";
            this.popupMenuSetImageQu.Ribbon = this.ribbonControl1;
            // 
            // btnSetImageQu
            // 
            this.btnSetImageQu.Appearance.Options.UseTextOptions = true;
            this.btnSetImageQu.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnSetImageQu.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnSetImageQu.DropDownControl = this.popupMenuSetImageQu;
            this.btnSetImageQu.Location = new System.Drawing.Point(3, 4);
            this.btnSetImageQu.Name = "btnSetImageQu";
            this.btnSetImageQu.Size = new System.Drawing.Size(136, 55);
            this.btnSetImageQu.TabIndex = 8;
            this.btnSetImageQu.Text = "Bild bearbeiten";
            // 
            // panQuestions
            // 
            this.panQuestions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panQuestions.Controls.Add(this.splitContainer2);
            this.panQuestions.Location = new System.Drawing.Point(64, 1047);
            this.panQuestions.Name = "panQuestions";
            this.panQuestions.Size = new System.Drawing.Size(870, 214);
            this.panQuestions.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnDuplicateQu);
            this.splitContainer2.Panel1.Controls.Add(this.btnSetImageQu);
            this.splitContainer2.Panel1.Controls.Add(this.btnDelQuestion);
            this.splitContainer2.Panel1.Controls.Add(this.btnAddQuestion);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.xtraTabCtrl_Questions);
            this.splitContainer2.Size = new System.Drawing.Size(866, 210);
            this.splitContainer2.SplitterDistance = 90;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnDuplicateQu
            // 
            this.btnDuplicateQu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDuplicateQu.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDuplicateQu.Enabled = false;
            this.btnDuplicateQu.Location = new System.Drawing.Point(19, 147);
            this.btnDuplicateQu.Name = "btnDuplicateQu";
            this.btnDuplicateQu.Size = new System.Drawing.Size(106, 53);
            this.btnDuplicateQu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDuplicateQu.TabIndex = 13;
            this.btnDuplicateQu.Text = "Dupliziere Frage";
            this.btnDuplicateQu.Click += new System.EventHandler(this.btnDuplicateQu_Click);
            // 
            // btnDelQuestion
            // 
            this.btnDelQuestion.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelQuestion.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDelQuestion.Location = new System.Drawing.Point(10, 107);
            this.btnDelQuestion.Name = "btnDelQuestion";
            this.btnDelQuestion.Size = new System.Drawing.Size(129, 32);
            this.btnDelQuestion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelQuestion.TabIndex = 10;
            this.btnDelQuestion.Text = "Antw. löschen";
            this.btnDelQuestion.Click += new System.EventHandler(this.btnDelQuestion_Click);
            // 
            // btnAddQuestion
            // 
            this.btnAddQuestion.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddQuestion.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddQuestion.Location = new System.Drawing.Point(6, 69);
            this.btnAddQuestion.Name = "btnAddQuestion";
            this.btnAddQuestion.Size = new System.Drawing.Size(133, 30);
            this.btnAddQuestion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddQuestion.TabIndex = 9;
            this.btnAddQuestion.Text = "Neue Antwort";
            this.btnAddQuestion.Click += new System.EventHandler(this.btnAddQuestion_Click);
            // 
            // xtraTabCtrl_Questions
            // 
            this.xtraTabCtrl_Questions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabCtrl_Questions.Location = new System.Drawing.Point(0, 0);
            this.xtraTabCtrl_Questions.Name = "xtraTabCtrl_Questions";
            this.xtraTabCtrl_Questions.Size = new System.Drawing.Size(772, 210);
            this.xtraTabCtrl_Questions.TabIndex = 0;
            this.xtraTabCtrl_Questions.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl2_SelectedPageChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(56, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // axWebBrowser1
            // 
            this.axWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.axWebBrowser1.Name = "axWebBrowser1";
            this.axWebBrowser1.Size = new System.Drawing.Size(483, 260);
            this.axWebBrowser1.TabIndex = 6;
            this.axWebBrowser1.SizeChanged += new System.EventHandler(this.axWebBrowser1_SizeChanged);
            // 
            // xContentTree1
            // 
            this.xContentTree1.AllowDrop = true;
            this.xContentTree1.Appearance.EvenRow.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xContentTree1.Appearance.EvenRow.Options.UseFont = true;
            this.xContentTree1.Appearance.GroupButton.BackColor = System.Drawing.SystemColors.Window;
            this.xContentTree1.Appearance.GroupButton.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.xContentTree1.Appearance.GroupButton.Options.UseBackColor = true;
            this.xContentTree1.Appearance.GroupButton.Options.UseForeColor = true;
            this.xContentTree1.Appearance.OddRow.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xContentTree1.Appearance.OddRow.Options.UseFont = true;
            this.xContentTree1.Appearance.VertLine.Options.UseTextOptions = true;
            this.xContentTree1.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xContentTree1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.xContentTree1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xContentTree1.LearnmapName = "";
            this.xContentTree1.Location = new System.Drawing.Point(456, 14);
            this.xContentTree1.Name = "xContentTree1";
            this.xContentTree1.OptionsBehavior.AutoChangeParent = false;
            this.xContentTree1.OptionsBehavior.AutoSelectAllInEditor = false;
            this.xContentTree1.OptionsBehavior.CloseEditorOnLostFocus = false;
            this.xContentTree1.OptionsBehavior.ShowToolTips = false;
            this.xContentTree1.OptionsBehavior.SmartMouseHover = false;
            this.xContentTree1.OptionsDragAndDrop.ExpandNodeOnDrag = false;
            this.xContentTree1.OptionsMenu.EnableColumnMenu = false;
            this.xContentTree1.OptionsMenu.EnableFooterMenu = false;
            this.xContentTree1.OptionsNavigation.MoveOnEdit = false;
            this.xContentTree1.OptionsPrint.AutoRowHeight = false;
            this.xContentTree1.OptionsPrint.AutoWidth = false;
            this.xContentTree1.OptionsPrint.PrintHorzLines = false;
            this.xContentTree1.OptionsPrint.PrintImages = false;
            this.xContentTree1.OptionsPrint.PrintPageHeader = false;
            this.xContentTree1.OptionsPrint.PrintReportFooter = false;
            this.xContentTree1.OptionsPrint.PrintTree = false;
            this.xContentTree1.OptionsPrint.PrintTreeButtons = false;
            this.xContentTree1.OptionsPrint.PrintVertLines = false;
            this.xContentTree1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.xContentTree1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.xContentTree1.OptionsSelection.KeepSelectedOnClick = false;
            this.xContentTree1.OptionsView.BestFitNodes = DevExpress.XtraTreeList.TreeListBestFitNodes.Visible;
            this.xContentTree1.OptionsView.ShowColumns = false;
            this.xContentTree1.OptionsView.ShowIndentAsRowStyle = true;
            this.xContentTree1.OptionsView.ShowIndicator = false;
            this.xContentTree1.Size = new System.Drawing.Size(512, 246);
            this.xContentTree1.TabIndex = 7;
            this.xContentTree1.UseType = SoftObject.TrainConcept.Libraries.ContentTreeViewList.UseType.FullContent;
            this.xContentTree1.Visible = false;
            this.xContentTree1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.xContentTree1_FocusedNodeChanged);
            this.xContentTree1.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.xContentTree1_ValidatingEditor);
            this.xContentTree1.ShownEditor += new System.EventHandler(this.xContentTree1_ShownEditor);
            this.xContentTree1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.xContentTree1_ShowingEditor);
            this.xContentTree1.DragDrop += new System.Windows.Forms.DragEventHandler(this.xContentTree1_DragDrop);
            this.xContentTree1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xContentTree1_KeyDown);
            this.xContentTree1.Leave += new System.EventHandler(this.xContentTree1_Leave);
            // 
            // bbgFontStyle
            // 
            this.bbgFontStyle.Caption = "Font Style";
            this.bbgFontStyle.Id = 2;
            this.bbgFontStyle.Name = "bbgFontStyle";
            // 
            // opFileDlgContent
            // 
            this.opFileDlgContent.DefaultExt = "jpeg";
            this.opFileDlgContent.Title = "Bitte wählen sie ein Bild aus";
            // 
            // opFileDlgDocuments
            // 
            this.opFileDlgDocuments.DefaultExt = "rtf";
            this.opFileDlgDocuments.Filter = "Alle Dateien (*.*)|*.*|Aufgaben-Files (*.rtf)|*.rtf|PDF-Files (*.pdf)|*.pdf|PPTX " +
    "Files (*.pptx)|*.pptx";
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl1;
            // 
            // panContentBrowser
            // 
            this.panContentBrowser.Controls.Add(this.transparentFrameControl1);
            this.panContentBrowser.Controls.Add(this.txtSelectedOverview);
            this.panContentBrowser.Controls.Add(this.axWebBrowser1);
            this.panContentBrowser.Location = new System.Drawing.Point(0, 0);
            this.panContentBrowser.Name = "panContentBrowser";
            this.panContentBrowser.Size = new System.Drawing.Size(483, 260);
            this.panContentBrowser.TabIndex = 8;
            // 
            // transparentFrameControl1
            // 
            this.transparentFrameControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.transparentFrameControl1.BackColor = System.Drawing.Color.Transparent;
            this.transparentFrameControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.transparentFrameControl1.Location = new System.Drawing.Point(203, 94);
            this.transparentFrameControl1.Margin = new System.Windows.Forms.Padding(6);
            this.transparentFrameControl1.Name = "transparentFrameControl1";
            this.transparentFrameControl1.Opacity = 100;
            this.transparentFrameControl1.Size = new System.Drawing.Size(0, 0);
            this.transparentFrameControl1.TabIndex = 8;
            this.transparentFrameControl1.Visible = false;
            // 
            // txtSelectedOverview
            // 
            this.txtSelectedOverview.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtSelectedOverview.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSelectedOverview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSelectedOverview.Location = new System.Drawing.Point(0, 0);
            this.txtSelectedOverview.Name = "txtSelectedOverview";
            this.txtSelectedOverview.ReadOnly = true;
            this.txtSelectedOverview.Size = new System.Drawing.Size(483, 35);
            this.txtSelectedOverview.TabIndex = 7;
            // 
            // FrmEditContent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 20);
            this.ClientSize = new System.Drawing.Size(796, 878);
            this.Controls.Add(this.panQuestions);
            this.Controls.Add(this.panContent);
            this.Controls.Add(this.panButtonBar);
            this.Controls.Add(this.panOverview);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.xContentTree1);
            this.Controls.Add(this.panContentBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEditContent";
            this.Text = "FrmEditContent";
            this.Closed += new System.EventHandler(this.FrmEditContent_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar3)).EndInit();
            this.panOverview.ResumeLayout(false);
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.panButtonBar.ResumeLayout(false);
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuNewAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gddTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabCtrl_Content)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuSetImageQu)).EndInit();
            this.panQuestions.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabCtrl_Questions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xContentTree1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.panContentBrowser.ResumeLayout(false);
            this.panContentBrowser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private XContentTree xContentTree1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panButtonBar;
        private System.Windows.Forms.Panel panOverview;
        private System.Windows.Forms.Button btnOverview;
        private System.Windows.Forms.Button btnContent;
        private System.Windows.Forms.Button btnQuestions;
        private System.Windows.Forms.Panel panContent;
        private System.Windows.Forms.Panel panQuestions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.Button btnOverviewDelete;
        private System.Windows.Forms.Label lblOvwTitle;
        private System.Windows.Forms.TextBox txtOverviewTitle;
        private System.Windows.Forms.Label lblOvwTyp;
        private System.Windows.Forms.TextBox txtOverviewType;
        private System.Windows.Forms.Button btnOverviewInsertPrev;
        private DevExpress.XtraTab.XtraTabControl xtraTabCtrl_Content = null;
        private DevExpress.XtraTab.XtraTabControl xtraTabCtrl_Questions = null;
        private WebBrowserEx axWebBrowser1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSave;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.RibbonGalleryBarItem ribbonGalleryBarItemTemplates;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageTemplates;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupTemplates;
        private DevExpress.XtraBars.BarButtonGroup bbgFontStyle;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.Ribbon.GalleryDropDown gddTemplates;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog opFileDlgContent;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageDocuments;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupNotices;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupPDF;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageGraphics;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupPosition;
        private DevExpress.XtraBars.BarButtonItem barButtonLeft;
        private DevExpress.XtraBars.BarButtonItem barButtonRight;
        private DevExpress.XtraBars.BarButtonGroup barMoveImage;
        private DevExpress.XtraBars.BarButtonItem barButtonUp;
        private DevExpress.XtraBars.BarButtonItem barButtonDown;
        private DevExpress.XtraBars.BarEditItem barEditWidth;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
        private DevExpress.XtraBars.BarEditItem barEditHeight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar2;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevComponents.DotNetBar.ButtonX btnAddQuestion;
        private DevComponents.DotNetBar.ButtonX btnDelQuestion;
        private DevExpress.XtraEditors.DropDownButton btnSetImageQu;
        private DevExpress.XtraBars.BarListItem barChooseQuestionImage;
        private DevExpress.XtraBars.PopupMenu popupMenuSetImageQu;
        private DevComponents.DotNetBar.ButtonX btnEdit;
        private DevExpress.XtraEditors.DropDownButton btnNewAction;
        private DevExpress.XtraBars.BarListItem barNewActionItems;
        private DevExpress.XtraBars.PopupMenu popupMenuNewAction;
        private System.Windows.Forms.OpenFileDialog opFileDlgDocuments;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAddDoc;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupControl;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelDoc;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private DevComponents.DotNetBar.ButtonX btnDuplicate;
        private System.Windows.Forms.TextBox txtOvwPageCount;
        private System.Windows.Forms.Label lblOvwPageCnt;
        private System.Windows.Forms.Panel panContentBrowser;
        private System.Windows.Forms.TextBox txtSelectedOverview;
        private System.Windows.Forms.Button btnSettings;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupPPT;
        private TransparentFrameControl transparentFrameControl1;
        private DevExpress.XtraBars.BarEditItem barSelectImageId;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevComponents.DotNetBar.ButtonX btnDuplicateQu;
        private DevComponents.DotNetBar.ButtonX btnDelAction;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblVersion;
        private DevExpress.XtraBars.BarCheckItem barCheckProp;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupxAPI;
    }
}
