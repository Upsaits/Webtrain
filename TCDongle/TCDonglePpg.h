#if !defined(AFX_TCDONGLEPPG_H__084FAA0D_87D7_48ED_8642_23F0D91D297D__INCLUDED_)
#define AFX_TCDONGLEPPG_H__084FAA0D_87D7_48ED_8642_23F0D91D297D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

// TCDonglePpg.h: Deklaration der Eigenschaftenseitenklasse CTCDonglePropPage.
#include "Resource.h"
////////////////////////////////////////////////////////////////////////////
// CTCDonglePropPage: Siehe TCDonglePpg.cpp.cpp für Implementierung

class CTCDonglePropPage : public COlePropertyPage
{
	DECLARE_DYNCREATE(CTCDonglePropPage)
	DECLARE_OLECREATE_EX(CTCDonglePropPage)

// Konstruktor
public:
	CTCDonglePropPage();

// Dialogfelddaten
	//{{AFX_DATA(CTCDonglePropPage)
	enum { IDD = IDD_PROPPAGE_TCDONGLE };
		// HINWEIS - Der Klassen-Assistent fügt Datenelemente hier ein.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_DATA

// Implementierung
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterstützung

// Nachrichtenzuordnungstabellen
protected:
	//{{AFX_MSG(CTCDonglePropPage)
		// HINWEIS - Der Klassen-Assistent fügt Member-Funktionen hier ein und entfernt diese.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // !defined(AFX_TCDONGLEPPG_H__084FAA0D_87D7_48ED_8642_23F0D91D297D__INCLUDED)
