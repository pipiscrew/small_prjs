using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface ISelectionProvider
	{

		IRawElementProviderSimple[] GetSelection();

		
		bool CanSelectMultiple
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		bool IsSelectionRequired
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}
	}
}
