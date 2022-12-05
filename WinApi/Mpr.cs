using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace WinApi
{
	
	public static class Mpr
	{
		
		
		[DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);

		
		public const int ERROR_MORE_DATA = 234;

		
		public const int ERROR_NOT_CONNECTED = 2250;

		
		public const int ERROR_SUCCESS = 0;
	}
}
