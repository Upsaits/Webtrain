using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SoftObject.TrainConcept.ClientServer;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Adapter
{
    class CTSServerAdapterImpl : ICTSServerAdapter
    {
        private FrmCTSServerConsole m_ctsServerConsole = null;
        private NoticeItemCollection m_aOldNotices = null;
        private NoticeItemCollection m_aNewNotices = null;
        private ICTSServerManager m_parentServerManager = null;
        private AppHandler AppHandler = Program.AppHandler;
        public CTSServerAdapterImpl(FrmCTSServerConsole frmServerConsole) 
        {
            m_ctsServerConsole = frmServerConsole;
        }

        public int GetTimeoutStandard()
        {
            return AppHandler.TimeOutStandard;
        }

        public int GetTimeoutTransfer()
        {
            return AppHandler.TimeOutTransfer;
        }

        public bool IsConsoleActive()
        {
            return AppHandler.CtsConsoleOn;
        }

        public void ShowConsole(bool isOn)
        {
            if (m_ctsServerConsole != null)
            {
                if (isOn)
                    m_ctsServerConsole.Show();
                else
                    m_ctsServerConsole.Hide();
            }
        }

        public void CloseConsole()
        {
            if (m_ctsServerConsole != null)
                m_ctsServerConsole.Close();
        }

        public string GetUserName()
        {
            return AppHandler.MainForm.ActualUserName;
        }

        public SoftObject.TrainConcept.ClientServer.ReceiveClassMessageDelegate GetDelegateReceiveClassMessage()
        {
            return AppHandler.MainForm.ReceiveClassMessage;
        }

        public string GetUserFileName()
        {
            return AppHandler.UserManager.FileName();
        }

        public int GetMapCnt()
        {
            return AppHandler.MapManager.GetMapCnt();
        }

        public bool MapHasUser(int mapId, string userName)
        {
            return AppHandler.MapManager.HasUser(mapId, userName);
        }

        public string GetMapFileName(int id)
        {
            return AppHandler.MapManager.GetFileName(id);
        }
        
        public string GetClassFileName()
        {
            return AppHandler.ClassManager.FileName();
        }

        public bool HasLibrary(string name)
        {
            return AppHandler.LibManager.GetLibrary(name) != null;
        }

        public int GetAllLibraryNames(ref List<String> aNames)
        {
            aNames.Clear();
            for (int i = 0; i < AppHandler.LibManager.GetLibraryCnt(); ++i)
            {
                var lib = AppHandler.LibManager.GetLibrary(i);
                aNames.Add(lib.title);
            }

            return aNames.Count;
        }

        public string GetLibraryFileName(string name)
        {
            return AppHandler.LibManager.GetFilePath(name);
        }

        public bool AskTestPermission(string userName, string mapTitle, string testName)
        {
            if (!AppHelpers.IsTestInMapAllowed(userName, mapTitle, testName))
                return false;
            AppHandler.TestResultManager.Start(userName, mapTitle, testName, DateTime.Now);
            return true;
        }

        public void SetTestResult(string testResult)
        {
            TestResultItem tri = AppHandler.TestResultManager.CreateResultItem(testResult);
            if (tri != null)
            {
                string s1 = tri.ToString();
                AppHandler.TestResultManager.End(tri);
            }
            else
            {
                AddConsoleMessage("unknow Testresult!");
            }
        }

        public int GetUserInfo(string userName, ref string passWord, ref string fullName)
        {
            int imgId = 0;
            return AppHandler.UserManager.GetUserInfo(userName, ref passWord, ref fullName, ref imgId);
        }

        public void AddConsoleMessage(string msg)
        {
            if (m_ctsServerConsole != null)
                m_ctsServerConsole.Add(msg);
            else
                Debug.WriteLine(msg);

        }

        public string GetLanguage()
        {
            return AppHandler.Language;
        }

        public void AddUserProgressInfo(string userName, string workingTitle, int iRegionId, ushort iRegionVal)
        {
            AppHelpers.AddUserProgressInfo(userName, workingTitle, iRegionId, iRegionVal);
            if ((UserProgressInfoManager.RegionType) iRegionId == UserProgressInfoManager.RegionType.Session)
            {
                byte hb = HelperMacros.HIBYTE((ushort) iRegionVal);
                byte lb = HelperMacros.LOBYTE((ushort) iRegionVal);

                if (lb == 1)
                {
                    //AppHandler.NoticeManager.DeleteNotices(userName);
                    m_parentServerManager.SendTransferRequest(userName, "NOTICELIST", "*");
                }
            }
        }

        public string GetNoticesDirectory(string userName)
        {
            return AppHandler.NoticeManager.GetNoticePath() + "\\" + userName;
        }

        public void StartNoticeTransfer(string userName, string title)
        {
            int nId = AppHandler.NoticeManager.Find(userName, title);
            if (nId >= 0)
                AppHandler.NoticeManager.SetDirty(nId, false);
        }

        public void AddNotice(int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle,
                              int iWorkedOut, string strFileName,
                              string strModificationDate)
        {
            // nothing to do?
            if (iCount <= 0)
                return;

            if (iCount > 1 && iId == 1) // 1st entry initializes helper collections
            {
                m_aOldNotices = new NoticeItemCollection();
                m_aNewNotices = new NoticeItemCollection();
                AppHandler.NoticeManager.Find(ref m_aOldNotices, userName); // remember old Notices
            }

            if (iCount > 0) // new notice comes in
            {
                int nId = AppHandler.NoticeManager.Find(userName, strTitle); // search
                NoticeItem ni = null;
                if (nId >= 0) // ok. Found
                {
                    ni = AppHandler.NoticeManager.GetNotice(nId); // get notice
                    string filePath = String.Format("{0}\\notices\\{1}", GetNoticesDirectory(userName), ni.fileName);
                    string filePathNew = String.Format("{0}\\cache\\{1}", GetNoticesDirectory(userName), ni.fileName);

                    try
                    {
                        // if notice isn't a task, copy it to server
                        if (ni.workedOutState < 0 || ni.dirtyFlag == 0)
                        {
                            if (File.Exists(filePathNew))
                                File.Copy(filePathNew, filePath, true);
                            ni.dirtyFlag = 0;
                            ni.ModificationDateString = strModificationDate;

                            /*
                            var dDocFiles=AppHandler.LibManager.GetDocumentFilenames();
                            string strDocFileName="";
                            String[] aTitles;
                            Utilities.SplitPath(ni.contentPath, out aTitles);
                            var libDocs = dDocFiles.FirstOrDefault(s => s.Key == aTitles[0]);
                            if (libDocs.Value!=null)
                            {
                                var doc = libDocs.Value.FirstOrDefault(f => f.Key == ni.title);
                                strDocFileName = AppHandler.ContentFolder+@"\"+doc.Value;
                            }
                            
                            string strFileCorrect = strDocFileName.Replace(".rtf", "_loesung.rtf");
                            if (File.Exists(strFileCorrect))
                            {
                                AppHandler.MainForm.BeginInvoke((Action)(() =>
                                {
                                    RichEditControl richEditControl1 = new RichEditControl();
                                    RichEditControl richEditControl2 = new RichEditControl();

                                    richEditControl1.LoadDocument(filePath, DocumentFormat.Rtf);
                                    richEditControl2.LoadDocument(strFileCorrect, DocumentFormat.Rtf);

                                    string strText1 = richEditControl1.Document.HtmlText;
                                    string strText2 = richEditControl2.Document.HtmlText;

                                    HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(strText1, strText2);

                                    // Lets add a block expression to group blocks we care about (such as dates)
                                    diffHelper.AddBlockExpression(new Regex(@"[\d]{1,2}[\s]*(Jan|Feb)[\s]*[\d]{4}", RegexOptions.IgnoreCase));

                                    int iFoundDiffCount;
                                    int iFoundInsCount;
                                    int iFoundDelCount;
                                    int iFoundReplCount;

                                    string strResult = diffHelper.Build(out iFoundDiffCount, out iFoundInsCount, out iFoundDelCount, out iFoundReplCount);

                                    if (iFoundReplCount >= 2 && iFoundDelCount>=1 && iFoundDiffCount>=3)
                                        ni.workedOutState=6;
                                    else
                                        ni.workedOutState=1;
                                    AppHandler.NoticeManager.Save();
                                    m_parentServerManager.SendNoticeWorkoutState(ni.title, ni.workedOutState, ni.userName);
                                }));
                            }
                            else
                            {*/
                            AppHandler.NoticeManager.Save();
                            //}

                            if (m_aNewNotices != null)
                                m_aNewNotices.Add(ni);
                        }
                        else // delete it
                        {
                            if (File.Exists(filePathNew))
                                File.Delete(filePathNew);
                        }
                    }
                    catch (System.Exception /*ex*/)
                    {
                        m_ctsServerConsole.Add(String.Format("Couldn't copy Notice {0}", strTitle));
                    }
                }
                else // wasn't found
                {
                    if (iWorkedOut <= 0)
                    {
                        nId = AppHandler.NoticeManager.CreateNotice(userName, strTitle, contentPath, iPageId,
                            iWorkedOut >= 0);
                        if (nId >= 0)
                        {
                            ni = AppHandler.NoticeManager.GetNotice(nId);
                            string filePath = String.Format("{0}\\notices\\{1}", GetNoticesDirectory(userName),
                                ni.fileName);
                            string filePathNew = String.Format("{0}\\cache\\{1}", GetNoticesDirectory(userName),
                                ni.fileName);

                            ni.title = strTitle;
                            ni.fileName = strFileName;
                            ni.dirtyFlag = 0;

                            try
                            {
                                if (File.Exists(filePathNew))
                                {
                                    File.Copy(filePathNew, filePath, true);
                                    File.Delete(filePathNew);
                                }

                                AppHandler.NoticeManager.Save();
                                if (m_aNewNotices != null)
                                    m_aNewNotices.Add(ni);
                            }
                            catch (System.Exception /*ex*/)
                            {
                                AppHandler.NoticeManager.DeleteNotice(nId);
                            }
                        }
                    }
                }

            }

            if (iId == iCount)
            {
                if ((m_aOldNotices != null && m_aOldNotices.Count > 0) && m_aNewNotices != null)
                {
                    foreach (NoticeItem n in m_aNewNotices)
                    {
                        var res = m_aOldNotices.FirstOrDefault(x => x.title == n.title);
                        if (res != null)
                            m_aOldNotices.Remove(res);
                    }

                    foreach (NoticeItem n in m_aOldNotices)
                    {
                        int nId = AppHandler.NoticeManager.Find(userName, n.title);
                        NoticeItem ni = AppHandler.NoticeManager.GetNotice(nId);

                        string filePath = String.Format("{0}\\notices\\notice_{1}", GetNoticesDirectory(userName),
                            ni.fileName);
                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        AppHandler.NoticeManager.DeleteNotice(nId);
                        AppHandler.NoticeManager.Save();
                    }
                }
            }

        }

        public void SetNoticeWorkoutState(string userName, string strTitle, int iState)
        {
            int iNoticeId = AppHandler.NoticeManager.Find(userName, strTitle);
            if (iNoticeId >= 0)
            {
                var ni = AppHandler.NoticeManager.GetNotice(iNoticeId);
                if (ni.workedOutState != iState)
                {
                    ni.workedOutState = iState;
                    AppHandler.NoticeManager.Save();
                }
            }
        }

        public NoticeItemCollection GetNotices(string userName)
        {
            var aNotices = new NoticeItemCollection();
            AppHandler.NoticeManager.Find(ref aNotices, userName);
            return aNotices;
        }

        public bool GetDocumentsFileNames(string userName,ref SortedList<string, Dictionary<string, string>> dDocs)
        {
            SortedList<string, Dictionary<string, string>> _dDocs = AppHandler.LibManager.GetDocumentFilenames();
            foreach (var d in _dDocs)
            {
                foreach (var f in d.Value)
                {
                    List<KeyValuePair<string, string>> dMaps;
                    if (AppHandler.MapManager.FindMapsByWorking(d.Key, out dMaps) > 0)
                    {
                        if (AppHandler.MapManager.HasUser(dMaps[0].Key, userName))
                        {
                            if (Path.GetExtension(f.Value) == ".rtf")
                            {
                                if (dDocs.Keys.IndexOf(d.Key)<0)
                                    dDocs[d.Key] = new Dictionary<string, string>();
                                dDocs[d.Key][f.Key] = AppHandler.ContentFolder + '\\' + f.Value;

                                string strSolFilename = "";
                                if (Utilities.GetSolutionFile(dDocs[d.Key][f.Key], ref strSolFilename))
                                {
                                    dDocs[d.Key][f.Key+"__Solution"] = strSolFilename;
                                }
                            }
                        }
                    }
                }
            }

            return (dDocs.Count > 0);
        }

        public int AskMapProgress(string userName, string mapTitle)
        {
            return AppHelpers.GetMapProgress(userName, mapTitle);

        }

        public int AskWorkProgress(string userName, string mapTitle, string workTitle)
        {
            return AppHelpers.GetWorkProgress(userName, mapTitle, workTitle);
        }

        public string AskWorkProgress(string userName, string mapTitle)
        {
            return AppHelpers.GetWorkProgress(userName, mapTitle);
        }

        public bool AskChangePassword(string userName, string passWord)
        {
            return AppHelpers.TryToChangeUserPassword(userName, passWord);
        }

        public IPAddress SelectIPAddress()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            var iCnt = host.AddressList.Where(o => o.AddressFamily == AddressFamily.InterNetwork);
            if (iCnt.Count() > 1)
            {
                if (AppHandler.CtsVPNServerManager.GetAdapter() == this)
                {
                    foreach (IPAddress ip in host.AddressList)
                        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                            ip.ToString().IndexOf("10.10.10.1", StringComparison.Ordinal) == 0)
                        {
                            AppHandler.MainForm.BeginInvoke((Action) (() =>
                            {
                                m_ctsServerConsole.SetTitle(ip.ToString());
                                m_ctsServerConsole.Show();
                            }));
                            return ip;
                        }
                        else
                        {
                            AppHandler.MainForm.BeginInvoke((Action)(() =>
                            {
                                m_ctsServerConsole.Hide();
                            }));

                        }
                    return null;
                }
                else
                {
                    var frm = new FrmSelectIPAddress();
                    frm.ShowDialog();
                    AppHandler.MainForm.BeginInvoke((Action)(() =>
                    {
                        m_ctsServerConsole.SetTitle(frm.SelIPAddress.ToString());
                        m_ctsServerConsole.Show();
                    }));

                    return frm.SelIPAddress;
                }
            }

            if (AppHandler.CtsVPNServerManager.GetAdapter() != this)
                foreach (IPAddress ip in host.AddressList)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        AppHandler.MainForm.BeginInvoke((Action)(() =>
                        {
                            //MessageBox.Show("bin drin");
                            Thread.Sleep(500);
                            m_ctsServerConsole.SetTitle(ip.ToString());
                            m_ctsServerConsole.Show();
                        }));
                        return ip;
                    }

            return null;
        }

        public void SetParentCTSServerManager(ICTSServerManager serverManager)
        {
            m_parentServerManager = serverManager;
        }
    }
}
