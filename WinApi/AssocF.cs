using System;

namespace WinApi
{
	
	
	public enum AssocF : uint
	{
		
		None = 0u,
		
		Init_NoRemapCLSID = 1u,
		
		Init_ByExeName = 2u,
		
		Open_ByExeName = 2u,
		
		Init_DefaultToStar = 4u,
		
		Init_DefaultToFolder = 8u,
		
		NoUserSettings = 16u,
		
		NoTruncate = 32u,
		
		Verify = 64u,
		
		RemapRunDll = 128u,
		
		NoFixUps = 256u,
		
		IgnoreBaseClass = 512u,
		
		Init_IgnoreUnknown = 1024u,
		
		Init_FixedProgId = 2048u,
		
		IsProtocol = 4096u,
		
		InitForFile = 8192u
	}
}
