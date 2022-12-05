using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IValueProvider
	{
		
		void SetValue([MarshalAs(UnmanagedType.LPWStr)] string value);

		
		string Value
		{
			
			get;
		}

		
		bool IsReadOnly
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}
	}
}
