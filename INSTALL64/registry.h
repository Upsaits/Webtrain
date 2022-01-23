#pragma once

class CRegistry
{
	protected:
		CString	m_startKey;

	public:
		CRegistry(const CString &startKey);
		virtual ~CRegistry();

		virtual bool   Get(const char* key,const char* name,      CString& data, HKEY parentKey = HKEY_LOCAL_MACHINE);	//!< Hole daten mit angegebenen Namen
		virtual bool   Set(const char* key,const char* name,const CString& data, HKEY parentKey = HKEY_LOCAL_MACHINE);	//!< schreibe daten wieder weg

		virtual bool   Get(const char* key,const char* name,  int& data, HKEY parentKey = HKEY_LOCAL_MACHINE);			//!< Hole daten mit angegebenen Namen
		virtual bool   Set(const char* key,const char* name,  int  data, HKEY parentKey = HKEY_LOCAL_MACHINE);			//!< schreibe daten wieder weg

		virtual bool   Get(const char* key,const char* name, double& data, HKEY parentKey = HKEY_LOCAL_MACHINE);			//!< Hole daten mit angegebenen Namen
		virtual bool   Set(const char* key,const char* name, double  data, HKEY parentKey = HKEY_LOCAL_MACHINE);			//!< schreibe daten wieder weg

		virtual void   Delete(const char *key, const char *name, HKEY parentKey = HKEY_LOCAL_MACHINE);					//!< lösche daten
		virtual void   Delete(const char *key, HKEY parentKey = HKEY_LOCAL_MACHINE);										//!< lösche key

		virtual bool   EnumNames (const char* key,CStringArray& array);				//!< delivers all names 
		virtual	bool   FindKey(const char *keyStart,const char *key,CString &keyPath);					//!< finds a key 

		virtual CString GetStartKey() {return m_startKey;}
	
		LONG	Create(HKEY hKeyParent,LPCTSTR lpszKeyName);

		double	String2Double(const  CString& strg);
		void	Double2String(double d,CString& strg);
		int		String2Int(const CString& strg);
		void	Int2String(int i,  CString& strg);
};