using System;
using System.Drawing;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for XTestTreeView.
	/// </summary>
    public partial class XTestTreeView : DevExpress.XtraTreeList.TreeList
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private string m_mapTitle="";
		private int m_successLevel=50;
        private AppHandler AppHandler = Program.AppHandler;

		public XTestTreeView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			InitTree();

		}

		private void InitTree() 
		{
			string sUser=AppHandler.LanguageHandler.GetText("FORMS","User","Benutzer");
            string sTestName = AppHandler.LanguageHandler.GetText("FORMS", "TestName", "TestName");
            string sStartTime = AppHandler.LanguageHandler.GetText("FORMS", "StartTime", "Beginn");
			string sEndTime=AppHandler.LanguageHandler.GetText("FORMS","EndTime","Ende");
			string sPercRight=AppHandler.LanguageHandler.GetText("FORMS","Percent_right","% - Richtig");
			string sResult=AppHandler.LanguageHandler.GetText("FORMS","Result","Ergebnis");

			CreateColumn(sUser,"User",0,DevExpress.Utils.FormatType.None,null);
            CreateColumn(sTestName, "TestName", 1, DevExpress.Utils.FormatType.None, null);
            CreateColumn(sStartTime, "StartTime", 2, DevExpress.Utils.FormatType.DateTime, "f");
			CreateColumn(sEndTime, "EndTime", 3, DevExpress.Utils.FormatType.DateTime, "f");
			CreateColumn(sPercRight, "PercRight", 4, DevExpress.Utils.FormatType.Numeric, "n");
			CreateColumn(sResult, "Result", 5, DevExpress.Utils.FormatType.None,null);

			this.KeyFieldName = "Id";
			this.ParentFieldName = "ParentId";

			BestFitColumns();
		}

		private void CreateColumn(string caption, string field, int visibleindex, DevExpress.Utils.FormatType formatType, string formatString) 
		{
			DevExpress.XtraTreeList.Columns.TreeListColumn col = Columns.Add();

			col.Caption = caption;
			col.FieldName = field;
			col.VisibleIndex = visibleindex;
			col.Format.FormatType = formatType;
			if (formatString!=null)
				col.Format.FormatString = formatString;
		}

		public void FillData(string mapTitle)
		{
			ClearNodes();
			DataSource=null;

            bool randomChoose = false;
            int questionCnt = 0;
            int trialCnt = 0;
            int successLevel = 0;
            TestItem ti = AppHandler.MapManager.GetTest(mapTitle, 0);
            if (ti != null)
            {
                randomChoose = ti.randomChoose;
                questionCnt = ti.questionCount;
                trialCnt = ti.trialCount;
                successLevel = ti.successLevel;
            }

			bool isAdmin=false;
			bool isTeacher=false;
			AppHandler.UserManager.GetUserRights(AppHandler.MainForm.ActualUserName,ref isAdmin,ref isTeacher);

			TestResultItemCollection aTestResults=new TestResultItemCollection();
			if (isTeacher)
				AppHandler.TestResultManager.Find(ref aTestResults,mapTitle);
			else
				AppHandler.TestResultManager.Find(ref aTestResults,AppHandler.MainForm.ActualUserName,mapTitle);

			string sSuccessfull=AppHandler.LanguageHandler.GetText("FORMS","Successfully","Bestanden");
			string sNotSuccessfull=AppHandler.LanguageHandler.GetText("FORMS","Not_successfully","Nicht bestanden");
			string sNotConceded=AppHandler.LanguageHandler.GetText("FORMS","Not_handed_over","Nicht abgegeben");

			if (aTestResults.Count>0)
			{
				TestTreeRecord[] records = new TestTreeRecord[aTestResults.Count];
				for(int i=0;i<aTestResults.Count;++i)
				{
					TestResultItem item = aTestResults.Item(i);

                    string strFullName = "";
                    string strPwd = "";
                    int iImgId=0;
                    AppHandler.UserManager.GetUserInfo(item.userName, ref strPwd,ref strFullName, ref iImgId);
                    string strUserInfo = String.Format("{0}({1})", strFullName, item.userName);

					if (item.startTime!=item.endTime)
                        records.SetValue(new TestTreeRecord(i + 1, 0, strUserInfo,item.testName, item.startTime, item.endTime, item.percRight,
							(item.percRight>(double)m_successLevel) ? sSuccessfull: sNotSuccessfull),i);
					else
                        records.SetValue(new TestTreeRecord(i + 1, 0, strUserInfo,item.testName,item.startTime, item.endTime, item.percRight,
							sNotConceded),i);
				}

				DataSource = records;
			}

			m_mapTitle=mapTitle;
		}

		private void TestTreeView_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
		{
			if(e.Column.FieldName=="PercRight" && e.CellValue != null) 
			{
				bool isFocusedCell = (e.Column == FocusedColumn && e.Node == FocusedNode);
				Rectangle r = e.Bounds;
				double val=(double) e.CellValue;

				Brush	brush=null;

				if (val<=((double)m_successLevel))
					brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.IndianRed,Color.DarkRed,0.0);
				else
					brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.LightGreen,Color.DarkGreen,0.0);

				r.Inflate(-2, -1);

				r.Width = (int)(double)(r.Width*val/100);

				e.Graphics.FillRectangle(brush, r);
				e.Appearance.DrawString(e.Cache,e.CellText,r);
				//e.Style.DrawString(e.Graphics, e.CellText, r);

				if(isFocusedCell)
					DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);

				e.Handled = true;
			}
		}
	}
	public class TestTreeRecord
	{
		private int m_parentId;
		private int m_id;
		private string m_user;
	    private string m_testName;
		private DateTime m_startTime;
		private DateTime m_endTime;
		private double m_percRight; 
		private string m_result;
        
		public TestTreeRecord(int id,int parentId,string user,string testName,DateTime startTime,DateTime endTime,
			double percRight,string result) 
		{
			m_id=id;
			m_parentId=parentId;
			m_user=user;
		    m_testName = testName;
			m_startTime=startTime;
			m_endTime=endTime;
			m_percRight=percRight;
			m_result=result;
		}

		public int ParentId
		{
			get{return m_parentId;}
		}

		public int Id
		{
			get{return m_id;}
		}

		public string User
		{
			get { return m_user; }
			set { m_user = value; }
		}

	    public string TestName
	    {
            get { return m_testName; }
            set { m_testName = value; }
	    }

		public DateTime StartTime
		{ 
			get  { return m_startTime; }
			set { m_startTime = value; }
		}

		public DateTime EndTime
		{ 
			get  { return m_endTime; }
			set { m_endTime = value; }
		}

		public double PercRight
		{
			get { return m_percRight; }
			set { m_percRight = value; }
		}

		public string Result
		{
			get { return m_result; }
			set { m_result = value; }
		}
	}
}

