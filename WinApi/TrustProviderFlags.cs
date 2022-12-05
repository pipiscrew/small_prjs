using System;

namespace WinApi
{
	
	public enum TrustProviderFlags
	{
		
		UseIE4Trust = 1,
		
		NoIE4Chain,
		
		NoPolicyUsage = 4,
		
		RevocationCheckNone = 16,
		
		RevocationCheckEndCert = 32,
		
		RevocationCheckChain = 64,
		
		RecovationCheckChainExcludeRoot = 128,
		
		Safer = 256,
		
		HashOnly = 512,
		
		UseDefaultOSVerCheck = 1024,
		
		LifetimeSigning = 2048
	}
}
