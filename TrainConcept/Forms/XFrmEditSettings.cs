using System;
using System.Windows.Forms;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmEditSettings : DevExpress.XtraEditors.XtraForm
    {
        private AppHandler AppHandler = Program.AppHandler;

        public XFrmEditSettings()
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
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(frm.SelectedServers.Length.ToString());
            }
        }

        private void btnSelectContent_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form2();
            frm.ShowDialog();
        }
        
    }
}
