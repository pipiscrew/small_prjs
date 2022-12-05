using System;

namespace WinApi
{
	
	public enum TextAlignment : uint
	{
		
		TA_NOUPDATECP,
		
		TA_UPDATECP,
		
		TA_LEFT = 0u,
		
		TA_RIGHT = 2u,
		
		TA_CENTER = 6u,
		
		TA_TOP = 0u,
		
		TA_BOTTOM = 8u,
		
		TA_BASELINE = 24u,
		
		TA_RTLREADING = 256u
	}
}
