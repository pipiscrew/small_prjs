using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct CHOOSEFONT
	{
		
		public int lStructSize;

		
		public IntPtr hwndOwner;

		
		public IntPtr hDC;

		
		public IntPtr lpLogFont;

		
		public int iPointSize;

		
		public int Flags;

		
		public int rgbColors;

		
		public IntPtr lCustData;

		
		public IntPtr lpfnHook;

		
		public string lpTemplateName;

		
		public IntPtr hInstance;

		
		public string lpszStyle;

		
		public short nFontType;

		
		public int nSizeMin;

		
		public int nSizeMax;
	}
}
