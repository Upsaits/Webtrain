namespace SoftObject.TrainConcept.Forms
{
    partial class XFrmImportContentModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFrmImportContentModule));
            this.lblFile = new DevExpress.XtraEditors.LabelControl();
            this.edtFile = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edtLanguage = new DevExpress.XtraEditors.TextEdit();
            this.lblLanguage = new DevExpress.XtraEditors.LabelControl();
            this.dtCreated = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.edtAutor = new DevExpress.XtraEditors.TextEdit();
            this.edtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.lblCtrlAutor = new DevExpress.XtraEditors.LabelControl();
            this.lblCtrlTitle = new DevExpress.XtraEditors.LabelControl();
            this.edtTitle = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.edtWarning = new DevExpress.XtraEditors.MemoEdit();
            this.picWarning = new System.Windows.Forms.PictureBox();
            this.chkAdaptTemplates = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.edtFile.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtLanguage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreated.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreated.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtAutor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtWarning.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFile
            // 
            this.lblFile.Enabled = false;
            this.lblFile.Location = new System.Drawing.Point(17, 23);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(29, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "Datei:";
            // 
            // edtFile
            // 
            this.edtFile.Location = new System.Drawing.Point(71, 20);
            this.edtFile.Name = "edtFile";
            this.edtFile.Properties.ReadOnly = true;
            this.edtFile.Size = new System.Drawing.Size(387, 20);
            this.edtFile.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edtLanguage);
            this.groupBox1.Controls.Add(this.lblLanguage);
            this.groupBox1.Controls.Add(this.dtCreated);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.edtAutor);
            this.groupBox1.Controls.Add(this.edtDescription);
            this.groupBox1.Controls.Add(this.lblCtrlAutor);
            this.groupBox1.Controls.Add(this.lblCtrlTitle);
            this.groupBox1.Controls.Add(this.edtTitle);
            this.groupBox1.Controls.Add(this.edtFile);
            this.groupBox1.Controls.Add(this.lblFile);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(13, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 267);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inhalt";
            // 
            // edtLanguage
            // 
            this.edtLanguage.Location = new System.Drawing.Point(317, 57);
            this.edtLanguage.Name = "edtLanguage";
            this.edtLanguage.Properties.ReadOnly = true;
            this.edtLanguage.Size = new System.Drawing.Size(100, 20);
            this.edtLanguage.TabIndex = 22;
            // 
            // lblLanguage
            // 
            this.lblLanguage.Location = new System.Drawing.Point(257, 57);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(43, 13);
            this.lblLanguage.TabIndex = 21;
            this.lblLanguage.Text = "Sprache:";
            // 
            // dtCreated
            // 
            this.dtCreated.EditValue = null;
            this.dtCreated.Location = new System.Drawing.Point(100, 57);
            this.dtCreated.Name = "dtCreated";
            this.dtCreated.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtCreated.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtCreated.Properties.ReadOnly = true;
            this.dtCreated.Size = new System.Drawing.Size(100, 20);
            this.dtCreated.TabIndex = 20;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 13);
            this.labelControl1.TabIndex = 19;
            this.labelControl1.Text = "erstellt am:";
            // 
            // labelControl2
            // 
            this.labelControl2.Enabled = false;
            this.labelControl2.Location = new System.Drawing.Point(17, 165);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 13);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Beschreibung:";
            // 
            // edtAutor
            // 
            this.edtAutor.Location = new System.Drawing.Point(72, 133);
            this.edtAutor.Name = "edtAutor";
            this.edtAutor.Properties.ReadOnly = true;
            this.edtAutor.Size = new System.Drawing.Size(386, 20);
            this.edtAutor.TabIndex = 17;
            // 
            // edtDescription
            // 
            this.edtDescription.Location = new System.Drawing.Point(71, 184);
            this.edtDescription.Name = "edtDescription";
            this.edtDescription.Properties.ReadOnly = true;
            this.edtDescription.Size = new System.Drawing.Size(387, 70);
            this.edtDescription.TabIndex = 16;
            // 
            // lblCtrlAutor
            // 
            this.lblCtrlAutor.Enabled = false;
            this.lblCtrlAutor.Location = new System.Drawing.Point(17, 137);
            this.lblCtrlAutor.Name = "lblCtrlAutor";
            this.lblCtrlAutor.Size = new System.Drawing.Size(31, 13);
            this.lblCtrlAutor.TabIndex = 15;
            this.lblCtrlAutor.Text = "Autor:";
            // 
            // lblCtrlTitle
            // 
            this.lblCtrlTitle.Enabled = false;
            this.lblCtrlTitle.Location = new System.Drawing.Point(18, 104);
            this.lblCtrlTitle.Name = "lblCtrlTitle";
            this.lblCtrlTitle.Size = new System.Drawing.Size(24, 13);
            this.lblCtrlTitle.TabIndex = 14;
            this.lblCtrlTitle.Text = "Titel:";
            // 
            // edtTitle
            // 
            this.edtTitle.Location = new System.Drawing.Point(71, 101);
            this.edtTitle.Name = "edtTitle";
            this.edtTitle.Properties.ReadOnly = true;
            this.edtTitle.Size = new System.Drawing.Size(387, 20);
            this.edtTitle.TabIndex = 13;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(23, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Suchen";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(162, 428);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Importieren";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(287, 428);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "ZIP Files (*.zip)|*.zip|All Files (*.*)|*.*";
            // 
            // edtWarning
            // 
            this.edtWarning.Location = new System.Drawing.Point(53, 356);
            this.edtWarning.Name = "edtWarning";
            this.edtWarning.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Salmon;
            this.edtWarning.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edtWarning.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.edtWarning.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.edtWarning.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.edtWarning.Properties.ReadOnly = true;
            this.edtWarning.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.edtWarning.Size = new System.Drawing.Size(445, 54);
            this.edtWarning.TabIndex = 6;
            this.edtWarning.Visible = false;
            // 
            // picWarning
            // 
            this.picWarning.Image = global::SoftObject.TrainConcept.Properties.Resources.warning_icon;
            this.picWarning.Location = new System.Drawing.Point(13, 357);
            this.picWarning.Name = "picWarning";
            this.picWarning.Size = new System.Drawing.Size(34, 28);
            this.picWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWarning.TabIndex = 7;
            this.picWarning.TabStop = false;
            this.picWarning.Visible = false;
            // 
            // chkAdaptTemplates
            // 
            this.chkAdaptTemplates.AutoSize = true;
            this.chkAdaptTemplates.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAdaptTemplates.Location = new System.Drawing.Point(13, 331);
            this.chkAdaptTemplates.Name = "chkAdaptTemplates";
            this.chkAdaptTemplates.Size = new System.Drawing.Size(244, 17);
            this.chkAdaptTemplates.TabIndex = 8;
            this.chkAdaptTemplates.Text = "Templates durch Standardtemplates ersetzen";
            this.chkAdaptTemplates.UseVisualStyleBackColor = true;
            this.chkAdaptTemplates.CheckedChanged += new System.EventHandler(this.chkAdaptTemplates_CheckedChanged);
            // 
            // XFrmImportContentModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 467);
            this.Controls.Add(this.chkAdaptTemplates);
            this.Controls.Add(this.picWarning);
            this.Controls.Add(this.edtWarning);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XFrmImportContentModule";
            this.Text = "Inhalte Importieren";
            ((System.ComponentModel.ISupportInitialize)(this.edtFile.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtLanguage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreated.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCreated.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtAutor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtWarning.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblFile;
        private DevExpress.XtraEditors.TextEdit edtFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit edtAutor;
        private DevExpress.XtraEditors.MemoEdit edtDescription;
        private DevExpress.XtraEditors.LabelControl lblCtrlAutor;
        private DevExpress.XtraEditors.LabelControl lblCtrlTitle;
        private DevExpress.XtraEditors.TextEdit edtTitle;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.DateEdit dtCreated;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit edtLanguage;
        private DevExpress.XtraEditors.LabelControl lblLanguage;
        private DevExpress.XtraEditors.MemoEdit edtWarning;
        private System.Windows.Forms.PictureBox picWarning;
        private System.Windows.Forms.CheckBox chkAdaptTemplates;
    }
}