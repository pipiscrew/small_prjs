using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	
	public interface IShellItem
	{
		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void BindToHandler([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, out IntPtr ppv);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetParent([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDisplayName([In] Win32.SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetAttributes([In] uint sfgaoMask, out uint psfgaoAttribs);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Compare([MarshalAs(UnmanagedType.Interface)] [In] IShellItem psi, [In] uint hint, out int piOrder);
	}
}
