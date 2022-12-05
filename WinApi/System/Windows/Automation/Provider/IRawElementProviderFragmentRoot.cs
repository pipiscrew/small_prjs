using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IRawElementProviderFragmentRoot : IRawElementProviderFragment, IRawElementProviderSimple
	{
		
		
		IRawElementProviderFragment ElementProviderFromPoint(double x, double y);

		
		
		IRawElementProviderFragment GetFocus();
	}
}
