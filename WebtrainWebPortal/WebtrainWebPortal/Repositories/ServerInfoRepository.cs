using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;
using Application = Wisej.Web.Application;

namespace WebtrainWebPortal 
{
    public sealed class ServerInfoRepository
    {
        private static readonly Lazy<ServerInfoRepository> lazy = new Lazy<ServerInfoRepository> (() => new ServerInfoRepository());
        public static ServerInfoRepository Instance { get { return lazy.Value; } }

        public MySqlConnection Connection
        {
            get => Application.Session.workflowMediator.Connection;
        }

        private ServerInfoRepository()
        {

        }

        public string[] GetAllServerInfos()
        {
            var aResults = new List<string>();
            Connection.Open();
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM wtserver"))
            {
                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    cmd.Connection = Connection;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
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

                        Connection.Close();
                        //return dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                        return aResults.ToArray();
                    }
                }
            }
        }


        public string[] GetServerInfos(int locationId)
        {
            var aResults = new List<string>();
            Connection.Open();
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

                        Connection.Close();
                        //return dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                        return aResults.ToArray();
                    }
                }
            }
        }

        public bool GetServerIPAddress(int licenseId, out string strIPAddress)
        {
            Connection.Open();

            strIPAddress = "";
            bool bRes = false;

            string strSQL = "select s.ipadress " +
                            "from wtserver s join location l ON l.id = s.locationId " +
                            "where s.licenseId=@licenseId";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@licenseId", licenseId);
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
            }
            catch (System.Exception /*ex*/)
            {

            }
            Connection.Close();
            return bRes;
        }


        public bool GetLicenseIdByServerIPAddress(string strIPAddress, out int licenseId)
        {
            Connection.Open();

            licenseId = -1;
            bool bRes = false;

            string strSQL = "select s.licenseId " +
                            "from wtserver s join location l ON l.id = s.locationId " +
                            "where s.ipadress=@strIPAdress";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(strSQL))
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@strIPAdress", strIPAddress);
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
            }
            catch (System.Exception /*ex*/)
            {

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
                Connection.Open();

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
                catch (System.Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                Connection.Close();
            }

            return bRes;
        }

        public bool GetLocationIdByLicenseId(int iLicenseId, out int iLocationId)
        {
            Connection.Open();

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
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Connection.Close();
            return bRes;
        }
    }
}