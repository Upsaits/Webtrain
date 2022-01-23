using System;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Forms
{
	/// <summary>
	/// Summary description for XFrmOnlineUsers.
	/// </summary>
    public partial class XFrmOnlineUsers : DevExpress.XtraEditors.XtraForm
	{
        private AppHandler AppHandler = Program.AppHandler;

        public XFrmOnlineUsers()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            string [] aUsers;
            AppHandler.UserManager.GetUserNames(out aUsers);

            //var aUsers2 = (string[]) aUsers.Where(s => !s.Contains(AppHandler.MainForm.ActualUserName)).ToArray();
            this.treeList1.UserList = aUsers;

            AppHandler.UserManager.UserManagerEvent += UserManager_UserManagerEvent;
        }

        void UserManager_UserManagerEvent(object sender, ref UserManagerEventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new OnUserManagerHandler(UserManager_UserManagerEvent), new object[] { sender, ea });
                return;
            }

            if (ea.Command == UserManagerEventArgs.CommandType.ChangeManagement)
            {
                string[] aUsers;
                AppHandler.UserManager.GetUserNames(out aUsers);
                this.treeList1.UserList = aUsers;
            }
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

		public bool RegisterStudent(string adress,string userName)
		{
            return treeList1.RegisterStudent(adress, userName);
		}
	
		public void UnregisterStudent(string userName)
		{
            treeList1.UnregisterStudent(userName);
		}

        public void UpdateData()
        {
            treeList1.UpdateData();
        }

		private void XFrmOnlineUsers_Closed(object sender, System.EventArgs e)
		{
            AfterActivation(false);
			AppHandler.ContentManager.CTSStudentsClosed();
		}

        private void OnTick(object sender, EventArgs e)
        {
            treeList1.Redraw();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            string[] aUsers;
            treeList1.GetSelectedUsers(out aUsers);
            if (aUsers.Length>0)
                foreach (var u in aUsers)
                {
                    AppHandler.CtsServerManager.SendLogout(u);
                    UnregisterStudent(u);
                }
        }
    }
}

