using System;

namespace WinApi
{
	
	public struct APPBARDATA
	{
		
		public uint cbSize;

		
		public IntPtr hWnd;

		
		public uint uCallbackMessage;

		
		public uint uEdge;

		
		public RECT rc;

		
		public int lParam;
	}
}
