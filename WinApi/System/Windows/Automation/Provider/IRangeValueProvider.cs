using System;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IRangeValueProvider
	{
		
		void SetValue(double value);

		
		double Value
		{
			
			get;
		}

		
		bool IsReadOnly
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		double Maximum
		{
			
			get;
		}

		
		double Minimum
		{
			
			get;
		}

		
		double LargeChange
		{
			
			get;
		}

		
		double SmallChange
		{
			
			get;
		}
	}
}
