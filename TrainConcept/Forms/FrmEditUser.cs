using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmEditUser.
	/// </summary>
	public partial class FrmEditUser : XtraForm
	{
        public enum EditType { Name, FullName, Type, AllExceptName, Administrator };
        
        private string userName = null;
		private ResourceHandler rh=null;
        private EditType eType = EditType.AllExceptName;
        private AppHandler AppHandler = Program.AppHandler;
        public string UserName
		{
			get
			{
				return userName;
			}
		}

		public FrmEditUser()
		{
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			InitializeComponent();
			this.Icon = rh.GetIcon("main");

			InitializeLanguage();

			txtPassword.Enabled=true;
			txtUserName.Select();

			rbnStudent.Checked=true;
		}

		public FrmEditUser(string _userName,EditType _eType)
		{
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			InitializeComponent();
			this.Icon = rh.GetIcon("main");

			InitializeLanguage();

			userName=_userName;
            eType = _eType;

			string password=null,fullName=null;
			bool isAdmin=false,isTeacher=false;
            int iImgId = 0;

			AppHandler.UserManager.GetUserInfo(userName,ref password,ref fullName,ref iImgId);
			AppHandler.UserManager.GetUserRights(userName,ref isAdmin,ref isTeacher);
			
			txtUserName.Text = userName;
			txtFullName.Text = fullName;
			txtPassword.Text = password;
            if (isTeacher)
                rbnTeacher.Checked = true;
            else
                rbnStudent.Checked = true;
            
            chkAdmin.Checked = isAdmin;

            if (eType == EditType.Name)
            {
                txtFullName.Enabled = false;
                rbnTeacher.Enabled = false;
                rbnStudent.Enabled = false;
                chkAdmin.Enabled = false;
                btnChgPwd.Enabled = false;
            }
            else if (eType == EditType.FullName)
            {
                txtUserName.Enabled = false;
                rbnTeacher.Enabled = false;
                rbnStudent.Enabled = false;
                chkAdmin.Enabled = false;
                btnChgPwd.Enabled = false;
            }
            else if (eType == EditType.Type)
            {
                txtFullName.Enabled = false;
                txtUserName.Enabled = false;
                btnChgPwd.Enabled = false;
            }
            else if (eType == EditType.Administrator)
            {
                rbnTeacher.Enabled = false;
                rbnStudent.Enabled = false;
                chkAdmin.Enabled = false;
                txtUserName.Enabled = false;
            }
            else
            {
                txtUserName.Enabled = false;
            }
		}

		private void InitializeLanguage()
		{
			this.label1.Text = AppHandler.LanguageHandler.GetText("FORMS","Username","Benutzername")+':';
			this.label2.Text = AppHandler.LanguageHandler.GetText("FORMS","Name","Name")+':';
			this.label3.Text = AppHandler.LanguageHandler.GetText("FORMS","Password","Passwort")+':';
			this.groupBox1.Text = AppHandler.LanguageHandler.GetText("FORMS","Usertype","Benutzertyp")+':';
			this.rbnStudent.Text = AppHandler.LanguageHandler.GetText("SYSTEM","Student","Schüler");
			this.rbnTeacher.Text = AppHandler.LanguageHandler.GetText("SYSTEM","Teacher","Lehrer");
			this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.btnChgPwd.Text = AppHandler.LanguageHandler.GetText("FORMS","Change_password","Paßwort ändern");
			this.Text = AppHandler.LanguageHandler.GetText("FORMS","Edit_userentry","Benutzereintrag bearbeiten");
		}

		private bool IsValidName(string sName)
		{
			for(int i=0;i<sName.Length;++i)
				if (!Char.IsLetterOrDigit(sName[i]) && sName[i]!='_')
					return false;
			return true;
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if (txtUserName.Text.Length==0)
			{
				string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Input_username","Benutzernamen angeben!");
				string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
				MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
				DialogResult = DialogResult.None;
				return;
			}

			if (!IsValidName(txtUserName.Text))
			{
				string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Invalid_username","Benutzernamen ungültig!");
				string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
				MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
				DialogResult = DialogResult.None;
				return;
			}

			if (userName==null)
			{
				string fullName=null,password=null;
                int iImgId = 0;
				if (AppHandler.UserManager.GetUserInfo(txtUserName.Text,ref password,ref fullName,ref iImgId)>=0)
				{
					string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Username_already_exists","Benutzername bereits vorhanden!");
					string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
					MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Error);
					DialogResult = DialogResult.None;
					return;
				}

                if (!AppHandler.UserManager.CreateUser(txtFullName.Text, txtUserName.Text, txtPassword.Text))
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Cannot_create_userentry", "Benutzererstellung nicht möglich!");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                    return;
                }
                userName = txtUserName.Text;
                AppHandler.UserManager.Save();
			}
			else
			{
                if (txtUserName.Text != userName)
                {
                    this.txtPassword.Text = "";
                    if (!AppHandler.UserManager.CreateUser(txtFullName.Text, txtUserName.Text, ""))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Cannot_create_userentry", "Benutzererstellung nicht möglich!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }

                    AppHandler.UserManager.DeleteUser(userName);
                    AppHandler.UserManager.Save();
                }

                if (txtFullName.Enabled)
				{
                    if (!AppHandler.UserManager.SetUserInfo(txtUserName.Text, txtFullName.Text, txtPassword.Text, 0))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Cannot_change_userentry", "Änderung nicht möglich!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    AppHandler.UserManager.Save();
                }

				if (rbnTeacher.Enabled)
				{
                    if (!AppHandler.UserManager.SetUserRights(txtUserName.Text, chkAdmin.Checked, rbnTeacher.Checked))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Cannot_change_userentry", "Änderung nicht möglich!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    AppHandler.UserManager.Save();
                }

                if (chkAdmin.Enabled)
                {
                    if (!AppHandler.UserManager.SetUserRights(txtUserName.Text, chkAdmin.Checked, rbnTeacher.Checked))
                    {
                        string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Cannot_change_userentry", "Änderung nicht möglich!");
                        string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                        MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    AppHandler.UserManager.Save();

                }
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
		}

		private void btnChgPwd_Click(object sender, System.EventArgs e)
		{
			string fullName=null,password=null;
            int iImgId = 0;
			AppHandler.UserManager.GetUserInfo(txtUserName.Text,ref password,ref fullName,ref iImgId);
			
			FrmChangePwd dlg = new FrmChangePwd(password);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
                if (AppHelpers.TryToChangeUserPassword(txtUserName.Text, dlg.Password))
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Password_changed", "Paßwort wurde geändert!");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Password_cannot_be_changed", "Password kann im Moment nicht geändert werden!");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                this.txtPassword.Text = dlg.Password;
			}
		}
	}
}


