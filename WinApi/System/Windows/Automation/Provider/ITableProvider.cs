using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	public interface ITableProvider : IGridProvider
	{
		
		IRawElementProviderSimple[] GetRowHeaders();

		
		IRawElementProviderSimple[] GetColumnHeaders();

		
		RowOrColumnMajor RowOrColumnMajor
		{
			
			get;
		}
	}
}
