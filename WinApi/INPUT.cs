using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	public struct INPUT
	{
		
		public static int Size
		{
			
			get
			{
				return Marshal.SizeOf(typeof(INPUT));
			}
		}

		
		public InputType Type;

		
		public InputUnion Data;
	}
}
