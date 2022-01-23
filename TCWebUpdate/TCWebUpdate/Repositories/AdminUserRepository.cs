using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using DevExpress.Utils.OAuth.Provider;
using MySql.Data.MySqlClient;

namespace TCWebUpdate.Repositories
{
    public sealed class AdminUserRepository
    {
        private static readonly Lazy<AdminUserRepository> lazy = new Lazy<AdminUserRepository>(() => new AdminUserRepository());
        public static AdminUserRepository Instance { get { return lazy.Value; } }
        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        private AdminUserRepository()
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
            }
            catch (Exception ex)
            {
#if USE_LOCALHOST
                MessageBox.Show(ex.Message);
#endif
            }
            Connection.Close();
        }

        public bool FindUser(string surname, string lastname, string email)
        {
            string strSQLQuery = "select email from adminuser where surname=@SurName and lastname=@LastName";

            string strRes = "";

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            
            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@SurName", surname);
                cmd.Parameters.AddWithValue("@LastName", lastname);

                try
                {
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        strRes = Convert.ToString(obj);
                        if (strRes == email)
                            return true;
                    }
                }
                catch (Exception ex)
                {
#if USE_LOCALHOST
                    MessageBox.Show(ex.Message);
#endif
                }
            }
            Connection.Close();
            return false;
        }

        public bool FindUser(string strEMail, out string strPassword)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            bool bRes = false;
            strPassword = "";

            string strSQL = "select password from adminuser where email=@EMail";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@EMail", strEMail);

                    try
                    {
                        var obj = cmd.ExecuteScalar();
                        if (obj != null)
                        {
                            string strResult = Convert.ToString(obj);
                            if (strResult.Length > 0)
                            {
                                bRes = true;
                                strPassword = strResult;
                            }
                            else
                            {
                                bRes = false;
                                strPassword = "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
#if USE_LOCALHOST
                        MessageBox.Show(ex.Message);
#endif
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

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

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