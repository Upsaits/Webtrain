#if !defined(AFX_DEALERINFODLG_H__83BE1F13_CA5D_41F2_9345_AB3DFAD0AB88__INCLUDED_)
#define AFX_DEALERINFODLG_H__83BE1F13_CA5D_41F2_9345_AB3DFAD0AB88__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// DealerInfoDlg.h : Header-Datei
//

/////////////////////////////////////////////////////////////////////////////
// Dialogfeld CDealerInfoDlg 

class CDealerInfoDlg : public CDialog
{
// Konstruktion
public:
	CDealerInfoDlg(CWnd* pParent = NULL);   // Standardkonstruktor

// Dialogfelddaten
	//{{AFX_DATA(CDealerInfoDlg)
	enum { IDD = IDD_INSTDEALER_DIALOG };
		// HINWEIS: Der Klassen-Assistent f�gt hier Datenelemente ein
	//}}AFX_DATA


// �berschreibungen
	// Vom Klassen-Assistenten generierte virtuelle Funktions�berschreibungen
	//{{AFX_VIRTUAL(CDealerInfoDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV-Unterst�tzung
	//}}AFX_VIRTUAL

// Implementierung
protected:

	// Generierte Nachrichtenzuordnungsfunktionen
	//{{AFX_MSG(CDealerInfoDlg)
		// HINWEIS: Der Klassen-Assistent f�gt hier Member-Funktionen ein
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ f�gt unmittelbar vor der vorhergehenden Zeile zus�tzliche Deklarationen ein.

#endif // AFX_DEALERINFODLG_H__83BE1F13_CA5D_41F2_9345_AB3DFAD0AB88__INCLUDED_
