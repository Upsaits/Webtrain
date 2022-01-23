// TCDongle.cpp: Implementierung von CTCDongleApp und DLL-Registrierung.

#include "stdafx.h"
#include "TCDongle.h"
#include "TCDongleApp.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

#include "resource.h"       // Hauptsymbole



CTCDongleApp NEAR theApp;

const GUID CDECL BASED_CODE _tlid =
		{ 0x5399692a, 0x5b1d, 0x4eef, { 0xbe, 0x1b, 0xc8, 0x80, 0x15, 0x10, 0x18, 0xfc } };

const WORD _wVerMajor = 1;
const WORD _wVerMinor = 0;


////////////////////////////////////////////////////////////////////////////
// CTCDongleApp::InitInstance - DLL-Initialisierung

BOOL CTCDongleApp::InitInstance()
{
	BOOL bInit = COleControlModule::InitInstance();

	if (bInit)
	{
		// ZU ERLEDIGEN: Hier den Code für das Initialisieren der eigenen Module einfügen.
	}

	return bInit;
}


////////////////////////////////////////////////////////////////////////////
// CTCDongleApp::ExitInstance - DLL-Beendigung

int CTCDongleApp::ExitInstance()
{
	// ZU ERLEDIGEN: Hier den Code für das Beenden der eigenen Module einfügen.

	return COleControlModule::ExitInstance();
}


/////////////////////////////////////////////////////////////////////////////
// DllRegisterServer - Fügt der Systemregistrierung Einträge hinzu

STDAPI DllRegisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleRegisterTypeLib(AfxGetInstanceHandle(), _tlid))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(TRUE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}


/////////////////////////////////////////////////////////////////////////////
// DllUnregisterServer - Entfernt Einträge aus der Systemregistrierung

STDAPI DllUnregisterServer(void)
{
	AFX_MANAGE_STATE(_afxModuleAddrThis);

	if (!AfxOleUnregisterTypeLib(_tlid, _wVerMajor, _wVerMinor))
		return ResultFromScode(SELFREG_E_TYPELIB);

	if (!COleObjectFactoryEx::UpdateRegistryAll(FALSE))
		return ResultFromScode(SELFREG_E_CLASS);

	return NOERROR;
}
