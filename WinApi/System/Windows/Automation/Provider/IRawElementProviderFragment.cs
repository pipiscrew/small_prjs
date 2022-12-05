using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WinApi;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IRawElementProviderFragment : IRawElementProviderSimple
	{
		
		
		IRawElementProviderFragment Navigate(NavigateDirection direction);

		
		
		int[] GetRuntimeId();

		
		UiaRect BoundingRectangle
		{
			
			get;
		}


		IRawElementProviderSimple[] GetEmbeddedFragmentRoots();

		
		void SetFocus();

		
		IRawElementProviderFragmentRoot FragmentRoot
		{
			
			get;
		}
	}
}
