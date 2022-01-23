using System;
using SoftObject.UtilityLibrary.Win32;
using System.Collections.Generic;
using System.Text;

namespace SoftObject.SOComponents.UtilityLibrary
{
	/// <summary>
	/// Zusammendfassende Beschreibung für ProfileHandler.
	/// </summary>
	public class ProfileHandler
	{
		string m_fileName=null;

		public ProfileHandler()
		{
			m_fileName = "";
		}

		public ProfileHandler(string fileName)
		{
			m_fileName = fileName;
		}

		public void SetFileName(String fileName)
		{
			m_fileName = fileName;
		}

		public String GetProfileString(String section,String key,String defKey)
		{
			//StringBuilder temp = new StringBuilder(255);
			string temp = new string(' ',255);
			int ret=WindowsAPI.GetPrivateProfileStringW(section,key,defKey,temp,255,m_fileName);
			return temp.Split('\0')[0];
		}

		public int GetProfileInt(String section,String key,int defKey)
		{
			return WindowsAPI.GetPrivateProfileIntW(section,key,defKey,m_fileName);
		}

		public bool WriteProfileString(String section,String key,String val)
		{
			return (WindowsAPI.WritePrivateProfileStringW(section,key,val,m_fileName))>0 ? true:false;
		}

		public bool WriteProfileInt(String section,String key,int val)
		{
			String sVal= String.Format("{0}",val);
			return (WindowsAPI.WritePrivateProfileStringW(section,key,sVal,m_fileName))>0 ? true:false;
		}

        public Dictionary<String,String> GetAllKeys(String section)
        {

            var dResult = new Dictionary<string, string>();

            /*
            List<string> result = new List<string>();
            byte[] buffer = new byte[1024];
            WindowsAPI.GetPrivateProfileSectionNames(buffer, buffer.Length, m_fileName);
            string allSections = System.Text.Encoding.Default.GetString(buffer);
            string[] sectionNames = allSections.Split('\0');
            foreach (string sectionName in sectionNames)
            {
                result.Add(sectionName);
            }*/

            byte[] buffer = new byte[256000];

            int iRet=WindowsAPI.GetPrivateProfileSectionW(section, buffer,256000, m_fileName);
            if (iRet >= 0)
            {
                String tmp = Encoding.ASCII.GetString(buffer);
                String tmp1 = tmp.Trim('\0');
                String[] tmp2 = tmp1.Split(new String[] { "\0\0" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tmp2.Length; ++i)
                    tmp2[i] = tmp2[i].Replace("\0", "");


                foreach (String entry in tmp2)
                    if (entry.Length > 0)
                    {
                        int found = entry.IndexOf('=');
                        if (found > 0)
                        {
                            dResult.Add(entry.Substring(0, found), entry.Substring(found + 1, entry.Length - (found + 1)));
                        }
                    }
            }                        
            return dResult;
        }
    }
}
