using System;

namespace WinApi
{
	
	public enum ExtendedNameFormat
	{
		
		NameUnknown,
		
		NameFullyQualifiedDN,
		
		NameSamCompatible,
		
		NameDisplay,
		
		NameUniqueId = 6,
		
		NameCanonical,
		
		NameUserPrincipal,
		
		NameCanonicalEx,
		
		NameServicePrincipal,
		
		NameDnsDomain = 12
	}
}
