using System;

namespace WinApi
{
	
	
	public enum MouseEventFlags : uint
	{
		
		LEFTDOWN = 2u,
		
		LEFTUP = 4u,
		
		MIDDLEDOWN = 32u,
		
		MIDDLEUP = 64u,
		
		MOVE = 1u,
		
		ABSOLUTE = 32768u,
		
		RIGHTDOWN = 8u,
		
		RIGHTUP = 16u,
		
		WHEEL = 2048u,
		
		XDOWN = 128u,
		
		XUP = 256u
	}
}
