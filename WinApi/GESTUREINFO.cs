using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	public struct GESTUREINFO
	{
		
		public int cbSize;

		
		public int dwFlags;

		
		public int dwID;

		
		public IntPtr hwndTarget;

		
		[MarshalAs(UnmanagedType.Struct)]
		public POINTS ptsLocation;

		
		public int dwInstanceID;

		
		public int dwSequenceID;

		
		public long ullArguments;

		
		public int cbExtraArgs;
	}
}
