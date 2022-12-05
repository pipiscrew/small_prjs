using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WinApi
{
	
	public struct FILEDESCRIPTOR
	{
		
		public uint dwFlags;

		
		public Guid clsid;

		
		public int sizelcx;

		
		public int sizelcy;

		
		public int pointlx;

		
		public int pointly;

		
		public uint dwFileAttributes;

		
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;

		
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;

		
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;

		
		public uint nFileSizeHigh;

		
		public uint nFileSizeLow;

		

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string cFileName;
	}
}
