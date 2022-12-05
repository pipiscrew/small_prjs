using System;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IToggleProvider
	{
		
		void Toggle();

		
		ToggleState ToggleState
		{
			
			get;
		}
	}
}
