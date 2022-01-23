using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SoftObject.TrainConcept.Libraries;

namespace TCWebUpdate.Repositories
{
    public class WebtrainPackageRepository
    {
        private static readonly Lazy<WebtrainPackageRepository> lazy = new Lazy<WebtrainPackageRepository>(() => new WebtrainPackageRepository());
        public static WebtrainPackageRepository Instance { get { return lazy.Value; } }
        public string ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        private WebtrainPackageRepository()
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

        public bool AddPackage(int licenseId, WebtrainPackageInfo wpi)
        {
            string strSQLQuery = "insert into wtpackage values (0, @licenseId , @Autor, @Title, @Description, @FileName, @CreatedOn, @languageId, @type , 0, @Data)";
            bool bRes = false;

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            using (MySqlCommand cmd = new MySqlCommand(strSQLQuery, Connection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@licenseId", licenseId);
                cmd.Parameters.AddWithValue("@Autor", wpi.Autor);
                cmd.Parameters.AddWithValue("@Title", wpi.Title);
                cmd.Parameters.AddWithValue("@Description", wpi.Description);
                cmd.Parameters.AddWithValue("@FileName", wpi.ContentFilename);
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmd.Parameters.AddWithValue("@languageId", wpi.Language);
                cmd.Parameters.AddWithValue("@type", wpi.PackageType);
                cmd.Parameters.AddWithValue("@data", wpi.Data);
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

        public WebtrainPackageInfo[] GetAllPackageInfos(int iLicenseId)
        {
            var aResults = new List<WebtrainPackageInfo>();

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            string strSQL = "select * " +
                            "from wtpackage " +
                            "where licenseId=@LicenseId";

            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.Parameters.AddWithValue("@LicenseId", iLicenseId);

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
                                WebtrainPackageInfo wpi = new WebtrainPackageInfo();
                                wpi.Autor = r["autor"].ToString();
                                wpi.ContentFilename = r["filename"].ToString();
                                wpi.Description = r["description"].ToString();
                                wpi.Title = r["title"].ToString();
                                wpi.PackageType = r["type"].ToString();
                                wpi.Data = r["data"].ToString();
                                aResults.Add(wpi);
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
            return aResults.ToArray();
        }

        public WebtrainPackageInfo[] GetLearnmapsToTransfer(int iLicenseId)
        {
            var aResults = new List<WebtrainPackageInfo>();

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            string strSQL = "select * " +
                            "from wtpackage " +
                            "where licenseId=@LicenseId " +
                            "and type='Learnmap' and transmitFlag=0";

            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.Parameters.AddWithValue("@LicenseId", iLicenseId);

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
                                WebtrainPackageInfo wpi = new WebtrainPackageInfo();
                                wpi.Autor = r["autor"].ToString();
                                wpi.ContentFilename = r["filename"].ToString();
                                wpi.Description = r["description"].ToString();
                                wpi.Title = r["title"].ToString();
                                wpi.PackageType = r["type"].ToString();
                                wpi.Data = r["data"].ToString();
                                aResults.Add(wpi);
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

                if (aResults.Count > 0)
                {
                    strSQL = "update wtpackage " +
                             "set transmitFlag=1 " +
                             "where licenseId=@LicenseId " +
                             "and type='Learnmap' and transmitFlag=0";
                    try
                    {
                        using (MySqlCommand cmd2 = new MySqlCommand(strSQL))
                        {
                            cmd2.Connection = Connection;
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Parameters.AddWithValue("@licenseId", iLicenseId);
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

        public bool DeletePackageInfo(int iLicenseId, string strFilename)
        {
            bool bRes = false;

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            string strSQL = "delete from wtpackage " +
                            "where licenseId=@LicenseId and filename=@Filename";

            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.Parameters.AddWithValue("@LicenseId", iLicenseId);
                cmd.Parameters.AddWithValue("@Filename", strFilename);

                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
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
            }

            Connection.Close();
            return bRes;
        }


    }
}