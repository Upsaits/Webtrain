using System;
using System.Windows.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for FrmEditKeywordAction.
	/// </summary>
    public partial class XFrmEditKeywordAction : DevExpress.XtraEditors.XtraForm
	{
		private bool m_isModified=false;
		private bool m_wasTooltip=false;
		private bool m_wasGlossary=false;
        private bool m_isNew = false;
        private string m_contentPath = "";
        private string m_keyId = "";
        private AppHandler AppHandler = Program.AppHandler;

        public string KeyText
		{
			get
			{
				return txtEditText.Text;
			}
		}

		public string KeyDescription
		{
			get
			{
				return txtEditDescription.Text;
			}
		}

        public string KeyId
        {
            get
            {
                return m_keyId;
            }
        }

		public bool IsTooltip
		{
			get
			{
				return chkIsTooltip.Checked;
			}
		}

		public bool IsGlossary
		{
			get
			{
				return chkIsGlossary.Checked;
			}
		}

		public bool IsModified
		{
			get
			{
				return m_isModified;
			}
		}

        public bool IsNew
        {
            get
            {
                return m_isNew;
            }
        }

        public XFrmEditKeywordAction(string contentPath)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            m_wasTooltip = false;
            m_wasGlossary = false;

            txtEditText.Text = "neues stichwort";
            txtEditDescription.Text = "Beschreibung.....";

            chkIsTooltip.Checked = true;
            chkIsGlossary.Checked = true;
            m_isNew = true;
            m_contentPath = contentPath;
        }

		public XFrmEditKeywordAction(string text,string description,bool isTooltip,bool isGlossary)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_wasTooltip=isTooltip;
			m_wasGlossary=isGlossary;
			
			txtEditText.Text = text;
			txtEditDescription.Text = description;

			chkIsTooltip.Checked = isTooltip;
			chkIsGlossary.Checked = isGlossary;
		}


		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if(txtEditText.CanUndo)
				m_isModified = true;

			if(txtEditDescription.CanUndo)
				m_isModified = true;

			if (chkIsTooltip.Checked != m_wasTooltip ||
				chkIsGlossary.Checked != m_wasGlossary)
				m_isModified = true;

            if (m_isNew)
            {
                var aKeywords = new KeywordCollection();
                AppHandler.LibManager.GetKeywords(m_contentPath, ref aKeywords);
                /*
                if (aKeywords.Count>0 && aKeywords.Find(KeyText)!=null)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "keyword_already_defined", "Stichwort bereits definiert!");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }*/
                m_keyId = String.Format("key{0}", aKeywords.Count + 1);
            }

			DialogResult = DialogResult.OK;
        }

        private void chkIsTooltip_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtEditText_Leave(object sender, EventArgs e)
        {
            if (m_isNew)
            {
                var aKeywords = new KeywordCollection();
                AppHandler.LibManager.GetKeywords(m_contentPath, ref aKeywords);
                if (aKeywords.Count > 0 && aKeywords.Find(KeyText) != null)
                {
                    string txt = AppHandler.LanguageHandler.GetText("ERROR", "keyword_already_defined", "Das Stichwort ist bereits definiert!");
                    string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    var keywordAction=aKeywords.Find(KeyText);
                    txtEditDescription.Text = keywordAction.description;
                    txtEditDescription.ReadOnly=true;
                }
                else
                    txtEditDescription.ReadOnly =false;
            }
        }
	}
}
