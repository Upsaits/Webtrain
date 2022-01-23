using System;
using System.Configuration;
using System.Data;
using System.Web.SessionState;
using MySql.Data.MySqlClient;
using TCWebUpdate.Repositories;

namespace TCWebUpdate.Account
{
    public partial class Login : System.Web.UI.Page 
    {
        private static AdminUserRepository m_adminUserRepo = AdminUserRepository.Instance;
        private static UserRepository m_userRepo = UserRepository.Instance;
        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        protected void Page_Load(object sender, EventArgs e) 
        {

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
                tbEMail.ErrorText = ex.Message;
                tbEMail.IsValid = false;
            }
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strPassword;
            string strUserName = "";
            bool bIsAdmin = true;
            int iLocationId=0;

            bool bUserExists = m_adminUserRepo.FindUser(tbEMail.Text, out strPassword);

            if (!bUserExists)
            {
                bUserExists = m_userRepo.FindUser(tbEMail.Text, out strPassword,out strUserName,out iLocationId,out int iAdmin);
                bIsAdmin = (iAdmin==1);
            }

            if (bUserExists)
            {
                if (strPassword != tbPassword.Text)
                {
                    tbPassword.ErrorText = "Passwort falsch!";
                    tbPassword.IsValid = false;
                }
                else
                {
                    Session["Email"] = tbEMail.Text;
                    Session["Username"] = strUserName;
                    if (iLocationId>0)
                        Session["LocationId"] = iLocationId;

                    if (bIsAdmin)
                        Response.Redirect("~/CampusPage.aspx");
                    else
                        Response.Redirect("~/DownloadsPage.aspx");
                }
            }
            else 
            {
                tbEMail.ErrorText = "Benutzer nicht registriert";
                tbEMail.IsValid = false;
            }
        }
    }
}