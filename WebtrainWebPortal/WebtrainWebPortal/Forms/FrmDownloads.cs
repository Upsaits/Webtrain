using System;
using System.Drawing;
using System.IO;
using WebtrainWebPortal.Views;
using Wisej.Web;

namespace WebtrainWebPortal.Forms
{
    public partial class FrmDownloads : Form
    {
        public WebtrainWebPortal.WorkflowMediator WorkflowMediator
        {
            get => Application.Session.workflowMediator;
        }

        public FrmDownloads()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filePath = Application.MapPath($"~/Remote/TCInstallCD_Remote.sfx.exe");
            if (File.Exists(filePath))
                DownloadFile(filePath);
            else
                AlertBox.Show("Datei ist am Server nicht vorhanden!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
        }


        private void DownloadFile(string localFilePath, string name = null)
        {
            FileInfo fiDownload = new FileInfo(localFilePath);

            if (name == null)
                name = fiDownload.Name;

            using (FileStream fileStream = fiDownload.OpenRead())
                Application.Download(fileStream, name);
        }


        private void btnNewPwdOk_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.OK;
        }

        private void btnNewPwdCancel_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strUsername = ((MainPage) Application.MainPage).UserInfo.Username;

            string filePath = Application.MapPath($"~/OpenVPN/{strUsername}/OpenVPN_Installer_{strUsername}.sfx.exe");
            if (File.Exists(filePath))
                Application.DownloadAndOpen("", filePath);
            else
            {
                AlertBox.Show("Zugang ist noch nicht angelegt!", MessageBoxIcon.Stop, true, ContentAlignment.MiddleCenter);
            }
        }
    }
}
