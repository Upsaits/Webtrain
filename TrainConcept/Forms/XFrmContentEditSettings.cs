using System;
using System.Windows.Forms;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmContentEditSettings : DevExpress.XtraEditors.XtraForm
    {
        private AppHandler AppHandler = Program.AppHandler;

        public XFrmContentEditSettings()
        {
            InitializeComponent();

            txtContentFolder.Text = AppHandler.ContentFolder;
            txtMediaFolder.Text = AppHandler.ContentEditMediaFolder;
        }

        private void btnSelectMediaFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = AppHandler.ContentEditMediaFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtMediaFolder.Text = folderBrowserDialog1.SelectedPath;
                AppHandler.ContentEditMediaFolder = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCheckPPT_Click(object sender, EventArgs e)
        {
            //var frm = new Form1();
            var frm = new FrmSelectServer();
            frm.ShowDialog();
        }
    }
}
