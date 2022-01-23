
namespace WebtrainWebPortal.Forms
{
    partial class FrmRegister
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
            this.components = new System.ComponentModel.Container();
            this.btnRegCancel = new Wisej.Web.Button();
            this.btnRegRegister = new Wisej.Web.Button();
            this.txtRegConfPassword = new Wisej.Web.TextBox();
            this.label10 = new Wisej.Web.Label();
            this.txtRegPassword = new Wisej.Web.TextBox();
            this.label9 = new Wisej.Web.Label();
            this.txtEmail = new Wisej.Web.TextBox();
            this.label8 = new Wisej.Web.Label();
            this.txtLastname = new Wisej.Web.TextBox();
            this.label7 = new Wisej.Web.Label();
            this.txtMiddlename = new Wisej.Web.TextBox();
            this.label6 = new Wisej.Web.Label();
            this.txtFirstname = new Wisej.Web.TextBox();
            this.label5 = new Wisej.Web.Label();
            this.errorProvider1 = new Wisej.Web.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRegCancel
            // 
            this.btnRegCancel.DialogResult = Wisej.Web.DialogResult.Cancel;
            this.btnRegCancel.Location = new System.Drawing.Point(212, 243);
            this.btnRegCancel.Name = "btnRegCancel";
            this.btnRegCancel.Size = new System.Drawing.Size(100, 27);
            this.btnRegCancel.TabIndex = 27;
            this.btnRegCancel.Text = "Abbrechen";
            this.btnRegCancel.Click += new System.EventHandler(this.btnRegCancel_Click);
            // 
            // btnRegRegister
            // 
            this.btnRegRegister.DialogResult = Wisej.Web.DialogResult.OK;
            this.btnRegRegister.Location = new System.Drawing.Point(44, 243);
            this.btnRegRegister.Name = "btnRegRegister";
            this.btnRegRegister.Size = new System.Drawing.Size(100, 27);
            this.btnRegRegister.TabIndex = 26;
            this.btnRegRegister.Text = "Registrieren";
            this.btnRegRegister.Click += new System.EventHandler(this.btnRegRegister_Click);
            // 
            // txtRegConfPassword
            // 
            this.txtRegConfPassword.InputType.Type = Wisej.Web.TextBoxType.Password;
            this.txtRegConfPassword.Location = new System.Drawing.Point(144, 185);
            this.txtRegConfPassword.Name = "txtRegConfPassword";
            this.txtRegConfPassword.PasswordChar = '*';
            this.txtRegConfPassword.Size = new System.Drawing.Size(198, 22);
            this.txtRegConfPassword.TabIndex = 25;
            this.txtRegConfPassword.Watermark = "P@sswort001";
            this.txtRegConfPassword.Leave += new System.EventHandler(this.txtRegConfPassword_Leave);
            this.txtRegConfPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtRegConfPassword_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 192);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 15);
            this.label10.TabIndex = 24;
            this.label10.Text = "Passwort bestätigen:";
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.InputType.Type = Wisej.Web.TextBoxType.Password;
            this.txtRegPassword.Location = new System.Drawing.Point(144, 157);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.PasswordChar = '*';
            this.txtRegPassword.Size = new System.Drawing.Size(198, 22);
            this.txtRegPassword.TabIndex = 23;
            this.txtRegPassword.Watermark = "P@sswort001";
            this.txtRegPassword.Leave += new System.EventHandler(this.txtRegPassword_Leave);
            this.txtRegPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtRegPassword_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "Passwort:";
            // 
            // txtEmail
            // 
            this.txtEmail.InputType.Type = Wisej.Web.TextBoxType.Email;
            this.txtEmail.Location = new System.Drawing.Point(144, 129);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(198, 22);
            this.txtEmail.TabIndex = 21;
            this.txtEmail.Watermark = "Max.Mustermann@muster.at";
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "E-Mail:";
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(144, 98);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(198, 22);
            this.txtLastname.TabIndex = 19;
            this.txtLastname.Watermark = "Mustermann";
            this.txtLastname.Leave += new System.EventHandler(this.txtLastname_Leave);
            this.txtLastname.Validating += new System.ComponentModel.CancelEventHandler(this.txtLastname_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Nachname:";
            // 
            // txtMiddlename
            // 
            this.txtMiddlename.Location = new System.Drawing.Point(144, 70);
            this.txtMiddlename.Name = "txtMiddlename";
            this.txtMiddlename.Size = new System.Drawing.Size(198, 22);
            this.txtMiddlename.TabIndex = 17;
            this.txtMiddlename.Watermark = "Muster";
            this.txtMiddlename.Leave += new System.EventHandler(this.txtMiddlename_Leave);
            this.txtMiddlename.Validating += new System.ComponentModel.CancelEventHandler(this.txtMiddlename_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Mittelname:";
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(144, 42);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(198, 22);
            this.txtFirstname.TabIndex = 15;
            this.txtFirstname.Watermark = "Max";
            this.txtFirstname.Leave += new System.EventHandler(this.txtFirstname_Leave);
            this.txtFirstname.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirstname_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Vorname:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmRegister
            // 
            this.AcceptButton = this.btnRegRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.CancelButton = this.btnRegCancel;
            this.ClientSize = new System.Drawing.Size(363, 300);
            this.Controls.Add(this.btnRegCancel);
            this.Controls.Add(this.btnRegRegister);
            this.Controls.Add(this.txtRegConfPassword);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtRegPassword);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMiddlename);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFirstname);
            this.Controls.Add(this.label5);
            this.KeepCentered = true;
            this.Name = "FrmRegister";
            this.ShowInTaskbar = false;
            this.StartPosition = Wisej.Web.FormStartPosition.CenterScreen;
            this.Text = "Registrierung";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wisej.Web.Button btnRegCancel;
        private Wisej.Web.Button btnRegRegister;
        private Wisej.Web.TextBox txtRegConfPassword;
        private Wisej.Web.Label label10;
        private Wisej.Web.TextBox txtRegPassword;
        private Wisej.Web.Label label9;
        private Wisej.Web.TextBox txtEmail;
        private Wisej.Web.Label label8;
        private Wisej.Web.TextBox txtLastname;
        private Wisej.Web.Label label7;
        private Wisej.Web.TextBox txtMiddlename;
        private Wisej.Web.Label label6;
        private Wisej.Web.TextBox txtFirstname;
        private Wisej.Web.Label label5;
        private Wisej.Web.ErrorProvider errorProvider1;
    }
}