using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;
using Application = Wisej.Web.Application;

namespace WebtrainWebPortal
{
    public sealed class AdminUserRepository
    {
        private static readonly Lazy<AdminUserRepository> lazy = new Lazy<AdminUserRepository>(() => new AdminUserRepository());
        public static AdminUserRepository Instance { get { return lazy.Value; } }
        public string ConnectionString { get; set; }

        public MySqlConnection Connection
        {
            get => Application.Session.workflowMediator.Connection;
        }

        private AdminUserRepository()
        {
        }

        public bool FindUser(string surname, string lastname, string email)
        {
            string strSQLQuery = "select email from adminuser where surname=@SurName and lastname=@LastName";

            string strRes = "";
            Connection.Open();
            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@SurName", surname);
                cmd.Parameters.AddWithValue("@LastName", lastname);
                var obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    strRes = Convert.ToString(obj);
                    if (strRes == email)
                        return true;
                }
            }
            Connection.Close();
            return false;
        }

        public bool FindUser(string strEMail, out string strPassword,out string strSurname,out string strLastname, out string strUsername)
        {
            Connection.Open();

            bool bRes = false;
            strPassword = "";
            strSurname = "";
            strLastname = "";
            strUsername = "";

            string strSQL = "select password,surname,lastname,username from adminuser where email=@EMail";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@EMail", strEMail);

                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            foreach (DataRow r in dt.Rows)
                            {
                                strPassword = r["password"].ToString();
                                strSurname = r["surname"].ToString();
                                strLastname = r["lastname"].ToString();
                                strUsername = r["username"].ToString();
                                bRes = true;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Connection.Close();
            return bRes;
        }


        public bool CreateUser(string surname, string lastname, string username, string password, string email)
        {
            string strSQLQuery =
                "insert into adminuser values (0, @Surname, @Lastname, @Username, @Password, @Email)";
            bool bRes = false;
            Connection.Open();
            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.Parameters.AddWithValue("@Lastname", lastname);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@EMail", email);

                try
                {
                    cmd.ExecuteNonQuery();
                    bRes = true;
                }
                catch (MySqlException sqlEx)
                {
    #if USE_LOCALHOST
                    MessageBox.Show(sqlEx.Message);
    #endif
                }
            }

            Connection.Close();
            return bRes;
        }
    }
}