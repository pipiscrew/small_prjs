using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	public struct SHITEMID
	{
		
		public ushort cb;

		[MarshalAs(UnmanagedType.LPArray)]
		public byte[] abID;
	}
}
