using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Newtonsoft.Json;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmImportContentModule : DevExpress.XtraEditors.XtraForm
    {
        private string m_strSelectedLibTitle;
        private bool m_bIsExisting;
        private string m_strSelectedZipFilename;
        private string m_strSelectedLibFilename;
        private AppHandler AppHandler = Program.AppHandler;

        public XFrmImportContentModule()
        {
            InitializeComponent();
            string strSrcTemplateFilePath = Program.AppHandler.ContentFolder + @"\Leer\Templates";
            chkAdaptTemplates.Checked = Directory.Exists(strSrcTemplateFilePath);
            chkAdaptTemplates.Enabled = Directory.Exists(strSrcTemplateFilePath);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = AppHandler.ImportExportFolder;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!bCheckWebtrainModule(openFileDialog1.FileName))
                    MessageBox.Show("Die gewählte Datei ist keine Webtrain-Inhaltsdatei!", "Error");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (m_bIsExisting)
            {
                if (AppHelpers.CreateSavedLibrary(m_strSelectedLibTitle))
                {
                    AppHandler.ContentManager.CloseContentEditor();
                    if (!AppHelpers.DestroyLibrary(m_strSelectedLibTitle))
                        return;
                }
            }

            if (AppHelpers.ImportLibrary(m_strSelectedLibTitle, m_strSelectedLibFilename, m_strSelectedZipFilename, this.chkAdaptTemplates.Checked))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Fehler beim Importieren", "Error");
                DialogResult = DialogResult.Cancel;
            }
        }

        private bool bCheckWebtrainModule(string zipFileName)
        {
            try
            {
	            using (ZipArchive archive = ZipFile.OpenRead(zipFileName))
	            {
	                foreach (ZipArchiveEntry entry in archive.Entries)
	                {
	                    if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
	                    {
	                        string strJsonFile = Path.GetTempPath() + Path.GetFileName(entry.FullName);
	                        entry.ExtractToFile(strJsonFile,true);
	
	                        WebtrainPackageInfo pi= null;
	                        using (StreamReader r = new StreamReader(strJsonFile))
	                        {
	                            string json=r.ReadToEnd();
	                            pi = JsonConvert.DeserializeObject<WebtrainPackageInfo>(json);
	                        }

                            if (pi != null)
                            {
                                groupBox1.Enabled = true;
                                edtFile.Text = zipFileName;
                                dtCreated.DateTime = pi.Created;
                                edtTitle.Text = pi.Title;
                                edtAutor.Text = pi.Autor;
                                edtDescription.Text = pi.Description;
                                btnImport.Enabled = true;

                                m_strSelectedLibTitle = pi.Title;
                                m_strSelectedZipFilename = zipFileName;
                                m_strSelectedLibFilename = pi.ContentFilename;

                                if (AppHandler.LibManager.GetLibrary(pi.Title) != null)
                                {
                                    string strText = "Das Modul {0} ist bereits im System vorhanden.\r\n" +
                                                     "Falls sie dieses Modul importieren wird eine Kopie der aktuellen Version erzeugt\r\n" +
                                                     "welches dann bei Bedarf wiederhergestellt werden kann.";
                                    edtWarning.Text = string.Format(strText, pi.Title);
                                    edtWarning.Visible = true;
                                    picWarning.Visible = true;
                                    m_bIsExisting = true;
                                }
                                else
                                {
                                    edtWarning.Visible = false;
                                    picWarning.Visible = false;
                                    m_bIsExisting = false;
                                }
                            }

                            archive.Dispose();
                            return true;
	                    }
	                }
	            }
            }
            catch (System.Exception /*ex*/)
            {
            	
            }
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void chkAdaptTemplates_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkAdaptTemplates.Checked)
            {
                MessageBox.Show("Achtung, ein Abwählen dieser Option, kann zu Schwierigkeiten bei der Änderung dieser Inhalte führen!", "Warnung",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
