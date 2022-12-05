using System;
using System.Runtime.CompilerServices;

namespace WinApi
{
	
	
	public struct CMINVOKECOMMANDINFO
	{
		
		public int cbSize;

		
		public int fMask;

		
		public IntPtr hwnd;

		
		public IntPtr lpVerb;

		
		public string lpParameters;

		
		public string lpDirectory;

		
		public int nShow;

		
		public int dwHotKey;

		
		public IntPtr hIcon;
	}
}
