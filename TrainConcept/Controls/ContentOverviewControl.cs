using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Controls
{
	/// <summary>
	/// Summary description for FrmContentOverview.
	/// </summary>
	public partial class ContentOverviewControl : ActivateableUserControl
	{
		private string mapTitle;
        private static bool bIgnoreCheck = false;
        private AppHandler AppHandler = Program.AppHandler;

        public ContentOverviewControl(string _mapTitle)
		{
			mapTitle = _mapTitle;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeTreeView();

            InitializeWorkingList();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		private void InitializeTreeView()
		{
			treeList1.UseType = ContentTreeViewList.UseType.LearnmapWorkingsContent;
			treeList1.LearnmapName = mapTitle;
			treeList1.FillTree();
			treeList1.PopulateColumns();
			treeList1.BestFitColumns();
			treeList1.ExpandAll();
			treeList1.Font = AppHandler.DefaultFont;
		}


        private void InitializeWorkingList()
        {
            string[] aWorkings = null;
            string[] aTests = null;
            if (AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings, false) &&
                AppHandler.MapManager.GetWorkingTests(mapTitle, ref aTests))
            {
                bool bIsContentOrientated = true;
                AppHandler.MapManager.GetProgressOrientation(mapTitle,ref bIsContentOrientated);
                for (int i = 0; i < aWorkings.Length; ++i)
                {
                    if (aWorkings[i].Length > 0)
                    {
                        int iCheckedState = AppHelpers.GetMapProgressOfWork(AppHandler.MainForm.ActualUserName, aWorkings[i]);
                        checkedListBoxControl1.Items.Add(aWorkings[i], iCheckedState == 1);
                    }
                    else if (aTests[i].Length > 0)
                    {
                        var aTRIs = new TestResultItemCollection();
                        AppHandler.TestResultManager.Find(out aTRIs, AppHandler.MainForm.ActualUserName, mapTitle,
                            aTests[i], true);
                        bool bPassed = false;
                        foreach (TestResultItem tr in aTRIs)
                        {
                            if (tr.percRight > 50)
                                bPassed = true;
                        }
                        this.checkedListBoxControl1.Items.Add(aTests[i], bPassed);
                    }
                }
            }
        }

        public override void SetActive(bool bIsOn)
        {
            if (bIsOn)
            {
                string[] aWorkings = null;
                string[] aTests = null;
                if (AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings, false) &&
                    AppHandler.MapManager.GetWorkingTests(mapTitle, ref aTests))
                    for (int i = 0; i < aWorkings.Length; ++i)
                    {
                        if (aWorkings[i].Length > 0)
                        {
                            int iCheckedState = AppHelpers.GetMapProgressOfWork(AppHandler.MainForm.ActualUserName, aWorkings[i]);
                            checkedListBoxControl1.Items[i].CheckState = (iCheckedState == 1) ? CheckState.Checked : ((iCheckedState<0) ? CheckState.Unchecked : CheckState.Indeterminate);
                        }
                    }
            }
        }

		private void ItemSelected(DevExpress.XtraTreeList.Nodes.TreeListNode node,string testname="")
		{
			ContentTreeViewRecord rp = (ContentTreeViewRecord) treeList1.ContentList[node.Id];
			if (rp.GetType() == ContentTreeViewRecordType.Point)
			{
                string strPath = treeList1.GetItemPath(node);
                FrmContent frmContent = (FrmContent)this.ParentForm;
			    if (frmContent != null)
                    frmContent.AddPage(rp.Title, strPath,testname, true);
            }
		}

        public string GetFirstPoint()
        {
            return treeList1.GetFirstPoint();
        }

        public string GetNextPoint(string strPath,out bool bIsTest)
        {
            string[] aWorkings = null;
            string[] aTests = null;
            if (AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings, false) &&
                AppHandler.MapManager.GetWorkingTests(mapTitle, ref aTests))
            {
                int id = Array.FindIndex(aWorkings, x => x == strPath);
                if (id < 0)
                    id = Array.FindIndex(aTests, x => x == strPath);
                {
                    if (id < (aWorkings.Length - 1))
                    {
                        if (aWorkings[id + 1].Length == 0)
                        {
                            bIsTest = true;
                            return aTests[id + 1];
                        }

                        bIsTest = false;
                        return aWorkings[id + 1];
                    }
                }
            }

            bIsTest = false;
            return "";
        }

        public string GetNextPoint(string strPath)
        {
            return treeList1.GetNextPoint(strPath);
        }

        public string GetPrevPoint(string strPath, out bool bIsTest)
        {
            string[] aWorkings = null;
            string[] aTests = null;
            if (AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings, false) &&
                AppHandler.MapManager.GetWorkingTests(mapTitle, ref aTests))
            {
                int id = Array.FindIndex(aWorkings, x => x == strPath);
                if (id < 0)
                    id = Array.FindIndex(aTests, x => x == strPath);
                if (id > 0)
                {
                    if (aWorkings[id-1].Length == 0)
                    {
                        bIsTest = true;
                        return aTests[id-1];
                    }

                    bIsTest = false;
                    return aWorkings[id-1];
                }
            }

            bIsTest = false;
            return "";
        }

	    public string GetPrevPoint(string strPath)
        {
            return treeList1.GetPrevPoint(strPath);
        }

        public bool SelectItem(string strWork)
        {
            if (treeList1.SelectItem(strWork))
            {
                ItemSelected(treeList1.FocusedNode);
                return true;
            }
            return false;
        }

		private void treeList1_StateImageClick(object sender, DevExpress.XtraTreeList.NodeClickEventArgs e)
		{
			XContentTree tv=sender as XContentTree;
			ContentTreeViewRecord rp = (ContentTreeViewRecord) treeList1.ContentList[e.Node.Id];
			if (rp.GetType() == ContentTreeViewRecordType.Point)
			{
				ContentTreeViewRecord rc = (ContentTreeViewRecord) treeList1.ContentList[rp.ParentID];
				ContentTreeViewRecord rb = (ContentTreeViewRecord) treeList1.ContentList[rc.ParentID];
				ContentTreeViewRecord rl = (ContentTreeViewRecord) treeList1.ContentList[rb.ParentID];

				var frame = this.ParentForm as FrmContent;
			    if (frame!=null)
			    {
                    string[] aPath = { rl.Title, rb.Title, rc.Title, rp.Title };

                    if (tv.GetCheckState(e.Node.Tag) == CheckState.Checked)
                        frame.AddPage(rp.Title, Utilities.MergePath(aPath), "", false);
                    else
                        frame.RemovePage(Utilities.MergePath(aPath));
			    }
			}
		}


        private void checkedListBoxControl1_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            //CheckedListBoxControl listBox = sender as CheckedListBoxControl;
            e.Cancel = true;
        }

        private void checkedListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void treeList1_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            var treeList = sender as XContentTree;
            if (treeList != null)
            {
                ContentTreeViewRecord rp = (ContentTreeViewRecord)treeList.ContentList[e.Node.Id];
                if (rp.GetType() == ContentTreeViewRecordType.Point && !bIgnoreCheck)
                {
                    string strPath = treeList.GetItemPath(e.Node);
                    string testName = "";
                    int iPosAfterTest = 0;
                    FrmContent frmContent = (FrmContent)this.ParentForm;
                    if (frmContent != null && !frmContent.IsPageAllowed(strPath, ref testName,ref iPosAfterTest))
                    {
                        if (iPosAfterTest >= 1)
                        {
                            string txt = AppHandler.LanguageHandler.GetText("ERROR",
                                "You_have_to_finish_the_intermediate_test_xxx_first",
                                "Sie müssen erst den Zwischentest {0} abschließen!");
                            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            txt = String.Format(txt, testName);
                            MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.CanFocus = false;
                        }
                    }
                }
            }
       }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var treeList = sender as XContentTree;
            if (treeList != null)
            {
                string strPath = treeList.GetItemPath(e.Node);
                string testName = "";
                int iPosAfterTest = 0;
                FrmContent frmContent = (FrmContent)this.ParentForm;
                if (frmContent != null && !frmContent.IsPageAllowed(strPath, ref testName,ref iPosAfterTest))
                    ItemSelected(e.Node, testName);
                else
                    ItemSelected(e.Node);
            }
        }

        private void checkedListBoxControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listBox = sender as CheckedListBoxControl;
            if (listBox != null)
            {
                int iId = listBox.SelectedIndex;
                if (iId >= 0)
                {
                    var item = listBox.Items[iId];
                    string text = item.Value as string;
                    string[] aWorkings = null;
                    string[] aTests = null;
                    if (AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings, false) &&
                        AppHandler.MapManager.GetWorkingTests(mapTitle, ref aTests))
                    {
                        int id = Array.FindIndex(aWorkings, x => x == text);
                        if (id<0)
                            id=Array.FindIndex(aTests, x => x == text);
                        if (aWorkings[id].Length != 0)
                        {
                            string strPath = treeList1.GetItemPath(treeList1.FocusedNode);
                            if (strPath == text)
                            {
                                FrmContent frmContent = (FrmContent)this.ParentForm;
                                if (frmContent != null)
                                    frmContent.SelectPage(strPath);
                            }
                            else
                                treeList1.SelectItem(text);
                        }
                        else
                        {
                            if (AppHelpers.IsTestPassed(AppHandler.MainForm.ActualUserName, mapTitle, aTests[id]))
                            {
                                string txt = AppHandler.LanguageHandler.GetText("MESSAGE",
                                    "Intermediate_test_xxx_is_already_workedout",
                                    "Zwischentest {0} ist bereits abgeschlossen!");
                                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                txt = String.Format(txt, aTests[id]);
                                MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                bIgnoreCheck = true;
                                if (aWorkings[ id + 1].Length>0)
                                    treeList1.SelectItem(aWorkings[id + 1]);
                                bIgnoreCheck = false;
                                //todo: start Test
                            }
                        }
                    }
                }
            }
        }

        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            var treeList = sender as XContentTree;
            if(e.Button == MouseButtons.Left) 
			{
				var hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
				if(hInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell) 	
                {
                    string strPath = treeList.GetItemPath(hInfo.Node);
                    string testName = "";
                    int iPosAfterTest = 0;
                    FrmContent frmContent = (FrmContent)this.ParentForm;
                    if (frmContent != null && !frmContent.IsPageAllowed(strPath, ref testName, ref iPosAfterTest))
                        ItemSelected(hInfo.Node, testName);
                    else
                        ItemSelected(hInfo.Node);
                }
			}
        }
	}
}
