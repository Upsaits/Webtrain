using System;
using System.Threading;
using System.Net;

namespace SoftObject.TrainConcept.ClientServer
{
    public interface ICTSServerManager
    {
        bool Create(int portId, ref AutoResetEvent jobDone);
        void SetParent(object parent);
        void Close();
        bool IsRunning();
        IPAddress GetIPAddress();
        int GetUsersOnline();
        void DisconnectUser(string userName);
        void SendClassMessage(int roomId, string toUserName, string message);
        void SendNoticeWorkoutState(string strTitle, int iWorkedOutState, string toUserName);
        void SendNotice(string toUserName, string noticeTitle);
        void SendLogout(string toUserName);
        void SendLogoutAllUsers();
        void SendTransferRequest(string toUserName, string typeName, string name);
        void SetAdapter(ICTSServerAdapter adapter);
        ICTSServerAdapter GetAdapter();
    }
}
