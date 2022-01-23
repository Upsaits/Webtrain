using System;
using System.Windows.Forms;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for XFrmClassroomEdit.
	/// </summary>
	public partial class XFrmClassroomEdit : DevExpress.XtraEditors.XtraForm
	{
		private string classname;
        private AppHandler AppHandler = Program.AppHandler;

        public string ClassName
        {
            get
            {
                return classname;
            }
        }

		public XFrmClassroomEdit(string classname)
		{
			this.classname = classname;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            Update();

            AppHandler.ClassManager.ClassroomManagerEvent += XFrmClassroomEdit_ClassroomManagerEvent;
            AppHandler.MapManager.LearnmapManagerEvent += MapManager_LearnmapManagerEvent;
            AppHandler.UserManager.UserManagerEvent += UserManager_UserManagerEvent;
            AppHandler.UserProgressInfoMgr.UserProgressInfoManagerEvent +=UserProgressInfoMgr_UserProgressInfoManagerEvent;
        }

        public new void Activate()
        {
            base.Activate();
            Update();
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

        private void ShowMapMode(bool bIsUserMap)
        {
            if (bIsUserMap)
            {
                this.panel3.Visible = false;
                this.panel3.Dock = System.Windows.Forms.DockStyle.None;
                this.panel4.Visible = true;
                this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            else 
            {
                this.panel3.Visible = true;
                this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel4.Visible = false;
                this.panel4.Dock = System.Windows.Forms.DockStyle.None;
            }
        }

        new private void Update()
        {
            bool useClassMaps = true;
            AppHandler.ClassManager.GetClassMapUsage(classname, ref useClassMaps);
            this.radioGroup1.EditValue = (useClassMaps) ? 1 : 0;

            this.listBoxControl1.Items.Clear();
            string[] aClassMaps;
            AppHandler.ClassManager.GetLearnmapNames(classname,out aClassMaps);
            this.listBoxControl1.Items.AddRange(aClassMaps);

            string[] aUserNames;
            AppHandler.ClassManager.GetUserNames(classname, out aUserNames);
            this.xUsersTree1.ClassName = classname;
            this.xUsersTree1.UserList = (aUserNames == null) ? new string[] { } : aUserNames;
        }

		private void XFrmClassroomEdit_Closed(object sender, System.EventArgs e)
		{
            AfterActivation(false);
			AppHandler.ContentManager.ClassroomClosed(classname);
        }

        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            ShowMapMode((int)this.radioGroup1.EditValue == 0);
            xUsersTree1.UpdateData();
        }

        private void XFrmClassroomEdit_ClassroomManagerEvent(object sender, ref Libraries.ClassroomManagerEventArgs ea)
        {
            if (ea.Command == Libraries.ClassroomManagerEventArgs.CommandType.AddUser ||
                ea.Command == Libraries.ClassroomManagerEventArgs.CommandType.RemoveUser)
            {
                string[] aUserNames;
                AppHandler.ClassManager.GetUserNames(classname, out aUserNames);
                this.xUsersTree1.UserList = (aUserNames == null) ? new string[] { } : aUserNames;
            }
            else if (ea.Command == Libraries.ClassroomManagerEventArgs.CommandType.AddLearnmap ||
                     ea.Command == Libraries.ClassroomManagerEventArgs.CommandType.RemoveLearnmap)
                Update();
        }

        private void MapManager_LearnmapManagerEvent(object sender, ref Libraries.LearnmapManagerEventArgs ea)
        {
            if (ea.Command == Libraries.LearnmapManagerEventArgs.CommandType.Create ||
                ea.Command == Libraries.LearnmapManagerEventArgs.CommandType.Destroy)
            {
                Update();
            }
        }

        private void UserManager_UserManagerEvent(object sender, ref Libraries.UserManagerEventArgs ea)
        {
            Update();
        }


        private void UserProgressInfoMgr_UserProgressInfoManagerEvent(object sender, EventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnUserProgressInfoManagerHandler(UserProgressInfoMgr_UserProgressInfoManagerEvent), new object[] { sender, ea });
            }
            else
            {
                Update();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string[] aOldUsers;
            AppHandler.ClassManager.GetUserNames(classname, out aOldUsers);
            XFrmUsersChoose frm = new XFrmUsersChoose(aOldUsers);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string[] aNewUsers;
                frm.GetCheckedUsers(out aNewUsers);
                AppHandler.ClassManager.SetUserNames(classname, aNewUsers);
                AppHandler.ClassManager.Save();

                string[] aLearnmaps;
                AppHandler.ClassManager.GetLearnmapNames(classname,out aLearnmaps);
                if (aLearnmaps!=null && aLearnmaps.Length>0)
                {
                    foreach (string map in aLearnmaps)
                    {
                        if (aOldUsers!=null && aOldUsers.Length>0)
                            foreach (string strOld in aOldUsers)
                                if (strOld.Length > 0 && (Array.Find(aOldUsers, p => p == strOld) != null))
                                    AppHandler.MapManager.DeleteUser(map,strOld);
                        if (aNewUsers != null && aNewUsers.Length > 0)
                            foreach (string user in aNewUsers)
                            AppHandler.MapManager.AddUser(map, user);
                        AppHandler.MapManager.Save(map);
                    }
                }

            }
        }

        private void btnResetProgress_Click(object sender, EventArgs e)
        {
            string[] aUsers = null;
            if (this.xUsersTree1.GetSelectedUsers(out aUsers) > 0)
            {
                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Delete_user_progressinfo", "Lernfortschritt der ausgewählten Benutzer löschen?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (string u in aUsers)
                    {
                        if (u != "admin")
                        {
                            if (AppHandler.MainForm.IsUserConnected(u))
                            {
                                txt = AppHandler.LanguageHandler.GetText("MESSAGE", "User_is_online", "Benutzer {0} ist angemeldet!");
                                cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                MessageBox.Show(String.Format(txt, u), cap, MessageBoxButtons.OK);
                                continue;
                            }

                            AppHandler.UserProgressInfoMgr.DeleteProgressInfoOfUser(u);
                            this.xUsersTree1.ResetMapsProgress(u);
                        }
                    }

                }
            }
        }

        private void radioGroup1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.OldValue != null)
            {
                bool bWasClassMapUsage = false;
                AppHandler.ClassManager.GetClassMapUsage(classname, ref bWasClassMapUsage);
                AppHandler.ClassManager.SetClassMapUsage(classname, !bWasClassMapUsage);
                AppHandler.ClassManager.Save();

                string[] aLearnmaps;
                string[] aUsers;
                AppHandler.ClassManager.GetLearnmapNames(classname, out aLearnmaps);
                AppHandler.ClassManager.GetUserNames(classname, out aUsers);
                if (aLearnmaps != null && aLearnmaps.Length > 0)
                    foreach (string map in aLearnmaps)
                    {
                        if (aUsers != null && aUsers.Length > 0)
                            foreach(string user in aUsers)
                            {
                                if (bWasClassMapUsage)
                                    AppHandler.MapManager.DeleteUser(map,user);
                                else
                                    AppHandler.MapManager.AddUser(map, user);
                            }
                        AppHandler.MapManager.Save(map);
                    }
            }

            /*
            bool bWasClassMapUsage = false;
            AppHandler.ClassManager.GetClassMapUsage(classname, ref bWasClassMapUsage);
            string txt;
            if (bWasClassMapUsage)
                txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_switch_to_usermap", "Bei diesem Vorgang werden alle aktuellen Klassen von der Mappe abgemeldet\nWollen Sie wirklich wechseln?");
            else
                txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_switch_to_classmap", "Bei diesem Vorgang werden alle aktuellen Benutzer von der Mappe abgemeldet\nWollen Sie wirklich wechseln?");
            string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
            if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppHandler.MapManager.SetUsers(mapTitle, null);
                e.Cancel = false;
            }
            else
                e.Cancel = true;*/
        }

        private void xUsersTree1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                btnResetProgress_Click(this, new EventArgs());
        }

    }
}

