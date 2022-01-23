// InfoDialog.cpp : implementation file
//

#include "stdafx.h"
#include "install.h"
#include "tcdonglehandler.h"
#include "registry.h"
#include "InfoDialog.h"
#include "INSTALLDlg.h"

#include "InstPatchDlg.h"
#include "filemanager.h"
#include "hostInfo.h"
#include "secdesc.h"
#include "checkdotnetversion.h"

static const CInfoDialog::LicenceType g_iLicServerSimulation=CInfoDialog::SingleLicense;

static BOOL IsCurrentUserLocalAdministrator();

const CString strSORegRoot(_T("SOFTWARE\\Wow6432Node"));
const CString strSORegKey(strSORegRoot+_T("\\SoftObject"));
const CString strClassesKey(_T("webtrain"));

const CString strRemoteInstallName="install_webtrain_remote.bat";

// CInfoDialog dialog
IMPLEMENT_DYNAMIC(CInfoDialog, CDialog)
CInfoDialog::CInfoDialog(CWnd* pParent /*=NULL*/)
	: CDialog(CInfoDialog::IDD, pParent)
{
	m_strInstallDir=GETAPPPTR->GetModulePath();
	m_installType=Normal;
	m_patchName="";
	m_strLicFilename="";
	m_iDongleId = -1;
}

CInfoDialog::~CInfoDialog()
{
}

void CInfoDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_TEXT, m_staText);
}


BEGIN_MESSAGE_MAP(CInfoDialog, CDialog)
	ON_WM_TIMER()
	ON_WM_CLOSE()
END_MESSAGE_MAP()


// CInfoDialog message handlers

BOOL CInfoDialog::OnInitDialog()
{
	CDialog::OnInitDialog();

	//CheckURIScheme();
	
	SetTimer(1,1000,NULL);

	//CString sParam = CString(GETAPPPTR->m_lpCmdLine);
	//if (sParam.IsEmpty())
		m_staText.SetWindowText("Überprüfe System..");
	//else if (sParam.Find("Patch")>-1)
	//{
	//	SetWindowPos(NULL,0,0,0,0,SWP_HIDEWINDOW);
	//}

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void CInfoDialog::CheckURIScheme()
{
	CRegistry reg(strClassesKey);
	reg.Set(_T(""),_T(""),_T("open webtrain client"),HKEY_CLASSES_ROOT);
	reg.Set(_T(""),_T("URL Protocol"),_T(""),HKEY_CLASSES_ROOT);
	reg.Create(HKEY_CLASSES_ROOT,_T("webtrain\\DefaultIcon"));
	reg.Set(_T("\\DefaultIcon"),_T(""),_T("C:\\Program Files (x86)\\SoftObject\\WebTrain\\trainconcept.exe"),HKEY_CLASSES_ROOT);
	reg.Create(HKEY_CLASSES_ROOT,_T("webtrain\\shell"));
	reg.Create(HKEY_CLASSES_ROOT,_T("webtrain\\shell\\open"));	
	reg.Create(HKEY_CLASSES_ROOT,_T("webtrain\\shell\\open\\command"));
	reg.Set(_T("\\shell\\open\\command"),_T(""),_T("\"C:\\Program Files (x86)\\SoftObject\\WebTrain\\launcher.exe\" \"%1\""),HKEY_CLASSES_ROOT);

}

void CInfoDialog::OnTimer(UINT_PTR nIDEvent)
{
	// Timer1: Start
	if (nIDEvent==1)
	{
		KillTimer(1);

		// Aufruf ohne Parameter? --> Erstinstallation
		if (GETAPPPTR->m_lpCmdLine[0] == _T('\0'))
		{
			m_installType=Normal;
			m_staText.SetWindowText("Überprüfe Windows-Rechte..");
			SetTimer(2,5000,NULL);
		}// Aufrúf mit Parameter? --> Patch, Remote oder OpenVPN-Installation
		else
		{
			CString sParam = CString(GETAPPPTR->m_lpCmdLine);
			CFileManager fm;

			// Remote-Zugang einrichten?
			if (sParam.Find("Remote")>-1)
			{
				m_installType=Remote;
				m_staText.SetWindowText("Überprüfe Windows-Rechte..");
				SetTimer(2,5000,NULL);
			}
			// Patch einspielen?
			else if (sParam.Find("Patch")>-1)
			{
				m_installType=Patch;
				m_patchName = sParam.Mid(6,sParam.GetLength()-6);
				m_staText.SetWindowText("Suche nach Webtrain..");
				SetTimer(5,1000,NULL);
			}
			// OpenVPN Zugang einrichten?
			else if (sParam.Find("OpenVPN")>-1)
			{
				m_installType=OpenVPN;
				m_staText.SetWindowText("Installiere OpenVPN-Zugang..");
				SetTimer(6,3000,NULL);
			}
			else // Ungültiger Parameter -> Beenden
			{
				PostMessage(WM_CLOSE);
				return;
			}

		}
	}
	// Timer2: Adminrechte checken
	else if (nIDEvent==2)
	{
		KillTimer(2);

		BOOL bAdmin = IsCurrentUserLocalAdministrator();
		CString sText;
		if (!bAdmin)
		{
			sText.Format("Damit sie Webtrain installieren können\n"
				"müssen sie das Installationsprogramm mit Administrator Rechte\n\n"
				"starten. Bitte wenden sie sich an den Webtrain-Administrator!\n");
			AfxMessageBox(sText,MB_OK);
			PostMessage(WM_CLOSE);
			return;
		}

		if (m_installType==Normal)
		{

			//CString strFilePath="C:\\Users\\fmair\\Desktop\\TCInstallCD_DEV\\*.*";
			//CString strLicFile=fm.FindFilesByExt(strFilePath,"lic",false);
			CFileManager fm;
			CString strLicFile=fm.FindFilesByExt(GETAPPPTR->GetModulePath(true)+"*.*","lic",false);
			//AfxMessageBox(GETAPPPTR->GetModulePath(true) +":"+strLicFile,MB_OK);

			if (!strLicFile.IsEmpty())
			{
				m_strLicFilename = strLicFile;
				m_staText.SetWindowText("Überprüfe Webtrain-Lizenz..");
				SetTimer(4,2000,NULL);
			}
			else
			{
				StartInstallation(false);
			}
		}
		else
		{
			CRegistry reg(strSORegKey);
			CString sVal;
			reg.Set("\\TrainConcept","InstallationType","1"); // Set InstallationType for Installshield

			StartRemoteInst();
			PostMessage(WM_CLOSE);
		}
	}
	// Timer3: Server-Installation prüfen
	else if (nIDEvent==3)
	{
		KillTimer(3);

		BOOL bAdmin = IsCurrentUserLocalAdministrator();
		CString sText;
		if (!bAdmin)
		{
			sText.Format("Damit sie Webtrain installieren können\n"
				"müssen sie das Installationsprogramm als Administrator!\n\n"
				"starten. Bitte wenden sie sich an den Webtrain-Administrator!\n");
			AfxMessageBox(sText,MB_OK);
			PostMessage(WM_CLOSE);
			return;
		}

		bool doIt=false;
		CRegistry regIIS("SYSTEM\\CurrentControlSet\\Services\\W3SVC");
		int iVersion=0;
		if (!regIIS.Get("\\Parameters","MajorVersion",iVersion))
			doIt=true;
		else
		{
			if (iVersion<4)
			{
				sText.Format("Sorry, but TrainConcept-Server-Version only runs\n"
					"with IIS version 4.0 or higher!\n\n");
				AfxMessageBox(sText,MB_OK);
				PostMessage(WM_CLOSE);
				return;
			}
		}

		if (doIt)
		{
			sText.Format("For using the TrainConcept-Server-Version you will need\n"
				"the following component(s):\n\n"
				"Microsoft Internet-Information-Service Version>=4.0\n\n"
				"Please restart Setup Program after the installation has completed!\n\n"
				"Do you want to install it?");
			if (AfxMessageBox(sText,MB_YESNO)==IDYES)
			{
				StartIISInst();
				PostMessage(WM_CLOSE);
				return;
			}
		}

		HostInfo hi;
		if (!hi.bIsValid())
		{
			sText.Format("Sorry, but the TrainConcept-Server-Version only runs\n"
						 "with systems that support a TCP/IP address!\n\n"
						 "Do you have a network connection?");
			AfxMessageBox(sText,MB_OK);
			PostMessage(WM_CLOSE);
			return;
		}

		if (!(::IsNetfx40ClientInstalled() || ::IsNetfx40FullInstalled()))
		{
			sText.Format("For using the TrainConcept-Server-Version you will need\n"
				"the following component(s):\n\n"
				"Microsoft .NET Framework 4.0\n\n"
				"Please install it and restart Setup Program after the installation has completed!\n\n");
			AfxMessageBox(sText,MB_OK);
			PostMessage(WM_CLOSE);
			return;
		}

		CRegistry reg(strSORegKey);
		reg.Set("\\TrainConcept","InstServerIPAdress",hi.getHostIPAddress());
		reg.Set("\\TrainConcept","InstServerName",hi.getHostName());
		reg.Delete("\\TrainConcept","InstallationCounter"); 
		reg.Delete("\\TrainConcept","InstallationPath"); 
		
		StartInstallation(false);
	}
	// Timer4: Installation starten
	else if (nIDEvent==4)
	{
		KillTimer(4);
		StartInstallation();
	}
	// Timer5: Patch-Installation
	else if (nIDEvent==5)
	{
		KillTimer(5);

		CString dir;
		CRegistry reg(strSORegKey);
		if (!reg.Get("\\TrainConcept","InstallationPath",dir))
			dir=FindTrainConceptDir();

		if (dir.IsEmpty())
		{
			AfxMessageBox("Please Install TrainConcept first!");
			PostMessage(WM_CLOSE);
		}
		else
		{
			CFileManager fm;
			fm.RemoveSlashAtEnd(dir);
			CRegistry reg(strSORegKey);
			reg.Set("\\TrainConcept","InstallationPath",dir);
			m_staText.SetWindowText("Preparing TrainConcept..");
			SetTimer(6,1000,NULL);
			m_strInstallDir=dir;
		}
	}
	// Timer6: OpenVPN-Zugangs-Installation
	else if (nIDEvent==6)
	{
		KillTimer(6);

		CFileManager fm;

		if (m_installType==Patch)
		{
			CInstPatchDlg dlg(m_patchName,m_strInstallDir,this);
			dlg.DoModal();

			CheckURIScheme();

			if (!dlg.m_bIsServer)
			{
				WritePrivateProfileString(_T("System"), _T("LicenseId"),_T("1019"),m_strInstallDir +"\\trainconcept.ini");
			}
		}
		else if (m_installType==OpenVPN)
		{
			TCHAR szPath[MAX_PATH];
			if (SUCCEEDED(SHGetSpecialFolderPath(NULL,szPath,CSIDL_PROGRAM_FILES,FALSE)))
			{
				PathAppend(szPath,_T("\\OpenVPN\\config\\"));
				fm.CopyAllFiles(m_strInstallDir+"OpenVPN\\*.*",szPath);
			}

			CRegistry reg(strSORegKey);
			CString sVal;
			reg.Set("\\TrainConcept","RunAlwaysClientOnly","\"C:\\Program Files\\OpenVPN\\bin\\openvpn-gui.exe\" --connect client_metzentrum.ovpn --silent_connection 1"); 
			reg.Delete("RunAlways");
		}

		PostMessage(WM_CLOSE);
	}

	CDialog::OnTimer(nIDEvent);
}

void CInfoDialog::OnClose()
{
	CDialog::OnClose();
}

void CInfoDialog::StartExplorerInst()
{
	STARTUPINFO si;
	PROCESS_INFORMATION pi;

	ZeroMemory( &si, sizeof(si) );
	si.cb = sizeof(si);
	ZeroMemory( &pi, sizeof(pi) );

	CString fileName = m_strInstallDir+"IExplorer60\\ie6setup.exe";
	if (CreateProcess(NULL,(LPSTR)(LPCTSTR)fileName,NULL,
		NULL,FALSE,
		CREATE_SUSPENDED|NORMAL_PRIORITY_CLASS,
		NULL,NULL,&si,&pi))
		ResumeThread(pi.hThread);
}

void CInfoDialog::StartIISInst()
{
	STARTUPINFO si;
	PROCESS_INFORMATION pi;

	ZeroMemory( &si, sizeof(si) );
	si.cb = sizeof(si);
	ZeroMemory( &pi, sizeof(pi) );

	CString fileName = m_strInstallDir+"IISInstall\\install.bat";
	CString sWorkingDir = m_strInstallDir+"IISInstall";

	if (CreateProcess(NULL,(LPSTR)(LPCTSTR)fileName,NULL,
		NULL,FALSE,
		CREATE_SUSPENDED|NORMAL_PRIORITY_CLASS,
		NULL,(LPTSTR)(LPCTSTR)sWorkingDir,&si,&pi))
		ResumeThread(pi.hThread);
}

void CInfoDialog::StartRemoteInst()
{
	STARTUPINFO si;
	PROCESS_INFORMATION pi;

	ZeroMemory( &si, sizeof(si) );
	si.cb = sizeof(si);
	ZeroMemory( &pi, sizeof(pi) );

	CString fileName = m_strInstallDir+strRemoteInstallName;
	CString sWorkingDir = m_strInstallDir;

	if (CreateProcess(NULL,(LPSTR)(LPCTSTR)fileName,NULL,
		NULL,FALSE,
		CREATE_SUSPENDED|NORMAL_PRIORITY_CLASS,
		NULL,(LPTSTR)(LPCTSTR)sWorkingDir,&si,&pi))
		ResumeThread(pi.hThread);
}

void CInfoDialog::StartTrainConcept(const CString &strStartPath)
{
	STARTUPINFO si;
	PROCESS_INFORMATION pi;

	ZeroMemory( &si, sizeof(si) );
	si.cb = sizeof(si);
	ZeroMemory( &pi, sizeof(pi) );

	CString fileName = strStartPath+"\\TrainConcept.exe";
	CString sWorkingDir = strStartPath;

	if (CreateProcess(NULL,(LPSTR)(LPCTSTR)fileName,NULL,
		NULL,FALSE,
		CREATE_SUSPENDED|NORMAL_PRIORITY_CLASS,
		NULL,(LPTSTR)(LPCTSTR)sWorkingDir,&si,&pi))
		ResumeThread(pi.hThread);
}

void CInfoDialog::StartInstallation(bool bShowDialog)
{
	CString fileName = m_strInstallDir+"TrainConcept 2008\\setup.exe";
	bool startSetup=true;
	if (bShowDialog)
	{
		long cnt=0;
		LicenceRestriction eLicRestrict=Unlimited;
		LicenceType eLicVer = GetUseType(eLicRestrict,cnt);

		if (eLicVer==InvalidLicense)
		{
			AfxMessageBox("invalid License!",MB_OK);
			PostMessage(WM_CLOSE);
			return;
		}
		else if (eLicRestrict == Time)
		{
			// Zeiteinträge prüfen
			CString sId;
			CRegistry reg("SOFTWARE\\Microsoft\\Windows");
			if (reg.Get("\\STLInfo","STLVersionId",sId))
			{
				if (reg.String2Int(sId)>=m_iDongleId)
				{
					AfxMessageBox("License already installed!",MB_OK);
					PostMessage(WM_CLOSE);
					return;
				}
			}

		}

		ShowWindow(SW_HIDE);
		CINSTALLDlg dlg(this);
		if (dlg.DoModal()==IDOK)
		{
			// Clean-up registry
			CRegistry regSW(strSORegRoot);
			regSW.Delete("SoftObject");

			CRegistry regMS("SOFTWARE\\Microsoft\\Windows");
			regMS.Delete("STLInfo");

			eLicVer = dlg.GetLicence();
			//CString sVal1;
			//sVal1.Format("%d",static_cast<int>(eLicVer));
			//AfxMessageBox(sVal1);

			if (eLicVer!=DemoLicense && eLicVer!=ClientLicense)
			{
				if (eLicRestrict == Time)
				{
					// Zeiteinträge durchführen
					CString sId;
					CRegistry reg("SOFTWARE\\Microsoft\\Windows");
					sId.Format("%d",m_iDongleId);
					reg.Set("\\STLInfo","STLVersionId",sId);
					CTime now=CTime::GetCurrentTime();

					CString sNow=now.Format("%d%m%Y");
					CString sErg;
					EncodeDate(sNow,sErg);
					reg.Set("\\STLInfo","STLVersion",sErg);
				}

				CRegistry regRO(strSORegKey);
				CString exeToRun="\""+m_strInstallDir+"setup64.exe\" " + m_strLicFilename;
				regRO.Set("\\TrainConcept","RunOnce",exeToRun);
			}

			CRegistry reg(strSORegKey);
			CString sVal;
			sVal.Format("%d",static_cast<int>(eLicVer));
			reg.Set("\\TrainConcept","InstallationType",sVal); // Set InstallationType for Installshield

			switch(eLicVer)
			{
			case SingleLicense:
			case ClientLicense:
			case DemoLicense:
				break;
			case ServerLicense:
				ShowWindow(SW_SHOW);
				m_staText.SetWindowText("Checking IIS-Version..");
				SetTimer(3,2000,NULL);
				return;
			case MultiLicense:
				{
					CString sCnt;
					sCnt.Format("%d",cnt);
					reg.Set("\\TrainConcept","InstallationCounter",sCnt);
					{
						CRegistry reg1("SOFTWARE\\Microsoft");
						reg1.Set("\\Windows","OBIVersion","080675");
					}
				}
				break;
			}

		}
		else
		{
			PostMessage(WM_CLOSE);
			startSetup=false;
		}
	}

	if (startSetup)
	{
		PostMessage(WM_CLOSE);

#ifndef _DEBUG
		STARTUPINFO si;
		PROCESS_INFORMATION pi;
		ZeroMemory( &si, sizeof(si) );
		si.cb = sizeof(si);
		ZeroMemory( &pi, sizeof(pi) );

		if (CreateProcess(NULL,(LPSTR)(LPCTSTR)fileName,NULL,
			NULL,FALSE,
			CREATE_SUSPENDED|NORMAL_PRIORITY_CLASS,
			NULL,NULL,&si,&pi))
			ResumeThread(pi.hThread);
#else
		AfxMessageBox("TCInstall started!");
#endif
	}
}


CInfoDialog::LicenceType CInfoDialog::GetUseType(LicenceRestriction &eLicRestrict,long &cnt)
{
	eLicRestrict = Unlimited;

#if defined(CHECK_LICENSE)
	// Linzenzfile vorhanden und gültig?
	if (!m_strLicFilename.IsEmpty() &&
		(m_dongleHnd.ReadLicenceFile(m_strInstallDir+m_strLicFilename,&m_iDongleId) && m_iDongleId>=1000 && m_iDongleId<=100000))
	{
		// Vorhergehende Installation abgebrochen?->InstallType bleibt mit Wert 5 erhalten
		// dann Ohne Zeiteinträge weiter
		CString sType;
		CRegistry regType(strSORegKey);
		if (regType.Get("\\TrainConcept","InstallationType",sType))
		{
			if (sType=="5")
			{
				return InvalidLicense;
			}
		}

		LicenceType eLicType=InvalidLicense;
		short grp=0;
		short type=0;
		long cnt=0;
		m_dongleHnd.GetLicence(&grp,&type);
		m_dongleHnd.GetLicenceTypeValue(&cnt);
		if (type==TCL_TYPE_TIMERCOUNTER)
			eLicRestrict=Time;
		else if (type ==TCL_TYPE_USERSONLINECOUNTER)
			eLicRestrict=UserCount;

		m_dongleHnd.GetLicenceTypeValue(&cnt);
		
		if (grp!=TCL_GRP_SINGLE)
		{ 
			if (grp==TCL_GRP_MULTI)
				return (cnt>0) ? MultiLicense :InvalidLicense;
			else
				return ServerLicense;
		}
		else
			return SingleLicense;
	}
	return DemoLicense;
	
#elif defined(CHECK_LICSERVER)
	switch(g_iLicServerSimulation)
	{
	case Multi:	 cnt=10;break;
	case Server: cnt=50;
	}
	return g_iLicServerSimulation;
#else
	CRegistry reg(strSORegKey);
	CString sVersion="3"; // Falls nicht gefunden, Demo-Setup starten
	if (reg.Get("\\TrainConcept","InstallationType",sVersion))
	{
		if (sVersion=="1" || sVersion=="2")
		{
			CString sCount;
			if (reg.Get("\\TrainConcept","InstallationCount",sCount))
				cnt = reg.String2Int(sCount);
		}
	}
	return static_cast<LicenceType>(reg.String2Int(sVersion));
#endif

}


CString CInfoDialog::FindSetupDir()
{
	CFileManager fm;
	int curdrive;
	static char path[_MAX_PATH];

	/* Save current drive. */
	curdrive = _getdrive();

	/* If we can switch to the drive, it exists. */
	for(int drive = 3; drive <= 26; drive++)
		if(!_chdrive(drive))
		{
			CString drv=fm.FindDir(CString((char)('A'+drive-1))+":\\*.*","TrainConcept 2008");
			if (drv.IsEmpty())
				drv=fm.FindDir(CString((char)('A'+drive-1))+":\\TCInstallCD\\*.*","TrainConcept 2008");
			if (!drv.IsEmpty())
			{
				_chdrive( curdrive );
				return drv+"\\";
			}
		}

		/* Restore original drive.*/
		_chdrive( curdrive );
		return "";
}


CString CInfoDialog::FindTrainConceptDir()
{
	CFileManager fm;
	int curdrive;
	static char path[_MAX_PATH];

	/* Save current drive. */
	curdrive = _getdrive();

	/* If we can switch to the drive, it exists. */
	for(int drive = 3; drive <= 26; drive++)
		if(!_chdrive(drive))
		{
			CString drv=fm.FindFile(CString((char)('A'+drive-1))+":\\*.*","trainconcept.ini");
			if (!drv.IsEmpty())
			{
				_chdrive( curdrive );
				return drv+"\\";
			}
		}

		/* Restore original drive.*/
		_chdrive( curdrive );
		return "";
}

void CInfoDialog::EncodeDate(const CString &str,CString &sOut)
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



void CInfoDialog::SetRegKeySecurity(CRegistry &reg)
{
	CSid sidEveryone(CSid::WST_EVERYONE);
	CSid sidLocalAdmin(CSid::WST_LOCALADMINS);

	CTrustee trEveryone(TRUSTEE_IS_WELL_KNOWN_GROUP, sidEveryone);
	CTrustee trLocalAdmin(TRUSTEE_IS_GROUP,sidLocalAdmin);


	EXPLICIT_ACCESS ea[2];

	ea[0] = CExplicitAccess(KEY_READ, SET_ACCESS, SUB_CONTAINERS_AND_OBJECTS_INHERIT, trEveryone);
	ea[1] = CExplicitAccess(KEY_ALL_ACCESS, SET_ACCESS, SUB_CONTAINERS_AND_OBJECTS_INHERIT, trLocalAdmin);
	CAcl acl;
	if(acl.SetEntriesInAcl(2, ea) == ERROR_SUCCESS)
	{
		// Initialize a security descriptor and add our ACL to it  
		CSecurityDescriptor sd;
		if(sd.SetSecurityDescriptorDacl(TRUE,     // fDaclPresent flag   
										acl, 
										FALSE))   // not a default DACL 
		{
			// Initialize a security attributes structure.
			CSecurityAttributes sa(sd, FALSE);

			// Use the security attributes to set the security descriptor 
			// when you create a key.

			// Use the security attributes to set the security descriptor 
			// when you create a key.
			DWORD	dwDisposition;
			HKEY	hKey;
			LONG lResult = ::RegCreateKeyEx(HKEY_LOCAL_MACHINE, "Software\\SoftObject2", 0, "", 0, 
											KEY_ALL_ACCESS, sa, &hKey, &dwDisposition); 
			//::RegCloseKey(hKey);

			lResult=::RegSetKeySecurity(hKey,DACL_SECURITY_INFORMATION,(PSECURITY_DESCRIPTOR)sd);
			::RegCloseKey(hKey);
		
			int test10=10;
		}
	}
}

//--> http://support.microsoft.com/kb/q118626/
/*-------------------------------------------------------------------------
IsCurrentUserLocalAdministrator ()

This function checks the token of the calling thread to see if the caller
belongs to the Administrators group.

Return Value:
   TRUE if the caller is an administrator on the local machine.
   Otherwise, FALSE.
--------------------------------------------------------------------------*/
BOOL IsCurrentUserLocalAdministrator()
{
   BOOL   fReturn         = FALSE;
   DWORD  dwStatus;
   DWORD  dwAccessMask;
   DWORD  dwAccessDesired;
   DWORD  dwACLSize;
   DWORD  dwStructureSize = sizeof(PRIVILEGE_SET);
   PACL   pACL            = NULL;
   PSID   psidAdmin       = NULL;

   HANDLE hToken              = NULL;
   HANDLE hImpersonationToken = NULL;

   PRIVILEGE_SET   ps;
   GENERIC_MAPPING GenericMapping;

   PSECURITY_DESCRIPTOR     psdAdmin           = NULL;
   SID_IDENTIFIER_AUTHORITY SystemSidAuthority = SECURITY_NT_AUTHORITY;


   /*
      Determine if the current thread is running as a user that is a member of
      the local admins group.  To do this, create a security descriptor that
      has a DACL which has an ACE that allows only local aministrators access.
      Then, call AccessCheck with the current thread's token and the security
      descriptor.  It will say whether the user could access an object if it
      had that security descriptor.  Note: you do not need to actually create
      the object.  Just checking access against the security descriptor alone
      will be sufficient.
   */
   const DWORD ACCESS_READ  = 1;
   const DWORD ACCESS_WRITE = 2;


   __try
   {

      /*
         AccessCheck() requires an impersonation token.  We first get a primary
         token and then create a duplicate impersonation token.  The
         impersonation token is not actually assigned to the thread, but is
         used in the call to AccessCheck.  Thus, this function itself never
         impersonates, but does use the identity of the thread.  If the thread
         was impersonating already, this function uses that impersonation context.
      */
      if (!OpenThreadToken(GetCurrentThread(), TOKEN_DUPLICATE|TOKEN_QUERY,TRUE, &hToken))
      {
         if (GetLastError() != ERROR_NO_TOKEN)
            __leave;

         if (!OpenProcessToken(GetCurrentProcess(),TOKEN_DUPLICATE|TOKEN_QUERY, &hToken))
            __leave;
      }

      if (!DuplicateToken (hToken, SecurityImpersonation,&hImpersonationToken))
          __leave;


      /*
        Create the binary representation of the well-known SID that
        represents the local administrators group.  Then create the security
        descriptor and DACL with an ACE that allows only local admins access.
        After that, perform the access check.  This will determine whether
        the current user is a local admin.
      */
      if (!AllocateAndInitializeSid(&SystemSidAuthority, 2,
                                    SECURITY_BUILTIN_DOMAIN_RID,
                                    DOMAIN_ALIAS_RID_ADMINS,
                                    0, 0, 0, 0, 0, 0, &psidAdmin))
         __leave;

      psdAdmin = LocalAlloc(LPTR, SECURITY_DESCRIPTOR_MIN_LENGTH);
      if (psdAdmin == NULL)
         __leave;

      if (!InitializeSecurityDescriptor(psdAdmin,SECURITY_DESCRIPTOR_REVISION))
         __leave;

      // Compute size needed for the ACL.
      dwACLSize = sizeof(ACL) + sizeof(ACCESS_ALLOWED_ACE) +
                  GetLengthSid(psidAdmin) - sizeof(DWORD);

      pACL = (PACL)LocalAlloc(LPTR, dwACLSize);
      if (pACL == NULL)
         __leave;

      if (!InitializeAcl(pACL, dwACLSize, ACL_REVISION2))
         __leave;

      dwAccessMask= ACCESS_READ | ACCESS_WRITE;

      if (!AddAccessAllowedAce(pACL, ACL_REVISION2, dwAccessMask,psidAdmin))
         __leave;

      if (!SetSecurityDescriptorDacl(psdAdmin, TRUE, pACL, FALSE))
         __leave;

      /*
         AccessCheck validates a security descriptor somewhat; set the group
         and owner so that enough of the security descriptor is filled out to
         make AccessCheck happy.
      */
      SetSecurityDescriptorGroup(psdAdmin, psidAdmin, FALSE);
      SetSecurityDescriptorOwner(psdAdmin, psidAdmin, FALSE);

      if (!IsValidSecurityDescriptor(psdAdmin))
         __leave;

      dwAccessDesired = ACCESS_READ;

      /*
         Initialize GenericMapping structure even though you
         do not use generic rights.
      */
      GenericMapping.GenericRead    = ACCESS_READ;
      GenericMapping.GenericWrite   = ACCESS_WRITE;
      GenericMapping.GenericExecute = 0;
      GenericMapping.GenericAll     = ACCESS_READ | ACCESS_WRITE;

      if (!AccessCheck(psdAdmin, hImpersonationToken, dwAccessDesired,
                       &GenericMapping, &ps, &dwStructureSize, &dwStatus,
                       &fReturn))
      {
         fReturn = FALSE;
         __leave;
      }
   }
   __finally
   {
      // Clean up.
      if (pACL) LocalFree(pACL);
      if (psdAdmin) LocalFree(psdAdmin);
      if (psidAdmin) FreeSid(psidAdmin);
      if (hImpersonationToken) CloseHandle (hImpersonationToken);
      if (hToken) CloseHandle (hToken);
   }

   return fReturn;
}
