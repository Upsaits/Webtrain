#if !defined(AFX_STDAFX_H__50F4070C_67D9_440C_87F0_3E74F1ADD3D5__INCLUDED_)
#define AFX_STDAFX_H__50F4070C_67D9_440C_87F0_3E74F1ADD3D5__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

// stdafx.h : Include-Datei für Standard-System-Include-Dateien,
//      oder häufig verwendete, projektspezifische Include-Dateien,
//      die nur in unregelmäßigen Abständen geändert werden.

#define VC_EXTRALEAN		// Selten benutzte Teile der Windows-Header nicht einbinden

#include <afxctl.h>         // MFC-Unterstützung für ActiveX-Steuerelemente
#include <afxext.h>         // MFC-Erweiterungen
#include <afxdtctl.h>		// MFC-Unterstützung für allgemeine Steuerelemente des Internet Explorers 4
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>			// MFC-Unterstützung für allgemeine Windows-Steuerelemente
#endif // _AFX_NO_AFXCMN_SUPPORT

// Nachstehende zwei Include-Anweisungen löschen,  falls die MFC nicht verwendet werden sollen
//  Datenbankklassen
//#include <afxdb.h>			// MFC-Datenbankklassen
//#include <afxdao.h>			// MFC DAO-Datenbankklassen
//#include <afxtempl.h>

#include <stdlib.h>
#include <fstream>
#include <istream>
#include <iostream>
#include <fcntl.h>
#include <share.h>
#include <sys/types.h>
#include <sys/stat.h>

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.
#endif // !defined(AFX_STDAFX_H__50F4070C_67D9_440C_87F0_3E74F1ADD3D5__INCLUDED_)
