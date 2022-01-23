#pragma once

/////////////////////////////////////////////////////////////////////////////
// CDibWnd window
/////////////////////////////////////////////////////////////////////////////
class CDibWnd : public CWnd
{
	public:
		CDib	*m_pDib;

	// Construction
	public:
		CDibWnd();
		virtual ~CDibWnd();

	// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDibWnd)
	//}}AFX_VIRTUAL

	// Implementation

	public:
		BOOL		SetDib(CString fileName);
		int			GetWidth();
		int			GetHeight();
		int			IsValid();

		//Generated message map functions
	protected:
		//{{AFX_MSG(CDibWnd)
		afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
		afx_msg void OnPaint();
		//}}AFX_MSG
		DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////
//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

