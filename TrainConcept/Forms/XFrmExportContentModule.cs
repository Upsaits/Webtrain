using System;
using System.IO;
using System.Windows.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmExportContentModule : DevExpress.XtraEditors.XtraForm
    {
        private string strZippedTempFileName;
        private string strLibTitle;
        private AppHandler AppHandler = Program.AppHandler;

        public bool IsPublished
        {
            get { return chkPublish.Checked; }
        }

        public string TargetFileName
        {
            get { return edtTargetPath.Text; }
        }

        public XFrmExportContentModule()
        {
            InitializeComponent();
        }

        public XFrmExportContentModule(string strLibTitle,string strLibFileNameWoExt,string strZippedTempFileName)
        {
            InitializeComponent();

            this.strZippedTempFileName = strZippedTempFileName;
            this.strLibTitle = strLibTitle;

            edtLanguage.Text = AppHandler.Language;
            edtTitle.Text = strLibTitle;
            edtTargetPath.Text = AppHandler.ImportExportFolder + @"\" + strLibFileNameWoExt+".zip";

            var lib = AppHandler.LibManager.GetLibrary(strLibTitle);
            edtVersion.Text = lib.version;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string strLibFilePath = AppHandler.LibManager.GetFilePath(strLibTitle);
            string strLibFileName = Path.GetFileName(strLibFilePath);

            var pi = new WebtrainPackageInfo();
            pi.PackageType = "Library";
            pi.Autor = edtAutor.Text;
            pi.Description = edtDescription.Text;
            pi.Title = edtTitle.Text;
            pi.Created = DateTime.Now;
            pi.Language = edtLanguage.Text;
            pi.ContentFilename = strLibFileName;
            pi.Data = "";

            if (AppHelpers.CreateLibraryZipFile(strLibTitle, strZippedTempFileName, pi))
            {
                try
                {
                    if (!Directory.Exists(AppHandler.ImportExportFolder))
                        Directory.CreateDirectory(AppHandler.ImportExportFolder);
                    File.Copy(strZippedTempFileName, edtTargetPath.Text, true);
                    File.Delete(strZippedTempFileName);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch (System.Exception /*ex*/)
                {
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }

        private void btnChangeExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = AppHandler.ImportExportFolder;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                edtTargetPath.Text = saveFileDialog1.FileName;
            }
        }
    };
}
