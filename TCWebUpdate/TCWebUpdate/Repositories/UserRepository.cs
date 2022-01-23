using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.WebPages;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Native;
using MySql.Data.MySqlClient;

namespace TCWebUpdate.Repositories
{
    public sealed class UserRepository
    {

        private static readonly Lazy<UserRepository> lazy = new Lazy<UserRepository> (() => new UserRepository());
        public static UserRepository Instance { get { return lazy.Value; } }
        private static ServerInfoRepository serverInfoRepo = ServerInfoRepository.Instance;
        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        private UserRepository()
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
            catch (MySqlException sqlEx)
            {
#if USE_LOCALHOST
                MessageBox.Show(sqlEx.Message);
#endif
            }
            Connection.Close();
        }

        public string[] GetAllUserInfo(int iLicenseId)
        {
            var aResults = new List<string>();

            int iLocationId;
            serverInfoRepo.GetLocationIdByLicenseId(iLicenseId, out iLocationId);

            if (iLocationId >= 0)
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string strSQL = "select * " +
                                "from wtuser " +
                                "where locationId = @LocationId";

                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.Parameters.AddWithValue("@LocationId", iLocationId);

                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            try
                            {
                                sda.Fill(dt);
                                foreach (DataRow r in dt.Rows)
                                {
                                    string strResult = r["surName"].ToString() + ',' +
                                                       r["middleName"].ToString() + ',' +
                                                       r["lastName"].ToString() + ',' +
                                                       r["degree"].ToString() + ',' +
                                                       r["userName"].ToString() + ',' +
                                                       r["password"].ToString() + ',' +
                                                       r["activated"].ToString() + ',' +
                                                       r["className"].ToString() + ',' +
                                                       r["admin"].ToString();
                                    Console.WriteLine(strResult);
                                    aResults.Add(strResult);
                                }
                            }
                            catch (MySqlException sqlEx)
                            {
#if USE_LOCALHOST
                                MessageBox.Show(sqlEx.Message);
#endif
                            }
                        }
                    }
                }
            }

            Connection.Close();
            return aResults.ToArray();
        }


        public int FindUser(int locationId, string surName, string middleName, string lastName, string degree)
        {
            string strSQLQuery = "select id from wtuser where surname=@Surname and lastname=@LastName and locationId=@LocationId";
            if (!middleName.IsEmpty())
            {
                strSQLQuery = "select id from wtuser where surname=@Surname and middlename=@Middlename and lastname=@LastName and locationId=@LocationId";
            }

            int iRes = -1;
            if (lastName.IndexOf("Schneeweiß") >= 0)
            {
                iRes = -2;
            }

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery,Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LocationId", locationId);
                cmd.Parameters.AddWithValue("@Surname", surName);
                cmd.Parameters.AddWithValue("@Lastname", lastName);
                if (!middleName.IsEmpty())
                    cmd.Parameters.AddWithValue("@Middlename", middleName);

                try
                {
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        if (Convert.ToInt32(obj) >= 0)
                        {
                            iRes = Convert.ToInt32(obj);
                        }
                    }
                }
                catch (MySqlException sqlEx)
                {
#if USE_LOCALHOST
                    MessageBox.Show(sqlEx.Message);
#endif
                }

            }
            Connection.Close();
            return iRes;
        }


        public int FindUser(int locationId, string userName)
        {
            string strSQLQuery = "select id from wtuser where username=@Username and locationId=@LocationId";

            int iRes = -1;

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LocationId", locationId);
                cmd.Parameters.AddWithValue("@Username", userName);

                try
                {
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        if (Convert.ToInt32(obj) >= 0)
                        {
                            iRes = Convert.ToInt32(obj);
                        }
                    }
                }
                catch (MySqlException sqlEx)
                {
#if USE_LOCALHOST
                    MessageBox.Show(sqlEx.Message);
#endif
                }
            }
            Connection.Close();
            return iRes;
        }

        public bool FindUser(string strEMail, out string strPassword, out string strUserName, out int iLocationId, out int iAdmin)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            bool bRes = false;
            strPassword = "";
            strUserName = "";
            iLocationId = 2;
            iAdmin = 0;

            string strSQL = "select password,username,locationId,admin from wtuser where email=@EMail";

            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EMail", strEMail);

                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    try
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            foreach (DataRow r in dt.Rows)
                            {
                                strPassword = r["password"].ToString();
                                strUserName = r["username"].ToString();
                                iLocationId = Convert.ToInt32(r["locationId"].ToString());
                                iAdmin = Convert.ToInt32(r["admin"].ToString());
                                bRes = true;
                            }
                        }
                    }
                    catch (MySqlException sqlEx)
                    {
#if USE_LOCALHOST
                        MessageBox.Show(sqlEx.Message);
#endif
                    }
                }
            }

            Connection.Close();
            return bRes;
        }


        public bool CreateUser(int locationId, string surName, string middleName, string lastName, string degree, bool bIsActive, string className, string email, bool bIsAdmin)
        {
            string strUserName = lastName + surName.Substring(0, 2);
            if (FindUser(locationId,strUserName) >= 0)
            {
                if (surName.Length > 2)
                    strUserName = lastName + surName.Substring(0, 3);
                else
                    strUserName = lastName + surName + "2";
            }

            strUserName = convert_uml(strUserName);

            string strSQLQuery = "insert into wtuser values (0, @locationId , @Surname, @Middlename, @Lastname, @Degree, @Username, @Password, @IsActive, @ClassName ,@EMail, 0, @IsAdmin)";
            bool bRes = false;

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@locationId", locationId);
                cmd.Parameters.AddWithValue("@Surname", surName);
                cmd.Parameters.AddWithValue("@Middlename", middleName);
                cmd.Parameters.AddWithValue("@Lastname", lastName);
                cmd.Parameters.AddWithValue("@Degree", degree);
                cmd.Parameters.AddWithValue("@Username", strUserName.ToLower());
                cmd.Parameters.AddWithValue("@Password", "");
                cmd.Parameters.AddWithValue("@IsActive", bIsActive ? 1 : 0);
                cmd.Parameters.AddWithValue("@ClassName", className);
                cmd.Parameters.AddWithValue("@EMail", email);
                cmd.Parameters.AddWithValue("@IsAdmin", bIsAdmin);

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

        public bool UpdateUser(int locationId, int userId, string surName, string middleName, string lastName, string degree, bool bIsActive, string className, string email)
        {
            string strSQLQuery = "update wtuser set surname=@Surname,middlename=@Middlename,lastname=@Lastname,degree=@Degree,activated=@IsActive,classname=@ClassName,email=@Email where locationId=@LocationId and id=@UserId";
            bool bRes = false;

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@LocationId", locationId);
                cmd.Parameters.AddWithValue("@Surname", surName);
                cmd.Parameters.AddWithValue("@Middlename", middleName);
                cmd.Parameters.AddWithValue("@Lastname", lastName);
                cmd.Parameters.AddWithValue("@Degree", degree);
                cmd.Parameters.AddWithValue("@Username", "");
                cmd.Parameters.AddWithValue("@Password", "");
                cmd.Parameters.AddWithValue("@IsActive", bIsActive ? 1 : 0);
                cmd.Parameters.AddWithValue("@ClassName", className);
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

        public bool SetPassword(int iLicenseId, string strUsername, string strPassword)
        {
            int iLocationId;
            serverInfoRepo.GetLocationIdByLicenseId(iLicenseId, out iLocationId);

            if (iLocationId >= 0)
            {
                string strSQLQuery = "update wtuser set password=@Password where username=@UserName and locationId=@LocationId";
                bool bRes = false;

                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserName", strUsername);
                    cmd.Parameters.AddWithValue("@LocationId", iLocationId);
                    cmd.Parameters.AddWithValue("@Password", strPassword);

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

            Connection.Close();
            return false;
        }

        public bool DeleteAllUsers(int iLocationId)
        {
            string strSQLQuery = "delete from wtuser where locationId=@LocationId";
            bool bRes = false;

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LocationId", iLocationId);

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

        public static string convert_uml(string old)
        {
            old = old.Replace("ä", "ae");
            old = old.Replace("ö", "oe");
            old = old.Replace("ü", "ue");
            return (old);
        }

    }
}