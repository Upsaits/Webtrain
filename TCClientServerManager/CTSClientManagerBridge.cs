using System;
using System.Threading;
using System.Windows.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.ClientServer
{
    public class CTSClientManagerBridge
    {
        public event OnCTSClientHandler CTSClientEventHandler;
        public event OnCTSClientHandler OnCTSClientEvent
        {
            add { CTSClientEventHandler += value; }
            remove { CTSClientEventHandler -= value; }
        }

        private ICTSClientManager m_imp;

        public CTSClientManagerBridge(ICTSClientManager imp)
        {
            m_imp = imp;
            m_imp.SetParent(this);
        }

        public bool Create(string ipAddr, int portId)
        {
            return m_imp.Create(ipAddr, portId);
        }

        public void Close()
        {
            m_imp.Close();
        }

        public bool IsRunning()
        {
            return m_imp.IsRunning();
        }

        public void SendLogin(string userName, string passWord)
        {
            m_imp.SendLogin(userName, passWord);
        }

        public bool SendLogin(string userName, string passWord, ref AutoResetEvent jobDone)
        {
            return m_imp.SendLogin(userName, passWord, ref jobDone);
        }

        public void SendTransferRequest(string typeName, string name)
        {
            m_imp.SendTransferRequest(typeName, name);
        }

        public bool SendTransferRequest(string typeName, string name, ref AutoResetEvent jobDone)
        {
            return m_imp.SendTransferRequest(typeName, name, ref jobDone);
        }

        public void SendClassMessage(int roomId, string toUserName, string message)
        {
            m_imp.SendClassMessage(roomId, toUserName, message);
        }

        public bool SendClassMessage(int roomId, string toUserName, string message, ref AutoResetEvent jobDone)
        {
            return m_imp.SendClassMessage(roomId, toUserName, message, ref jobDone);
        }

        public void FireEvent(ref CTSClientEventArgs ea)
        {
            if (CTSClientEventHandler != null)
                CTSClientEventHandler(this, ea);
        }

        public void AskTestPermission(string userName, string mapTitle,string testName)
        {
            m_imp.AskTestPermission(userName, mapTitle,testName);
        }

        public bool AskTestPermission(string userName, string mapTitle, string testName, ref AutoResetEvent jobDone)
        {
            return m_imp.AskTestPermission(userName, mapTitle, testName, ref jobDone);
        }

        public void SendTestResult(TestResultItem testResult)
        {
            m_imp.SendTestResult(testResult);
        }

        public void SendUserProgressInfo(string userName, string workingName, int iRegionId, ushort iRegionValue)
        {
            m_imp.SendUserProgressInfo(userName, workingName, iRegionId, iRegionValue);
        }

        public void SendNotice(string noticeTitle)
        {
            m_imp.SendNotice(noticeTitle);
        }

        public bool SendNotice(string noticeTitle, ref AutoResetEvent jobDone)
        {
            return m_imp.SendNotice(noticeTitle, ref jobDone);
        }

        public void SendNoticeWorkoutState(string userName, string strTitle, int iWorkedOutState)
        {
            m_imp.SendNoticeWorkoutState(userName,strTitle,iWorkedOutState);
        }

        public void AskMapProgress(string userName, string mapTitle)
        {
            m_imp.AskMapProgress(userName, mapTitle);
        }

        public bool AskMapProgress(string userName, string mapTitle, ref AutoResetEvent jobDone)
        {
            return m_imp.AskMapProgress(userName, mapTitle, ref jobDone);
        }

        public void AskWorkProgress(string userName, string mapTitle, string workTitle)
        {
            m_imp.AskWorkProgress(userName, mapTitle, workTitle);
        }

        public bool AskWorkProgress(string userName, string mapTitle, string workTitle, ref AutoResetEvent jobDone)
        {
            return m_imp.AskWorkProgress(userName, mapTitle, workTitle, ref jobDone);
        }

        public void AskChangePassword(string userName, string passWord)
        {
            m_imp.AskChangePassword(userName, passWord);
        }

        public bool AskChangePassword(string userName, string passWord, ref AutoResetEvent jobDone)
        {
            return m_imp.AskChangePassword(userName, passWord, ref jobDone);
        }

        public void StartKeepAlive()
        {
            m_imp.StartKeepAlive();
        }

        public void AbortTransfer()
        {
            m_imp.AbortTransfer();
        }

        public void SetAdapter(ICTSClientAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public ICTSClientAdapter GetAdapter()
        {
            return m_imp.GetAdapter();
        }

    }
}
