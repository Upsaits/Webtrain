namespace SoftObject.TrainConcept.Controls
{
    public partial class XMapsTree
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

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colColor = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.colTitle = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUserList = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.colClassList = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckedComboBoxEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Appearance.SelectedRow.BackColor = System.Drawing.Color.Blue;
            this.treeList1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colColor,
            this.colTitle,
            this.colUserList,
            this.colClassList});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsNavigation.MoveOnEdit = false;
            this.treeList1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckedComboBoxEdit1,
            this.repositoryItemCheckedComboBoxEdit2,
            this.repositoryItemColorEdit1});
            this.treeList1.SelectImageList = this.imageList1;
            this.treeList1.Size = new System.Drawing.Size(404, 435);
            this.treeList1.StateImageList = this.imageList2;
            this.treeList1.TabIndex = 0;
            this.treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
            this.treeList1.SelectionChanged += new System.EventHandler(this.treeList1_SelectionChanged);
            this.treeList1.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeList1_CustomDrawNodeCell);
            this.treeList1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDown);
            // 
            // colColor
            // 
            this.colColor.Caption = "Farbe";
            this.colColor.ColumnEdit = this.repositoryItemColorEdit1;
            this.colColor.CustomizationCaption = "Farbe";
            this.colColor.FieldName = "Color";
            this.colColor.MinWidth = 65;
            this.colColor.Name = "colColor";
            this.colColor.Visible = true;
            this.colColor.VisibleIndex = 0;
            this.colColor.Width = 114;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            // 
            // colTitle
            // 
            this.colTitle.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTitle.AppearanceCell.Options.UseFont = true;
            this.colTitle.AppearanceCell.Options.UseTextOptions = true;
            this.colTitle.Caption = "Lernmappe";
            this.colTitle.FieldName = "Content";
            this.colTitle.MinWidth = 99;
            this.colTitle.Name = "colTitle";
            this.colTitle.OptionsColumn.AllowEdit = false;
            this.colTitle.OptionsColumn.AllowMove = false;
            this.colTitle.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colTitle.OptionsColumn.AllowSort = false;
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 1;
            this.colTitle.Width = 117;
            // 
            // colUserList
            // 
            this.colUserList.Caption = "Benutzerliste";
            this.colUserList.ColumnEdit = this.repositoryItemCheckedComboBoxEdit1;
            this.colUserList.FieldName = "UserList";
            this.colUserList.Name = "colUserList";
            this.colUserList.OptionsColumn.AllowMove = false;
            this.colUserList.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colUserList.OptionsColumn.AllowSort = false;
            this.colUserList.Visible = true;
            this.colUserList.VisibleIndex = 2;
            this.colUserList.Width = 93;
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // colClassList
            // 
            this.colClassList.Caption = "Klassenliste";
            this.colClassList.ColumnEdit = this.repositoryItemCheckedComboBoxEdit2;
            this.colClassList.FieldName = "ClassList";
            this.colClassList.Name = "colClassList";
            this.colClassList.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colClassList.OptionsColumn.AllowSize = false;
            this.colClassList.OptionsColumn.AllowSort = false;
            this.colClassList.Visible = true;
            this.colClassList.VisibleIndex = 3;
            this.colClassList.Width = 63;
            // 
            // repositoryItemCheckedComboBoxEdit2
            // 
            this.repositoryItemCheckedComboBoxEdit2.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit2.Name = "repositoryItemCheckedComboBoxEdit2";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // XMapsTree
            // 
            this.Controls.Add(this.treeList1);
            this.Name = "XMapsTree";
            this.Size = new System.Drawing.Size(404, 435);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit2)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTitle;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUserList;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colClassList;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colColor;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;

    }
}
