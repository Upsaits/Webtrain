
namespace WebtrainWebPortal.Forms
{
    partial class FrmDownloads
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
            this.linkLabel1 = new Wisej.Web.LinkLabel();
            this.btnNewPwdCancel = new Wisej.Web.Button();
            this.btnNewPwdOk = new Wisej.Web.Button();
            this.textBox1 = new Wisej.Web.TextBox();
            this.linkLabel2 = new Wisej.Web.LinkLabel();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new Wisej.Web.LinkArea(0, 30);
            this.linkLabel1.LinkBehavior = Wisej.Web.LinkBehavior.HoverUnderline;
            this.linkLabel1.Location = new System.Drawing.Point(45, 102);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(171, 15);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.Text = "1.Schritt: Webtrain einrichten";
            this.linkLabel1.LinkClicked += new Wisej.Web.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnNewPwdCancel
            // 
            this.btnNewPwdCancel.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.btnNewPwdCancel.DialogResult = Wisej.Web.DialogResult.Cancel;
            this.btnNewPwdCancel.Location = new System.Drawing.Point(231, 195);
            this.btnNewPwdCancel.Name = "btnNewPwdCancel";
            this.btnNewPwdCancel.Size = new System.Drawing.Size(100, 27);
            this.btnNewPwdCancel.TabIndex = 17;
            this.btnNewPwdCancel.Text = "Abbrechen";
            this.btnNewPwdCancel.Click += new System.EventHandler(this.btnNewPwdCancel_Click);
            // 
            // btnNewPwdOk
            // 
            this.btnNewPwdOk.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.btnNewPwdOk.DialogResult = Wisej.Web.DialogResult.OK;
            this.btnNewPwdOk.Location = new System.Drawing.Point(60, 195);
            this.btnNewPwdOk.Name = "btnNewPwdOk";
            this.btnNewPwdOk.Size = new System.Drawing.Size(100, 27);
            this.btnNewPwdOk.TabIndex = 16;
            this.btnNewPwdOk.Text = "Ok";
            this.btnNewPwdOk.Click += new System.EventHandler(this.btnNewPwdOk_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(337, 69);
            this.textBox1.TabIndex = 18;
            this.textBox1.Text = "Die Einrichtung des Webtrain-Clients unter Windows erfolgt in zwei Schritten. Bit" +
    "te schalten sie den Anti-Virus-Scanner vorher vorübergehend aus, es kann sonst z" +
    "u installationsproblemen  kommen";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkBehavior = Wisej.Web.LinkBehavior.HoverUnderline;
            this.linkLabel2.Location = new System.Drawing.Point(45, 145);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(276, 15);
            this.linkLabel2.TabIndex = 19;
            this.linkLabel2.Text = "2. Schritt: Persönlichen VPN Zugang einrichten";
            this.linkLabel2.LinkClicked += new Wisej.Web.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // FrmDownloads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 249);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnNewPwdCancel);
            this.Controls.Add(this.btnNewPwdOk);
            this.Controls.Add(this.linkLabel1);
            this.FormBorderStyle = Wisej.Web.FormBorderStyle.Fixed;
            this.KeepCentered = true;
            this.MinimizeBox = false;
            this.Name = "FrmDownloads";
            this.ShowInTaskbar = false;
            this.Text = "Download Pakete";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wisej.Web.LinkLabel linkLabel1;
        private Wisej.Web.Button btnNewPwdCancel;
        private Wisej.Web.Button btnNewPwdOk;
        private Wisej.Web.TextBox textBox1;
        private Wisej.Web.LinkLabel linkLabel2;
    }
}