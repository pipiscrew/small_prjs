using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	[Guid("00000002-0000-0000-C000-000000000046"), InterfaceType(1)]
	
	public interface IMalloc
	{
		
		[PreserveSig]
		IntPtr Alloc([In] uint cb);

		
		[PreserveSig]
		IntPtr Realloc([In] IntPtr pv, [In] uint cb);

		
		[PreserveSig]
		void Free([In] IntPtr pv);

		
		[PreserveSig]
		uint GetSize([In] IntPtr pv);

		
		[PreserveSig]
		short DidAlloc([In] IntPtr pv);

		
		[PreserveSig]
		void HeapMinimize();
	}
}
