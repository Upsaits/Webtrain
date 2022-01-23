#include "stdafx.h"
#include "tcdonglehandler.h"

using namespace std;
/* ***************************** CTCDongleData ***************************** */
CTCDongleData::CTCDongleData()
{
	Clear();
}

CTCDongleData::CTCDongleData(long *paValues,int size)
{
	SetValues(paValues,size);
}

CTCDongleData::CTCDongleData(BYTE *paValues,int size)
{
	SetValues(paValues,size);
}

void CTCDongleData::SetValues(long *paValues,int size)
{
	m_aData.RemoveAll();
	for(int i=0;i<size;++i)
	{
		m_aData.Add(*((BYTE*)(paValues+i)));
		m_aData.Add(*((BYTE*)(paValues+i)+1));
		m_aData.Add(*((BYTE*)(paValues+i)+2));
		m_aData.Add(*((BYTE*)(paValues+i)+3));
	}
	// Decodieren
	Encode();
}

void CTCDongleData::SetValues(BYTE *paValues,int size)
{
	m_aData.RemoveAll();
	for(int i=0;i<size;++i)
		m_aData.Add(*(paValues+i));
	// Decodieren
	Encode();
}

void CTCDongleData::GetValues(long *paValues,int size)
{
	// Codieren
	Encode();
	for(int i=0;i<size;++i)
	{
		*(((BYTE *)(paValues+i)+0))=m_aData[i*4+0];
		*(((BYTE *)(paValues+i)+1))=m_aData[i*4+1];
		*(((BYTE *)(paValues+i)+2))=m_aData[i*4+2];
		*(((BYTE *)(paValues+i)+3))=m_aData[i*4+3];
	}
	// Decodieren
	Encode();
}

void CTCDongleData::GetValues(BYTE *paValues,int size)
{
	// Codieren
	Encode();
	for(int i=0;i<m_aData.GetSize();++i)
		*(paValues+i) = m_aData[i];
	// Decodieren
	Encode();
}

void CTCDongleData::SetDongleID(long dongleID)
{
	memcpy(m_aData.GetData(),&dongleID,4);
}

void CTCDongleData::GetDongleID(long *pDongleID)
{
	memcpy(pDongleID,m_aData.GetData(),4);
}

void CTCDongleData::GetLicence(short *pLicGrp,short  *pLicType)
{
	memcpy(pLicGrp,m_aData.GetData()+4,2);
	memcpy(pLicType,m_aData.GetData()+6,2);
}

void CTCDongleData::SetLicence(short  licGrp,short licType)
{
	memcpy(m_aData.GetData()+4,&licGrp,2);
	memcpy(m_aData.GetData()+6,&licType,2);
}

void CTCDongleData::GetLicenceTypeValue(long *pLicTypeValue)
{
	memcpy(pLicTypeValue,m_aData.GetData()+8,4);
}

void CTCDongleData::SetLicenceTypeValue(long licTypeValue)
{
	memcpy(m_aData.GetData()+8,&licTypeValue,4);
}

void CTCDongleData::GetUpdate(long *pMajorId,long *pMinorId)
{
	memcpy(pMajorId,m_aData.GetData()+12,4);
	memcpy(pMinorId,m_aData.GetData()+16,4);
}

void CTCDongleData::SetUpdate(long majorId,long minorId)
{
	memcpy(m_aData.GetData()+12,&majorId,4);
	memcpy(m_aData.GetData()+16,&minorId,4);
}

void CTCDongleData::Clear()
{
	m_aData.RemoveAll();
	long aValues[16];
	memset(aValues,0x0,sizeof(aValues));
	SetValues(aValues,16);
}

void CTCDongleData::Encode()
{
   for(int i=0;i<m_aData.GetSize();i++)
	   m_aData[i] ^= ((i*13)^0x86)%255;
}
/* ************************************************************************* */

/* ***************************** CTCDongleHandler ************************** */
CTCDongleHandler::CTCDongleHandler()
{
}

void CTCDongleHandler::GetLicence(short *pLicGroup,short *pLicType)
{
	m_dongleData.GetLicence(pLicGroup,pLicType);
}

void CTCDongleHandler::SetLicence(short licGroup,short licType)
{
	m_dongleData.SetLicence(licGroup,licType);
}

void CTCDongleHandler::GetLicenceTypeValue(long *pLicTypeValue)
{
	m_dongleData.GetLicenceTypeValue(pLicTypeValue);
}

void CTCDongleHandler::SetLicenceTypeValue(long licTypeValue)
{
	m_dongleData.SetLicenceTypeValue(licTypeValue);
}

void CTCDongleHandler::GetUpdate(long *pMajorId,long *pMinorId)
{
	m_dongleData.GetUpdate(pMajorId,pMinorId);
}

void CTCDongleHandler::SetUpdate(long majorId,long minorId)
{
	m_dongleData.SetUpdate(majorId,minorId);
}

BOOL CTCDongleHandler::ReadLicenceFile(const char *fileName,long *pDongleID) 
{
	ifstream	stream;
	// Datei öffnen
	stream.open(fileName, ios::in | /*ios::nocreate |*/ ios::binary);

	if (strlen(fileName)==0 || !stream)
		return FALSE;

	stream.seekg(0L, ios::beg);

	BYTE aInput[64];
	stream.read((char *) &aInput[0],64);

	if (stream.gcount()!=64)
		return FALSE;

	m_dongleData.SetValues(aInput,64);

	m_dongleData.GetDongleID(pDongleID);

	stream.close();

	return TRUE;
}

BOOL CTCDongleHandler::WriteLicenceFile(const char *fileName) 
{
	ofstream stream;

	// Datei öffnen
	stream.open(fileName,ios::out | ios::binary | ios::trunc);

	if (!stream)
		return FALSE;

	BYTE aInput[64];
	m_dongleData.GetValues(aInput,64);
	
	stream.write((char *) &aInput[0],64);

	stream.close();

	return TRUE;
}

void CTCDongleHandler::SetDongleID(long dongleId)
{
	m_dongleData.SetDongleID(dongleId);
}

/* ************************************************************************* */
