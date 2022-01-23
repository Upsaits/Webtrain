using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;

namespace SoftObject.TrainConcept.Forms
{
    public partial class FrmNewNotice : XtraForm
    {
        private string title;
        private ResourceHandler rh = null;
        private bool bCheckExistingNotice=true;
        private AppHandler AppHandler = Program.AppHandler;

        public string Title
        {
            get
            {
                return this.title;
            }
        }

        public FrmNewNotice(bool bHasNoticeTitle=true, bool bCheckExistingNotice = true)
        {
            rh = new ResourceHandler(AppHandler.ResourceName, GetType().Assembly);
            InitializeComponent();

            this.Icon = rh.GetIcon("main");

            this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS", "Ok", "OK");
            this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS", "Cancel", "Abbrechen");
            this.label1.Text = AppHandler.LanguageHandler.GetText("FORMS", "Title", "Titel") + ':';
            if (bHasNoticeTitle)
                this.Text = AppHandler.LanguageHandler.GetText("FORMS", "New_notice", "Neue Notiz");
            else
                this.Text = AppHandler.LanguageHandler.GetText("FORMS", "New_document", "Neues Dokument");

            edtTitle.Select();
            this.bCheckExistingNotice = bCheckExistingNotice;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            title = edtTitle.Text;
            if (title.Length == 0)
            {
                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Please_enter_title", "Bitte Titel angeben!");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }

            if (bCheckExistingNotice && AppHandler.NoticeManager.Find(AppHandler.MainForm.ActualUserName,title) >= 0)
            {
                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Notice_already_exists", "Notiz existiert bereits!");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void edtTitle_Enter(object sender, EventArgs e)
        {

        }
    }
}
