using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmInputIPAdress.
	/// </summary>
	public partial class FrmInputIPAdress : XtraForm
	{
		private ResourceHandler rh=null;
        private string m_strNewServerName = "";
        private string m_strAutoServerName = "";
        private IPAddress m_serverIPAdress=null;
        private AppHandler AppHandler = Program.AppHandler;
        public string NewServerName {  get { return m_strNewServerName; } }

		public FrmInputIPAdress()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			InitializeComponent();
			this.Icon = rh.GetIcon("main");

			this.label1.Text	= AppHandler.LanguageHandler.GetText("FORMS","Actual_server_adress","Aktuelle Server Adresse")+':';
			this.label2.Text	= AppHandler.LanguageHandler.GetText("FORMS","New_server_adress","Neue Server Adresse")+':';
			this.btnOk.Text		= AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.Text			= AppHandler.LanguageHandler.GetText("FORMS","Choose_server","Server festlegen");

            m_strAutoServerName = AppCommunication.AutoServerName;
            rbnAutoServer.Checked = (m_strAutoServerName.Length > 0);
            rbnManual.Checked = !rbnAutoServer.Checked;
        }



        private void SetState(bool bIsAuto)
        {
            if (bIsAuto)
            {
                if (m_strAutoServerName.Length>0)
                {
                    txtAutoServer.Text = m_strAutoServerName;
                    btnCheck.Enabled = true;
                    btnCheck.ImageIndex = AppHandler.CtsClientManager.IsRunning() ? 1 : 0;
                }
                else
                {
                    txtAutoServer.Text= "unknown!";
                    btnCheck.Enabled = false;
                    btnCheck.ImageIndex = -1;
                }
                txtNewServer.Enabled = false;
            }
            else
            {
                txtOldServer.Text = AppHandler.ServerName;
                txtNewServer.Enabled = true;
                btnCheck.ImageIndex = -1;
                btnCheck.Enabled = txtNewServer.Text.Length > 0 || txtOldServer.Text.Length > 0;
            }
        }

        public static bool HostName2IP(string strHostname, out IPAddress ipAdr)
        {
            ipAdr = null;

            // resolve the hostname into an iphost entry using the dns class
            try
            {
                IPHostEntry iphost = System.Net.Dns.Resolve(strHostname);
                // get all of the possible IP addresses for this hostname
                IPAddress[] addresses = iphost.AddressList;

                // get each ip address
                foreach (IPAddress adr in addresses)
                {
                    if (IPAddress.TryParse(adr.ToString(),out ipAdr))
                        return true;
                }

            }
            catch (System.ArgumentNullException)
            {
                
            }
            catch (System.ArgumentOutOfRangeException)
            {
               
            }
            catch (System.Net.Sockets.SocketException)
            {

            }
            return false;
        }



      private static bool ParseIPAdress(string ipAddress, out IPAddress ipAdr)
      {
        try
        {
            if (!IPAddress.TryParse(ipAddress, out ipAdr))
            {
                if (HostName2IP(ipAddress,out ipAdr))
                    return true;
            }

            return true;
        }
        catch (ArgumentNullException /*ex*/)
        {
            //MessageBox.Show(ex.Message);
        }
        catch (FormatException /*ex*/)
        {
            //MessageBox.Show(ex.Message);
        }

        ipAdr = null;
        return false;
       }


		private void btnOk_Click(object sender, System.EventArgs e)
		{
            if (m_serverIPAdress!=null)
			    m_strNewServerName = m_serverIPAdress.ToString();

            if (!AppHandler.CtsClientManager.IsRunning())
            {
                btnCheck_Click(this, new EventArgs());
                DialogResult = DialogResult.None;
            }
            else
                DialogResult = DialogResult.OK;
		}

        private void btnCheck_Click(object sender, EventArgs e)
        {
            circularProgress1.Visible = true;
            Cursor.Hide();
            ToastNotification.Show(this, "Bitte warten...(IP-Adresse wird gecheckt)", null, 10000, eToastGlowColor.Blue, eToastPosition.MiddleCenter);

            btnCheck.ImageIndex = -1;
            btnOk.Enabled = false;
            btnCancel.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker2.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void rbnAutoServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnAutoServer.Checked)
                SetState(true);
        }

        private void rbnManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnManual.Checked)
                SetState(false);
        }

        private void txtNewServer_TextChanged(object sender, EventArgs e)
        {
           //btnCheck.Enabled = ParseIPAdress(txtNewServer.Text,out m_serverIPAdress);
           btnCheck.Enabled = txtNewServer.Text.Length > 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (btnCheck.Enabled)
            {

            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while(true)
            {
                System.Threading.Thread.Sleep(50);
                backgroundWorker1.ReportProgress(i++);
                if (i == 100)
                    i = 0;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            circularProgress1.Value = e.ProgressPercentage;
            if (circularProgress1.Value >= circularProgress1.Maximum)
                circularProgress1.Value = 0;
        }

        private void FrmInputIPAdress_Load(object sender, EventArgs e)
        {
        }

        private void FrmInputIPAdress_FormClosed(object sender, FormClosedEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            string strServerName="";

            if (rbnAutoServer.Checked)
                strServerName = txtAutoServer.Text;
            else
            {
                ParseIPAdress(txtNewServer.Text, out m_serverIPAdress);
                if (m_serverIPAdress != null)
                {
                    strServerName = m_serverIPAdress.ToString();
                    AppHandler.MainForm.BeginInvoke((MethodInvoker)delegate
                    {
                        txtOldServer.Text = m_serverIPAdress.ToString();
                    });
                }
            }
            
            bool bSendOk = false;
            if (strServerName.Length>0)
                bSendOk = AppHandler.CtsClientManager.Create(strServerName, AppHandler.PortNr);
            btnCheck.ImageIndex = bSendOk ? 1 : 0;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            circularProgress1.Visible = false;
            btnOk.Enabled = true;
            btnCancel.Enabled = true;
            Cursor.Show();
            ToastNotification.Close(this);
            backgroundWorker1.CancelAsync();
        }
    }
}
