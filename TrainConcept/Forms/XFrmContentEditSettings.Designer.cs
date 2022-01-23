namespace SoftObject.TrainConcept.Forms
{
    partial class XFrmContentEditSettings
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
            this.lblContentPath = new DevExpress.XtraEditors.LabelControl();
            this.txtContentFolder = new DevExpress.XtraEditors.TextEdit();
            this.btnSelectMediaFolder = new System.Windows.Forms.Button();
            this.txtMediaFolder = new DevExpress.XtraEditors.TextEdit();
            this.lblMediaPath = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCheckPPT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtContentFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaFolder.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContentPath
            // 
            this.lblContentPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblContentPath.Location = new System.Drawing.Point(13, 35);
            this.lblContentPath.Name = "lblContentPath";
            this.lblContentPath.Size = new System.Drawing.Size(90, 13);
            this.lblContentPath.TabIndex = 0;
            this.lblContentPath.Text = "Inhaltsverzeichnis:";
            // 
            // txtContentFolder
            // 
            this.txtContentFolder.EditValue = "Test";
            this.txtContentFolder.Enabled = false;
            this.txtContentFolder.Location = new System.Drawing.Point(110, 31);
            this.txtContentFolder.Name = "txtContentFolder";
            this.txtContentFolder.Size = new System.Drawing.Size(402, 20);
            this.txtContentFolder.TabIndex = 1;
            // 
            // btnSelectMediaFolder
            // 
            this.btnSelectMediaFolder.Location = new System.Drawing.Point(519, 58);
            this.btnSelectMediaFolder.Name = "btnSelectMediaFolder";
            this.btnSelectMediaFolder.Size = new System.Drawing.Size(47, 20);
            this.btnSelectMediaFolder.TabIndex = 2;
            this.btnSelectMediaFolder.Text = "...";
            this.btnSelectMediaFolder.UseVisualStyleBackColor = true;
            this.btnSelectMediaFolder.Click += new System.EventHandler(this.btnSelectMediaFolder_Click);
            // 
            // txtMediaFolder
            // 
            this.txtMediaFolder.EditValue = "Test";
            this.txtMediaFolder.Enabled = false;
            this.txtMediaFolder.Location = new System.Drawing.Point(110, 57);
            this.txtMediaFolder.Name = "txtMediaFolder";
            this.txtMediaFolder.Size = new System.Drawing.Size(402, 20);
            this.txtMediaFolder.TabIndex = 4;
            // 
            // lblMediaPath
            // 
            this.lblMediaPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblMediaPath.Location = new System.Drawing.Point(13, 61);
            this.lblMediaPath.Name = "lblMediaPath";
            this.lblMediaPath.Size = new System.Drawing.Size(91, 13);
            this.lblMediaPath.TabIndex = 3;
            this.lblMediaPath.Text = "Medienverzeichnis:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(249, 86);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnCheckPPT
            // 
            this.btnCheckPPT.Location = new System.Drawing.Point(466, 86);
            this.btnCheckPPT.Name = "btnCheckPPT";
            this.btnCheckPPT.Size = new System.Drawing.Size(75, 23);
            this.btnCheckPPT.TabIndex = 6;
            this.btnCheckPPT.Text = "Check PPT";
            this.btnCheckPPT.UseVisualStyleBackColor = true;
            this.btnCheckPPT.Click += new System.EventHandler(this.btnCheckPPT_Click);
            // 
            // XFrmContentEditSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 121);
            this.Controls.Add(this.btnCheckPPT);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtMediaFolder);
            this.Controls.Add(this.lblMediaPath);
            this.Controls.Add(this.btnSelectMediaFolder);
            this.Controls.Add(this.txtContentFolder);
            this.Controls.Add(this.lblContentPath);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "XFrmContentEditSettings";
            this.Text = "Lerninhalteditor-Einstellungen";
            ((System.ComponentModel.ISupportInitialize)(this.txtContentFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaFolder.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblContentPath;
        private DevExpress.XtraEditors.TextEdit txtContentFolder;
        private System.Windows.Forms.Button btnSelectMediaFolder;
        private DevExpress.XtraEditors.TextEdit txtMediaFolder;
        private DevExpress.XtraEditors.LabelControl lblMediaPath;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnCheckPPT;
    }
}