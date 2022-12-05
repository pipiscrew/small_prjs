using System;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IExpandCollapseProvider
	{
		
		void Expand();

		
		void Collapse();

		
		ExpandCollapseState ExpandCollapseState
		{
			
			get;
		}
	}
}
