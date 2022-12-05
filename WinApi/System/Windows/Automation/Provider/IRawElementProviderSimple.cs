using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IRawElementProviderSimple
	{
		
		ProviderOptions ProviderOptions
		{
			
			get;
		}

		
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetPatternProvider(int patternId);

		
		object GetPropertyValue(int propertyId);

		
		IRawElementProviderSimple HostRawElementProvider
		{
			
			get;
		}
	}
}
