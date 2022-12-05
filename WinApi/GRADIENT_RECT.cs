using System;

namespace WinApi
{
	
	public struct GRADIENT_RECT
	{
		
		public GRADIENT_RECT(uint upLeft, uint lowRight)
		{
			this.UpperLeft = upLeft;
			this.LowerRight = lowRight;
		}

		
		public uint UpperLeft;

		
		public uint LowerRight;
	}
}
