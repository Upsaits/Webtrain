namespace SoftObject.TrainConcept.Controls
{
    public partial class XUsersTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XUsersTree));
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLongName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLoginTime = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIPAddress = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colOnlineStatistics = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMapsState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.userEditTreeRecordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userEditTreeRecordBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Appearance.Empty.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.Empty.Options.UseFont = true;
            this.treeList1.Appearance.EvenRow.BackColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeList1.Appearance.EvenRow.Options.UseFont = true;
            this.treeList1.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.FocusedCell.Options.UseFont = true;
            this.treeList1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.treeList1.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.OddRow.Options.UseBackColor = true;
            this.treeList1.Appearance.OddRow.Options.UseFont = true;
            this.treeList1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.treeList1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.Row.Options.UseBackColor = true;
            this.treeList1.Appearance.Row.Options.UseFont = true;
            this.treeList1.BestFitVisibleOnly = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colShortName,
            this.colLongName,
            this.colTypeName,
            this.colLoginTime,
            this.colIPAddress,
            this.colOnlineStatistics,
            this.colMapsState});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.treeList1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsNavigation.MoveOnEdit = false;
            this.treeList1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeList1.OptionsSelection.MultiSelect = true;
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.treeList1.SelectImageList = this.imageList1;
            this.treeList1.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.treeList1.Size = new System.Drawing.Size(470, 414);
            this.treeList1.StateImageList = this.imageList2;
            this.treeList1.TabIndex = 10;
            this.treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
            this.treeList1.SelectionChanged += new System.EventHandler(this.treeList1_SelectionChanged);
            this.treeList1.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeList1_CustomDrawNodeCell);
            this.treeList1.DoubleClick += new System.EventHandler(this.treeList1_DoubleClick);
            this.treeList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeList1_KeyDown);
            this.treeList1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDown);
            // 
            // colShortName
            // 
            this.colShortName.Caption = "Benutzername";
            this.colShortName.FieldName = "UserName";
            this.colShortName.MinWidth = 289;
            this.colShortName.Name = "colShortName";
            this.colShortName.OptionsColumn.AllowEdit = false;
            this.colShortName.OptionsColumn.AllowMove = false;
            this.colShortName.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colShortName.Visible = true;
            this.colShortName.VisibleIndex = 0;
            this.colShortName.Width = 189;
            // 
            // colLongName
            // 
            this.colLongName.Caption = "Name";
            this.colLongName.FieldName = "FullName";
            this.colLongName.Name = "colLongName";
            this.colLongName.OptionsColumn.AllowEdit = false;
            this.colLongName.OptionsColumn.AllowMove = false;
            this.colLongName.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colLongName.Visible = true;
            this.colLongName.VisibleIndex = 1;
            this.colLongName.Width = 88;
            // 
            // colTypeName
            // 
            this.colTypeName.Caption = "Benutzertyp";
            this.colTypeName.FieldName = "UserType";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.OptionsColumn.AllowEdit = false;
            this.colTypeName.OptionsColumn.AllowMove = false;
            this.colTypeName.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colTypeName.Visible = true;
            this.colTypeName.VisibleIndex = 2;
            this.colTypeName.Width = 88;
            // 
            // colLoginTime
            // 
            this.colLoginTime.Caption = "Login-Zeit";
            this.colLoginTime.FieldName = "LoginTime";
            this.colLoginTime.Name = "colLoginTime";
            this.colLoginTime.OptionsColumn.AllowEdit = false;
            this.colLoginTime.OptionsColumn.AllowMove = false;
            this.colLoginTime.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colLoginTime.Visible = true;
            this.colLoginTime.VisibleIndex = 3;
            // 
            // colIPAddress
            // 
            this.colIPAddress.Caption = "IP-Adresse";
            this.colIPAddress.FieldName = "IPAdress";
            this.colIPAddress.Name = "colIPAddress";
            this.colIPAddress.OptionsColumn.AllowEdit = false;
            this.colIPAddress.OptionsColumn.AllowMove = false;
            this.colIPAddress.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colIPAddress.Visible = true;
            this.colIPAddress.VisibleIndex = 4;
            // 
            // colOnlineStatistics
            // 
            this.colOnlineStatistics.Caption = "Online-Statistik";
            this.colOnlineStatistics.FieldName = "MapsProgress";
            this.colOnlineStatistics.Name = "colOnlineStatistics";
            this.colOnlineStatistics.OptionsColumn.AllowEdit = false;
            this.colOnlineStatistics.OptionsColumn.AllowMove = false;
            this.colOnlineStatistics.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colOnlineStatistics.OptionsColumn.AllowSort = false;
            this.colOnlineStatistics.Visible = true;
            this.colOnlineStatistics.VisibleIndex = 5;
            // 
            // colMapsState
            // 
            this.colMapsState.Caption = "Lernfortschritt";
            this.colMapsState.ColumnEdit = this.repositoryItemProgressBar1;
            this.colMapsState.FieldName = "MapsProgress";
            this.colMapsState.Name = "colMapsState";
            this.colMapsState.OptionsColumn.AllowEdit = false;
            this.colMapsState.OptionsColumn.AllowFocus = false;
            this.colMapsState.OptionsColumn.AllowMove = false;
            this.colMapsState.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.colMapsState.OptionsColumn.ReadOnly = true;
            this.colMapsState.Visible = true;
            this.colMapsState.VisibleIndex = 6;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.repositoryItemProgressBar1.EndColor = System.Drawing.Color.Lime;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.repositoryItemProgressBar1.ReadOnly = true;
            this.repositoryItemProgressBar1.ShowTitle = true;
            this.repositoryItemProgressBar1.StartColor = System.Drawing.Color.Green;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(64, 64);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            // 
            // userEditTreeRecordBindingSource
            // 
            this.userEditTreeRecordBindingSource.DataSource = typeof(UserEditTreeRecord);
            // 
            // XUsersTree
            // 
            this.Controls.Add(this.treeList1);
            this.Name = "XUsersTree";
            this.Size = new System.Drawing.Size(470, 414);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userEditTreeRecordBindingSource)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShortName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLongName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTypeName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.BindingSource userEditTreeRecordBindingSource;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLoginTime;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIPAddress;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOnlineStatistics;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMapsState;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;

    }
}
