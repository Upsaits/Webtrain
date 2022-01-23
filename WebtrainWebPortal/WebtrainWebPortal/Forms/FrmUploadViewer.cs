using System;
using System.IO;
using System.Web;
using Wisej.Web;

namespace WebtrainWebPortal.Forms
{
    public partial class FrmUploadViewer : Form
    {
        public WorkflowMediator WorkflowMediator
        {
            get => Application.Session.workflowMediator;
        }

        private int RecId { get; }
        public string FilePath { get; set; } = "";
        public string Username { get; set; } = "";

        public FrmUploadViewer(string strTitle="", string strUsername="", int iRecId = -1,string strFilename="")
        {
            InitializeComponent();
            if (strTitle.Length > 0)
            {
                Text = strTitle;
                upload1.Visible = false;
                pdfViewer1.PdfSource = strFilename;
            }

            RecId = iRecId;
            Username = strUsername;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (FilePath.Length>0 && File.Exists(FilePath))
                File.Delete(FilePath);
            Close();
            DialogResult = DialogResult.Cancel;
        }

        private void LoadFile(HttpFileCollection files)
        {
            if (files == null)
                return;

            var f = files[0];
            if (f != null)
            {
                string filePath = Application.MapPath($"~/Data/{f.FileName}");
                f.SaveAs(filePath);
            }
        }

        private void upload1_Uploaded(object sender, UploadedEventArgs e)
        {
            if (e.Files.Count > 1)
            {
                //todo: geht nicht
            }


            //LoadFile(e.Files);
            //this.pdfViewer1.PdfSource = Application.MapPath($"~/Data/{e.Files[0].FileName}");
            //this.pdfViewer1.PdfSource = Application.MapPath($"~/Data/SicknessConfirmation_156.pdf"); //geht

            //this.pdfViewer1.PdfSource = Application.MapPath($"~/Data") + @"\SicknessConfirmation_156.pdf";

            if (e.Files == null || e.Files.Count > 1)
            {
                //todo: geht nicht
            }

            string strFilename = $"~/Data/SicknessConfirmation_{Username}_{RecId}.pdf";
            string strFilePath = Application.MapPath(strFilename);
            if (File.Exists(strFilePath))
                File.Delete(strFilePath);
            if (e.Files != null)
            {
                e.Files[0]?.SaveAs(strFilePath);
                //LoadFile(e.Files);
                pdfViewer1.PdfSource = strFilePath;
                upload1.Enabled = false;
                FilePath = strFilePath;
                //pdfViewer1.PdfSource = Application.MapPath($"~/Data/SicknessConfirmation_156.pdf"); //geht
            }
        }
    }
}
