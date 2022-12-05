using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IGridProvider
	{
		
		IRawElementProviderSimple GetItem(int row, int column);

		
		int RowCount
		{
			
			get;
		}

		
		int ColumnCount
		{
			
			get;
		}
	}
}
