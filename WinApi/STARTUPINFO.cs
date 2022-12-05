﻿using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class STARTUPINFO
	{
		
		public int cb;

		
		public IntPtr lpReserved;

		
		public IntPtr lpDesktop;

		
		public IntPtr lpTitle;

		
		public int dwX;

		
		public int dwY;

		
		public int dwXSize;

		
		public int dwYSize;

		
		public int dwXCountChars;

		
		public int dwYCountChars;

		
		public int dwFillAttribute;

		
		public int dwFlags;

		
		public short wShowWindow;

		
		public short cbReserved2;

		
		public IntPtr lpReserved2;

		
		public IntPtr hStdInput;

		
		public IntPtr hStdOutput;

		
		public IntPtr hStdError;
	}
}
