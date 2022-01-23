// InstPatchDlg.cpp: Implementierungsdatei
//

#include "stdafx.h"
#include "install.h"
#include "InstPatchDlg.h"
#include "FileManager.h"
#include <Psapi.h>
#include <ShlObj.h>

static const TCHAR kAPPLICATION_NAME1[] = _T("WebTrain");
static const TCHAR kAPPLICATION_NAME2[] = _T("WebTrain :");
static const TCHAR kINSTALLERININAME[]  = _T("Installer.ini");

static TCHAR kPROCESSNAME[] = _T("TrainConcept.exe");
static HWND g_hRunningWnd;
static HWND g_thisWnd;


/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CInstPatchDlg 
CInstPatchDlg::CInstPatchDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CInstPatchDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CInstPatchDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Elementinitialisierung ein
	//}}AFX_DATA_INIT
}

CInstPatchDlg::CInstPatchDlg(const CString &patchName,const CString &strSetupDir,CWnd* pParent /*=NULL*/)
	: CDialog(CInstPatchDlg::IDD, pParent),m_bIsServer(false)
{
	//{{AFX_DATA_INIT(CInstPatchDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Elementinitialisierung ein
	//}}AFX_DATA_INIT
	m_patchName = patchName;
	m_strSetupDir = strSetupDir;

	TCHAR szPath[MAX_PATH];
	if (SUCCEEDED(SHGetFolderPath(NULL,CSIDL_COMMON_APPDATA,NULL,0,szPath)))
	{
		PathAppend(szPath,_T("\\SoftObject\\Webtrain"));
		m_strAppDataDir = CString(szPath);
		//AfxMessageBox(m_strAppDataDir);
	}

	CString sInstallType;
	UINT dwRet = GetPrivateProfileInt(_T("System"), _T("UseType"),0,strSetupDir +"\\trainconcept.ini");
	if (dwRet>0)
		m_bIsServer = (dwRet==2);
}

void CInstPatchDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CInstPatchDlg)
	DDX_Control(pDX, IDC_STA_TEXT, m_text);
	DDX_Control(pDX, IDC_LST_OUTPUT, m_lstOutput);
	DDX_Control(pDX, IDC_EDIT1, m_edtProgFolder);
	DDX_Control(pDX, IDC_EDIT2, m_edtUseType);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CInstPatchDlg, CDialog)
	//{{AFX_MSG_MAP(CInstPatchDlg)
	//}}AFX_MSG_MAP
	ON_WM_TIMER()
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// Behandlungsroutinen für Nachrichten CInstPatchDlg 

BOOL CInstPatchDlg::OnInitDialog() 
{
	CDialog::OnInitDialog();
	
	CString sText1,sText2;
	m_text.GetWindowText(sText1);
	
	sText2.Format(sText1,m_patchName);
	m_text.SetWindowText(sText2);

	m_lstOutput.ShowWindow(SW_HIDE);

	m_edtProgFolder.SetWindowText(m_strSetupDir);
	m_edtUseType.SetWindowText(m_bIsServer ? _T("Server"):_T("Client"));

	g_thisWnd = GetSafeHwnd();

	GetDlgItem(IDOK)->EnableWindow(FALSE);

	SetTimer(1,1000,NULL);

	return TRUE;  // return TRUE unless you set the focus to a control
	              // EXCEPTION: OCX-Eigenschaftenseiten sollten FALSE zurückgeben
}


void CInstPatchDlg::OnTimer(UINT_PTR nIDEvent)
{
	CFileManager fm;
	CString strOut;

	if (nIDEvent==1)
	{
		KillTimer(1);
		m_lstOutput.ShowWindow(SW_SHOW);
		strOut.Format(_T("close WebTrain.. "),m_strSetupDir);
		m_lstOutput.AddString(strOut);
		SetTimer(2,2000,NULL);
	}
	else if (nIDEvent==2)
	{
		KillTimer(2);
		vAdjustProcessPrivileges(); 
		bKillRunningInstance();
		SetTimer(3,2000,NULL);
	}
	else if (nIDEvent==3)
	{
		KillTimer(3);
		strOut.Format(_T("copy content-files to %s "),m_strSetupDir);
		m_lstOutput.AddString(strOut);
		SetTimer(4,2000,NULL);
	}
	else if (nIDEvent==4)
	{
		KillTimer(4);
		strOut.Format(_T("copy program-files to %s "),m_strSetupDir);
		m_lstOutput.AddString(strOut);
		if (m_bIsServer)
		{
			//AfxMessageBox(m_strAppDataDir+"\\Contents\\Content\\");
			fm.CopyAllFiles(GETAPPPTR->GetModulePath()+"Content\\*.*",m_strAppDataDir+"\\Contents\\Content\\");
		}
		SetTimer(5,2000,NULL);
	}
	else if (nIDEvent==5)
	{
		KillTimer(5);
		fm.CopyAllFiles(GETAPPPTR->GetModulePath()+"Exe\\*.*",m_strSetupDir+"\\");
		strOut.Format(_T("copy library-files to %s "),m_strSetupDir);
		m_lstOutput.AddString(strOut);
		SetTimer(6,2000,NULL);
	}
	else if (nIDEvent==6)
	{
		KillTimer(6);
		if (m_bIsServer)
		{
			fm.CopyAllFiles(GETAPPPTR->GetModulePath()+"Libraries\\*.*",m_strAppDataDir+"\\Contents\\Libraries\\");
		}
		strOut.Format(_T("all files successfully copied! "));
		m_lstOutput.AddString(strOut);
		GetDlgItem(IDOK)->EnableWindow(TRUE);
	}

	CDialog::OnTimer(nIDEvent);
}


void CInstPatchDlg::vAdjustProcessPrivileges() const
{ 
	HANDLE hToken = NULL;
	OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken);

	LUID luid;
	LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &luid);

	TOKEN_PRIVILEGES tkp;
	tkp.PrivilegeCount = 1;
	tkp.Privileges[0].Luid = luid;
	tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
	AdjustTokenPrivileges(hToken, FALSE, &tkp, sizeof(tkp), NULL, NULL);

	CloseHandle(hToken);
}

bool CInstPatchDlg::bKillRunningInstance()
{
	HWND  pWndPrev=NULL;
	DWORD dwPrevProcessId=0;

	// Determine if another window with our class name exists...
	pWndPrev = hWndGetRunningMainWnd();
	if (pWndPrev != NULL)
	{
		// Try to close it "softly" with WM_CLOSE (like Alt+F4)
		::PostMessage(pWndPrev, WM_CLOSE, 0, 0L);
	}
	return true;
}


BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lParam)
{
	char buf[256];

	if (hwnd!=g_thisWnd && ::GetWindowText(hwnd,buf,256)>0)
	{
		CString strWndName(buf);
		if (strWndName.Find(kAPPLICATION_NAME1)>=0 ||
			strWndName.Find(kAPPLICATION_NAME2)>=0)
		{
			g_hRunningWnd=hwnd;
			return FALSE;
		}
	}
	return TRUE;
}


HWND CInstPatchDlg::hWndGetRunningMainWnd()
{
	g_hRunningWnd = NULL;
	::EnumWindows(EnumWindowsProc,0L);
	return g_hRunningWnd;
}


