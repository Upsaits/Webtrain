using System;
using SoftObject.TrainConcept.Controls;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for XFrmMapsEdit.
	/// </summary>
    public partial class XFrmMapsEdit : DevExpress.XtraEditors.XtraForm
	{
        private AppHandler AppHandler = Program.AppHandler;

        public XFrmMapsEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//xMapsTree1.IsSelectable=false;
			xMapsTree1.UpdateData();

            AppHandler.MapManager.LearnmapManagerEvent += XFrmMapsEdit_LearnmapEvent;
            AppHandler.ClassManager.ClassroomManagerEvent += ClassManager_ClassroomManagerEvent;
            AppHandler.UserManager.UserManagerEvent += UserManager_UserManagerEvent;

            this.xMapsTree1.TreeList.ValidatingEditor += treeList1_ValidatingEditor;
            this.xMapsTree1.TreeList.ShownEditor += treeList1_ShownEditor;
        }


        public new void Activate()
        {
            base.Activate();
            AfterActivation(true);
        }

        public void AfterActivation(bool bIsOn)
        {
            if (!bIsOn)
            {
                AppHandler.MainForm.CloseBar.Visible = false;
            }
            else
            {
                AppHandler.MainForm.CloseBar.Visible = true;
            }
        }

		private void XFrmMapsEdit_Closed(object sender, System.EventArgs e)
		{
            AfterActivation(false);
			AppHandler.ContentManager.LearnmapDistributorClosed();
		}

        private void btnDelete_Click(object sender, EventArgs e)
        {
			string[] aTitles=null;
            if (this.xMapsTree1.GetSelected(out aTitles) > 0)
                AppHandler.MainForm.ControlCenter.DeleteEditLearnmap(aTitles[0]);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AppHandler.MainForm.ControlCenter.CreateNewEditLearnmap();
        }

        private void xMapsTree1_OnSelectionChangedEvent(object sender, EventArgs e)
        {
            string[] aTitles = null;
            this.xMapsTree1.GetSelected(out aTitles);
            if (aTitles == null || aTitles.Length == 0)
            {
                this.btnDelete.Visible = false;
            }
            else if (aTitles.Length >= 1)
            {
                this.btnDelete.Visible = true;
            }
        }

        private void btnAssignColor_Click(object sender, EventArgs e)
        {

        }

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
            bool bIsClassMap = false;
            DevExpress.XtraTreeList.TreeList tree = sender as DevExpress.XtraTreeList.TreeList;
            AppHandler.MapManager.GetUsage(xMapsTree1.Records[tree.FocusedNode.Id].Content, ref bIsClassMap);
            if (!((tree.FocusedColumn.FieldName == "UserList" && !bIsClassMap) ||
                  (tree.FocusedColumn.FieldName == "ClassList" && bIsClassMap) ||
                   tree.FocusedColumn.FieldName == "Color") ||
                  tree.FocusedNode.ParentNode != null)
                tree.ActiveEditor.Enabled = false;
        }

        private void treeList1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (e.Valid)
            {
                var tree = sender as DevExpress.XtraTreeList.TreeList;

                bool bIsUserList = tree.FocusedColumn.FieldName == "UserList";
                bool bIsClassList = tree.FocusedColumn.FieldName == "ClassList";
                bool bIsColor = tree.FocusedColumn.FieldName == "Color";
                LearnmapTreeRecord rec = xMapsTree1.Records[tree.FocusedNode.Id];
                string mapTitle = rec.Content;

                if (bIsUserList || bIsClassList)
                {
                    string strValue = (e.Value as string).Replace(", ", ",");
                    string[] aNewEntries = strValue.Split(new char[] { ',' });
                    if (bIsUserList && rec.UserList != strValue)
                    {
                        AppHandler.MapManager.SetUsers(mapTitle, aNewEntries);
                        AppHandler.MapManager.Save(mapTitle);
                    }
                    else if (bIsClassList && rec.ClassList != strValue)
                    {
                        string[] aOldClasses = rec.ClassList.Replace(", ", ",").Split(new char[] { ',' });
                        foreach (var c in aOldClasses)
                            if (c.Length > 0 && (Array.Find(aNewEntries, p => p == c) == null))
                                AppHelpers.RemoveMapFromClass(c, mapTitle);

                        foreach (var c in aNewEntries)
                            if (c.Length > 0)
                                AppHelpers.AddMapToClass(c, mapTitle);

                        AppHandler.ClassManager.Save();
                    }
                }
                else if (bIsColor)
                {
                    Nullable<System.Drawing.Color> col = e.Value as Nullable<System.Drawing.Color>;
                    if (col != null)
                    {
                        string test1 = String.Format("{0},{1},{2}", col.Value.R.ToString(), col.Value.G.ToString(), col.Value.B.ToString());
                        AppHandler.MapManager.SetColor(mapTitle, test1);
                        AppHandler.MapManager.Save(mapTitle);
                    }
                }
            }
        }

        private void XFrmMapsEdit_LearnmapEvent(object sender, ref LearnmapManagerEventArgs ea)
        {
            if (ea.Command != LearnmapManagerEventArgs.CommandType.Close)
                this.xMapsTree1.UpdateData();
        }

        private void ClassManager_ClassroomManagerEvent(object sender, ref ClassroomManagerEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnClassroomManagerHandler(ClassManager_ClassroomManagerEvent), new object[] { sender, ea });
                return;
            }
            this.xMapsTree1.UpdateData();
        }

        private void UserManager_UserManagerEvent(object sender, ref UserManagerEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnUserManagerHandler(UserManager_UserManagerEvent), new object[] { sender, ea });
                return;
            }
            this.xMapsTree1.UpdateData();
        }

     }
}

