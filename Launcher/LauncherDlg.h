// LauncherDlg.h : Headerdatei
//

#pragma once


// CLauncherDlg Dialogfeld
class CLauncherDlg : public CDialog
{
// Konstruktion
public:
	CLauncherDlg(CWnd* pParent = NULL);	// Standardkonstruktor

// Dialogfelddaten
	enum { IDD = IDD_LAUNCHER_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV-Unterst�tzung


// Implementierung
protected:
	HICON m_hIcon;

	// Generierte Funktionen f�r die Meldungstabellen
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
};
