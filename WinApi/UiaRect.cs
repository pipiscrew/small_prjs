using System;

namespace WinApi
{
	
	public struct UiaRect
	{
		
		public UiaRect(double left, double top, double width, double height)
		{
			this.left = left;
			this.top = top;
			this.width = width;
			this.height = height;
		}

		
		private static UiaRect CreateEmptyRect()
		{
			return new UiaRect
			{
				left = double.PositiveInfinity,
				top = double.PositiveInfinity,
				width = double.NegativeInfinity,
				height = double.NegativeInfinity
			};
		}

		
		public double left;

		
		public double top;

		
		public double width;

		
		public double height;

		
		public static UiaRect Empty = UiaRect.CreateEmptyRect();
	}
}
