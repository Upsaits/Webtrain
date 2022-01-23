#ifndef profile_h
#define profile_h

/**
 *  CProfileHandler
 *  Klasse zur vereinfachten Handhabung von INI-Dateien
*/

const	CString	PROF_SYS_ON=					"ON";
const	CString	PROF_SYS_OFF=					"OFF";
const	CString PROF_SYS_COM1=					"COM1";
const	CString PROF_SYS_COM2=					"COM2";
const	CString PROF_SYS_COM3=					"COM3";
const	CString PROF_SYS_COM4=					"COM4";

class CProfileHandler: public CObject
{
	// ----------------------- Attribute -------------------------
	public:
		/**  PortType.
		  *  Typ des Seriellen Anschlusses 
		*/
		enum PortType{
						COM1,	/**< COM1 */
						COM2,	/**< COM2 */
						COM3,	/**< COM3 */
						COM4	/**< COM4 */
					};
	
	// ----------------------- Attribute -------------------------
	protected:
		CString		m_iniFileName;	// Dateiname der INI-Datei

	// ----------------------- Methoden --------------------------
	public:
		CProfileHandler() {};
		CProfileHandler(const CString &iniFileName);

	public:
		void		SetINIFileName(const CString &iniFileName) {m_iniFileName=iniFileName;};
		BOOL		IsOn(CString str);
		BOOL		IsSerialPort(CString str);
		void		StringToPortSettings(CString str,int *aSettings,int count=4);

	public:
		CString		GetProfileString(CString section,CString key,CString defKey="");
		int			GetProfileInt(CString section,CString key, int defKey=0);
		double		GetProfileFloat(CString section,CString key, double defVal=0);
		UINT 		GetProfileKeys(CString section,CStringArray &aSystems);
		BOOL		WriteProfileString(CString section,CString key,CString value);
		BOOL		WriteProfileFloat(CString section,CString key,
									  double val, CString format = "%15.8f");
		BOOL		WriteProfileInt(CString section,CString key,int value);

		CSize 		StringToSize(CString str);
		CPoint 		StringToPoint(CString str);
		void		StringToRect(CString str,CRect &rect);
		CString		SizeToString(CSize size);
		CString		PointToString(CPoint point);
		CString		RectToString(CRect rect);
		BOOL		IsAbsolutePath(CString path);

		CString		Encode(CString str);
		CString		Decode(CString str);
};

#endif
/////////////////////////////////////////////////////////////////////////////



