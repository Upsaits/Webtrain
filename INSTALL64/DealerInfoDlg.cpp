// DealerInfoDlg.cpp: Implementierungsdatei
//

#include "stdafx.h"
#include "install.h"
#include "DealerInfoDlg.h"

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CDealerInfoDlg 


CDealerInfoDlg::CDealerInfoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDealerInfoDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDealerInfoDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Elementinitialisierung ein
	//}}AFX_DATA_INIT
}


void CDealerInfoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDealerInfoDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier DDX- und DDV-Aufrufe ein
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CDealerInfoDlg, CDialog)
	//{{AFX_MSG_MAP(CDealerInfoDlg)
		// HINWEIS: Der Klassen-Assistent fügt hier Zuordnungsmakros für Nachrichten ein
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// Behandlungsroutinen für Nachrichten CDealerInfoDlg 
