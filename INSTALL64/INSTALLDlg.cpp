// INSTALLDlg.cpp : Implementierungsdatei
//

#include "stdafx.h"
#include "INSTALL.h"
#include "tcdonglehandler.h"
#include "registry.h"
#include "InfoDialog.h"
#include "INSTALLDlg.h"
#include ".\installdlg.h"

/////////////////////////////////////////////////////////////////////////////
// CINSTALLDlg Dialogfeld

CINSTALLDlg::CINSTALLDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CINSTALLDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_tLicense = CInfoDialog::InvalidLicense;
	m_tOldLicense  = CInfoDialog::InvalidLicense;
	m_lLicCnt=0L;
}

void CINSTALLDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_RAD_SL, m_btnRad_SL);
	DDX_Control(pDX, IDC_RAD_ML, m_btnRad_ML);
	DDX_Control(pDX, IDC_RAD_SRVL, m_btnRad_SRVL);
	DDX_Control(pDX, IDC_RAD_CLIENT, m_btnRad_Client);
	DDX_Control(pDX, IDC_GRP_DONGLE_PRESENT, m_grp_DoPresent);
	DDX_Control(pDX, IDC_GRP_DONGLE_NOTPRESENT, m_grp_DoNotPresent);
}


BEGIN_MESSAGE_MAP(CINSTALLDlg, CDialog)
	//{{AFX_MSG_MAP(CINSTALLDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BUTTON1, OnBnClickedOk)
	ON_BN_CLICKED(IDC_BUTTON2, OnBnClickedCancel)
	ON_WM_TIMER()
	ON_CONTROL_RANGE(BN_CLICKED,IDC_RAD_CLIENT,IDC_RAD_DEMO,&CINSTALLDlg::OnBnClickedRadClientDemo)
	ON_CONTROL_RANGE(BN_CLICKED,IDC_RAD_SL,IDC_RAD_SRVL,&CINSTALLDlg::OnBnClickedRadLicenses)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CINSTALLDlg Nachrichten-Handler

BOOL CINSTALLDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	SetIcon(m_hIcon, TRUE);			// Großes Symbol verwenden
	SetIcon(m_hIcon, FALSE);		// Kleines Symbol verwenden

	CInfoDialog *pInfoDlg= (CInfoDialog *) GetParent();
	long cnt=0;
	CInfoDialog::LicenceRestriction eLicRestrict=CInfoDialog::Unlimited;
	CInfoDialog::LicenceType ver=pInfoDlg->GetUseType(eLicRestrict,cnt);
	SetUseType(ver,cnt);

	//AfxMessageBox(_T("CINSTALLDlg::OnInitDialog"));
	return TRUE;  // Geben Sie TRUE zurück, außer ein Steuerelement soll den Fokus erhalten
}


void CINSTALLDlg::SetUseType(CInfoDialog::LicenceType licType,long cnt)
{
	int iChecked=0;
	switch(licType)
	{
	case CInfoDialog::SingleLicense:
	case CInfoDialog::MultiLicense:
	case CInfoDialog::ServerLicense:
			m_grp_DoPresent.EnableWindow(TRUE);
			m_btnRad_SL.EnableWindow(licType==CInfoDialog::SingleLicense);
			m_btnRad_ML.EnableWindow(licType==CInfoDialog::MultiLicense);
			m_btnRad_SRVL.EnableWindow(licType==CInfoDialog::ServerLicense);
			CheckRadioButton(IDC_RAD_SL,IDC_RAD_SRVL,IDC_RAD_SL+static_cast<int>(licType));
			m_grp_DoNotPresent.EnableWindow(TRUE);
			m_btnRad_Client.EnableWindow(TRUE);
			CheckRadioButton(IDC_RAD_CLIENT,IDC_RAD_DEMO,0);
			if (licType==CInfoDialog::MultiLicense)
			{
				CString strBuf;
				strBuf.Format("Multi License(%d)",cnt);
				m_btnRad_ML.SetWindowText(strBuf);
			}

			break;
	case CInfoDialog::DemoLicense:
			m_grp_DoPresent.EnableWindow(FALSE);
			m_btnRad_SL.EnableWindow(FALSE);
			m_btnRad_ML.EnableWindow(FALSE);
			m_btnRad_ML.SetWindowText("Multi License");
			m_btnRad_SRVL.EnableWindow(FALSE);
			CheckRadioButton(IDC_RAD_SL,IDC_RAD_SRVL,0);
			m_grp_DoNotPresent.EnableWindow(TRUE);
			m_btnRad_Client.EnableWindow(TRUE);
			iChecked=GetCheckedRadioButton(IDC_RAD_CLIENT,IDC_RAD_DEMO);
			if (iChecked!=IDC_RAD_CLIENT && iChecked!=IDC_RAD_DEMO)
				CheckRadioButton(IDC_RAD_CLIENT,IDC_RAD_DEMO,IDC_RAD_DEMO);
			break;
	default:
		m_grp_DoPresent.EnableWindow(FALSE);
		m_btnRad_SL.EnableWindow(FALSE);
		m_btnRad_ML.EnableWindow(FALSE);
		m_btnRad_ML.SetWindowText("Multi License");
		m_btnRad_SRVL.EnableWindow(FALSE);
		CheckRadioButton(IDC_RAD_SL,IDC_RAD_SRVL,0);
		m_grp_DoNotPresent.EnableWindow(TRUE);
		m_btnRad_Client.EnableWindow(TRUE);
		CheckRadioButton(IDC_RAD_CLIENT,IDC_RAD_DEMO,IDC_RAD_DEMO);
		break;
	}

	m_tLicense = licType;
	m_tOldLicense = m_tLicense;
	m_lLicCnt = cnt;
}


// Wollen Sie Ihrem Dialogfeld eine Schaltfläche "Minimieren" hinzufügen, benötigen Sie 
//  den nachstehenden Code, um das Symbol zu zeichnen. Für MFC-Anwendungen, die das 
//  Dokument/Ansicht-Modell verwenden, wird dies automatisch für Sie erledigt.
void CINSTALLDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // Gerätekontext für Zeichnen

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Symbol in Client-Rechteck zentrieren
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Symbol zeichnen
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

HCURSOR CINSTALLDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}


void CINSTALLDlg::OnBnClickedOk()
{
	if (m_tLicense == CInfoDialog::DemoLicense)
	{
		m_tLicense = static_cast<CInfoDialog::LicenceType>(GetCheckedRadioButton(IDC_RAD_CLIENT,IDC_RAD_DEMO)==IDC_RAD_CLIENT ? CInfoDialog::Client : CInfoDialog::DemoLicense);
	}
	EndDialog(IDOK);
}

void CINSTALLDlg::OnBnClickedCancel()
{
	EndDialog(IDCANCEL);
}

void CINSTALLDlg::OnTimer(UINT_PTR nIDEvent)
{
	if (nIDEvent==1 || nIDEvent==2)
	{
	}

	CDialog::OnTimer(nIDEvent);
}


void CINSTALLDlg::OnBnClickedRadClientDemo(UINT iCtrlId)
{
	if (m_tLicense!=CInfoDialog::DemoLicense)
	{
		m_tOldLicense = m_tLicense;
		switch(m_tLicense)
		{
		case CInfoDialog::SingleLicense: m_btnRad_SL.SetCheck(BST_UNCHECKED);break;
		case CInfoDialog::MultiLicense: m_btnRad_ML.SetCheck(BST_UNCHECKED);break;
		case CInfoDialog::ServerLicense: m_btnRad_SRVL.SetCheck(BST_UNCHECKED);break;
		}
		m_tLicense = CInfoDialog::DemoLicense;
	}
}


void CINSTALLDlg::OnBnClickedRadLicenses(UINT iCtrlId)
{
	m_tLicense = m_tOldLicense;
	m_btnRad_Client.SetCheck(BST_UNCHECKED);
}
