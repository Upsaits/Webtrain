using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TCWebUpdate.Repositories
{
    public sealed class ServerInfoRepository
    {
        private static readonly Lazy<ServerInfoRepository> lazy = new Lazy<ServerInfoRepository> (() => new ServerInfoRepository());
        public static ServerInfoRepository Instance { get { return lazy.Value; } }
        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }
    
        private ServerInfoRepository()
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

        public string[] GetAllServerInfos()
        {
            var aResults = new List<string>();

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM wtserver"))
            {
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = Connection;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        try
                        {
                            sda.Fill(dt);
                            foreach (DataRow r in dt.Rows)
                            {
                                string strResult = r["locationId"].ToString() + ',' +
                                                   r["name"].ToString() + ',' +
                                                   r["licenseId"].ToString() + ',' +
                                                   r["ipadress"].ToString();
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

            Connection.Close();
            //return dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
            return aResults.ToArray();
        }


        public string[] GetServerInfos(int locationId)
        {
            var aResults = new List<string>();

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM wtserver WHERE locationId=@LocationId"))
            {
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@LocationId", locationId);

                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        try
                        {
                            sda.Fill(dt);
                            foreach (DataRow r in dt.Rows)
                            {
                                string strResult = r["locationId"].ToString() + ',' +
                                                   r["name"].ToString() + ',' +
                                                   r["licenseId"].ToString() + ',' +
                                                   r["ipadress"].ToString();
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
            Connection.Close();
            //return dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
            return aResults.ToArray();
        }

        public bool GetServerIPAddress(int licenseId, out string strIPAddress)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            strIPAddress = "";
            bool bRes = false;

            string strSQL = "select s.ipadress " +
                            "from wtserver s join location l ON l.id = s.locationId " +
                            "where s.licenseId=@licenseId";

            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@licenseId", licenseId);

                try
                {
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        if (Convert.ToString(obj) != "")
                        {
                            strIPAddress = (string)obj;
                            bRes = true;
                        }
                        else
                        {
                            bRes = false;
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
            return bRes;
        }


        public bool GetLicenseIdByServerIPAddress(string strIPAddress, out int licenseId)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            licenseId = -1;
            bool bRes = false;

            string strSQL = "select s.licenseId " +
                            "from wtserver s join location l ON l.id = s.locationId " +
                            "where s.ipadress=@strIPAdress";


            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@strIPAdress", strIPAddress);

                try
                {
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        if (Convert.ToString(obj) != "")
                        {
                            licenseId = (int)obj;
                            bRes = true;
                        }
                        else
                        {
                            bRes = false;
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
            return bRes;
        }

        public bool SetServerIPAddress(int licenseId, string strIPAddress)
        {
            int iLocationId;
            bool bRes = false;
            GetLocationIdByLicenseId(licenseId, out iLocationId);

            if (iLocationId >= 0)
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string strSQL = "update wtserver " +
                                "set ipadress=@IpAdress " +
                                "where locationId = @LocationId and licenseId=@licenseId";

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL))
                    {
                        cmd.Connection = Connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LocationId", iLocationId);
                        cmd.Parameters.AddWithValue("@licenseId", licenseId);
                        cmd.Parameters.AddWithValue("@IpAdress", strIPAddress);

                        cmd.ExecuteNonQuery();
                        bRes = true;
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
            return bRes;
        }

        public bool GetLocationIdByLicenseId(int iLicenseId, out int iLocationId)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            bool bRes = false;
            string strSQL = "select locationId " +
                            "from wtserver " +
                            "where licenseId = @LicenseId";
            iLocationId = -1;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@LicenseId", iLicenseId);
                    var obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        if (Convert.ToInt32(obj) >= 0)
                        {
                            iLocationId = (int)obj;
                            bRes = true;
                        }
                        else
                        {
                            bRes = false;
                        }
                    }
                }
            }
            catch (MySqlException sqlEx)
            {
#if USE_LOCALHOST
                MessageBox.Show(sqlEx.Message);
#endif
            }

            Connection.Close();
            return bRes;
        }
    }
}