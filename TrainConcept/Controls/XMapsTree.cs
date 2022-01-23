using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes.Operations;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Libraries;


namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for XMapsTree.
	/// </summary>
    public partial class XMapsTree : DevExpress.XtraEditors.XtraUserControl
	{
        private LearnmapTreeRecord[] m_aRecords;
		private bool m_bIsCheckable=false;
        private ResourceHandler rh=null;
        private AppHandler AppHandler = Program.AppHandler;

        public bool IsCheckable
		{ 
			get { return m_bIsCheckable;}
			set { m_bIsCheckable=value; }
		}

        public LearnmapTreeRecord[] Records
        {
            get { return m_aRecords; }
        }

        public DevExpress.XtraTreeList.TreeList TreeList
        {
            get { return this.treeList1; }
        }

        public event OnSelectionChangedHandler SelectionChangedEventHandler;
		public event OnSelectionChangedHandler OnSelectionChangedEvent
		{
			add { SelectionChangedEventHandler += value;}
			remove { SelectionChangedEventHandler -= value;}
		}

		public XMapsTree()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            string resName = AppHandler.ResourceName;
            System.Reflection.Assembly ass = GetType().Assembly;
            if (resName != null && ass != null)
            {
                rh = new ResourceHandler(AppHandler.ResourceName, GetType().Assembly);
                this.imageList1.Images.Add(rh.GetBitmap("learnmap"));
                this.imageList1.Images.Add(rh.GetBitmap("lib"));
                this.imageList1.Images.Add(rh.GetBitmap("book_closed"));
                this.imageList1.Images.Add(rh.GetBitmap("chapter_closed"));
                this.imageList1.Images.Add(rh.GetBitmap("point"));
                this.imageList2.Images.Add(rh.GetBitmap("unchecked"));
                this.imageList2.Images.Add(rh.GetBitmap("checked"));
            }
		}

		private void InitData() 
		{
			treeList1.KeyFieldName = "Id";
			treeList1.ParentFieldName = "ParentId";

			// Gesamtbearbeitungsanzahl bestimmen
            int iRecordCnt = 0;
            for (int i = 0; i < AppHandler.MapManager.GetMapCnt(); ++i)
            {
                ++iRecordCnt; // auch leere mappen brauchen zumindest einen Knoten
				string[] aWorkings=null;
                if (AppHandler.MapManager.GetWorkings(i, ref aWorkings))
                    iRecordCnt += aWorkings.Length;
            }

            int iRecId = 0;
            m_aRecords = new LearnmapTreeRecord[iRecordCnt*4]; // *4 -> weil Platz für Knoten lib+book+chp+point
            for (int i = 0; i < AppHandler.MapManager.GetMapCnt(); ++i)
			{
				string title="";
				string strClasses="";
				string strUsers="";
                string strColor="";
				string[] aWorkings=null;
				string[] aUserNames=null;
                bool bIsClassMap=false;
                AppHandler.MapManager.GetTitle(i, out title);
                AppHandler.MapManager.GetWorkings(i, ref aWorkings);
                AppHandler.MapManager.GetUsers(i, ref aUserNames);
                AppHandler.MapManager.GetColor(i, out strColor);
                AppHandler.MapManager.GetUsage(i,ref bIsClassMap);

                if (!bIsClassMap)
                {
                    if (aUserNames != null && aUserNames.Length > 0)
                    {
                        strUsers = aUserNames[0];
                        if (aUserNames.Length > 1)
                            for (int j = 1; j < aUserNames.Length; ++j)
                                strUsers += ',' + aUserNames[j];
                    }
                }
                else
                {
                    string[] astrClassNames;
                    int iFound = 0;
                    AppHandler.ClassManager.GetClassNames(out astrClassNames);
                    if (astrClassNames!=null)
                        foreach (string strClass in astrClassNames)
                            if (AppHandler.ClassManager.HasLearnmap(strClass, title))
                            {
                                if (iFound == 0)
                                    strClasses = strClass;
                                else
                                    strClasses += ',' + strClass;
                                ++iFound;
                            }
                }

                if (strColor.Length==0)
                    strColor="255,255,255";

                int iMapId = iRecId;
                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, -1, title, strUsers, strClasses,LearnmapTreeRecord.RecordType.Map,strColor), iRecId++);

                if (aWorkings != null && aWorkings.Length > 0)
				{
					//strWorkouts = aTitles[aTitles.Length-1];
					if (aWorkings.Length>0)
					{
                        string[] aTitles;
                        Utilities.SplitPath(aWorkings[0], out aTitles);
                        int libId = iRecId;
                        m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, iMapId, aTitles[0], "", "", LearnmapTreeRecord.RecordType.Library), iRecId++);
                        int bookId = iRecId;
                        m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, libId, aTitles[1], "", "", LearnmapTreeRecord.RecordType.Book), iRecId++);
                        int chpId = iRecId;
                        m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, bookId, aTitles[2], "", "", LearnmapTreeRecord.RecordType.Chapter), iRecId++);
                        m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, chpId, aTitles[3], "", "", LearnmapTreeRecord.RecordType.Point), iRecId++);

                        for (int j = 1; j < aWorkings.Length; ++j)
						{
                            Utilities.SplitPath(aWorkings[j], out aTitles);
                            if (m_aRecords[libId].Content != aTitles[0])
                            {
                                libId = iRecId;
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, iMapId, aTitles[0], "", "", LearnmapTreeRecord.RecordType.Library), iRecId++);
                                bookId = iRecId;
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, libId, aTitles[1], "", "", LearnmapTreeRecord.RecordType.Book), iRecId++);
                                chpId = iRecId;
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, bookId, aTitles[2], "", "", LearnmapTreeRecord.RecordType.Chapter), iRecId++);
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, chpId, aTitles[3], "", "", LearnmapTreeRecord.RecordType.Point), iRecId++);
                            }
                            else if (m_aRecords[bookId].Content != aTitles[1])
                            {
                                bookId = iRecId;
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, libId, aTitles[1], "", "", LearnmapTreeRecord.RecordType.Book), iRecId++);
                                chpId = iRecId;
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, bookId, aTitles[2], "", "", LearnmapTreeRecord.RecordType.Chapter), iRecId++);
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, chpId, aTitles[3], "", "", LearnmapTreeRecord.RecordType.Point), iRecId++);
                            }
                            else if (m_aRecords[chpId].Content != aTitles[2])
                            {
                                chpId = iRecId;
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, bookId, aTitles[2], "", "", LearnmapTreeRecord.RecordType.Chapter), iRecId++);
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, chpId, aTitles[3], "", "", LearnmapTreeRecord.RecordType.Point), iRecId++);
                            }
                            else
                                m_aRecords.SetValue(new LearnmapTreeRecord(iRecId, chpId, aTitles[3], "", "", LearnmapTreeRecord.RecordType.Point), iRecId++);
						}
					}
				}
			}
            Array.Resize(ref m_aRecords, iRecId);
		}

		public void UpdateData()
		{
            this.repositoryItemCheckedComboBoxEdit1.Items.Clear();
            string[] aUserNames;
            AppHandler.UserManager.GetUserNames(out aUserNames);
            foreach (string u in aUserNames)
                if (u!="admin")
                    this.repositoryItemCheckedComboBoxEdit1.Items.Add(new CheckedListBoxItem(u, u));

            this.repositoryItemCheckedComboBoxEdit2.Items.Clear();
            string[] aClassNames;
            AppHandler.ClassManager.GetClassNames(out aClassNames);
            if (aClassNames!=null)
                foreach (string c in aClassNames)
                    this.repositoryItemCheckedComboBoxEdit2.Items.Add(new CheckedListBoxItem(c, c));

			InitData();
			treeList1.ClearNodes();
			treeList1.DataSource = m_aRecords;
            //treeList1.PopulateColumns();
            treeList1.BestFitColumns();
            treeList1.CollapseAll();
        }

		public void SetUser(string userName)
		{
			treeList1.BeginUpdate();
			DevExpress.XtraTreeList.Nodes.Operations.TreeListNodesIterator iter = treeList1.NodesIterator;
			TreeListSetUserOperation operation= new TreeListSetUserOperation(userName);
			iter.DoOperation(operation);
			treeList1.EndUpdate();
		}

        public void SetAllChecked()
        {
            treeList1.BeginUpdate();
            TreeListNodesIterator iter = treeList1.NodesIterator;
            iter.DoOperation(new TreeListCheckAllOperation());
            treeList1.EndUpdate();
        }

		public int GetCheckedMaps(ref string[] aMaps)
		{
			DevExpress.XtraTreeList.Nodes.Operations.TreeListNodesIterator iter = treeList1.NodesIterator;
			TreeListGetCheckOperation operation= new TreeListGetCheckOperation(CheckState.Checked);
			iter.DoOperation(operation);
			int[] aiIds;
			operation.GetResult(out aiIds);
			if (aiIds.Length>0)
			{
				aMaps = new string[aiIds.Length];
				for(int i=0;i<aiIds.Length;++i)
					aMaps[i] = m_aRecords[aiIds[i]].Content;
			}
			return aiIds.Length;
		}

		public int GetUnCheckedMaps(ref string[] aMaps)
		{
			DevExpress.XtraTreeList.Nodes.Operations.TreeListNodesIterator iter = treeList1.NodesIterator;
			TreeListGetCheckOperation operation= new TreeListGetCheckOperation(CheckState.Unchecked);
			iter.DoOperation(operation);
			int[] aiIds;
			operation.GetResult(out aiIds);
			if (aiIds.Length>0)
			{
				aMaps = new string[aiIds.Length];
				for(int i=0;i<aiIds.Length;++i)
					aMaps[i] = m_aRecords[aiIds[i]].Content;
			}
			return aiIds.Length;
		}

		private CheckState GetCheckState(object obj) 
		{
			if(obj != null) return (CheckState)obj;
			return CheckState.Unchecked;
		}

		private void SetCheckedNode(DevExpress.XtraTreeList.Nodes.TreeListNode node,bool isChecked) 
		{
			CheckState check = GetCheckState(node.Tag);
			if(check == CheckState.Indeterminate || check == CheckState.Unchecked) 
				check = CheckState.Checked;
			else 
				check = CheckState.Unchecked;

			treeList1.BeginUpdate();
			node.Tag = check;
			treeList1.EndUpdate();
		}

        public int GetSelected(out string[] aItems)
        {
            DevExpress.XtraTreeList.TreeListMultiSelection sel = this.treeList1.Selection;
            if (sel.Count > 0)
            {
                aItems = new string[sel.Count];
                for (int i = 0; i < sel.Count; ++i)
                    aItems[i] = m_aRecords[sel[i].Id].Content;
                return aItems.Length;
            }
            aItems = null;
            return 0;
        }


		private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
		{
            if (e.Column.FieldName == "Content" && e.Node.ParentNode==null && e.CellValue != null) 
			{
				bool isFocusedCell = (e.Column == treeList1.FocusedColumn && e.Node == treeList1.FocusedNode);
				Rectangle r = e.Bounds;
				string sName=(string) e.CellValue;

                if (m_aRecords[e.Node.Id].Color != null)
                {
                    Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, m_aRecords[e.Node.Id].Color.Value,0.0);
				    r.Inflate(-2, -1);

				    e.Graphics.FillRectangle(brush, r);
				    e.Appearance.DrawString(e.Cache, e.CellText, r);

				    if(isFocusedCell)
                        DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);

                    e.Handled = true;
                }
			}
		}

		private void treeList1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left) 
			{
				DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
				if(hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.StateImage) 
					SetCheckedNode(hInfo.Node,true);
			}
		}

		private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
		{
			e.NodeImageIndex = -1;
			if (m_bIsCheckable && m_aRecords!=null && m_aRecords.Length>e.Node.Id)
			{
				LearnmapTreeRecord r = (LearnmapTreeRecord) m_aRecords[e.Node.Id];
				CheckState check = GetCheckState(e.Node.Tag);
				if(check == CheckState.Checked)
					e.NodeImageIndex = 1;
				else
					e.NodeImageIndex = 0;
			}
		}

        private void treeList1_SelectionChanged(object sender, EventArgs e)
        {
            DevExpress.XtraTreeList.TreeListMultiSelection sel = this.treeList1.Selection;
            if (sel != null && SelectionChangedEventHandler != null)
                SelectionChangedEventHandler(this, new EventArgs());
        }


        static public Nullable<System.Drawing.Color> ToColor(string strColor)
        {
            if (strColor.Length > 2)
            {
                string[] aRGB = strColor.Split(new char[] { ',' }, 3);
                if (aRGB.Length == 3)
                    return Color.FromArgb(Convert.ToInt32(aRGB[0]), Convert.ToInt32(aRGB[1]), Convert.ToInt32(aRGB[2]));
            }
            return null;
        }
	}


    public class TreeListGetCheckOperation : TreeListOperation
    {
        private ArrayList m_aMaps = new ArrayList();
        private CheckState m_tCheck = CheckState.Indeterminate;
        public TreeListGetCheckOperation(CheckState tCheck)
        {
            m_tCheck = tCheck;
        }

        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (node.Tag != null && ((CheckState)node.Tag) == m_tCheck)
                m_aMaps.Add(node.Id);
        }

        public int GetResult(out int[] aItems)
        {
            aItems = new int[m_aMaps.Count];
            aItems = (int[])m_aMaps.ToArray(typeof(int));
            return aItems.Length;
        }
    };

    public class TreeListSetUserOperation : TreeListOperation
    {
        private string m_userName = "";

        public TreeListSetUserOperation(string userName)
        {
            m_userName = userName;
        }

        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (Program.AppHandler.MapManager.HasUser(node.Id, m_userName))
                node.Tag = CheckState.Checked;
            else
                node.Tag = CheckState.Unchecked;
        }
    };

    public class TreeListCheckAllOperation : TreeListOperation
    {
        public TreeListCheckAllOperation() { }
        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            node.Checked = true;
        }
    };


	public class LearnmapTreeRecord
	{
        public enum RecordType { Empty, Map, Library, Book, Chapter, Point};

		private int m_parentId;
		private int m_id;
		private string m_contentName;
		private string m_userList;
		private string m_classList;
        private RecordType m_type;
        private Nullable<System.Drawing.Color> m_color;
        
		public LearnmapTreeRecord(int id,int parentId,string contentName,string userList,string classList,RecordType type,string color="") 
		{
			m_id=id;
			m_parentId=parentId;
            
            m_color = XMapsTree.ToColor(color);
			m_contentName=contentName;
			m_userList=userList;
			m_classList=classList;
            m_type = type;
		}

		public LearnmapTreeRecord(int id) 
		{
			m_id=id;
			m_parentId=0;
            m_color=null;
            m_contentName = "";
            m_userList = "";
            m_classList = "";
            m_type = RecordType.Empty;
		}

		public int ParentId
		{
			get{return m_parentId;}
		}

		public int Id
		{
			get{return m_id;}
		}

		public string Content
		{
			get { return m_contentName; }
		}
		
		public string UserList
		{ 
			get { return m_userList; }
			set { m_userList= value; }
		}

        public string ClassList
        {
            get { return m_classList; }
            set { m_classList = value; }
        }
        
        public Nullable<System.Drawing.Color> Color
        {
            get{ return  (m_color!=null) ? m_color:System.Drawing.Color.Transparent; }
            set { m_color = value; }
        }

        public int ImageIndex 
		{
            get
            {
                switch (m_type)
                {
                    case RecordType.Map:        return 0;
                    case RecordType.Library:    return 1;
                    case RecordType.Book:       return 2;
                    case RecordType.Chapter:    return 3;
                    case RecordType.Point:      return 4;
                    default: return 0;
                }
            }
        }
	}
}

