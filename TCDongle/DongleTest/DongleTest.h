// DongleTest.h : Haupt-Header-Datei f�r die Anwendung DONGLETEST
//

#if !defined(AFX_DONGLETEST_H__29CC46F0_E2BF_4531_B5CA_A0A785876E4D__INCLUDED_)
#define AFX_DONGLETEST_H__29CC46F0_E2BF_4531_B5CA_A0A785876E4D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// Hauptsymbole

/////////////////////////////////////////////////////////////////////////////
// CDongleTestApp:
// Siehe DongleTest.cpp f�r die Implementierung dieser Klasse
//

class CDongleTestApp : public CWinApp
{
public:
	CTCDongleHandler	m_dongleHnd;

public:
	CDongleTestApp();

// �berladungen
	// Vom Klassenassistenten generierte �berladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CDongleTestApp)
	public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	//}}AFX_VIRTUAL

// Implementierung

	//{{AFX_MSG(CDongleTestApp)
		// HINWEIS - An dieser Stelle werden Member-Funktionen vom Klassen-Assistenten eingef�gt und entfernt.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VER�NDERN!
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#define	GETAPPPTR	((CDongleTestApp *) AfxGetApp())

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ f�gt unmittelbar vor der vorhergehenden Zeile zus�tzliche Deklarationen ein.

#endif // !defined(AFX_DONGLETEST_H__29CC46F0_E2BF_4531_B5CA_A0A785876E4D__INCLUDED_)
