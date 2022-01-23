using System;
using System.Windows.Forms;
using SoftObject.TrainConcept.Controls;

namespace SoftObject.TrainConcept.Forms
{
    public partial class XFrmUsersChoose : DevExpress.XtraEditors.XtraForm
    {
        public XFrmUsersChoose()
        {
            InitializeComponent();
            this.xUsersTree1.UpdateData();
        }

        public XFrmUsersChoose(string [] aCheckedUserNames)
        {
            InitializeComponent();

            string[] aUsers;
            Program.AppHandler.UserManager.GetUserNames(out aUsers);
            this.xUsersTree1.UserList = aUsers;
            this.xUsersTree1.SetCheckedUsers(aCheckedUserNames);
            this.xUsersTree1.Type = XUsersTree.ViewType.EditView;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public int GetCheckedUsers(out string[] aUsers)
        {
            return this.xUsersTree1.GetCheckedUsers(out aUsers);
        }
    }
}
