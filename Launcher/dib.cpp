#include "stdafx.h"

#include <windowsx.h>
#include <lzexpand.h>

IMPLEMENT_DYNCREATE(CDib,CObject);

CDib::CDib()
{
	Init();
}

CDib::CDib(char *fileName)
{
	Init();
	Create(fileName);
}


CDib::~CDib()
{
	if (m_pDib)
		GlobalFreePtr(m_pDib);
	delete m_pFileHeader;
}


void CDib::Init()
{
	m_pDib        = NULL;
	m_pFileHeader = new BITMAPFILEHEADER;
	m_size        = 0;
}


BOOL CDib::Load(char *fileName)
{
	if (m_pDib)
		GlobalFreePtr(m_pDib);

	return Create(fileName);
}


HBITMAP CDib::GetBitmap(CDC *pDC,HPALETTE palette)
{
	HBITMAP         newHandle=0;

	if (IsValid())
	{
		if (((UINT)palette) && SupportsPalette(pDC))
		{
			pDC->SelectPalette(CPalette::FromHandle(palette),FALSE);
			pDC->RealizePalette();
		}
		newHandle = ::CreateDIBitmap(pDC->m_hDC,(LPBITMAPINFOHEADER)InfoHeader(), CBM_INIT,(LPSTR) Bits(),(LPBITMAPINFO)InfoHeader(), DIB_RGB_COLORS);
	}
	return newHandle;
}



BOOL  CDib::IsValid()
{
	return (m_pDib != NULL);
}

LPBITMAPINFO CDib::Info()
{
	return ((LPBITMAPINFO) m_pDib);
}


LPBITMAPINFOHEADER CDib::InfoHeader()
{
	return ((LPBITMAPINFOHEADER) m_pDib);
}


LPBITMAPCOREHEADER CDib::CoreHeader()
{
	return ((LPBITMAPCOREHEADER) m_pDib);
}

BOOL  CDib::IsCoreHeader()
{
	return (InfoHeader()->biSize == sizeof(BITMAPCOREHEADER));
}

LPBITMAPFILEHEADER CDib::FileHeader()
{
	return ((LPBITMAPFILEHEADER) m_pFileHeader); 
}


UINT CDib::Width()
{
	if (IsValid())
	{
		if (IsCoreHeader())
			return (UINT) (CoreHeader()->bcWidth) ;
		else
			return (UINT) (InfoHeader()->biWidth) ;
	}
	return 0;
}

UINT CDib::Height ()
{
	if (IsValid())
	{
		if (IsCoreHeader())
			return (UINT) (CoreHeader()->bcHeight) ;
		else
			return (UINT) (InfoHeader()->biHeight) ;
	}
	return 0;
}


UINT CDib::Colors()
{
	if (IsCoreHeader())
		return (1 << CoreHeader()->bcBitCount);
	else
		return (1 << InfoHeader()->biBitCount);
}


BYTE *CDib::Bits()
{
	DWORD   dwNumColors;
	DWORD   dwColorTableSize ;
	UINT    wBitCount ;

	if (IsCoreHeader())
	{
		wBitCount = CoreHeader()->bcBitCount ;

		if (wBitCount != 24)
			dwNumColors = 1L << wBitCount ;
		else	
			dwNumColors = 0 ;

		dwColorTableSize = dwNumColors * sizeof (RGBTRIPLE) ;
	}
	else
	{
		wBitCount = InfoHeader()->biBitCount ;

		if (InfoHeader()->biSize >= 36)
			dwNumColors = InfoHeader()->biClrUsed ;
		else	
			dwNumColors = 0 ;

		if (dwNumColors == 0)
		{
			if (wBitCount != 24)
				dwNumColors = 1L << wBitCount ;
			else
				dwNumColors = 0 ;
		}

		dwColorTableSize = dwNumColors * sizeof (RGBQUAD) ;
	}

	return m_pDib + InfoHeader()->biSize + dwColorTableSize ;
}


BOOL CDib::Create(char *fileName)
{
	LPSTR pBuf;
	DWORD dwOffset;
	DWORD dwHeaderSize ;
	DWORD dwSize;
	HFILE hFile ;
	HFILE hCompFile;
	UINT  wDibRead ;
	int   erg;

	if (-1 == (hFile = _lopen (fileName, OF_READ | OF_SHARE_DENY_WRITE)))
		return FALSE;

	pBuf = (LPSTR) GlobalAllocPtr(GMEM_MOVEABLE,32767L);

	hCompFile = LZInit(hFile);

	if (hCompFile<0)
	{
		_lclose(hFile);
		return FALSE;
	}

	if (LZRead(hCompFile,(LPSTR) m_pFileHeader,sizeof(BITMAPFILEHEADER))<0)
	{
		LZClose(hCompFile);
		return FALSE;
	}

	if (m_pFileHeader->bfType != * (UINT *) "BM")
	{
		LZClose(hCompFile);
		return FALSE;
	}

	dwSize = m_pFileHeader->bfSize - sizeof (BITMAPFILEHEADER) ;
	m_size   = dwSize;

	m_pDib = (BYTE *) GlobalAllocPtr (GMEM_MOVEABLE,dwSize) ;

	if (!m_pDib)
	{
		LZClose(hCompFile);
		return FALSE;
	}

	dwOffset = 0 ;

	while (dwSize)
	{
		wDibRead = (UINT) ((dwSize > 32767ul) ? 32767ul:dwSize);

		erg = LZRead(hCompFile,pBuf,wDibRead);

		if ((int) wDibRead != erg)
		{
			LZClose(hCompFile);
			GlobalFreePtr(m_pDib);
			m_pDib = NULL;
			return FALSE;
		}

		for (int i=0;i<(int)wDibRead;++i)
			*(m_pDib+dwOffset+i) = *(pBuf+i); 

		dwSize    -= wDibRead ;
		dwOffset  += wDibRead ;
	}

	LZClose(hCompFile);

	dwHeaderSize = InfoHeader()->biSize;

	if (dwHeaderSize < 12 || (dwHeaderSize > 12 && dwHeaderSize < 16))
	{
		GlobalFreePtr (m_pDib) ;
		m_pDib = NULL;
		return FALSE;
	}

	GlobalFreePtr(pBuf);

	return TRUE;
}




BOOL CDib::SupportsPalette(CDC *pDC)
{
	BOOL b1= (pDC->GetDeviceCaps(RASTERCAPS) & RC_PALETTE);
	BOOL b2= pDC->GetDeviceCaps(DRIVERVERSION) >= 0x300;

	b1=b1;
	b2=b2;

	return ((pDC->GetDeviceCaps(RASTERCAPS) & RC_PALETTE) &&
			(pDC->GetDeviceCaps(DRIVERVERSION) >= 0x300));
}


HPALETTE CDib::GetPalette()
{
	HPALETTE newPalette;
	UINT     nColors;

	nColors   = Colors();
	if (nColors)
	{
		LOGPALETTE* logPal = (LOGPALETTE*) new BYTE[sizeof(LOGPALETTE) + (nColors-1)*sizeof(PALETTEENTRY)];

		logPal->palVersion  = 0x300;
		logPal->palNumEntries = nColors;
		for (UINT n = 0; n < nColors; n++)
		{
			logPal->palPalEntry[n].peRed   = Info()->bmiColors[n].rgbRed;
			logPal->palPalEntry[n].peGreen = Info()->bmiColors[n].rgbGreen;
			logPal->palPalEntry[n].peBlue  = Info()->bmiColors[n].rgbBlue;
			logPal->palPalEntry[n].peFlags = 0;
		}
		newPalette = CreatePalette(logPal);

		delete logPal;
		return newPalette;
	}
	else
		return 0;
}

void CDib::Show(CDC *pDC,int xStart,int yStart,DWORD mode )
{
  HPALETTE  thePalette=NULL;
  HBITMAP   newBitmap;

  if (SupportsPalette(pDC))
	thePalette = GetPalette();

  newBitmap = GetBitmap(pDC,thePalette);

  Show(pDC,newBitmap,xStart,yStart,mode);

  DeleteBitmap(newBitmap);

  if ((UINT) thePalette)
	DeletePalette(thePalette);
}


void CDib::Show(CDC *pDC, HBITMAP hBitmap, int xStart,int yStart,DWORD mode )
{
	CDC       memDC;
	CBitmap   *pOldBitmap;
	POINT     ptSize,ptOrg;
	HPALETTE  thePalette=NULL;
	BITMAP	  bm;
	CBitmap	  *pBitmap = CBitmap::FromHandle(hBitmap);

	pBitmap->GetBitmap(&bm);

	memDC.CreateCompatibleDC(pDC);
	pOldBitmap = memDC.SelectObject(pBitmap);

	if (SupportsPalette(pDC))
	{
		thePalette = GetPalette();
		pDC->SelectPalette(CPalette::FromHandle(thePalette),FALSE);
		pDC->RealizePalette();
		memDC.SelectPalette(CPalette::FromHandle(thePalette),FALSE);
	}

	ptSize.x = bm.bmWidth;
	ptSize.y = bm.bmHeight;
	pDC->DPtoLP(&ptSize,1);

	ptOrg.x = 0;
	ptOrg.y = 0;
	memDC.DPtoLP(&ptOrg,1);
	pDC->BitBlt(xStart,yStart,ptSize.x,ptSize.y,&memDC,ptOrg.x,ptOrg.y,mode);

	memDC.SelectObject(pOldBitmap);

	if ((UINT) thePalette)
		DeletePalette(thePalette);
}

void CDib::Show(CDC *pDC,CRect rect,DWORD mode)
{
	HPALETTE  thePalette=NULL;
	HBITMAP   newBitmap;

	if(SupportsPalette(pDC))
		thePalette = GetPalette();

	newBitmap  = GetBitmap(pDC,thePalette);

	Show(pDC,newBitmap,rect,mode);

	DeleteBitmap(newBitmap);

	if ((UINT) thePalette)
		DeletePalette(thePalette);
}

void CDib::Show(CDC *pDC,HBITMAP hBitmap,CRect rect,DWORD mode)
{
	POINT	ptOrg;
	CBitmap	*pBitmap = CBitmap::FromHandle(hBitmap);
	BITMAP  bm;

	pBitmap->GetBitmap(&bm);

	ptOrg.x = (rect.Width() - bm.bmWidth)/2;  //lio-Ecke berechnen
	ptOrg.y = (rect.Height() - bm.bmHeight)/2;

	Show(pDC,hBitmap,ptOrg.x,ptOrg.y,mode);
}

HBITMAP CDib::Stretch(CDC *pDC,HBITMAP ABitmap, int dx, int dy)
{
	CBitmap		*pOldBitmap[2];
	CDC			memDC[2];
	HBITMAP		newBitmap;
	HPALETTE    thePalette=NULL;

	memDC[0].CreateCompatibleDC(pDC);
	memDC[1].CreateCompatibleDC(pDC);
  
	pOldBitmap[0] = (CBitmap *) memDC[0].SelectObject(CBitmap::FromHandle(ABitmap));
	newBitmap     = ::CreateCompatibleBitmap(memDC[0].m_hDC,dx,dy);

	pOldBitmap[1] = (CBitmap *) memDC[1].SelectObject(CBitmap::FromHandle(newBitmap));
	memDC[1].SetStretchBltMode(COLORONCOLOR);

	if (SupportsPalette(pDC))
	{
		thePalette = GetPalette();
		pDC->SelectPalette(CPalette::FromHandle(thePalette),FALSE);
		pDC->RealizePalette();
		memDC[0].SelectPalette(CPalette::FromHandle(thePalette),FALSE);
		memDC[1].SelectPalette(CPalette::FromHandle(thePalette),FALSE);
	}

	memDC[1].StretchBlt(0,0,dx,dy,&memDC[0],0,0,Width(),Height(),SRCCOPY);

	memDC[0].SelectObject(pOldBitmap[0]);
	memDC[1].SelectObject(pOldBitmap[1]);

	if ((UINT) thePalette)
		DeletePalette(thePalette);
	return (newBitmap);
}


HBITMAP CDib::Stretch(CDC *pDC,int dx,int dy)
{
	HPALETTE thePalette=NULL;
	HBITMAP  newBitmap;
	HBITMAP  strBitmap;

	if(SupportsPalette(pDC))
		thePalette = GetPalette();

	newBitmap = GetBitmap(pDC,thePalette);

	strBitmap = Stretch(pDC,newBitmap,dx,dy);

	DeleteBitmap(newBitmap);

	if ((UINT) thePalette)
		DeletePalette(thePalette);
	return strBitmap;
}
