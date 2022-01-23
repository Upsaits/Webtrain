using System;
using System.Drawing;
using WebtrainWebPortal.Forms;
using Wisej.Web;


namespace WebtrainWebPortal.Views
{
    public partial class LoginPage : Page
    {
        public WorkflowMediator WorkflowMediator
        {
            get => Application.Session.workflowMediator;
        }

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginPage_ResponsiveProfileChanged(object sender, ResponsiveProfileChangedEventArgs e)
        {
            //AlertBox.Show($"ResponsiveProfileChanged: von {e.PreviousProfile.Name} zu {e.CurrentProfile.Name}");

            if (e.CurrentProfile.Name == "Phone")
                this.flowLayoutPanel1.Width = e.CurrentProfile.MaxWidth;
            else
            {
                this.flowLayoutPanel1.Width = 360;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var ui = WorkflowMediator.TryToLogin(textBoxUserEmail.Text, txtBoxPasswort.Text);
            if (ui!=null)
            {
                //Hide();
                WorkflowMediator.SetActiveUser(ui);
                WorkflowMediator.SetViewState(ui,eViewState.eTimeRecording);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //WorkflowMediator.ViewState = WorkflowMediator.eViewState.eRegister;
            using (var dlg = new FrmRegister())
            {
                // show the dialog and wait for it to close.
                if (dlg.ShowDialog() == Wisej.Web.DialogResult.OK)
                {
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var dlg = new FrmChangePassword())
            {
                // show the dialog and wait for it to close.
                if (dlg.ShowDialog() == Wisej.Web.DialogResult.OK)
                {
                }
            }
        }
    }
}
