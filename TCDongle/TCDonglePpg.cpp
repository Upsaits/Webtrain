// TCDonglePpg.cpp: Implementierung der Eigenschaftenseitenklasse CTCDonglePropPage.

#include "stdafx.h"
#include "TCDongle.h"
#include "TCDonglePpg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


IMPLEMENT_DYNCREATE(CTCDonglePropPage, COlePropertyPage)


/////////////////////////////////////////////////////////////////////////////
// Nachrichtenzuordnungstabelle

BEGIN_MESSAGE_MAP(CTCDonglePropPage, COlePropertyPage)
	//{{AFX_MSG_MAP(CTCDonglePropPage)
	// HINWEIS - Der Klassen-Assistent fügt Nachrichtenzuordnungseinträge hinzu und entfernt diese
	//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()


/////////////////////////////////////////////////////////////////////////////
// Klassenerzeugung und GUID initialisieren

IMPLEMENT_OLECREATE_EX(CTCDonglePropPage, "TCDONGLE.TCDonglePropPage.1",
	0x94b760bd, 0xffaa, 0x4b75, 0x86, 0x19, 0x45, 0xfc, 0xb5, 0xc2, 0x35, 0xea)


/////////////////////////////////////////////////////////////////////////////
// CTCDonglePropPage::CTCDonglePropPageFactory::UpdateRegistry -
// Fügt Einträge der Systemregistrierung für CTCDonglePropPage hinzu oder entfernt diese

BOOL CTCDonglePropPage::CTCDonglePropPageFactory::UpdateRegistry(BOOL bRegister)
{
	if (bRegister)
		return AfxOleRegisterPropertyPageClass(AfxGetInstanceHandle(),
			m_clsid, IDS_TCDONGLE_PPG);
	else
		return AfxOleUnregisterClass(m_clsid, NULL);
}


/////////////////////////////////////////////////////////////////////////////
// CTCDonglePropPage::CTCDonglePropPage - Konstruktor

CTCDonglePropPage::CTCDonglePropPage() :
	COlePropertyPage(IDD, IDS_TCDONGLE_PPG_CAPTION)
{
	//{{AFX_DATA_INIT(CTCDonglePropPage)
	// HINWEIS: Der Klassen-Assistent fügt die Elementinitialisierung hier ein
	//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_DATA_INIT
}


/////////////////////////////////////////////////////////////////////////////
// CTCDonglePropPage::DoDataExchange - Verschiebt Daten zwischen Dialogfeld+++ und den Variablen+++

void CTCDonglePropPage::DoDataExchange(CDataExchange* pDX)
{
	//{{AFX_DATA_MAP(CTCDonglePropPage)
	// HINWEIS: Der Klassen-Assistent fügt  DDP-, DDX- und DDV-Aufrufe hier ein
	//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_DATA_MAP
	DDP_PostProcessing(pDX);
}


/////////////////////////////////////////////////////////////////////////////
// CTCDonglePropPage-Behandlungsroutinen für Nachrichten
