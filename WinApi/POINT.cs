using System;
using System.Drawing;

namespace WinApi
{
	
	public struct POINT
	{
		
		public POINT(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		
		public static implicit operator Point(POINT p)
		{
			return new Point(p.X, p.Y);
		}

		
		public static implicit operator POINT(Point p)
		{
			return new POINT(p.X, p.Y);
		}

		
		public int X;

		
		public int Y;
	}
}
