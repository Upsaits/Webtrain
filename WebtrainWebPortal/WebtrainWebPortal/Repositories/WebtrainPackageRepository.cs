using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;
using Application = Wisej.Web.Application;

namespace WebtrainWebPortal
{
    public class WebtrainPackageRepository
    {

        private static readonly Lazy<WebtrainPackageRepository> lazy = new Lazy<WebtrainPackageRepository>(() => new WebtrainPackageRepository());
        public static WebtrainPackageRepository Instance { get { return lazy.Value; } }
        public MySqlConnection Connection
        {
            get => Application.Session.workflowMediator.Connection;
        }

        private WebtrainPackageRepository()
        {
        }

        public bool AddPackage(int licenseId, WebtrainPackageInfo wpi)
        {
            string strSQLQuery = "insert into wtpackage values (0, @licenseId , @Autor, @Title, @Description, @FileName, @CreatedOn, @languageId, @type , 0, @Data)";
            bool bRes = false;

            Connection.Open();
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
                    Connection.Close();
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

            Connection.Open();

            string strSQL = "select * " +
                            "from wtpackage " +
                            "where licenseId=@LicenseId";

            using (MySqlCommand cmd = new MySqlCommand(strSQL))
            {
                cmd.Connection = Connection;
                cmd.Parameters.AddWithValue("@LicenseId", iLicenseId);

                using (MySqlDataAdapter sda = new MySqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        foreach (DataRow r in dt.Rows)
                        {
                            WebtrainPackageInfo wpi= new WebtrainPackageInfo();
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
                Connection.Close();
            }
            return aResults.ToArray();
        }

        public WebtrainPackageInfo[] GetLearnmapsToTransfer(int iLicenseId)
        {
            var aResults = new List<WebtrainPackageInfo>();

            Connection.Open();

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
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Connection.Close();
            }
            return aResults.ToArray();
        }


    }
}