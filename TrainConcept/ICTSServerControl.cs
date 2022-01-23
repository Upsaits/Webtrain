using System;
using System.Threading;

namespace SoftObject.TrainConcept
{
	public delegate void OnCTSServerHandler(object sender,ref CTSServerEventArgs ea);

	public class CTSServerEventArgs : EventArgs
	{
		public enum CommandType {Login,Logout};
		private CommandType m_command;
		private string m_target;
		private string m_userName;
		private string m_password;
		private int m_retValue;
		private string m_language;
		
		public CommandType Command
		{
			get
			{
				return m_command;
			}
		}

		public string Target
		{
			get
			{
				return m_target;
			}
		}

		public string UserName
		{
			get
			{
				return m_userName;
			}
		}

		public string Password
		{
			get
			{
				return m_password;
			}
		}

		public string Language
		{
			get
			{
				return m_language;
			}
		}

		public int ReturnValue
		{
			get 
			{
				return m_retValue;
			}
			set
			{
				m_retValue = value;
			}
		}

		public CTSServerEventArgs(CommandType command,string target,string userName,string password,string language)
		{
			m_command = command;
			m_target = target;
			m_userName = userName;
			m_password = password;
			m_language = language;
		}

		public CTSServerEventArgs(CommandType command,string userName)
		{
			m_command = command;
			m_userName = userName;
		}
	}

	public interface ICTSServerManager
	{
		void Create(int portId,ref AutoResetEvent jobDone);
		void SetParent(object parent);
		void Close();
		bool IsRunning();
		int  GetUsersOnline();
		void DisconnectUser(string userName);
		void AddMessage(string sMessage);
		void SendClassMessage(int roomId,string toUserName,string message);
	}
	
	public class CTSServerManager
	{
		public event OnCTSServerHandler CTSServerEventHandler;
		public event OnCTSServerHandler OnCTSServerEvent
		{
			add { CTSServerEventHandler += value;}
			remove { CTSServerEventHandler -= value;}
		}

		private ICTSServerManager	m_imp;
		
		public CTSServerManager(ICTSServerManager imp)
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

		public void AddMessage(string sMessage)
		{
			m_imp.AddMessage(sMessage);
		}

		public void SendClassMessage(int roomId,string toUserName,string message)
		{
			m_imp.SendClassMessage(roomId,toUserName,message);
		}

		public void DisconnectUser(string userName)
		{
			m_imp.DisconnectUser(userName);
		}

		public int  GetUsersOnline()
		{
			return m_imp.GetUsersOnline();
		}

		public void FireEvent(ref CTSServerEventArgs ea)
		{
			if (CTSServerEventHandler!=null)
				CTSServerEventHandler(this,ref ea);
		}
	}
}