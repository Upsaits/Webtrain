using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.SOComponents.UtilityLibrary;


namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmEditCertification.
	/// </summary>
	public partial class FrmEditCertification :XtraForm
	{
		private string userName="";
		private string contentTitle="";
		private ResourceHandler rh=null;
		private Image m_imageLogo = null;
        private AppHandler AppHandler = Program.AppHandler;
		public string UserName
		{
			get {return txtPerson.Text;}
		}

		public string ContentTitle
		{
			get {return txtTitle.Text;}
		}

		public string TeacherName
		{
			get {return txtTeacher.Text;}
		}

		public string Place
		{
			get {return txtPlace.Text;}
		}

		public DateTime Time
		{
			get {return dateTimePicker1.Value;}
		}
		
		public int SuccessLevel
		{
			get
			{
				if (rbnNoSuccess.Checked)
					return 0;
				else if (rbnWithSuccess.Checked)
					return 1;
				else if (rbnGoodSuccess.Checked)
					return 2;
				else if (rbnVeryGoodSuccess.Checked)
					return 3;
				else
					return 4;
			}
		}

		public Image LogoImage
		{
			get {return this.iedLogo.Image;}
		}


		public FrmEditCertification()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);

			InitializeComponent();

			this.Icon = rh.GetIcon("main");
		}

		public FrmEditCertification(string _userName,string _contentTitle)
		{
			userName=_userName;
			contentTitle=_contentTitle;

			InitializeComponent();

			this.lblPerson.Text = AppHandler.LanguageHandler.GetText("FORMS","Practician","Auszubildender")+':';
			this.lblContentTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","CourseTitle","Ausbildungstitel")+':';
			this.grpSuccessType.Text = AppHandler.LanguageHandler.GetText("FORMS","Mark","Beurteilung")+':';
			this.rbnWonderfulSuccess.Text = AppHandler.LanguageHandler.GetText("FORMS","with_wonderful_success","mit Ausgezeichnetem Erfolg");
			this.rbnVeryGoodSuccess.Text = AppHandler.LanguageHandler.GetText("FORMS","with_very_good_success","mit Sehr Gutem Erfolg");
			this.rbnGoodSuccess.Text = AppHandler.LanguageHandler.GetText("FORMS","with_good_success","mit Gutem Erfolg");
			this.rbnWithSuccess.Text = AppHandler.LanguageHandler.GetText("FORMS","with_success","mit Erfolg");
			this.rbnNoSuccess.Text = AppHandler.LanguageHandler.GetText("FORMS","without_success","ohne Erfolg");
			this.btnCancel.Text = AppHandler.LanguageHandler.GetText("FORMS","Cancel","Abbrechen");
			this.btnOk.Text = AppHandler.LanguageHandler.GetText("FORMS","Ok","Ok");
			this.lblTeacher.Text = AppHandler.LanguageHandler.GetText("FORMS","PracticeTeacher","Ausbildungsleiter:");
			this.lblPlace.Text = AppHandler.LanguageHandler.GetText("FORMS","PracticePlace","Ausbildungsort:");
			this.lblDate.Text = AppHandler.LanguageHandler.GetText("FORMS","Date","Datum:");
			this.Text = AppHandler.LanguageHandler.GetText("FORMS","make_certification","Zertifikat erstellen");



			this.txtPerson.Text = _userName;
			this.txtTitle.Text = _contentTitle;
			this.dateTimePicker1.Value = DateTime.Now;
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
		}

		private void iedLogo_ImageChanged(object sender, System.EventArgs e)
		{
		}

		private void FrmEditCertification_Load(object sender, System.EventArgs e)
		{
			try
			{
				m_imageLogo=null;
                string strFilename = AppHandler.ImgCertificationHeader;

                if (System.IO.File.Exists(strFilename))
                {
                    m_imageLogo = Image.FromFile(strFilename);
                    if (m_imageLogo != null)
                        iedLogo.Image = m_imageLogo;
                }
                
			}
			catch(System.Exception /*ex*/)
			{
			}
		}

        private void btnChooseLogo_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = AppHandler.ExeFolder;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AppHandler.ImgCertificationHeader = openFileDialog1.FileName;
                iedLogo.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

	}
}
