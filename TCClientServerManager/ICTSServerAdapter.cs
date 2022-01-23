using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftObject.TrainConcept.Libraries;

namespace SoftObject.TrainConcept.ClientServer
{
    public delegate void ReceiveClassMessageDelegate(int roomId, string userName, string message);
    public interface ICTSServerAdapter
    {
        int     GetTimeoutStandard();
        int     GetTimeoutTransfer();
        bool    IsConsoleActive();
        void    ShowConsole(bool isOn);
        void    CloseConsole();
        void    AddConsoleMessage(string msg);
        string  GetUserName();
        ReceiveClassMessageDelegate GetDelegateReceiveClassMessage();
        string  GetUserFileName();
        int     GetMapCnt();
        bool    MapHasUser(int mapId,string userName);
        string  GetMapFileName(int id);
        string  GetClassFileName();
        int     GetAllLibraryNames(ref List<String> aNames);
        bool    HasLibrary(string name);
        string  GetLibraryFileName(string name);
        bool    AskTestPermission(string userName, string mapTitle,string testName);
        int     AskMapProgress(string userName, string mapTitle);
        int     AskWorkProgress(string userName, string mapTitle, string workTitle);
        string  AskWorkProgress(string userName, string mapTitle);
        bool    AskChangePassword(string userName, string passWord);
        void    SetTestResult(string testResult);
        int     GetUserInfo(string userName,ref string passWord,ref string fullName);
        string  GetLanguage();
        void    AddUserProgressInfo(string userName, string workingTitle, int iRegionId, ushort iRegionVal);
        string  GetNoticesDirectory(string userName);
        NoticeItemCollection GetNotices(string userName);
        void    StartNoticeTransfer(string userName, string title);
        void    AddNotice(int iCount, int iId, string userName, string contentPath, int iPageId, string strTitle, int iWorkedOut, string strFileName, 
                          string strModificationDate);
        void SetNoticeWorkoutState(string userName, string strTitle, int iWorkoutState);
        bool GetDocumentsFileNames(string strUserName, ref SortedList<string, Dictionary<string, string>> dDocs);
        System.Net.IPAddress SelectIPAddress();     
        void SetParentCTSServerManager(ICTSServerManager serverManager);
    }
}
