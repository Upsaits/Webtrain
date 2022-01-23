using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using DevExpress.Web;
using Ionic.Zip;
using Newtonsoft.Json;
using SoftObject.TrainConcept.Libraries;
using TCWebUpdate.Models;
using TCWebUpdate.Repositories;

namespace TCWebUpdate
{
    /// <summary>
    /// Summary description for QueryUpdate
    /// </summary>
    [WebService(Namespace = "http://webtrain.softobject.at/webservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QueryUpdate : WebService
    {
        private static readonly Version m_strActualVersion = new Version("3.1.0.26");
        private static readonly string m_strActualUpdateFilename = "P2021-P1-8.zip";
        private static ServerInfoRepository m_serverInfoRepo = ServerInfoRepository.Instance;
        private static WebtrainPackageRepository m_webtrainPackageRepo = WebtrainPackageRepository.Instance;
        private static UserRepository m_userRepo = UserRepository.Instance;
        private static LaunchPackageRepository m_launchPkgRepo = LaunchPackageRepository.Instance;
        private static BinaryWriter m_binWriter = null;
        private static string m_strLocalFilename="";

        public static ServerInfoRepository ServerInfoRepo { get { return m_serverInfoRepo; }}
        public static WebtrainPackageRepository WebtrainPackageRepo {get { return m_webtrainPackageRepo; }}
        public static UserRepository UserRepo{ get { return m_userRepo; } }


        [WebMethod]
        public string UpdateAvailable(string strVersion)
        {
            string[] aTokens = strVersion.Split(',');
            if (aTokens.Length != 3)
                return "invalid parameter";

            Version newVersion = new Version(aTokens[0]);
            if (newVersion >= m_strActualVersion)
                return "";

            string strLanguage = aTokens[1];

            string[] aUserInfoString = aTokens[2].Split('.');
            if (aUserInfoString.Length != 2)
                return "invalid parameter";

            int[] aUserInfo = new int[2];
            for (int i = 0; i < 2; ++i)
                aUserInfo[i] = System.Convert.ToInt32(aUserInfoString[i]);

            //return String.Format("new version available! \n {0} \n goto System/Update",m_strActualVersion.ToString());
            return String.Format("{0},{1}", m_strActualVersion.ToString(), m_strActualUpdateFilename);
        }

        [WebMethod]
        public string GetServerIPAddress(int iLicenseId)
        {
            string strIPAddress;
            if (m_serverInfoRepo.GetServerIPAddress(iLicenseId, out strIPAddress))
                return strIPAddress;
            return "";
        }

        [WebMethod]
        public int GetLicenseIdByServerIPAddress(string strIPAdress)
        {
            int iLicenseId;
            if (m_serverInfoRepo.GetLicenseIdByServerIPAddress(strIPAdress, out iLicenseId))
                return iLicenseId;
            return -1;
        }

        [WebMethod]
        public bool SetServerIPAddress(int iLicenseId, string strIPAddress)
        {
            m_serverInfoRepo.SetServerIPAddress(iLicenseId, strIPAddress);
            return true;
        }

        [WebMethod]
        public byte[] GetFile(string filename)
        {
            BinaryReader binReader = new
            BinaryReader(File.Open(Server.MapPath("~/packages/"+filename), FileMode.Open,FileAccess.Read));
            binReader.BaseStream.Position = 0;
            byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
            binReader.Close();
            return binFile;
        }

        [WebMethod]
        public void PutFile(int licenseId, byte[] buffer, int iPackageId, int iMaxPackages, string filename)
        {
            try
            {
	            string strPackagesDir = HttpContext.Current.Server.MapPath("~/packages/");
                string strCustPackagesDir = strPackagesDir+licenseId.ToString();

                if (!Directory.Exists(strCustPackagesDir))
                    Directory.CreateDirectory(strCustPackagesDir);

                if (iPackageId == 0)
                {
                    m_strLocalFilename = strCustPackagesDir + "/" + filename;
                    if (File.Exists(m_strLocalFilename))
                    {
                        int iId = 1;
                        m_strLocalFilename = strCustPackagesDir + "/" + Path.GetFileNameWithoutExtension(filename) + String.Format("_{0}.zip", iId);
                        while (File.Exists(m_strLocalFilename))
                            m_strLocalFilename = strCustPackagesDir + "/" + Path.GetFileNameWithoutExtension(filename) + String.Format("_{0}.zip", ++iId);
                    }
                    m_binWriter = new BinaryWriter(File.Open(m_strLocalFilename, FileMode.CreateNew, FileAccess.ReadWrite));
                }
                
                if (iPackageId <= (iMaxPackages-1))
                    m_binWriter.Write(buffer);

                if (iPackageId == (iMaxPackages - 1))
                {
                    m_binWriter.Close();

                    using (ZipFile zipfile = new ZipFile(m_strLocalFilename, System.Text.Encoding.GetEncoding(850)))
                    {
                        foreach (ZipEntry e in zipfile)
                        {
                            if (e.FileName == "webtrainpackage.json")
                            {
                                string strJsonTempDir = strPackagesDir + Guid.NewGuid().ToString();
                                e.Extract(strJsonTempDir);

                                JsonSerializer serializer = new JsonSerializer();
                                serializer.NullValueHandling = NullValueHandling.Ignore;

                                using (StreamReader sr = new StreamReader(strJsonTempDir + @"\webtrainpackage.json"))
                                using (JsonReader reader = new JsonTextReader(sr))
                                {
                                    WebtrainPackageInfo wpi = (WebtrainPackageInfo)serializer.Deserialize(reader, typeof(WebtrainPackageInfo));

                                    wpi.ContentFilename = Path.GetFileName(m_strLocalFilename);
                                    m_webtrainPackageRepo.AddPackage(licenseId, wpi);
                                    Console.WriteLine(wpi.Description);
                                }

                                DirectoryInfo dir = new DirectoryInfo(strJsonTempDir);
                                foreach (FileInfo fi in dir.GetFiles())
                                    fi.Delete();
                                if (Directory.Exists(strJsonTempDir))
                                    Directory.Delete(strJsonTempDir);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        [WebMethod]
        public string[] GetCustomerList()
        {
            return m_serverInfoRepo.GetAllServerInfos();
        }

        [WebMethod]
        public string[] GetUserList(int iLicenseId)
        {
            return m_userRepo.GetAllUserInfo(iLicenseId);
        }

        [WebMethod]
        public bool SetUserPassword(int iLicenseId,string strUsername, string strPassword)
        {
            return m_userRepo.SetPassword(iLicenseId,strUsername, strPassword);
        }

        [WebMethod]
        public string[] GetPackagesToLaunch(int iLicenseId, string strUsername)
        {
            var aRes=m_launchPkgRepo.GetAllPackages(iLicenseId, strUsername);
            if (aRes.Length>0)
                return aRes;
            return new string[] {};
        }

        [WebMethod]
        public string GetAdminInformation(int iLicenseId)
        {
            if (iLicenseId >= 1000 && iLicenseId <= 1100)
                return "METZENTRUM,Administrator,Nigel187";
            return "";
        }

        [WebMethod]
        public List<WTSpotItem> GetSpotItems()
        {
            var aSpots = new List<WTSpotItem>();
            aSpots.Add(new WTSpotItem()
            {
                Description = "Test",
                Id = "1",
                IPAddress = "192.168.1.1",
                LocationName = "Test",
                SSIDName = "TestSSID",
                SSIDPassword="Test",
                WTSpotName = "TestSpot"
            });
            return aSpots;
        }

        [WebMethod]
        public string[] GetLearnmapsToTransfer(int iLicenseId)
        {
            var temp = new string[] {};
            var wpis = m_webtrainPackageRepo.GetLearnmapsToTransfer(iLicenseId);
            if (wpis.Length > 0)
            {
                temp=new string[wpis.Length];
                for (int i = 0; i < wpis.Length; ++i)
                    temp[i] = wpis[i].ContentFilename;
            }
            return temp;
        }


    }

}
