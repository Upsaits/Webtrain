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
using DevExpress.XtraPrinting.Native;

namespace SoftObject.SOComponents.Forms
{
    public partial class FrmShowFilename : XtraForm
    {
        public FrmShowFilename()
        {
            InitializeComponent();
        }

        public FrmShowFilename(string strFilename)
        {
            InitializeComponent();
            textBox1.Text = strFilename;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
