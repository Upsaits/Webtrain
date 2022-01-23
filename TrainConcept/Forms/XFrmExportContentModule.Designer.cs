namespace SoftObject.TrainConcept.Forms
{
    partial class XFrmExportContentModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFrmExportContentModule));
            this.edtTitle = new DevExpress.XtraEditors.TextEdit();
            this.lblCtrlTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblCtrlAutor = new DevExpress.XtraEditors.LabelControl();
            this.edtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.edtLanguage = new DevExpress.XtraEditors.TextEdit();
            this.btnChangeExport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edtTargetPath = new DevExpress.XtraEditors.TextEdit();
            this.chkPublish = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.edtAutor = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.edtVersion = new DevExpress.XtraEditors.TextEdit();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.edtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLanguage.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtTargetPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtAutor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtVersion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // edtTitle
            // 
            this.edtTitle.Location = new System.Drawing.Point(66, 52);
            this.edtTitle.Name = "edtTitle";
            this.edtTitle.Properties.ReadOnly = true;
            this.edtTitle.Size = new System.Drawing.Size(387, 20);
            this.edtTitle.TabIndex = 0;
            // 
            // lblCtrlTitle
            // 
            this.lblCtrlTitle.Location = new System.Drawing.Point(13, 55);
            this.lblCtrlTitle.Name = "lblCtrlTitle";
            this.lblCtrlTitle.Size = new System.Drawing.Size(24, 13);
            this.lblCtrlTitle.TabIndex = 1;
            this.lblCtrlTitle.Text = "Titel:";
            // 
            // lblCtrlAutor
            // 
            this.lblCtrlAutor.Location = new System.Drawing.Point(12, 88);
            this.lblCtrlAutor.Name = "lblCtrlAutor";
            this.lblCtrlAutor.Size = new System.Drawing.Size(31, 13);
            this.lblCtrlAutor.TabIndex = 2;
            this.lblCtrlAutor.Text = "Autor:";
            // 
            // edtDescription
            // 
            this.edtDescription.Location = new System.Drawing.Point(66, 135);
            this.edtDescription.Name = "edtDescription";
            this.edtDescription.Size = new System.Drawing.Size(387, 70);
            this.edtDescription.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Sprache:";
            // 
            // edtLanguage
            // 
            this.edtLanguage.Location = new System.Drawing.Point(66, 13);
            this.edtLanguage.Name = "edtLanguage";
            this.edtLanguage.Properties.ReadOnly = true;
            this.edtLanguage.Size = new System.Drawing.Size(91, 20);
            this.edtLanguage.TabIndex = 5;
            // 
            // btnChangeExport
            // 
            this.btnChangeExport.Location = new System.Drawing.Point(363, 16);
            this.btnChangeExport.Name = "btnChangeExport";
            this.btnChangeExport.Size = new System.Drawing.Size(75, 23);
            this.btnChangeExport.TabIndex = 6;
            this.btnChangeExport.Text = "Ändern";
            this.btnChangeExport.UseVisualStyleBackColor = true;
            this.btnChangeExport.Click += new System.EventHandler(this.btnChangeExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edtTargetPath);
            this.groupBox1.Controls.Add(this.btnChangeExport);
            this.groupBox1.Location = new System.Drawing.Point(18, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 43);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zieldatei";
            // 
            // edtTargetPath
            // 
            this.edtTargetPath.Location = new System.Drawing.Point(6, 17);
            this.edtTargetPath.Name = "edtTargetPath";
            this.edtTargetPath.Size = new System.Drawing.Size(351, 20);
            this.edtTargetPath.TabIndex = 7;
            // 
            // chkPublish
            // 
            this.chkPublish.AutoSize = true;
            this.chkPublish.Location = new System.Drawing.Point(24, 302);
            this.chkPublish.Name = "chkPublish";
            this.chkPublish.Size = new System.Drawing.Size(99, 17);
            this.chkPublish.TabIndex = 8;
            this.chkPublish.Text = "Veröffentlichen";
            this.chkPublish.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(187, 298);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Exportieren";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 298);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // edtAutor
            // 
            this.edtAutor.Location = new System.Drawing.Point(67, 84);
            this.edtAutor.Name = "edtAutor";
            this.edtAutor.Size = new System.Drawing.Size(386, 20);
            this.edtAutor.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 116);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 13);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "Beschreibung:";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "ZIP Files (*.zip)|*.zip|All Files (*.*)|*.*";
            // 
            // edtVersion
            // 
            this.edtVersion.Location = new System.Drawing.Point(362, 9);
            this.edtVersion.Name = "edtVersion";
            this.edtVersion.Properties.ReadOnly = true;
            this.edtVersion.Size = new System.Drawing.Size(91, 20);
            this.edtVersion.TabIndex = 14;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(309, 12);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(39, 13);
            this.lblVersion.TabIndex = 13;
            this.lblVersion.Text = "Version:";
            // 
            // XFrmExportContentModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 339);
            this.Controls.Add(this.edtVersion);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.edtAutor);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.chkPublish);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.edtLanguage);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.edtDescription);
            this.Controls.Add(this.lblCtrlAutor);
            this.Controls.Add(this.lblCtrlTitle);
            this.Controls.Add(this.edtTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.Name = "XFrmExportContentModule";
            this.Text = "Exportieren";
            ((System.ComponentModel.ISupportInitialize)(this.edtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLanguage.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edtTargetPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtAutor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtVersion.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit edtTitle;
        private DevExpress.XtraEditors.LabelControl lblCtrlTitle;
        private DevExpress.XtraEditors.LabelControl lblCtrlAutor;
        private DevExpress.XtraEditors.MemoEdit edtDescription;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit edtLanguage;
        private System.Windows.Forms.Button btnChangeExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit edtTargetPath;
        private System.Windows.Forms.CheckBox chkPublish;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private DevExpress.XtraEditors.TextEdit edtAutor;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraEditors.TextEdit edtVersion;
        private DevExpress.XtraEditors.LabelControl lblVersion;
    }
}