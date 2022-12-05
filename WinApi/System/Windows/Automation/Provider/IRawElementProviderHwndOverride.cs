using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IRawElementProviderHwndOverride : IRawElementProviderSimple
	{
		
		
		IRawElementProviderSimple GetOverrideProviderForHwnd(IntPtr hwnd);
	}
}
