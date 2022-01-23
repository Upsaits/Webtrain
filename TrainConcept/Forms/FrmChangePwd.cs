using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.TrainConcept
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmChangePwd.
	/// </summary>
    public partial class FrmChangePwd : XtraForm
	{
		private string password;
		private string oldPassword;
		private ResourceHandler rh=null;
        private AppHandler AppHandler = Program.AppHandler;
		public string Password
		{
			get
			{
				return password;
			}
		}

		public FrmChangePwd(string _oldPassword)
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);

			InitializeComponent();

			this.Icon = rh.GetIcon("main");

			this.label3.Text = AppHandler.LanguageHandler.GetText("FORMS","Password","Kennwort")+':';
			this.label1.Text = AppHandler.LanguageHandler.GetText("FORMS","New_password","Neues Kennwort")+":";
			this.label2.Text = AppHandler.LanguageHandler.GetText("FORMS","Repeat_new_password","Neues Kennwort wiederholen")+":";
			this.btnOk.Text  = AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.Text = AppHandler.LanguageHandler.GetText("FORMS","Change_password","Kennwort ändern");

			oldPassword=_oldPassword;
			if (oldPassword.Length==0)
			{
				txtNewPassword.Enabled=true;
				txtNewPassword2.Enabled=true;
				txtNewPassword.Select();
			}
			else
				txtPassword.Select();
		}

		private void txtPassword_TextChanged(object sender, System.EventArgs e)
		{
			if (String.Compare(txtPassword.Text,oldPassword,true)==0)
			{
				this.txtNewPassword.Enabled=true;
				this.txtNewPassword2.Enabled=true;
			}
			else
			{
				this.txtNewPassword.Enabled=false;
				this.txtNewPassword2.Enabled=false;
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if (!txtNewPassword.Enabled)
			{
				string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Enter_actual_password","Aktuelles Passwort angeben!");
				string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
				MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				DialogResult = DialogResult.None;
				return;
			}

			if (String.Compare(txtNewPassword.Text,txtNewPassword2.Text,true)!=0)
			{
				string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Repeated_password_wrong","Wiederholtes Passwort falsch!");
				string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","WebTrain");
				MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				DialogResult = DialogResult.None;
				return;
			}

			password = txtNewPassword.Text;
			DialogResult = DialogResult.OK;
		}
	}
}
