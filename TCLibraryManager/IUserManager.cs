using System;
using System.Collections;
using System.Collections.Generic;

namespace SoftObject.TrainConcept.Libraries
{
    public delegate void OnUserManagerHandler(object sender, ref UserManagerEventArgs ea);
	/// <summary>
	/// Summary description for IUserManager.
	/// </summary>
	public interface IUserManager
	{
        void SetParent(object parent);
        bool Open(string fileName);
		void Close();
		void Save(string fileName);
		void Save();
		bool IsOpen();
		string FileName();
		bool CreateUser(string fullName,string userName,string password,bool isActive,bool isTeacher);
		int  GetUserInfo(string userName,ref string password,ref string fullName,ref int imgId);
		bool SetUserInfo(string userName,string fullName,string password,int imgId);
		bool GetUserRights(string userName,ref bool isAdmin,ref bool isTeacher);
		bool SetUserRights(string userName,bool isAdmin,bool isTeacher);
		bool DeleteUser(string userName);
		int  GetUserNames(out string [] aNames,bool bOnlyActive);
		int GetUserId(string userName);
        void SetAdapter(IUserAdapter adapter);
        int GetUserCount();
        bool IsCentralManaged();
        void SetCentralManaged(bool bCentralManaged,List<string> lUserList);
        void UpdateCentralManagedUsers(List<string> lUserList,Action<string> delLogProcess = null);
        bool IsUserActive(string userName);
    }	
}