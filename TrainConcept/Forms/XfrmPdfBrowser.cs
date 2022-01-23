using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XfrmPdfBrowser : XtraForm
    {
        private string m_filePath="";
        private AppHandler AppHandler = Program.AppHandler;

        public XfrmPdfBrowser()
        {
            InitializeComponent();
        }

        public XfrmPdfBrowser(string strTitle,string strFilePath)
        {
            m_filePath = strFilePath;

            InitializeComponent();

            this.Text = strTitle;
        }


        private void XfrmPdfBrowser_Load(object sender, EventArgs e)
        {
            var uri = new Uri(m_filePath);
            if (!uri.IsFile)
            {
                MemoryStream stream;
                var wc = new WebClient();
                byte[] data = wc.DownloadData(m_filePath);
                stream = new MemoryStream(data);
                pdfViewer1.LoadDocument(stream);
            }
            else
            {
                pdfViewer1.LoadDocument(m_filePath);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "document_not_saved_correctly");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");

            string strDestFolder = AppHandler.ReportsFolder;

            try
            {
                if (!Directory.Exists(strDestFolder))
                    Directory.CreateDirectory(AppHandler.ReportsFolder);
            }
            catch (System.Exception /*ex*/)
            {
                strDestFolder = AppHandler.WorkingFolder;
            }

            string strDest = strDestFolder + @"\" + String.Format("sozialanamnese_{0}.pdf", AppHandler.MainForm.ActualUserName);

            try
            {
               if (pdfViewer1.SaveDocument(strDest))
                   txt = AppHandler.LanguageHandler.GetText("MESSAGE", "document_saved_correctly");
            }
            catch (System.Exception /*ex*/)
            {
                // ignored
            }

            MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
