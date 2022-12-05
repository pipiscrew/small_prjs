using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	
	public interface IFileDialogEvents
	{
		
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		HRESULT OnFileOk([MarshalAs(UnmanagedType.Interface)] [In] IFileDialog pfd);

		
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		HRESULT OnFolderChanging([MarshalAs(UnmanagedType.Interface)] [In] IFileDialog pfd, [MarshalAs(UnmanagedType.Interface)] [In] IShellItem psiFolder);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnFolderChange([MarshalAs(UnmanagedType.Interface)] [In] IFileDialog pfd);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnSelectionChange([MarshalAs(UnmanagedType.Interface)] [In] IFileDialog pfd);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnShareViolation([MarshalAs(UnmanagedType.Interface)] [In] IFileDialog pfd, [MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi, out Win32.FDE_SHAREVIOLATION_RESPONSE pResponse);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnTypeChange([MarshalAs(UnmanagedType.Interface)] [In] IFileDialog pfd);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnOverwrite([MarshalAs(UnmanagedType.Interface)] [In] IFileDialog pfd, [MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi, out Win32.FDE_OVERWRITE_RESPONSE pResponse);
	}
}
