#include "stdafx.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/* **************************** Konstanten ******************************* */
const char c_aEncodes[] = {1,6,3,9,7,8,10,2,11,4};


/* ************************* CProfileHandler ***************************** */
/////////////////////////////////////////////////////////////////////////////
// KONSTRUKTOR
/////////////////////////////////////////////////////////////////////////////
/**
* 1.Konstruktor
* @param		exePath		 Pfad der Anwendung
*/
CProfileHandler::CProfileHandler(const CString &iniFileName)
{
	m_iniFileName = iniFileName;
}

/**
* Key als String aus der INI-datei holen
* @param	section		Sektion-Bezeichnung
* @param	key			Key-Bezeichnung
* @param	defkey		Default-Key-Eintrag
* @return				eingetragener Wert
*/
CString	CProfileHandler::GetProfileString(CString section,CString key,
										  CString defKey)
{
	char	buf[255];

	::GetPrivateProfileString(section,key,defKey,buf,255,m_iniFileName);

	return CString(buf);
}


/**
* Key als Integer aus der INI-datei holen
* @param	section		Sektion-Bezeichnung
* @param	key			Key-Bezeichnung
* @param	defkey		Default-Key-Eintrag
* @return				eingetragener Wert
*/
int	CProfileHandler::GetProfileInt(CString section,CString key,
								   int defKey)
{
	return ::GetPrivateProfileInt(section,key,defKey,m_iniFileName);
}

/**
* Key als Double aus der INI-datei holen
* @param	section		Sektion-Bezeichnung
* @param	key			Key-Bezeichnung
* @param	defVal		Default-Wert-Eintrag
* @return				eingetragener Wert
*/
double	CProfileHandler::GetProfileFloat(CString section,CString key,
										  double defVal)
{
	char	buf[255];
    CString defStr;

    ::GetPrivateProfileString(section,key,defStr,buf,255,m_iniFileName);


    if(strlen(buf))
    		return atof(buf);
    else 	return defVal;
}


/**
* Alle Keys aus einer Sektion holen
* @param	section		Sektion-Bezeichnung
* @param	aSystems	Referenz auf CStringArray für die Keys
* @return				Anzahl der übernommenen Keys
*/
UINT CProfileHandler::GetProfileKeys(CString section,CStringArray &aSystems)
{
	char	buf[10240];
	char	*ptrBuf;
	int		idx=0;
	int		len;

	// Einlesen der gesamten Sektion
	::GetPrivateProfileString(section,NULL,"",buf,sizeof(buf),m_iniFileName);

	// Durchsuche den gesamten String
	ptrBuf = &buf[idx];
	while (len=(int)strlen(ptrBuf))	// Eintrag vorhanden ?
	{
		aSystems.Add(CString(ptrBuf)); // In Array eintragen
		idx+=len+1;
		ptrBuf = &buf[idx];
	}
	return (UINT) aSystems.GetSize();
}


/**
* Key als String in die INI-Datei schreiben
* @param	section		Sektion-Bezeichnung
* @param	key			Key-Bezeichnung
* @param	value		Default-Key-Eintrag
* @return				Wert eingetragen?
*/
BOOL	CProfileHandler::WriteProfileString(CString section,CString key,
											CString value)
{
	return ::WritePrivateProfileString(section,key,value,m_iniFileName);
}


/**
* Key als Integer in die INI-Datei schreiben
* @param	section		Sektion-Bezeichnung
* @param	key			Key-Bezeichnung
* @param	value		Default-Key-Eintrag
* @return				Wert eingetragen?
*/
BOOL	CProfileHandler::WriteProfileInt(CString section,CString key,int value)
{
	CString	str;

	str.Format("%d",value);
	return ::WritePrivateProfileString(section,key,str,m_iniFileName);
}


/**
* Key als Double in die INI-Datei schreiben
* @param	section		Sektion-Bezeichnung
* @param	key			Key-Bezeichnung
* @param	value		Default-Key-Eintrag
* @return				Wert eingetragen?
*/
BOOL	CProfileHandler::WriteProfileFloat(CString section,CString key,
											double val, CString format/*=..*/)
{
   	BYTE buf[256];
    sprintf_s((char *)buf, 256, format, val);
	return ::WritePrivateProfileString(section,key,(char *)buf,m_iniFileName);
}



/**
* String in Punkt umwandeln.
* Wandelt einen String im Format "x,y" in CPoint-Klasse um
* @param	str			übergebener String
* @return				entsprechender CPoint-Wert
*/
CPoint CProfileHandler::StringToPoint(CString str)
{
	CString	xStr("");
    CString yStr("");
    int	found = str.Find(',');

    if (found > 0)
    {
    	xStr = str.Left(found);
        yStr = str.Mid(found+1);
    }
	return CPoint(atoi(xStr),atoi(yStr));
}


/**
* String in Size umwandeln.
* Wandelt einen String im Format "x,y" in CSize-Klasse um
* @param	str			übergebener String
* @return				entsprechender CSize-Wert
*/
CSize CProfileHandler::StringToSize(CString str)
{
	return CSize(StringToPoint(str));
}

/**
* Point in String umwandeln.
* Wandelt einen CPoint in einen String um
* @param	point		übergebener CPoint
* @return				entsprechender CString-Wert
*/
CString CProfileHandler::PointToString(CPoint point)
{
	CString	buf;
    buf.Format("%d,%d",point.x,point.y);
	return buf;
}


/**
* Size in String umwandeln.
* Wandelt einen CSize in einen String um
* @param	size		übergebener CSize
* @return				entsprechender CString-Wert
*/
CString CProfileHandler::SizeToString(CSize size)
{
	return PointToString(CPoint(size));
}



/**
* String in Rechteck umwandeln.
* Wandelt einen String im Format "x,y,b,h" in einen String um
* @param	str			übergebener String
* @param	rect		Ergebnis als Referenz auf CRect-Objekt
*/
void CProfileHandler::StringToRect(CString str,CRect &rect)
{
	int x,y,w,h;

	CString	xStr("");
    CString yStr("");
    CString wStr("");
	CString hStr("");

    int	found = str.Find(',');
    if (found > 0)
    {
    	xStr = str.Left(found);
        yStr = str.Mid(found+1);

        found = yStr.Find(',');
        if (found > 0)
        {
            wStr = yStr.Mid(found+1);
        	yStr = yStr.Left(found);

			found = wStr.Find(',');

			if (found > 0)
			{
				hStr = wStr.Mid(found+1);
        		wStr = wStr.Left(found);
			}
        }
    }

    x = atoi((LPCTSTR)xStr);
	y = atoi((LPCTSTR)yStr);
    w = atoi((LPCTSTR)wStr);
    h = atoi((LPCTSTR)hStr);

	rect = CRect(CPoint(x,y),CSize(w,h));
}

/**
* Rechteck in String umwandeln.
* Wandelt ein Rechteck in einen String im Format "x,y,b,h" um
* @param	rect		übergebenes Rechteck
* @return				String als Ergebnis
*/
CString CProfileHandler::RectToString(CRect rect)
{
	CString	buf;
    buf.Format("%3d,%3d,%3d,%3d",rect.left,rect.top,rect.Width(),rect.Height());
	return buf;
}


/**
* Ist angegebener Pfad absolut, oder relativ.
* @param	path		übergebener Pfad
* @return				Pfad absolut oder relativ?
*/
BOOL CProfileHandler::IsAbsolutePath(CString path)
{
    #ifdef __BORLANDC__
    int  flags;

    flags=fnsplit(path,NULL,NULL,NULL,NULL);

    return (flags & DRIVE);

    #else
    char sDrive[_MAX_DRIVE];
    char sDir[_MAX_DIR];
    char sFName[_MAX_FNAME];
    char sExt[_MAX_EXT];

    _splitpath_s(path,sDrive,sDir,sFName,sExt);
	return (strlen(sDrive) ? true:false);
    #endif
}

/**
* Ist der angegebene String "ON"?
* @param	str			übergebener String
* @return				"ON"?
*/
BOOL CProfileHandler::IsOn(CString str)
{
    str.MakeUpper();
    if (str.GetLength())
    	return (str == PROF_SYS_ON); 
	return FALSE;
}

/**
* Kodierung eines Strings mit vordefiniertem ASCII-Schlüssel
* @param	str			übergebener String
* @return				Kodierter String
*/
CString CProfileHandler::Encode(CString str)
{
	int maxChars=min(10,str.GetLength());

	for(int i=0;i<maxChars;++i)
		str.SetAt(i,str.GetAt(i)+c_aEncodes[i]);
	return str;
}

/**
* Dekodierung eines Strings mit vordefiniertem ASCII-Schlüssel
* @param	str			übergebener String
* @return				dekodierter String
*/
CString CProfileHandler::Decode(CString str)
{
	int maxChars=min(10,str.GetLength());

	for(int i=0;i<maxChars;++i)
		str.SetAt(i,str.GetAt(i)-c_aEncodes[i]);
	return str;
}

/**
* Ist der übergebene String ein Port-Parameter?
* @param	str			übergebener String
* @return				Port-Parameter?
*/
BOOL CProfileHandler::IsSerialPort(CString str)
{
    str.MakeUpper();
    if (str.GetLength())
    	return ((str == PROF_SYS_COM1) ||
                (str == PROF_SYS_COM2) ||
				(str == PROF_SYS_COM3) ||
                (str == PROF_SYS_COM4));
	return FALSE;
}


/**
* Wandelt einen String in Portsettings für die serielle Schnittstelle
* @param	str			String im Format "baudrate,dataBits,stopBits,parity"
* @param	aSettings	Array aus Integer's als Ergebnis
* @param	count		Anzahl der Integer-Werte im Array
*/
void CProfileHandler::StringToPortSettings(CString str,int *aSettings,int count)
{
	int v1,v2,v3,v4;

	ASSERT(count == 4);				// Arraygröße MUSS 4 sein!

	CString	s1("");
    CString s2("");
    CString s3("");
	CString s4("");

    int	found = str.Find(',');
    if (found > 0)
    {
    	s1 = str.Left(found);
        s2 = str.Mid(found+1);

        found = s2.Find(',');
        if (found > 0)
        {
            s3 = s2.Mid(found+1);
        	s2 = s2.Left(found);

			found = s3.Find(',');

			if (found > 0)
			{
				s4 = s3.Mid(found+1);
        		s3 = s3.Left(found);
			}
        }
    }

    v1 = atoi((LPCTSTR)s1);
	v2 = atoi((LPCTSTR)s2);
    v3 = atoi((LPCTSTR)s3);

	if (s4.GetLength() == 1)
	{
		char ch = s4[0];
		v4 = (int) ch;
	}
	else
		v4 = 0;

	aSettings[0] = v1;
	aSettings[1] = v2;
	aSettings[2] = v3;
	aSettings[3] = v4;
}
