using System;

namespace WinApi
{
	
	public enum BlendOps : byte
	{
		
		AC_SRC_OVER,
		
		AC_SRC_ALPHA,
		
		AC_SRC_NO_PREMULT_ALPHA = 1,
		
		AC_SRC_NO_ALPHA,
		
		AC_DST_NO_PREMULT_ALPHA = 16,
		
		AC_DST_NO_ALPHA = 32
	}
}
