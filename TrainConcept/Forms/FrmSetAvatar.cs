using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmSetAvatar : XtraForm
    {
        private int m_imgId;
        private string m_strUserName;
        private bool m_shouldRemove=false;
        private AppHandler AppHandler = Program.AppHandler;

        public bool ShouldRemove { get { return m_shouldRemove; }}
        public int ImageId { get { return m_imgId; } }

        public FrmSetAvatar()
        {
            InitializeComponent();
        }

        public FrmSetAvatar(string strUserName)
        {
            InitializeComponent();

            m_strUserName = strUserName;
            Image img = AppHandler.UserAvatars.Get(m_strUserName);
            if (img != null)
            {
                this.pictureEdit1.Image = img;
                m_imgId = AppHandler.UserAvatars.GetId(m_strUserName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Picture Files |*.bmp;*.gif;*.jpg;*.jpeg;*.ico;*.png";
            dlg.Title = "Open";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool bOk = true;
                Image img = null;
                try
                {
                    img = Image.FromFile(dlg.FileName);
                }
                catch
                {
                    bOk = false;
                }

                string strExt = Path.GetExtension(dlg.FileName);
                int iRes = -1;
                string txt = AppHandler.LanguageHandler.GetText("ERROR", "picture_cannot_be_loaded", "Bild kann nicht geladen werden");

                if (bOk && (img != null) && (strExt != null))
                {
                    iRes = AppHandler.UserAvatars.Add(m_strUserName + strExt, img);
                    if (iRes==0)
                        txt = AppHandler.LanguageHandler.GetText("MESSAGE", "picture_already_used", "Bild wird schon verwendet");
                    else
                    {
                        this.pictureEdit1.Image = AppHandler.UserAvatars.Get(m_strUserName);
                        m_imgId = AppHandler.UserAvatars.GetId(m_strUserName);
                    }
                }

                if (iRes <= 0)
                {
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.pictureEdit1.Image = null;
            m_imgId = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.pictureEdit1.Image == null && AppHandler.UserAvatars.GetId(m_strUserName) >= 0)
                m_shouldRemove = true;
            this.DialogResult = DialogResult.OK;
        }

    }
}
