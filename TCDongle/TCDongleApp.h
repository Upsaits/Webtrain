#pragma once

/////////////////////////////////////////////////////////////////////////////
// CTCDongleApp: Siehe TCDongle.cpp für Implementierung
/////////////////////////////////////////////////////////////////////////////
class CTCDongleApp : public COleControlModule
{
public:
	BOOL InitInstance();
	int ExitInstance();
};
extern const GUID CDECL _tlid;
extern const WORD _wVerMajor;
extern const WORD _wVerMinor;

