// DongleTestDlg.h : Header-Datei
//

#include "afxwin.h"
#if !defined(AFX_DONGLETESTDLG_H__6A014011_63ED_4B87_91FA_F3D9042DE0E1__INCLUDED_)
#define AFX_DONGLETESTDLG_H__6A014011_63ED_4B87_91FA_F3D9042DE0E1__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CDongleTestDlg Dialogfeld

class CDongleTestDlg : public CDialog
{
protected:
	long m_dongleID;

// Konstruktion
public:
	CDongleTestDlg(CWnd* pParent = NULL);	// Standard-Konstruktor

// Dialogfelddaten
	//{{AFX_DATA(CDongleTestDlg)
	enum { IDD = IDD_DONGLETEST_DIALOG };
	CButton	m_btnWriteLicFile;
	CButton	m_btnReadLicFile;
	CEdit	m_edtUpdateMinor;
	CEdit	m_edtUpdateMajor;
	CEdit	m_edtLicType;
	CEdit	m_edtLicTypeValue;
	CEdit	m_edtLicGrp;
	CEdit	m_edtDongleId;
	CEdit	m_edtRegDongleId;
	//}}AFX_DATA

	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CDongleTestDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV-Unterstützung
	//}}AFX_VIRTUAL

// Implementierung
protected:
	HICON m_hIcon;

	void ShowValues();
	void GetValues();


	// Generierte Message-Map-Funktionen
	//{{AFX_MSG(CDongleTestDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnReadDongle();
	afx_msg void OnWriteDongle();
	afx_msg void OnReadLicenceFile();
	afx_msg void OnWriteLicenceFile();
	afx_msg void OnSetDongleID();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
//	afx_msg void OnEnKillfocus_EdtDongleId();
	CEdit m_edt_RegTimeLimit;
private:
	void EncodeDate(const CString &str,CString &sOut);
	bool DecodeDate(CString str,COleDateTime &dt);
	void UpdateTimeLimit();
public:
	afx_msg void OnBnClickedBtnCleartimelimit();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // !defined(AFX_DONGLETESTDLG_H__6A014011_63ED_4B87_91FA_F3D9042DE0E1__INCLUDED_)
