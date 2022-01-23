using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SOComponentsTest
{
    public partial class FrmSelectVideo : XtraForm
    {
        public String SelectedFile
        {
            get
            {
                return this.textBox1.Text;
            }
        }

        public String InstallationPath { get; set; }

        public FrmSelectVideo()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.DefaultExt = "avi";
            if (!string.IsNullOrEmpty(InstallationPath))
                this.openFileDialog1.InitialDirectory = InstallationPath;
            this.openFileDialog1.Filter = "Videodateien (*.avi)|*.avi|Alle Dateien (*.*)|*.*";
            this.openFileDialog1.Title = "Video Öffnen";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
