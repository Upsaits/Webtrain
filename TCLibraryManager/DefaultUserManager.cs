using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SoftObject.TrainConcept.Libraries
{
	/// <summary>
	/// 
	/// </summary>
	public class DefaultUserManager : IUserManager
	{
		private	string	m_fileName;
		private List<UserItem> m_aUsers=new List<UserItem>();
		private bool m_isOpen=false;
        private IUserAdapter m_adapter=null;
        private UserManagerBridge m_parent = null;
        private bool m_bCentralManaged=false;
		public DefaultUserManager()
		{
		}

        public void SetParent(object parent)
        {
            m_parent = (UserManagerBridge)parent;
        }
        
        public  bool Open(string fileName)
		{
			if (m_isOpen)
				Close();

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(UserList));
				FileStream fs = new FileStream(fileName, FileMode.Open,FileAccess.Read);
				UserList ul;
				ul = (UserList) serializer.Deserialize(fs);

                m_aUsers.Clear();
                bool bIsChanged = false;
				UserItem [] aUsers = ul.Users;
                foreach (UserItem item in aUsers)
                {
                    if (HasCapitals(item.userName))
                    {
                        item.userName = item.userName.ToLower();
                        if (!bIsChanged)
                            bIsChanged = true;
                    }

                    if (item.userName == "administrator")
                    {
                        item.userName = "admin";
                        item.password = "admin";
                        if (!bIsChanged)
                            bIsChanged = true;
                    }
                    m_aUsers.Add(item);
                }
				fs.Close();

				m_fileName = fileName;
				m_isOpen = true;
                 m_bCentralManaged = !ul.isLocal;

                UserManagerEventArgs ea = new UserManagerEventArgs(UserManagerEventArgs.CommandType.Open, fileName);
                m_parent.FireEvent(ref ea);

                if (bIsChanged)
                    Save();
				return true;
			}
			catch(Exception e)
			{
				m_isOpen=false;
				return false;
			}
		}

		public  void Close()
		{
			if (m_isOpen)
				Save();
			m_aUsers.Clear();
			m_fileName="";
			m_isOpen=false;

            UserManagerEventArgs ea = new UserManagerEventArgs(UserManagerEventArgs.CommandType.Close, "");
            m_parent.FireEvent(ref ea);
		}

		public  void Save(string fileName)
		{
			try
			{
				if (m_aUsers.Count>0 && fileName.Length>0)
				{
					XmlSerializer serializer = new XmlSerializer(typeof(UserList));
					TextWriter writer = new StreamWriter(fileName);
				
					UserList usrList = new UserList();
					usrList.Users = new UserItem[m_aUsers.Count];
                    usrList.isLocal = !m_bCentralManaged;
					int i=0;
					foreach(UserItem item in m_aUsers)
						usrList.Users.SetValue(item,i++); 

					// Serialize the Userlist, and close the TextWriter.
					serializer.Serialize(writer, usrList);
					writer.Close();

					m_fileName=fileName;

                    UserManagerEventArgs ea = new UserManagerEventArgs(UserManagerEventArgs.CommandType.Update, m_fileName);
                    m_parent.FireEvent(ref ea);
				}
			}
			catch(Exception ex)
			{
                Debug.WriteLine(ex.Message);
                m_adapter.ShowMessageBox("ERROR","Failed_to_save_userfile","Fehler beim Speichern der Userdatei!");
			}
		}

		public void Save()
		{
			Save(m_fileName);
		}

		public bool IsOpen()
		{
			return m_isOpen;
		}

		public string FileName()
		{
			return m_fileName;
		}

		public  bool CreateUser(string fullName,string userName,string password, bool isActive, bool isTeacher)
		{
			if (GetUserId(userName)>=0)
				return false;

            m_aUsers.Add(new UserItem(fullName, userName, password, new UserRights(isTeacher,isTeacher), 0,true));
            
            UserManagerEventArgs ea = new UserManagerEventArgs(UserManagerEventArgs.CommandType.Create, userName);
            m_parent.FireEvent(ref ea);
            return true;
        }

		public  int  GetUserInfo(string userName, ref string password,ref string fullName,ref int imgId)
		{
			int id = GetUserId(userName);
			if (id>=0)
			{
				password = ((UserItem) m_aUsers[id]).password;
				fullName = ((UserItem) m_aUsers[id]).fullName;
                imgId    = ((UserItem) m_aUsers[id]).imgId;
				return id;
			}
			password="";
			fullName="";
            imgId = 0;
			return -1;
		}

		public  bool GetUserRights(string userName,ref bool isAdmin,ref bool isTeacher)
		{
			int id=GetUserId(userName);
			if (id<0)
				return false;
			
			UserItem usr=(UserItem)m_aUsers[id];
			isAdmin = usr.userRights.isAdministrator;
			isTeacher = usr.userRights.isTeacher;
			return true;
		}
		
		public  bool SetUserRights(string userName,bool isAdmin,bool isTeacher)
		{
			int id=GetUserId(userName);
			if (id<0)
				return false;
			
			UserItem usr=(UserItem)m_aUsers[id];
			usr.userRights.isAdministrator = isAdmin;
			usr.userRights.isTeacher = isTeacher;
			return true;
		}

		public  bool SetUserInfo(string userName,string fullName,string password,int imgId)
		{
			int id=GetUserId(userName);
			if (id<0)
				return false;
			
			UserItem usr=(UserItem)m_aUsers[id];
			usr.fullName=fullName;
			usr.password=password;
            usr.imgId = imgId;
			return true;
		}

		public  bool DeleteUser(string userName)
		{
			int id=GetUserId(userName);
			if (id>=0)
			{
				m_aUsers.RemoveAt(id);
                UserManagerEventArgs ea = new UserManagerEventArgs(UserManagerEventArgs.CommandType.Destroy, userName);
                m_parent.FireEvent(ref ea);
				return true;
			}
			return false;
		}

		public  int  GetUserNames(out string [] aNames,bool bOnlyActive)
        {
            List<string> aEntries=new List<string>();
			foreach(var u in m_aUsers)
                if ((bOnlyActive && u.isActive) || !bOnlyActive)
                {
                    aEntries.Add(u.userName);
                }

            aNames = aEntries.ToArray();
            return aNames.Length;
        }

		public int GetUserId(string userName)
		{
            if (userName.Length > 0)
            {
                //todo: Replace with Linq
                userName = userName.ToLower();
                for (int i = 0; i != m_aUsers.Count; ++i)
                {
                    UserItem usr = (UserItem)m_aUsers[i];
                    if (String.Compare(usr.userName, userName, true) == 0)
                        return i;
                }
            }
			return -1;
		}

        public void SetAdapter(IUserAdapter adapter)
        {
            m_adapter = adapter;
        }

        public int GetUserCount()
        {
            return m_aUsers.Count;
        }

        public bool IsCentralManaged()
        {
            return m_bCentralManaged;
        }

        public void SetCentralManaged(bool bCentralManaged, List<string> lUserList)
        {
            if (bCentralManaged && !m_bCentralManaged)
            {
                string oldFileName = m_fileName;
                string strPath = Path.GetDirectoryName(m_fileName);
                string strLocalName = strPath + @"\users_local.xml";

                Save(strLocalName);
                Close();

                CreateUser("Administrator", "admin", "admin", true, true);
                SetUserRights("admin", true, true);

                foreach (var u in lUserList)
                {
                    string[] strTokens = u.Split(',');
                    if (strTokens.Length == 9)
                    {
                        /* see: TCWebUpdate/ServerInfoRepository.cs
                        string strResult = r["surName"].ToString() + ',' +      [0]
                                           r["middleName"].ToString() + ',' +   [1]
                                           r["lastName"].ToString() + ',' +     [2]
                                           r["degree"].ToString() + ',' +       [3]
                                           r["userName"].ToString() + ',' +     [4]     
                                           r["password"].ToString() + ',' +     [5]
                                           r["activated"].ToString() + ',' +    [6]
                                           r["className"].ToString() + ',' +    [7]
                                           r["admin"].ToString();               [8]
                         */
                        string strFullName;
                        if (strTokens[1].Length > 0)
                            strFullName = strTokens[0] + ' ' + strTokens[1] + ' ' + strTokens[2];
                        else
                            strFullName = strTokens[0] + ' ' + strTokens[2];

                        bool bIsActive  = Convert.ToInt32(strTokens[6]) == 1 ? true : false;
                        bool bIsTeacher = Convert.ToInt32(strTokens[8]) == 1;

                        CreateUser(strFullName, strTokens[4].ToLower(), strTokens[5], bIsActive, bIsTeacher);
                    }
                }

                m_bCentralManaged = bCentralManaged;
                Save(oldFileName);
                m_isOpen = true;

                UserManagerEventArgs ea = new UserManagerEventArgs(UserManagerEventArgs.CommandType.ChangeManagement, m_fileName);
                m_parent.FireEvent(ref ea);
            }
            else if (!bCentralManaged && m_bCentralManaged)
            {
                string strPath = Path.GetDirectoryName(m_fileName);
                string strOldFilename=strPath + @"\users_local.xml";
                if (File.Exists(strOldFilename))
                {
                    Close();
                    Open(strOldFilename);
                    Save(strPath + @"\users.xml");
                }

                m_bCentralManaged = false;
                UserManagerEventArgs ea = new UserManagerEventArgs(UserManagerEventArgs.CommandType.ChangeManagement, m_fileName);
                m_parent.FireEvent(ref ea);
            }
        }

        public void UpdateCentralManagedUsers(List<string> lUserList, Action<string> delLogProcess=null)
        {
            if (m_bCentralManaged)
            {
                Dictionary<string, bool> dPresentUsers = new Dictionary<string, bool>(m_aUsers.Count);
                foreach (var u in m_aUsers)
                {
                    if (u.userName != "admin")
                        dPresentUsers[u.userName] = u.isActive;
                }

                Dictionary<string, bool> dWorkedOut = new Dictionary<string, bool>(lUserList.Count);
                foreach (var u in lUserList)
                {
                    string[] strTokens = u.Split(',');

                    if (delLogProcess != null)
                        delLogProcess.Invoke($"tokenlength {strTokens.Length}");

                    if (strTokens.Length == 9)
                    {
                        /* see: TCWebUpdate/ServerInfoRepository.cs
                        string strResult = r["surName"].ToString() + ',' +      [0]
                                           r["middleName"].ToString() + ',' +   [1]
                                           r["lastName"].ToString() + ',' +     [2]
                                           r["degree"].ToString() + ',' +       [3]
                                           r["userName"].ToString() + ',' +     [4]     
                                           r["password"].ToString() + ',' +     [5]
                                           r["activated"].ToString() + ',' +    [6]
                                           r["className"].ToString() + ',' +    [7]
                                           r["admin"].ToString();               [8]
                         */
                        string strFullName;
                        if (strTokens[1].Length > 0)
                            strFullName = strTokens[0] + ' ' + strTokens[1] + ' ' + strTokens[2];
                        else
                            strFullName = strTokens[0] + ' ' + strTokens[2];

                        string strUsername = strTokens[4].ToLower();
                        bool bIsActive = Convert.ToInt32(strTokens[6]) == 1 ? true : false;
                        bool bIsTeacher = Convert.ToInt32(strTokens[8]) == 1 ? true : false;

                        int iId = GetUserId(strUsername);

                        if (delLogProcess!=null)
                            delLogProcess.Invoke($"update {strUsername}");

                        if (iId < 0) // nicht vorhanden, neu erstellen
                            CreateUser(strFullName, strUsername, strTokens[5], bIsActive, bIsTeacher);
                        else
                        {
                            var user = m_aUsers[iId];
                            user.password = strTokens[5];
                            user.isActive = bIsActive;
                            user.userRights.isTeacher = bIsTeacher;
                            user.userRights.isAdministrator = bIsTeacher;
                        }

                        dWorkedOut[strUsername] = true;
                    }
                }


                // alle die nicht gefunden wurden löschen!
                foreach (var p in dPresentUsers)
                {
                    if (!dWorkedOut.Keys.Contains(p.Key))
                    {
                        DeleteUser(p.Key);
                    }
                }

                // alle die nicht gefunden wurden löschen!
                foreach (var w in dWorkedOut)
                {
                    if (!w.Value)
                    {
                        DeleteUser(w.Key);
                    }
                }

                Save();
            }
        }

        private bool HasCapitals(string strText)
        {
            foreach (var c in strText)
                if (char.IsUpper(c))
                    return true;
            return false;
        }

        public bool IsUserActive(string userName)
        {
            int iId = GetUserId(userName);
            if (iId >= 0)
                return m_aUsers[iId].isActive;
            return false;
        }
    }
}
