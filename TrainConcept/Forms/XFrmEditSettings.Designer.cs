namespace SoftObject.TrainConcept.Forms
{
    partial class XFrmEditSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XFrmEditSettings));
            this.btnCheckPPT = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtMediaFolder = new DevExpress.XtraEditors.TextEdit();
            this.lblMediaPath = new DevExpress.XtraEditors.LabelControl();
            this.btnSelectMediaFolder = new System.Windows.Forms.Button();
            this.txtContentFolder = new DevExpress.XtraEditors.TextEdit();
            this.lblContentPath = new DevExpress.XtraEditors.LabelControl();
            this.btnSelectContent = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContentFolder.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheckPPT
            // 
            this.btnCheckPPT.Location = new System.Drawing.Point(12, 199);
            this.btnCheckPPT.Name = "btnCheckPPT";
            this.btnCheckPPT.Size = new System.Drawing.Size(75, 23);
            this.btnCheckPPT.TabIndex = 13;
            this.btnCheckPPT.Text = "Check PPT";
            this.btnCheckPPT.UseVisualStyleBackColor = true;
            this.btnCheckPPT.Click += new System.EventHandler(this.btnCheckPPT_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(233, 233);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtMediaFolder
            // 
            this.txtMediaFolder.EditValue = "Test";
            this.txtMediaFolder.Location = new System.Drawing.Point(111, 61);
            this.txtMediaFolder.Name = "txtMediaFolder";
            this.txtMediaFolder.Properties.ReadOnly = true;
            this.txtMediaFolder.Size = new System.Drawing.Size(402, 20);
            this.txtMediaFolder.TabIndex = 11;
            // 
            // lblMediaPath
            // 
            this.lblMediaPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblMediaPath.Location = new System.Drawing.Point(14, 65);
            this.lblMediaPath.Name = "lblMediaPath";
            this.lblMediaPath.Size = new System.Drawing.Size(91, 13);
            this.lblMediaPath.TabIndex = 10;
            this.lblMediaPath.Text = "Medienverzeichnis:";
            // 
            // btnSelectMediaFolder
            // 
            this.btnSelectMediaFolder.Location = new System.Drawing.Point(520, 62);
            this.btnSelectMediaFolder.Name = "btnSelectMediaFolder";
            this.btnSelectMediaFolder.Size = new System.Drawing.Size(47, 20);
            this.btnSelectMediaFolder.TabIndex = 9;
            this.btnSelectMediaFolder.Text = "...";
            this.btnSelectMediaFolder.UseVisualStyleBackColor = true;
            this.btnSelectMediaFolder.Click += new System.EventHandler(this.btnSelectMediaFolder_Click);
            // 
            // txtContentFolder
            // 
            this.txtContentFolder.EditValue = "Test";
            this.txtContentFolder.Location = new System.Drawing.Point(111, 35);
            this.txtContentFolder.Name = "txtContentFolder";
            this.txtContentFolder.Properties.ReadOnly = true;
            this.txtContentFolder.Size = new System.Drawing.Size(402, 20);
            this.txtContentFolder.TabIndex = 8;
            // 
            // lblContentPath
            // 
            this.lblContentPath.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblContentPath.Location = new System.Drawing.Point(14, 39);
            this.lblContentPath.Name = "lblContentPath";
            this.lblContentPath.Size = new System.Drawing.Size(90, 13);
            this.lblContentPath.TabIndex = 7;
            this.lblContentPath.Text = "Inhaltsverzeichnis:";
            // 
            // btnSelectContent
            // 
            this.btnSelectContent.Location = new System.Drawing.Point(520, 34);
            this.btnSelectContent.Name = "btnSelectContent";
            this.btnSelectContent.Size = new System.Drawing.Size(47, 20);
            this.btnSelectContent.TabIndex = 14;
            this.btnSelectContent.Text = "...";
            this.btnSelectContent.UseVisualStyleBackColor = true;
            this.btnSelectContent.Click += new System.EventHandler(this.btnSelectContent_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Check RTF-Diff";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // XFrmEditSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSelectContent);
            this.Controls.Add(this.btnCheckPPT);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtMediaFolder);
            this.Controls.Add(this.lblMediaPath);
            this.Controls.Add(this.btnSelectMediaFolder);
            this.Controls.Add(this.txtContentFolder);
            this.Controls.Add(this.lblContentPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XFrmEditSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen";
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContentFolder.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckPPT;
        private System.Windows.Forms.Button btnOk;
        private DevExpress.XtraEditors.TextEdit txtMediaFolder;
        private DevExpress.XtraEditors.LabelControl lblMediaPath;
        private System.Windows.Forms.Button btnSelectMediaFolder;
        private DevExpress.XtraEditors.TextEdit txtContentFolder;
        private DevExpress.XtraEditors.LabelControl lblContentPath;
        private System.Windows.Forms.Button btnSelectContent;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
    }
}