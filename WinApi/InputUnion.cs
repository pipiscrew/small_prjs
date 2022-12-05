using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	[StructLayout(LayoutKind.Explicit)]
	public struct InputUnion
	{
		
		[FieldOffset(0)]
		public MOUSEINPUT Mouse;

		
		[FieldOffset(0)]
		public KEYBDINPUT Keyboard;

		
		[FieldOffset(0)]
		public HARDWAREINPUT Hardware;
	}
}
