using System;
using System.Runtime.InteropServices;

namespace System.Windows.Automation
{
	
	
	public enum WindowInteractionState
	{
		
		Running,
		
		Closing,
		
		ReadyForUserInteraction,
		
		BlockedByModalWindow,
		
		NotResponding
	}
}
