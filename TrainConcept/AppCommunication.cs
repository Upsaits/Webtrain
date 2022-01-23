using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SoftObject.SOComponents.Forms;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.TCLocalServiceReference1;
using SoftObject.UtilityLibrary;
using QueryUpdateSoapClient = SoftObject.TrainConcept.TCServiceReference1.QueryUpdateSoapClient;

namespace SoftObject.TrainConcept
{
    public class AppCommunication
    {
        private bool m_bAutoUpdateInstalled = false;
        private const int m_iMaxPackageSize = 1048576;
        private AppHandler AppHandler = Program.AppHandler;
        public static string AdminInfo { get; private set; } = "";
        public static string AvailableUpdateFilename { get; private set; }
        public static string AutoServerName { get; private set; }
        public static bool AutoServerNameChecked { get; private set; }
        public static bool CustomerNamesChecked { get; private set; }
        public static bool UserListChecked { get; private set; }
        public static bool AvailableUpdatesChecked { get; private set; }
        public static bool UpdatesAvailable { get; private set; }
        public static string AvailableUpdateMsg { get; private set; }
        public static bool AvailableUpdateFileChecked { get; private set; }
        public static bool UpdateFileAvailable { get; private set; }
        public static List<string> UserList { get; private set; }
        public static List<string> Customers { get; private set; }

        public AppCommunication()
        {
            Customers = new List<string>();
            UserList = new List<string>();
            UpdateFileAvailable = false;
            AvailableUpdateFileChecked = false;
            AvailableUpdateMsg = "";
            UpdatesAvailable = false;
            AvailableUpdatesChecked = false;
            UserListChecked = false;
            CustomerNamesChecked = false;
            AutoServerNameChecked = false;
            AutoServerName = "";
            AvailableUpdateFilename = "";
        }

        public void StartWebtrainCommunication()
        {
            new Thread(new ThreadStart(delegate
            {
                TryToGetAvailableUpdate();

                if (AppHandler.IsServer)
                    TryToGetCustomerNames();
                else if (AppHandler.IsSingle)
                {
                    if (AppHandler.ServerName.Length > 0)
                        AutoServerName = AppHandler.ServerName;
                    else
                        TryToGetAutoServerName();

                    TryToGetCustomerNames();
                }
                else
                {
                    TryToGetAutoServerName();
                }

            })).Start();
        }


        public void CheckUserList()
        {
            new Thread(new ThreadStart(delegate
            {
                if (!AppHandler.IsClient)
                    TryToGetUserList();
            })).Start();
        }
        public static void TryToGetAvailableUpdate()
        {
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Server wird kontaktiert...",
                      (string str1, string str2, DoWorkEventArgs e) =>
                      {
                          string strMessage = "";
                          try
                          {
                              var queryWeb = new QueryUpdateSoapClient();
                              queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 20);
                              queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 0, 20);

                              var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                              if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                                  queryLocal.Endpoint.Address = new EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");

                              string strVersion = Program.AppHandler.VersionString + ',' + Program.AppHandler.Language + ',' + Program.AppHandler.UseType + '.' + Program.AppHandler.DongleId.ToString();
                              string strNewVersion = "";
                              string strAdminInfo = "";

                              int iDongleId = Program.AppHandler.DongleId;
                              if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                              {
                                  strNewVersion = queryLocal.UpdateAvailable(strVersion);
                                  strAdminInfo = queryLocal.GetAdminInformation(Program.AppHandler.DongleId);
                                  if (Program.AppHandler.IsClient)
                                      iDongleId = queryLocal.GetLicenseIdByServerIPAddress(Program.AppHandler.ServerName);
                              }
                              else
                              {
                                  strNewVersion = queryWeb.UpdateAvailable(strVersion);
                                  strAdminInfo = queryWeb.GetAdminInformation(Program.AppHandler.DongleId);
                              }

                              if (strNewVersion.Length > 0)
                              {
                                  string msg = String.Format("{0} {1}", DateTime.UtcNow.ToString(), strNewVersion);
                                  Program.AppHandler.MainForm.Messages.Add(msg);
                                  strMessage = strNewVersion;
                                  UpdatesAvailable = true;
                              }
                              else if (Program.AppHandler.IsTimeLimited())
                              {
                                  string txt = Program.AppHandler.LanguageHandler.GetText("MESSAGE", "Demo-Version: Rest: {0} Tage");
                                  int days = Program.AppHandler.GetLicenceRestDays();
                                  strMessage = String.Format(txt, days);
                              }
                              else
                              {
                                  strMessage = Program.AppHandler.LanguageHandler.GetText("MESSAGE", "Welcome");
                              }

                              AvailableUpdateMsg = strMessage;
                              string[] strTokens = AvailableUpdateMsg.Split(',');
                              if (strTokens.Length == 2)
                              {
                                  AvailableUpdateMsg =
                                      String.Format(
                                          "Neue Version {0} verfügbar!\n Wird heruntergeladen und installiert",
                                          strTokens[0]);
                                  AvailableUpdateFilename = strTokens[1];
                              }

                              AdminInfo = (Program.AppHandler.ConfigAdminInfo.Length > 0) ? Program.AppHandler.ConfigAdminInfo : strAdminInfo;
                              AvailableUpdatesChecked = true;

                              e.Result = "OK";
                          }
                          catch (Exception ex)
                          {
                              AvailableUpdateMsg = Program.AppHandler.LanguageHandler.GetText("MESSAGE", "update server not found!"); ;
                              AvailableUpdatesChecked = false;
                              //MessageBox.Show(ex.Message);
                              e.Result = ex.Message;
                          }

                      },
                      (RunWorkerCompletedEventArgs e) =>
                      {
                      });

            if (AvailableUpdatesChecked && UpdatesAvailable)
            {
                TryToGetAvailableUpdateFile();
            }
        }

        private static void TryToGetAvailableUpdateFile()
        {
            if (AvailableUpdateFilename.Length > 0)
            {
                var frm = new XFrmLongProcessToastNotificationEmpty();
                frm.Start("Server wird kontaktiert...",
                    (string str1, string str2, DoWorkEventArgs e) =>
                    {
                        using (var client = new WebClient())
                        {
                            try
                            {
                                string strTargetPath = Path.GetTempPath();
                                string strURL = Program.AppHandler.WebtrainServer + "/Patches/" + AvailableUpdateFilename;
                                //client.Credentials = new NetworkCredential("Franky", "W3btr@1n");
                                client.DownloadFile(strURL, strTargetPath + AvailableUpdateFilename);
                                UpdateFileAvailable = true;
                                e.Result = "OK";
                            }
                            catch (Exception ex)
                            {
                                UpdateFileAvailable = false;
                                e.Result = ex.Message;
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                AvailableUpdateFileChecked = true;
                            }
                            Console.WriteLine("Test");
                        }
                    },
                    (RunWorkerCompletedEventArgs e) =>
                    {

                    });
            }
        }

        private static void TryToGetAutoServerName()
        {
            string strResult = "";
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Server wird kontaktiert...",
                      (string str1, string str2, DoWorkEventArgs e) =>
                      {
                          try
                          {
                              var queryWeb = new QueryUpdateSoapClient();
                              var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                              if (queryWeb.Endpoint.Binding != null)
                              {
                                  queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 10);
                                  queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 0, 10);
                              }

                              int iLicenseId = -1;
                              string strLicenseId = Program.AppHandler.LicenseId;
                              iLicenseId = Convert.ToInt32(strLicenseId);
                              if (Program.AppHandler.WebtrainServer.Length > 0)
                              {
                                  if (iLicenseId >= 0)
                                  {
                                      if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                                      {
                                          queryLocal.Endpoint.Address = new EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");
                                          strResult = queryLocal.GetServerIPAddress(iLicenseId);
                                      }
                                      else
                                          strResult = queryWeb.GetServerIPAddress(iLicenseId);

                                      AutoServerName = strResult;
                                      Program.AppHandler.ServerName = AutoServerName;
                                      e.Result = "OK";
                                  }
                                  else if (Program.AppHandler.ServerName.Length>0)
                                  {
                                      if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                                      {
                                          queryLocal.Endpoint.Address = new EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");
                                          iLicenseId = queryLocal.GetLicenseIdByServerIPAddress(Program.AppHandler.ServerName);
                                          strResult = queryLocal.GetServerIPAddress(iLicenseId);
                                      }
                                      else
                                      {
                                          iLicenseId = queryWeb.GetLicenseIdByServerIPAddress(Program.AppHandler.ServerName);
                                          strResult = queryWeb.GetServerIPAddress(iLicenseId);
                                      }

                                      Program.AppHandler.LicenseId = iLicenseId.ToString();
                                      AutoServerName = strResult;
                                      Program.AppHandler.ServerName = strResult;
                                      e.Result = "OK";
                                  }
                                  else
                                  {
                                      AutoServerName = "";
                                      e.Result = "Failed";
                                  }
                              }
                              else
                              {
                                  AutoServerName = "";
                                  e.Result = "Failed";
                              }
                              AutoServerNameChecked = true;
                          }
                          catch (Exception ex)
                          {
                              AutoServerNameChecked = false;
                              e.Result = ex.Message;
                          }
                      },
                      (RunWorkerCompletedEventArgs e) =>
                      {

                      });
            if (AutoServerNameChecked)
            {

            }
        }

        private static void TryToGetCustomerNames()
        {
            List<string> lResult = new List<string>();
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Server wird kontaktiert...",
                      (string str1, string str2, DoWorkEventArgs e) =>
                      {
                          try
                          {
                              var queryWeb = new QueryUpdateSoapClient();
                              var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                              queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 20);
                              queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 0, 20);
                              if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                              {
                                  queryLocal.Endpoint.Address = new EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");
                                  lResult = queryLocal.GetCustomerList();
                              }
                              else
                              {
                                  lResult = queryWeb.GetCustomerList();
                              }

                              Customers = lResult;

                              CustomerNamesChecked = true;

                              e.Result = "OK";
                          }
                          catch (Exception ex)
                          {
                              CustomerNamesChecked = false;
                              e.Result = ex.Message;
                          }
                      },
                      (RunWorkerCompletedEventArgs e) =>
                      {
                      });
            if (CustomerNamesChecked)
            {
                TryToGetUserList();
            }
        }

        private static void TryToGetUserList()
        {
            List<string> lResult = new List<string>();
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Server wird kontaktiert...",
                      (string str1, string str2, DoWorkEventArgs e) =>
                      {
                          try
                          {
                              var queryWeb = new QueryUpdateSoapClient();
                              var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                              queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 20);
                              queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 0, 20);
                              if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                              {
                                  queryLocal.Endpoint.Address = new EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");
                                  lResult = queryLocal.GetUserList(Convert.ToInt32(Program.AppHandler.LicenseId)/*1002*/);
                                  //MessageBox.Show(queryLocal.Endpoint.Address.ToString() + " Anzahl:" + lResult.Count.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              }
                              else
                              {
                                  lResult = queryWeb.GetUserList(Convert.ToInt32(Program.AppHandler.LicenseId)/*1002*/);
                                  //MessageBox.Show(queryWeb.Endpoint.Address.ToString() + " Anzahl:" + lResult.Count.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              }

                              UserList = lResult;

                              UserListChecked = true;

                              e.Result = "OK";
                          }
                          catch (Exception ex)
                          {
                              UserListChecked = false;
                              e.Result = ex.Message;
                          }
                      },
                      (RunWorkerCompletedEventArgs e) =>
                      {
                      });
            if (UserListChecked)
            {
                Program.AppHandler.MainForm.BeginInvoke((MethodInvoker)delegate
                {
                    Program.AppHandler.UserManager.UpdateCentralManagedUsers(AppCommunication.UserList/*, (string str1) =>
                    {
                        MessageBox.Show(str1, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }*/
                    );


                    Program.AppHandler.MainForm.UpdateUserList();
                });
            }
        }

        public void SetIPAddress(string strIPAddress)
        {
            string strResult = "";
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Server wird kontaktiert...",
                      (string str1, string str2, DoWorkEventArgs e) =>
                      {
                          try
                          {
                              var queryWeb = new QueryUpdateSoapClient();
                              var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                              queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 10);
                              queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 0, 10);

                              string strLicenseId = Program.AppHandler.LicenseId;
                              if (AppHandler.WebtrainServer.Contains("localhost"))
                              {
                                  queryLocal.Endpoint.Address = new EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");
                                  strResult = queryLocal.SetServerIPAddress(Convert.ToInt32(strLicenseId), strIPAddress) ? "Ok" : "Failed";
                              }
                              else
                                  strResult = queryWeb.SetServerIPAddress(Convert.ToInt32(strLicenseId), strIPAddress) ? "Ok" : "Failed";

                              e.Result = "OK";
                          }
                          catch (Exception ex)
                          {
                              e.Result = ex.Message;
                          }
                      },
                      (RunWorkerCompletedEventArgs e) =>
                      {
                      });
        }

        public void SetUserPassword(string strUsername, string strPassword)
        {
            string strResult = "";
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Server wird kontaktiert...",
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        var queryWeb = new QueryUpdateSoapClient();
                        var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                        queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 0, 10);
                        queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 0, 10);

                        string strLicenseId = Program.AppHandler.LicenseId;
                        if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                        {
                            queryLocal.Endpoint.Address = new EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");
                            strResult = queryLocal.SetUserPassword(Convert.ToInt32(strLicenseId), strUsername, strPassword) ? "Ok" : "Failed";
                        }
                        else
                            strResult = queryWeb.SetUserPassword(Convert.ToInt32(strLicenseId), strUsername, strPassword) ? "Ok" : "Failed";

                        e.Result = "OK";
                    }
                    catch (Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                });
        }

        public void StartAutoUpdate()
        {
            if (AvailableUpdateFilename.Length > 0 && !m_bAutoUpdateInstalled)
            {
                string strTargetPath = Path.GetTempPath();
                try
                {
                    string strFromPath = strTargetPath + AvailableUpdateFilename;
                    string strPatchName = Path.GetFileNameWithoutExtension(AvailableUpdateFilename);
                    string strToPath = strTargetPath + "Webtrain" + strPatchName;
                    if (Directory.Exists(strToPath))
                        Directory.Delete(strToPath, true);

                    AppHelpers.ZipFileExtractToDirectory(strFromPath, strToPath);
                    //ZipFile.ExtractToDirectory(strFromPath, strToPath, Encoding.GetEncoding(850));

                    // todo: start installation
                    Process proc = new Process();
                    SecureString ssPwd = new SecureString();
                    proc.StartInfo.UseShellExecute = false;

                    //runs application for the first time as admin with admin credentials. 
                    proc.StartInfo.FileName = strToPath + @"\install.exe";
                    proc.StartInfo.Arguments = String.Format("Patches {0}", strPatchName);
                    string password = "";
                    if (AdminInfo.Length > 0)
                    {
                        string[] strTokens = AdminInfo.Split(',');
                        //string strAdminInfo = String.Format("{0},{1},{2},{3}", m_strAdminInfo, strTokens[0],
                        //    strTokens[1], strTokens[2]);
                        //MessageBox.Show(strAdminInfo, "WebTrain", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (strTokens.Length == 3)
                        {
                            proc.StartInfo.Domain = strTokens[0];
                            proc.StartInfo.UserName = strTokens[1];
                            password = strTokens[2];
                            for (int x = 0; x < password.Length; x++)
                            {
                                ssPwd.AppendChar(password[x]);
                            }
                        }
                    }
                    else
                    {
                        proc.StartInfo.Domain = "";
                        proc.StartInfo.UserName = "";
                    }

                    proc.StartInfo.Password = ssPwd;

                    m_bAutoUpdateInstalled = true;

                    proc.Start();
                }
                catch (Exception ex)
                {
                    string strAdminInfo = "";
                    if (AdminInfo.Length > 0)
                    {
                        string[] strTokens = AdminInfo.Split(',');
                        strAdminInfo = String.Format("{0}: Domain: {1},Username: {2},Password: {3}", AdminInfo, strTokens[0],
                            strTokens[1], strTokens[2]);
                    }

                    Debug.WriteLine(ex.Message);
                    MessageBox.Show("Update konnte nicht installiert werden!\n" +
                                         "Domain,Administrator oder Passwort ungültig: \n" +
                                          strAdminInfo, "WebTrain", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void SendLibraryZipfile(string strTitle, int iToLicenseId, string strZipFilename, string strTargetFilename)
        {
            var frm = new XFrmLongProcessToastNotification();
            frm.Start(strTitle,
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        string filename = strZipFilename;
                        System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                        br.BaseStream.Position = 0;
                        byte[] buffer = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                        br.Close();
                        //byte[] buffer = Enumerable.Repeat((byte)0x20, 22499782).ToArray();

                        var queryWeb = new TCServiceReference1.QueryUpdateSoapClient();
                        var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                        queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 10, 0);
                        queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 10, 0);

                        if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                            queryLocal.Endpoint.Address = new System.ServiceModel.EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");

                        if (buffer.Length > m_iMaxPackageSize)
                        {
                            frm.ShowCancelButton(true);

                            int iCntPackages = 0;
                            int iCntSent = 0;
                            int iMaxPackages = (buffer.Length % m_iMaxPackageSize) > 0
                                ? (buffer.Length / m_iMaxPackageSize) + 1
                                : (buffer.Length / m_iMaxPackageSize);
                            for (int i = 0; i < buffer.Length; i += m_iMaxPackageSize)
                            {
                                int iRest = buffer.Length - i;
                                byte[] buffer2 = new byte[iRest >= m_iMaxPackageSize ? m_iMaxPackageSize : iRest];
                                Array.Copy(buffer, i, buffer2, 0, iRest >= m_iMaxPackageSize ? m_iMaxPackageSize : iRest);
                                if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                                    queryLocal.PutFile(iToLicenseId, buffer2, iCntPackages, iMaxPackages, strTargetFilename);
                                else
                                    queryWeb.PutFile(iToLicenseId, buffer2, iCntPackages, iMaxPackages, strTargetFilename);

                                //Console.WriteLine(String.Format("Dongle-Id: {0}, buffer2.Length: {1}, iCnt: {2}, iMaxCnt: {3}, iRest {4}", m_iDongleId, buffer2.Length, iCntPackages, iMaxPackages,iRest));
                                iCntSent += buffer2.Length;
                                frm.SetStatusText(String.Format("Sende: {0:N0} k /{1:N0} k", iCntSent / 1024, buffer.Length / 1024));
                                ++iCntPackages;

                                if (frm.WasCancelled)
                                    break;
                            }
                        }
                        else
                        {
                            if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                                queryLocal.PutFile(iToLicenseId, buffer, 0, 1, strTargetFilename);
                            else
                                queryWeb.PutFile(iToLicenseId, buffer, 0, 1, strTargetFilename);
                        }

                        e.Result = !frm.WasCancelled ? "OK" : "Failed";
                        Thread.Sleep(2000);
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                    if (e.Result.ToString() == "OK")
                    {
                        frm.DialogResult = DialogResult.OK;
                        Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                        {
                            FrmBrowser frmPackages = (FrmBrowser)Program.AppHandler.ContentManager.GetBrowser("Webtrain-Pakete");
                            if (frmPackages != null)
                                frmPackages.UpdateGridview();
                            //string txt = Program.AppHandler.LanguageHandler.GetText("MESSAGE", "sendfile_completed_successfully", "Datenübertragung erfolgreich abgeschlossen!");
                            //string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            //MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }));
                    }
                    else
                    {
                        frm.DialogResult = DialogResult.Cancel;
                        Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                        {
                            string txt = Program.AppHandler.LanguageHandler.GetText("ERROR", "sending_file_not_possible", "**Webtrain-Paket konnte nicht übertragen werden!");
                            //string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            //MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Program.AppHandler.CTSServerConsole.Add(txt);
                        }));

                    }
                });
        }

        public static void GetTransferedLearnmaps()
        {
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Übertrage gesendete Lernmappen...",
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        var queryWeb = new TCServiceReference1.QueryUpdateSoapClient();
                        var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                        queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 10, 0);
                        queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 10, 0);

                        if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                            queryLocal.Endpoint.Address = new System.ServiceModel.EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");


                        List<string> aMaps = null;
                        if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                            aMaps = queryLocal.GetLearnmapsToTransfer(Convert.ToInt32(Program.AppHandler.LicenseId));
                        else
                            aMaps = queryWeb.GetLearnmapsToTransfer(Convert.ToInt32(Program.AppHandler.LicenseId));

                        if (aMaps != null && aMaps.Count>0)
                        {
                            string strTempPath = Path.GetTempPath() + Guid.NewGuid().ToString();
                            if (!Directory.Exists(strTempPath))
                                Directory.CreateDirectory(strTempPath);

                            foreach (var s in aMaps)
                            {
                                byte[] aBinaryBytes=null;
                                if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                                    aBinaryBytes = queryLocal.GetFile(String.Format("/{0}/", Program.AppHandler.LicenseId) + s);
                                else
                                    aBinaryBytes = queryWeb.GetFile(String.Format("/{0}/", Program.AppHandler.LicenseId) + s);
                                if (aBinaryBytes != null)
                                {
                                    string strFilePath = strTempPath + @"\" + s;
                                    using (BinaryWriter writer = new BinaryWriter(File.Open(strFilePath, FileMode.Create)))
                                    {
                                        writer.Write(aBinaryBytes);
                                        writer.Close();
                                    }

                                    Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                                    {
                                        AppHelpers.ImportLearnmap(strFilePath);
                                        var di = new DirectoryInfo(strTempPath);
                                        foreach (FileInfo file in di.GetFiles())
                                            file.Delete();
                                        foreach (DirectoryInfo subDirectory in di.GetDirectories())
                                            subDirectory.Delete(true);
                                        Directory.Delete(strTempPath);
                                    }));
                                    //Thread.Sleep(5000);
                                }
                            }
                        }

                        e.Result = "OK";
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                    if (e.Result.ToString() == "OK")
                    {
                    }
                    else
                    {
                        frm.DialogResult = DialogResult.Cancel;
                        Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                        {
                            string txt = Program.AppHandler.LanguageHandler.GetText("ERROR", "sending_learnmap_not_possible", "**Lernmappe konnte nicht übertragen werden!");
                            txt += e.Result.ToString();
                            //string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                            //MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            if (Program.AppHandler.IsServer)
                                Program.AppHandler.CTSServerConsole.Add(txt);

                        }));

                    }
                });
        }


        public static void GetLaunchPackage()
        {
            var frm = new XFrmLongProcessToastNotificationEmpty();
            frm.Start("Übertrage zu startende Lernmappen...",
                (string str1, string str2, DoWorkEventArgs e) =>
                {
                    try
                    {
                        var queryWeb = new TCServiceReference1.QueryUpdateSoapClient();
                        var queryLocal = new TCLocalServiceReference1.QueryUpdateSoapClient();
                        queryWeb.Endpoint.Binding.SendTimeout = new TimeSpan(0, 10, 0);
                        queryWeb.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 10, 0);

                        if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                            queryLocal.Endpoint.Address = new System.ServiceModel.EndpointAddress(Program.AppHandler.WebtrainServer + "/QueryUpdate.asmx");

                        List<string> aMaps = null;
                        if (Program.AppHandler.WebtrainServer.Contains("localhost"))
                            aMaps = queryLocal.GetPackagesToLaunch(Convert.ToInt32(Program.AppHandler.LicenseId), "mairfr"/*Program.AppHandler.MainForm.ActualUserName*/);
                        else
                            aMaps = queryWeb.GetPackagesToLaunch(Convert.ToInt32(Program.AppHandler.LicenseId), "mairfr"/*Program.AppHandler.MainForm.ActualUserName*/);

                        if (aMaps!=null && aMaps.Count>0)
                        {
                            Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                            {
                                if (Program.AppHandler.MainForm.WindowState == FormWindowState.Minimized)
                                    Program.AppHandler.MainForm.WindowState = FormWindowState.Normal;
                                Program.AppHandler.MainForm.Activate();
                                if (Program.AppHandler.MapManager.GetId(aMaps[0])>=0)
                                 Program.AppHandler.ContentManager.OpenLearnmap(Program.AppHandler.MainForm, aMaps[0]);
                            }));
                        }

                        e.Result = "OK";
                    }
                    catch (System.Exception ex)
                    {
                        e.Result = ex.Message;
                    }
                },
                (RunWorkerCompletedEventArgs e) =>
                {
                    if (e.Result.ToString() == "OK")
                    {
                    }
                    else
                    {
                    }
                });
        }

        public static bool FindCustomerName(int iLicenseId,out string strName)
        {
            if (CustomerNamesChecked)
            {
                 foreach (var c in Customers)
                 {
                    string[] strTokens = c.Split(',');
                    if (strTokens.Length == 4)
                    {
                        //r["locationId"].ToString() + ',' +
                        //r["name"].ToString() + ',' +
                        //r["licenseId"].ToString() + ',' +
                        //r["ipadress"].ToString();
                        if (int.Parse(strTokens[2])==iLicenseId)
                        {
                            strName=strTokens[1];
                            return true;
                        }
                    }
                 }
            }
            strName="";
            return false;
        }
    }
}
