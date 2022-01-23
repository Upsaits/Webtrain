#include "afxwin.h"
#pragma once

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CInstPatchDlg 

class CInstPatchDlg : public CDialog
{
protected:
	CString m_patchName;
	CString m_strSetupDir;
	CString m_strAppDataDir;


// Konstruktion
public:
	CInstPatchDlg(CWnd* pParent = NULL);   // Standardkonstruktor
	CInstPatchDlg(const CString &patchName,const CString &strSetupDir,CWnd* pParent = NULL);   // Konstruktor mit Patchnamen
// Dialogfelddaten
	//{{AFX_DATA(CInstPatchDlg)
	enum { IDD = IDD_INSTPATCH_DIALOG };
	CStatic	m_text;
	//}}AFX_DATA


// Überschreibungen
	// Vom Klassen-Assistenten generierte virtuelle Funktionsüberschreibungen
	//{{AFX_VIRTUAL(CInstPatchDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung
	//}}AFX_VIRTUAL

// Implementierung
protected:

	// Generierte Nachrichtenzuordnungsfunktionen
	//{{AFX_MSG(CInstPatchDlg)
	virtual BOOL OnInitDialog();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	CEdit m_edtProgFolder;
	CListBox m_lstOutput;
	CEdit m_edtUseType;
	bool m_bIsServer;
	afx_msg void OnTimer(UINT_PTR nIDEvent);

private:
	bool bKillRunningInstance();
	void vAdjustProcessPrivileges() const;
	HWND hWndGetRunningMainWnd();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

