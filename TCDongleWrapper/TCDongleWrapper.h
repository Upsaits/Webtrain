// TCDongleWrapper.h

#pragma once

using namespace System;

namespace SoftObject {
	namespace TrainConcept {

	public ref class TCDongleHandler
	{
	private:
		class CTCDongleHandler *m_oDongleHandler;

	public:
		void Init();
		void Exit();
		void GetLicence(short %licGroup,short %licType);
		void SetLicence(short licGroup,short licType);
		void GetLicenceTypeValue(long %licTypeValue);
		void SetLicenceTypeValue(long licTypeValue);
		void GetUpdate(long %majorId,long %minorId);
		void SetUpdate(long majorId,long minorId);
		bool ReadLicenceFile(String ^fileName,long %dongleID);		
		bool WriteLicenceFile(String ^fileName);
		void SetDongleID(long dongleId);	
		bool DecodeDate(String ^str,DateTime %date);
		void EncodeDate(DateTime date,String ^str);
	};

	}
}
