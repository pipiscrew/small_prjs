using System;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IScrollProvider
	{
		
		void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount);

		
		void SetScrollPercent(double horizontalPercent, double verticalPercent);

		
		double HorizontalScrollPercent
		{
			
			get;
		}

		
		double VerticalScrollPercent
		{
			
			get;
		}

		
		double HorizontalViewSize
		{
			
			get;
		}

		
		double VerticalViewSize
		{
			
			get;
		}

		
		bool HorizontallyScrollable
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		bool VerticallyScrollable
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}
	}
}
