using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	[Guid("b4db1657-70d7-485e-8e3e-6fcb5a5c1802"), InterfaceType(1)]
	
	public interface IModalWindow
	{
		
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int Show([In] IntPtr parent);
	}
}
