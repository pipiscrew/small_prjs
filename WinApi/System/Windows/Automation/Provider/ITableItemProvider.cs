using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface ITableItemProvider : IGridItemProvider
	{

		IRawElementProviderSimple[] GetRowHeaderItems();


		IRawElementProviderSimple[] GetColumnHeaderItems();
	}
}
