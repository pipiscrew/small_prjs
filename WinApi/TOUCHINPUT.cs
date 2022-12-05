using System;

namespace WinApi
{
	
	public struct TOUCHINPUT
	{
		
		public int x;

		
		public int y;

		
		public IntPtr hSource;

		
		public int dwID;

		
		public int dwFlags;

		
		public int dwMask;

		
		public int dwTime;

		
		public IntPtr dwExtraInfo;

		
		public int cxContact;

		
		public int cyContact;
	}
}
