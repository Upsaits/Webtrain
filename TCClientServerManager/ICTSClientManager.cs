using System;
using System.Threading;
using System.Windows.Forms;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.ClientServer
{
    public interface ICTSClientManager
    {
        bool Create(string ipAddr, int portId);
        void SetParent(object parent);
        void Close();
        void StartKeepAlive();
        void SendLogin(string userName, string passWord);
        bool SendLogin(string userName, string passWord, ref AutoResetEvent jobDone);
        bool SendLogin(string language, string userName, string passWord, ref AutoResetEvent jobDone);
        void SendTransferRequest(string typeName, string name);
        bool SendTransferRequest(string typeName, string name, ref AutoResetEvent jobDone);
        void SendClassMessage(int roomId, string toUserName, string message);
        bool SendClassMessage(int roomId, string toUserName, string message, ref AutoResetEvent jobDone);
        void AskTestPermission(string userName, string mapTitle, string testName);
        bool AskTestPermission(string userName, string mapTitle, string testName, ref AutoResetEvent jobDone);
        void SendTestResult(TestResultItem testResult);
        void SendUserProgressInfo(string userName, string workingName, int iRegionId, ushort iRegionValue);
        void SendNotice(string noticeTitle);
        bool SendNotice(string noticeTitle, ref AutoResetEvent jobDone);
        void SendNoticeWorkoutState(string userName, string strTitle, int iWorkedOutState);
        void AskMapProgress(string userName, string mapTitle);
        bool AskMapProgress(string userName, string mapTitle, ref AutoResetEvent jobDone);
        void AskWorkProgress(string userName, string mapTitle, string workTitle);
        bool AskWorkProgress(string userName, string mapTitle, string workTitle, ref AutoResetEvent jobDone);
        void AskChangePassword(string userName, string passWord);
        bool AskChangePassword(string userName, string passWord, ref AutoResetEvent jobDone);
        bool IsRunning();
        void AbortTransfer();
        void SetAdapter(ICTSClientAdapter adapter);
        ICTSClientAdapter GetAdapter();
    }
}
