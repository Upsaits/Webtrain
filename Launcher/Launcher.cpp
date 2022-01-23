#include "stdafx.h"

// CLauncherApp

BEGIN_MESSAGE_MAP(CLauncherApp, CWinApp)
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()


// CLauncherApp-Erstellung

CLauncherApp::CLauncherApp()
{
	m_wndClassName = _T("");
	m_bmpFileName=_T("");
	m_applicationName=_T("");
	m_installationPath=_T("");
}


// Das einzige CLauncherApp-Objekt

CLauncherApp theApp;


// CLauncherApp Initialisierung

BOOL CLauncherApp::InitInstance()
{
	// InitCommonControls() ist für Windows XP erforderlich, wenn ein Anwendungsmanifest
	// die Verwendung von ComCtl32.dll Version 6 oder höher zum Aktivieren
	// von visuellen Stilen angibt. Ansonsten treten beim Erstellen von Fenstern Fehler auf.
	InitCommonControls();

	CWinApp::InitInstance();

	AfxEnableControlContainer();


	const CString strSORegRoot(_T("SOFTWARE\\Wow6432Node"));
	const CString strSORegKey(strSORegRoot+_T("\\SoftObject"));
	CRegistry reg(strSORegKey);
	if (!reg.Get("\\TrainConcept","InstallationPath",m_installationPath))
	{
		AfxMessageBox("TrainConcept not installed!");
		return FALSE;
	}
	
	m_profHnd.SetINIFileName(m_installationPath+"\\launcher.ini");

	m_wndClassName = m_profHnd.GetProfileString("SYSTEM","WndClassName","LauncherWndClass");
	m_bmpFileName = m_profHnd.GetProfileString("SYSTEM","BitmapFileName","wtsplash.bmp");
	m_applicationName = m_profHnd.GetProfileString("SYSTEM","ApplicationName","trainconcept.exe");

	m_bmpFileName = m_installationPath + _T("\\")+m_bmpFileName;
	
	LPCTSTR lpClsName = RegisterWndClass(CS_DBLCLKS|CS_HREDRAW|CS_VREDRAW,::LoadCursor (NULL, IDC_ARROW),
											(HBRUSH)(COLOR_WINDOW+1)); 
	if (!m_launcherWnd.CreateEx(WS_EX_TOPMOST|WS_EX_CLIENTEDGE,lpClsName,"LauncherWnd",WS_VISIBLE|WS_POPUP|WS_DLGFRAME,CRect(0,0,0,0),NULL,NULL))
		return FALSE;

	m_pMainWnd = &m_launcherWnd;

	return TRUE;
}


LPCTSTR CLauncherApp::RegisterWndClass(UINT nClassStyle,HCURSOR hCursor, HBRUSH hbrBackground, HICON hIcon)
{
	// Returns a temporary string name for the class
	//  Save in a CString if you want to use it for a long time
	LPTSTR lpszName = (LPSTR)(LPCTSTR)m_wndClassName;

	// generate a synthetic name for this class
	HINSTANCE hInst = AfxGetInstanceHandle();

	// see if the class already exists
	WNDCLASS wndcls;
	if (::GetClassInfo(hInst, lpszName, &wndcls))
	{
		// already registered, assert everything is good
		ASSERT(wndcls.style == nClassStyle);

		// NOTE: We have to trust that the hIcon, hbrBackground, and the
		//  hCursor are semantically the same, because sometimes Windows does
		//  some internal translation or copying of those handles before
		//  storing them in the internal WNDCLASS retrieved by GetClassInfo.
		return lpszName;
	}

	// otherwise we need to register a new class
	wndcls.style = nClassStyle;
	wndcls.lpfnWndProc = DefWindowProc;
	wndcls.cbClsExtra = wndcls.cbWndExtra = 0;
	wndcls.hInstance = hInst;
	wndcls.hIcon = hIcon;
	wndcls.hCursor = hCursor;
	wndcls.hbrBackground = hbrBackground;
	wndcls.lpszMenuName = NULL;
	wndcls.lpszClassName = lpszName;
	if (!AfxRegisterClass(&wndcls))
		AfxThrowResourceException();

	// return thread-local pointer
	return lpszName;
}

CString	CLauncherApp::GetModulePath() const
{
	char	ppath[1024];
	CString	sBuf;
	int		found;

	// Projektpfad ermitteln
	::GetModuleFileName(m_hInstance, ppath, sizeof(ppath));
	sBuf = ppath; 
	found = sBuf.ReverseFind('\\');

	return (sBuf.Left(found+1));
}
