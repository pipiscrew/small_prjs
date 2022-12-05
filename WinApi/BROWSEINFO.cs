using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	public struct BROWSEINFO
	{
		
		public IntPtr hwndOwner;

		
		public IntPtr pidlRoot;

		
		public IntPtr pszDisplayName;

		
		[MarshalAs(UnmanagedType.LPTStr)]
		public string lpszTitle;

		
		public uint ulFlags;

		
		public IntPtr lpfn;

		
		public int lParam;

		
		public IntPtr iImage;
	}
}
