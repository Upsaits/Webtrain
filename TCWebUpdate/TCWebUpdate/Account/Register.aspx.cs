using System;
using System.Configuration;
using System.Web.Security;
using MySql.Data.MySqlClient;
using TCWebUpdate.Repositories;

namespace TCWebUpdate.Account
{
    public partial class Register : System.Web.UI.Page 
    {
        private static AdminUserRepository m_adminUserRepo = AdminUserRepository.Instance;
        private static UserRepository m_userRepo = UserRepository.Instance;

        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        protected void Page_Load(object sender, EventArgs e) 
        {
            var serverInfoRep = ServerInfoRepository.Instance;

#if USE_LOCALHOST
            ConnectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;
#else
#if USE_SMARTERASPNET
            ConnectionString = ConfigurationManager.ConnectionStrings["smarteraspnetConnection"].ConnectionString;
#else
            ConnectionString = ConfigurationManager.ConnectionStrings["world4YouConnection"].ConnectionString;
#endif
#endif
            Connection = new MySqlConnection(ConnectionString);

            try
            {
                Connection.Open();
                Connection.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                tbEmail.ErrorText = ex.Message;
                tbEmail.IsValid = false;
            }

        }

        protected void btnCreateUser_Click(object sender, EventArgs e) 
        {
            /*
            if (tbPassword.Text != tbConfirmPassword.Text)
            {
                tbConfirmPassword.ErrorText = "Passwort stimmt nicht überein!";
                tbConfirmPassword.IsValid = false;
            }
            else
            {
                bool bUserExists = m_adminUserRepo.FindUser(tbFirstName.Text, tbLastName.Text, tbEmail.Text);
                if (!bUserExists)
                {
                    string strPassWord = "";
                    string strUserName = "";
                    bUserExists = m_userRepo.FindUser(1,tbEmail.Text, out strPassWord,out strUserName);
                }

                if (bUserExists)
                {
                    tbEmail.ErrorText = "Benutzer bereits registriert!";
                    tbEmail.IsValid = false;
                }
                else
                {
                    bool bRes = m_userRepo.CreateUser(1,tbFirstName.Text,tbMiddleName.Text,tbLastName.Text,"",true,"LAPAV1120",tbEmail.Text);
                    if (bRes)
                        Response.Redirect("~/Account/RegisterSuccess.aspx");
                }
            }*/

        }
    }
}