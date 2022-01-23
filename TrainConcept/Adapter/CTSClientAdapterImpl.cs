using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SoftObject.TrainConcept.ClientServer;
using SoftObject.TrainConcept.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.Adapter
{
    class CTSClientAdapterImpl : ICTSClientAdapter
    {
        private NoticeItemCollection m_aOldNotices = null;
        private NoticeItemCollection m_aNewNotices = null;
        FrmCTSServerConsole m_ctsClientConsole=null;
        private AppHandler AppHandler = Program.AppHandler;
        public int GetKeepAliveTime()
        {
            return AppHandler.KeepAliveTimer;
        }

        public int GetTimeoutStandard()
        {
            return AppHandler.TimeOutStandard;
        }

        public int GetTimeoutTransfer()
        {
            return AppHandler.TimeOutTransfer;
        }

        public string GetUserName()
        {
            return AppHandler.MainForm.ActualUserName;
        }

        public string GetUserFileName()
        {
            return AppHandler.UserFileName;
        }

        public string GetClassesFolderPath()
        {
            if (AppHandler.IsSingle)
            {
                string strTempPath = Path.GetTempPath() + Guid.NewGuid().ToString();
                if (!Directory.Exists(strTempPath))
                    Directory.CreateDirectory(strTempPath);
                return strTempPath;
            }
            return AppHandler.ClassesFolder;
        }

        public string GetMapsFolderPath()
        {
            if (AppHandler.IsSingle)
            {
                string strTempPath = Path.GetTempPath() + Guid.NewGuid().ToString();
                if (!Directory.Exists(strTempPath))
                    Directory.CreateDirectory(strTempPath);
                return strTempPath;
            }
            return AppHandler.MapsFolder;
        }

        public string GetLibsFolderPath()
        {
            if (AppHandler.IsSingle)
            {
                string strTempPath = Path.GetTempPath() + Guid.NewGuid().ToString();
                if (!Directory.Exists(strTempPath))
                    Directory.CreateDirectory(strTempPath);
                return strTempPath;
            }
            return AppHandler.LibsFolder;
        }

        public NoticeItemCollection GetNotices(string userName)
        {
            var aNotices = new NoticeItemCollection();
            AppHandler.NoticeManager.Find(ref aNotices, userName);
            return aNotices;
        }

        public string GetNoticesDirectory(string userName)
        {
            return AppHandler.NoticeManager.GetNoticePath() + "\\" + userName;
        }

        public void StartNoticeTransfer(string userName, string title)
        {
            int nId = AppHandler.NoticeManager.Find(userName, title);
            if (nId >= 0)
                AppHandler.NoticeManager.SetDirty(nId,false);
        }

        public void AddNotice(int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle, int iWorkedOut, string strFileName,
                              string strModificationDate)
        {
            // nothing to do?
            if (iCount <= 0)
                return;

            if (iId == 1)
            {
                m_aOldNotices = new NoticeItemCollection();
                m_aNewNotices = new NoticeItemCollection();
                AppHandler.NoticeManager.Find(ref m_aOldNotices, userName);
            }

            if (iCount > 0) // new notice comes in
            {
                int nid = AppHandler.NoticeManager.Find(userName, strTitle);
                NoticeItem ni = null;
                if (nid >= 0)
                {
                    ni = AppHandler.NoticeManager.GetNotice(nid);
                    string filePath = String.Format("{0}\\notices\\{1}", GetNoticesDirectory(userName), ni.fileName);
                    string filePathNew = String.Format("{0}\\cache\\{1}", GetNoticesDirectory(userName), ni.fileName);

                    try
                    {
                        DateTime dtModified = DateTime.Now;
                        bool bDateTimeValid=false;
                        if (strModificationDate.Length > 0)
                        {
                            try
                            {
                                dtModified = DateTime.Parse(strModificationDate);
                                bDateTimeValid = true;
                            }
                            catch (System.Exception /*ex*/)
                            {
                            }
                        }

                        // tasks? => replace existing ones.
                        if (iWorkedOut>=0)
                        {
                            if (File.Exists(filePathNew))
                                File.Copy(filePathNew, filePath, true);
                            ni.workedOutState = iWorkedOut;

                            string txt = AppHandler.LanguageHandler.GetText("MESSAGE", "task_X_has_been_worked_out", "Die Aufgabe {0} wurde ausgearbeitet!");
                            string txt1 = String.Format(txt,ni.title);
                            AppHandler.MainForm.TaskbarNotifier.Show("", txt1, 500, 3000, 500);
                            string msg = String.Format("{0} {1}", DateTime.UtcNow.ToString(), txt1);
                            AppHandler.MainForm.Messages.Add(msg);

                            AppHandler.NoticeManager.Save();
                        }
                        else if (bDateTimeValid && (ni.ModificationDate <= dtModified))
                        {
                            if (File.Exists(filePathNew))
                                File.Copy(filePathNew, filePath, true);
                        }
                        else // no tasks or older notices from user => don't take it
                        {
                            if (File.Exists(filePathNew))
                                File.Delete(filePathNew);
                        }

                        if (m_aNewNotices != null) // remember new ones
                            m_aNewNotices.Add(ni);
                    }
                    catch (System.Exception /*ex*/)
                    {

                    }
                }
                else
                {
                    nid = AppHandler.NoticeManager.CreateNotice(userName, strTitle, contentPath, iPageId, iWorkedOut >= 0);
                    if (nid >= 0)
                    {
                        ni = AppHandler.NoticeManager.GetNotice(nid);
                        ni.title = strTitle;
                        ni.fileName = strFileName;
                        ni.dirtyFlag = 0;

                        string filePath = String.Format("{0}\\notices\\{1}", GetNoticesDirectory(userName), ni.fileName);
                        string filePathNew = String.Format("{0}\\cache\\{1}", GetNoticesDirectory(userName), ni.fileName);
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
                            AppHandler.NoticeManager.DeleteNotice(nid);
                        }
                    }
                }
            }

            if (iId == iCount)
            {
                // do cleanup of local notices..
                if ((m_aOldNotices != null && m_aOldNotices.Count > 0) && m_aNewNotices != null)
                {
                    foreach (NoticeItem n in m_aNewNotices)
                    {
                        // first remove all notices which came in
                        var res = m_aOldNotices.FirstOrDefault(x => x.title == n.title);
                        if (res != null)
                            m_aOldNotices.Remove(res);
                    }

                    // now we only have the one who didn't come in.
                    foreach (NoticeItem n in m_aOldNotices)
                    {
                        int nId = AppHandler.NoticeManager.Find(userName, n.title);
                        NoticeItem ni = AppHandler.NoticeManager.GetNotice(nId);
                        // notices which where tasks were not sent from the server -> remove it.
                        if (ni.workedOutState >= 0)
                        {
                            string filePath = String.Format("{0}\\notices\\notice_{1}", GetNoticesDirectory(userName), ni.fileName);
                            if (File.Exists(filePath))
                                File.Delete(filePath);

                            AppHandler.NoticeManager.DeleteNotice(nId);
                            AppHandler.NoticeManager.Save();
                        }
                    }
                }
            }


        }

        public bool GetCircProgressLocation(out int iXPos, out int iYPos)
        {
            iXPos = AppHandler.MainForm.CircularProgressLocation.X;
            iYPos = AppHandler.MainForm.CircularProgressLocation.Y;
            return true;
        }

        public void SetNoticeWorkoutState(string strTitle, int iWorkedOutState)
        {
			int nid = AppHandler.NoticeManager.Find(AppHandler.MainForm.ActualUserName,strTitle);
            if (nid >= 0)
            {
                if (iWorkedOutState == 7) // notiz löschen?
                {
                    AppHandler.NoticeManager.DeleteNotice(nid);
                    AppHandler.NoticeManager.Save();
                }
                else
                {
                    NoticeItem ni = AppHandler.NoticeManager.GetNotice(nid);
                    ni.workedOutState = iWorkedOutState;
                    AppHandler.NoticeManager.Save();
                    AppHandler.MainForm.Notice.UpdateWorkoutState(ni.title);
                }
            }
        }

        public string GetDocumentsFolderPath(string libName)
        {
            return AppHandler.GetDocumentsFolder(libName);
        }

        public bool IsConsoleActive()
        {
            return (m_ctsClientConsole != null);
        }

        public void ShowConsole(bool isOn)
        {
            if (isOn)
            {
                if (m_ctsClientConsole == null)
                    m_ctsClientConsole = new FrmCTSServerConsole();
                m_ctsClientConsole.Text = "CTS-Client-Console";
                m_ctsClientConsole.Show();
            }
            else
            {
                if (m_ctsClientConsole != null)
                {
                    m_ctsClientConsole.Close();
                    m_ctsClientConsole = null;
                }
            }
        }

        public void AddConsoleMessage(string msg)
        {
            if (m_ctsClientConsole != null)
                m_ctsClientConsole.Add(msg);
            else
                Debug.WriteLine(msg);
        }
    }
}
