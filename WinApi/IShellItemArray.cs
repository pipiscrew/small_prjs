using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	
	public interface IShellItemArray
	{
		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void BindToHandler([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPropertyStore([In] int Flags, [In] ref Guid riid, out IntPtr ppv);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetPropertyDescriptionList([In] ref Win32.PROPERTYKEY keyType, [In] ref Guid riid, out IntPtr ppv);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetAttributes([In] Win32.SIATTRIBFLAGS dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCount(out uint pdwNumItems);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetItemAt([In] uint dwIndex, [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnumItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenumShellItems);
	}
}
