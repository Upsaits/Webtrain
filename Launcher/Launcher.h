// Launcher.h : Hauptheaderdatei f�r die Launcher-Anwendung
//


// CLauncherApp:
// Siehe Launcher.cpp f�r die Implementierung dieser Klasse
//

class CLauncherApp : public CWinApp
{
protected:
	CLauncherWnd	m_launcherWnd;
	CProfileHandler m_profHnd;

public:
	CString			m_wndClassName;
	CString			m_bmpFileName;
	CString			m_applicationName;
	CString			m_installationPath;

public:
	CLauncherApp();

// �berschreibungen
	public:
	virtual BOOL InitInstance();

// Implementierung
	LPCTSTR RegisterWndClass(UINT nClassStyle,HCURSOR hCursor, HBRUSH hbrBackground, HICON hIcon=NULL);
	CString	GetModulePath() const;

	DECLARE_MESSAGE_MAP()
};

//extern CLauncherApp theApp;

#define GETAPP	((CLauncherApp *) AfxGetApp())
