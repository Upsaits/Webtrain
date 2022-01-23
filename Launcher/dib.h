#pragma once

class CDib : public CObject
{
	DECLARE_DYNCREATE(CDib)

	private:
		BYTE	*m_pDib;
		LPBITMAPFILEHEADER  m_pFileHeader;
		DWORD               m_size;

	public:
		CDib();
		CDib(char *fileName);
		~CDib();

		LPBITMAPFILEHEADER   FileHeader();
		LPBITMAPINFO         Info();
		LPBITMAPINFOHEADER   InfoHeader();
		LPBITMAPCOREHEADER   CoreHeader();
		BYTE				 *Bits();
 
		BOOL       Load(char *fFileName);
		HBITMAP    GetBitmap(CDC *pDC,HPALETTE APalette=NULL);
		BOOL       IsValid();
		BOOL       IsCoreHeader();
		HPALETTE   GetPalette();
		BOOL       SupportsPalette(CDC *pDC);

		UINT       Width();
		UINT       Height();
		UINT       Colors();

		void       Show(CDC *pDC,int xStart=0,int yStart=0,DWORD mode = SRCCOPY );
		void       Show(CDC *pDC,HBITMAP hBitmap, int xStart=0,int yStart=0,DWORD mode= SRCCOPY );
		void       Show(CDC *pDC,CRect rect,DWORD mode=SRCCOPY);
		void       Show(CDC *pDC,HBITMAP hBitmap,CRect rect,DWORD mode = SRCCOPY);
		HBITMAP    Stretch(CDC *pDC,int dx, int dy);
		HBITMAP    Stretch(CDC *pDC,HBITMAP ABitmap, int dx, int dy);

	protected:
		BOOL       Create(char * szFileName);
		void       Init();
};
