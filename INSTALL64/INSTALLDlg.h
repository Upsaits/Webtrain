// INSTALLDlg.h : Header-Datei
//
#pragma once

#include "afxwin.h"

/////////////////////////////////////////////////////////////////////////////
// CINSTALLDlg Dialogfeld

class CINSTALLDlg : public CDialog
{
	enum { IDD = IDD_INSTALL_DIALOG };
	CInfoDialog::LicenceType m_tLicense;
	long m_lLicCnt;
	CInfoDialog::LicenceType m_tOldLicense;

// Konstruktion
public:
	CINSTALLDlg(CWnd* pParent = NULL);	// Standard-Konstruktor

	CInfoDialog::LicenceType GetLicence() {return m_tLicense;};
	long GetLicenceCnt() { return m_lLicCnt;};

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV-Unterstützung
	void SetUseType(CInfoDialog::LicenceType licType,long cnt);

// Implementierung
protected:
	HICON m_hIcon;

	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();


	DECLARE_MESSAGE_MAP()

public:
	CButton m_btnRad_SL;
	CButton m_btnRad_ML;
	CButton m_btnRad_SRVL;
	CButton m_btnRad_Client;

	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	CStatic m_grp_DoPresent;
	CStatic m_grp_DoNotPresent;
	afx_msg void OnBnClickedRadClientDemo(UINT iCtrlId);
	afx_msg void OnBnClickedRadLicenses(UINT iCtrlId);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

