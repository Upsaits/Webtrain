// INSTALL.h : Haupt-Header-Datei f�r die Anwendung INSTALL
//
#pragma once

#include "resource.h"		// Hauptsymbole

/////////////////////////////////////////////////////////////////////////////
// CINSTALLApp:
// Siehe INSTALL.cpp f�r die Implementierung dieser Klasse
//

class CINSTALLApp : public CWinApp
{
public:
	CINSTALLApp();

public:
	CString	GetFileName();
	CString	GetModulePath(bool bIncludedBackslash=true);

// �berladungen
	// Vom Klassenassistenten generierte �berladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CINSTALLApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementierung

	//{{AFX_MSG(CINSTALLApp)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#define		GETAPPPTR		((CINSTALLApp *) AfxGetApp())

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ f�gt unmittelbar vor der vorhergehenden Zeile zus�tzliche Deklarationen ein.

