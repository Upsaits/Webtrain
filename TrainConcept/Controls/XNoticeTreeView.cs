using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
    public partial class XNoticeTreeView : DevExpress.XtraTreeList.TreeList
    {
        private string m_mapTitle;
        private AppHandler AppHandler = Program.AppHandler;

        public XNoticeTreeView()
        {
            InitializeComponent();
            InitTree();
        }

        public XNoticeTreeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            InitTree();
        }

        private void InitTree()
        {
            string sUser = AppHandler.LanguageHandler.GetText("FORMS", "User", "Benutzer");
            string sTitle = AppHandler.LanguageHandler.GetText("FORMS", "Title", "Notiz-Titel");
            string sWork = AppHandler.LanguageHandler.GetText("FORMS", "ContentPath", "Inhaltspfad");
            string sWorkedOutState = AppHandler.LanguageHandler.GetText("FORMS", "WorkedOutState", "Ausarbeitungszustand");

            CreateColumn(sUser, "User", 0, DevExpress.Utils.FormatType.None, null);
            CreateColumn(sTitle, "Title", 1, DevExpress.Utils.FormatType.None, null);
            CreateColumn(sWork, "Work", 2, DevExpress.Utils.FormatType.None, null);
            CreateColumn(sWorkedOutState, "WorkedOutState", 3, DevExpress.Utils.FormatType.Numeric, "n");

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
            if (formatString != null)
                col.Format.FormatString = formatString;
        }

        public void FillData(string mapTitle)
        {
            ClearNodes();
            DataSource = null;

            /*
            bool isAdmin = false;
            bool isTeacher = false;
            AppHandler.UserManager.GetUserRights(AppHandler.MainForm.ActualUserName, ref isAdmin, ref isTeacher);*/

            var lNoticeTreeItems = new List<NoticeTreeRecord>();
            int t=0;
            string[] aWorkings=null;
            if (AppHandler.MapManager.GetWorkings(mapTitle,ref aWorkings))
                foreach(var w in aWorkings)
                {
                    var nic = new NoticeItemCollection();
                    int iCnt=0;
                    if (AppHandler.IsServer)
                        iCnt = AppHandler.NoticeManager.Find(ref nic,"",w,-1);
                    else
                        iCnt = AppHandler.NoticeManager.Find(ref nic,AppHandler.MainForm.ActualUserName, w, -1);

                    if (iCnt > 0)
                        foreach (var n in nic)
                            lNoticeTreeItems.Add(new NoticeTreeRecord(++t, 0, n.userName, n.title, n.contentPath, n.workedOutState));
                }

            DataSource = lNoticeTreeItems.ToArray();

            BestFitColumns();
            m_mapTitle = mapTitle;
        }

        public int GetSelectedNotices(out NoticeTreeRecord[] aNotices)
        {
            var sel = this.Selection;
            if (sel.Count > 0)
            {
                aNotices = new NoticeTreeRecord[sel.Count];
                for (int i = 0; i < sel.Count; ++i)
                    aNotices[i] = (this.DataSource as NoticeTreeRecord[])[sel[i].Id];
                return aNotices.Length;
            }
            aNotices = null;
            return 0;
        }
        
        private void XNoticeTreeView_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Column.FieldName == "WorkedOutState" && e.CellValue != null)
            {
                bool isFocusedCell = (e.Column == FocusedColumn && e.Node == FocusedNode);
                Rectangle r = e.Bounds;
                int val = (int)e.CellValue;
                string strText = "";
                Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, Color.White, 0.0);

                if (val == 0)
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, Color.Gray, 0.0);
                    strText = "nicht beurteilt";
                }
                else if (val >= 1 && val<=5)
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, Color.DarkGreen, 0.0);
                    strText = "Richtig";
                }
                else if (val == 6)
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, Color.DarkRed, 0.0);
                    strText = "Falsch";
                }

                r.Inflate(-2, -1);

                //r.Width = (int)(double)(r.Width * val / 100);

                e.Graphics.FillRectangle(brush, r);
                StringFormat format = new StringFormat(StringFormatFlags.NoWrap);
                format.Alignment = StringAlignment.Center;
                e.Appearance.DrawString(e.Cache, strText,r, new Font("Arial Black",9.0f),format);

                if (isFocusedCell)
                    DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);

                e.Handled = true;
            }
        }


    }

    public class NoticeTreeRecord
    {
        private int m_parentId;
        private int m_id;
        private string m_user;
        private string m_title;
        private string m_work;
        private int m_iWorkedOutState;

        public NoticeTreeRecord(int id, int parentId, string user, string title, string work, int iWorkedOutState)
        {
            m_id = id;
            m_parentId = parentId;
            m_user = user;
            m_title = title;
            m_work = work;
            m_iWorkedOutState = iWorkedOutState;
        }

        public int ParentId
        {
            get { return m_parentId; }
        }

        public int Id
        {
            get { return m_id; }
        }

        public string User
        {
            get { return m_user; }
            set { m_user = value; }
        }

        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        public string Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        public int WorkedOutState
        {
            get { return m_iWorkedOutState; }
            set { m_iWorkedOutState = value; }
        }
    }

}
