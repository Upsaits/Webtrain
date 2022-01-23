
namespace WebtrainWebPortal.Views
{
    partial class LoginPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginPage));
            this.helpTip1 = new Wisej.Web.HelpTip(this.components);
            this.btnRegister = new Wisej.Web.Button();
            this.buttonLogin = new Wisej.Web.Button();
            this.txtBoxPasswort = new Wisej.Web.TextBox();
            this.textBoxUserEmail = new Wisej.Web.TextBox();
            this.linkLabel1 = new Wisej.Web.LinkLabel();
            this.flowLayoutPanel1 = new Wisej.Web.FlowLayoutPanel();
            this.panel2 = new Wisej.Web.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left)));
            this.btnRegister.Font = new System.Drawing.Font("@default", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnRegister.ImageSource = "resource.wx/Wisej.Ext.MaterialDesign/square-add-button.svg";
            this.btnRegister.Location = new System.Drawing.Point(13, 16);
            this.btnRegister.Margin = new Wisej.Web.Padding(4);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("btnRegister.ResponsiveProfiles"))));
            this.btnRegister.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("btnRegister.ResponsiveProfiles1"))));
            this.btnRegister.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("btnRegister.ResponsiveProfiles2"))));
            this.btnRegister.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("btnRegister.ResponsiveProfiles3"))));
            this.btnRegister.Size = new System.Drawing.Size(132, 62);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "&Registrieren";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Right)));
            this.buttonLogin.Font = new System.Drawing.Font("@default", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.buttonLogin.ImageSource = "resource.wx/Wisej.Ext.MaterialDesign/locked-padlock.svg";
            this.buttonLogin.Location = new System.Drawing.Point(179, 16);
            this.buttonLogin.Margin = new Wisej.Web.Padding(4);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("buttonLogin.ResponsiveProfiles"))));
            this.buttonLogin.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("buttonLogin.ResponsiveProfiles1"))));
            this.buttonLogin.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("buttonLogin.ResponsiveProfiles2"))));
            this.buttonLogin.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("buttonLogin.ResponsiveProfiles3"))));
            this.buttonLogin.Size = new System.Drawing.Size(144, 62);
            this.buttonLogin.TabIndex = 8;
            this.buttonLogin.Text = "&Anmelden";
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // txtBoxPasswort
            // 
            this.txtBoxPasswort.Anchor = Wisej.Web.AnchorStyles.Bottom;
            this.txtBoxPasswort.AutoComplete = Wisej.Web.AutoComplete.Off;
            this.txtBoxPasswort.AutoSize = false;
            this.txtBoxPasswort.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtBoxPasswort.InputType.Type = Wisej.Web.TextBoxType.Password;
            this.txtBoxPasswort.Location = new System.Drawing.Point(28, 202);
            this.txtBoxPasswort.Margin = new Wisej.Web.Padding(4);
            this.txtBoxPasswort.Name = "txtBoxPasswort";
            this.txtBoxPasswort.PasswordChar = '*';
            this.txtBoxPasswort.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("txtBoxPasswort.ResponsiveProfiles"))));
            this.txtBoxPasswort.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("txtBoxPasswort.ResponsiveProfiles1"))));
            this.txtBoxPasswort.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("txtBoxPasswort.ResponsiveProfiles2"))));
            this.txtBoxPasswort.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("txtBoxPasswort.ResponsiveProfiles3"))));
            this.txtBoxPasswort.ScrollBars = Wisej.Web.ScrollBars.None;
            this.txtBoxPasswort.Size = new System.Drawing.Size(294, 61);
            this.txtBoxPasswort.TabIndex = 7;
            this.txtBoxPasswort.Watermark = "Passwort";
            // 
            // textBoxUserEmail
            // 
            this.textBoxUserEmail.Anchor = Wisej.Web.AnchorStyles.Bottom;
            this.textBoxUserEmail.AutoComplete = Wisej.Web.AutoComplete.Off;
            this.textBoxUserEmail.AutoSize = false;
            this.textBoxUserEmail.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxUserEmail.Location = new System.Drawing.Point(28, 133);
            this.textBoxUserEmail.Margin = new Wisej.Web.Padding(4);
            this.textBoxUserEmail.Name = "textBoxUserEmail";
            this.textBoxUserEmail.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("textBoxUserEmail.ResponsiveProfiles"))));
            this.textBoxUserEmail.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("textBoxUserEmail.ResponsiveProfiles1"))));
            this.textBoxUserEmail.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("textBoxUserEmail.ResponsiveProfiles2"))));
            this.textBoxUserEmail.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("textBoxUserEmail.ResponsiveProfiles3"))));
            this.textBoxUserEmail.Size = new System.Drawing.Size(294, 61);
            this.textBoxUserEmail.TabIndex = 6;
            this.textBoxUserEmail.Watermark = "Benutzer Email";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = Wisej.Web.AnchorStyles.Bottom;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new Wisej.Web.LinkArea(0, 19);
            this.linkLabel1.Location = new System.Drawing.Point(110, 270);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("linkLabel1.ResponsiveProfiles"))));
            this.linkLabel1.Size = new System.Drawing.Size(129, 15);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.Text = "Passwort vergessen?";
            this.linkLabel1.LinkClicked += new Wisej.Web.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.linkLabel1);
            this.flowLayoutPanel1.Controls.Add(this.txtBoxPasswort);
            this.flowLayoutPanel1.Controls.Add(this.textBoxUserEmail);
            this.flowLayoutPanel1.Dock = Wisej.Web.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = Wisej.Web.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("flowLayoutPanel1.ResponsiveProfiles"))));
            this.flowLayoutPanel1.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("flowLayoutPanel1.ResponsiveProfiles1"))));
            this.flowLayoutPanel1.Size = new System.Drawing.Size(354, 394);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.TabStop = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = Wisej.Web.AnchorStyles.None;
            this.panel2.Controls.Add(this.btnRegister);
            this.panel2.Controls.Add(this.buttonLogin);
            this.panel2.Location = new System.Drawing.Point(3, 291);
            this.panel2.Name = "panel2";
            this.panel2.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("panel2.ResponsiveProfiles"))));
            this.panel2.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("panel2.ResponsiveProfiles1"))));
            this.panel2.ResponsiveProfiles.Add(((Wisej.Base.ResponsiveProfile)(resources.GetObject("panel2.ResponsiveProfiles2"))));
            this.panel2.Size = new System.Drawing.Size(344, 100);
            this.panel2.TabIndex = 0;
            this.panel2.TabStop = true;
            // 
            // LoginPage
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
            this.BackgroundImageLayout = Wisej.Web.ImageLayout.Zoom;
            this.BackgroundImageSource = "Images/Background_metal_leer.jpg";
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "LoginPage";
            this.Size = new System.Drawing.Size(1035, 394);
            this.ResponsiveProfileChanged += new Wisej.Web.ResponsiveProfileChangedEventHandler(this.LoginPage_ResponsiveProfileChanged);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Wisej.Web.HelpTip helpTip1;
        private Wisej.Web.Button btnRegister;
        private Wisej.Web.Button buttonLogin;
        private Wisej.Web.TextBox txtBoxPasswort;
        private Wisej.Web.TextBox textBoxUserEmail;
        private Wisej.Web.LinkLabel linkLabel1;
        private Wisej.Web.FlowLayoutPanel flowLayoutPanel1;
        private Wisej.Web.Panel panel2;
    }
}
