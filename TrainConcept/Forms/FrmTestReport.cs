using System;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmTestReport.
	/// </summary>
    public partial class FrmTestReport : XtraForm
	{
		private static Color emcoColor=System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(10)), ((System.Byte)(0)));
		private static int lineHeight=20;
		private static int medLineHeight=45;
		private static int bigLineHeight=50;
		private static int lineWidth=600;
		private static int tabWidth1=150;
		private static int tabWidth2=500;
        private AppHandler AppHandler = Program.AppHandler;

		private TestResultItem m_testResult=null;

		public FrmTestReport()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
		}

		public FrmTestReport(TestResultItem testResult)
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			m_testResult = testResult;

			this.lblHeaderLinePage.Text = AppHandler.LanguageHandler.GetText("FORMS","Page_No_Of","Seite {0}/{1}");
			this.lblHeaderLine.Text = AppHandler.LanguageHandler.GetText("FORMS","HeaderLine","Webtrain");
			this.lblPercRight.Text = AppHandler.LanguageHandler.GetText("FORMS","Percent_right","% - Richtig")+':';
			this.lblPercWrong.Text = AppHandler.LanguageHandler.GetText("FORMS","Percent_wrong","% - Falsch")+':';
			this.lblResultTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","Result","Ergebnis");
			this.lblQuestionTitle.Text = AppHandler.LanguageHandler.GetText("FORMS","Question","Frage");
			this.lblLearnmap.Text = AppHandler.LanguageHandler.GetText("FORMS","Learnmap","Learnmappe")+':';
			this.lblUser.Text = AppHandler.LanguageHandler.GetText("FORMS","User","Benutzer")+':';
			this.lblDate.Text = AppHandler.LanguageHandler.GetText("FORMS","Date","Datum")+':';
			this.lblTime.Text = AppHandler.LanguageHandler.GetText("FORMS","Time","Uhrzeit")+':';
			this.lblResult.Text = AppHandler.LanguageHandler.GetText("FORMS","Testresult","TESTERGEBNIS");
		}


		private void FrmTestReport_Load(object sender, System.EventArgs e)
		{
			link1.CreateDocument();
			link1.ShowPreview();
			printingSystem1.PreviewFormEx.Closed += new EventHandler(ClosePreviewForm);
		}


		private void FrmTestReport_Activated(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void link1_CreatePageHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
			// ZeilenHeader
			RectangleF r = new RectangleF(20,20,0,lineHeight);
			e.Graph.Font = e.Graph.DefaultFont;
			e.Graph.BackColor = Color.Transparent;
			PageInfoBrick b = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal,this.lblHeaderLinePage.Text, Color.Black, r, BorderSide.None);
			b.Alignment = BrickAlignment.Far;
			b.AutoWidth = true;

			b = e.Graph.DrawPageInfo(PageInfo.None,this.lblHeaderLine.Text,Color.Black, r, BorderSide.None);
			b.Alignment = BrickAlignment.Near;
			b.AutoWidth = true;
		}

		private void link1_CreateDetailArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
			string sFullName="";
			string sPassword="";
            int iImgId = 0; 
			AppHandler.UserManager.GetUserInfo(m_testResult.userName,ref sPassword,ref sFullName,ref iImgId);

			int h=bigLineHeight;
			e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
			e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
			e.Graph.Font = this.lblResult.Font;
			e.Graph.DrawString(this.lblResult.Text,this.lblResult.ForeColor, new Rectangle(0, 0, lineWidth, h), BorderSide.All);

			// Informationsteil
			h+=3*lineHeight;
			e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Near);
			e.Graph.Font = this.lblUser.Font;
			e.Graph.DrawString(this.lblUser.Text,Color.Black, new Rectangle(0,h,tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(sFullName,Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);

			h+=lineHeight;
			e.Graph.Font = this.lblDate.Font;
			e.Graph.DrawString(this.lblDate.Text,Color.Black, new Rectangle(0, h, tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(m_testResult.startTime.ToString("D"),Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);

			h+=lineHeight;
			e.Graph.Font = this.lblTime.Font;
			e.Graph.DrawString(this.lblTime.Text,Color.Black, new Rectangle(0,h,tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(m_testResult.startTime.ToString("T"),Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);

			h+=lineHeight;
			e.Graph.DrawString(this.lblLearnmap.Text,Color.Black, new Rectangle(0,h, tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(m_testResult.mapName,Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);

			h+=bigLineHeight;
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center, StringAlignment.Center);
			e.Graph.BackColor = Color.LightGray;
			e.Graph.ForeColor = Color.Black;
			e.Graph.Font = this.lblQuestionTitle.Font;
			e.Graph.DrawString(this.lblQuestionTitle.Text, new Rectangle(0,h,tabWidth2,medLineHeight));
			e.Graph.DrawString(this.lblResultTitle.Text, new Rectangle(tabWidth2,h,lineWidth-tabWidth2,medLineHeight));

			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near, StringAlignment.Center);
			e.Graph.BackColor = Color.White;
			e.Graph.ForeColor = Color.Black;
			e.Graph.Font = this.lblQuestion.Font;

			string sTR=m_testResult.ToString();
            Console.WriteLine(sTR);

			for(int i=0;i<m_testResult.aTestQuestionResults.Length;++i)
			{
				TestQuestionResultItem result=m_testResult.aTestQuestionResults[i];
				QuestionItem qu= AppHandler.LibManager.GetQuestion(result.path,result.quId);
				if (qu!=null)
				{
					h+=medLineHeight;
                    if (qu.type == "MultipleChoice")
                    {
                        e.Graph.DrawString(qu.question, new Rectangle(0, h, tabWidth2, medLineHeight));
                        e.Graph.DrawCheckBox(new Rectangle(tabWidth2, h, lineWidth - tabWidth2, medLineHeight), result.IsRight(qu.correctAnswerMask));
                    }
                    else if (qu.type == "Completion")
                    {
                        foreach (var action in qu.QuestionActions)
                        {
                            if (action is TextActionItem && action.id == "txtdescription")
                            {
                                e.Graph.DrawString((action as TextActionItem).text+"["+result.quizAnswers+"]", new Rectangle(0, h, tabWidth2, medLineHeight));
                                e.Graph.DrawCheckBox(new Rectangle(tabWidth2, h, lineWidth - tabWidth2, medLineHeight),result.quizResult);
                            }
                        }
                    }
				}
			}

			h+=(bigLineHeight+lineHeight);
			e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Near);
			e.Graph.Font = this.lblPercRight.Font;
			e.Graph.DrawString(this.lblPercRight.Text,Color.Black, new Rectangle(0,h,tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(String.Format("{0}",m_testResult.percRight),Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);

			h+=lineHeight;
			e.Graph.Font = this.lblPercWrong.Font;
			e.Graph.DrawString(this.lblPercWrong.Text,Color.Black, new Rectangle(0, h,tabWidth1,lineHeight), BorderSide.None);
			e.Graph.DrawString(String.Format("{0}",(100-m_testResult.percRight)),Color.Black, new Rectangle(tabWidth1,h,lineWidth-tabWidth1,lineHeight), BorderSide.None);
		}

		private void link1_CreateDetailFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
		}

		private void link1_CreateDetailHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
		{
		}

		private void ClosePreviewForm(object sender, System.EventArgs e) 
		{
			Close();
		}
	}
}
