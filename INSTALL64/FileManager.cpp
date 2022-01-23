/****************************************************************************
 * IMPLEMENTIERUNG der FileManager Klasse
 ****************************************************************************
 * Modulname:  FileManager.cpp
 ****************************************************************************
 * Funktionsbeschreibung:
 *
 *  Dieses Modul enthält die Klassen zum Handling der Files 
 *
 ****************************************************************************
 *===========================================================================
 * Erstellungsdatum:
 *===========================================================================
 * Änderungen:
 *==========================================================================*/
#include "stdafx.h"

#include "FileManager.h"


//////////////////////////////////////////////////////////////////////
// Konstruktion/Destruktion
//////////////////////////////////////////////////////////////////////
CFileManager::CFileManager()
{

}

CFileManager::~CFileManager()
{

}

////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::CreateFile(const CString &newFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(newFilePath,FALSE))//kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;

	if(ExistFile(newFilePath)) //Source File vorhanden ?
		return FALSE;

	CFile file;	
	CString strFilePath=GetDir(newFilePath);
	CreateDir(strFilePath); //dir zuvor erzeugen 
	BOOL bReturn=file.Open(newFilePath,CFile::modeCreate);
	if(bReturn)
		file.Close();
	return(bReturn);
}

////////////////////////////////////////////////////////////////////////////////
int	CFileManager::CountFiles (CString strDir)
////////////////////////////////////////////////////////////////////////////////
{
	AddSlashAtEnd(strDir);

	//Suchstring inkl. Fileextension 
	CString searchFilePath=strDir + "*.*";
	
	CFileFind ff;
	BOOL bWorking=ff.FindFile(searchFilePath);
	int iFileCount=0;
	//zuerst alle Subdirektories inkl. Files löschen 	
	while (bWorking)
	{
		bWorking=ff.FindNextFile();
		if(ff.IsDots())// || ff.IsDirectory())
			continue;
		iFileCount++;
	}//while
	return iFileCount;
}

////////////////////////////////////////////////////////////////////////////////
CString CFileManager::GetDir(const CString &newFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	int iSlashIndex=newFilePath.ReverseFind('\\');
	return(newFilePath.Left(iSlashIndex +1));
}

////////////////////////////////////////////////////////////////////////////////
CString CFileManager::GetName (CString newFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	int iSlashIndex=newFilePath.ReverseFind('\\');
	//befindet sich der gefunden Slash NICHT am Ende des Strings
	//so ist das ein Hinweis, daß ein FileName dem Pfad übergeben wurde 
	// z.B.: 'C:\TEST\hugo.txt
	if(iSlashIndex != (newFilePath.GetLength() -1))
		return(newFilePath.Mid(iSlashIndex +1));
	else 
		//Position des Slash entspricht der Länge des Strings z.B.: 'C:\TEST\'
		//das ist dann Directorie 
		return (CString(""));
}

////////////////////////////////////////////////////////////////////////////////
CString CFileManager::GetNameOfFile (const CString &strFile)
////////////////////////////////////////////////////////////////////////////////
{//gibt von einem File 'test.cpp' den Namen zurück z.B. 'test'

	int iDotIndex=strFile.Find('.'); // DOT !!
	if(iDotIndex !=  -1) 
		//Dot gefunden 		
		return(strFile.Left(iDotIndex));
	else 
		return strFile;	
}

////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::DeleteFile(const CString &delFilePath, BOOL p_bDelFromNetworkDrive)
////////////////////////////////////////////////////////////////////////////////
{//TODO: eventuell auch Wildcards beim Löschen zulassen 
//	if(!IsSyntaxOkay(delFilePath,FALSE))	//kein WildCard bei der Filespezifikation erlaubt ! 
//		return FALSE;						wird in IsNetworkDrive ohnehin abgefragt
	if(/*!ExistFile(delFilePath) ||*/ !p_bDelFromNetworkDrive && IsNetworkDrive(delFilePath))
		return FALSE;
	ResetAttribute(delFilePath);
	return ::DeleteFile((LPCTSTR)delFilePath); //löschen 
//	return(!ExistFile((LPCTSTR)delFilePath));
}

////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::CopyFile(const CString &srcFilePath, const CString &targetFilePath)
////////////////////////////////////////////////////////////////////////////////
{//TODO: eventuell auch Wildcards beim Kopieren zulassen 
	
	if(!IsSyntaxOkay(srcFilePath,FALSE))//kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;

	if(!IsSyntaxOkay(targetFilePath))
		return FALSE;
	
	if(!ExistFile(srcFilePath)) //Source File vorhanden ?
		return FALSE;

	CString targetPath= GetDir(targetFilePath);
	CreateDir(targetPath); // target Dir gegebenfalls anlegen 
	if(ExistFile(targetFilePath))
		SetFileReadonly(targetFilePath,FALSE); //das Attribute 'readOnly' removen		

	//Hinweis zum 3. Parameter von ::CopyFile(..)
	//Specifies how this operation is to proceed if a file of the same name as that 
	//specified by lpNewFileName already exists. 
	//If this parameter is TRUE and the new file already exists, the function fails. 
	//If this parameter is FALSE and the new file already exists, the function 
	//overwrites the existing file and succeeds. 
	return(::CopyFile((LPCTSTR)srcFilePath,(LPCTSTR)targetFilePath,FALSE));
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::CopyAllFiles (const CString &srcFilePath, const CString &targetFilePath)
////////////////////////////////////////////////////////////////////////////////
//	zum Kopieren einzelner oder mehrerer Files 
//
//	srcFilePath: Pfad der Quelldatei ev. mit Wildcards
//	targetFilePath: Pfad zur Zieldatei oder wenn Wildcards verwerdet wurden zum Zielverzeichnis
//
{
	if (srcFilePath.Find ('*') >= 0)			//	mehrere Files kopieren
	{
		CreateDir (targetFilePath);				//	Target Dir gegebenfalls anlegen 

		CFileFind FF;
		if (FF.FindFile (srcFilePath))
		{
			int nBSl = srcFilePath.ReverseFind ('\\');
			CString sSrcPath = srcFilePath.Left (nBSl + 1);

			BOOL bWeiter;
			do
			{
				bWeiter = FF.FindNextFile ();
				if (!FF.IsDots ())
				{
					CString sName = FF.GetFileName (); 
					CString sSource = sSrcPath + sName;
					CString sTarget = targetFilePath + sName;

					if (FF.IsDirectory ())
					{							// rekursiver Aufruf für Verzeichnisse
						if (!CopyAllFiles (sSource + "\\*.*", sTarget + "\\"))
						{
							FF.Close ();
							return false;
						}
					}
					else
					{
						if (ExistFile (sTarget))
						{
							SetFileReadonly (sTarget, FALSE);	//	das Attribute 'readOnly' removen
							SetFileHidden (sTarget, FALSE);		//	das Attribute 'Hidden' removen
						}		
						if (!::CopyFile ((LPCTSTR) sSource, (LPCTSTR) sTarget, FALSE))
						{	
							FF.Close ();
							return false;
						}
					}
				}
			}
			while (bWeiter);
		}
		FF.Close ();
		return true;
	}
	else
	{
		CString targetPath = GetDir (targetFilePath);
		CreateDir (targetPath);					//	Target Dir gegebenfalls anlegen 

		if (ExistFile (targetFilePath))
		{
			SetFileReadonly (targetFilePath, FALSE);			//	das Attribute 'readOnly' removen
			SetFileHidden (targetFilePath, FALSE);				//	das Attribute 'Hidden' removen
		}		
		return (::CopyFile ((LPCTSTR) srcFilePath, (LPCTSTR) targetFilePath, FALSE));
	}
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::RenameFile(const CString &oldFilePath, const CString &newFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	if(ExistFile(newFilePath))
		return FALSE;

	if(!IsSyntaxOkay(oldFilePath,FALSE))	//	kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;
	if(!IsSyntaxOkay(newFilePath,FALSE))	//	kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;
	
	//Prüfung:Beide Pfade müssen ident sein !
	CString oldDir=GetDir(oldFilePath);
	CString newDir=GetDir(newFilePath);
	VERIFY (oldDir == newDir);
	
	if(IsFileReadonly(oldFilePath))
		SetFileReadonly(oldFilePath,FALSE);
	CFile::Rename(oldFilePath,newFilePath);
	return (ExistFile(newFilePath));
}



////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::ExistFile(const CString & strFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;
/*
	CFile file;
	BOOL bReturn=file.Open(strFilePath,CFile::modeRead);
	if(bReturn)
		file.Close();
	return(bReturn);
*/	
	CFile file;
	CFileStatus	fileStatus;
	return (file.GetStatus(strFilePath, fileStatus));
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::IsNetworkDrive (const CString &strDir)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strDir,FALSE)) //kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;

	return (GetDriveType (strDir.Left (3)) == 4);
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::IsSyntaxOkay(const CString &strFilePath, BOOL bWildCard/*TRUE*/)
////////////////////////////////////////////////////////////////////////////////
{//die WildCard Einstellung ist natürlich nur für die Files bestimmt
	//Folgende Fehler werden abgefangen
	// 1: C:\TES*\hugo.txt
	// 2: C:\TEST\h*?.txt
	// 3: C:\TEST\.txt
	// 4: C:\TEST\txt.  <ein Punkt am Ende des Files>

	//Prüfung des Pfades 
	CString strDir=GetDir(strFilePath);	
	if(strDir.FindOneOf("*?") != -1)
		return FALSE;
	
	//Prüfung des Filenamens 
	BOOL bAsterix,bQuestionMark;
	bAsterix=bQuestionMark=FALSE;
	CString strName=GetName(strFilePath);	
	int iFileLength;
	int iPos;
	iFileLength=strName.GetLength();
	if(iFileLength)
	{
		iPos=strName.Find('.');
		if(iPos != -1)		
		{//Filetrenner gefunden
			if(iPos == 0) //Filepunkt steht am Filebeginn z.B.C:\TEST\.txt
				return FALSE;

			if(iPos ==(iFileLength-1)) //Filepunkt steht am FileEnde 
				return FALSE;//C:\TEST\txt.  <Punkt am Ende>
		}

		if(strName.Find('*') != -1)
			bAsterix=TRUE;
		if(strName.Find('?') != -1)
			bQuestionMark=TRUE;

		if(!bWildCard && (bAsterix==TRUE || bQuestionMark==TRUE)) 
			return FALSE;

		if(bAsterix==TRUE && bQuestionMark== TRUE) 
			return FALSE;//beides gleichzeitig ist nicht erlaubt 

	}
		return TRUE;
}

////////////////////////////////////////////////////////////////////////////////
BYTE CFileManager::GetAttribute(const CString &strFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard erlaubt ! 
		return 0;
	if(!ExistFile(strFilePath))	// Nur wenn das File existiert Attribute umsetzen
		return 0;
	CFileStatus fileStatus;
	CFile::GetStatus(LPCTSTR(strFilePath),fileStatus);
	return((BYTE)fileStatus.m_attribute);
}



////////////////////////////////////////////////////////////////////////////////
void CFileManager::SetAttribute(CString strFilePath, BYTE attribute)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard erlaubt ! 
		return ;
	if(!ExistFile(strFilePath))	// Nur wenn das File existiert Attribute umsetzen
		return;
	CFileStatus fileStatus;
	CFile::GetStatus(LPCTSTR(strFilePath),fileStatus);
	fileStatus.m_mtime = 0;	// Zeitstempel nicht verändern!
	
	fileStatus.m_attribute |= attribute; //mit neuen Attributen versehen 
	RemoveSlashAtEnd(strFilePath);
	CFile::SetStatus(LPCTSTR (strFilePath),fileStatus);	
}

////////////////////////////////////////////////////////////////////////////////
void CFileManager::ResetAttribute(CString strFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard erlaubt ! 
		return ;
	if(!ExistFile(strFilePath))	// Nur wenn das File existiert Attribute umsetzen
		return ;
	CFileStatus fileStatus;
	CFile::GetStatus(LPCTSTR(strFilePath),fileStatus);
	fileStatus.m_mtime = 0;	// Zeitstempel nicht verändern!
	
	fileStatus.m_attribute = (BYTE)FILE_ATTRIBUTE_NORMAL;
	RemoveSlashAtEnd(strFilePath);
	CFile::SetStatus(LPCTSTR (strFilePath),fileStatus);	
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::IsFileHidden(const CString &strFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard erlaubt ! 
		return FALSE;

	CFileStatus fileStatus;
	CFile::GetStatus(LPCTSTR(strFilePath),fileStatus);
	return(fileStatus.m_attribute & CFile::Attribute::hidden ? TRUE :FALSE);
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::SetFileHidden(const CString &strFilePath, BOOL bHidden /*TRUE*/)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard erlaubt ! 
		return FALSE; 

	BOOL bWasReadOnly=FALSE;
	CFileStatus fileStatus;
	CFile::GetStatus(LPCTSTR(strFilePath),fileStatus);
	fileStatus.m_mtime = 0;	// Zeitstempel nicht verändern!
	
	//zuerst das ReadOnly Flag umlegen 
	if((fileStatus.m_attribute) & (CFile::Attribute::readOnly))
	{
		bWasReadOnly=TRUE;		
		fileStatus.m_attribute &= ~ (CFile::Attribute::readOnly);
		CFile::SetStatus(LPCTSTR (strFilePath),fileStatus);	
	}

	if(bHidden)
		fileStatus.m_attribute |= CFile::Attribute::hidden;
	else 
		fileStatus.m_attribute &= ~CFile::Attribute::hidden;
	
	CFile::SetStatus(LPCTSTR (strFilePath),fileStatus);	
	
	if(bWasReadOnly)
	{
		fileStatus.m_attribute |= CFile::Attribute::readOnly;
		CFile::SetStatus(LPCTSTR (strFilePath),fileStatus);	
	}
	return TRUE;
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::IsFileReadonly(const CString &strFilePath)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard erlaubt ! 
		return FALSE;

	CFileStatus fileStatus;
	CFile::GetStatus(LPCTSTR(strFilePath),fileStatus);
	return(fileStatus.m_attribute & CFile::Attribute::readOnly ? TRUE :FALSE);
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::SetFileReadonly(const CString &strFilePath, BOOL bReadonly/*TRUE*/)
////////////////////////////////////////////////////////////////////////////////
{
/*
	DWORD dwFileAttributes=::GetFileAttributes(LPCTSTR(strFilePath));
	
	if(bReadonly)
		dwFileAttributes |=  FILE_ATTRIBUTE_READONLY;
	else 
		dwFileAttributes &= ~ FILE_ATTRIBUTE_READONLY;

	::SetFileAttributes(LPCTSTR(strFilePath),dwFileAttributes);
*/
	if(!IsSyntaxOkay(strFilePath,FALSE)) //kein WildCard erlaubt ! 
		return FALSE;

	CFileStatus fileStatus;
	CFile::GetStatus(LPCTSTR(strFilePath),fileStatus);
	fileStatus.m_mtime = 0;	// Zeitstempel nicht verändern!
	
	if(bReadonly)
		fileStatus.m_attribute |= FILE_ATTRIBUTE_READONLY;
	else 
		fileStatus.m_attribute &= ~FILE_ATTRIBUTE_READONLY;

	CFile::SetStatus(LPCTSTR (strFilePath),fileStatus);	
	return TRUE;
}


// *******************************************************************************

////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::CreateDir (CString strDir)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strDir,FALSE)) //kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;
	VERIFY(strDir.GetLength() > 3);	//root Verzeichnis ist zuwenig 	
	AddSlashAtEnd(strDir);
	
	BOOL bReturn;
	DWORD dLastError;
	CString strSubDir;
	int iSlashIndex=0;

	//nach dem ersten Auftreten eines Slash suchen
	iSlashIndex=strDir.Find('\\',iSlashIndex);
	while(iSlashIndex != -1)
	{//die Subdirectories extrahieren 
		strSubDir=strDir.Left(iSlashIndex +1);
		bReturn=::CreateDirectory(strSubDir,NULL);
	
		if(!ExistDir(strSubDir))
		{
			if(!bReturn)
			{	
				dLastError=::GetLastError();
				if(dLastError != ERROR_ALREADY_EXISTS)
					return FALSE;
			}
		}
		iSlashIndex=strDir.Find('\\',iSlashIndex+1);
	}


	if(iSlashIndex)
	{	//zum Schluß den ganzen Pfad anlegen z.B: 'C:\HUGO\TEST
		//im oberen Teil wurde nur bis 'C:\HUGO' angelegt 
		bReturn=::CreateDirectory(strDir,NULL);	 //gesamtes Verzeichnis !
		if(!bReturn)
		{
			dLastError=::GetLastError();
			if(dLastError != ERROR_ALREADY_EXISTS)
				return FALSE;
		}
	}
	return TRUE;
}


////////////////////////////////////////////////////////////////////////////////
//BOOL CFileManager::DeleteDir	(CString strDir, 
//								 BOOL bDelTotalPath /*TRUE*/,
//								 fileDelTyp fdt/*fdtAll*/)
////////////////////////////////////////////////////////////////////////////////
/*
{
	
	if(!IsSyntaxOkay(strDir,FALSE)) //kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;

	if(!strDir.GetLength())
		return FALSE;  //da ist ewas oberfaul
	// --------------------
	
	//Prüfung:Es darf kein Laufwerksverzeichnis alleine übergeben werden 	
	VERIFY(strDir.GetLength() > 3);
	AddSlashAtEnd(strDir);

	//Suchstring inkl. Fileextension 
	CString searchFilePath=strDir + "*.*";


	LPWIN32_FIND_DATA lpFindFileData;
	HANDLE hFindFile = ::FindFirstFile((LPCTSTR)searchFilePath,lpFindFileData);
	if(hFindFile ==INVALID_HANDLE_VALUE)
		return FALSE;

	
	//zuerst alle Subdirektories inkl. Files löschen 	
	while (1)
	{
		if(!(::FindNextFile	(hFindFile,lpFindFileData)))
			break; //aus maus mit der gaudi
	
		//Verzeichnis gefunden 
		if(lpFindFileData->dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{
			int iLength	= strlen(lpFindFileData->cFileName);
			// Verzeichnis ist ein \.  oder \..
			if(lpFindFileData->cFileName[iLength-1]== '.' ||
				lpFindFileData->cFileName[iLength-2]== '.')
					continue; //nächstes File holen 
	
			//evtl. löschen
			if(fdt==fdtAll || fdt==fdtJustDir)
			{
				DeleteDir(CString((LPCSTR)lpFindFileData->cFileName)); //rekursiver Aufruf 
				::RemoveDirectory((LPCTSTR)lpFindFileData->cFileName);
			}
		}
		else if(fdt==fdtAll || fdt==fdtJustFiles)			
		{
			ResetAttribute(CString((LPCSTR)lpFindFileData->cFileName));			
			CFile::Remove((LPCSTR)lpFindFileData->cFileName);
		}
	}//while

	::FindClose(hFindFile);
	DWORD dLastError=::GetLastError();		
	//abschließende Aktion:jetzt kann das eigentliche Verzeichnis gelöscht werden 
	if(bDelTotalPath)
	{		
		//::SetFileAttributes((LPCTSTR)strDir,FILE_ATTRIBUTE_NORMAL);
		//return(::RemoveDirectory((LPCTSTR)strDir));
		//ResetAttribute(strDir);		
	//	::RemoveDirectory((LPCTSTR)strDir);	
		dLastError=::GetLastError();		
	}
	return TRUE;
}

*/

////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::DeleteDir	(CString strDir, 
								 BOOL bDelTotalPath /*TRUE*/,
								 fileDelTyp fdt/*fdtAll*/)
////////////////////////////////////////////////////////////////////////////////
{

	if(!IsSyntaxOkay(strDir,FALSE)) //kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;

	if(!strDir.GetLength())
		return FALSE;  //da ist ewas oberfaul
	// --------------------
	//Prüfung:Es darf kein Laufwerksverzeichnis alleine übergeben werden 	
	VERIFY(strDir.GetLength() > 3);
	AddSlashAtEnd(strDir);
	//Suchstring inkl. Fileextension 
	CString searchFilePath=strDir + "*.*";

	CString strFilePath,strRekPath;		
	CFileFind ff;
	BOOL bWorking=ff.FindFile(searchFilePath);
	
	//zuerst alle Subdirektories inkl. Files löschen 	
	while (bWorking)
	{
		bWorking=ff.FindNextFile();
		if(ff.IsDots())
			continue;

		if(ff.IsDirectory())
		{			
			if(fdt==fdtAll || fdt==fdtJustDir)
			{
				DeleteDir(ff.GetFilePath()); //rekursiver Aufruf 
				::RemoveDirectory((LPCTSTR)ff.GetFilePath());
			}
		}
		else if(fdt==fdtAll || fdt==fdtJustFiles)			
		{
			strFilePath=ff.GetFilePath();
			ResetAttribute(strFilePath);			
			::DeleteFile((LPCTSTR)strFilePath);
		}
	}//while

	//Suchen beenden 
	ff.Close();	

	//abschließende Aktion:jetzt kann das eigentliche Verzeichnis gelöscht werden 
	if(bDelTotalPath && ExistDir(strDir))
		::RemoveDirectory((LPCTSTR)strDir);	
	return TRUE;
}

	
////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::CopyDir (CString srcDir, CString targetDir, BOOL bIncludeSrcSubDir /*FALSE*/)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(srcDir,FALSE) || !IsSyntaxOkay(targetDir,FALSE)) //kein WildCard erlaubt ! 
		return FALSE;
	VERIFY(srcDir.GetLength() > 3);	
	VERIFY(targetDir.GetLength() > 3);	
	AddSlashAtEnd(srcDir);
	AddSlashAtEnd(targetDir);

	if(srcDir == targetDir)
		return TRUE;

	int iOldDirSlashCount,iNewDirSlashCount;
	iOldDirSlashCount=iNewDirSlashCount=0;
	// -------------		
	//Prüfung:Die Anzahl der Slashes muß ident sein, ansonst Fehler  
	for(int i=0;i<srcDir.GetLength();i++)
	{
		if(srcDir[i] == '\\') 
			iOldDirSlashCount ++;
	}
	for(int i=0;i<targetDir.GetLength();i++)
	{
		if(targetDir[i] == '\\') 
			iNewDirSlashCount ++;
	}
	VERIFY(iNewDirSlashCount == iOldDirSlashCount);

	//-----------------------
	//neues Directory anlegen 
	CreateDir(targetDir);

	//-----------------------
	//Files kopieren 
	//Suchstring inkl. Fileextension 
	CString searchFilePath=srcDir + "*.*";

	CString strSrcFilePath, strFileName,strTargetFilePath,strSubDir;		
	CFileFind ff;
	
	BOOL bWorking=ff.FindFile(searchFilePath);

	while (bWorking)
	{
		bWorking=ff.FindNextFile();
		if(ff.IsDots())// || ff.IsDirectory())
			continue;

		//Sonderbehandlung der SubDirectories 
		if(ff.IsDirectory())
		{
			if(bIncludeSrcSubDir)
			{
				strSrcFilePath=ff.GetFilePath(); // 'c:\myhtml\myfile'  d.h. <Dir> 
				strSubDir=ff.GetFileName();		//damit wird der SubDir Name herausgezogen 
				strTargetFilePath=targetDir +  strSubDir;//das neue Zieldirectory zusammenbauen
				//rekursiver Aufruf 
				CopyDir(strSrcFilePath,strTargetFilePath,TRUE);
			}
			else 
				continue;
		}
		
		//ab hier gibt es nur mehr Files 
		strSrcFilePath=ff.GetFilePath(); // 'c:\myhtml\myfile.txt' 
		strFileName=ff.GetFileName(); // 'myfile.txt'
		strTargetFilePath=targetDir + strFileName;
		::CopyFile( (LPCTSTR) strSrcFilePath,(LPCTSTR) strTargetFilePath,FALSE);
	}//while
	return TRUE;
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::RenameDir	(CString oldDir, CString newDir)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(oldDir,FALSE) || !IsSyntaxOkay(newDir,FALSE)) //kein WildCard erlaubt ! 
		return FALSE;
	VERIFY(oldDir.GetLength() > 3);	
	VERIFY(newDir.GetLength() > 3);	
	int iOldDirSlashCount,iNewDirSlashCount;
	iOldDirSlashCount=iNewDirSlashCount=0;

	//Prüfung:Die Anzahl der Slashes muß ident sein, ansonst Fehler  
	for(int i=0;i<oldDir.GetLength();i++)
	{
		if(oldDir[i] == '\\') 
			iOldDirSlashCount ++;
	}
	for(int i=0;i<newDir.GetLength();i++)
	{
		if(newDir[i] == '\\') 
			iNewDirSlashCount ++;
	}
	
	VERIFY(iNewDirSlashCount == iOldDirSlashCount);

	//kein Slash für die ::MoveFile() sonst funktioniert das Umbenenen nicht!! 
	RemoveSlashAtEnd(oldDir);
	RemoveSlashAtEnd(newDir);
	if(oldDir == newDir)
		return TRUE;
	
	return (::MoveFile((LPCTSTR) oldDir, (LPCTSTR) newDir));
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::ExistDir (CString strDir)
////////////////////////////////////////////////////////////////////////////////
{
	if(!IsSyntaxOkay(strDir,FALSE)) //kein WildCard bei der Filespezifikation erlaubt ! 
		return FALSE;

	RemoveSlashAtEnd(strDir);

	//RootAbfragen z.B. 'C:' wird abgefangen
	//denn das macht bei WinNT Probleme !!
	int iLength	= strDir.GetLength();
	if(iLength <= 2 && (strDir.GetAt(iLength-1)==':'))
		return TRUE;
	
	CFileFind ff;
	ff.FindFile(strDir);
	
	if (!ff.FindFile(strDir))
	{
		DWORD dLastError=::GetLastError();
		return FALSE;
	}
	ff.FindNextFile();
	return !((ff.GetFilePath()).CompareNoCase(strDir));
}


////////////////////////////////////////////////////////////////////////////////
BOOL CFileManager::HasDirSlashAtEnd(const CString &strDir)
////////////////////////////////////////////////////////////////////////////////
{
	int iSlashIndex = strDir.ReverseFind('\\');
	int iLength		= strDir.GetLength();
	if(iSlashIndex != -1)
	{//Slash gefunden 
		if((iLength-1) == iSlashIndex)
			return TRUE;//Länge des Strings muß mit dem 'FundOrt' des '\\' übereinstimmen
	}
	return FALSE;
}

/////////////////////////////////////////////////////////////////////////////
// METHODE: AddSlashAtEnd											protected
/////////////////////////////////////////////////////////////////////////////
CString &CFileManager::AddSlashAtEnd(CString &strDir)
{
	int iSlashIndex = strDir.ReverseFind('\\');
	int iLength		= strDir.GetLength();
	
	if(iSlashIndex != -1)
	{//Slash gefunden 
		if((iLength-1) != iSlashIndex)
			strDir += "\\";
	}
	else 
		strDir += "\\";

	return strDir;
}

/////////////////////////////////////////////////////////////////////////////
// METHODE: RemoveSlashAtEnd										protected
/////////////////////////////////////////////////////////////////////////////
CString	&CFileManager::RemoveSlashAtEnd(CString &strDir)
{
	int iSlashIndex = strDir.ReverseFind('\\');
	int iLength		= strDir.GetLength();
	if(iSlashIndex != -1)
	{//Slash gefunden 
		if((iLength-1) == iSlashIndex)
			strDir=strDir.Left(iSlashIndex); //kein +1
	}
	return strDir;
}

////////////////////////////////////////////////////////////////////////////////
CString  CFileManager::GetFileExtension(const CString &strFileName)
////////////////////////////////////////////////////////////////////////////////
{
	CString strReturn("");
	int iDotPos=strFileName.ReverseFind('.');
	if(iDotPos != -1) 
		strReturn=strFileName.Mid(iDotPos+1);
	return (strReturn);
}

////////////////////////////////////////////////////////////////////////////////
CString & CFileManager::AddFileExtension(CString & strFileName,const CString &strExt)
////////////////////////////////////////////////////////////////////////////////
{
	int iDotPos=strFileName.ReverseFind('.');
	int iLength=strFileName.GetLength();
	if(iDotPos != -1)
	{//Punkt gefunden 
		if(iLength > (iDotPos+1))
			return strFileName; //das File besitzt bereits eine Extension 
		else //File hat einen '.', aber noch eine Extension 
			strFileName += strExt;
	}
	else //keinen '.' gefunden 
	{
		if(strExt.GetLength()) //es gibt wirklich einen Extension 
			strFileName += "." + strExt;
	}
	return strFileName;
}

CString CFileManager::FindFile(const CString &sStartDir,const CString &fileName)
{
   CFileFind finder;

   // start working for files
   BOOL bWorking = finder.FindFile(sStartDir);

   while (bWorking)
   {
      bWorking = finder.FindNextFile();

      // skip . and .. files; otherwise, we'd
      // recur infinitely!
      if (finder.IsDots())
         continue;

      // if it's a directory, recursively search it

      if (finder.IsDirectory())
	  {
         CString pErg= FindFile(finder.GetFilePath()+"\\*.*",fileName);
		 if(!pErg.IsEmpty())
			 return pErg;
	  }

	  CString name=finder.GetFileName();
	  name.MakeLower();
	  if (name==fileName)
		  return finder.GetRoot();
   }

   finder.Close();
   return "";
}

CString CFileManager::FindDir(const CString &sStartDir,const CString &dirName,bool recursive/*=false*/)
{
   CFileFind finder;

   // start working for files
   BOOL bWorking = finder.FindFile(sStartDir);

   while (bWorking)
   {
      bWorking = finder.FindNextFile();

      // skip . and .. files; otherwise, we'd
      // recur infinitely!
      if (finder.IsDots())
         continue;

      if (finder.IsDirectory())
	  {	
		  CString name=finder.GetFileTitle();
		  if (name==dirName)
			  return finder.GetRoot();
		  else if(recursive)
		  {
			 // recursively search it
			 LPCTSTR pErg= FindFile(finder.GetFilePath()+"\\*.*",dirName);
			 if(!CString(pErg).IsEmpty())
				 return pErg;
		  }
	  }
   }

   finder.Close();
   return "";
}

CString CFileManager::FindFilesByExt(const CString &sStartDir,const CString &ext,bool bSearchSubDir/*=true*/)
{
	CFileFind finder;

	// start working for files
	BOOL bWorking = finder.FindFile(sStartDir);

	while (bWorking)
	{
		bWorking = finder.FindNextFile();

		// skip . and .. files; otherwise, we'd
		// recur infinitely!
		if (finder.IsDots())
			continue;

		// if it's a directory, recursively search it
		if (bSearchSubDir && finder.IsDirectory())
		{
			CString pErg= FindFilesByExt(finder.GetFilePath()+"\\*.*",ext,true);
			if(!pErg.IsEmpty())
				return pErg;
		}

		CString name=finder.GetFileName();
		name.MakeLower();
		if (GetFileExtension(name)==ext)
			return name;
	}

	finder.Close();
	return "";

}

/* *************************************************************************
	CFileManager fileManager;

	//--- Fileoperationen 
	BOOL retValue;
	//retValue=fileManager.CreateFile(CString("C:\\TEST\\hugo.txt"));
	//retValue=fileManager.DeleteFile(CString("C:\\TEST\\hugo.txt"));
	//retValue=fileManager.CopyFile  (CString("C:\\TEST\\hugo.txt"),CString("C:\\TESTKK\\XXXX.txt"));
	//retValue=fileManager.RenameFile(CString("C:\\TEST\\hugo.txt"),CString("C:\\TEST\\rename.txt"));	
	
	//--- Dir operationen 
	retValue=fileManager.CreateDir(CString("C:\\TESTSS\\"));
	//retValue=fileManager.DeleteDir(CString("C:\\TEST"));
	//retValue=fileManager.CopyDir  (CString("C:\\TEST"),CString("C:\\HUGO\\"),TRUE);	
	//retValue=fileManager.RenameDir(CString("C:\\TEST"),CString("C:\\ZIPF"));		
*/