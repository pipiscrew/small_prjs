using System;

namespace WinApi
{
	
	
	public enum LoadLibraryExFlags : uint
	{
		
		DontResolveDllReferences = 1u,
		
		LoadLibraryAsDatafile = 2u,
		
		LoadWithAlteredSearchPath = 8u,
		
		LoadIgnoreCodeAuthzLevel = 16u
	}
}
