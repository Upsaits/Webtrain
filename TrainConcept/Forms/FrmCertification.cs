using System;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmCertification.
	/// </summary>
	public partial class FrmCertification : XtraForm
	{
		private static Color emcoColor=System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(10)), ((System.Byte)(0)));
		private static int lineHeight=20;
		private static int bigLineHeight=50;
		private static int lineWidth=600;
		private static int logoWidth=600;
		private static int logoHeight=200;
		private static int tabWidth1=150;

		private string	userName="";
		private string  contentTitle="";
		private string	successTitle="";
		private Image   imgLogo=null;
        private AppHandler AppHandler = Program.AppHandler;
		public FrmCertification()
		{
			userName="Franz Mair";
			contentTitle="Grundlagen der Programmierung";
			successTitle="mit ausgezeichnetem Erfolg";
			
			InitializeComponent();

			this.lblTitle.Text  = "ZERTIFIKAT";
			this.lblPerson.Text = userName;
			this.lblVisitedText.Text = "besuchte das Ausbildungsseminar";
			this.lblContentTitle.Text = contentTitle;
			this.lblSuccess.Text = successTitle;
			this.lblTeacher.Text = "Hr. Ing. Scheinecker Wolfgang";
			this.lblPlace.Text = "Gmunden";
			this.lblDate.Text = DateTime.Now.ToString("D");
		}

		public FrmCertification(string _userName,string _contentTitle,int _successLevel,
								string _teacherName,string _place,DateTime _dateTime,
								Image _imgLogo)
		{
			userName=_userName;
			contentTitle=_contentTitle;
			imgLogo = _imgLogo;
		
			switch(_successLevel)
			{
				case 0: successTitle=AppHandler.LanguageHandler.GetText("FORMS","without_success","ohne Erfolg");break;
				case 1: successTitle=AppHandler.LanguageHandler.GetText("FORMS","with_success","mit Erfolg");break;
				case 2: successTitle=AppHandler.LanguageHandler.GetText("FORMS","with_good_success","mit Gutem Erfolg");break;
				case 3: successTitle=AppHandler.LanguageHandler.GetText("FORMS","with_very_good_success","mit Sehr Gutem Erfolg");break;
				case 4: successTitle=AppHandler.LanguageHandler.GetText("FORMS","with_wonderful_success","mit Ausgezeichnetem Erfolg");break;
			}

			InitializeComponent();

			this.lblTeacher1.Text = AppHandler.LanguageHandler.GetText("FORMS","PracticeTeacher","Ausbildungsleiter:");
			this.lblDate1.Text	  = AppHandler.LanguageHandler.GetText("FORMS","Date","Datum:");
			this.lblPlace1.Text   = AppHandler.LanguageHandler.GetText("FORMS","PracticePlace","Ausbildungsort:");
			this.lblTitle.Text	  = AppHandler.LanguageHandler.GetText("FORMS","certification","ZERTIFIKAT");
			this.lblVisitedText.Text = AppHandler.LanguageHandler.GetText("FORMS","visited_the_practice","besuchte das Ausbildungsseminar");

			this.lblPerson.Text = userName;
			this.lblContentTitle.Text = contentTitle;
			this.lblSuccess.Text = successTitle;
			this.lblTeacher.Text = _teacherName;
			this.lblPlace.Text = _place;
			this.lblDate.Text = _dateTime.ToString("D");
		}

		private void FrmCertification_Activated(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void FrmCertification_Load(object sender, System.EventArgs e)
		{
			link1.CreateDocument();
			link1.ShowPreview();
			printingSystem1.PreviewFormEx.Closed += new EventHandler(ClosePreviewForm);
		}

		private void ClosePreviewForm(object sender, System.EventArgs e)
		{
			Close();
		}


		private void link1_CreatePageHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
		}

		private void link1_CreateDetailArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
			int h=0;
			if (imgLogo!=null)
			{
				//h=lineWidth*imgLogo.Size.Height/imgLogo.Size.Width;
				h=logoHeight;
				RectangleF r = new Rectangle(0,0,logoWidth,h);
				e.Graph.DrawImage(imgLogo,r,BorderSide.None,Color.White);
			}

			h+=lineHeight;
			e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
			e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
			e.Graph.Font = this.lblTitle.Font;
			e.Graph.DrawString(lblTitle.Text,lblTitle.ForeColor, new Rectangle(0, h, lineWidth, bigLineHeight),BorderSide.None);

			h+=2*bigLineHeight;
			e.Graph.Font = this.lblPerson.Font;
			e.Graph.DrawString(lblPerson.Text,lblPerson.ForeColor, new Rectangle(0, h, lineWidth, 2*bigLineHeight),BorderSide.None);

			h+=2*bigLineHeight;
			e.Graph.Font = this.lblVisitedText.Font;
			e.Graph.DrawString(lblVisitedText.Text,lblVisitedText.ForeColor, new Rectangle(0, h, lineWidth, bigLineHeight),BorderSide.None);

			h+=2*bigLineHeight;
			e.Graph.Font = this.lblContentTitle.Font;
			e.Graph.DrawString(lblContentTitle.Text,lblContentTitle.ForeColor, new Rectangle(0, h, lineWidth, bigLineHeight),BorderSide.None);

			h+=2*bigLineHeight;
			e.Graph.Font = this.lblSuccess.Font;
			e.Graph.DrawString(lblSuccess.Text,lblSuccess.ForeColor, new Rectangle(0, h, lineWidth, bigLineHeight),BorderSide.None);

			// Informationsteil
			h+=2*bigLineHeight;
			e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Near);
			e.Graph.Font = this.lblTeacher1.Font;
			e.Graph.DrawString(lblTeacher1.Text,Color.Black, new Rectangle(0,h,tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(this.lblTeacher.Text,Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);

			h+=lineHeight;
			e.Graph.Font = this.lblDate1.Font;
			e.Graph.DrawString(lblDate1.Text,Color.Black, new Rectangle(0, h, tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(this.lblDate.Text,Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);

			h+=lineHeight;
			e.Graph.Font = this.lblPlace1.Font;
			e.Graph.DrawString(this.lblPlace1.Text,Color.Black, new Rectangle(0,h,tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(this.lblPlace.Text,Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);
		}

		private void link1_CreateDetailHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
		
		}

		private void link1_CreatePageFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
		
		}
	}
}
