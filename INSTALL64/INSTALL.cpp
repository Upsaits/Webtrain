// INSTALL.cpp : Legt das Klassenverhalten für die Anwendung fest.
//

#include "stdafx.h"
#include "INSTALL.h"
#include "tcdonglehandler.h"
#include "registry.h"
#include "InfoDialog.h"
#include <winsock2.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CINSTALLApp

BEGIN_MESSAGE_MAP(CINSTALLApp, CWinApp)
	//{{AFX_MSG_MAP(CINSTALLApp)
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CINSTALLApp Konstruktion

CINSTALLApp::CINSTALLApp()
{
}

/////////////////////////////////////////////////////////////////////////////
// Das einzige CINSTALLApp-Objekt

CINSTALLApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CINSTALLApp Initialisierung

BOOL CINSTALLApp::InitInstance()
{
	WORD wVersionRequested;
	WSAData wsaData;
	int err;

	wVersionRequested = MAKEWORD( 1, 1 );
	err = WSAStartup(wVersionRequested, &wsaData);

	CInfoDialog dlg;
	m_pMainWnd = &dlg;
	dlg.DoModal();

	// Da das Dialogfeld geschlossen wurde, FALSE zurückliefern, so dass wir die
	//  Anwendung verlassen, anstatt das Nachrichtensystem der Anwendung zu starten.
	return FALSE;
}


CString	CINSTALLApp::GetFileName()
{
	char	ppath[1024];

	// Projektpfad ermitteln
	::GetModuleFileName(m_hInstance, ppath, sizeof(ppath));
	return CString(ppath);
}


CString	CINSTALLApp::GetModulePath(bool bIncludedBackslash/*=true*/)
{
	char	ppath[1024];
	CString	sBuf;
	int		found;

	// Projektpfad ermitteln
	::GetModuleFileName(m_hInstance, ppath, sizeof(ppath));
	sBuf = ppath; 
	found = sBuf.ReverseFind('\\');

	return (bIncludedBackslash ? sBuf.Left(found+1) : sBuf.Left(found));
}
