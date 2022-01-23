
namespace WebtrainWebPortal.Forms
{
    partial class FrmUploadViewer
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

        #region Wisej Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUploadViewer));
            this.upload1 = new Wisej.Web.Upload();
            this.pdfViewer1 = new Wisej.Web.PdfViewer();
            this.panel1 = new Wisej.Web.Panel();
            this.btnCancel = new Wisej.Web.Button();
            this.btnOk = new Wisej.Web.Button();
            this.panel2 = new Wisej.Web.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // upload1
            // 
            this.upload1.ButtonPosition = System.Drawing.ContentAlignment.MiddleCenter;
            this.upload1.Dock = Wisej.Web.DockStyle.Bottom;
            this.upload1.Location = new System.Drawing.Point(0, 323);
            this.upload1.Name = "upload1";
            this.upload1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("upload1.ResponsiveProfiles"))));
            this.upload1.Size = new System.Drawing.Size(369, 22);
            this.upload1.TabIndex = 0;
            this.upload1.Text = "Datei auswählen";
            this.upload1.Uploaded += new Wisej.Web.UploadedEventHandler(this.upload1_Uploaded);
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Dock = Wisej.Web.DockStyle.Fill;
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("pdfViewer1.ResponsiveProfiles"))));
            this.pdfViewer1.Size = new System.Drawing.Size(369, 323);
            this.pdfViewer1.TabIndex = 1;
            this.pdfViewer1.ViewerType = Wisej.Web.PdfViewerType.Mozilla;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = Wisej.Web.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 345);
            this.panel1.Name = "panel1";
            this.panel1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("panel1.ResponsiveProfiles"))));
            this.panel1.Size = new System.Drawing.Size(369, 46);
            this.panel1.TabIndex = 2;
            this.panel1.TabStop = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = Wisej.Web.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(245, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("btnCancel.ResponsiveProfiles"))));
            this.btnCancel.Size = new System.Drawing.Size(102, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.btnOk.AutoSize = true;
            this.btnOk.DialogResult = Wisej.Web.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(19, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 27);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pdfViewer1);
            this.panel2.Controls.Add(this.upload1);
            this.panel2.Dock = Wisej.Web.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("panel2.ResponsiveProfiles"))));
            this.panel2.Size = new System.Drawing.Size(369, 345);
            this.panel2.TabIndex = 3;
            this.panel2.TabStop = true;
            // 
            // FrmUploadViewer
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(369, 391);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmUploadViewer";
            this.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("$this.ResponsiveProfiles"))));
            this.ShowInTaskbar = false;
            this.StartPosition = Wisej.Web.FormStartPosition.CenterScreen;
            this.Text = "Datei hochladen";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Wisej.Web.Upload upload1;
        private Wisej.Web.PdfViewer pdfViewer1;
        private Wisej.Web.Panel panel1;
        private Wisej.Web.Button btnCancel;
        private Wisej.Web.Button btnOk;
        private Wisej.Web.Panel panel2;
    }
}