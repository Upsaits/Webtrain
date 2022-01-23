// INSTALL.h : Haupt-Header-Datei für die Anwendung INSTALL
//
#pragma once

#include "resource.h"		// Hauptsymbole

/////////////////////////////////////////////////////////////////////////////
// CINSTALLApp:
// Siehe INSTALL.cpp für die Implementierung dieser Klasse
//

class CINSTALLApp : public CWinApp
{
public:
	CINSTALLApp();

public:
	CString	GetFileName();
	CString	GetModulePath(bool bIncludedBackslash=true);

// Überladungen
	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
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
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

