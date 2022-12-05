using System;

namespace System.Windows.Automation
{
	
	
	public enum ProviderOptions
	{
		
		ClientSideProvider = 1,
		
		ServerSideProvider = 2,
		
		NonClientAreaProvider = 4,
		
		OverrideProvider = 8,
		
		ProviderOwnsSetFocus = 16,
		
		UseComThreading = 32
	}
}
