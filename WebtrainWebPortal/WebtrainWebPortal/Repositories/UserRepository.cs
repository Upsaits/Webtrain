using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using MySqlConnector;
using WebtrainWebPortal.Wrappers;
using Wisej.Web;
using Application = Wisej.Web.Application;

namespace WebtrainWebPortal
{
    public sealed class UserRepository
    {
        private static readonly Lazy<UserRepository> lazy = new Lazy<UserRepository> (() => new UserRepository());
        public static UserRepository Instance { get { return lazy.Value; } }
        private static ServerInfoRepository serverInfoRepo = ServerInfoRepository.Instance;


        public MySqlConnection Connection
        {
            get => Application.Session.workflowMediator.Connection;
        }

        private UserRepository()
        {
        }

        public string[] GetAllUserInfo(int iLicenseId)
        {
            var aResults = new List<string>();

            int iLocationId;
            serverInfoRepo.GetLocationIdByLicenseId(iLicenseId, out iLocationId);

            if (iLocationId >= 0)
            {
                Connection.Open();

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
                                                   r["className"].ToString();
                                Console.WriteLine(strResult);
                                aResults.Add(strResult);
                            }
                        }
                    }

                    Connection.Close();
                }
            }
            return aResults.ToArray();
        }


        public int FindUser(int locationId, string surName, string middleName, string lastName, string degree)
        {
            string strSQLQuery = "select id from wtuser where surname=@Surname and lastname=@LastName and locationId=@LocationId";
            if (middleName.Length <= 0)
            {
            }
            else
            {
                strSQLQuery = "select id from wtuser where surname=@Surname and middlename=@Middlename and lastname=@LastName and locationId=@LocationId";
            }

            int iRes = -1;
            if (lastName.IndexOf("Schneeweiß") >= 0)
            {
                iRes = -2;
            }
            Connection.Open();
            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery,Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LocationId", locationId);
                cmd.Parameters.AddWithValue("@Surname", surName);
                cmd.Parameters.AddWithValue("@Lastname", lastName);
                if (middleName.Length>0)
                    cmd.Parameters.AddWithValue("@Middlename", middleName);

                var obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        iRes=Convert.ToInt32(obj);
                    }
                }
            }
            Connection.Close();
            return iRes;
        }


        public int FindUser(int locationId, string userName)
        {
            string strSQLQuery = "select id from wtuser where username=@Username and locationId=@LocationId";

            int iRes = -1;
            Connection.Open();
            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LocationId", locationId);
                cmd.Parameters.AddWithValue("@Username", userName);
                var obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        iRes = Convert.ToInt32(obj);
                    }
                }
            }
            Connection.Close();
            return iRes;
        }

        public bool FindUser(int locationId, string strEMail, out string strPassword, out string strSurname, out string strMiddlename, out string strLastname, out string strUsername,out string strClassname)
        {
            Connection.Open();

            bool bRes = false;
            strPassword = "";
            strSurname = "";
            strLastname = "";
            strUsername = "";
            strMiddlename = "";
            strClassname = "";

            string strSQL = "select password,surname,middlename,lastname,username,classname from wtuser where email=@EMail and locationId=@LocationId";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@LocationId", locationId);
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
                                strMiddlename = r["middlename"].ToString();
                                strLastname = r["lastname"].ToString();
                                strUsername = r["username"].ToString();
                                strClassname = r["classname"].ToString();
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


        public bool CreateUser(int locationId, string surName, string middleName, string lastName, string degree, bool bIsActive, string className, string email,string password)
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

            string strSQLQuery = "insert into wtuser values (0, @locationId , @Surname, @Middlename, @Lastname, @Degree, @Username, @Password, @IsActive, @ClassName, @EMail, 0)";
            bool bRes = false;
            Connection.Open();
            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@locationId", locationId);
                cmd.Parameters.AddWithValue("@Surname", surName);
                cmd.Parameters.AddWithValue("@Middlename", middleName);
                cmd.Parameters.AddWithValue("@Lastname", lastName);
                cmd.Parameters.AddWithValue("@Degree", degree);
                cmd.Parameters.AddWithValue("@Username", strUserName.ToLower());
                cmd.Parameters.AddWithValue("@Password", password);
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

        public bool UpdateUser(int locationId, int userId, string surName, string middleName, string lastName, string degree, bool bIsActive, string className, string email)
        {
            string strSQLQuery = "update wtuser set surname=@Surname,middlename=@Middlename,lastname=@Lastname,degree=@Degree,activated=@IsActive,classname=@ClassName,email=@Email where locationId=@LocationId and id=@UserId";
            bool bRes = false;
            Connection.Open();
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
                Connection.Open();
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

            return false;
        }


        public bool SetNewPassword(int iLocationId, string strEMail, string strNewPassword)
        {
            if (iLocationId >= 0)
            {
                string strSQLQuery = "update wtuser set password=@Password where email=@EMail and locationId=@LocationId";
                bool bRes = false;
                Connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@EMail", strEMail);
                    cmd.Parameters.AddWithValue("@LocationId", iLocationId);
                    cmd.Parameters.AddWithValue("@Password", strNewPassword);

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

            return false;
        }
        public bool DeleteAllUsers(int iLocationId)
        {
            string strSQLQuery = "delete from wtuser where locationId=@LocationId";
            bool bRes = false;
            Connection.Open();
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



        public DataTable GetUsersForLocation(int iLocationId)
        {
            String strSql = "select surname as 'Vorname',"+
                             "middlename as '2.Vorname',"+
                             "lastname as 'Nachname'," +
                             "username as 'Benutzername'," +
                             "email as 'E-Mail'," +
                             "activated as 'Aktiv'," +
                             "className as 'Klasse' FROM wtuser ";

            var mySqlDataAdapter = new MySqlDataAdapter(strSql + $"WHERE locationId={iLocationId} and not className='LAPTrainer'", Connection);
            var ds = new DataSet();
            mySqlDataAdapter.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable GetStudentsForLocation(int iLocationId)
        {
            String strSql = "select surname as 'Vorname'," +
                            "middlename as '2.Vorname'," +
                            "lastname as 'Nachname'," +
                            "username as 'Benutzername'," +
                            "email as 'E-Mail'," +
                            "activated as 'Aktiv'," +
                            "className as 'Klasse' FROM wtuser ";

            var mySqlDataAdapter = new MySqlDataAdapter(strSql + $"WHERE locationId={iLocationId} and not className='LAPTrainer'", Connection);
            var ds = new DataSet();
            mySqlDataAdapter.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable GetTrainerForLocation(int iLocationId)
        {
            String strSql = "select surname as 'Vorname'," +
                            "middlename as '2.Vorname'," +
                            "lastname as 'Nachname'," +
                            "username as 'Benutzername'," +
                            "email as 'E-Mail'  FROM wtuser ";

            var mySqlDataAdapter = new MySqlDataAdapter(strSql + $"WHERE locationId={iLocationId} and className='LAPTrainer'", Connection);
            var ds = new DataSet();
            mySqlDataAdapter.Fill(ds);
            return ds.Tables[0];
        }

        public bool CheckUserEntry(string name, string className, string email, bool bActive,out int iCntUpdated,out int  iCntAdded)
        {
            iCntAdded = 0;
            iCntUpdated = 0;

            if (name.Length == 0)
                return false;

            // search for academic title
            string[] names = name.Split(',');
            string strDegree = "";
            if (names.Length > 1)
            {
                strDegree = names[1];
                name = names[0];
            }

            names = name.Split(' ');
            if (names.Length < 2 || names.Length > 3)
                return false;

            if (names.Length == 3)
            {
                if (names[0] == "Al")
                {
                    names[0] += $"_{names[1]}";
                    names[1] = names[2];
                    Array.Resize(ref names,2);
                }
            }

            int userId = -1;
            if (names.Length == 2)
                userId = FindUser(1, names[1], "", names[0], strDegree);
            else
                userId = FindUser(1, names[2], names[1], names[0], strDegree);

            bool bRes = false;
            if (userId < 0)
            {
                if (names.Length == 2)
                    bRes = CreateUser(1, names[1], "", names[0], strDegree, bActive, className, email,"");
                else
                    bRes = CreateUser(1, names[2], names[1], names[0], strDegree, bActive, className, email,"");
               
                ++iCntAdded;
            }
            else
            {
                if (names.Length == 2)
                    bRes = UpdateUser(1, userId, names[1], "", names[0], strDegree, bActive, className, email);
                else
                    bRes = UpdateUser(1, userId, names[2], names[1], names[0], strDegree, bActive, className, email);
               
                ++iCntUpdated;
            }
            return bRes;
        }

        public void AnalyzeExcelSheetForUsers(string strFilePath)
        {
            var spreadsheet = new AspXSpreadsheetWrapper();
            spreadsheet.Open(strFilePath);

            DateTime dt = DateTime.Today;
            var ci = new CultureInfo("DE-de");
            string monthname = ci.DateTimeFormat.GetMonthName(dt.Month);
            string year = (dt.Year - 2000).ToString();
            string actualSheet = monthname;
            string actualSheet1 = ' ' + monthname;
            string actualSheet2 = ' ' + monthname + ' ';

            //AlertBox.Show($"Anzahl der Worksheets={spreadsheet.Document.Worksheets.Count}!");

            if (spreadsheet.Document.Worksheets.Contains(actualSheet1))
                actualSheet = actualSheet1;
            else if (spreadsheet.Document.Worksheets.Contains(actualSheet2))
                actualSheet = actualSheet2;
            
            if (!(spreadsheet.Document.Worksheets.Contains(actualSheet)))
            {
                actualSheet = monthname + ' ' + year;
                if (!spreadsheet.Document.Worksheets.Contains(actualSheet))
                {
                    AlertBox.Show($"{actualSheet} im Excel-Sheet nicht gefunden!");
                    return;
                }
            }

            int FirstRowId = 0;
            int nameColId = 0;
            int classColId = 1;
            int emailColId = -1;
            // all ids 0 based
            if (strFilePath.Contains("Anwesenheitsliste_Auswahl"))
            {
                classColId = 1;
                FirstRowId = 7;
                nameColId = 2;
            }
            else if (strFilePath.Contains("Anwesenheit_TeilnehmerInnen"))
            {
                classColId = 1;
                FirstRowId = 8;
                nameColId = 3;
                emailColId = 6;
            }
            else if (strFilePath.Contains("Anwesenheit_Trainer"))
            {
                classColId = 0;
                FirstRowId = 2;
                nameColId = 1;
                emailColId = 2;
            }
            else
                return;

            bool bExit = false;
            int iCntAdded = 0;
            int iCntUpdated = 0;

            do
            {

                string name = spreadsheet.Document.Worksheets[actualSheet].Columns[nameColId][FirstRowId].DisplayText;
                string className = spreadsheet.Document.Worksheets[actualSheet].Columns[classColId][FirstRowId].DisplayText;
                string email = (emailColId >= 0) ? spreadsheet.Document.Worksheets[actualSheet].Columns[emailColId][FirstRowId].DisplayText : "";
                bool bActive = spreadsheet.Document.Worksheets[actualSheet].Columns[nameColId][FirstRowId].Font.Color.ToArgb().Equals(Color.FromArgb(0, 0, 0, 0).ToArgb());

                //string strTemp = $"Analse: {name},{className},{((bActive) ? "active" : "inactive")}";
                //if (name.IndexOf("Burger") >= 0)
                //{
                //    Console.WriteLine("Halt!");
                //}
                if (name.Length > 0/* && className.All(Char.IsDigit)*/)
                    CheckUserEntry(name.Trim(' '), $"LAP{className}", email, bActive,out iCntUpdated,out iCntAdded);
                else
                    bExit = true;

                ++FirstRowId;

            } while (!bExit);

            AlertBox.Show($"{iCntUpdated} Datensätze aktualisiert,{iCntAdded} Datensätze hinzugefügt!");
        }

        public bool GetUserIdByEmail(string strEmail, out int iUserId)
        {
            Connection.Open();

            bool bRes = false;
            string strSQL = "select id " +
                            "from wtuser " +
                            "where email = @EMail";
            iUserId = -1;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@EMail", strEmail);
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        if (Convert.ToInt32(obj) >= 0)
                        {
                            iUserId = (int)obj;
                            bRes = true;
                        }
                        else
                        {
                            bRes = false;
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


        public bool SetActivated(string strUsername, int iActivated)
        {
            bool bRet = false;
            Connection.Open();
            string strSQL = "update wtuser " +
                            "set activated=@Activated " +
                            "where username=@Username";
            try
            {
                using (MySqlCommand cmd2 = new MySqlCommand(strSQL))
                {
                    cmd2.Connection = Connection;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@Username", strUsername);
                    cmd2.Parameters.AddWithValue("@Activated", iActivated);
                    cmd2.ExecuteNonQuery();
                }
                bRet = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Connection.Close();

            return bRet;
        }


    }
}