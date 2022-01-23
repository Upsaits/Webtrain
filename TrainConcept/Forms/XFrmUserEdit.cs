using System;
using System.ComponentModel;
using System.Windows.Forms;
using SoftObject.SOComponents.Forms;
using SoftObject.TrainConcept.Controls;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for XFrmUserEdit.
	/// </summary>
    public partial class XFrmUserEdit : DevExpress.XtraEditors.XtraForm
	{
        private AppHandler AppHandler = Program.AppHandler;
        public XFrmUserEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            string[] aUsers;
            AppHandler.UserManager.GetUserNames(out aUsers);
            AppHandler.UserManager.UserManagerEvent += UserManager_UserManagerEvent;
            xUsersTree1.UserList = aUsers;

            AppHandler.MapManager.LearnmapManagerEvent += new OnLearnmapManagerHandler(XFrmUserEdit_LearnmapEvent);
            xUsersTree1.TreeKeyDown += xUsersTree1_TreeKeyDown;

            rbgAdminType.EditValue = AppHandler.UserManager.IsCentralManaged() ? 1 : 0;
            rbgAdminType.Properties.Items[1].Enabled = (AppCommunication.UserList.Count>0);
            btnSyncUsers.Visible = AppHandler.UserManager.IsCentralManaged();
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

		private void AddMapsToUsers(string[] aMaps,string[] aUsers)
		{
			for(int iMap=0;iMap<aMaps.Length;++iMap)
			{
				string mapTitle=aMaps[iMap];
				if (mapTitle!=null)
				{
					for(int i=0;i<aUsers.Length;++i)
					{
						if (AppHandler.MapManager.AddUser(mapTitle,aUsers[i]))
							AppHandler.MapManager.Save(mapTitle);
					}
				}
			}
		}

		private void DelMapsFromUsers(string[] aMaps,string[] aUsers)
		{
			for(int iMap=0;iMap<aMaps.Length;++iMap)
			{
				string mapTitle=aMaps[iMap];
				if (mapTitle!=null)
				{
					for(int i=0;i<aUsers.Length;++i)
					{
						if (AppHandler.MapManager.DeleteUser(mapTitle,aUsers[i]))
							AppHandler.MapManager.Save(mapTitle);
					}
				}
			}
		}


		private void XFrmUserEdit_Closed(object sender, System.EventArgs e)
		{
            AfterActivation(false);
			AppHandler.ContentManager.UserEditorClosed();
		}


        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEditUser dlg = new FrmEditUser();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                UpdateUserList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string[] aUsers = null;
            if (this.xUsersTree1.GetSelectedUsers(out aUsers)>0)
            {
                string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Delete_user", "Ausgewählte Benutzer löschen?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (string u in aUsers)
                    {
                        if (u == "admin")
                        {
                            txt = AppHandler.LanguageHandler.GetText("MESSAGE", "admin_cannot_be_deleted", "Benutzer kann nicht gelöscht werden!");
                            cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            MessageBox.Show(String.Format(txt, u), cap, MessageBoxButtons.OK);
                            continue;
                        }
                        else if (AppHandler.MainForm.IsUserConnected(u))
                        {
                            txt = AppHandler.LanguageHandler.GetText("MESSAGE", "User_is_online", "Benutzer {0} ist angemeldet!");
                            cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            MessageBox.Show(String.Format(txt,u), cap, MessageBoxButtons.OK);
                            continue;
                        }

                        AppHandler.UserManager.DeleteUser(u);
                        AppHandler.UserManager.Save();

                        AppHandler.MapManager.DeleteUser(u);

                        AppHandler.ClassManager.RemoveUser(u);
                        AppHandler.ClassManager.Save();
                    }

                    UpdateUserList();
                }
            }
        }

        private void btnSetPicture_Click(object sender, EventArgs e)
        {
            string[] aUsers = null;
            if (this.xUsersTree1.GetSelectedUsers(out aUsers) == 1)
            {
                string strUserName=aUsers[0];
                FrmSetAvatar dlg = new FrmSetAvatar(strUserName);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.ShouldRemove)
                        AppHandler.UserAvatars.Remove(strUserName);
                    UpdateUserList();
                }
            }
        }

        private void XFrmUserEdit_LearnmapEvent(object sender, ref LearnmapManagerEventArgs ea)
        {
            UpdateUserList();
        }

        private void xUsersTree1_OnSelectionChangedEvent(object sender, System.EventArgs e)
        {
            string[] aUsers = null;
            this.xUsersTree1.GetSelectedUsers(out aUsers);

            btnDelete.Enabled = (aUsers != null && aUsers.Length > 0 && aUsers[0]!="admin");
            btnEdit.Enabled = (aUsers != null && aUsers.Length > 0);
        }

        private void xUsersTree1_OnSelectionDblClkEvent(object sender, EventArgs e)
        {
            XUsersTree.XUsersTreeEventArgs ea = e as XUsersTree.XUsersTreeEventArgs;
            DevExpress.XtraTreeList.Columns.TreeListColumn col = ea.HitInfo.Column;
            if (ea.HitInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell)
            {
                if (col.AbsoluteIndex >= 0 && col.AbsoluteIndex <= 2)
                {
                    FrmEditUser.EditType editType = FrmEditUser.EditType.AllExceptName;
                    switch (col.AbsoluteIndex)
                    {
                        case 0: editType = FrmEditUser.EditType.Name; break;
                        case 1: editType = FrmEditUser.EditType.FullName; break;
                        case 2: editType = FrmEditUser.EditType.Type; break;
                    }

                    string[] aUsers;
                    this.xUsersTree1.GetSelectedUsers(out aUsers);
                    if (aUsers.Length == 1)
                    {
                        if (AppHandler.MainForm.IsUserConnected(aUsers[0]))
                        {
                            String txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "User_is_online", "Änderungen sind nicht möglich wenn der User angemeldet ist!");
                            String cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            MessageBox.Show(txt1, cap, MessageBoxButtons.OK);
                            return;
                        }
                        else if (aUsers[0] == "admin" && editType != FrmEditUser.EditType.FullName)
                        {
                            String txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "User_cannot_be_modified", "Standard-Administrator kann nicht verändert werden!");
                            String cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            MessageBox.Show(txt1, cap, MessageBoxButtons.OK);
                            return;
                        }

                        FrmEditUser frm = new FrmEditUser(aUsers[0], editType);
                        frm.ShowDialog();
                        UpdateUserList();
                    }
                }
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            string[] aUsers = null;
            this.xUsersTree1.GetSelectedUsers(out aUsers);
            if (aUsers != null && aUsers.Length == 1)
            {
                if (AppHandler.MainForm.IsUserConnected(aUsers[0]))
                {
                    String txt1 = AppHandler.LanguageHandler.GetText("MESSAGE", "User_is_online", "Änderungen sind nicht möglich wenn der angemeldet ist!");
                    String cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                    MessageBox.Show(txt1, cap, MessageBoxButtons.OK);
                    return;
                }

                FrmEditUser.EditType eType= FrmEditUser.EditType.AllExceptName;
                if (aUsers[0] == "admin")
                    eType= FrmEditUser.EditType.Administrator;

                FrmEditUser dlg = new FrmEditUser(aUsers[0], eType);
                if (dlg.ShowDialog() == DialogResult.OK)
                    UpdateUserList();
            }
        }

        public void UpdateUserList()
        {
            string[] aUsers;
            AppHandler.UserManager.GetUserNames(out aUsers);
            xUsersTree1.UserList = aUsers;
            this.xUsersTree1.UpdateData();
        }

        void xUsersTree1_TreeKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                btnDelete_Click(this, new EventArgs());
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
                        }
                        else if (AppHandler.MainForm.ActualUserName != "admin")
                        {
                            continue;
                        }

                        AppHandler.UserProgressInfoMgr.DeleteProgressInfoOfUser(u);
                        this.xUsersTree1.ResetMapsProgress(u);
                        
                    }

                }
            }

        }

        private void rbgAdminType_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            bool bWasCentralManaged = AppHandler.UserManager.IsCentralManaged();

            if (((int)e.NewValue==1 && bWasCentralManaged) ||
                ((int)e.NewValue==0 && !bWasCentralManaged))
                e.Cancel = false;
            else
            {
                string txt;
                if (bWasCentralManaged)
                    txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_switch_to_local_managed_users", "Bei diesem Vorgang werden alle bestehenden Klassen gelöscht\nWollen Sie wirklich wechseln?");
                else
                    txt = AppHandler.LanguageHandler.GetText("MESSAGE", "Do_you_want_to_switch_to_classmap", "Bei diesem Vorgang werden alle bestehenden Klassen gelöscht\nWollen Sie wirklich wechseln?");
                string cap = AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                if (MessageBox.Show(txt, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private void UserManager_UserManagerEvent(object sender, ref UserManagerEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnUserManagerHandler(UserManager_UserManagerEvent), new object[] { sender, ea });
                return;
            }

            if (ea.Command == UserManagerEventArgs.CommandType.ChangeManagement ||
                ea.Command == UserManagerEventArgs.CommandType.Update)
            {
                string[] aUsers;
                AppHandler.UserManager.GetUserNames(out aUsers);
                xUsersTree1.UserList = aUsers;

                if (ea.Command == UserManagerEventArgs.CommandType.ChangeManagement)
                {
                    // delete all classrooms
                    string[] astrClassNames;
                    AppHandler.ClassManager.GetClassNames(out astrClassNames);
                    if (astrClassNames != null)
                        foreach (string strClass in astrClassNames)
                            AppHandler.ClassManager.DeleteClassroom(strClass);
                }

                if (AppHandler.UserManager.IsCentralManaged())
                {
                    foreach (var u in AppCommunication.UserList)
                    {
                        string[] strTokens = u.Split(',');
                        if (strTokens.Length == 9)
                        {
                            /* see: TCWebUpdate/ServerInfoRepository.cs
                            string strResult = r["surName"].ToString() + ',' +      [0]
                                               r["middleName"].ToString() + ',' +   [1]
                                               r["lastName"].ToString() + ',' +     [2]
                                               r["degree"].ToString() + ',' +       [3]
                                               r["userName"].ToString() + ',' +     [4]     
                                               r["password"].ToString() + ',' +     [5]
                                               r["activated"].ToString() + ',' +    [6]
                                               r["className"].ToString() + ',' +    [7]
                                               r["admin"].ToString();               [8]
                              */
                            if (strTokens[7].Length > 0 && AppHandler.ClassManager.GetClassId(strTokens[7])<0)
                                AppHandler.ClassManager.CreateClassroom(strTokens[7]);
                            AppHandler.ClassManager.AddUser(strTokens[7], strTokens[4].ToLower());
                        }
                    }

                    AppHandler.ClassManager.Save();
                }
            }
        }


        private void rbgAdminType_EditValueChanged(object sender, EventArgs evtArgs)
        {
            bool bIsCentralAdmin = (int)this.rbgAdminType.EditValue != 0;
            bool bWasCentralManaged = AppHandler.UserManager.IsCentralManaged();
            if (bWasCentralManaged != bIsCentralAdmin)
            {
                var frm = new XFrmLongProcessToastNotificationSmall();
                frm.Start("Zentrale Uservewaltung wird aktiviert..",
                    (DoWorkEventArgs e) =>
                    {
                        AppHandler.MainForm.BeginInvoke((MethodInvoker)delegate
                        {
                            AppHandler.UserManager.SetCentralManaged(bIsCentralAdmin, AppCommunication.UserList);
                            btnSyncUsers.Visible = bIsCentralAdmin;
                        });
                    });
            }
        }

        private void btnSyncUsers_Click(object sender, EventArgs e)
        {
            Program.AppCommunication.CheckUserList();
        }
    }
}

