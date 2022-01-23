// TCDongleCtl.cpp: Implementierung der CTCDongleCtrl-ActiveX-Steuerelementklasse.

#include "stdafx.h"
#include "TCDongle.h"
#include "TCDongleApp.h"
#include "TCDongleHandler.h"
#include "TCDongleCtl.h"
#include "TCDonglePpg.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


IMPLEMENT_DYNCREATE(CTCDongleCtrl, COleControl)


/////////////////////////////////////////////////////////////////////////////
// Nachrichtenzuordnungstabelle

BEGIN_MESSAGE_MAP(CTCDongleCtrl, COleControl)
	//{{AFX_MSG_MAP(CTCDongleCtrl)
	// HINWEIS - Der Klassen-Assistent fügt Nachrichtenzuordnungseinträge hinzu und entfernt diese
	//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_MSG_MAP
	ON_OLEVERB(AFX_IDS_VERB_PROPERTIES, OnProperties)
END_MESSAGE_MAP()


/////////////////////////////////////////////////////////////////////////////
// Dispatch-Tabelle

BEGIN_DISPATCH_MAP(CTCDongleCtrl, COleControl)
	//{{AFX_DISPATCH_MAP(CTCDongleCtrl)
	DISP_FUNCTION(CTCDongleCtrl, "GetDongleID", GetDongleID, VT_I4, VTS_NONE)
	DISP_FUNCTION(CTCDongleCtrl, "ReadDongle", ReadDongle, VT_BOOL, VTS_NONE)
	DISP_FUNCTION(CTCDongleCtrl, "ReadLicenceFile", ReadLicenceFile, VT_BOOL, VTS_BSTR VTS_PI4)
	DISP_FUNCTION(CTCDongleCtrl, "WriteLicenceFile", WriteLicenceFile, VT_BOOL, VTS_BSTR)
	DISP_FUNCTION(CTCDongleCtrl, "GetLicence", GetLicence, VT_EMPTY, VTS_PI2 VTS_PI2)
	DISP_FUNCTION(CTCDongleCtrl, "SetLicence", SetLicence, VT_EMPTY, VTS_I2 VTS_I2)
	DISP_FUNCTION(CTCDongleCtrl, "GetLicenceTypeValue", GetLicenceTypeValue, VT_EMPTY, VTS_PI4)
	DISP_FUNCTION(CTCDongleCtrl, "SetLicenceTypeValue", SetLicenceTypeValue, VT_EMPTY, VTS_I4)
	DISP_FUNCTION(CTCDongleCtrl, "GetUpdate", GetUpdate, VT_EMPTY, VTS_PI4 VTS_PI4)
	DISP_FUNCTION(CTCDongleCtrl, "SetUpdate", SetUpdate, VT_EMPTY, VTS_I4 VTS_I4)
	DISP_FUNCTION(CTCDongleCtrl, "WriteDongle", WriteDongle, VT_BOOL, VTS_NONE)
	DISP_FUNCTION(CTCDongleCtrl, "SetDongleID", SetDongleID, VT_EMPTY, VTS_I4)
	//}}AFX_DISPATCH_MAP
	DISP_FUNCTION_ID(CTCDongleCtrl, "AboutBox", DISPID_ABOUTBOX, AboutBox, VT_EMPTY, VTS_NONE)
END_DISPATCH_MAP()


/////////////////////////////////////////////////////////////////////////////
// Ereignistabelle

BEGIN_EVENT_MAP(CTCDongleCtrl, COleControl)
	//{{AFX_EVENT_MAP(CTCDongleCtrl)
	// HINWEIS - Der Klassen-Assistent fügt Einträge in die Ereignistabelle ein und entfernt diese
	//    Innerhalb dieser generierten Quelltextabschnitte NICHTS VERÄNDERN!
	//}}AFX_EVENT_MAP
END_EVENT_MAP()


/////////////////////////////////////////////////////////////////////////////
// Eigenschaftenseiten

// ZU ERLEDIGEN: Fügen Sie mehr Eigenschaftenseiten ein, als erforderlich sind.  Denken Sie daran, den Zähler zu erhöhen!
BEGIN_PROPPAGEIDS(CTCDongleCtrl, 1)
	PROPPAGEID(CTCDonglePropPage::guid)
END_PROPPAGEIDS(CTCDongleCtrl)


/////////////////////////////////////////////////////////////////////////////
// Klassenerzeugung und GUID initialisieren

IMPLEMENT_OLECREATE_EX(CTCDongleCtrl, "TCDONGLE.TCDongleCtrl.1",
	0x7bdcbfdc, 0x6fce, 0x4310, 0x96, 0xb0, 0x59, 0x29, 0xc8, 0x6d, 0x26, 0x7c)


/////////////////////////////////////////////////////////////////////////////
// Typbibliothek-ID und Version

IMPLEMENT_OLETYPELIB(CTCDongleCtrl, _tlid, _wVerMajor, _wVerMinor)


/////////////////////////////////////////////////////////////////////////////
// Schnittstellen-IDs

const IID BASED_CODE IID_DTCDongle =
		{ 0x5f7278d, 0x39d3, 0x4946, { 0x91, 0xb8, 0xd, 0x46, 0x65, 0x4e, 0x70, 0x25 } };
const IID BASED_CODE IID_DTCDongleEvents =
		{ 0x802c06d9, 0x493b, 0x496d, { 0x90, 0x74, 0xa9, 0x31, 0xc1, 0xea, 0xb6, 0x26} };


/////////////////////////////////////////////////////////////////////////////
// Steuerelement-Typinformation

static const DWORD BASED_CODE _dwTCDongleOleMisc =
	OLEMISC_INVISIBLEATRUNTIME |
	OLEMISC_SETCLIENTSITEFIRST |
	OLEMISC_INSIDEOUT |
	OLEMISC_CANTLINKINSIDE |
	OLEMISC_RECOMPOSEONRESIZE;

IMPLEMENT_OLECTLTYPE(CTCDongleCtrl, IDS_TCDONGLE, _dwTCDongleOleMisc)


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::CTCDongleCtrlFactory::UpdateRegistry -
// Fügt Einträge der Systemregistrierung für CTCDongleCtrl hinzu oder entfernt diese

BOOL CTCDongleCtrl::CTCDongleCtrlFactory::UpdateRegistry(BOOL bRegister)
{
	// ZU ERLEDIGEN: Prüfen Sie, ob Ihr Steuerelement den Thread-Regeln nach dem "Apartment"-Modell entspricht.
	// Weitere Informationen finden Sie unter MFC TechNote 64.
	// Falls Ihr Steuerelement nicht den Regeln nach dem Apartment-Modell entspricht, so
	// müssen Sie den nachfolgenden Code ändern, indem Sie den 6. Parameter von
	// afxRegApartmentThreading auf 0 ändern.

	if (bRegister)
		return AfxOleRegisterControlClass(
			AfxGetInstanceHandle(),
			m_clsid,
			m_lpszProgID,
			IDS_TCDONGLE,
			IDB_TCDONGLE,
			afxRegApartmentThreading,
			_dwTCDongleOleMisc,
			_tlid,
			_wVerMajor,
			_wVerMinor);
	else
		return AfxOleUnregisterClass(m_clsid, m_lpszProgID);
}


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::CTCDongleCtrl - Konstruktor

CTCDongleCtrl::CTCDongleCtrl()
{
	InitializeIIDs(&IID_DTCDongle, &IID_DTCDongleEvents);

	m_dongleHnd.Init();
}


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::~CTCDongleCtrl - Destruktor

CTCDongleCtrl::~CTCDongleCtrl()
{
	m_dongleHnd.Exit();
}


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::OnDraw - Zeichenfunktion

void CTCDongleCtrl::OnDraw(CDC* pdc, const CRect& rcBounds, const CRect& rcInvalid)
{
	// ZU ERLEDIGEN: Folgenden Code durch eigene Zeichenfunktion ersetzen.
	pdc->FillRect(rcBounds, CBrush::FromHandle((HBRUSH)GetStockObject(WHITE_BRUSH)));
	pdc->Ellipse(rcBounds);
}


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::DoPropExchange - Unterstützung dauerhafter Eigenschaften

void CTCDongleCtrl::DoPropExchange(CPropExchange* pPX)
{
	ExchangeVersion(pPX, MAKELONG(_wVerMinor, _wVerMajor));
	COleControl::DoPropExchange(pPX);

	// ZU ERLEDIGEN: PX_ Funktionen für jede dauerhafte benutzerdefinierte Eigenschaft aufrufen.

}


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::GetControlFlags -
// Attribute, um die MFC-Implementierung von ActiveX-Steuerelementen anzupassen.
//
// Weitere Informationen über die Verwendung dieser Attribute, finden Sie unter MFC Technische Hinweise
// #nnn, "Optimieren eines ActiveXSteuerelements".
DWORD CTCDongleCtrl::GetControlFlags()
{
	DWORD dwFlags = COleControl::GetControlFlags();


	// Das Steuerelement lässt sich aktivieren, ohne ein Fenster zu erstellen.
	// ZU ERLEDIGEN: Wenn Sie die Behandlungsroutine für Nachrichten des Steuerelements schreiben,
	//		prüfen Sie zunächst, ob die Member-Variable m_hWnd
	//		einen Wert ungleich NULL besitzt.
	dwFlags |= windowlessActivate;
	return dwFlags;
}


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::OnResetState - Setzt das Steuerelement in den Standardzustand zurück

void CTCDongleCtrl::OnResetState()
{
	COleControl::OnResetState();  // Setzt die Standards zurück, die in DoPropExchange gefunden wurden

	// ZU ERLEDIGEN: Andere Steuerelementzustände hier zurücksetzen.
}


/////////////////////////////////////////////////////////////////////////////
// CTCDongleCtrl::AboutBox - Ein Dialogfeld "Info" für den Benutzer anzeigen

void CTCDongleCtrl::AboutBox()
{
	CDialog dlgAbout(IDD_ABOUTBOX_TCDONGLE);
	dlgAbout.DoModal();
}


// Dongle-ID auslesen
long CTCDongleCtrl::GetDongleID() 
{
	return m_dongleHnd.GetDongleID();
}

BOOL CTCDongleCtrl::ReadDongle() 
{
	return m_dongleHnd.ReadDongle();
}

BOOL CTCDongleCtrl::ReadLicenceFile(LPCTSTR fileName,long *pDongleId) 
{
	return m_dongleHnd.ReadLicenceFile(fileName,pDongleId);
}

BOOL CTCDongleCtrl::WriteLicenceFile(LPCTSTR fileName) 
{
	return m_dongleHnd.WriteLicenceFile(fileName);
}

void CTCDongleCtrl::GetLicence(short FAR* pLicGrp, short FAR* pLicType) 
{
	m_dongleHnd.GetLicence(pLicGrp,pLicType);
}

void CTCDongleCtrl::SetLicence(short licGrp, short licType) 
{
	m_dongleHnd.SetLicence(licGrp,licType);
}

void CTCDongleCtrl::GetLicenceTypeValue(long FAR* pLicTypeVal) 
{
	m_dongleHnd.GetLicenceTypeValue(pLicTypeVal);
}

void CTCDongleCtrl::SetLicenceTypeValue(long licTypeValue) 
{
	m_dongleHnd.SetLicenceTypeValue(licTypeValue);	
}

void CTCDongleCtrl::GetUpdate(long FAR* pMajorId, long FAR* pMinorId) 
{
	m_dongleHnd.GetUpdate(pMajorId,pMinorId);
}

void CTCDongleCtrl::SetUpdate(long majorId, long minorId) 
{
	m_dongleHnd.SetUpdate(majorId,minorId);
}

BOOL CTCDongleCtrl::WriteDongle() 
{
	return m_dongleHnd.WriteDongle();
}

void CTCDongleCtrl::SetDongleID(long dongleID) 
{
	m_dongleHnd.SetDongleID(dongleID);
}
