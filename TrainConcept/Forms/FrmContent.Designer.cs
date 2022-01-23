using SoftObject.TrainConcept.Controls;
using SoftObject.SOComponents.Controls;

namespace SoftObject.TrainConcept.Forms
{
    partial class FrmContent
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContent));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.topPanel = new System.Windows.Forms.Panel();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.label1 = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.ctrlBarPanel = new System.Windows.Forms.Panel();
            this.soChart1 = new SOChart();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnNotices = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.btnCertification = new System.Windows.Forms.Button();
            this.btnTests = new System.Windows.Forms.Button();
            this.clientPanel = new System.Windows.Forms.Panel();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xTreeViewTests = new XTestTreeView();
            this.xTreeViewNotices = new XNoticeTreeView(this.components);
            this.timerMapProgress = new System.Windows.Forms.Timer(this.components);
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.ctrlBarPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTreeViewTests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTreeViewNotices)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.topPanel.Controls.Add(this.progressBarControl1);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(992, 34);
            this.topPanel.TabIndex = 2;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.progressBarControl1.EditValue = 50;
            this.progressBarControl1.Location = new System.Drawing.Point(785, 0);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.progressBarControl1.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressBarControl1.Properties.AppearanceDisabled.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressBarControl1.Properties.AppearanceFocused.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressBarControl1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.progressBarControl1.Properties.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.progressBarControl1.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.progressBarControl1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Properties.StartColor = System.Drawing.Color.Green;
            this.progressBarControl1.Properties.Step = 1;
            this.progressBarControl1.Size = new System.Drawing.Size(207, 34);
            this.progressBarControl1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(729, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.ctrlBarPanel);
            this.bottomPanel.Controls.Add(this.buttonPanel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 497);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(992, 65);
            this.bottomPanel.TabIndex = 7;
            // 
            // ctrlBarPanel
            // 
            this.ctrlBarPanel.Controls.Add(this.soChart1);
            this.ctrlBarPanel.Location = new System.Drawing.Point(428, 6);
            this.ctrlBarPanel.Name = "ctrlBarPanel";
            this.ctrlBarPanel.Size = new System.Drawing.Size(396, 48);
            this.ctrlBarPanel.TabIndex = 13;
            this.ctrlBarPanel.Visible = false;
            // 
            // soChart1
            // 
            this.soChart1.Alpha1ONE = 255;
            this.soChart1.Alpha1THREE = 255;
            this.soChart1.Alpha1TWO = 255;
            this.soChart1.Alpha2ONE = 155;
            this.soChart1.Alpha2THREE = 155;
            this.soChart1.Alpha2TWO = 155;
            this.soChart1.BackColor = System.Drawing.Color.FloralWhite;
            this.soChart1.Color1ONE = System.Drawing.Color.DarkSlateBlue;
            this.soChart1.Color1THREE = System.Drawing.Color.Maroon;
            this.soChart1.Color1TWO = System.Drawing.Color.DarkOliveGreen;
            this.soChart1.Color2ONE = System.Drawing.Color.DarkBlue;
            this.soChart1.Color2THREE = System.Drawing.Color.DarkRed;
            this.soChart1.Color2TWO = System.Drawing.Color.DarkGreen;
            this.soChart1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.soChart1.Delay = 0;
            this.soChart1.Dock = System.Windows.Forms.DockStyle.Left;
            this.soChart1.EndSize = new System.Drawing.Size(300, 150);
            this.soChart1.Font = new System.Drawing.Font("Arial", 8F);
            this.soChart1.FrameColor = System.Drawing.Color.Orange;
            this.soChart1.Location = new System.Drawing.Point(0, 0);
            this.soChart1.Name = "soChart1";
            this.soChart1.PercentONE = 0;
            this.soChart1.PercentTHREE = 0;
            this.soChart1.PercentTWO = 0;
            this.soChart1.Size = new System.Drawing.Size(25, 48);
            this.soChart1.TabIndex = 8;
            this.soChart1.TextColor = System.Drawing.Color.Blue;
            this.soChart1.TextONE = null;
            this.soChart1.TextTHREE = null;
            this.soChart1.TextTWO = null;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.btnNotices);
            this.buttonPanel.Controls.Add(this.btnDelete);
            this.buttonPanel.Controls.Add(this.btnResult);
            this.buttonPanel.Controls.Add(this.btnCertification);
            this.buttonPanel.Controls.Add(this.btnTests);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(992, 65);
            this.buttonPanel.TabIndex = 8;
            // 
            // btnNotices
            // 
            this.btnNotices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNotices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotices.Location = new System.Drawing.Point(135, 11);
            this.btnNotices.Name = "btnNotices";
            this.btnNotices.Size = new System.Drawing.Size(88, 34);
            this.btnNotices.TabIndex = 14;
            this.btnNotices.Text = "Notizen";
            this.btnNotices.Click += new System.EventHandler(this.btnNotices_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(450, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 34);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Löschen";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnResult
            // 
            this.btnResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResult.Location = new System.Drawing.Point(262, 11);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(88, 34);
            this.btnResult.TabIndex = 10;
            this.btnResult.Text = "Ergebnis";
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // btnCertification
            // 
            this.btnCertification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCertification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCertification.Location = new System.Drawing.Point(356, 11);
            this.btnCertification.Name = "btnCertification";
            this.btnCertification.Size = new System.Drawing.Size(88, 34);
            this.btnCertification.TabIndex = 11;
            this.btnCertification.Text = "Zertifikat";
            this.btnCertification.Click += new System.EventHandler(this.btnCertification_Click);
            // 
            // btnTests
            // 
            this.btnTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTests.Location = new System.Drawing.Point(41, 11);
            this.btnTests.Name = "btnTests";
            this.btnTests.Size = new System.Drawing.Size(88, 34);
            this.btnTests.TabIndex = 9;
            this.btnTests.Text = "Tests";
            this.btnTests.Click += new System.EventHandler(this.btnTests_Click);
            // 
            // clientPanel
            // 
            this.clientPanel.Controls.Add(this.xtraTabControl1);
            this.clientPanel.Controls.Add(this.xTreeViewTests);
            this.clientPanel.Controls.Add(this.xTreeViewNotices);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 34);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(992, 463);
            this.clientPanel.TabIndex = 8;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            serializableAppearanceObject1.BackColor = System.Drawing.Color.Gray;
            serializableAppearanceObject1.BorderColor = System.Drawing.SystemColors.Control;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Underline);
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.DarkGreen;
            serializableAppearanceObject1.Image = global::SoftObject.TrainConcept.Properties.Resources.true_icon;
            serializableAppearanceObject1.Options.UseBackColor = true;
            serializableAppearanceObject1.Options.UseBorderColor = true;
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            serializableAppearanceObject1.Options.UseImage = true;
            this.xtraTabControl1.CustomHeaderButtons.AddRange(new DevExpress.XtraTab.Buttons.CustomHeaderButton[] {
            new DevExpress.XtraTab.Buttons.CustomHeaderButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Übersicht", 70, true, true, editorButtonImageOptions1, serializableAppearanceObject1, "", null, null)});
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.WhenNeeded;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.Size = new System.Drawing.Size(992, 463);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            this.xtraTabControl1.CloseButtonClick += new System.EventHandler(this.xtraTabControl1_CloseButtonClick);
            this.xtraTabControl1.CustomHeaderButtonClick += new DevExpress.XtraTab.ViewInfo.CustomHeaderButtonEventHandler(this.xtraTabControl1_CustomHeaderButtonClick);
            // 
            // xTreeViewTests
            // 
            this.xTreeViewTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xTreeViewTests.KeyFieldName = "Id";
            this.xTreeViewTests.Location = new System.Drawing.Point(0, 0);
            this.xTreeViewTests.Name = "xTreeViewTests";
            this.xTreeViewTests.ParentFieldName = "ParentId";
            this.xTreeViewTests.Size = new System.Drawing.Size(992, 463);
            this.xTreeViewTests.TabIndex = 4;
            this.xTreeViewTests.Visible = false;
            this.xTreeViewTests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.testTreeView_KeyDown);
            this.xTreeViewTests.MouseDown += new System.Windows.Forms.MouseEventHandler(this.testTreeView_MouseDown);
            // 
            // xTreeViewNotices
            // 
            this.xTreeViewNotices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xTreeViewNotices.KeyFieldName = "Id";
            this.xTreeViewNotices.Location = new System.Drawing.Point(0, 0);
            this.xTreeViewNotices.Name = "xTreeViewNotices";
            this.xTreeViewNotices.ParentFieldName = "ParentId";
            this.xTreeViewNotices.Size = new System.Drawing.Size(992, 463);
            this.xTreeViewNotices.TabIndex = 4;
            this.xTreeViewNotices.Visible = false;
            this.xTreeViewNotices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xTreeViewNotices_KeyDown);
            this.xTreeViewNotices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xTreeViewNotices_MouseDown);
            // 
            // timerMapProgress
            // 
            this.timerMapProgress.Interval = 20000;
            this.timerMapProgress.Tick += new System.EventHandler(this.timerMapProgress_Tick);
            // 
            // FrmContent
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(992, 562);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(840, 558);
            this.Name = "FrmContent";
            this.Text = "FrmContent";
            this.Closed += new System.EventHandler(this.OnClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmContent_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmContent_FormClosed);
            this.Load += new System.EventHandler(this.FrmContent_Load);
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.ctrlBarPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTreeViewTests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTreeViewNotices)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private XTestTreeView xTreeViewTests;
        private XNoticeTreeView xTreeViewNotices;
        public System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel clientPanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnDelete;
        private SOChart soChart1;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.Button btnCertification;
        private System.Windows.Forms.Button btnTests;
        private System.Windows.Forms.Panel ctrlBarPanel;
        private System.Windows.Forms.Button btnNotices;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.Timer timerMapProgress;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
    };
}