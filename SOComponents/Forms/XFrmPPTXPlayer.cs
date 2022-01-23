using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.Controls;

namespace Forms
{
    public partial class XFrmPPTXPlayer : XtraForm
    {
        private string m_strFilename = "";
        PowerPointViewer.PowerPointViewerControl objPPTViewer = null;

        public XFrmPPTXPlayer()
        {
            InitializeComponent();
            objPPTViewer = new PowerPointViewer.PowerPointViewerControl();
            objPPTViewer.SerialNumber = "FranzMair3";
            objPPTViewer.LicenseKey = "9955";

            transparentFrameControl1.Initialize(-1, -1, -1, -1, objPPTViewer);
            objPPTViewer.IsHide = false;
            objPPTViewer.Fastmode = true;
            objPPTViewer.IsTopMost = true;
        }


        public void SetPPTXFile(string strFilename)
        {
            m_strFilename = strFilename;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void XFrmPPTXPlayer_Load(object sender, EventArgs e)
        {
            if (m_strFilename.Length > 0 && System.IO.File.Exists(m_strFilename))
            {
                Point absLoc = this.PointToScreen(transparentFrameControl1.Location);
                Point absSize = new Point(transparentFrameControl1.Width, transparentFrameControl1.Height);
                objPPTViewer.Open(m_strFilename, absLoc.X + 5, absLoc.Y + 5, absSize.X - 10, absSize.Y - 10);
            }

        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {

        }


        private void XFrmPPTXPlayer_FormClosed(object sender, FormClosedEventArgs e)
        {
            objPPTViewer.Close();
        }

        private void transparentFrameControl1_FrameChanged(object sender, ref SoftObject.SOComponents.TransparentFrameEventArgs ea)
        {
            Point absLoc = this.PointToScreen((sender as TransparentFrameControl).Location);
            (ea.Context as PowerPointViewer.PowerPointViewerControl).Move(absLoc.X, absLoc.Y);
        }

        private void XFrmPPTXPlayer_Move(object sender, EventArgs e)
        {
        }
    }
}
