using System;
using System.Text.RegularExpressions;
using Wisej.Web;

namespace WebtrainWebPortal.Forms
{
    public partial class FrmRegister : Form
    {
        public WebtrainWebPortal.WorkflowMediator WorkflowMediator
        {
            get => Application.Session.workflowMediator;
        }

        public FrmRegister()
        {
            InitializeComponent();
        }

        private void btnRegRegister_Click(object sender, EventArgs e)
        {
            if (WorkflowMediator.TryToRegister(1,txtFirstname.Text, txtMiddlename.Text, txtLastname.Text,
                txtEmail.Text, txtRegPassword.Text, txtRegConfPassword.Text))
            {
                Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void btnRegCancel_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }

        private void txtFirstname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = txtFirstname.Text.Trim();
            if (name.Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstname, "Geben sie bitte einen gültigen Vornamen ein.");
            }
            else
            {
                if (!Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$"))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtFirstname, "Vorname nicht gültig!");
                }
                else
                {
                    name = name.Replace("'", "&#39;");
                    errorProvider1.SetError(txtFirstname, "");
                }
            }
        }

        private void txtMiddlename_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = txtMiddlename.Text.Trim();
            if (name.Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMiddlename, "Gebn sie bitte einen gültigen Mittelname ein.");
            }
            else
            {
                if (!Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$"))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtMiddlename, "Mittelname nicht gültig!");
                }
                else
                {
                    name = name.Replace("'", "&#39;");
                    errorProvider1.SetError(txtMiddlename, "");
                }
            }
        }

        private void txtLastname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = txtLastname.Text.Trim();
            if (name.Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastname, "Geben sie bitte einen gültigen Nachnamen ein.");
            }
            else
            {
                if (!Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$"))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtLastname, "Nachname nicht gültig!");
                }
                else
                {
                    name = name.Replace("'", "&#39;");
                    errorProvider1.SetError(txtLastname, "");
                }
            }
        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = txtEmail.Text.Trim();
            if (name.Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Geben sie bitte eine eMail-Adresse ein.");
            }
            else
            {
                if (!WorkflowMediator.IsValidEmail(name))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtEmail, "e-Mail Adresse nicht gültig!");
                }
                else
                    errorProvider1.SetError(txtEmail, "");
            }
        }

        private void txtRegPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = txtRegPassword.Text.Trim();
            if (name.Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRegPassword, "Gebn sie bitte ein Passwort ein.");
            }
            else
            {
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasSpecialChars = new Regex(@"[#?!@$%^&*-]+");
                var hasMinimum8Chars = new Regex(@".{8,}");

                if (!hasNumber.IsMatch(name))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtRegPassword, "Passwort muß Zahlen enthalten!");
                }
                else if (!hasUpperChar.IsMatch(name))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtRegPassword, "Passwort muß Grossbuchstabe enthalten!");
                }
                else if (!hasMinimum8Chars.IsMatch(name))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtRegPassword, "Passwort muß mindestens 8 Zeichen enthalten!");
                }
                else if (!hasSpecialChars.IsMatch(name))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtRegPassword, "Passwort muß Spezialzeichen enthalten!");
                }
                else
                    errorProvider1.SetError(txtRegPassword, "");

            }
        }

        private void txtRegConfPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = txtRegConfPassword.Text;
            if (name.Length == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRegConfPassword, "Wiederholen sie bitte das Passwort.");
            }
            else
            {
                if (name!=txtRegPassword.Text)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtRegConfPassword, "Passwort stimmt nicht überein!");
                }
                else
                {
                    errorProvider1.SetError(txtRegConfPassword, "");
                }
            }
        }

        private void txtFirstname_Leave(object sender, EventArgs e)
        {
            if (btnRegCancel.Focused)
                this.AutoValidate = AutoValidate.Disable;
        }

        private void txtMiddlename_Leave(object sender, EventArgs e)
        {
            if (btnRegCancel.Focused)
                this.AutoValidate = AutoValidate.Disable;
        }

        private void txtLastname_Leave(object sender, EventArgs e)
        {
            if (btnRegCancel.Focused)
                this.AutoValidate = AutoValidate.Disable;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (btnRegCancel.Focused)
                this.AutoValidate = AutoValidate.Disable;
        }

        private void txtRegPassword_Leave(object sender, EventArgs e)
        {
            if (btnRegCancel.Focused)
                this.AutoValidate = AutoValidate.Disable;
        }

        private void txtRegConfPassword_Leave(object sender, EventArgs e)
        {
            if (btnRegCancel.Focused)
                this.AutoValidate = AutoValidate.Disable;
        }
    }
}
