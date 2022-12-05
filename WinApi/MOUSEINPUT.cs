using System;

namespace WinApi
{
	
	public struct MOUSEINPUT
	{
		
		public int X;

		
		public int Y;

		
		public int MouseData;

		
		public MOUSEEVENTF Flags;

		
		public uint Time;

		
		public UIntPtr ExtraInfo;
	}
}
