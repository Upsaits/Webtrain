#pragma once

// CLauncherWnd

class CLauncherWnd : public CWnd
{
	DECLARE_DYNAMIC(CLauncherWnd)
protected:
	HICON m_hIcon;
	CDibWnd m_dibWnd;

public:
	CLauncherWnd();
	virtual ~CLauncherWnd();

protected:
	DECLARE_MESSAGE_MAP()

public:	
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnTimer(UINT nIDEvent);
};


