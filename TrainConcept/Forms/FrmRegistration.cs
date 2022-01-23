using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for FrmRegistration.
	/// </summary>
    public partial class FrmRegistration : XtraForm
	{
		private string m_userName="";
		private string m_password;
		private bool m_hasUserList;
		private ResourceHandler rh=null;
        private AppHandler AppHandler = Program.AppHandler;

		public string UserName
		{
			get
			{
				return m_userName;
			}
		}

		public string Password
		{
			get
			{
				return m_password;
			}
		}

		public FrmRegistration(string strUserName="",string strPassword="")
		{
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			InitializeComponent();

			this.Icon = rh.GetIcon("main");
			this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.labelPassword.Text = AppHandler.LanguageHandler.GetText("FORMS","Password","Kennwort")+':';
			this.labelUsername.Text = AppHandler.LanguageHandler.GetText("FORMS","Username","Benutzername")+':';
			this.btnChangePwd.Text = AppHandler.LanguageHandler.GetText("FORMS","Change_password","Kennwort ändern");
			this.btnProperties.Text = AppHandler.LanguageHandler.GetText("FORMS","Adjusts","Einstellungen");
			this.Text = AppHandler.LanguageHandler.GetText("FORMS","Login_title","Webtrain - Anmeldung");

			m_hasUserList=AppHandler.UserManager.IsOpen();
            txtUserName.Text = strUserName;
            txtPassword.Text = strPassword;
            if (txtUserName.Text.Length > 0)
                chkSaveInfo.Checked = true;
			Initialize();
		}
				
		private void Initialize()
		{
			if (m_hasUserList)
			{
				string[] aUsers;
				AppHandler.UserManager.GetUserNames(out aUsers);
                if (!AppHandler.IsServer)
                    aUsers=aUsers.RemoveFromArray("admin");

				if (aUsers.Length>0)
                    cboUserNames.Items.AddRange(aUsers);

				cboUserNames.Visible = true;
				txtUserName.Visible = false;
				btnProperties.Enabled = AppHandler.IsClient;
				if (cboUserNames.Items.Count>0)
				    cboUserNames.SelectedIndex = 0;
				txtPassword.Select();
			}
			else
			{
				cboUserNames.Visible = false;
				txtUserName.Select();
			}
		}

        
        private void btnOk_Click(object sender, System.EventArgs e)
		{
			m_password=txtPassword.Text;
			
			if (m_hasUserList)
			{
				m_userName=cboUserNames.Text;
					
				string password="";
				string fullName="";
                int iImgId = 0;
                AppHandler.UserManager.GetUserInfo(m_userName, ref password, ref fullName, ref iImgId);

				if (String.Compare(m_password,password,true)!=0)
				{
					string txt=AppHandler.LanguageHandler.GetText("ERROR","Invalid_password");
					string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
					MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					DialogResult = DialogResult.None;
				}
			}
			else
				m_userName=txtUserName.Text;

            if (chkSaveInfo.Checked)
            {
                AppHandler.LoginName = m_userName;
                AppHandler.LoginPassword = m_password;
            }
            else
            {
                AppHandler.LoginName = "";
                AppHandler.LoginPassword = "";
            }

		}

		private void btnChangePwd_Click(object sender, System.EventArgs e)
		{
			if (m_hasUserList)
			{
				string userName=cboUserNames.Text;
                string oldPassword = "";
                string fullName = "";
                int iImgId = 0;
                AppHandler.UserManager.GetUserInfo(userName, ref oldPassword, ref fullName, ref iImgId);

                FrmChangePwd dlg = new FrmChangePwd(oldPassword);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
                    if (AppHelpers.TryToChangeUserPassword(userName, dlg.Password))
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
				}
			}
			else
			{
			}
		}

		private void btnProperties_Click(object sender, System.EventArgs e)
		{
            FrmInputIPAdress dlg = new FrmInputIPAdress();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.NewServerName.Length > 0 && AppHandler.CtsClientManager.IsRunning())
                {
                    AppHandler.ServerName = dlg.NewServerName;
                    this.DialogResult = DialogResult.Retry;
                    Close();
                }
            }
		}
	}
}
