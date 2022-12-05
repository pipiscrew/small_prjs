using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IGridItemProvider
	{
		
		int Row
		{
			
			get;
		}

		
		int Column
		{
			
			get;
		}

		
		int RowSpan
		{
			
			get;
		}

		
		int ColumnSpan
		{
			
			get;
		}

		
		IRawElementProviderSimple ContainingGrid
		{
			
			get;
		}
	}
}
