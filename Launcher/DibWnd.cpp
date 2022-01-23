#include "stdafx.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDibWnd
/////////////////////////////////////////////////////////////////////////////
CDibWnd::CDibWnd() : m_pDib(NULL)
{
}

CDibWnd::~CDibWnd()
{
	if (m_pDib)
		delete m_pDib;
}


BOOL CDibWnd::SetDib(CString fileName)
{
	m_pDib = new CDib((LPSTR)(LPCTSTR)(fileName));

	return m_pDib->IsValid();
}

int	CDibWnd::GetWidth()
{
	if (m_pDib && m_pDib->IsValid())
		return m_pDib->Width();
	return 0;
}

int	CDibWnd::GetHeight()
{
	if (m_pDib && m_pDib->IsValid())
		return m_pDib->Height();
	return 0;
}

int CDibWnd::IsValid()
{
	return (m_pDib && m_pDib->IsValid());
}

BEGIN_MESSAGE_MAP(CDibWnd, CWnd)
	//{{AFX_MSG_MAP(CDibWnd)
	ON_WM_CREATE()
	ON_WM_PAINT()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()


/////////////////////////////////////////////////////////////////////////////
// CDibWnd message handlers
/////////////////////////////////////////////////////////////////////////////
int CDibWnd::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CWnd::OnCreate(lpCreateStruct) == -1)
		return -1;
	
	return 0;
}


void CDibWnd::OnPaint() 
{
	CPaintDC dc(this); // device context for painting
	
	CRect		rect;
	GetClientRect(&rect);

	if (m_pDib && m_pDib->IsValid())
		m_pDib->Show(&dc,rect);
	else
		dc.FillRect(rect,&CBrush(RGB(128,128,128)));
}
