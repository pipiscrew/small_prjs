using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IRawElementProviderAdviseEvents : IRawElementProviderSimple
	{
		
		void AdviseEventAdded(int eventId, int[] properties);

		
		void AdviseEventRemoved(int eventId, int[] properties);
	}
}
