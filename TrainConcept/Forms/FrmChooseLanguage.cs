using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;

namespace SoftObject.TrainConcept
{
	/// <summary>
	/// Zusammendfassende Beschreibung für Language.
	/// </summary>
    public partial class FrmChooseLanguage :XtraForm
	{
		private string m_startLanguage;
		private string m_language;
		private ResourceHandler rh=null;
        private AppHandler AppHandler = Program.AppHandler;
		public string Language
		{
			get
			{
				return m_language;
			}
		}

		public FrmChooseLanguage()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			InitializeComponent();
			this.Icon = rh.GetIcon("main");

			this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS","Ok","OK");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.Text = AppHandler.LanguageHandler.GetText("FORMS","ChooseLanguage","Sprachauswahl");

			m_startLanguage=AppHandler.Language;
			if (m_startLanguage == "de")
				this.BtnGerman.Checked = true;
			else if (m_startLanguage == "en_GB")
				this.BtnEngland.Checked = true;
			else if (m_startLanguage == "fr_FR")
				this.BtnFrench.Checked = true;
			else if (m_startLanguage == "it_IT")
				this.BtnItaly.Checked = true;
			else if (m_startLanguage == "es_ES")
				this.BtnSpain.Checked = true;
			else if (m_startLanguage == "es_ES")
				this.BtnSpain.Checked = true;
			else if (m_startLanguage == "es_ES")
				this.BtnSpain.Checked = true;
			else if (m_startLanguage == "et")
				this.BtnEstonia.Checked = true;
			else if (m_startLanguage == "zh_CN")
				this.BtnChinese.Checked = true;
			else if (m_startLanguage == "hu")
				this.BtnHungarian.Checked = true;
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if (this.BtnGerman.Checked)
				 m_language="de";
			else if (this.BtnEngland.Checked)
				 m_language="en_GB";
			else if (this.BtnFrench.Checked)
				m_language="fr_FR";
			else if (this.BtnItaly.Checked)
				m_language="it_IT";
			else if (this.BtnSpain.Checked)
				m_language="es_ES";
			else if (this.BtnEstonia.Checked)
				m_language="et";
			else if (this.BtnChinese.Checked)
				m_language="zh_CN";
			else if (this.BtnHungarian.Checked)
				m_language="hu";

			DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			m_language=m_startLanguage;
			DialogResult = DialogResult.Cancel;
		}
	}
}
