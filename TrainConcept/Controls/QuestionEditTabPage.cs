using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Libraries;
using SoftObject.UtilityLibrary;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for QuestionEditTabPage.
	/// </summary>
	/// 
	public class QuestionEditTabPage : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private string m_sPath="";
		private string m_contentFolder="";
		private QuestionItem m_question=null;
		private int m_questionId=0;
		private ShowPageCallback  m_fnShowPage=null;
		private System.Windows.Forms.Label label1=null;
		private System.Windows.Forms.ComboBox comboBox1=null;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ImageList imageList1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraTreeList.TreeList treAnswers;
        private CheckBox chkUserForExamin;
        private CheckBox chkUserForTesting;
		private System.ComponentModel.IContainer components;
        private AppHandler AppHandler = Program.AppHandler;

        public string TemplatePath
		{
			get
			{
				if (m_question!=null)
					return 	m_contentFolder+"\\Templates\\"+m_question.templateName;
				else
					return "";
			}
		}
		
		public QuestionItem Question
		{
			get
			{
				return m_question;
			}
		}

        public int QuestionId
        {
            get { return m_questionId; }
            set { m_questionId = value; }
        }

		public QuestionEditTabPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		public QuestionEditTabPage(string sPath,int questId,ShowPageCallback fnShowPage)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			ResourceHandler rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
			this.imageList1.Images.Add(rh.GetBitmap("unchecked"));
			this.imageList1.Images.Add(rh.GetBitmap("checked"));
			this.imageList1.Images.Add(rh.GetBitmap("full_checked"));

			m_sPath = sPath;
			m_fnShowPage = fnShowPage;
			m_questionId = questId;

			// Folder für Inhalte bestimmen
			m_contentFolder = AppHandler.ContentFolder + '\\'+AppHandler.LibManager.GetFileName(m_sPath);
			m_question = AppHandler.LibManager.GetQuestion(m_sPath,m_questionId);
			if (m_question!=null)
			{
				if(m_question.Answers!=null)
				{
					BitArray corrMask = new BitArray(new int[] {m_question.correctAnswerMask});
					treAnswers.BeginUnboundLoad();
					for (int i=0;i<m_question.Answers.Length;++i)
					{
						TreeListNode node = treAnswers.AppendNode(new object[] {m_question.Answers[i]},-1);
						node.Tag = corrMask[i] ? CheckState.Checked:CheckState.Unchecked;
					}	
					treAnswers.EndUnboundLoad();
                }

                this.textBox1.Text = m_question.question;
                this.chkUserForExamin.Checked = m_question.useForExaming;
                this.chkUserForTesting.Checked = m_question.useForTesting;
                this.comboBox1.Text = m_question.templateName;
            }

		}

        public bool HasQuestionImage()
        {
            if (m_question != null && m_question.QuestionActions != null)
            {
                var cnt = m_question.QuestionActions.Where(p => p is ImageActionItem).Count();
                return (cnt >= 1);
            }
            return false;
        }

        public void SetQuestionImage(ImageActionItem imgItem)
        {
			if (m_question!=null)
			{
                if (HasQuestionImage())
                {
                    for (int i = m_question.QuestionActions.Count()-1 ; i >= 0; i--)
                        if (m_question.QuestionActions[i] is ImageActionItem)
                            AppHandler.LibManager.DeleteQuestionAction(m_question,i);
                }

                if (imgItem != null)
                {
                    AppHandler.LibManager.AddQuestionAction(m_question, imgItem);
                    AppHandler.LibManager.SetQuestion(m_sPath, m_question, m_questionId);
                    AppHandler.LibManager.SetModified(m_sPath);
                }
                m_fnShowPage(this,false);
            }
        }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


        public void AddQuestion()
        {
            treAnswers.AppendNode(new object[] { " " }, -1);
        }

        public void DeleteQuestion()
        {
            if (treAnswers.FocusedNode != null && MessageBox.Show("Delete current Answer?", "Answers", MessageBoxButtons.YesNo) == DialogResult.Yes)
                treAnswers.DeleteNode(treAnswers.FocusedNode);
        }

        public bool SaveQuestion()
        {
            TreeListVerifyAnswerOperation op1 = new TreeListVerifyAnswerOperation();
            treAnswers.NodesIterator.DoOperation(op1);

            if (op1.ErrNodeId >= 0)
            {
                MessageBox.Show("No empty answers allowed!", "Answers");
                return false;
            }

            TreeListGetCorrectAnswerOperation op2 = new TreeListGetCorrectAnswerOperation(m_question, treAnswers.Nodes.Count);
            treAnswers.NodesIterator.DoOperation(op2);
            QuestionItem que = AppHandler.LibManager.GetQuestion(m_sPath, m_questionId);
            if (que != null)
            {
                QuestionItem newQue = (QuestionItem)que.Clone();
                newQue.correctAnswerMask = m_question.correctAnswerMask;
                newQue.question = textBox1.Text;
                newQue.Answers = m_question.Answers;
                newQue.useForExaming = chkUserForExamin.Checked;
                newQue.useForTesting = chkUserForTesting.Checked;
                AppHandler.LibManager.SetQuestion(m_sPath, newQue, m_questionId);
                m_question = AppHandler.LibManager.GetQuestion(m_sPath, m_questionId);
                m_fnShowPage(this,false);
            }
            return true;
        }



		#region Component Designer generated code
		/// <summary> 
		/// required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.treAnswers = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkUserForExamin = new System.Windows.Forms.CheckBox();
            this.chkUserForTesting = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.treAnswers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(246, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Template:";
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(305, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(222, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(268, 98);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "textBox1";
            // 
            // treAnswers
            // 
            this.treAnswers.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treAnswers.Dock = System.Windows.Forms.DockStyle.Left;
            this.treAnswers.Location = new System.Drawing.Point(0, 0);
            this.treAnswers.Name = "treAnswers";
            this.treAnswers.OptionsView.ShowPreview = true;
            this.treAnswers.OptionsView.ShowRoot = false;
            this.treAnswers.OptionsView.ShowVertLines = false;
            this.treAnswers.Size = new System.Drawing.Size(237, 188);
            this.treAnswers.StateImageList = this.imageList1;
            this.treAnswers.TabIndex = 8;
            this.treAnswers.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treAnswers_GetStateImage);
            this.treAnswers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treAnswers_KeyDown);
            this.treAnswers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treAnswers_MouseDown);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Answers";
            this.treeListColumn1.FieldName = "Answers";
            this.treeListColumn1.MinWidth = 34;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 111;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(247, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 118);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Question";
            // 
            // chkUserForExamin
            // 
            this.chkUserForExamin.AutoSize = true;
            this.chkUserForExamin.Location = new System.Drawing.Point(253, 167);
            this.chkUserForExamin.Name = "chkUserForExamin";
            this.chkUserForExamin.Size = new System.Drawing.Size(87, 17);
            this.chkUserForExamin.TabIndex = 10;
            this.chkUserForExamin.Text = "Übungsfrage";
            this.chkUserForExamin.UseVisualStyleBackColor = true;
            // 
            // chkUserForTesting
            // 
            this.chkUserForTesting.AutoSize = true;
            this.chkUserForTesting.Location = new System.Drawing.Point(407, 167);
            this.chkUserForTesting.Name = "chkUserForTesting";
            this.chkUserForTesting.Size = new System.Drawing.Size(71, 17);
            this.chkUserForTesting.TabIndex = 11;
            this.chkUserForTesting.Text = "Testfrage";
            this.chkUserForTesting.UseVisualStyleBackColor = true;
            // 
            // QuestionEditTabPage
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.chkUserForTesting);
            this.Controls.Add(this.chkUserForExamin);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treAnswers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "QuestionEditTabPage";
            this.Size = new System.Drawing.Size(536, 188);
            ((System.ComponentModel.ISupportInitialize)(this.treAnswers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

       
        static public CheckState GetCheckState(object obj) 
		{
			if(obj != null) return (CheckState)obj;
			return CheckState.Unchecked;
		}

		private void SetCheckedNode(TreeListNode node) 
		{
			CheckState check = GetCheckState(node.Tag);

			if( check == CheckState.Indeterminate || 
				check == CheckState.Unchecked) 
				check = CheckState.Checked;
			else 
				check = CheckState.Unchecked;

			treAnswers.BeginUpdate();
			node.Tag = check;
			treAnswers.EndUpdate();
		}

		private void treAnswers_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e) 
		{
			CheckState check = GetCheckState(e.Node.Tag);
			if(check == CheckState.Unchecked)
				e.NodeImageIndex = 0;
			else if(check == CheckState.Checked)
				e.NodeImageIndex = 1;
			else e.NodeImageIndex = 2;
		}

		private void treAnswers_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) 
		{
			if(e.KeyData == Keys.Space) 
				SetCheckedNode(treAnswers.FocusedNode);
		}

		private void treAnswers_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) 
		{
			if(e.Button == MouseButtons.Left) 
			{
				DevExpress.XtraTreeList.TreeListHitInfo hInfo = treAnswers.CalcHitInfo(new Point(e.X, e.Y));
				if(hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.StateImage) 
					SetCheckedNode(hInfo.Node);
			}
		}
	}

	public class TreeListGetCorrectAnswerOperation : DevExpress.XtraTreeList.Nodes.Operations.TreeListOperation 
	{
		private QuestionItem m_question;
		public TreeListGetCorrectAnswerOperation(QuestionItem question,int iAnswers) 
		{
			this.m_question = question;
			this.m_question.correctAnswerMask=0;
			this.m_question.Answers=null;
			if (iAnswers>0)
				this.m_question.Answers = new string[iAnswers];
		}

		// incrementing the counter if the node's value exceeds the limit
		public override void Execute(TreeListNode node) 
		{
			if (QuestionEditTabPage.GetCheckState(node.Tag)== CheckState.Checked)
				m_question.correctAnswerMask += (1 << node.Id);
			m_question.Answers[node.Id]=(string)node.GetValue(0);
		}
	}

	public class TreeListVerifyAnswerOperation : DevExpress.XtraTreeList.Nodes.Operations.TreeListOperation 
	{
		private int m_iErrNodeId=-1;

		// incrementing the counter if the node's value exceeds the limit
		public override void Execute(TreeListNode node) 
		{
			if (m_iErrNodeId<0)
			{
				string strAnswer = (string)node.GetValue(0);
				if (strAnswer!=null)
				{
					strAnswer=strAnswer.Trim();
					if (strAnswer.Length==0)
						m_iErrNodeId=node.Id;
				}
				else
					m_iErrNodeId=node.Id;
			}
		}

		public int ErrNodeId
		{
			get {return m_iErrNodeId;}
		}
	}
}
