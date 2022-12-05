using System;
using System.Drawing;

namespace WinApi
{
	
	public struct WINDOWPLACEMENT
	{
		
		public int length;

		
		public int flags;

		
		public int showCmd;

		
		public Point ptMinPosition;

		
		public Point ptMaxPosition;

		
		public RECT rcNormalPosition;
	}
}
