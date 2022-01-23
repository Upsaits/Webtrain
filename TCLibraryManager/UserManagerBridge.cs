using System;
using System.Collections.Generic;

namespace SoftObject.TrainConcept.Libraries
{
    public class UserManagerBridge
    {

        public event OnUserManagerHandler UserManagerEventHandler;
        public event OnUserManagerHandler UserManagerEvent
        {
            add { UserManagerEventHandler += value; }
            remove { UserManagerEventHandler -= value; }
        }

        private IUserManager m_imp;

        public UserManagerBridge(IUserManager imp)
        {
            m_imp = imp;
            m_imp.SetParent(this);
        }

        public bool Open(string fileName)
        {
            return m_imp.Open(fileName);
        }

        public void Close()
        {
            m_imp.Close();
        }

        public void Save(string fileName)
        {
            m_imp.Save(fileName);
        }

        public void Save()
        {
            m_imp.Save();
        }

        public bool IsOpen()
        {
            return m_imp.IsOpen();
        }

        public string FileName()
        {
            return m_imp.FileName();
        }

        public bool CreateUser(string fullName, string userName, string password, bool isActive=true,bool isTeacher=false)
        {
            return m_imp.CreateUser(fullName, userName, password, isActive, isTeacher);
        }
        
        public int GetUserInfo(string userName, ref string password, ref string fullName, ref int imgId)
        {
            return m_imp.GetUserInfo(userName, ref password, ref fullName, ref imgId);
        }

        public bool GetUserRights(string userName, ref bool isAdmin, ref bool isTeacher)
        {
            return m_imp.GetUserRights(userName, ref isAdmin, ref isTeacher);
        }

        public bool SetUserRights(string userName, bool isAdmin, bool isTeacher)
        {
            return m_imp.SetUserRights(userName, isAdmin, isTeacher);
        }

        public bool SetUserInfo(string userName, string fullName, string password, int imgId)
        {
            return m_imp.SetUserInfo(userName, fullName, password, imgId);
        }

        public bool DeleteUser(string userName)
        {
            return m_imp.DeleteUser(userName);
        }

        public int GetUserNames(out string[] aNames, bool bOnlyActive=true)
        {
            return m_imp.GetUserNames(out aNames,bOnlyActive);
        }

        public int GetUserId(string userName)
        {
            return m_imp.GetUserId(userName);
        }

        public void SetAdapter(IUserAdapter adapter)
        {
            m_imp.SetAdapter(adapter);
        }

        public int GetUserCount()
        {
            return m_imp.GetUserCount();
        }

        public bool IsCentralManaged()
        {
            return m_imp.IsCentralManaged();
        }

        public void SetCentralManaged(bool bCentralManaged, List<string> lUserList)
        {
            m_imp.SetCentralManaged(bCentralManaged,lUserList);
        }

        public void UpdateCentralManagedUsers(List<string> lUserList,Action<string> delLogProcess = null)
        {
            m_imp.UpdateCentralManagedUsers(lUserList, delLogProcess);
        }

        public bool IsUserActive(string userName)
        {
            return m_imp.IsUserActive(userName);
        }

        public void FireEvent(ref UserManagerEventArgs ea)
        {
            if (UserManagerEventHandler != null)
                UserManagerEventHandler(this, ref ea);
        }

    }
}
