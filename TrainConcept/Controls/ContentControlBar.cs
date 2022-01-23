using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;
using SoftObject.SOComponents.Controls;
using SoftObject.SOComponents.UtilityLibrary;
using DevExpress.XtraEditors;
using DevComponents.DotNetBar;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Zusammendfassende Beschreibung für ContentControlBar.
	/// </summary>
	public partial class ContentControlBar : System.Windows.Forms.UserControl
	{
		public enum WorkingModeType 
		{
			Learning,
			Examing,
			Testing
		};

		private SORadioBmpBtn btnLearning;
		private SORadioBmpBtn btnExaming;
		private SORadioBmpBtn btnTesting;
		private WorkingModeType workingMode;
		private ResourceHandler rh=null;
        private int pageId = 0;
		private int maxPages=0;
        private SOChart soChart1=null;
		private AppHandler AppHandler = Program.AppHandler;

		public SoftObject.SOComponents.Controls.SOChart ChartControl
        {
            get { return soChart1; }
            set { soChart1 = value; }
        }

		public int PageId
		{
			get { return pageId;}
			set
			{
				pageId=value;
				if(pageId==0)
				{
					btnForward.Enabled = false;
					btnBack.Enabled = false;
                    HighlightButton(ref btnNextPage, false);
                    HighlightButton(ref btnPrevPage, false);
                }
				else if(pageId == 1)
				{
					btnBack.Enabled =false;
                    HighlightButton(ref btnPrevPage, true);
                    if (maxPages <= 1)
                        btnForward.Enabled = false;
                    else
                    {
                        btnForward.Enabled = true;
                        HighlightButton(ref btnNextPage, false);
                    }
				}
				else if(pageId == maxPages)
				{
					btnBack.Enabled =true;
					btnForward.Enabled = false;
                    HighlightButton(ref btnNextPage, true);
                    HighlightButton(ref btnPrevPage, false);
                }
				else
				{
					btnForward.Enabled = true;
					btnBack.Enabled = true;
                    HighlightButton(ref btnNextPage, false);
                    HighlightButton(ref btnPrevPage, false);
                }
				label1.Text = pageId.ToString() + "/" + maxPages.ToString();
			}
	    }

        public bool ButtonPrevPageEnabled
        {
            set
            {
                btnPrevPage.Enabled = value;
                if (!btnPrevPage.Enabled)
                    HighlightButton(ref btnPrevPage, false);
            }
        }

        public bool ButtonNextPageEnabled
        {
            set
            {
                btnNextPage.Enabled = value;
                if (!btnNextPage.Enabled)
                    HighlightButton(ref btnNextPage, false);
            }
        }

		public int MaxPages
		{
			get
			{
				return maxPages;
			}

			set
			{
				maxPages=value;
				if (maxPages>0)
					PageId=1;
				else
					PageId=0;
			}
		}

		public WorkingModeType WorkingMode
		{
			get
			{
				return workingMode;
			}

			set
			{
				workingMode = value;
				switch(workingMode)
				{
					case WorkingModeType.Learning:
						 btnWorkout.Hide();
						 btnSolution.Hide();
						 btnChoose.Hide();
						 btnBack.Show();
						 btnForward.Show();
						 btnPrevPage.Show();
						 btnNextPage.Show();
						 btnLearning.Checked = true;
						 label1.Show();
						break;
					case WorkingModeType.Examing:	
						 btnBack.Hide();
						 btnForward.Hide();
						 btnPrevPage.Hide();
						 btnNextPage.Hide();
						 btnWorkout.Show();
						 btnSolution.Show();
						 btnChoose.Show();
						 btnExaming.Checked = true;
						 label1.Hide();
						 OnResize(EventArgs.Empty);
						break;
					case WorkingModeType.Testing:	
						 btnBack.Show();
						 btnForward.Show();
						 btnPrevPage.Hide();
						 btnNextPage.Hide();
						 btnWorkout.Show();
						 btnSolution.Hide();
						 btnChoose.Hide();
						 label1.Hide();
						 OnResize(EventArgs.Empty);
						break;
				}
				btnVideo.Hide();
				btnAnim.Hide();
				btnGrafic.Hide();
				btnSimulation.Hide();
				btnJump.Hide();
			}
		}

        public BubbleButtonCollection DocumentButtons
        {
            get
            {
                return bubbleBarTab1.Buttons;
            }
        }

	    public SORadioBmpBtn BtnLearning
	    {
	        get { return btnLearning; }
	    }

        public SORadioBmpBtn BtnExaming
        {
            get { return btnExaming; }
        }

        public SORadioBmpBtn BtnTesting
        {
            get { return btnTesting; }
        }

	    public SimpleButton BtnBack
	    {
	        get { return btnBack; }
	    }

	    public SimpleButton BtnForward
	    {
	        get { return btnForward; }
	    }

        public SimpleButton BtnWorkout
        {
            get { return btnWorkout; }
        }

        public SimpleButton BtnSolution
	    {
            get { return btnSolution; }
	    }

        public SimpleButton BtnChoose
        {
            get { return btnChoose; }
        }

        public SimpleButton BtnJump
        {
            get { return btnJump; }
        }

        public SimpleButton BtnGrafic
        {
            get { return btnGrafic; }
        }

        public SimpleButton BtnVideo
        {
            get { return btnVideo; }
        }

        public SimpleButton BtnAnim
        {
            get { return btnAnim; }
        }

        public SimpleButton BtnSimulation
        {
            get { return btnSimulation; }
        }

        public Button BtnNextPage
        {
            get { return btnNextPage; }
        }

	    public Button BtnPrevPage
	    {
	        get { return btnPrevPage; }
	    }

		public ContentControlBar()
		{
			//
			// Required for Windows Form Designer support
			//
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);

			InitializeComponent();
			
			SetupButtons();

			WorkingMode=WorkingModeType.Learning;

			OnResize(EventArgs.Empty);
		}


		protected override void OnResize(EventArgs ea)
		{
			base.OnResize(ea);
            if (soChart1 != null)
            {
                Point pos = btnNextPage.Location;
                pos.Offset(3, 3);
                soChart1.Location = this.PointToClient(pos);
            }
		}

		private void SetupButtons()
		{
			this.SuspendLayout();

			lBtnImages.Images.Add(rh.GetBitmap("left"));
			lBtnImages.Images.Add(rh.GetBitmap("right"));
			lBtnImages.Images.Add(rh.GetBitmap("auswerten"));
			lBtnImages.Images.Add(rh.GetBitmap("loesung"));
			lBtnImages.Images.Add(rh.GetBitmap("ziehen"));
			lBtnImages.Images.Add(rh.GetBitmap("Sprung"));
			lBtnImages.Images.Add(rh.GetBitmap("Grafic"));
			lBtnImages.Images.Add(rh.GetBitmap("anim"));
			lBtnImages.Images.Add(rh.GetBitmap("video"));
			lBtnImages.Images.Add(rh.GetBitmap("Simulation"));


			btnBack.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Back","Zurück");
			btnBack.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			btnBack.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnBack.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","BackDescription","zurück blättern");
			btnBack.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnBack.LookAndFeel.UseDefaultLookAndFeel = false;
			btnBack.Font = AppHandler.DefaultFont;

			btnForward.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Forward","Vorwärts");
			btnForward.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			btnForward.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnForward.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","ForwardDescription","vorwärts blättern");
			btnForward.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnForward.LookAndFeel.UseDefaultLookAndFeel = false;
			btnForward.Font = AppHandler.DefaultFont;

			btnWorkout.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Workout","Auswerten");
			btnWorkout.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			btnWorkout.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnWorkout.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","WorkoutDescription","Fragen auswerten");
			btnWorkout.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnWorkout.LookAndFeel.UseDefaultLookAndFeel = false;
			btnWorkout.Font = AppHandler.DefaultFont;

			// --- Lösung---
			btnSolution.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Solution","Lösung");
			btnSolution.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			btnSolution.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnSolution.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","SolutionDescription","Lösung anzeigen");
			btnSolution.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnSolution.LookAndFeel.UseDefaultLookAndFeel = false;
			btnSolution.Font = AppHandler.DefaultFont;
		
			// --- Ziehen ---
			btnChoose.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Choose","Ziehen");
			btnChoose.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			btnChoose.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnChoose.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","ChooseDescription","Neue Fragen ziehen");
			btnChoose.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnChoose.LookAndFeel.UseDefaultLookAndFeel = false;
			btnChoose.Font = AppHandler.DefaultFont;

			// --- Rücksprung ---
			btnJump.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","JumpBack","Rücksprung");
			btnJump.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			btnJump.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnJump.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","JumpBackDescription","Rücksprung von Verzweigung");
			btnJump.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnJump.LookAndFeel.UseDefaultLookAndFeel = false;
			btnJump.Font = AppHandler.DefaultFont;


			// --- Grafik ---
			btnGrafic.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Grafic","Grafik");
			btnGrafic.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","GraficDescription","Nächste Grafik zeigen");
			btnGrafic.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			btnGrafic.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnGrafic.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
			btnGrafic.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnGrafic.LookAndFeel.UseDefaultLookAndFeel = false;
			btnGrafic.Font = AppHandler.DefaultFont;
			
			// --- Video ---
			btnVideo.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Video","Video");
			btnVideo.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","VideoDescription","Video zeigen");
			btnVideo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			btnVideo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnVideo.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnVideo.LookAndFeel.UseDefaultLookAndFeel = false;
			btnVideo.Font = AppHandler.DefaultFont;
			
			// --- Animation ---
			btnAnim.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Animation","Anim");
			btnAnim.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","AnimationDescription","Animation zeigen");
			btnAnim.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			btnAnim.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnAnim.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnAnim.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAnim.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			btnAnim.Font = AppHandler.DefaultFont;

			// --- Simulation ---
			btnSimulation.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Simulation","Sim");
			btnSimulation.ToolTip = AppHandler.LanguageHandler.GetText("CONTROLBAR","SimulationDescription","Simulation aufrufen");
			btnSimulation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			btnSimulation.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			btnSimulation.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			btnSimulation.LookAndFeel.UseDefaultLookAndFeel = false;
			btnSimulation.Font = AppHandler.DefaultFont;

			// --- Lernen ---
			imageList1.Images.Add(rh.GetBitmap("learningNormal"));
			imageList1.Images.Add(rh.GetBitmap("learningOver"));
			imageList1.Images.Add(rh.GetBitmap("learningPressed"));
			btnLearning = new SORadioBmpBtn(imageList1,0,imageList1.Images[0].Width,imageList1.Images[0].Height);
			btnLearning.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Learn","Lernen");
			btnLearning.ToolTipText = AppHandler.LanguageHandler.GetText("CONTROLBAR","LearnDescription","Aus der Mappe Lernen");
			btnLearning.TextAlign = ContentAlignment.BottomCenter;
			btnLearning.AutoCheck = true;
			btnLearning.Checked = true;
            btnLearning.Font = AppHandler.ContentModeButtonFont;

			// --- Üben ---
			imageList2.Images.Add(rh.GetBitmap("exerciseNormal"));
			imageList2.Images.Add(rh.GetBitmap("exerciseOver"));
			imageList2.Images.Add(rh.GetBitmap("exercisePressed"));
			btnExaming = new SORadioBmpBtn(imageList2,0,imageList2.Images[0].Width,imageList2.Images[0].Height);
			btnExaming.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Examing","Üben");
			btnExaming.ToolTipText = AppHandler.LanguageHandler.GetText("CONTROLBAR","ExamingDescription","Aus der Mappe Üben");
			btnExaming.TextAlign = ContentAlignment.BottomCenter;
			btnExaming.AutoCheck = true;
            btnExaming.Font = AppHandler.ContentModeButtonFont;

			// --- Prüfen ---
			imageList3.Images.Add(rh.GetBitmap("checkNormal"));
			imageList3.Images.Add(rh.GetBitmap("checkOver"));
			imageList3.Images.Add(rh.GetBitmap("checkPressed"));
			btnTesting = new SORadioBmpBtn(imageList3,0,imageList3.Images[0].Width,imageList3.Images[0].Height);
			btnTesting.Text = AppHandler.LanguageHandler.GetText("CONTROLBAR","Testing","Testen");
			btnTesting.ToolTipText = AppHandler.LanguageHandler.GetText("CONTROLBAR","TestingDescription","Aus der Mappe Testen");
			btnTesting.TextAlign = ContentAlignment.BottomCenter;
			btnTesting.AutoCheck = true;
            btnTesting.Font = AppHandler.ContentModeButtonFont;

			this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {this.btnLearning,
																				 this.btnExaming,
																				 this.btnTesting});

			Size btnSize3 = imageList3.Images[0].Size;

			btnLearning.Location = new System.Drawing.Point(0,(panel3.ClientSize.Height-btnSize3.Height)/2); 
			btnExaming.Location = new System.Drawing.Point(btnSize3.Width,(panel3.ClientSize.Height-btnSize3.Height)/2); 
			btnTesting.Location = new System.Drawing.Point(2*btnSize3.Width,(panel3.ClientSize.Height-btnSize3.Height)/2); 

			this.ResumeLayout();
        }

        public void HighlightButton(ref Button btn, bool bIsOn)
        {
            if (bIsOn)
            {
                btn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
                btn.FlatAppearance.BorderSize = 3;
            }
            else 
            {
                btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                btn.FlatAppearance.BorderSize = 1;
            }
        }

        public void ClearDocuments()
        {
            bubbleBarTab1.Buttons.Clear();
            bubbleBar1.Visible = false;
        }

        public void AddDocumentButton(string strTitle, int typeId)
        {
			if (typeId>0 && typeId<=3)
            {
				var bubbtn = new BubbleButton();
				if (typeId == 1)
				{
					bubbtn.Image = rh.GetIcon("file_pdf").ToBitmap();
					bubbtn.ImageLarge = rh.GetIcon("file_pdf").ToBitmap();
				}
				else if (typeId == 2)
				{
					bubbtn.Image = rh.GetIcon("file_pptx").ToBitmap();
					bubbtn.ImageLarge = rh.GetIcon("file_pptx").ToBitmap();
				}
				else if (typeId == 3)
                {
					bubbtn.Image = rh.GetIcon("file_xapi").ToBitmap();
					bubbtn.ImageLarge = rh.GetIcon("file_xapi").ToBitmap();
				}

				bubbtn.Name = strTitle;
				bubbtn.TooltipText = strTitle;
				bubbleBarTab1.Buttons.Add(bubbtn);
				bubbleBar1.Visible = true;
			}
		}

        private void btnWorkout_Click(object sender, EventArgs e)
        {

        }
	}
}
