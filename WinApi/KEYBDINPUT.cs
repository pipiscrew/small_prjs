using System;

namespace WinApi
{
	
	public struct KEYBDINPUT
	{
		
		public VirtualKeyShort KeyCode;

		
		public ScanCodeShort Scan;

		
		public KEYEVENTF Flags;

		
		public int Time;

		
		public UIntPtr ExtraInfo;
	}
}
