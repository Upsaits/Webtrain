using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using DevExpress.XtraBars;
using DevExpress.XtraRichEdit.Import.OpenXml;
using Forms;
using Ionic.Zip;
using Newtonsoft.Json;
using SoftObject.SOComponents.Forms;
using SoftObject.SOComponents.UtilityLibrary;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;
using ZipFile = System.IO.Compression.ZipFile;


namespace SoftObject.TrainConcept
{
    public class AppHelpers
    {
        public AppHelpers()
        {

        }

        public void ExportLibrary(string strLibTitle)
        {
            string strLibFileNameWoExt = Path.GetFileNameWithoutExtension(Program.AppHandler.LibManager.GetFilePath(strLibTitle));
            string startPath = Program.AppHandler.ContentFolder + '\\' + strLibFileNameWoExt;
            string zipPath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".zip";

            var frm = new XFrmLongProcessToastNotification();
            frm.Start("Modul wird exportiert..",
                      (string str1, string str2, DoWorkEventArgs e) =>
                      {
                          ZipFile.CreateFromDirectory(str1, str2, CompressionLevel.Optimal, true, Encoding.GetEncoding(850));
                          e.Result = "OK";
                      },
                      (RunWorkerCompletedEventArgs e) =>
                      {
                          if (e.Result.ToString() == "OK")
                          {
                              frm.DialogResult = DialogResult.OK;
                              Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                              {
                                  var frmExport = new XFrmExportContentModule(strLibTitle, strLibFileNameWoExt, zipPath);
                                  if (frmExport.ShowDialog() == DialogResult.OK)
                                  {
                                      if (frmExport.IsPublished)
                                          AppCommunication.SendLibraryZipfile("Modul wird übermittelt...", Program.AppHandler.DongleId,frmExport.TargetFileName, Path.GetFileName(frmExport.TargetFileName));

                                      string txt = Program.AppHandler.LanguageHandler.GetText("MESSAGE", "export_completed_successfully", "Export erfolgreich abgeschlossen!");
                                      string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                      MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                  }
                              }));

                          }
                      },
                      startPath, zipPath);
        }

        public static bool ImportLibrary(string strLibTitle, string strLibFilename, string strZipFilename, bool bAdaptTemplates)
        {
            var frm = new XFrmLongProcessToastNotification();
            frm.Start("Modul wird importiert..",
                     (string str1, string str2, DoWorkEventArgs e) =>
                     {
                         try
                         {
                             //ZipFile.ExtractToDirectory(str1, str2, Encoding.GetEncoding(850));
                             ZipFileExtractToDirectory(str1, str2);
                             e.Result = "OK";
                         }
                         catch (Exception ex)
                         {
                             e.Result = ex.Message;
                         }
                     },
                     (RunWorkerCompletedEventArgs e) =>
                     {
                         if (e.Result.ToString() == "OK")
                         {
                             File.Delete(Program.AppHandler.ContentFolder + @"\webtrainpackage.json");

                             if (bAdaptTemplates)
                             {
                                 string strSrcTemplateFilePath = Program.AppHandler.ContentFolder + @"\Leer\Templates";
                                 string strTargetTemplateFilePath = Program.AppHandler.ContentFolder + @"\" + strLibTitle + @"\Templates";
                                 FileAndDirectoryHelpers.Copy(strSrcTemplateFilePath, strTargetTemplateFilePath);
                             }

                             string strSrcLibFilePath = Program.AppHandler.ContentFolder + @"\" + strLibFilename;
                             string strTargetLibFilePath = Program.AppHandler.LibsFolder + @"\" + strLibFilename;
                             File.Copy(strSrcLibFilePath, strTargetLibFilePath, true);
                             File.Delete(strSrcLibFilePath);

                             frm.DialogResult = DialogResult.OK;
                             Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                             {
                                 Program.AppHandler.LibManager.CloseAll();
                                 if (!LoadLibraries())
                                 {
                                     string txt = Program.AppHandler.LanguageHandler.GetText("ERROR", "libraries_not_found");
                                     string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                     MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                 }
                             }));
                         }
                         else
                         {
                             string txt = Program.AppHandler.LanguageHandler.GetText("ERROR", "error_on_unzipping", "Fehler beim entpacken");
                             string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                             txt=String.Format(txt + ": " + e.Result);
                             MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                         }
                     },
                     strZipFilename, Program.AppHandler.ContentFolder);

            return true;
        }

        public static void ZipFileExtractToDirectory(string zipPath, string extractPath)
        {
            using (System.IO.Compression.ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string completeFileName = Path.Combine(extractPath, entry.FullName);
                    string directory = Path.GetDirectoryName(completeFileName);

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    if (Path.GetFileName(completeFileName).Length>0)
                        entry.ExtractToFile(completeFileName, true);
                }
            }
        }

        public static bool CreateLibraryZipFile(string strLibTitle, string strZippedTempFileName,WebtrainPackageInfo wpi)
        {
            var pi = wpi;

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            string strJsonTempFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".json";
            using (StreamWriter sw = new StreamWriter(strJsonTempFile))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, pi);
            }

            try
            {
                using (var zipDest = ZipFile.Open(strZippedTempFileName, ZipArchiveMode.Update, System.Text.Encoding.GetEncoding(850)))
                {
                    string strLibFile = Program.AppHandler.LibManager.GetFilePath(strLibTitle);
                    zipDest.CreateEntryFromFile(strJsonTempFile, "webtrainpackage.json");
                    zipDest.CreateEntryFromFile(strLibFile, Path.GetFileName(strLibFile));
                    zipDest.Dispose();
                    File.Delete(strJsonTempFile);
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        public static bool CreateSavedLibrary(string strLibName)
        {
            string strLibFileName = Path.GetFileNameWithoutExtension(Program.AppHandler.LibManager.GetFilePath(strLibName));
            string startPath = Program.AppHandler.ContentFolder + '\\' + strLibFileName;
            string zipPath = Program.AppHandler.ImportExportFolder + strLibFileName + "_save.zip";

            var frm = new XFrmLongProcessToastNotification();
            frm.Start("Modul wird gesichert..",
                        (string str1, string str2, DoWorkEventArgs e) =>
                        {
                            try
                            {
                                if (File.Exists(str2))
                                    File.Delete(str2);
                                ZipFile.CreateFromDirectory(str1, str2, CompressionLevel.Optimal, true, Encoding.GetEncoding(850));
                                e.Result = "OK";
                            }
                            catch (Exception ex)
                            {
                                e.Result = ex.Message;
                            }
                        },
                        (RunWorkerCompletedEventArgs e) =>
                        {
                        },
                        startPath, zipPath);

            return true;
        }

        public static bool DestroyLibrary(string strLibName)
        {
            try
            {
                string strLibFilePath = Program.AppHandler.LibManager.GetFilePath(strLibName);
                string strLibFileName = Path.GetFileNameWithoutExtension(strLibFilePath);
                string startPath = Program.AppHandler.ContentFolder + '\\' + strLibFileName;
                if (Directory.Exists(startPath))
                {
                    var di = new DirectoryInfo(startPath);
                    foreach (FileInfo file in di.GetFiles())
                        file.Delete();
                    foreach (DirectoryInfo subDirectory in di.GetDirectories())
                        subDirectory.Delete(true);
                    Directory.Delete(startPath);
                }

                File.Delete(strLibFilePath);
                Program.AppHandler.LibManager.Close(strLibName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static bool CreateLibrary(string strLibName, bool bPrev)
        {
            Program.AppHandler.LibManager.AddLibrary(strLibName, bPrev);
            try
            {
                string strLibFilePath = Program.AppHandler.LibManager.GetFilePath(strLibName);
                string strLibFileName = Path.GetFileNameWithoutExtension(strLibFilePath);
                string startPath = Program.AppHandler.ContentFolder + @"\leer";
                string targetPath = Program.AppHandler.ContentFolder + '\\' + strLibFileName;

                FileAndDirectoryHelpers.Copy(startPath, targetPath);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        public static void AddUserProgressInfo(string userName, string workingTitle, int iRegionId, ushort iRegionVal)
        {
            if ((Program.AppHandler.IsClient|| Program.AppHandler.IsSingle) && Program.AppHandler.CtsClientManager.IsRunning())
                Program.AppHandler.CtsClientManager.SendUserProgressInfo(userName, workingTitle, iRegionId, iRegionVal);
            Program.AppHandler.UserProgressInfoMgr.AddUserProgressInfo(userName, workingTitle, (UserProgressInfoManager.RegionType)iRegionId, iRegionVal);
        }


        public static bool TryToChangeUserPassword(string userName, string passWord)
        {
            string oldPassword = "";
            string fullName = "";
            int iImgId = 0;
            Program.AppHandler.UserManager.GetUserInfo(userName, ref oldPassword, ref fullName, ref iImgId);

            if (Program.AppHandler.IsClient || Program.AppHandler.IsSingle)
            {
                if (Program.AppHandler.CtsClientManager.IsRunning())
                {
                    AutoResetEvent jobDone = new AutoResetEvent(false);
                    if (Program.AppHandler.CtsClientManager.AskChangePassword(userName, passWord, ref jobDone))
                    {
                        Program.AppHandler.UserManager.SetUserInfo(userName, fullName, passWord, iImgId);
                        Program.AppHandler.UserManager.Save();
                        return true;
                    }
                }
            }
            else
            {
                if (Program.AppHandler.UserManager.IsCentralManaged())
                    Program.AppCommunication.SetUserPassword(userName, passWord);

                Program.AppHandler.UserManager.SetUserInfo(userName, fullName, passWord, iImgId);
                Program.AppHandler.UserManager.Save();
                return true;
            }

            return false;
        }

        public static int GetMapProgress(string userName, string mapTitle)
        {
            string[] aWorkings = null;
            int iSumCntWork = 0;
            int iSumCntWorkedOut = 0;

            if (Program.AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings))
            {
                bool bIsContentOrientated = true;
                Program.AppHandler.MapManager.GetProgressOrientation(mapTitle,ref bIsContentOrientated);
                foreach (string w in aWorkings)
                {
                    if (bIsContentOrientated)
                    {
                        int iPageCntWork;
                        Program.AppHandler.LibManager.GetPageCnt(w, out iPageCntWork);
                        int iPageCntWorkedOut = Program.AppHandler.UserProgressInfoMgr.GetUserProgressInfo(userName, w,UserProgressInfoManager.RegionType.Learning);
                        iSumCntWork += iPageCntWork;
                        iSumCntWorkedOut += iPageCntWorkedOut;
                        //if (iPageCntWorkedOut < iPageCntWork)
                        //    Console.WriteLine(String.Format("unworked map:{0}, work:{1}",mapTitle,w));
                    }
                    else
                    {
                        int iQuCount = 0;
                        Program.AppHandler.LibManager.GetQuestionCnt(w, out iQuCount);
                        int iQuWorkedOut = Program.AppHandler.UserProgressInfoMgr.GetUserProgressInfo(userName, w, UserProgressInfoManager.RegionType.Examing);
                        iSumCntWork += iQuCount;
                        iSumCntWorkedOut += iQuWorkedOut;
                    }
                }
            }
            return iSumCntWork > 0 ? Math.Min(iSumCntWorkedOut * 100 / iSumCntWork, 100) : 0;
        }

        public static int GetMapProgressOfWork(string userName, string working, int iCntWorkedOut = -1)
        {
            bool bIsContentOrientated = true;
            Program.AppHandler.MapManager.GetProgressOrientation(working, ref bIsContentOrientated);

            if (bIsContentOrientated)
            {
                int iPageCntWork;
                Program.AppHandler.LibManager.GetPageCnt(working, out iPageCntWork);

                if (iCntWorkedOut < 0)
                    iCntWorkedOut = Program.AppHandler.UserProgressInfoMgr.GetUserProgressInfo(userName, working, UserProgressInfoManager.RegionType.Learning);
                if (iCntWorkedOut == 0)
                    return -1;
                else if (iCntWorkedOut < iPageCntWork)
                    return 0;
                else if (iCntWorkedOut >= iPageCntWork)
                    return 1;
                return -1;
            }
            else
            {
                int iQuCntWork;
                Program.AppHandler.LibManager.GetQuestionCnt(working, out iQuCntWork);

                if (iCntWorkedOut < 0)
                    iCntWorkedOut = Program.AppHandler.UserProgressInfoMgr.GetUserProgressInfo(userName, working,UserProgressInfoManager.RegionType.Examing);
                if (iCntWorkedOut == 0)
                    return -1;
                else if (iCntWorkedOut < iQuCntWork)
                    return 0;
                else if (iCntWorkedOut >= iQuCntWork)
                    return 1;
                return -1;
                
            }
        }


        public static int GetWorkProgress(string userName, string mapTitle, string workTitle)
        {
            bool bIsContentOrientated = true;
            Program.AppHandler.MapManager.GetProgressOrientation(mapTitle, ref  bIsContentOrientated);
            return Program.AppHandler.UserProgressInfoMgr.GetUserProgressInfo(userName, workTitle, 
                   bIsContentOrientated ? UserProgressInfoManager.RegionType.Learning : UserProgressInfoManager.RegionType.Examing);
        }

        public static string GetWorkProgress(string userName, string mapTitle)
        {
            string[] aWorkings = null;
            string strCntWorkedOut = "";

            if (Program.AppHandler.MapManager.GetWorkings(mapTitle, ref aWorkings))
            {
                bool bIsContentOrientated = true;
                Program.AppHandler.MapManager.GetProgressOrientation(mapTitle,ref  bIsContentOrientated);
                if (aWorkings.Length > 1)
                    for (int i = 0; i < aWorkings.Length; ++i)
                    {
                        int iPageCntWorkedOut = Program.AppHandler.UserProgressInfoMgr.GetUserProgressInfo(userName, aWorkings[i],
                                                bIsContentOrientated ? UserProgressInfoManager.RegionType.Learning : UserProgressInfoManager.RegionType.Examing);
                        if (i < (aWorkings.Length - 1))
                            strCntWorkedOut += iPageCntWorkedOut.ToString() + ',';
                        else
                            strCntWorkedOut += iPageCntWorkedOut.ToString();
                    }
                else if (aWorkings.Length > 0)
                    strCntWorkedOut = Program.AppHandler.UserProgressInfoMgr.GetUserProgressInfo(userName, aWorkings[0], 
                                      bIsContentOrientated ? UserProgressInfoManager.RegionType.Learning : UserProgressInfoManager.RegionType.Examing).ToString();
            }

            Debug.WriteLine(String.Format("GetWorkProgress({0},{1})={2}", userName, mapTitle, strCntWorkedOut));
            return strCntWorkedOut.Length > 0 ? strCntWorkedOut : "0";
        }

        public static void RemoveMapFromClass(string strClassName, string strMapName)
        {
            Program.AppHandler.ClassManager.RemoveLearnmap(strClassName, strMapName);
            Program.AppHandler.ClassManager.Save();

            string[] aClassUsers;
            Program.AppHandler.ClassManager.GetUserNames(strClassName, out aClassUsers);
            if (aClassUsers != null)
            {
                foreach (var u in aClassUsers)
                    Program.AppHandler.MapManager.DeleteUser(strMapName, u);
                Program.AppHandler.MapManager.Save(strMapName);
            }
        }

        public static void AddMapToClass(string strClassName, string strMapName)
        {
            Program.AppHandler.ClassManager.AddLearnmap(strClassName, strMapName);
            Program.AppHandler.ClassManager.Save();

            string[] aClassUsers;
            Program.AppHandler.ClassManager.GetUserNames(strClassName, out aClassUsers);
            if (aClassUsers != null)
            {
                foreach (var u in aClassUsers)
                    Program.AppHandler.MapManager.AddUser(strMapName, u);
                Program.AppHandler.MapManager.Save(strMapName);
            }
        }

        public static bool IsTestInMapAllowed(string userName, string mapTitle, string testName)
        {
            // Alle ausgewerteten Tests holen
            TestResultItemCollection aTestResults;
            Program.AppHandler.TestResultManager.Find(out aTestResults, userName, mapTitle, testName, false);

            // Wieviel sind zugelassen?
            int trialCnt = 0;
            TestItem ti = Program.AppHandler.MapManager.GetTest(mapTitle, testName);
            if (ti != null)
                trialCnt = ti.trialCount;

            // Noch einer erlaubt?
            if (trialCnt > aTestResults.Count)
            {
                // Beim Finaltest unausgewertetes TestErgebnis vorhanden? -> abweisen!
                if (testName != Utilities.c_strDefaultFinalTest || aTestResults.Count == 0)
                    return true;
            }
            return false;
        }

        public static bool IsTestPassed(string strUsername, string mapTitle, string strTestname)
        {
            var aTRIs = new TestResultItemCollection();
            Program.AppHandler.TestResultManager.Find(out aTRIs, strUsername, mapTitle, strTestname, true);
            bool bPassed = false;
            foreach (TestResultItem tri in aTRIs)
            {
                if (tri.percRight > 50)
                    bPassed = true;
            }

            return bPassed;
        }

        public static bool IsTestAttended(string strUsername, string mapTitle, string strTestname)
        {
            var aTRIs = new TestResultItemCollection();
            Program.AppHandler.TestResultManager.Find(out aTRIs, strUsername, mapTitle, strTestname, true);
            return (aTRIs.Count>0);
        }

        public static bool LoadLibraries()
        {
            string libFolder = Program.AppHandler.LibsFolder;
            if (!Directory.Exists(Program.AppHandler.LibsFolder))
                return false;

            string[] libFiles = Directory.GetFiles(Program.AppHandler.LibsFolder, "*.xml");
            for (int i = 0; i < libFiles.Length; ++i)
            {
                Program.AppHandler.LibManager.Open(Program.AppHandler.LibsFolder, libFiles[i]);
#if(SHOWPAGECOUNT)
				LibraryItem lib = Program.AppHandler.LibManager.GetLibrary(i);
				MessageBox.Show(String.Format("Seitenanzahl:{0}={1}",lib.title,Program.AppHandler.LibManager.GetPageCnt(i)));
#endif
            }
            Program.AppHandler.LibManager.Initialize();

            for (int i = 0; i < Program.AppHandler.MapManager.GetMapCnt(); ++i)
            {
                string mapTitle;
                Program.AppHandler.MapManager.GetTitle(i, out mapTitle);
                string[] aWorkings = null;
                if (Program.AppHandler.MapManager.GetWorkings(i, ref aWorkings))
                    for (int j = 0; j < aWorkings.Length; ++j)
                        if (Program.AppHandler.LibManager.GetPoint(aWorkings[j]) == null)
                        {
                            Program.AppHandler.MapManager.DeleteWorking(mapTitle, aWorkings[j]);
                            Program.AppHandler.MapManager.Save(i);
                        }
            }
            return true;
        }

        public static bool DeleteLibraries()
        {
            string libFolder = Program.AppHandler.LibsFolder;
            if (!Directory.Exists(Program.AppHandler.LibsFolder))
                return false;

            Program.AppHandler.LibManager.CloseAll();

            string[] libFiles = Directory.GetFiles(Program.AppHandler.LibsFolder, "*.xml");
            for (int i = 0; i < libFiles.Length; ++i)
                File.Delete(libFiles[i]);
            return true;
        }

        public static bool LoadLearnmaps()
        {
            string mapFolder = Program.AppHandler.MapsFolder;
            if (!System.IO.Directory.Exists(Program.AppHandler.MapsFolder))
                return false;

            Program.AppHandler.MapManager.OpenFromDirectory(Program.AppHandler.MapsFolder);

            return true;
        }

        public static bool LoadClasses()
        {
            string classFolder = Program.AppHandler.ClassesFolder;
            if (!System.IO.Directory.Exists(Program.AppHandler.ClassesFolder))
                return false;

            Program.AppHandler.ClassManager.Close();

            string[] classFiles = Directory.GetFiles(classFolder, "*.xml");
            for (int i = 0; i < classFiles.Length; ++i)
                Program.AppHandler.ClassManager.Open(classFiles[i]);

            string[] astrClassNames;
            if (Program.AppHandler.ClassManager.GetClassNames(out astrClassNames) >= 0)
            {
                if (astrClassNames!=null && astrClassNames.Length > 0)
                    foreach (var c in astrClassNames)
                    {
                        // delete all not existing Maps assigned to classes
                        string[] astrMapNames;
                        if (Program.AppHandler.ClassManager.GetLearnmapNames(c, out astrMapNames) > 0)
                            foreach (var m in astrMapNames)
                                if (Program.AppHandler.MapManager.GetId(m) < 0)
                                    Program.AppHandler.ClassManager.RemoveLearnmap(c, m);

                        string[] astrUserNames;
                        if (Program.AppHandler.ClassManager.GetUserNames(c, out astrUserNames) > 0)
                            foreach (var cu in astrUserNames)
                                if (Program.AppHandler.UserManager.GetUserId(cu) < 0)
                                    Program.AppHandler.ClassManager.RemoveUser(cu);
                    }

                if (AppCommunication.UserListChecked)
                {
                    // wer/welche Klasse ist neu?
                    foreach (var u in AppCommunication.UserList)
                    {
                        string[] strTokens = u.Split(',');
                        if (strTokens.Length == 9)
                        {
                            /* see: TCWebUpdate/ServerInfoRepository.cs
                            string strResult = r["surName"].ToString() + ',' +      [0]
                                               r["middleName"].ToString() + ',' +   [1]
                                               r["lastName"].ToString() + ',' +     [2]
                                               r["degree"].ToString() + ',' +       [3]
                                               r["userName"].ToString() + ',' +     [4]     
                                               r["password"].ToString() + ',' +     [5]
                                               r["activated"].ToString() + ',' +    [6]
                                               r["className"].ToString() + ',' +    [7]
                                               r["admin"].ToString();               [8] 
                            */
                            string strClassName = strTokens[7];
                            string strUsername = strTokens[4].ToLower();
                            // if classroom is not already created, create it
                            if (Program.AppHandler.ClassManager.GetClassId(strClassName) < 0)
                                Program.AppHandler.ClassManager.CreateClassroom(strClassName);

                            string[] astrMapNames;
                            bool bClassHasMaps = Program.AppHandler.ClassManager.GetLearnmapNames(strClassName, out astrMapNames) > 0;

                            // who got active?
                            if (Program.AppHandler.UserManager.IsUserActive(strUsername))
                            {
                                if (!Program.AppHandler.ClassManager.HasUser(strClassName, strUsername))
                                {
                                    // remove user from other classrooms
                                    if (astrClassNames != null && astrClassNames.Length > 0)
                                        foreach (var c in astrClassNames)
                                            if (c != strClassName && Program.AppHandler.ClassManager.HasUser(c, strUsername))
                                                Program.AppHandler.ClassManager.RemoveUser(c, strUsername);
                                    // add user to new classroom
                                    Program.AppHandler.ClassManager.AddUser(strClassName, strUsername);

                                    if (bClassHasMaps)
                                        foreach (var m in astrMapNames)
                                            if (!Program.AppHandler.MapManager.HasUser(m, strUsername))
                                                Program.AppHandler.MapManager.AddUser(m, strUsername);
                                }
                            }
                            else
                            {
                                Program.AppHandler.ClassManager.RemoveUser(strClassName, strUsername);

                                if (bClassHasMaps)
                                    foreach (var m in astrMapNames)
                                        if (Program.AppHandler.MapManager.HasUser(m, strUsername))
                                            Program.AppHandler.MapManager.DeleteUser(m, strUsername);
                            }

                            if (bClassHasMaps)
                            {
                                foreach (var m in astrMapNames)
                                {
                                    string[] astrUserNames;
                                    if (Program.AppHandler.ClassManager.GetUserNames(strClassName, out astrUserNames)>0)
                                        foreach (var cu in astrUserNames)
                                            if (!Program.AppHandler.MapManager.HasUser(m, cu))
                                                Program.AppHandler.MapManager.AddUser(m, cu);
                                }
                            }
                        }
                    }

                    if (Program.AppHandler.ClassManager.GetClassNames(out astrClassNames) > 0)
                    {
                        foreach (var c in astrClassNames)
                        {
                            string[] astrUserNames;
                            if (c.IndexOf("LAP", StringComparison.Ordinal) == 0 && Program.AppHandler.ClassManager.GetUserNames(c, out astrUserNames) == 0)
                                Program.AppHandler.ClassManager.DeleteClassroom(c);
                        }
                    }
                }

                string filePath = Program.AppHandler.ClassesFolder + "\\" + "classes.xml";
                Program.AppHandler.ClassManager.Save(filePath);
            }

            return true;
        }

        public static bool DestroyAllLearnmaps()
        {
            string mapFolder = Program.AppHandler.MapsFolder;
            if (!System.IO.Directory.Exists(Program.AppHandler.MapsFolder))
                return false;

            Program.AppHandler.MapManager.CloseAll();

            string[] mapFiles = System.IO.Directory.GetFiles(Program.AppHandler.MapsFolder, "*.xml");
            for (int i = 0; i < mapFiles.Length; ++i)
                System.IO.File.Delete(mapFiles[i]);
            return true;
        }


        public static void DeleteAllServerMaps()
        {
            var mapManager = Program.AppHandler.MapManager;

            bool bExit = false;
            do
            {
                string mapFound = "";
                for (int i = 0; i < mapManager.GetMapCnt(); ++i)
                {
                    mapManager.GetTitle(i, out var title);
                    var map = mapManager.GetItem(title);
                    if (map.isServerMap)
                    {
                        mapFound = title;
                        break;
                    }
                }

                if (mapFound.Length > 0)
                    mapManager.Destroy(mapFound);
                else
                    bExit = true;

            } while (!bExit);

        }

        public static bool CreateLearnmapZipFile(string strMapTitle, string strZippedTempFileName, WebtrainPackageInfo wpi)
        {
            var pi = wpi;

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            string strJsonTempFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".json";
            using (StreamWriter sw = new StreamWriter(strJsonTempFile))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, pi);
            }

            try
            {
                using (var zipDest = ZipFile.Open(strZippedTempFileName, ZipArchiveMode.Update, System.Text.Encoding.GetEncoding(850)))
                {
                    //string strLibFile = Program.AppHandler.MapManager.GetFileName(strMapTitle);
                    zipDest.CreateEntryFromFile(strJsonTempFile, "webtrainpackage.json");
                    zipDest.CreateEntryFromFile(wpi.ContentFilename, Path.GetFileName(wpi.ContentFilename));
                    zipDest.Dispose();
                    File.Delete(strJsonTempFile);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }


        public static void ExportLearnmap(string strMapTitle,int[] aToLicenses)
        {
            string startPath = Program.AppHandler.MapsFolder;
            string zipPath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".zip";

            var frm = new XFrmLongProcessToastNotification();
            frm.Start("Lernmappe wird gesendet..",
                      (string str1, string str2, DoWorkEventArgs e) =>
                      {
                          //ZipFile.CreateFromDirectory(str1, str2, CompressionLevel.Optimal, true, Encoding.GetEncoding(850));
                          e.Result = "OK";
                      },
                      (RunWorkerCompletedEventArgs e) =>
                      {
                          if (e.Result.ToString() == "OK")
                          {
                              frm.DialogResult = DialogResult.OK;
                              Program.AppHandler.MainForm.BeginInvoke((Action)(() =>
                              {
                                  string strMapFileName = Program.AppHandler.MapManager.GetFileName(strMapTitle);
                                  
                                  string[] aClasses;
                                  string strClasses = "";
                                  if (Program.AppHandler.ClassManager.GetClassNames(out aClasses) > 0)
                                  {
                                      var aResults = aClasses.Where(c => Program.AppHandler.ClassManager.HasLearnmap(c, strMapTitle));
                                      if (aResults.Count() > 0)
                                      {
                                          var aFoundClasses = aResults.ToArray();
                                          strClasses = aFoundClasses[0];
                                          for(int i=1;i<aFoundClasses.Count();++i)
                                              if (i==(aFoundClasses.Count()-1))
                                                  strClasses += (','+aFoundClasses[i]);
                                              else
                                                  strClasses += (','+aFoundClasses[i] + ',');
                                      }
                                  }

                                  var pi = new WebtrainPackageInfo();

                                  pi.PackageType = "Learnmap";
                                  pi.Autor = Program.AppHandler.LicenseId;
                                  pi.Description = String.Format("Learnmap sent by {0}", pi.Autor);
                                  pi.Title = strMapTitle;
                                  pi.Created = DateTime.Now;
                                  pi.Language = "DE_AT";
                                  pi.ContentFilename = strMapFileName;
                                  pi.Data = strClasses;

                                  AppHelpers.CreateLearnmapZipFile(strMapTitle, zipPath, pi);

                                  foreach (var i in aToLicenses)
                                  {
                                      if (i != Program.AppHandler.DongleId)
                                          AppCommunication.SendLibraryZipfile(String.Format("Lernmappe wird übermittelt an {0}", i.ToString()), i, zipPath, "Learnmap_" + strMapTitle + ".zip");
                                  }

                                  //string txt = Program.AppHandler.LanguageHandler.GetText("MESSAGE", "export_completed_successfully", "Export erfolgreich abgeschlossen!");
                                  //string cap = Program.AppHandler.LanguageHandler.GetText("SYSTEM", "Title", "WebTrain");
                                  //MessageBox.Show(txt, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                              }));

                          }
                      },
                      startPath, zipPath);
        }

        public static void ImportLearnmap(string strZipFile)
        {
            using (Ionic.Zip.ZipFile zipfile = new Ionic.Zip.ZipFile(strZipFile, System.Text.Encoding.GetEncoding(850)))
            {
                string strNewLearnmap = "";
                string strAutor = "";
                string strClasses = "";
                string strTempDir = Path.GetDirectoryName(strZipFile);
                foreach (ZipEntry e in zipfile)
                {
                    e.Extract(strTempDir);
                    if (e.FileName == "webtrainpackage.json")
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.NullValueHandling = NullValueHandling.Ignore;

                        using (StreamReader sr = new StreamReader(strTempDir + @"\webtrainpackage.json"))
                        using (JsonReader reader = new JsonTextReader(sr))
                        {
                            var wpi = (WebtrainPackageInfo)serializer.Deserialize(reader, typeof(WebtrainPackageInfo));
                            strNewLearnmap = wpi.Title;
                            strAutor = wpi.Autor;
                            strClasses = wpi.Data;
                        }
                    }
                    else
                    {
                        DefaultLearnmapManager tempMapManager=new DefaultLearnmapManager();
                        tempMapManager.OpenSilent(strTempDir+@"\"+e.FileName);
                        var newItem = tempMapManager.GetItem(strNewLearnmap);

                        // Save xml file and try to  add to MapManager
                        string strNewMapName = strAutor + '_' + strNewLearnmap;
                        int iId = 1;
                        while (Program.AppHandler.MapManager.GetId(strNewMapName)>=0)
                        {
                            ++iId;
                            strNewMapName = strAutor + '_' + strNewLearnmap + '_'+iId.ToString();
                        }

                        string filePath = Program.AppHandler.MapsFolder + "\\" + strNewMapName + ".xml";
                        if (Program.AppHandler.MapManager.Create(filePath, strNewMapName, Program.AppHandler.IsServer))
                        {
                            newItem.title = strNewMapName;
                            newItem.isServerMap = Program.AppHandler.IsServer;
                            Program.AppHandler.MapManager.SetItem(strNewMapName, newItem);
                            Program.AppHandler.MapManager.Save(strNewMapName);
                        }

                        if (strClasses!=null && strClasses.Length > 0)
                        {
                            string[] strTokens = strClasses.Split(',');
                            foreach (var c in strTokens)
                                Program.AppHandler.ClassManager.AddLearnmap(c, strNewMapName);
                        }

                    }
                }
            }
        }

        public static void ShowDocument(string strWork, DocumentActionItem docAction, bool bEditSolution = false)
        {
            string[] aTitles = null;
            Utilities.SplitPath(strWork, out aTitles);
            string filePath = Program.AppHandler.GetDocumentsFolder(aTitles[0]) + "\\" + Path.GetFileName(docAction.fileName);
            // typeId==0 -> rtf file 
            if (docAction.typeId == 0)
                Program.AppHandler.MainForm.ShowDocumentAsRtf(docAction.id, filePath, bEditSolution);
            // typeId==1 -> pptx file
            else if (docAction.typeId == 1)
            {
                var frmViewer = new XfrmPdfBrowser(docAction.id, filePath);
                frmViewer.Show();

            }
            else if (docAction.typeId == 2)
            {
                var frmViewer = new XFrmPPTXPlayer { StartPosition = FormStartPosition.CenterParent };
                frmViewer.SetPPTXFile(filePath);
                frmViewer.ShowDialog(Program.AppHandler.MainForm);
            }
            else if (docAction.typeId == 3)
            {
                string strUrl = Program.AppHandler.ContentFolder + "\\" + aTitles[0] + "\\xAPI\\" + docAction.id + "\\" + docAction.fileName; 
                Program.AppHandler.ContentManager.OpenBrowser(Program.AppHandler.MainForm, docAction.id, strUrl, new Rectangle(0, 50, 900, 630));
                Program.AppHandler.MainForm.Update();
            }
        }
    }


}
