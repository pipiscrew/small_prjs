using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	
	public interface IFileDialog : IModalWindow
	{
		
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Show([In] IntPtr parent);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetFileTypes([In] uint cFileTypes, [MarshalAs(UnmanagedType.LPArray)] [In] Win32.COMDLG_FILTERSPEC[] rgFilterSpec);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetFileTypeIndex([In] uint iFileType);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFileTypeIndex(out uint piFileType);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Advise([MarshalAs(UnmanagedType.Interface)] [In] IFileDialogEvents pfde, out uint pdwCookie);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Unadvise([In] uint dwCookie);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetOptions([In] Win32.FOS fos);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetOptions(out Win32.FOS pfos);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetDefaultFolder([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetFolder([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFolder([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszName);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetTitle([MarshalAs(UnmanagedType.LPWStr)] [In] string pszTitle);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetResult([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddPlace([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi, Win32.FDAP fdap);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] [In] string pszDefaultExtension);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Close([MarshalAs(UnmanagedType.Error)] int hr);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetClientGuid([In] ref Guid guid);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ClearClientData();

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);
	}
}
