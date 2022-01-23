#include "stdafx.h"

#include "HostInfo.h"

HostInfo::HostInfo()
		 :m_strError(""),m_bIsValid(false)
{
	char sName[HOST_NAME_LENGTH+1];
	memset(sName,0,sizeof(sName));
	gethostname(sName,HOST_NAME_LENGTH);
	m_pHost = gethostbyname(sName);
	m_bIsValid = (m_pHost!=NULL);
}

HostInfo::HostInfo(const CString& strHostName,hostType type)
{
	if (type == NAME)
	{
		// Retrieve host by name
		m_pHost = gethostbyname(strHostName);
		m_bIsValid = (m_pHost != NULL);
    }
	else if (type == ADDRESS)
	{
		// Retrieve host by address
		unsigned long netAddr = inet_addr(strHostName);
		if (netAddr != -1)
		{
			m_pHost = gethostbyaddr((char *)&netAddr, sizeof(netAddr), AF_INET);
			m_bIsValid = (m_pHost != NULL);
		}
		else
			m_bIsValid=false;

		if (!m_bIsValid)
			detectErrorGethostbyaddr(m_strError);
    }
}

void HostInfo::detectErrorGethostbyname(CString& errorMsg)
{
	int errCode = WSAGetLastError();
	
	if ( errCode == WSANOTINITIALISED )
		errorMsg = CString("need to call WSAStartup to initialize socket system on Window system.");
	else if ( errCode == WSAENETDOWN )
		errorMsg = CString("The network subsystem has failed.");
	else if ( errCode == WSAHOST_NOT_FOUND )
		errorMsg = CString("Authoritative Answer Host not found.");
	else if ( errCode == WSATRY_AGAIN )
		errorMsg = CString("Non-Authoritative Host not found, or server failure.");
	else if ( errCode == WSANO_RECOVERY )
		errorMsg = CString("Nonrecoverable error occurred.");
	else if ( errCode == WSANO_DATA )
		errorMsg = CString("Valid name, no data record of requested type.");
	else if ( errCode == WSAEINPROGRESS )
		errorMsg = CString("A blocking Windows Sockets 1.1 call is in progress, or the service provider is still processing a callback function.");
	else if ( errCode == WSAEFAULT )
		errorMsg = CString("The name parameter is not a valid part of the user address space.");
	else if ( errCode == WSAEINTR )
		errorMsg = CString("A blocking Windows Socket 1.1 call was canceled through WSACancelBlockingCall.");
}

void HostInfo::detectErrorGethostbyaddr(CString& errorMsg)
{
	int errCode = WSAGetLastError();

	if ( errCode == WSANOTINITIALISED )
		errorMsg = CString("A successful WSAStartup must occur before using this function.");
	if ( errCode == WSAENETDOWN )
		errorMsg = CString("The network subsystem has failed.");
	if ( errCode == WSAHOST_NOT_FOUND )
		errorMsg = CString("Authoritative Answer Host not found.");
	if ( errCode == WSATRY_AGAIN )
		errorMsg = CString("Non-Authoritative Host not found, or server failed."); 
	if ( errCode == WSANO_RECOVERY )
		errorMsg = CString("Nonrecoverable error occurred.");
	if ( errCode == WSANO_DATA )
		errorMsg = CString("Valid name, no data record of requested type.");
	if ( errCode == WSAEINPROGRESS )
		errorMsg = CString("A blocking Windows Sockets 1.1 call is in progress, or the service provider is still processing a callback function.");
	if ( errCode == WSAEAFNOSUPPORT )
		errorMsg = CString("The type specified is not supported by the Windows Sockets implementation.");
	if ( errCode == WSAEFAULT )
		errorMsg = CString("The addr parameter is not a valid part of the user address space, or the len parameter is too small.");
	if ( errCode == WSAEINTR )
		errorMsg = CString("A blocking Windows Socket 1.1 call was canceled through WSACancelBlockingCall.");
}

