using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmDongleState.
	/// </summary>
	public partial class FrmDongleState : XtraForm
	{
		private ResourceHandler rh=null;
        private TCDongleHandler dongleHandler = null;
        private AppHandler AppHandler = Program.AppHandler;
		public FrmDongleState(TCDongleHandler _dongleHandler) : this()
        {
            dongleHandler = _dongleHandler;
            ShowDongleInfo();
        }

        public FrmDongleState()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);

			InitializeComponent();

			this.Icon = rh.GetIcon("main");

			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");

			this.lblLicenceTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","License","Lizenz");
			this.lblSWVersionId.Text = AppHandler.LanguageHandler.GetText("FORMS","SoftwareVersion","Software-Version");
			this.lblaLicVer.Text = AppHandler.LanguageHandler.GetText("FORMS","LicenseId","Lizenz-Id");

            this.staLicenceTitle.Text = "unlizenzierte Version";
			this.staLicVer.Text = "nicht verfügbar";
			this.staSWVersionId.Text = AppHandler.VersionString;
		}

		private void ShowDongleInfo()
		{
			if (AppHandler.IsClient)
			{
				this.staLicenceTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","Client_Licence","Client-Lizenz");
			}
			else if (AppHandler.IsMultiLicence())
			{
				this.staLicenceTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","Multiplace_Licence","Mehrplatz-Lizenz");
			}
			else if (AppHandler.IsServer)
			{
				this.staLicenceTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","Server_Licence","Netzwerk-Lizenz");
			}
            else
				this.staLicenceTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","Singleplace_Licence","Einzelplatz-Lizenz");

			string strName="";
			if (AppCommunication.FindCustomerName(AppHandler.DongleId, out strName))
				this.staLicVer.Text = strName + " (" + AppHandler.DongleId.ToString() +')';

			if (!AppHandler.IsClient && AppHandler.IsTimeLimited())
			{
				int licTypeVal=0;

				short grpId=0,licType=0;
                dongleHandler.GetLicence(ref grpId, ref licType);

                if (licType == 4 || licType == 16)
                    dongleHandler.GetLicenceTypeValue(ref licTypeVal);

				DateTime start=DateTime.Now;
				AppHandler.GetTimeLimit(ref start);

				TimeSpan ts=new TimeSpan(licTypeVal,0,0,0);
				DateTime end = start+ts;
				TimeSpan diff= end-DateTime.Now;

                this.staTimeLimitInfo.Visible = true;
				string txt=AppHandler.LanguageHandler.GetText("FORMS","TimeLimited_Licence","Zeit limitierte Lizenz(Rest: {0} Tage)");
				this.staTimeLimitInfo.Text = String.Format(txt,diff.Days);
			}
		}

        private void btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

	}
}
