using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes.Operations;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.UtilityLibrary;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for XUsersTree.
	/// </summary>
    /// 

	#region Delegates
	public delegate void OnSelectionChangedHandler(object sender,System.EventArgs e);
	public delegate void OnSelectionDblClkEventHandler(object sender,System.EventArgs e);
	#endregion

    public partial class XUsersTree : DevExpress.XtraEditors.XtraUserControl
	{
        public enum ViewType { EditView, OnlineView, ClassView };
        
        private UserEditTreeRecord[] m_aRecords = null;
        private bool m_bIsCheckable = false;
        private string[] m_aUsers = null;
        private ViewType m_tView = ViewType.EditView;
        private string m_className="";
        private AppHandler AppHandler = Program.AppHandler;

        #region Properties
        public bool IsCheckable
        {
            get { return m_bIsCheckable; }
            set { m_bIsCheckable = value; }
        }

        public string[] UserList
        {
            get { return m_aUsers; }
            set 
            {
                if (value != null)
                {
                    m_aUsers = value;
                    UpdateData();
                }
            }
        }

        public string ClassName
        {
            get { return m_className; }
            set { m_className = value; }
        }

        public ViewType Type
        {
            get { return m_tView; }
            set 
            { 
                m_tView = value;
                switch (m_tView)
                {
                    case ViewType.EditView:
                        this.colIPAddress.Visible = false;
                        this.colLoginTime.Visible = false;
                        this.colOnlineStatistics.Visible = false;
                        this.colMapsState.Visible = false;
                        break;
                    case ViewType.OnlineView:
                        this.colIPAddress.Visible = true;
                        this.colLoginTime.Visible = true;
                        this.colOnlineStatistics.Visible = true;
                        this.colMapsState.Visible = false;
                        break;
                    case ViewType.ClassView:
                        this.colIPAddress.Visible = false;
                        this.colLoginTime.Visible = false;
                        this.colOnlineStatistics.Visible = false;
                        this.colMapsState.Visible = true;
                        break;
                }
            }
        }
        #endregion

        #region Events

        private event KeyEventHandler m_evtKeyDown;
        public event KeyEventHandler TreeKeyDown
        {
            add
            {
                m_evtKeyDown += value;
            }
            remove
            {
                m_evtKeyDown -= value;
            }
        }

        public class XUsersTreeEventArgs : System.EventArgs
        {
            private DevExpress.XtraTreeList.TreeListHitInfo hitInfo;
            public DevExpress.XtraTreeList.TreeListHitInfo HitInfo { get { return hitInfo; }}
            public XUsersTreeEventArgs(DevExpress.XtraTreeList.TreeListHitInfo _hitInfo) 
            {
                this.hitInfo = _hitInfo;
            }
        }

		public event OnSelectionChangedHandler SelectionChangedEventHandler;
		public event OnSelectionChangedHandler OnSelectionChangedEvent
		{
			add { SelectionChangedEventHandler += value;}
			remove { SelectionChangedEventHandler -= value;}
		}

        public event OnSelectionDblClkEventHandler SelectionDblClkEventHandler;
        public event OnSelectionDblClkEventHandler OnSelectionDblClkEvent
        {
            add { SelectionDblClkEventHandler += value; }
            remove { SelectionDblClkEventHandler -= value; }
        }
		#endregion

		public XUsersTree()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

        public XUsersTree(string [] aCheckedUsers)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            UpdateData();
            SetCheckedUsers(aCheckedUsers);
        }

        public XUsersTree(string[] aUsers,string[] aCheckedUsers=null)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            m_aUsers = aUsers;
            UpdateData();
            if (aCheckedUsers!=null)
                SetCheckedUsers(aCheckedUsers);
        }

		private void InitData() 
		{
			treeList1.KeyFieldName = "Id";
			treeList1.ParentFieldName = "ParentId";

            if (m_aUsers == null)
                m_aRecords = new UserEditTreeRecord[AppHandler.UserManager.GetUserNames(out m_aUsers)];
            else if (m_aUsers.Length > 0)
                m_aRecords = new UserEditTreeRecord[m_aUsers.Length];
            else
                m_aRecords = null;

            for (int i = 0; i < m_aUsers.Length; ++i)
			{
				string strPassword="";
				string strFullName="";
				bool bIsAdmin=false;
				bool bIsTeacher=false;
                int iImgId = 0;

                AppHandler.UserManager.GetUserInfo(m_aUsers[i], ref strPassword, ref strFullName, ref iImgId);
                AppHandler.UserManager.GetUserRights(m_aUsers[i], ref bIsAdmin, ref bIsTeacher);

                //int iAvatarId = AppHandler.UserAvatars.GetId(m_aUsers[i]);
                int iAvatarId = imageList1.Images.IndexOfKey(m_aUsers[i]);

                UserEditTreeRecord rec = new UserEditTreeRecord(i + 1, 0, iAvatarId, m_aUsers[i], strFullName, bIsTeacher ? "Lehrer" : "Schüler");
                if (m_tView==ViewType.ClassView)
                    rec.MapsProgress = GetMapsProgress(m_aUsers[i]);
                else if (m_tView == ViewType.OnlineView)
                    rec.MapsProgress = (int)AppHandler.UserProgressInfoMgr.GetUserProgressTime(m_aUsers[i]).TotalMinutes;
                m_aRecords.SetValue(rec, i);
			}
		}

        public void Clear()
        {
            imageList1.Images.Clear();
            treeList1.ClearNodes();
        }

        private void UpdateImages()
        {
            Clear();

            ResourceHandler rh = new ResourceHandler(AppHandler.ResourceName, GetType().Assembly);
            if (rh != null)
            {
                Bitmap defUser = rh.GetBitmap("DefaultUser");
                imageList1.Images.Add(defUser);
            }

            string[] aAllUsers;
            AppHandler.UserManager.GetUserNames(out aAllUsers);
            for (int i = 0; i < aAllUsers.Length; ++i)
            {
                Image img = AppHandler.UserAvatars.Get(aAllUsers[i]);
                if (img != null)
                {
                    //imageList1.ImageSize = img.Size;
                    imageList1.Images.Add(img);
                    this.imageList1.Images.SetKeyName(imageList1.Images.Count - 1, aAllUsers[i]);
                }
            }
        }

		public void UpdateData()
		{
            try
            {
	            UpdateImages();
				InitData();
				treeList1.ClearNodes();
				treeList1.DataSource = m_aRecords;
	            //treeList1.PopulateColumns();
	            treeList1.BestFitColumns();
                //treeList1.ExpandAll(); //@todo: forces crash- should be fidex with DevExpress V18.1.10
            }
            catch (System.Exception ex)
            {
                String strTxt = String.Format("Server-Exception: {0}", ex.Message);
                AppHandler.CtsServerManager.GetAdapter().AddConsoleMessage(strTxt);
            }
		}

        public void Redraw()
        {
            treeList1.LayoutChanged();
        }

		public int GetSelectedUsers(out string[] aUsers)
		{
			DevExpress.XtraTreeList.TreeListMultiSelection sel=this.treeList1.Selection;
			if (sel.Count>0)
			{
				aUsers = new string[sel.Count];
				for(int i=0;i<sel.Count;++i)
					aUsers[i] = m_aRecords[sel[i].Id].UserName;
				return aUsers.Length;
			}
			aUsers=null;
			return 0;
		}

        public void SetCheckedUsers(string[] aUserNames)
        {
            if (aUserNames != null && aUserNames.Length > 0)
            {
                //treeList1.PopulateColumns();
                treeList1.BestFitColumns();
                treeList1.ExpandAll();

                DevExpress.XtraTreeList.Nodes.Operations.TreeListNodesIterator iter = treeList1.NodesIterator;
                TreeListSetUsersCheckedOperation operation = new TreeListSetUsersCheckedOperation(m_aRecords,aUserNames);
                iter.DoOperation(operation);
            }
        }

        public int GetCheckedUsers(out string[] aUserNames)
        {
            DevExpress.XtraTreeList.Nodes.Operations.TreeListNodesIterator iter = treeList1.NodesIterator;
            TreeListGetCheckedOperation operation = new TreeListGetCheckedOperation();
            iter.DoOperation(operation);
            int[] aiIds;
            operation.GetResults(out aiIds);
            if (aiIds!=null && aiIds.Length > 0)
            {
                aUserNames = new string[aiIds.Length];
                for (int i = 0; i < aiIds.Length; ++i)
                    aUserNames[i] = m_aRecords[aiIds[i]].UserName;
                return aUserNames.Length;
            }
            aUserNames = null;
            return 0;
        }

        private CheckState GetCheckState(object obj)
        {
            if (obj != null) return (CheckState)obj;
            return CheckState.Unchecked;
        }

        private void SetCheckedNode(DevExpress.XtraTreeList.Nodes.TreeListNode node, bool isChecked)
        {
            CheckState check = GetCheckState(node.Tag);
            if (check == CheckState.Indeterminate || check == CheckState.Unchecked)
                check = CheckState.Checked;
            else
                check = CheckState.Unchecked;

            treeList1.BeginUpdate();
            node.Tag = check;
            treeList1.EndUpdate();
        }


        public bool RegisterStudent(string adress, string userName)
        {
            String strDate = DateTime.Now.ToString("f");
            try
            {
	            var item = m_aRecords.Where(p => p.UserName == userName).Single();
	            if (item != null)
	            {
	                item.LoginTime = DateTime.Now.ToString("f");
	                item.IPAdress = adress;
	                treeList1.LayoutChanged();
	                return true;
	            }
            }
            catch (InvalidOperationException /*ex*/)
            {
            	
            }
            return false;
        }

        public bool UnregisterStudent(string userName)
        {
            String strDate = DateTime.Now.ToString("f");
            try
            {
	            var item = m_aRecords.Where(p => p.UserName == userName).Single();
	            if (item != null)
	            {
	                item.LoginTime = "";
	                item.IPAdress = "";
	                treeList1.LayoutChanged();
	                return true;
	            }
            }
            catch (InvalidOperationException /*ex*/)
            {

            }
            return false;
        }

        public void ResetMapsProgress(string userName)
        {
            var item = m_aRecords.Where(p => p.UserName == userName).Single();
            if (item != null)
            {
                item.MapsProgress = 0;
                treeList1.LayoutChanged();
            }
        }

        private int GetMapsProgress(string strUserName)
        {
            int iSumCntWork = 0;
            int iSumCntWorkedOut = 0;
            string[] aLearnmaps=null;

            bool bClassMapUsage=true;
            if (m_className.Length > 0)
            {
               AppHandler.ClassManager.GetClassMapUsage(m_className, ref bClassMapUsage);
               if (bClassMapUsage)
                   AppHandler.ClassManager.GetLearnmapNames(m_className,out aLearnmaps);
            }

            if (!bClassMapUsage /*&& aLearnmaps == null*/)
            {
                var lFoundMaps= new List<string>();
                for (int i = 0; i < AppHandler.MapManager.GetMapCnt(); i++)
                    if (AppHandler.MapManager.HasUser(i, strUserName))
                    {
                        string strTitle;
                        AppHandler.MapManager.GetTitle(i, out strTitle);
                        lFoundMaps.Add(strTitle);
                    }
                if (lFoundMaps.Count > 0)
                {
                    aLearnmaps = new string[lFoundMaps.Count];
                    lFoundMaps.CopyTo(aLearnmaps);
                }
            }

            if (aLearnmaps!=null && aLearnmaps.Length>0)
            {
                foreach (string map in aLearnmaps)
                {
                    string[] aWorkings = null;
                    if (AppHandler.MapManager.GetWorkings(map, ref aWorkings))
                    {
                        bool bIsContentOrientated = true;
                        AppHandler.MapManager.GetProgressOrientation(map, ref bIsContentOrientated);
                        foreach (string w in aWorkings)
                        {
                            if (bIsContentOrientated)
                            {
                                int iPageCntWork;
                                AppHandler.LibManager.GetPageCnt(w, out iPageCntWork);
                                int iPageCntWorkedOut = AppHandler.UserProgressInfoMgr.GetUserProgressInfo(strUserName, w,UserProgressInfoManager.RegionType.Learning);
                                iSumCntWork += iPageCntWork;
                                iSumCntWorkedOut += iPageCntWorkedOut;
                            }
                            else
                            {
                                int iQuCntWork;
                                AppHandler.LibManager.GetQuestionCnt(w, out iQuCntWork);
                                int iQuCntWorkedOut = AppHandler.UserProgressInfoMgr.GetUserProgressInfo(strUserName, w, UserProgressInfoManager.RegionType.Learning);
                                iSumCntWork += iQuCntWork;
                                iSumCntWorkedOut += iQuCntWorkedOut;
                            }
                        }
                    }
                }
            }
            return iSumCntWork>0 ? (iSumCntWorkedOut * 100 / iSumCntWork):0;
        }

		private void treeList1_SelectionChanged(object sender, System.EventArgs e)
		{
			DevExpress.XtraTreeList.TreeListMultiSelection sel=this.treeList1.Selection;
			if (sel!=null && SelectionChangedEventHandler!=null)
				SelectionChangedEventHandler(this,new EventArgs());
		}

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraTreeList.TreeListHitInfo hi = this.treeList1.CalcHitInfo((e as MouseEventArgs).Location);
            DevExpress.XtraTreeList.TreeListMultiSelection sel = this.treeList1.Selection;
            if (sel != null && SelectionDblClkEventHandler != null)
                SelectionDblClkEventHandler(this, new XUsersTreeEventArgs(hi));
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            e.NodeImageIndex = -1;
            if (m_bIsCheckable && m_aRecords != null && m_aRecords.Length > e.Node.Id)
            {
                UserEditTreeRecord r = (UserEditTreeRecord)m_aRecords[e.Node.Id];
                CheckState check = GetCheckState(e.Node.Tag);
                if (check == CheckState.Checked)
                    e.NodeImageIndex = 1;
                else
                    e.NodeImageIndex = 0;
            }
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.StateImage)
                    SetCheckedNode(hInfo.Node, true);
            }
        }

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (m_tView == ViewType.OnlineView)
            {
                if (e.Column.FieldName == "UserName" && e.CellValue != null)
                {
                    UserEditTreeRecord rec = (UserEditTreeRecord)m_aRecords[e.Node.Id];
                    bool isFocusedCell = (e.Column == treeList1.FocusedColumn && e.Node == treeList1.FocusedNode);
                    Rectangle r = e.Bounds;
                    Brush brush = null;
                    //if (rec.LoginTime.Length == 0)
                    if (!AppHandler.MainForm.IsUserConnected(rec.UserName))
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.IndianRed, Color.DarkRed, 0.0);
                    else
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.LightGreen, Color.DarkGreen, 0.0);

                    r.Inflate(-2, -1);

                    e.Graphics.FillRectangle(brush, r);
                    e.Appearance.DrawString(e.Cache, e.CellText, r);

                    if (isFocusedCell)
                        DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);

                    e.Handled = true;
                }
                else if (e.Column.FieldName == "MapsProgress" && e.CellValue != null)
                {
                    UserEditTreeRecord rec = (UserEditTreeRecord)m_aRecords[e.Node.Id];
                    Rectangle r = e.Bounds;
                    r.Inflate(-2, -1);
                    string strText = AppHandler.UserProgressInfoMgr.GetUserProgressTime(rec.UserName).ToString(@"dd\.hh\:mm\:ss");

                    e.Appearance.DrawString(e.Cache, strText, r);
                    e.Handled = true;
                }

            }
        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_evtKeyDown!=null)
                m_evtKeyDown.Invoke(sender, e);
        }
	}


    public class TreeListGetCheckedOperation : TreeListOperation
    {
        private ArrayList m_aEntries = new ArrayList();
        public TreeListGetCheckedOperation() {}
        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (node.Tag != null && ((CheckState)node.Tag) == CheckState.Checked)
                m_aEntries.Add(node.Id);
        }

        public int GetResults(out int[] aItems)
        {
            if (m_aEntries.Count > 0)
            {
                aItems = new int[m_aEntries.Count];
                aItems = (int[])m_aEntries.ToArray(typeof(int));
                return aItems.Length;
            }
            aItems = null;
            return 0;
        }
    };


    public class TreeListSetUsersCheckedOperation : TreeListOperation
    {
        private string[] m_aUserNames = null;
        private UserEditTreeRecord[] m_aRecords = null;

        public TreeListSetUsersCheckedOperation(UserEditTreeRecord[] aRecords,string [] aUserNames) 
        {
            m_aRecords = aRecords;
            m_aUserNames = aUserNames;
        }

        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (Array.Find(m_aUserNames,p => p == m_aRecords[node.Id].UserName)!=null)
                node.Tag = CheckState.Checked;
            else
                node.Tag = CheckState.Unchecked;
        }
    };



	public class UserEditTreeRecord
	{
		private int m_parentId;
		private int m_id;
		private int m_imgId;
		private string m_userName;
		private string m_fullName;
		private string m_userType;
        private string m_loginTime;
        private string m_ipAdress;
        private string m_onlineStatistics;
        private int m_mapsProgress;

		public UserEditTreeRecord(int id,int parentId,int imgId,string userName,string fullName,string userType) 
		{
			m_id=id;
			m_parentId=parentId;
			m_imgId = imgId;
			m_userName=userName;
			m_fullName=fullName;
			m_userType=userType;
            m_ipAdress = "";
            m_loginTime = "";
            m_onlineStatistics = "";
            m_mapsProgress = 50;
        }

		public UserEditTreeRecord(int id) 
		{
			m_id=id;
			m_parentId=0;
			m_imgId=0;
			m_userName="";
			m_fullName="";
			m_userType="";
            m_ipAdress = "";
            m_loginTime = "";
            m_onlineStatistics = "";
            m_mapsProgress = 50;
        }

		public int ParentId
		{
			get{return m_parentId;}
		}

		public int Id
		{
			get{return m_id;}
		}

		public int ImageIndex
		{
			get{return m_imgId;}
		}

		public string UserName
		{
			get { return m_userName; }
		}
		
		public string FullName
		{ 
			get { return m_fullName;}
		}

		public string UserType
		{ 
			get  { return m_userType; }
		}

        public string LoginTime
        {
            get { return m_loginTime; }
            set { m_loginTime = value; }
        }

        public string IPAdress
        {
            get { return m_ipAdress; }
            set { m_ipAdress = value; }
        }

        public string OnlineStatistics
        {
            get { return m_onlineStatistics; }
        }
        
        public int MapsProgress
        {
            get { return m_mapsProgress; }
            set { m_mapsProgress = value; }
        }
	}
}

