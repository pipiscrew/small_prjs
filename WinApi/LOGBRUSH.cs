using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	[StructLayout(LayoutKind.Sequential)]
	public class LOGBRUSH
	{
		
		public int lbStyle;

		
		public int lbColor;

		
		public IntPtr lbHatch;
	}
}
