#pragma once

/****************************************************************************
 * HEADER-DEFINITIONEN für die File Veränderungs Klasse
 ****************************************************************************
 * Modulname:  FileManager.h
 ****************************************************************************
 * Funktionsbeschreibung:
 *
 *  Dieses Modul enthält die Klassen zum Handling von Files 
 *
 ****************************************************************************
 *==========================================================================
 * Erstellungsdatum:
 *==========================================================================
 * Änderungen:
 *
 ***************************************************************************/


/////////////////////////////////////////////////////////////////////////////
// CFileHandling
/////////////////////////////////////////////////////////////////////////////
class CFileManager : public CFile
{

	// -----------------Konstruktor&Destruktor-------------------
	public:
		CFileManager();
		virtual ~CFileManager();

	// --- Daten ------------------------------
	public:
		enum fileDelTyp {fdtAll,fdtJustFiles,fdtJustDir	};


    // --- Methoden----------------------------
	protected:
		BOOL		IsSyntaxOkay(const CString &strFilePath, BOOL bWildCard=TRUE);
	public:

		CString		GetName (CString strFilePath); //ÜbergabeParameter ist ein Pfad 		
		CString		GetNameOfFile(const CString & strFile);//Übergabeparameter ist ein File
		BOOL 		CreateFile	(const CString &newFilePath);
		BOOL		DeleteFile	(const CString &delFilePath, BOOL = false);
		BOOL		CopyFile	(const CString &srcFilePath, const CString &targetFilePath);
		BOOL		CopyAllFiles (const CString &srcFilePath, const CString &targetFilePath);
		BOOL		RenameFile	(const CString &oldFilePath, const CString &newFilePath);
		BOOL		ExistFile	(const CString &strFilePath);

		CString		GetDir		(const CString &strDir);
		BOOL		CreateDir	(CString strDir);
		BOOL		DeleteDir	(CString strDir, BOOL bTotalDir=TRUE, fileDelTyp fdt=fdtAll);
		BOOL		CopyDir		(CString srcDir, CString targetDir, BOOL bIncludeSrcSubDir=FALSE);
		BOOL		RenameDir	(CString oldDir, CString newDir);
		BOOL		ExistDir	(CString strDir);
		BOOL		IsNetworkDrive (const CString &strDir);		
		BOOL		HasDirSlashAtEnd (const CString &strDir);
		int			CountFiles (CString strDir);
		
		//Attribute 
		BYTE		GetAttribute (const CString &strFilePath);
		void		SetAttribute (CString strFilePath, BYTE attribute);
		void		ResetAttribute(CString strFilePath);
		BOOL		IsFileHidden	(const CString &strFilePath);
		BOOL		IsFileReadonly	(const CString &strFilePath);		
		BOOL		SetFileReadonly	(const CString &strFilePath, BOOL bReadonly=TRUE);
		BOOL		SetFileHidden	(const CString &strFilePath, BOOL bHidden=TRUE);

		CString &   AddSlashAtEnd(CString &strDir);	
		CString	&	RemoveSlashAtEnd(CString &strDir);
		
		CString		GetFileExtension(const CString &strFileName);
		CString &   AddFileExtension(CString &strFileName, const CString &strExt);

		CString		FindFile(const CString &sStartDir,const CString &fileName);
		CString		FindFilesByExt(const CString& sStartDir, const CString& ext, bool bSearchSubDir=true);
		CString		FindDir(const CString &sStartDir,const CString &dirName,bool recursive=false);
};

