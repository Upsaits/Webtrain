#define VC_EXTRALEAN		// Selten verwendete Teile der Windows-Header nicht einbinden

#include <afxwin.h>         // MFC-Kern- und -Standardkomponenten
#include <afxext.h>         // MFC-Erweiterungen
#include <afxdtctl.h>		// MFC-Unterstützung für allgemeine Steuerelemente von Internet Explorer 4
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>			// MFC-Unterstützung für gängige Windows-Steuerelemente
#endif // _AFX_NO_AFXCMN_SUPPORT

#include <afxtempl.h>
#include <atlbase.h>
#include <afxstr.h>
#include "secdesc.h"
#include "resource.h"		// Hauptsymbole

#include "dib.h"
#include "dibwnd.h"
#include "profile.h"
#include "registry.h"
#include "LauncherWnd.h"
#include "Launcher.h"