using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using SoftObject.TrainConcept.Libraries;
using SoftObject.TrainConcept.Learnmaps;
using SoftObject.UtilityLibrary;

namespace SoftObject.TrainConcept
{
	/// <summary>
	/// Zusammendfassende Beschreibung für FrmLMEditContent.
	/// </summary>
	public class FrmLMEditContent : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ListView lvwWorkings;
		private SoftObject.TrainConcept.XContentTree treContent;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Button btnDistribute;
		private System.Windows.Forms.Panel panButtonBar;
		private System.Windows.Forms.Panel panWorkingBar;
		private System.Windows.Forms.Button btnWorkings;
		private System.Windows.Forms.Panel panTestBar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton rbnQuSelect;
		private System.Windows.Forms.RadioButton rbnQuChoose;
		private System.Windows.Forms.ListView lvwQuestions;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.NumericUpDown spnQuCnt;
		private System.Windows.Forms.NumericUpDown spnSuccessLevel;
		private System.Windows.Forms.NumericUpDown spnTrialCnt;
		private enum EditMode {Workings,Tests};
		private string mapTitle="";
		private ResourceHandler rh=null;

		public FrmLMEditContent(string _mapTitle)
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
			rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			this.Icon = rh.GetIcon("main");
			this.btnWorkings.Text = AppHandler.LanguageHandler.GetText("FORMS","Contents","Inhalte");
			this.btnDistribute.Text = AppHandler.LanguageHandler.GetText("FORMS","Distribute","Verteilen");
			this.btnTest.Text = AppHandler.LanguageHandler.GetText("FORMS","Tests","Tests");
			this.label4.Text = AppHandler.LanguageHandler.GetText("FORMS","Trialcount","Anzahl Versuche")+':';
			this.label2.Text = AppHandler.LanguageHandler.GetText("FORMS","Successlevel","Erfolgsgrenze")+':';
			this.label1.Text = AppHandler.LanguageHandler.GetText("FORMS","Questioncount","Fragenanzahl")+':';
			this.rbnQuChoose.Text = AppHandler.LanguageHandler.GetText("FORMS","Choose_questions_randomized","Fragen zufällig wählen");
			this.rbnQuSelect.Text = AppHandler.LanguageHandler.GetText("FORMS","Use_questionlist","Fragenliste verwenden");
			
			mapTitle = _mapTitle;

			Initialize();
		}

		public void Initialize()
		{
			string sWorkings=AppHandler.LanguageHandler.GetText("FORMS","Workings","Bearbeitungen");
			string sPages=AppHandler.LanguageHandler.GetText("FORMS","Pages","Seiten");
			string sFromContent=AppHandler.LanguageHandler.GetText("FORMS","From_content","Aus Inhalt");
			string sQuestion=AppHandler.LanguageHandler.GetText("FORMS","Question","Frage");

			lvwWorkings.Columns.Add(sWorkings,lvwWorkings.Size.Width*2/3,HorizontalAlignment.Center);
			lvwWorkings.Columns.Add(sPages,lvwWorkings.Size.Width/3,HorizontalAlignment.Center);

			string [] aWorkings=null;
			if (AppHandler.MapManager.GetWorkings(mapTitle,ref aWorkings))
				for(int i=0;i<aWorkings.Length;++i)
					AddWorking(aWorkings[i]);

			this.lvwQuestions.Columns.Add(sFromContent,lvwWorkings.Size.Width/2,HorizontalAlignment.Center);
			this.lvwQuestions.Columns.Add(sQuestion,lvwWorkings.Size.Width/2,HorizontalAlignment.Center);

			SwitchEditMode(EditMode.Workings);
		}

		private void SwitchEditMode(EditMode tEdit)
		{
			if (tEdit==EditMode.Workings)
			{
				if (this.rbnQuSelect.Checked)
				{
					if (this.lvwQuestions.Items.Count<this.spnQuCnt.Value)
					{
						string txt=AppHandler.LanguageHandler.GetText("MESSAGE","QuestionCount_not_achieved","Fragenanzahl noch nicht erreicht!");
						string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","TrainConcept");
						MessageBox.Show(txt,cap,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						return;
					}
				}

				this.panWorkingBar.Dock = DockStyle.Fill;
				this.panWorkingBar.Visible=true;
				this.btnTest.Visible=true;
				this.btnDistribute.Visible=true;
				this.panTestBar.Visible =false;
				this.btnWorkings.Visible=false;
				this.treContent.UseType = ContentTreeViewList.UseType.FullContent;
			}
			else
			{
				this.panWorkingBar.Visible=false;
				this.btnTest.Visible=false;
				this.btnDistribute.Visible=false;
				this.panTestBar.Visible =true;
				this.panTestBar.Dock = DockStyle.Fill;
				this.btnWorkings.Visible=true;
				this.treContent.UseType = ContentTreeViewList.UseType.QuestionContent;
				this.treContent.LearnmapName = mapTitle;

				bool randomChoose=true;
				int questionCnt=10;
				int trialCnt=1;
				int successLevel=85;
				if (!AppHandler.MapManager.GetTest(mapTitle,ref randomChoose,ref questionCnt,ref trialCnt,ref successLevel))
				{
					AppHandler.MapManager.SetTest(mapTitle,randomChoose,questionCnt,trialCnt,successLevel);
					AppHandler.MapManager.Save(mapTitle);
				}

				if (randomChoose)
					this.rbnQuChoose.Checked=true;
				else
					this.rbnQuSelect.Checked=true;

				this.spnQuCnt.Text=questionCnt.ToString();
				this.spnSuccessLevel.Text = successLevel.ToString();
				this.spnTrialCnt.Text = trialCnt.ToString();
			}

			treContent.FillTree();
			treContent.PopulateColumns();
			treContent.BestFitColumns();
			treContent.CollapseAll();
		}

		protected override void OnResize(EventArgs ea)
		{
			base.OnResize(ea);

			treContent.Size = new Size(ClientSize.Width,ClientSize.Height/2);

			if (lvwWorkings.Columns.Count>0)
			{
				lvwWorkings.Columns[0].Width = ClientSize.Width*2/3;
				lvwWorkings.Columns[1].Width = ClientSize.Width/3;
			}

			if (lvwQuestions.Columns.Count>0)
			{
				lvwQuestions.Columns[0].Width = panel1.ClientSize.Width/2;
				lvwQuestions.Columns[1].Width = panel1.ClientSize.Width/2;
			}
		}

		private TreeListNode GetDragNode(IDataObject data)
		{
			return data.GetData(typeof(TreeListNode)) as DevExpress.XtraTreeList.Nodes.TreeListNode;
		}

		private void AddPoint(string path)
		{
			if (!ExistsWorking(path))
			{
				AddWorking(path);
				AppHandler.MapManager.AddWorking(mapTitle,path);
				AppHandler.MapManager.Save(mapTitle);
				AppHandler.ContentManager.CloseLearnmap(mapTitle);
			}
		}

		private void AddChapter(string path)
		{
			ChapterItem chp=AppHandler.LibManager.GetChapter(path);
			if (chp!=null)
			{
				int cntAdded=0;
				for(int i=0;i<chp.Points.Length;++i)
				{
					string [] aPath= {path,chp.Points[i].title};
					string sWorking = AppHandler.LibManager.MergePath(aPath);
					if (!ExistsWorking(sWorking))
					{
						AddWorking(sWorking);
						AppHandler.MapManager.AddWorking(mapTitle,sWorking);
						++cntAdded;
					}
				}

				if (cntAdded>0)
				{
					AppHandler.MapManager.Save(mapTitle);
					AppHandler.ContentManager.CloseLearnmap(mapTitle);
				}
			}
		}

		private void AddBook(string path)
		{
			BookItem book=AppHandler.LibManager.GetBook(path);
			if (book!=null)
				for(int i=0;i<book.Chapters.Length;++i)
				{
					string [] aPath= {path,book.Chapters[i].title};
					AddChapter(AppHandler.LibManager.MergePath(aPath));
				}
		}

		private void AddLibrary(string title)
		{
			LibraryItem lib=AppHandler.LibManager.GetLibrary(title);
			if (lib!=null)
				for(int i=0;i<lib.Books.Length;++i)
				{
					string [] aPath= {title,lib.Books[i].title};
					AddBook(AppHandler.LibManager.MergePath(aPath));
				}
		}

		private void AddQuestion(string path,int quId)
		{
			if (ExistsWorking(path) && !ExistsQuestion(path,quId))
			{
				QuestionItem que=AppHandler.LibManager.GetQuestion(path,quId);

				ListViewItem lvi = new ListViewItem(path);
				lvi.SubItems.Add(que.question);
				lvi.Tag=quId;
				this.lvwQuestions.Items.Add(lvi);

				if (Int32.Parse(this.spnQuCnt.Text)<this.lvwQuestions.Items.Count)
					spnQuCnt.Text=lvwQuestions.Items.Count.ToString();


				AppHandler.MapManager.AddTestQuestion(mapTitle,path,quId);
				AppHandler.MapManager.Save(mapTitle);
			}
		}

		private bool ExistsWorking(string working)
		{
			string [] aWorkings=null;
			if (AppHandler.MapManager.GetWorkings(mapTitle,ref aWorkings))
				for(int i=0;i<aWorkings.Length;++i)
					if (String.Compare(aWorkings[i],working,true)==0)
						return true;
			return false;
		}

		private bool ExistsQuestion(string contentPath,int questionId)
		{
			TestQuestionItem [] aItems=null;
			if (AppHandler.MapManager.GetTestQuestions(mapTitle,ref aItems))
				for(int i=0;i<aItems.Length;++i)
					if (String.Compare(aItems[i].contentPath,contentPath,true)==0 &&
						aItems[i].questionId == questionId)
							return true;
			return false;
		}

		private void AddWorking(string working)
		{
			int pageCnt;
			bool isOk = AppHandler.LibManager.GetPageCnt(working,out pageCnt);

			ListViewItem lvi = new ListViewItem(working);
			lvi.SubItems.Add(pageCnt.ToString());
			lvwWorkings.Items.Add(lvi);
		}

		private void SetTestValues()
		{
			bool randomChoose=rbnQuChoose.Checked;
			int questionCnt=(int) spnQuCnt.Value;
			int trialCnt=(int) spnTrialCnt.Value;
			int successLevel=(int) spnSuccessLevel.Value;
			AppHandler.MapManager.SetTest(mapTitle,randomChoose,questionCnt,trialCnt,successLevel);
			AppHandler.MapManager.Save(mapTitle);
			AppHandler.TestResultManager.FireEvent(EventArgs.Empty);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.treContent = new SoftObject.TrainConcept.XContentTree();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panButtonBar = new System.Windows.Forms.Panel();
			this.btnWorkings = new System.Windows.Forms.Button();
			this.btnDistribute = new System.Windows.Forms.Button();
			this.btnTest = new System.Windows.Forms.Button();
			this.panWorkingBar = new System.Windows.Forms.Panel();
			this.lvwWorkings = new System.Windows.Forms.ListView();
			this.panTestBar = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lvwQuestions = new System.Windows.Forms.ListView();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.spnTrialCnt = new System.Windows.Forms.NumericUpDown();
			this.spnSuccessLevel = new System.Windows.Forms.NumericUpDown();
			this.spnQuCnt = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.rbnQuChoose = new System.Windows.Forms.RadioButton();
			this.rbnQuSelect = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.treContent)).BeginInit();
			this.panButtonBar.SuspendLayout();
			this.panWorkingBar.SuspendLayout();
			this.panTestBar.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spnTrialCnt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnSuccessLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spnQuCnt)).BeginInit();
			this.SuspendLayout();
			// 
			// treContent
			// 
			this.treContent.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.treContent.Appearance.Empty.Options.UseBackColor = true;
			this.treContent.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(50)), ((System.Byte)(135)), ((System.Byte)(206)), ((System.Byte)(250)));
			this.treContent.Appearance.EvenRow.Options.UseBackColor = true;
			this.treContent.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.treContent.Appearance.FocusedCell.Options.UseBackColor = true;
			this.treContent.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(10)), ((System.Byte)(36)), ((System.Byte)(106)));
			this.treContent.Appearance.FocusedRow.Options.UseBackColor = true;
			this.treContent.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.treContent.Appearance.FooterPanel.Options.UseBackColor = true;
			this.treContent.Appearance.GroupButton.BackColor = System.Drawing.SystemColors.Window;
			this.treContent.Appearance.GroupButton.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.treContent.Appearance.GroupButton.Options.UseBackColor = true;
			this.treContent.Appearance.GroupButton.Options.UseForeColor = true;
			this.treContent.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.treContent.Appearance.GroupFooter.Options.UseBackColor = true;
			this.treContent.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(230)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.treContent.Appearance.HeaderPanel.Options.UseBackColor = true;
			this.treContent.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(128)));
			this.treContent.Appearance.HideSelectionRow.Options.UseBackColor = true;
			this.treContent.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(90)), ((System.Byte)(255)), ((System.Byte)(160)), ((System.Byte)(122)));
			this.treContent.Appearance.OddRow.Options.UseBackColor = true;
			this.treContent.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(120)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.treContent.Appearance.Preview.Options.UseBackColor = true;
			this.treContent.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(90)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.treContent.Appearance.Row.Options.UseBackColor = true;
			this.treContent.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(230)), ((System.Byte)(10)), ((System.Byte)(36)), ((System.Byte)(106)));
			this.treContent.Appearance.SelectedRow.Options.UseBackColor = true;
			this.treContent.Appearance.VertLine.Options.UseTextOptions = true;
			this.treContent.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.treContent.BestFitVisibleOnly = true;
			this.treContent.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.treContent.Dock = System.Windows.Forms.DockStyle.Top;
			this.treContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.treContent.LearnmapName = null;
			this.treContent.Location = new System.Drawing.Point(0, 0);
			this.treContent.Name = "treContent";
			this.treContent.OptionsBehavior.AutoChangeParent = false;
			this.treContent.OptionsBehavior.AutoSelectAllInEditor = false;
			this.treContent.OptionsBehavior.CloseEditorOnLostFocus = false;
			this.treContent.OptionsBehavior.DragNodes = true;
			this.treContent.OptionsBehavior.Editable = false;
			this.treContent.OptionsBehavior.ExpandNodeOnDrag = false;
			this.treContent.OptionsBehavior.KeepSelectedOnClick = false;
			this.treContent.OptionsBehavior.MoveOnEdit = false;
			this.treContent.OptionsBehavior.ShowToolTips = false;
			this.treContent.OptionsBehavior.SmartMouseHover = false;
			this.treContent.OptionsMenu.EnableColumnMenu = false;
			this.treContent.OptionsMenu.EnableFooterMenu = false;
			this.treContent.OptionsPrint.AutoRowHeight = false;
			this.treContent.OptionsPrint.AutoWidth = false;
			this.treContent.OptionsPrint.PrintHorzLines = false;
			this.treContent.OptionsPrint.PrintImages = false;
			this.treContent.OptionsPrint.PrintPageHeader = false;
			this.treContent.OptionsPrint.PrintReportFooter = false;
			this.treContent.OptionsPrint.PrintTree = false;
			this.treContent.OptionsPrint.PrintTreeButtons = false;
			this.treContent.OptionsPrint.PrintVertLines = false;
			this.treContent.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.treContent.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.treContent.OptionsView.ShowColumns = false;
			this.treContent.OptionsView.ShowIndentAsRowStyle = true;
			this.treContent.OptionsView.ShowIndicator = false;
			this.treContent.Size = new System.Drawing.Size(624, 192);
			//this.treContent.Styles.AddReplace("PressedColumn", new DevExpress.Utils.ViewStyle("PressedColumn", "TreeList", new System.Drawing.Font("Microsoft Sans Serif", 8F), "HeaderPanel", ((DevExpress.Utils.StyleOptions)(((DevExpress.Utils.StyleOptions.StyleEnabled | DevExpress.Utils.StyleOptions.UseBackColor) 
			//	| DevExpress.Utils.StyleOptions.UseForeColor))), true, false, false, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, null, System.Drawing.SystemColors.ControlDark, System.Drawing.SystemColors.ControlLightLight));
			this.treContent.TabIndex = 0;
			this.treContent.UseType = SoftObject.TrainConcept.ContentTreeViewList.UseType.FullContent;
			this.treContent.DragEnter += new System.Windows.Forms.DragEventHandler(this.treContent_DragEnter);
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.Control;
			this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 192);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(624, 6);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// panButtonBar
			// 
			this.panButtonBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panButtonBar.Controls.Add(this.btnWorkings);
			this.panButtonBar.Controls.Add(this.btnDistribute);
			this.panButtonBar.Controls.Add(this.btnTest);
			this.panButtonBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panButtonBar.Location = new System.Drawing.Point(0, 498);
			this.panButtonBar.Name = "panButtonBar";
			this.panButtonBar.Size = new System.Drawing.Size(624, 48);
			this.panButtonBar.TabIndex = 3;
			// 
			// btnWorkings
			// 
			this.btnWorkings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWorkings.Location = new System.Drawing.Point(8, 8);
			this.btnWorkings.Name = "btnWorkings";
			this.btnWorkings.Size = new System.Drawing.Size(88, 32);
			this.btnWorkings.TabIndex = 2;
			this.btnWorkings.Text = "Inhalte";
			this.btnWorkings.Visible = false;
			this.btnWorkings.Click += new System.EventHandler(this.btnWorkings_Click);
			// 
			// btnDistribute
			// 
			this.btnDistribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDistribute.Location = new System.Drawing.Point(112, 8);
			this.btnDistribute.Name = "btnDistribute";
			this.btnDistribute.Size = new System.Drawing.Size(88, 32);
			this.btnDistribute.TabIndex = 1;
			this.btnDistribute.Text = "Verteilen";
			this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
			// 
			// btnTest
			// 
			this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTest.Location = new System.Drawing.Point(8, 8);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(88, 32);
			this.btnTest.TabIndex = 0;
			this.btnTest.Text = "Tests";
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// panWorkingBar
			// 
			this.panWorkingBar.Controls.Add(this.lvwWorkings);
			this.panWorkingBar.Location = new System.Drawing.Point(0, 216);
			this.panWorkingBar.Name = "panWorkingBar";
			this.panWorkingBar.Size = new System.Drawing.Size(624, 96);
			this.panWorkingBar.TabIndex = 4;
			// 
			// lvwWorkings
			// 
			this.lvwWorkings.AllowDrop = true;
			this.lvwWorkings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwWorkings.Location = new System.Drawing.Point(0, 0);
			this.lvwWorkings.Name = "lvwWorkings";
			this.lvwWorkings.Size = new System.Drawing.Size(624, 96);
			this.lvwWorkings.TabIndex = 6;
			this.lvwWorkings.View = System.Windows.Forms.View.Details;
			this.lvwWorkings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwWorkings_KeyDown);
			this.lvwWorkings.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwWorkings_DragDrop);
			this.lvwWorkings.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwWorkings_DragEnter);
			this.lvwWorkings.SelectedIndexChanged += new System.EventHandler(this.lvwWorkings_SelectedIndexChanged);
			// 
			// panTestBar
			// 
			this.panTestBar.Controls.Add(this.panel1);
			this.panTestBar.Controls.Add(this.splitter2);
			this.panTestBar.Controls.Add(this.panel2);
			this.panTestBar.Location = new System.Drawing.Point(8, 312);
			this.panTestBar.Name = "panTestBar";
			this.panTestBar.Size = new System.Drawing.Size(616, 184);
			this.panTestBar.TabIndex = 7;
			this.panTestBar.Visible = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lvwQuestions);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(410, 184);
			this.panel1.TabIndex = 10;
			// 
			// lvwQuestions
			// 
			this.lvwQuestions.AllowDrop = true;
			this.lvwQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwQuestions.Enabled = false;
			this.lvwQuestions.Location = new System.Drawing.Point(0, 0);
			this.lvwQuestions.Name = "lvwQuestions";
			this.lvwQuestions.Size = new System.Drawing.Size(410, 184);
			this.lvwQuestions.TabIndex = 2;
			this.lvwQuestions.View = System.Windows.Forms.View.Details;
			this.lvwQuestions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwQuestions_KeyDown);
			this.lvwQuestions.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmLMEditContent_DragDrop);
			this.lvwQuestions.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmLMEditContent_DragEnter);
			this.lvwQuestions.SelectedIndexChanged += new System.EventHandler(this.lvwQuestions_SelectedIndexChanged);
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter2.Location = new System.Drawing.Point(410, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(6, 184);
			this.splitter2.TabIndex = 11;
			this.splitter2.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.spnTrialCnt);
			this.panel2.Controls.Add(this.spnSuccessLevel);
			this.panel2.Controls.Add(this.spnQuCnt);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.rbnQuChoose);
			this.panel2.Controls.Add(this.rbnQuSelect);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(416, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(200, 184);
			this.panel2.TabIndex = 12;
			// 
			// spnTrialCnt
			// 
			this.spnTrialCnt.Location = new System.Drawing.Point(128, 144);
			this.spnTrialCnt.Maximum = new System.Decimal(new int[] {
																		20,
																		0,
																		0,
																		0});
			this.spnTrialCnt.Minimum = new System.Decimal(new int[] {
																		1,
																		0,
																		0,
																		0});
			this.spnTrialCnt.Name = "spnTrialCnt";
			this.spnTrialCnt.Size = new System.Drawing.Size(48, 20);
			this.spnTrialCnt.TabIndex = 12;
			this.spnTrialCnt.Value = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			this.spnTrialCnt.ValueChanged += new System.EventHandler(this.spnTrialCnt_ValueChanged);
			// 
			// spnSuccessLevel
			// 
			this.spnSuccessLevel.Location = new System.Drawing.Point(128, 104);
			this.spnSuccessLevel.Minimum = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			0});
			this.spnSuccessLevel.Name = "spnSuccessLevel";
			this.spnSuccessLevel.Size = new System.Drawing.Size(48, 20);
			this.spnSuccessLevel.TabIndex = 11;
			this.spnSuccessLevel.Value = new System.Decimal(new int[] {
																		  1,
																		  0,
																		  0,
																		  0});
			this.spnSuccessLevel.ValueChanged += new System.EventHandler(this.spnSuccessLevel_ValueChanged);
			// 
			// spnQuCnt
			// 
			this.spnQuCnt.Location = new System.Drawing.Point(128, 72);
			this.spnQuCnt.Maximum = new System.Decimal(new int[] {
																	 10,
																	 0,
																	 0,
																	 0});
			this.spnQuCnt.Minimum = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 0});
			this.spnQuCnt.Name = "spnQuCnt";
			this.spnQuCnt.Size = new System.Drawing.Size(64, 20);
			this.spnQuCnt.TabIndex = 10;
			this.spnQuCnt.Value = new System.Decimal(new int[] {
																   1,
																   0,
																   0,
																   0});
			this.spnQuCnt.ValueChanged += new System.EventHandler(this.spnQuCnt_ValueChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 32);
			this.label4.TabIndex = 8;
			this.label4.Text = "Anzahl Versuche:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(176, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(16, 24);
			this.label3.TabIndex = 7;
			this.label3.Text = "%";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 32);
			this.label2.TabIndex = 5;
			this.label2.Text = "Erfolgsgrenze:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 32);
			this.label1.TabIndex = 3;
			this.label1.Text = "Fragenanzahl:";
			// 
			// rbnQuChoose
			// 
			this.rbnQuChoose.Checked = true;
			this.rbnQuChoose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbnQuChoose.Location = new System.Drawing.Point(8, 16);
			this.rbnQuChoose.Name = "rbnQuChoose";
			this.rbnQuChoose.Size = new System.Drawing.Size(168, 16);
			this.rbnQuChoose.TabIndex = 0;
			this.rbnQuChoose.TabStop = true;
			this.rbnQuChoose.Text = "Fragen zufällig wählen";
			this.rbnQuChoose.CheckedChanged += new System.EventHandler(this.rbnQuChoose_CheckedChanged);
			// 
			// rbnQuSelect
			// 
			this.rbnQuSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbnQuSelect.Location = new System.Drawing.Point(8, 40);
			this.rbnQuSelect.Name = "rbnQuSelect";
			this.rbnQuSelect.Size = new System.Drawing.Size(168, 16);
			this.rbnQuSelect.TabIndex = 1;
			this.rbnQuSelect.Text = "Fragenliste verwenden";
			this.rbnQuSelect.CheckedChanged += new System.EventHandler(this.rbnQuSelect_CheckedChanged);
			// 
			// FrmLMEditContent
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 546);
			this.Controls.Add(this.panTestBar);
			this.Controls.Add(this.panWorkingBar);
			this.Controls.Add(this.panButtonBar);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treContent);
			this.Name = "FrmLMEditContent";
			this.Text = "FrmLMEditContent";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmLMEditContent_DragDrop);
			this.Closed += new System.EventHandler(this.FrmLMEditContent_Closed);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmLMEditContent_DragEnter);
			((System.ComponentModel.ISupportInitialize)(this.treContent)).EndInit();
			this.panButtonBar.ResumeLayout(false);
			this.panWorkingBar.ResumeLayout(false);
			this.panTestBar.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spnTrialCnt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnSuccessLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spnQuCnt)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void lvwWorkings_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ListView lv = (ListView) sender;
			if (lv.SelectedIndices.Count>0)
			{
				ListViewItem lvi = lv.SelectedItems[0];
				if (lvi!=null)
					treContent.SelectItem(lvi.Text);
			}
		}

		private void lvwWorkings_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			ListView lv = sender as ListView;
			TreeListNode node = GetDragNode(e.Data);
			if(node != null) 
			{
				string dragString = treContent.ContentList.GetPath(node.Id);

				string [] aTitles=null;
				AppHandler.LibManager.SplitPath(dragString,out aTitles);

				if (aTitles.Length==4)
				{
					AddPoint(dragString);
				}
				else if (aTitles.Length==3)
				{
					AddChapter(dragString);
				}
				else if (aTitles.Length==2)
				{
					AddBook(dragString);
				}
				else if (aTitles.Length==1)
				{
					AddLibrary(dragString);
				}
			}
		}

		private void treContent_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
		}

		private void lvwWorkings_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeListNode node = GetDragNode(e.Data);
			if (node!=null)
				e.Effect = DragDropEffects.Copy;
		}

		private void lvwWorkings_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				ListView lv = (ListView) sender;
				if (lv.SelectedIndices.Count>0)
				{
					string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Do_you_want_to_delete_selected_items","Wollen Sie alle markierten Einträge löschen?");
					string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","TrainConcept");
					if (MessageBox.Show(txt,cap,MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
					{
						for(int i=lv.SelectedIndices.Count;i>0;--i)
						{
							ListViewItem lvi = lv.SelectedItems[i-1];
							AppHandler.MapManager.DeleteWorking(mapTitle,lvi.Text);
							lv.Items.Remove(lvi);
						}

						AppHandler.MapManager.Save(mapTitle);
						AppHandler.ContentManager.CloseLearnmap(mapTitle);
					}
				}
			}
		}

		private void FrmLMEditContent_Closed(object sender, System.EventArgs e)
		{
			if (btnWorkings.Visible)
			{
				if (this.rbnQuSelect.Checked)
				{	
					if (this.lvwQuestions.Items.Count==0)
					{
						AppHandler.ContentManager.LearnmapEditorClosed(mapTitle);
						return;
					}
					if(this.lvwQuestions.Items.Count < this.spnQuCnt.Value)
						spnQuCnt.Value=lvwQuestions.Items.Count;
				}
				SetTestValues();
			}

			AppHandler.ContentManager.LearnmapEditorClosed(mapTitle);
		}

		private void btnDistribute_Click(object sender, System.EventArgs e)
		{
			FrmMapDistribut dlg = new FrmMapDistribut(mapTitle);
			dlg.ShowDialog();
		}

		private void btnTest_Click(object sender, System.EventArgs e)
		{
			SwitchEditMode(EditMode.Tests);
		}

		private void btnWorkings_Click(object sender, System.EventArgs e)
		{
			SwitchEditMode(EditMode.Workings);
		}

		private void rbnQuChoose_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.rbnQuChoose.Checked)
			{
				lvwQuestions.Enabled=false;
				lvwQuestions.Items.Clear();

				SetTestValues();
			}
		}

		private void rbnQuSelect_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.rbnQuSelect.Checked)
			{
				lvwQuestions.Enabled=true;

				TestQuestionItem [] aItems=null;
				if (AppHandler.MapManager.GetTestQuestions(mapTitle,ref aItems))
					for(int i=0;i<aItems.Length;++i)
					{
						QuestionItem que=AppHandler.LibManager.GetQuestion(aItems[i].contentPath,aItems[i].questionId);
						if (que!=null)
						{
							ListViewItem lvi = new ListViewItem(aItems[i].contentPath);
							lvi.SubItems.Add(que.question);
							lvi.Tag=aItems[i].questionId;
							this.lvwQuestions.Items.Add(lvi);
						}
					}

				if (aItems!=null)
				{
					if (aItems.Length>Int32.Parse(this.spnQuCnt.Text))
						spnQuCnt.Text=aItems.Length.ToString();
				}

				SetTestValues();
			}
		}

		private void FrmLMEditContent_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			ListView lv = sender as ListView;
			TreeListNode node = GetDragNode(e.Data);
			if(node != null) 
			{
				string contentPath = treContent.ContentList.GetPath(node.ParentNode.Id);
				AddQuestion(contentPath,((ContentTreeViewList.Record) treContent.ContentList[node.Id]).GetQuestionId());
			}
		}

		private void FrmLMEditContent_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeListNode node = GetDragNode(e.Data);
			if (node!=null)
				e.Effect = DragDropEffects.Copy;
		}

		private void lvwQuestions_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				ListView lv = (ListView) sender;
				if (lv.SelectedIndices.Count>0)
				{
					string txt=AppHandler.LanguageHandler.GetText("MESSAGE","Do_you_want_to_delete_selected_items","Wollen Sie alle markierten Einträge löschen?");
					string cap=AppHandler.LanguageHandler.GetText("SYSTEM","Title","TrainConcept");
					if (MessageBox.Show(txt,cap,MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
					{
						for(int i=lv.SelectedIndices.Count;i>0;--i)
						{
							ListViewItem lvi = lv.SelectedItems[i-1];
							AppHandler.MapManager.DeleteTestQuestion(mapTitle,lvi.Text,(int) lvi.Tag);
							lv.Items.Remove(lvi);
						}

						AppHandler.MapManager.Save(mapTitle);
						AppHandler.ContentManager.CloseLearnmap(mapTitle);
					}
				}
			}
		}

		private void spnQuCnt_ValueChanged(object sender, System.EventArgs e)
		{
			SetTestValues();		
		}

		private void spnSuccessLevel_ValueChanged(object sender, System.EventArgs e)
		{
			SetTestValues();		
		}

		private void spnTrialCnt_ValueChanged(object sender, System.EventArgs e)
		{
			SetTestValues();		
		}

		private void lvwQuestions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ListView lv = (ListView) sender;
			if (lv.SelectedIndices.Count>0)
			{
				ListViewItem lvi = lv.SelectedItems[0];
				if (lvi!=null)
					treContent.SelectItem(lvi.Text);
			}
		}
	}
}
