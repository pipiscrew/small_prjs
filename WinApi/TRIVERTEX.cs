using System;

namespace WinApi
{
	
	public struct TRIVERTEX
	{
		
		public TRIVERTEX(int x, int y, ushort red, ushort green, ushort blue, ushort alpha)
		{
			this.x = x;
			this.y = y;
			this.Red = red;
			this.Green = green;
			this.Blue = blue;
			this.Alpha = alpha;
		}

		
		public int x;

		
		public int y;

		
		public ushort Red;

		
		public ushort Green;

		
		public ushort Blue;

		
		public ushort Alpha;
	}
}
