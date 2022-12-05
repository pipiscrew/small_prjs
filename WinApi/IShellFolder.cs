using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	
	public interface IShellFolder
	{
		
		[PreserveSig]
		uint ParseDisplayName(IntPtr hwnd, IntPtr pbc, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszDisplayName, out uint pchEaten, out IntPtr ppidl, ref uint pdwAttributes);

		
		[PreserveSig]
		uint EnumObjects(IntPtr hwnd, uint grfFlags, out IntPtr ppenumIDList);

		
		[PreserveSig]
		uint BindToObject(IntPtr pidl, IntPtr pbc, [In] ref Guid riid, out IntPtr ppv);

		
		[PreserveSig]
		uint BindToStorage(IntPtr pidl, IntPtr pbc, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

		
		[PreserveSig]
		int CompareIDs(int lParam, IntPtr pidl1, IntPtr pidl2);

		
		[PreserveSig]
		uint CreateViewObject(IntPtr hwndOwner, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

		
		[PreserveSig]
		uint GetAttributesOf(int cidl, [MarshalAs(UnmanagedType.LPArray)] [In] IntPtr[] apidl, [MarshalAs(UnmanagedType.LPArray)] uint[] rgfInOut);

		
		[PreserveSig]
		uint GetUIObjectOf(IntPtr hwndOwner, int cidl, [MarshalAs(UnmanagedType.LPArray)] [In] IntPtr[] apidl, [In] ref Guid riid, IntPtr rgfReserved, [MarshalAs(UnmanagedType.Interface)] out object ppv);

		
		[PreserveSig]
		uint GetDisplayNameOf(IntPtr pidl, uint uFlags, out IntPtr pName);

		
		[PreserveSig]
		uint SetNameOf(IntPtr hwnd, IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszName, uint uFlags, out IntPtr ppidlOut);
	}
}
