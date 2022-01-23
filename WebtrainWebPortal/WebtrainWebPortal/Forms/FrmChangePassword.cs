using System;
using Wisej.Web;

namespace WebtrainWebPortal.Forms
{
    public partial class FrmChangePassword : Form
    {
        public WebtrainWebPortal.WorkflowMediator WorkflowMediator
        {
            get => Application.Session.workflowMediator;
        }

        public FrmChangePassword()
        {
            InitializeComponent();
        }

        private void btnNewPwdOk_Click(object sender, EventArgs e)
        {
            if (WorkflowMediator.TryToSetNewPassword(txtNewPwdEmail.Text, txtNewPassword.Text, txtConfirmNewPassword.Text))
            {
                Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void btnNewPwdCancel_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }
    }
}
