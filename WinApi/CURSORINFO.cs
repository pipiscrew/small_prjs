using System;

namespace WinApi
{
	
	public struct CURSORINFO
	{
		
		public int cbSize;

		
		public int flags;

		
		public IntPtr hCursor;

		
		public POINT ptScreenPos;
	}
}
