using System;

namespace WinApi
{
	
	
	public enum MOUSEEVENTF : uint
	{
		
		MOVE = 1u,
		
		LEFTDOWN = 2u,
		
		LEFTUP = 4u,
		
		RIGHTDOWN = 8u,
		
		RIGHTUP = 16u,
		
		MIDDLEDOWN = 32u,
		
		MIDDLEUP = 64u,
		
		XDOWN = 128u,
		
		XUP = 256u,
		
		WHEEL = 2048u,
		
		HWHEEL = 4096u,
		
		MOVE_NOCOALESCE = 8192u,
		
		VIRTUALDESK = 16384u,
		
		ABSOLUTE = 32768u
	}
}
