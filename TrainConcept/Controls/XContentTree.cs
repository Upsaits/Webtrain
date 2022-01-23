using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for XContentTree.
	/// </summary>
    public partial class XContentTree : DevExpress.XtraTreeList.TreeList
	{
        public enum NodeType { Unknown,Library, Book, Chapter, Point };

        private string learnmapName = "";
		private ResourceHandler rh=null;
		private ContentTreeViewList contentList;
		private ContentTreeViewList.UseType	m_tType=ContentTreeViewList.UseType.FullContent;
        private AppHandler AppHandler = Program.AppHandler;

        public ContentTreeViewList ContentList
		{
			get {return contentList;}
		}

		public ContentTreeViewList.UseType UseType
		{
			get { return m_tType;}
			set { m_tType = value;}
		}

		public string LearnmapName
		{
			get { return learnmapName;}
			set { learnmapName = value;}
		}

		public XContentTree()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
	
			string resName=AppHandler?.ResourceName;
			System.Reflection.Assembly ass=GetType().Assembly;
			if (resName!=null && ass!=null)
			{
				rh = new ResourceHandler(AppHandler.ResourceName,GetType().Assembly);
				this.BackgroundImage = rh.GetBitmap("ContentTreeView_Bkgrnd");
				this.imageList1.Images.Add(rh.GetBitmap("lib"));
				this.imageList1.Images.Add(rh.GetBitmap("book_closed"));
				this.imageList1.Images.Add(rh.GetBitmap("book_open"));
				this.imageList1.Images.Add(rh.GetBitmap("chapter_closed"));
				this.imageList1.Images.Add(rh.GetBitmap("chapter_open"));
				this.imageList1.Images.Add(rh.GetBitmap("point"));
				this.imageList1.Images.Add(rh.GetBitmap("question"));
				this.imageList2.Images.Add(rh.GetBitmap("unchecked"));
				this.imageList2.Images.Add(rh.GetBitmap("checked"));
				this.imageList2.Images.Add(rh.GetBitmap("full_checked"));
			}

			m_tType = ContentTreeViewList.UseType.FullContent;
		}

	
		public void FillTree()
		{
			switch(m_tType)
			{
				case ContentTreeViewList.UseType.FullContent:
				case ContentTreeViewList.UseType.QuestionContent:
				case ContentTreeViewList.UseType.LearnmapContent:
                case ContentTreeViewList.UseType.LearnmapWorkingsContent:
					contentList = new ContentTreeViewList(AppHandler.LibManager,AppHandler.MapManager,m_tType,learnmapName);
					break;
				default: return;
			}

			contentList.Fill();

			try
			{
				this.DataSource = ContentList;
			}
			catch(Exception ex)
			{
                Console.WriteLine(ex.Message);
			}

            if (Columns.Count > 0)
            {
                var riText = new RepositoryItemTextEdit();
                riText.EditValueChanging += riText_EditValueChanging;
                RepositoryItems.Add(riText);
                Columns[0].ColumnEdit = riText;
            }
		}

        void riText_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            
        }

		public bool SelectItem(string path,int subId=0)
		{
			int id = contentList.GetId(path);
            if (id >= 0)
            {
			    //TreeListNode node=FindNodeByKeyID(id+subId);
                TreeListNode node=FindNodeByID(id + subId);
                if (node != null/* && node!=FocusedNode*/)
                {
                    //CollapseAll();
                    FocusedNode = node;
                    return true;
                }
            }
            return false;
		}

        public bool IsItemSelected(string path)
        {
            int id = contentList.GetId(path);
            if (id >= 0 && FocusedNode!=null)
                return FocusedNode.Id == id;
            return false;
        }


        public bool CheckItem(string path,int iCheckState)
        {
            int id = contentList.GetId(path);
            TreeListNode node = FindNodeByKeyID(id);
            if (node != null)
            {
                CheckState check;
                if (iCheckState == -1)
                    check = CheckState.Unchecked;
                else if (iCheckState == 0)
                    check = CheckState.Indeterminate;
                else
                    check = CheckState.Checked;

                BeginUpdate();
                node.Tag = check;
                EndUpdate();
                return true;
            }
            return false;
        }

        public string GetFirstPoint()
        {
            int iNextPt = ContentList.FindNextPointId(0);
            if (iNextPt >= 0)
                return ContentList.GetPath(iNextPt);
            return "";
        }

        public string GetNextPoint(string strPath)
        {
            int iId = ContentList.GetId(strPath);
            if (iId >= 0)
            {
                int iNextPt = ContentList.FindNextPointId(iId);
                if (iNextPt >= 0)
                    return ContentList.GetPath(iNextPt);
            }
            return "";
        }

        public string GetPrevPoint(string strPath)
        {
            int iId = ContentList.GetId(strPath);
            if (iId >= 0)
            {
                int iNextPt = ContentList.FindPrevPointId(iId);
                if (iNextPt >= 0)
                    return ContentList.GetPath(iNextPt);
            }
            return "";
        }

        public string GetItemPath(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            int iSubId = 0;
            return GetItemPath(node, ref iSubId);
        }

        public string GetItemPath(DevExpress.XtraTreeList.Nodes.TreeListNode node,ref int iSubId)
        {
            if (node!=null)
            {
                ContentTreeViewRecord rec = (ContentTreeViewRecord)contentList[node.Id];
                if (rec.GetType() == ContentTreeViewRecordType.Question)
                {
                    ContentTreeViewRecord rp = (ContentTreeViewRecord)contentList[rec.ParentID];
                    ContentTreeViewRecord rc = (ContentTreeViewRecord)contentList[rp.ParentID];
                    ContentTreeViewRecord rb = (ContentTreeViewRecord)contentList[rc.ParentID];
                    ContentTreeViewRecord rl = (ContentTreeViewRecord)contentList[rb.ParentID];

                    string[] aPath = { rl.Title, rb.Title, rc.Title, rp.Title};
                    iSubId = rec.GetQuestionId();
                    return Utilities.MergePath(aPath);
                }
                if (rec.GetType() == ContentTreeViewRecordType.Point)
                {
                    ContentTreeViewRecord rc = (ContentTreeViewRecord)contentList[rec.ParentID];
                    ContentTreeViewRecord rb = (ContentTreeViewRecord)contentList[rc.ParentID];
                    ContentTreeViewRecord rl = (ContentTreeViewRecord)contentList[rb.ParentID];

                    string[] aPath = { rl.Title, rb.Title, rc.Title, rec.Title };
                    return Utilities.MergePath(aPath);
                }
                else if (rec.GetType() == ContentTreeViewRecordType.Chapter)
                {
                    ContentTreeViewRecord rb = (ContentTreeViewRecord)contentList[rec.ParentID];
                    ContentTreeViewRecord rl = (ContentTreeViewRecord)contentList[rb.ParentID];

                    string[] aPath = { rl.Title, rb.Title, rec.Title };
                    return Utilities.MergePath(aPath);
                }
                else if (rec.GetType() == ContentTreeViewRecordType.Book)
                {
                    ContentTreeViewRecord rl = (ContentTreeViewRecord)contentList[rec.ParentID];

                    string[] aPath = { rl.Title, rec.Title };
                    return Utilities.MergePath(aPath);
                }
                else if (rec.GetType() == ContentTreeViewRecordType.LibraryItem)
                {
                    return rec.Title;
                }
            }
            return "";
        }
	
		public CheckState GetCheckState(object obj) 
		{
			if(obj != null) return (CheckState)obj;
			return CheckState.Unchecked;
		}

		private void SetCheckedNode(DevExpress.XtraTreeList.Nodes.TreeListNode node,bool isChecked) 
		{
			if (m_tType == ContentTreeViewList.UseType.LearnmapContent)
			{
				ContentTreeViewRecord r = (ContentTreeViewRecord) ContentList[node.Id];
				if (r.GetType() == ContentTreeViewRecordType.Point)
				{
					BeginUpdate();
					node.Tag = (isChecked) ? CheckState.Checked:CheckState.Unchecked;
					EndUpdate();
				}
			}
			else
			{
				CheckState check = GetCheckState(node.Tag);
				if(check == CheckState.Indeterminate || check == CheckState.Unchecked) 
					check = CheckState.Checked;
				else 
					check = CheckState.Unchecked;

				BeginUpdate();
				node.Tag = check;
				SetCheckedChildNodes(node, check);
				SetCheckedParentNodes(node, check);
				EndUpdate();
			}
		}

		private void SetCheckedChildNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check) 
		{
			for(int i = 0; i < node.Nodes.Count; i++) 
			{
				node.Nodes[i].Tag = check;
				SetCheckedChildNodes(node.Nodes[i], check);
			}
		}

		private void SetCheckedParentNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check) 
		{
			if(node.ParentNode != null) 
			{
				bool b = false;
				for(int i = 0; i < node.ParentNode.Nodes.Count; i++) 
				{
					if(!check.Equals(node.ParentNode.Nodes[i].Tag)) 
					{
						b = !b;
						break;
					}
				}
				node.ParentNode.Tag = b ? CheckState.Indeterminate : check; 
				SetCheckedParentNodes(node.ParentNode, check);
			}
		}
        
        public bool AnalyseContentNode(out NodeType eNodeType, out string strTitle)
        {
            return AnalyseContentNode(this.FocusedNode, out eNodeType, out strTitle);
        }

        public bool AnalyseContentNode(TreeListNode node, out NodeType eNodeType, out string strTitle)
        {
            eNodeType = NodeType.Unknown;
            strTitle = "";
            if (node != null)
            {
                ContentTreeViewRecord rec = (ContentTreeViewRecord)contentList[node.Id];
                strTitle = rec.Title;
                switch (rec.GetType())
                {
                    case ContentTreeViewRecordType.LibraryItem: eNodeType = NodeType.Library; break;
                    case ContentTreeViewRecordType.Book: eNodeType = NodeType.Book; break;
                    case ContentTreeViewRecordType.Chapter: eNodeType = NodeType.Chapter; break;
                    case ContentTreeViewRecordType.Point: eNodeType = NodeType.Point; break;
                }
                return true;
            }
            return false;
        }

		private void ContentTreeView_GetSelectImage(object sender, DevExpress.XtraTreeList.GetSelectImageEventArgs e)
		{
			ContentTreeViewRecord r = (ContentTreeViewRecord) ContentList[e.Node.Id];

			if(r.GetType() == ContentTreeViewRecordType.Book)
			{
				if	(e.Node.Expanded) 
					e.NodeImageIndex = 2;
				else
					e.NodeImageIndex = 1;
			}
			else if (r.GetType() == ContentTreeViewRecordType.Chapter)
			{
				if	(e.Node.Expanded) 
					e.NodeImageIndex = 4;
				else
					e.NodeImageIndex = 3;
			}
		}



		private void ContentTreeView_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
		{
			if (m_tType == ContentTreeViewList.UseType.LearnmapContent)
			{
				ContentTreeViewRecord r = (ContentTreeViewRecord) ContentList[e.Node.Id];
				if (r.GetType() == ContentTreeViewRecordType.Point)
				{
					CheckState check = GetCheckState(e.Node.Tag);
					if(check == CheckState.Unchecked)
						e.NodeImageIndex = 0;
					else if(check == CheckState.Checked)
						e.NodeImageIndex = 1;
					else e.NodeImageIndex = 2;
				}
			}
		}

		private void ContentTreeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (m_tType == ContentTreeViewList.UseType.LearnmapContent)
			{
				if(e.Button == MouseButtons.Left) 
				{
					DevExpress.XtraTreeList.TreeListHitInfo hInfo = CalcHitInfo(new Point(e.X, e.Y));
					if(hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell) 
						SetCheckedNode(hInfo.Node,true);
				}
			}
		}

		private void ContentTreeView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (m_tType == ContentTreeViewList.UseType.LearnmapContent)
			{
				if(e.KeyData == Keys.Space) 
					SetCheckedNode(FocusedNode,(GetCheckState(FocusedNode.Tag) == CheckState.Checked)?false:true);
			}
		}

		private void ContentTreeView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			DevExpress.XtraTreeList.TreeListHitInfo hInfo = CalcHitInfo(new Point(e.X, e.Y));
			if(hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell) 
			{
				ContentTreeViewRecord r = (ContentTreeViewRecord) ContentList[hInfo.Node.Id];
				if (r.GetType() == ContentTreeViewRecordType.Point)
				{
					Cursor = Cursors.Hand;
					return;
				}
			}

			Cursor = Cursors.Arrow;
		}

		private void ContentTreeView_StateImageClick(object sender, DevExpress.XtraTreeList.NodeClickEventArgs e)
		{
			if (m_tType == ContentTreeViewList.UseType.LearnmapContent)
				SetCheckedNode(FocusedNode,(GetCheckState(FocusedNode.Tag) == CheckState.Checked)?false:true);
		}

        private TreeListNode GetDragNode(IDataObject data)
        {
            return data.GetData(typeof(TreeListNode)) as DevExpress.XtraTreeList.Nodes.TreeListNode;
        }

        private void XContentTree_DragDrop(object sender, DragEventArgs e)
        {
            TreeListNode dragNode, targetNode;
            TreeList tl = sender as TreeList;
            Point p = tl.PointToClient(new Point(e.X, e.Y));

            dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            targetNode = tl.CalcHitInfo(p).Node;

            tl.SetNodeIndex(dragNode, tl.GetNodeIndex(targetNode));
            e.Effect = DragDropEffects.None;

            NodeType nodeTypeDrag;
            string strTitleDrag;
            NodeType nodeTypeTarget;
            string strTitleTarget;

            AnalyseContentNode(dragNode, out nodeTypeDrag, out strTitleDrag);
            AnalyseContentNode(targetNode, out nodeTypeTarget, out strTitleTarget);

            string sParentPath = ContentList.GetPath(dragNode.ParentNode.Id);
            switch (nodeTypeDrag)
            {
                case NodeType.Book: AppHandler.LibManager.MoveBook(sParentPath, strTitleDrag, strTitleTarget); break;
                case NodeType.Chapter: AppHandler.LibManager.MoveChapter(sParentPath, strTitleDrag, strTitleTarget); break;
                case NodeType.Point: AppHandler.LibManager.MovePoint(sParentPath, strTitleDrag, strTitleTarget); break;
            }
        
        }

        private DragDropEffects GetDragDropEffect(TreeList tl, TreeListNode dragNode)
        {
            TreeListNode targetNode;
            Point p = tl.PointToClient(MousePosition);
            targetNode = tl.CalcHitInfo(p).Node;

            if (dragNode != null && targetNode != null
                && dragNode != targetNode
                && dragNode.ParentNode == targetNode.ParentNode)
            {
                ContentTreeViewRecord rec = (ContentTreeViewRecord)contentList[targetNode.Id];
                if (rec != null && rec.GetType() != ContentTreeViewRecordType.LibraryItem)
                    return DragDropEffects.Move;
            }
            
            return DragDropEffects.None;
        }

        private void XContentTree_CalcNodeDragImageIndex(object sender, DevExpress.XtraTreeList.CalcNodeDragImageIndexEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl != null)
            {
                if (GetDragDropEffect(tl, tl.FocusedNode) == DragDropEffects.None)
                    e.ImageIndex = -1;  // no icon
                else
                    e.ImageIndex = 1;  // the reorder icon (a curved arrow)
            }
        }

        private void XContentTree_DragOver(object sender, DragEventArgs e)
        {
            TreeListNode dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            e.Effect = GetDragDropEffect(sender as TreeList, dragNode);
        }

        private void XContentTree_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e.Node == this.FocusedNode)
                e.Appearance.BackColor = Color.LightSkyBlue;
        }

    }
}

