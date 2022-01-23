#include "stdafx.h"


// CLauncherWnd

IMPLEMENT_DYNAMIC(CLauncherWnd, CWnd)
CLauncherWnd::CLauncherWnd()
{
	//m_hIcon = LoadIcon(AfxGetApp()->m_hInstance,MAKEINTRESOURCE(IDR_MAINFRAME));
}

CLauncherWnd::~CLauncherWnd()
{
}

BEGIN_MESSAGE_MAP(CLauncherWnd, CWnd)
	ON_WM_CREATE()
	ON_WM_SIZE()
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CLauncherWnd-Meldungshandler
int CLauncherWnd::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CWnd::OnCreate(lpCreateStruct) == -1)
		return -1;

	if (!m_dibWnd.Create(NULL,"",WS_CHILD|WS_VISIBLE,CRect(0,0,0,0),this,100))
		return -1;

	if (m_dibWnd.SetDib(GETAPP->m_bmpFileName))
		MoveWindow(0,0,m_dibWnd.GetWidth(),m_dibWnd.GetHeight(),TRUE);

	SetTimer(1,2000,NULL);
	SetTimer(2,60000,NULL);

	return 0;
}

void CLauncherWnd::OnSize(UINT nType, int cx, int cy)
{
	CWnd::OnSize(nType, cx, cy);

	CRect rClient;
	GetClientRect(&rClient);

	if (m_dibWnd.IsValid()>0)
		m_dibWnd.MoveWindow(rClient);

	CenterWindow();
}

void CLauncherWnd::OnTimer(UINT nIDEvent)
{
	if (nIDEvent==1)
	{
		KillTimer(1);

		STARTUPINFO si;
		PROCESS_INFORMATION pi;

		ZeroMemory( &si, sizeof(si) );
		si.cb = sizeof(si);
		ZeroMemory( &pi, sizeof(pi) );

		auto pApp=static_cast<CLauncherApp*>(AfxGetApp());
		CString instDir=pApp->m_installationPath;
		CString sParam = _T("");
		if (pApp->m_lpCmdLine[0] != _T('\0'))
			sParam = CString(pApp->m_lpCmdLine);
		
		//CString strDir;
		//strDir.Format("%s %s",GETAPP->m_applicationName,dir);
		//AfxMessageBox(strDir);
		CString strAppPath=const_cast<LPSTR>(static_cast<LPCTSTR>(instDir + _T("\\") + GETAPP->m_applicationName));
		CString strCmdLine=strAppPath+_T(" ")+sParam;
		
		if (CreateProcess(nullptr,
						  const_cast<LPSTR>(static_cast<LPCTSTR>(strCmdLine)),
						  nullptr,nullptr,FALSE,
						  CREATE_SUSPENDED|NORMAL_PRIORITY_CLASS,
						  nullptr,const_cast<LPSTR>(static_cast<LPCTSTR>(instDir)),&si,&pi))
			ResumeThread(pi.hThread);
	} 
	else if (nIDEvent==2)
		PostMessage(WM_CLOSE);

	CWnd::OnTimer(nIDEvent);
}
