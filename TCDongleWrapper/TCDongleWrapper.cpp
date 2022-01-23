// This is the main DLL file.

#include "stdafx.h"

#include "TCDongleHandler.h"
#include "TCDongleWrapper.h"

#include "msclr/marshal_cppstd.h"

namespace SoftObject {
	namespace TrainConcept {

	void TCDongleHandler::Init()
	{
		m_oDongleHandler = new CTCDongleHandler();
	}

	void TCDongleHandler::GetLicence(short %licGroup,short %licType)
	{
		short _licGroup=0,_licType=0;
		if (m_oDongleHandler!=nullptr)
			m_oDongleHandler->GetLicence(&_licGroup,&_licType);
		licGroup = _licGroup;
		licType = _licType;
	}

	void TCDongleHandler::Exit()
	{
		delete m_oDongleHandler;
	}

	void TCDongleHandler::SetLicence(short licGroup,short licType)
	{
		if (m_oDongleHandler!=nullptr)
			m_oDongleHandler->SetLicence(licGroup,licType);
	}

	void TCDongleHandler::GetLicenceTypeValue(long %licTypeValue)
	{
		long _licTypeValue=0;
		if (m_oDongleHandler!=nullptr)
			m_oDongleHandler->GetLicenceTypeValue(&_licTypeValue);
		licTypeValue = _licTypeValue;
	}

	void TCDongleHandler::SetLicenceTypeValue(long licTypeValue)
	{
		if (m_oDongleHandler!=nullptr)
			m_oDongleHandler->SetLicenceTypeValue(licTypeValue);
	}

	void TCDongleHandler::GetUpdate(long %majorId,long %minorId)
	{
		long _majorId=0,_minorId=0;
		if (m_oDongleHandler!=nullptr)
			m_oDongleHandler->GetUpdate(&_majorId,&_minorId);
		majorId = _majorId;
		minorId = _minorId;

	}

	void TCDongleHandler::SetUpdate(long majorId,long minorId)
	{
		if (m_oDongleHandler!=nullptr)
			m_oDongleHandler->SetUpdate(majorId,minorId);
	}

	bool TCDongleHandler::ReadLicenceFile(String ^fileName,long %dongleID)
	{
		long _dongleId=0;
		if (m_oDongleHandler!=nullptr)
		{
			if (m_oDongleHandler->ReadLicenceFile(msclr::interop::marshal_as<std::string>(fileName).c_str(),&_dongleId))
			{
				dongleID = _dongleId;
				return true;
			}

		}
		return false;
	}

	bool TCDongleHandler::WriteLicenceFile(String ^fileName)
	{
		if (m_oDongleHandler!=nullptr)
			return m_oDongleHandler->WriteLicenceFile(msclr::interop::marshal_as<std::string>(fileName).c_str())==TRUE;
		return false;
	}

	void TCDongleHandler::SetDongleID(long dongleId)
	{
		if (m_oDongleHandler!=nullptr)
			m_oDongleHandler->SetDongleID(dongleId);
	}
	
	bool TCDongleHandler::DecodeDate(String ^str,DateTime %date)
	{
		/*
		String[] aTokens=str.Split(new char[] {' '},8);

		if (aTokens.Length!=8)
			return false;

		char [] aChars = new Char[8];
		int  i;
		for(i=0;i<aTokens.Length;++i)
		{
			byte b=Convert.ToByte(aTokens[i]);
			b ^= (byte)(((i*13)^0x86)%255);
			aChars[i]=Convert.ToChar(b);
		}
		string sDate=new string(aChars);
		date=new DateTime(Int32.Parse(sDate.Substring(4,4)),Int32.Parse(sDate.Substring(2,2)),Int32.Parse(sDate.Substring(0,2)));*/
		return true;
	}

	void TCDongleHandler::EncodeDate(DateTime date,String ^str)
	{
		String ^l_str=date.ToString("ddMMyyyy");
		for(int i=0;i<l_str->Length;++i)
		{
			byte b=Convert::ToByte(Convert::ToByte(l_str[i]) ^ ((i*13)^0x86)%255);
			str+=String::Format("{0} ",b);
		}
	}



	}
}

