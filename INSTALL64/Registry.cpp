#include "stdafx.h"

#include <atlbase.h>
#include "secdesc.h"
#include "registry.h"

/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param keine
 *
 * @return  : 
 */
CRegistry::CRegistry(const CString &startKey)
{
	m_startKey=startKey;
}


/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param keine
 *
 * @return  : 
 */
CRegistry::~CRegistry()
{
}

/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param key : 
 * @param name : 
 * @param data : 
 *
 * @return bool  : 
 */
bool CRegistry::Get(const char* key,const char* name, CString& data, HKEY parentKey)
{
	CRegKey	regKey;
	TCHAR value[255];
	ULONG len=255;

	if (regKey.Open(parentKey,m_startKey+key,KEY_EXECUTE)!=ERROR_SUCCESS)
		return false;

	if(regKey.QueryStringValue(name,value,&len)==ERROR_SUCCESS)
	{
		data = value;
		return true;
	}

	return false;
}


/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param key : 
 * @param name : 
 * @param data : 
 *
 * @return bool  : 
 */
bool CRegistry::Set(const char* key,const char* name,const CString& data, HKEY parentKey)
{
	CRegKey	regKey;
	if (regKey.Open(parentKey,m_startKey+key)!=ERROR_SUCCESS)
	{
		if (Create(parentKey,m_startKey+key)!=ERROR_SUCCESS || 
			regKey.Open(parentKey,m_startKey+key)!=ERROR_SUCCESS)
			return false;
	}

	regKey.SetStringValue(name,data);
	return true;
}

/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param key : 
 * @param name : 
 * @param data : 
 *
 * @return bool  : 
 */
bool CRegistry::Get(const char* key,const char* name,int& data, HKEY parentKey)
{
	CRegKey	regKey;
	DWORD   dwValue=0;

	if (regKey.Open(parentKey,m_startKey+key,KEY_EXECUTE)!=ERROR_SUCCESS)
		return false;

	if(regKey.QueryDWORDValue(name,dwValue)==ERROR_SUCCESS)
	{
		data = (int)dwValue;
		return true;
	}
	return false;
}

/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param key : 
 * @param name : 
 * @param data : 
 *
 * @return bool  : 
 */
bool CRegistry::Set(const char* key,const char* name,  int  data, HKEY parentKey)
{
	CRegKey	regKey;
	if (regKey.Open(parentKey,m_startKey+key,KEY_READ)!=ERROR_SUCCESS)
	{
		if (Create(parentKey,m_startKey+key)!=ERROR_SUCCESS || 
			regKey.Open(parentKey,m_startKey+key)!=ERROR_SUCCESS)
			return false;
	}
	regKey.SetDWORDValue(name,(DWORD)data);
	return true;
}


/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param key : 
 * @param name : 
 * @param data : 
 *
 * @return bool  : 
 */
bool CRegistry::Get(const char* key,const char* name, double& data, HKEY parentKey)
{
	CString strg;

	if (Get(key,name,strg))
	{
		data = String2Double(strg);
		return true;
	}
	return false;
}

/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param key : 
 * @param name : 
 * @param data : 
 *
 * @return bool  : 
 */
bool CRegistry::Set(const char* key,const char* name, double  data, HKEY parentKey)
{
	CString strg;
	Double2String(data,strg);
	
	return Set(key,name,strg);
}

/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param key : 
 * @param array : 
 *
 * @return bool CRegistry::EnumNames  : 
 */
bool CRegistry::EnumNames (const char* key,CStringArray& array)
{
	array.RemoveAll();

	bool back = false;

	CString sRegPath = m_startKey;
	sRegPath += key;

	HKEY   hKey=0;
	DWORD  retCode=0;

	retCode = RegOpenKeyEx (HKEY_LOCAL_MACHINE,    // Key handle at root level.
					        sRegPath,     // Path name of child key.
							0,           // Reserved.
							KEY_EXECUTE, // Requesting read access.
							&hKey);      // Address of key to be returned.

	if (retCode == ERROR_SUCCESS) 
	{	
		CHAR   ClassName[MAX_PATH];
		DWORD  dwcClassLen = MAX_PATH;
		DWORD  dwcSubKeys;
		DWORD  dwcMaxSubKey;
		DWORD  dwcMaxClass;
		DWORD  dwcValues;
		DWORD  dwcMaxValueName;
		DWORD  dwcMaxValueData;
		DWORD  dwcSecDesc;
		FILETIME  ftLastWriteTime;

		retCode =
		RegQueryInfoKey (hKey,              // Key handle.
					   ClassName,         // Buffer for class name.
					   &dwcClassLen,      // Length of class string.
					   NULL,              // Reserved.
					   &dwcSubKeys,       // Number of sub keys.
					   &dwcMaxSubKey,     // Longest sub key size.
					   &dwcMaxClass,      // Longest class string.
					   &dwcValues,        // Number of values for this key.
					   &dwcMaxValueName,  // Longest Value name.
					   &dwcMaxValueData,  // Longest Value data.
					   &dwcSecDesc,       // Security descriptor.
					   &ftLastWriteTime); // Last write time.

		if (dwcSubKeys > 0 && retCode == ERROR_SUCCESS)
		{
			DWORD j=0;

			dwcMaxSubKey++;  // \0 dazufügen
			char* cKeyName=(char*) malloc(dwcMaxSubKey);
			memset(cKeyName,0,dwcMaxSubKey);

			array.SetSize(dwcSubKeys);
			for (j = 0; j < dwcSubKeys; j++)
			{
				FILETIME ft;
				DWORD dwcSubKey = dwcMaxSubKey;// Size of value name.

				retCode = RegEnumKeyEx(
							hKey,				// handle to key to enumerate
							j,					// index of subkey to enumerate
							cKeyName,			// address of buffer for subkey name
							&dwcSubKey,			// address for size of subkey buffer
							NULL,				// reserved
							NULL,				// address of buffer for class string
							NULL,				// address for size of class buffer
							&ft					 // address for time key last written to
							);
				
				if (retCode == ERROR_SUCCESS)
				{
					array[j]=cKeyName;
					back = true;
				}
				else
				{
					back = false;
					break;
				}
			}
			free (cKeyName);
		}
	    RegCloseKey (hKey);   // Close the key handle.
	}

	return back;
}


bool CRegistry::FindKey(const char *keyStart,const char *key,CString &keyPath)
{
	CStringArray aKeys;
	CString		 l_keyStart(keyStart);

	if (EnumNames(keyStart,aKeys))
	{
		for(int i=0;i<aKeys.GetSize();++i)
		{
			keyPath=m_startKey+keyStart;//+"\\"+aKeys[i];

			if (aKeys[i]==key)
				return true;
			else
			{
				if (FindKey(l_keyStart+"\\"+aKeys[i],key,keyPath))
					return true;
			}
		}
	}
	return false;
}


/*!
* <Genaue Beschreibung der Methode>
*
* @param strg : 
*
* @return double  : 
*/
void CRegistry::Delete(const char *key, const char *name,HKEY parentKey)
{
	CRegKey	regKey;

	if (regKey.Open(parentKey,m_startKey+CString(key))==ERROR_SUCCESS)
		regKey.DeleteValue(name);
}

/*!
* <Genaue Beschreibung der Methode>
*
* @param strg : 
*
* @return double  : 
*/
void CRegistry::Delete(const char *key,HKEY parentKey)
{
	CRegKey	regKey;
	if (regKey.Open(parentKey,m_startKey)==ERROR_SUCCESS)
		regKey.RecurseDeleteKey(key);
}


/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param strg : 
 *
 * @return double  : 
 */
double CRegistry::String2Double(const  CString& strg)
{
	return atof(strg);
}


/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param d : 
 * @param strg : 
 *
 * @return void   : 
 */
void   CRegistry::Double2String(double d,CString& strg)
{
	strg.Format("%7.3f",d);
}


/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param strg : 
 *
 * @return int  : 
 */
int CRegistry::String2Int(const CString& strg)
{
	return atoi(strg);
}


/*!
 * <Genaue Beschreibung der Methode>
 *
 * @param i : 
 * @param strg : 
 *
 * @return void  : 
 */
void CRegistry::Int2String(int i,  CString& strg)
{
	strg.Format("%d",i);
}


LONG CRegistry::Create(HKEY hKeyParent,LPCTSTR lpszKeyName)
{
	LONG lResult=ERROR_SUCCESS;

	CSid sidEveryone(CSid::WST_EVERYONE);
	CSid sidLocalAdmin(CSid::WST_LOCALADMINS);

	CTrustee trEveryone(TRUSTEE_IS_WELL_KNOWN_GROUP, sidEveryone);
	CTrustee trLocalAdmin(TRUSTEE_IS_GROUP,sidLocalAdmin);


	EXPLICIT_ACCESS ea[2];
	ea[0] = CExplicitAccess(KEY_ALL_ACCESS, SET_ACCESS, SUB_CONTAINERS_AND_OBJECTS_INHERIT, trEveryone);
	ea[1] = CExplicitAccess(KEY_ALL_ACCESS, SET_ACCESS, SUB_CONTAINERS_AND_OBJECTS_INHERIT, trLocalAdmin);

	CAcl acl;
	lResult = acl.SetEntriesInAcl(2, ea);
	if(lResult == ERROR_SUCCESS)
	{
		// Initialize a security descriptor and add our ACL to it  
		CSecurityDescriptor sd;
		if(sd.SetSecurityDescriptorDacl(TRUE,     // fDaclPresent flag   
										acl, 
										FALSE))   // not a default DACL 
		{
			// Initialize a security attributes structure.
			CSecurityAttributes sa(sd, FALSE);

			// Use the security attributes to set the security descriptor 
			// when you create a key.
			// Use the security attributes to set the security descriptor 
			// when you create a key.
			DWORD	dwDisposition;
			HKEY	hKey;
			lResult = ::RegCreateKeyEx(hKeyParent,lpszKeyName, 0, "", 0, 
									  KEY_ALL_ACCESS, sa, &hKey, &dwDisposition); 
			::RegCloseKey(hKey);
		}
		else
			lResult = -1;
	}

	return lResult;
}

