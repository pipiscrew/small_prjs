using System;

namespace WinApi
{
	
	public struct IMAGEINFO
	{
		
		public IntPtr hbmImage;

		
		public IntPtr hbmMask;

		
		public int Unused1;

		
		public int Unused2;

		
		public RECT rcImage;
	}
}
