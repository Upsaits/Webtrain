using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace TCWebUpdate.Repositories
{
    public sealed class LaunchPackageRepository
    {
        private static readonly Lazy<LaunchPackageRepository> lazy = new Lazy<LaunchPackageRepository>(() => new LaunchPackageRepository());
        public static LaunchPackageRepository Instance { get { return lazy.Value; } }
        private static ServerInfoRepository serverInfoRepo = ServerInfoRepository.Instance;
        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        private LaunchPackageRepository()
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


        public string[] GetAllPackages(int licenseId, string userName)
        {
            var aResults = new  List<string>();
            int iLocationId;

            if (Connection.State == ConnectionState.Open)
                return new string[] {};

            serverInfoRepo.GetLocationIdByLicenseId(licenseId, out iLocationId);

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            string strSQL = "select * " +
                            "from launchpackage " +
                            "where locationId=@LocationId and " +
                            "username=@UserName and " +
                            "isActive=1";

            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@LocationId", iLocationId);

                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    try
                    {
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            foreach (DataRow r in dt.Rows)
                                aResults.Add(r["packageName"].ToString());
                        }
                    }
                    catch (MySqlException sqlEx)
                    {
#if USE_LOCALHOST
                        MessageBox.Show(sqlEx.Message);
#endif
                    }
                }

                if (aResults.Count > 0)
                {
                    strSQL = "update launchpackage " +
                             "set isActive=0 " +
                             "where locationId=@LocationId and " +
                             "username=@UserName and isActive=1";
                    try
                    {
                        using (MySqlCommand cmd2 = new MySqlCommand(strSQL))
                        {
                            cmd2.Connection = Connection;
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Parameters.AddWithValue("@UserName", userName);
                            cmd2.Parameters.AddWithValue("@LocationId", iLocationId);
                            cmd2.ExecuteNonQuery();
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
            return aResults.ToArray();
        }


        public bool AddPackage(int licenseId, string userName, string packageName)
        {
            int iLocationId;
            bool bRes = false;


            serverInfoRepo.GetLocationIdByLicenseId(licenseId, out iLocationId);

            if (iLocationId >= 0)
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                string strSQL = "insert into launchpackage values (0, @locationId , @Username, @PackageName, 1, 0)";

                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(strSQL))
                    {
                        cmd.Connection = Connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LocationId", iLocationId);
                        cmd.Parameters.AddWithValue("@Username", userName);
                        cmd.Parameters.AddWithValue("@PackageName", packageName);

                        cmd.ExecuteNonQuery();
                        bRes = true;
                    }
                }
                catch (System.Exception sqlEx)
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