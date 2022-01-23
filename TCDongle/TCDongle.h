

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0595 */
/* at Thu Jul 23 16:31:31 2015
 */
/* Compiler settings for TCDongle.odl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0595 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __TCDongle_h__
#define __TCDongle_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef ___DTCDongle_FWD_DEFINED__
#define ___DTCDongle_FWD_DEFINED__
typedef interface _DTCDongle _DTCDongle;

#endif 	/* ___DTCDongle_FWD_DEFINED__ */


#ifndef ___DTCDongleEvents_FWD_DEFINED__
#define ___DTCDongleEvents_FWD_DEFINED__
typedef interface _DTCDongleEvents _DTCDongleEvents;

#endif 	/* ___DTCDongleEvents_FWD_DEFINED__ */


#ifndef __TCDongle_FWD_DEFINED__
#define __TCDongle_FWD_DEFINED__

#ifdef __cplusplus
typedef class TCDongle TCDongle;
#else
typedef struct TCDongle TCDongle;
#endif /* __cplusplus */

#endif 	/* __TCDongle_FWD_DEFINED__ */


#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_TCDongle_0000_0000 */
/* [local] */ 

#pragma once
#pragma region Desktop Family
#pragma endregion


extern RPC_IF_HANDLE __MIDL_itf_TCDongle_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_TCDongle_0000_0000_v0_0_s_ifspec;


#ifndef __TCDONGLELib_LIBRARY_DEFINED__
#define __TCDONGLELib_LIBRARY_DEFINED__

/* library TCDONGLELib */
/* [control][helpstring][helpfile][version][uuid] */ 


EXTERN_C const IID LIBID_TCDONGLELib;

#ifndef ___DTCDongle_DISPINTERFACE_DEFINED__
#define ___DTCDongle_DISPINTERFACE_DEFINED__

/* dispinterface _DTCDongle */
/* [hidden][helpstring][uuid] */ 


EXTERN_C const IID DIID__DTCDongle;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("A8127788-65E0-4A8A-8C87-B425F6968283")
    _DTCDongle : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DTCDongleVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DTCDongle * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DTCDongle * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DTCDongle * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DTCDongle * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DTCDongle * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DTCDongle * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DTCDongle * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } _DTCDongleVtbl;

    interface _DTCDongle
    {
        CONST_VTBL struct _DTCDongleVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DTCDongle_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _DTCDongle_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _DTCDongle_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _DTCDongle_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _DTCDongle_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _DTCDongle_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _DTCDongle_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DTCDongle_DISPINTERFACE_DEFINED__ */


#ifndef ___DTCDongleEvents_DISPINTERFACE_DEFINED__
#define ___DTCDongleEvents_DISPINTERFACE_DEFINED__

/* dispinterface _DTCDongleEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__DTCDongleEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("2D479C09-288D-4471-B373-8B9EBF6563D6")
    _DTCDongleEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _DTCDongleEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _DTCDongleEvents * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _DTCDongleEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _DTCDongleEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _DTCDongleEvents * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _DTCDongleEvents * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _DTCDongleEvents * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _DTCDongleEvents * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        END_INTERFACE
    } _DTCDongleEventsVtbl;

    interface _DTCDongleEvents
    {
        CONST_VTBL struct _DTCDongleEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _DTCDongleEvents_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _DTCDongleEvents_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _DTCDongleEvents_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _DTCDongleEvents_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _DTCDongleEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _DTCDongleEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _DTCDongleEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___DTCDongleEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_TCDongle;

#ifdef __cplusplus

class DECLSPEC_UUID("97476B6F-32C3-41D5-BD86-EFE3286E3082")
TCDongle;
#endif
#endif /* __TCDONGLELib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


