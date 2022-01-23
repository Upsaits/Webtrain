using System;

namespace SoftObject.SOComponents.UtilityLibrary
{
	/// <summary>
	/// 
	/// </summary>
	public class LanguageHandler : ProfileHandler
	{
		private bool m_isValid=false;

		public bool IsValid
		{
			get
			{
				return m_isValid;
			}
		}

		public LanguageHandler()
		{
		}

		public LanguageHandler(string folder,string fileName,string language)
		{
			SetLanguage(folder,fileName,language);
		}

		public bool SetLanguage(string folder,string fileName,string language)
		{
			string filePath = folder+"\\"+fileName+"."+language;
			if (!System.IO.File.Exists(filePath))
				return false;
			SetFileName(filePath);
			m_isValid=true;
			return true;
		}

		public string GetText(string section,int id)
		{
			return GetProfileString(section,id.ToString(),"#### missing Text #####");
		}

		public string GetText(string section,string key)
		{
			return GetProfileString(section,key,key);
		}

		public string GetText(string section,string key,string defkey)
		{
			return GetProfileString(section,key,defkey);
		}
	}
}
