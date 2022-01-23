using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using SoftObject.TrainConcept.TestResults;

namespace SoftObject.TrainConcept
{
	public delegate void OnCTSClientHandler(object sender,ref CTSClientEventArgs ea);

	public class CTSClientTransferData
	{
		private string m_typeName;
		private string m_name;
		private string m_fileName;
		private long m_fileSize=0;
		private long m_actualSize=0;
		StreamWriter m_stream=null;
		private double m_percDone=0;
		private bool m_isActive=false;
		private int m_packageCnt=0;
		private int m_packageId=0;

		public string TypeName
		{
			get{return m_typeName;}
		}

		public string Name
		{
			get{return m_name;}
		}
		
		public string FileName
		{
			get{return m_fileName;}
		}

		public double PercDone
		{
			get{return m_percDone;}
		}
		
		public bool IsActive
		{
			get{return m_isActive;}
		}

		public int PackageCount
		{
			get{return m_packageCnt;}
		}
	
		public int PackageId
		{
			get{return m_packageId;}
			set{m_packageId = value;}
		}

		public CTSClientTransferData(string typeName,string name,string fileName,int fileSize)
		{
			m_typeName = typeName;
			m_name = name;
			m_packageCnt=1;
			m_packageId =1;
			m_fileName = fileName;
			m_fileSize = fileSize;
		}

		public CTSClientTransferData(string typeName,string name,int packageCnt,int packageId,
									 string fileName,int fileSize)
		{
			m_typeName = typeName;
			m_name = name;
			m_packageCnt=packageCnt;
			m_packageId =packageId;
			m_fileName = fileName;
			m_fileSize = fileSize;
		}

		public void Start()
		{
			m_actualSize=0;
			m_stream = new StreamWriter(m_fileName,false,System.Text.Encoding.UTF8);
			m_stream.AutoFlush=true;
			m_isActive = true;
		}

		public void Stop()
		{
			if (m_stream!=null)
			{
				m_stream.Close();
				m_stream=null;
			}
			m_isActive=false;
		}

		public void DoTransfer(string text)
		{
			if (m_isActive)
			{
				m_stream.Write(text);
				m_actualSize=m_stream.BaseStream.Position;
				m_percDone = ((double)m_actualSize)*100/((double)m_fileSize);
				Trace.WriteLine("-->"+String.Format("{0},{1})", m_actualSize,m_fileSize));
				Trace.WriteLine(String.Format("TEXT({0})",text));
				if (m_actualSize>=m_fileSize)
					Stop();
			}
		}

		public bool IsLastPackage()
		{
			return m_packageCnt == m_packageId;
		}
	}


	public class CTSClientEventArgs : EventArgs
	{
		public enum CommandType {Login,Logout,TransferStart,TransferProgress,TransferEnd,
								 ReceiveClassMesssage,TestPermission};
		private CommandType m_command;
		private string m_userName;
		private string m_mapName;
		private int m_retValue;
		private CTSClientTransferData m_transferData;
		private int m_roomId;
		private string m_message;
		
		public CommandType Command
		{
			get
			{
				return m_command;
			}
		}

		public string UserName
		{
			get {return m_userName;}
		}

		public string MapName
		{
			get {return m_mapName;}
		}

		public int ReturnValue
		{
			get 
			{
				return m_retValue;
			}
		}

		public int RoomId
		{
			get
			{
				return m_roomId;
			}
		}

		public string Message
		{
			get {return m_message;}
		}

		public CTSClientTransferData TransferData
		{
			get{ return m_transferData;}
		}

		public CTSClientEventArgs(CommandType command,string userName,int retValue)
		{
			m_command = command;
			m_userName = userName;
			m_retValue = retValue;
		}

		public CTSClientEventArgs(CommandType command,string userName,string mapName,int retValue)
		{
			m_command = command;
			m_userName = userName;
			m_mapName = mapName;
			m_retValue = retValue;
		}

		public CTSClientEventArgs(CommandType command,CTSClientTransferData transferData)
		{
			m_command = command;
			m_transferData = transferData;
		}

		public CTSClientEventArgs(CommandType command,string userName,int roomId,string message)
		{
			m_command = command;
			m_userName = userName;
			m_roomId = roomId;
			m_message = message;
		}
	}

	public interface ICTSClientManager
	{
		bool Create(string ipAddr,int portId);
		void SetParent(object parent);
		void Close();
		void StartKeepAlive();
		void SendLogin(string userName,string passWord);
		bool SendLogin(string userName,string passWord,ref AutoResetEvent jobDone);
		bool SendLogin(string language,string userName,string passWord,ref AutoResetEvent jobDone);
		void SendTransferRequest(string typeName,string name);
		bool SendTransferRequest(string typeName,string name,ref AutoResetEvent jobDone);
		void SendClassMessage(int roomId,string toUserName,string message);
		bool SendClassMessage(int roomId,string toUserName,string message,ref AutoResetEvent jobDone);
		void AskTestPermission(string userName,string mapTitle);
		bool AskTestPermission(string userName,string mapTitle,ref AutoResetEvent jobDone);
		void SendTestResult(TestResultItem testResult);
		bool SendTestResult(TestResultItem testResult,ref AutoResetEvent jobDone);
		bool IsRunning();
		void AbortTransfer();
	}

	public class CTSClientManager
	{
		public event OnCTSClientHandler CTSClientEventHandler;
		public event OnCTSClientHandler OnCTSClientEvent
		{
			add { CTSClientEventHandler += value;}
			remove { CTSClientEventHandler -= value;}
		}

		private ICTSClientManager	m_imp;
		
		public CTSClientManager(ICTSClientManager imp)
		{
			m_imp = imp;
			m_imp.SetParent(this);
		}

		public bool Create(string ipAddr,int portId)
		{
			return m_imp.Create(ipAddr,portId);
		}

		public void Close()
		{
			m_imp.Close();
		}

		public bool IsRunning()
		{
			return m_imp.IsRunning();
		}

		public void SendLogin(string userName,string passWord)
		{
			m_imp.SendLogin(userName,passWord);
		}

		public bool SendLogin(string userName,string passWord,ref AutoResetEvent jobDone)
		{
			return m_imp.SendLogin(userName,passWord,ref jobDone);
		}

		public bool SendLogin(string language,string userName,string passWord,ref AutoResetEvent jobDone)
		{
			return m_imp.SendLogin(language,userName,passWord,ref jobDone);
		}

		public void SendTransferRequest(string typeName,string name)
		{
			m_imp.SendTransferRequest(typeName,name);
		}

		public bool SendTransferRequest(string typeName,string name,ref AutoResetEvent jobDone)
		{
			return m_imp.SendTransferRequest(typeName,name,ref jobDone);
		}

		public void SendClassMessage(int roomId,string toUserName,string message)
		{
			m_imp.SendClassMessage(roomId,toUserName,message);
		}

		public bool SendClassMessage(int roomId,string toUserName,string message,ref AutoResetEvent jobDone)
		{
			return m_imp.SendClassMessage(roomId,toUserName,message,ref jobDone);
		}

		public void FireEvent(ref CTSClientEventArgs ea)
		{
			if (CTSClientEventHandler!=null)
				CTSClientEventHandler(this,ref ea);
		}

		public void AskTestPermission(string userName,string mapTitle)
		{
			m_imp.AskTestPermission(userName,mapTitle);
		}

		public bool AskTestPermission(string userName,string mapTitle,ref AutoResetEvent jobDone)
		{
			return m_imp.AskTestPermission(userName,mapTitle,ref jobDone);
		}
		
		public void SendTestResult(TestResultItem testResult)
		{
			m_imp.SendTestResult(testResult);
		}
		
		public bool SendTestResult(TestResultItem testResult,ref AutoResetEvent jobDone)
		{
			return 	m_imp.SendTestResult(testResult,ref jobDone);
		}

		public void StartKeepAlive()
		{
			m_imp.StartKeepAlive();
		}

		public void AbortTransfer()
		{
			m_imp.AbortTransfer();
		}
	}
}
