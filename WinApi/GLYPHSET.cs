using System;
using System.Runtime.CompilerServices;

namespace WinApi
{
	
	public struct GLYPHSET
	{
		
		public ushort cbThis;

		
		public ushort flAccel;

		
		public ushort cGlyphsSupported;

		
		public ushort cRanges;

		

		public WCRANGE[] ranges;
	}
}
