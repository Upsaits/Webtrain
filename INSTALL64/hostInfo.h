#pragma once

#include <string>
using namespace std;

#include <winsock2.h>
#include <stdio.h>
    
enum hostType {NAME, ADDRESS};

const int HOST_NAME_LENGTH = 64;

class HostInfo
{
private:
	struct hostent *m_pHost;    // Entry within the host address database
	bool m_bIsValid;
	CString m_strError;

public:
    // Default constructor
    HostInfo();
    // Retrieves the host entry based on the host name or address
	HostInfo(const CString& hostName, hostType type);

	bool bIsValid() { return m_bIsValid;};

    // Retrieves the hosts IP address
    char* getHostIPAddress() 
    {
        struct in_addr *addr_ptr;
		// the first address in the list of host addresses
        addr_ptr = (struct in_addr *)*m_pHost->h_addr_list;
		// changed the address format to the Internet address in standard dot notation
        return inet_ntoa(*addr_ptr);
    }    
    
    // Retrieves the hosts name
    char* getHostName()
    {
        return m_pHost->h_name;
    }

private:
	void detectErrorGethostbyname(CString &errorMsg);
	void detectErrorGethostbyaddr(CString &errorMsg);
};
