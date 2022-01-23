#if !defined(AFX_TCDONGLECTL_H__E52D9652_3F3E_4DB8_9D6D_EB9BF50D9AD3__INCLUDED_)
#define AFX_TCDONGLECTL_H__E52D9652_3F3E_4DB8_9D6D_EB9BF50D9AD3__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

// TCDongleCtl.h: Deklaration der CTCDongleCtrl-ActiveX-Steuerelementeklasse.

/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl: Siehe TCDongleCtl.cpp für Implementierung.
class CTCDongleCtrl : public COleControl
{
	DECLARE_DYNCREATE(CTCDongleCtrl)

protected:
	CTCDongleHandler	m_dongleHnd;

// Konstruktor
public:
	CTCDongleCtrl();

// Überladungen
	// Vom Klassenassistenten generierte Überladungen virtueller Funktionen
	//{{AFX_VIRTUAL(CTCDongleCtrl)
	public:
	virtual void OnDraw(CDC* pdc, const CRect& rcBounds, const CRect& rcInvalid);
	virtual void DoPropExchange(CPropExchange* pPX);
	virtual void OnResetState();
	virtual DWORD GetControlFlags();
	//}}AFX_VIRTUAL

// Implementierung
protected:
	~CTCDongleCtrl();

	DECLARE_OLECREATE_EX(CTCDongleCtrl)    // Klassenerzeugung und GUID
	DECLARE_OLETYPELIB(CTCDongleCtrl)      // GetTypeInfo
	DECLARE_PROPPAGEIDS(CTCDongleCtrl)     // Eigenschaftenseiten-IDs
	DECLARE_OLECTLTYPE(CTCDongleCtrl)		// Typname und versch. Status

// Nachrichtenzuordnungstabellen
	//{{AFX_MSG(CTCDongleCtrl)
		// HINWEIS - Der Klassen-Assistent fügt Member-Funktionen hier ein und entfernt diese.
		//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

// Dispatch-Tabellen
	//{{AFX_DISPATCH(CTCDongleCtrl)
	afx_msg long GetDongleID();
	afx_msg BOOL ReadDongle();
	afx_msg BOOL ReadLicenceFile(LPCTSTR fileName,long *pDongleId);
	afx_msg BOOL WriteLicenceFile(LPCTSTR fileName);
	afx_msg void GetLicence(short FAR* pLicGrp, short FAR* pLicType);
	afx_msg void SetLicence(short licGrp, short licType);
	afx_msg void GetLicenceTypeValue(long FAR* pLicTypeVal);
	afx_msg void SetLicenceTypeValue(long licTypeValue);
	afx_msg void GetUpdate(long FAR* pMajorId, long FAR* pMinorId);
	afx_msg void SetUpdate(long majorId, long minorId);
	afx_msg BOOL WriteDongle();
	afx_msg void SetDongleID(long dongleID);
	//}}AFX_DISPATCH
	DECLARE_DISPATCH_MAP()

	afx_msg void AboutBox();

// Ereignistabellen
	//{{AFX_EVENT(CTCDongleCtrl)
	//}}AFX_EVENT
	DECLARE_EVENT_MAP()

// Dispatch- und Ereignis-IDs
public:
	enum {
	//{{AFX_DISP_ID(CTCDongleCtrl)
	dispidGetDongleID = 1L,
	dispidReadDongle = 2L,
	dispidReadLicenceFile = 3L,
	dispidWriteLicenceFile = 4L,
	dispidGetLicence = 5L,
	dispidSetLicence = 6L,
	dispidGetLicenceTypeValue = 7L,
	dispidSetLicenceTypeValue = 8L,
	dispidGetUpdate = 9L,
	dispidSetUpdate = 10L,
	dispidWriteDongle = 11L,
	dispidSetDongleID = 12L,
	//}}AFX_DISP_ID
	};
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ fügt unmittelbar vor der vorhergehenden Zeile zusätzliche Deklarationen ein.

#endif // !defined(AFX_TCDONGLECTL_H__E52D9652_3F3E_4DB8_9D6D_EB9BF50D9AD3__INCLUDED)
