
namespace WebtrainWebPortal.Forms
{
    partial class FrmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangePassword));
            this.txtNewPwdEmail = new Wisej.Web.TextBox();
            this.label1 = new Wisej.Web.Label();
            this.btnNewPwdCancel = new Wisej.Web.Button();
            this.btnNewPwdOk = new Wisej.Web.Button();
            this.textBox3 = new Wisej.Web.TextBox();
            this.txtConfirmNewPassword = new Wisej.Web.TextBox();
            this.txtNewPassword = new Wisej.Web.TextBox();
            this.label2 = new Wisej.Web.Label();
            this.SuspendLayout();
            // 
            // txtNewPwdEmail
            // 
            this.txtNewPwdEmail.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.txtNewPwdEmail.InputType.Type = Wisej.Web.TextBoxType.Email;
            this.txtNewPwdEmail.Location = new System.Drawing.Point(158, 35);
            this.txtNewPwdEmail.Name = "txtNewPwdEmail";
            this.txtNewPwdEmail.Size = new System.Drawing.Size(198, 22);
            this.txtNewPwdEmail.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "E-Mail:";
            // 
            // btnNewPwdCancel
            // 
            this.btnNewPwdCancel.Anchor = Wisej.Web.AnchorStyles.None;
            this.btnNewPwdCancel.DialogResult = Wisej.Web.DialogResult.Cancel;
            this.btnNewPwdCancel.Location = new System.Drawing.Point(202, 192);
            this.btnNewPwdCancel.Name = "btnNewPwdCancel";
            this.btnNewPwdCancel.Size = new System.Drawing.Size(100, 27);
            this.btnNewPwdCancel.TabIndex = 15;
            this.btnNewPwdCancel.Text = "Abbrechen";
            this.btnNewPwdCancel.Click += new System.EventHandler(this.btnNewPwdCancel_Click);
            // 
            // btnNewPwdOk
            // 
            this.btnNewPwdOk.Anchor = Wisej.Web.AnchorStyles.None;
            this.btnNewPwdOk.DialogResult = Wisej.Web.DialogResult.OK;
            this.btnNewPwdOk.Location = new System.Drawing.Point(67, 192);
            this.btnNewPwdOk.Name = "btnNewPwdOk";
            this.btnNewPwdOk.Size = new System.Drawing.Size(100, 27);
            this.btnNewPwdOk.TabIndex = 14;
            this.btnNewPwdOk.Text = "Ok";
            this.btnNewPwdOk.Click += new System.EventHandler(this.btnNewPwdOk_Click);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.textBox3.BorderStyle = Wisej.Web.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(13, 121);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ScrollBars = Wisej.Web.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(122, 46);
            this.textBox3.TabIndex = 12;
            this.textBox3.Text = "Neues Passwort  bestätigen:";
            // 
            // txtConfirmNewPassword
            // 
            this.txtConfirmNewPassword.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.txtConfirmNewPassword.InputType.Type = Wisej.Web.TextBoxType.Password;
            this.txtConfirmNewPassword.Location = new System.Drawing.Point(158, 128);
            this.txtConfirmNewPassword.Name = "txtConfirmNewPassword";
            this.txtConfirmNewPassword.PasswordChar = '*';
            this.txtConfirmNewPassword.Size = new System.Drawing.Size(194, 22);
            this.txtConfirmNewPassword.TabIndex = 13;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.txtNewPassword.InputType.Type = Wisej.Web.TextBoxType.Password;
            this.txtNewPassword.Location = new System.Drawing.Point(158, 75);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(194, 22);
            this.txtNewPassword.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Neues Passwort:";
            // 
            // FrmChangePassword
            // 
            this.AcceptButton = this.btnNewPwdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.CancelButton = this.btnNewPwdCancel;
            this.ClientSize = new System.Drawing.Size(370, 233);
            this.Controls.Add(this.txtNewPwdEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNewPwdCancel);
            this.Controls.Add(this.btnNewPwdOk);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.txtConfirmNewPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = Wisej.Web.FormBorderStyle.Fixed;
            this.Icon = ((System.Drawing.Image)(resources.GetObject("$this.Icon")));
            this.KeepCentered = true;
            this.MinimizeBox = false;
            this.Name = "FrmChangePassword";
            this.ShowInTaskbar = false;
            this.StartPosition = Wisej.Web.FormStartPosition.CenterScreen;
            this.Text = "Passwort ändern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wisej.Web.TextBox txtNewPwdEmail;
        private Wisej.Web.Label label1;
        private Wisej.Web.Button btnNewPwdCancel;
        private Wisej.Web.Button btnNewPwdOk;
        private Wisej.Web.TextBox textBox3;
        private Wisej.Web.TextBox txtConfirmNewPassword;
        private Wisej.Web.TextBox txtNewPassword;
        private Wisej.Web.Label label2;
    }
}