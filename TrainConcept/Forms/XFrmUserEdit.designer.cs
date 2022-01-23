using SoftObject.TrainConcept.Controls;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmUserEdit
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFrmUserEdit));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbgAdminType = new DevExpress.XtraEditors.RadioGroup();
            this.btnResetProgress = new System.Windows.Forms.Button();
            this.btnSetPicture = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSyncUsers = new DevExpress.XtraEditors.SimpleButton();
            this.xUsersTree1 = new XUsersTree();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbgAdminType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSyncUsers);
            this.panel1.Controls.Add(this.rbgAdminType);
            this.panel1.Controls.Add(this.btnResetProgress);
            this.panel1.Controls.Add(this.btnSetPicture);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 63);
            this.panel1.TabIndex = 8;
            // 
            // rbgAdminType
            // 
            this.rbgAdminType.EditValue = 0;
            this.rbgAdminType.Location = new System.Drawing.Point(500, 16);
            this.rbgAdminType.Name = "rbgAdminType";
            this.rbgAdminType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.rbgAdminType.Properties.Appearance.Options.UseBackColor = true;
            this.rbgAdminType.Properties.Columns = 2;
            this.rbgAdminType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Lokale Userverwaltung"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Zentrale Userverwaltung", false)});
            this.rbgAdminType.Size = new System.Drawing.Size(291, 39);
            this.rbgAdminType.TabIndex = 9;
            this.rbgAdminType.EditValueChanged += new System.EventHandler(this.rbgAdminType_EditValueChanged);
            this.rbgAdminType.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rbgAdminType_EditValueChanging);
            // 
            // btnResetProgress
            // 
            this.btnResetProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetProgress.Location = new System.Drawing.Point(399, 16);
            this.btnResetProgress.Name = "btnResetProgress";
            this.btnResetProgress.Size = new System.Drawing.Size(93, 36);
            this.btnResetProgress.TabIndex = 8;
            this.btnResetProgress.Text = "Lernfortschritt löschen";
            this.btnResetProgress.Click += new System.EventHandler(this.btnResetProgress_Click);
            // 
            // btnSetPicture
            // 
            this.btnSetPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPicture.Location = new System.Drawing.Point(303, 16);
            this.btnSetPicture.Name = "btnSetPicture";
            this.btnSetPicture.Size = new System.Drawing.Size(80, 36);
            this.btnSetPicture.TabIndex = 7;
            this.btnSetPicture.Text = "Bild setzen";
            this.btnSetPicture.Click += new System.EventHandler(this.btnSetPicture_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(107, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 36);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Löschen";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(205, 16);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 36);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Bearbeiten";
            this.btnEdit.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(9, 16);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 36);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "Neu";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSyncUsers
            // 
            this.btnSyncUsers.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSyncUsers.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnSyncUsers.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnSyncUsers.Location = new System.Drawing.Point(847, 0);
            this.btnSyncUsers.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.btnSyncUsers.Name = "btnSyncUsers";
            this.btnSyncUsers.Size = new System.Drawing.Size(66, 63);
            this.btnSyncUsers.TabIndex = 10;
            this.btnSyncUsers.Text = "Sync";
            this.btnSyncUsers.Click += new System.EventHandler(this.btnSyncUsers_Click);
            // 
            // xUsersTree1
            // 
            this.xUsersTree1.ClassName = "";
            this.xUsersTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xUsersTree1.IsCheckable = false;
            this.xUsersTree1.Location = new System.Drawing.Point(0, 0);
            this.xUsersTree1.Name = "xUsersTree1";
            this.xUsersTree1.Size = new System.Drawing.Size(913, 416);
            this.xUsersTree1.TabIndex = 1;
            this.xUsersTree1.Type = XUsersTree.ViewType.EditView;
            this.xUsersTree1.UserList = null;
            this.xUsersTree1.OnSelectionChangedEvent += new OnSelectionChangedHandler(this.xUsersTree1_OnSelectionChangedEvent);
            this.xUsersTree1.OnSelectionDblClkEvent += new OnSelectionDblClkEventHandler(this.xUsersTree1_OnSelectionDblClkEvent);
            // 
            // XFrmUserEdit
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(913, 479);
            this.Controls.Add(this.xUsersTree1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XFrmUserEdit";
            this.Text = "XFrmUserEdit";
            this.Closed += new System.EventHandler(this.XFrmUserEdit_Closed);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbgAdminType.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSetPicture;
        private XUsersTree xUsersTree1;
        private System.Windows.Forms.Button btnResetProgress;
        private DevExpress.XtraEditors.RadioGroup rbgAdminType;
        private DevExpress.XtraEditors.SimpleButton btnSyncUsers;

    }
}
