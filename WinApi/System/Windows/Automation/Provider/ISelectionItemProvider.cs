using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface ISelectionItemProvider
	{
		
		void Select();

		
		void AddToSelection();

		
		void RemoveFromSelection();

		
		bool IsSelected
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		IRawElementProviderSimple SelectionContainer
		{
			
			get;
		}
	}
}
