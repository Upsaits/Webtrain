#pragma once
#include "afxwin.h"


// CInfoDialog dialog
class CInfoDialog : public CDialog
{
	DECLARE_DYNAMIC(CInfoDialog)
	// Dialog Data
	enum { IDD = IDD_INFODIALOG };

public:
	enum RunType {Normal, Patch, Remote, OpenVPN, Client};
	enum LicenceType {InvalidLicense=-1, SingleLicense, MultiLicense, ServerLicense, DemoLicense, ClientLicense};
	enum LicenceRestriction {Unlimited=0,Time,UserCount};

protected:
	CTCDongleHandler	m_dongleHnd;
	CString				m_strInstallDir;
	RunType				m_installType;
	CString				m_patchName;
	CString				m_strLicFilename;
	long				m_iDongleId;

public:
	CInfoDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~CInfoDialog();

	LicenceType  GetUseType(LicenceRestriction &eLicRestrict,long &cnt);
	CString		 GetSetupDir() {return m_strInstallDir;};

protected:
	void StartExplorerInst();
	void StartInstallation(bool bShowDialog=true);
	void StartIISInst();
	void StartRemoteInst();
	void StartTrainConcept(const CString &strStartPath);

	CString FindSetupDir();
	CString FindTrainConceptDir();
	void	EncodeDate(const CString &str,CString &sOut);
	void	SetRegKeySecurity(CRegistry &reg);
	void	CheckURIScheme();

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CStatic m_staText;
	virtual BOOL OnInitDialog();

	void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnClose();
};
