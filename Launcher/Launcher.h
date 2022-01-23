// Launcher.h : Hauptheaderdatei für die Launcher-Anwendung
//


// CLauncherApp:
// Siehe Launcher.cpp für die Implementierung dieser Klasse
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

// Überschreibungen
	public:
	virtual BOOL InitInstance();

// Implementierung
	LPCTSTR RegisterWndClass(UINT nClassStyle,HCURSOR hCursor, HBRUSH hbrBackground, HICON hIcon=NULL);
	CString	GetModulePath() const;

	DECLARE_MESSAGE_MAP()
};

//extern CLauncherApp theApp;

#define GETAPP	((CLauncherApp *) AfxGetApp())
