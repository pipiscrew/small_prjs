using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	
	public interface IFileDialogControlEvents
	{
		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnItemSelected([MarshalAs(UnmanagedType.Interface)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl, [In] int dwIDItem);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnButtonClicked([MarshalAs(UnmanagedType.Interface)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnCheckButtonToggled([MarshalAs(UnmanagedType.Interface)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl, [In] bool bChecked);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnControlActivating([MarshalAs(UnmanagedType.Interface)] [In] IFileDialogCustomize pfdc, [In] int dwIDCtl);
	}
}
