// DongleTestDlg.cpp : Implementierungsdatei
//

#include "stdafx.h"
#include "..\tcdonglehandler.h"
#include "DongleTest.h"
#include "DongleTestDlg.h"
#include "..\..\INSTALL64\registry.h"
#include <vector>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg-Dialogfeld für Anwendungsbefehl "Info"

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialogfelddaten
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung
	//}}AFX_VIRTUAL

// Implementierung
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// Keine Nachrichten-Handler
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDongleTestDlg Dialogfeld

CDongleTestDlg::CDongleTestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDongleTestDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDongleTestDlg)
	//}}AFX_DATA_INIT
	// Beachten Sie, dass LoadIcon unter Win32 keinen nachfolgenden DestroyIcon-Aufruf benötigt
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_dongleID=0;
}

void CDongleTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDongleTestDlg)
	DDX_Control(pDX, IDC_BTN_WRITEFILE, m_btnWriteLicFile);
	DDX_Control(pDX, IDC_BTN_READFILE, m_btnReadLicFile);
	DDX_Control(pDX, IDC_EDT_UPDMIN, m_edtUpdateMinor);
	DDX_Control(pDX, IDC_EDT_UPDMAJ, m_edtUpdateMajor);
	DDX_Control(pDX, IDC_EDT_LICTYPE, m_edtLicType);
	DDX_Control(pDX, IDC_EDT_LICTYPVAL, m_edtLicTypeValue);
	DDX_Control(pDX, IDC_EDT_LICGRP, m_edtLicGrp);
	DDX_Control(pDX, IDC_EDT_DONGLEID, m_edtDongleId);
	DDX_Control(pDX, IDC_EDT_REGDONGLEID, m_edtRegDongleId);
	DDX_Control(pDX, IDC_EDT_REGTIMELIMIT, m_edt_RegTimeLimit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CDongleTestDlg, CDialog)
	//{{AFX_MSG_MAP(CDongleTestDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_TIMER()
	ON_BN_CLICKED(IDC_BTN_READFILE, OnReadLicenceFile)
	ON_BN_CLICKED(IDC_BTN_WRITEFILE, OnWriteLicenceFile)
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BTN_CLEARTIMELIMIT, &CDongleTestDlg::OnBnClickedBtnCleartimelimit)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDongleTestDlg Nachrichten-Handler

BOOL CDongleTestDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Hinzufügen des Menübefehls "Info..." zum Systemmenü.

	// IDM_ABOUTBOX muss sich im Bereich der Systembefehle befinden.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{	
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Symbol für dieses Dialogfeld festlegen. Wird automatisch erledigt
	//  wenn das Hauptfenster der Anwendung kein Dialogfeld ist
	SetIcon(m_hIcon, TRUE);			// Großes Symbol verwenden
	SetIcon(m_hIcon, FALSE);		// Kleines Symbol verwenden
	
	m_dongleID = 0;

	m_btnReadLicFile.EnableWindow(TRUE);
	m_btnWriteLicFile.EnableWindow(TRUE);

	UpdateTimeLimit();

	return TRUE;  // Geben Sie TRUE zurück, außer ein Steuerelement soll den Fokus erhalten
}

void CDongleTestDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// Wollen Sie Ihrem Dialogfeld eine Schaltfläche "Minimieren" hinzufügen, benötigen Sie 
//  den nachstehenden Code, um das Symbol zu zeichnen. Für MFC-Anwendungen, die das 
//  Dokument/Ansicht-Modell verwenden, wird dies automatisch für Sie erledigt.

void CDongleTestDlg::OnPaint() 
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

// Die Systemaufrufe fragen den Cursorform ab, die angezeigt werden soll, während der Benutzer
//  das zum Symbol verkleinerte Fenster mit der Maus zieht.
HCURSOR CDongleTestDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CDongleTestDlg::ShowValues()
{
	CString txt;
	short licGrp,licType;
	long licTypeVal;
	long updateMajor,updateMinor;

	GETAPPPTR->m_dongleHnd.GetLicence(&licGrp,&licType);
	GETAPPPTR->m_dongleHnd.GetLicenceTypeValue(&licTypeVal);
	GETAPPPTR->m_dongleHnd.GetUpdate(&updateMajor,&updateMinor);

	if (m_dongleID==0)
		m_edtDongleId.SetWindowText("---");
	else
	{
		txt.Format("%ld",m_dongleID);
		m_edtDongleId.SetWindowText(txt);
	}

	txt.Format("%d",licGrp);
	m_edtLicGrp.SetWindowText(txt);

	txt.Format("%d",licType);
	m_edtLicType.SetWindowText(txt);

	txt.Format("%d",licTypeVal);
	m_edtLicTypeValue.SetWindowText(txt);

	txt.Format("%d",updateMajor);
	m_edtUpdateMajor.SetWindowText(txt);

	txt.Format("%d",updateMinor);
	m_edtUpdateMinor.SetWindowText(txt);
}

void CDongleTestDlg::GetValues()
{
	short licGrp,licType;
	long dongleId,licTypeVal;
	long updateMajor,updateMinor;

	CString txt;
	m_edtDongleId.GetWindowText(txt);
	dongleId=atol(txt);

	m_edtLicGrp.GetWindowText(txt);
	licGrp=atoi(txt);

	m_edtLicType.GetWindowText(txt);
	licType=atoi(txt);

	m_edtLicTypeValue.GetWindowText(txt);
	licTypeVal=atol(txt);

	m_edtUpdateMajor.GetWindowText(txt);
	updateMajor=atol(txt);

	m_edtUpdateMinor.GetWindowText(txt);
	updateMinor=atol(txt);

	GETAPPPTR->m_dongleHnd.SetDongleID(dongleId);
	GETAPPPTR->m_dongleHnd.SetLicence(licGrp,licType);
	GETAPPPTR->m_dongleHnd.SetLicenceTypeValue(licTypeVal);
	GETAPPPTR->m_dongleHnd.SetUpdate(updateMajor,updateMinor);
}

void CDongleTestDlg::OnReadLicenceFile() 
{	
	CFileDialog	dlg(TRUE,"lic",NULL,OFN_HIDEREADONLY|OFN_OVERWRITEPROMPT|OFN_FILEMUSTEXIST,
					"LIC-Files (*.lic)|*.lic||",this);
	if (dlg.DoModal() == IDOK)
	{
		if (GETAPPPTR->m_dongleHnd.ReadLicenceFile(dlg.GetPathName(),&m_dongleID))
			ShowValues();
		else
			MessageBox("Dongle-File nicht vorhanden oder undgültig!");
	}
}

void CDongleTestDlg::OnWriteLicenceFile() 
{
	GetValues();
	CFileDialog	dlg(FALSE,"lic","licence",OFN_HIDEREADONLY|OFN_OVERWRITEPROMPT,
					"LIC-Files (*.lic)|*.lic||",this);
	if (dlg.DoModal() == IDOK)
	{
		if (!GETAPPPTR->m_dongleHnd.WriteLicenceFile(dlg.GetPathName()))
			MessageBox("Dongle-File undgültig!");
	}
}

void CDongleTestDlg::EncodeDate(const CString &str,CString &sOut)
{
	int  i;
	for(i=0;i<str.GetLength();++i)
	{
		BYTE b=str.GetAt(i) ^ ((i*13)^0x86)%255;
		CString sb;
		sb.Format("%d ",b);
		sOut+=sb;
	}
}

static std::vector<char *> splitString(char in[])
{
	std::vector<char *> parts;
	char seps[]   = " ,\t\n";
	char *next_token1 = NULL;
	char *token1 = NULL;
	token1 = strtok_s(in, seps, &next_token1);
	if (token1!=NULL)
		parts.push_back(token1);
	while ((token1 != NULL))
	{
		if (token1 != NULL)
		{
			token1 = strtok_s( NULL, seps, &next_token1);
			//printf( " %s\n", token1 );
			if (token1!=NULL)
				parts.push_back(token1);
		}
	}
	return parts;
}


bool CDongleTestDlg::DecodeDate(CString str,COleDateTime &dt)
{
	std::vector<char *> aTokens=splitString(str.GetBuffer());
	
	if (aTokens.size()!=8)
		return false;

	char sDate[9];
	memset(sDate,0x0,9);

	for(int i=0;i<(int)aTokens.size();++i)
	{
		int iVal=_ttoi(aTokens[i]);
		iVal ^= (((i*13) ^ 0x86) % 255);
		sDate[i]=(char)iVal;
	}

	CString strDate=CString(sDate);
	dt.SetDateTime(_ttoi(strDate.Mid(4,4)),_ttoi(strDate.Mid(2,2)),_ttoi(strDate.Mid(0,2)),0,0,0);

	/*
	char [] aChars = new Char[8];
	int  i;
	for(i=0;i<aTokens.Length;++i)
	{
		byte b=Convert.ToByte(aTokens[i]);
		b ^= (byte)(((i*13)^0x86)%255);
		aChars[i]=Convert.ToChar(b);
	}
	string sDate=new string(aChars);
	date=new DateTime(Int32.Parse(sDate.Substring(4,4)),Int32.Parse(sDate.Substring(2,2)),Int32.Parse(sDate.Substring(0,2)));
	*/
	return true;
}


void CDongleTestDlg::UpdateTimeLimit()
{
	m_edtRegDongleId.SetWindowText(_T(""));
	m_edt_RegTimeLimit.SetWindowText(_T(""));

	CString sTemp;
	CRegistry reg("SOFTWARE\\Microsoft\\Windows");
	if (reg.Get("\\STLInfo","STLVersionId",sTemp))
		m_edtRegDongleId.SetWindowText(sTemp);

	if (reg.Get("\\STLInfo","STLVersion",sTemp))
	{
		COleDateTime dt;
		//DecodeDate(_T("182 178 172 152 128 247 249 232"),dt);
		DecodeDate(sTemp,dt);
		m_edt_RegTimeLimit.SetWindowText(dt.Format());
	}
}


void CDongleTestDlg::OnBnClickedBtnCleartimelimit()
{
	CRegistry regMS("SOFTWARE\\Microsoft\\Windows");
	regMS.Delete("STLInfo");
	UpdateTimeLimit();
}
