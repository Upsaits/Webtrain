#ifndef registry_h
#define registry_h

class CRegistry
{
	protected:
		CString	m_startKey;

	public:
		CRegistry(const CString &startKey);
		virtual ~CRegistry();

		virtual bool   Get(const char* key,const char* name,      CString& data);	//!< Hole daten mit angegebenen Namen
		virtual bool   Set(const char* key,const char* name,const CString& data);	//!< schreibe daten wieder weg

		virtual bool   Get(const char* key,const char* name,  int& data);			//!< Hole daten mit angegebenen Namen
		virtual bool   Set(const char* key,const char* name,  int  data);			//!< schreibe daten wieder weg

		virtual bool   Get(const char* key,const char* name, double& data);			//!< Hole daten mit angegebenen Namen
		virtual bool   Set(const char* key,const char* name, double  data);			//!< schreibe daten wieder weg

		virtual void   Delete(const char *key, const char *name);					//!< lösche daten
		virtual void   Delete(const char *key);										//!< lösche key

		virtual bool   EnumNames (const char* key,CStringArray& array);				//!< delivers all names 
		virtual	bool   FindKey(const char *keyStart,const char *key,CString &keyPath);					//!< finds a key 

		virtual CString GetStartKey() {return m_startKey;}

		double	String2Double(const  CString& strg);
		void	Double2String(double d,CString& strg);
		int		String2Int(const CString& strg);
		void	Int2String(int i,  CString& strg);

	protected:
		LONG	Create(HKEY hKeyParent,LPCTSTR lpszKeyName);
};



#endif