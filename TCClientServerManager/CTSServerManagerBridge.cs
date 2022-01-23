using System;
using System.Threading;
using System.Net;

namespace SoftObject.TrainConcept.ClientServer
{
	public delegate void OnCTSServerHandler(object sender, CTSServerEventArgs ea);
	
	public class CTSServerManagerBridge
	{
		public event OnCTSServerHandler CTSServerEventHandler;
		public event OnCTSServerHandler OnCTSServerEvent
		{
            add
            {
                CTSServerEventHandler += value;
            }
            remove
            {
                CTSServerEventHandler -= value;
            }
		}

		private ICTSServerManager	m_imp;
		
		public CTSServerManagerBridge(ICTSServerManager imp)
		{
			m_imp = imp;
			m_imp.SetParent(this);
		}

		public void Create(int portId,ref AutoResetEvent jobDone)
		{
			m_imp.Create(portId,ref jobDone);
		}

		public void Close()
		{
			m_imp.Close();
		}

		public bool IsRunning()
		{
			return m_imp.IsRunning();
		}

        public IPAddress GetIPAddress()
        {
            return m_imp.GetIPAddress();
        }

		public void SendClassMessage(int roomId,string toUserName,string message)
		{
			m_imp.SendClassMessage(roomId,toUserName,message);
		}

        public void SendNoticeWorkoutState(string strTitle, int iWorkedOutState, string toUserName)
        {
            m_imp.SendNoticeWorkoutState(strTitle, iWorkedOutState, toUserName);
        }

        public void SendNotice(string toUserName, string noticeTitle)
        {
            m_imp.SendNotice(toUserName, noticeTitle);
        }

        public void SendTransferRequest(string toUserName, string typeName, string name)
        {
            m_imp.SendTransferRequest(toUserName, typeName, name);
        }
        
        public void DisconnectUser(string userName)
		{
			m_imp.DisconnectUser(userName);
		}

		public int  GetUsersOnline()
		{
			return m_imp.GetUsersOnline();
		}

		public void FireEvent(CTSServerEventArgs ea)
		{
			if (CTSServerEventHandler!=null)
				CTSServerEventHandler(this, ea);
		}
        
        public void SetAdapter(ICTSServerAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public ICTSServerAdapter GetAdapter()
        {
            return m_imp.GetAdapter();
        }

        public void SendLogout(string toUserName)
        {
			m_imp.SendLogout(toUserName);
        }

		public void SendLogoutAllUsers()
        {
            m_imp.SendLogoutAllUsers();
        }
    }
}