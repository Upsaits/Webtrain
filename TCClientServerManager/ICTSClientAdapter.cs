using System;
using System.Collections;
using System.Linq;
using System.Text;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.ClientServer
{
    public interface ICTSClientAdapter
    {
        int GetKeepAliveTime();
        int GetTimeoutStandard();
        int GetTimeoutTransfer();
        bool IsConsoleActive();
        void ShowConsole(bool isOn);
        void AddConsoleMessage(string msg);
        string GetUserName();
        string GetUserFileName();
        string GetMapsFolderPath();
        string GetLibsFolderPath();
        string GetDocumentsFolderPath(string libName);
        string GetClassesFolderPath();
        string GetNoticesDirectory(string userName);
        NoticeItemCollection GetNotices(string userName);
        void StartNoticeTransfer(string userName, string title);
        void SetNoticeWorkoutState(string strTitle, int iWorkedOutState);
        void AddNotice(int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle, int iWorkedOut, string strFileName,
                       string strModificationDate);
        bool GetCircProgressLocation(out int iXPos, out int iYPos);
    }
}
