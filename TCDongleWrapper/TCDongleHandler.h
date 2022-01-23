#ifndef tcdonglehandler_h
#define tcdonglehandler_h


#define	TCL_FIRMCODE						13804

#define	TCL_GRP_SINGLE						0x0
#define	TCL_GRP_MULTI						0x1
#define	TCL_GRP_NETWARE						0x2

#define	TCL_TYPE_UNLIMITED 		 			0x0
#define	TCL_TYPE_USERSINSTALLEDCOUNTER		0x1
#define	TCL_TYPE_STARTCOUNTER		 		0x2
#define	TCL_TYPE_TIMERCOUNTER		 		0x4
#define	TCL_TYPE_USERSONLINECOUNTER  		0x8
#define	TCL_TYPE_RESTARTTIMERCOUNTER  		0x10

class CTCDongleData : public CObject
{
	protected:
		CByteArray	m_aData;

	public:
		CTCDongleData();
		CTCDongleData(long *paValues,int size);
		CTCDongleData(BYTE *paValues,int size);

	public:
		void SetValues(long *paValues,int size);
		void SetValues(BYTE *paValues,int size);
		void GetValues(long *paValues,int size);
		void GetValues(BYTE *paValues,int size);
		void SetDongleID(long dongleID);
		void GetDongleID(long *pDongleID);
		void GetLicence(short *pLicGrp,short *pLicType);
		void SetLicence(short licGrp,short licType);
		void GetLicenceTypeValue(long *pLicTypeValue);
		void SetLicenceTypeValue(long licTypeValue);
		void GetUpdate(long *pMajorId,long *pMinorId);
		void SetUpdate(long majorId,long minorId);
		void Clear();

	protected:
		void Encode();
};



class CTCDongleHandler : public CObject
{
	protected:
		CTCDongleData m_dongleData;

	public:
		CTCDongleHandler();

	public:
		void GetLicence(short *pLicGroup,short *pLicType);
		void SetLicence(short licGroup,short licType);
		void GetLicenceTypeValue(long *pLicTypeValue);
		void SetLicenceTypeValue(long licTypeValue);
		void GetUpdate(long *pMajorId,long *pMinorId);
		void SetUpdate(long majorId,long minorId);
		BOOL ReadLicenceFile(const char *fileName,long *pDongleID);		
		BOOL WriteLicenceFile(const char *fileName);
		void SetDongleID(long dongleId);
};

#endif