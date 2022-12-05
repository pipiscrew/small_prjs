using System;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface IWindowProvider
	{
		
		bool Maximizable
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		bool Minimizable
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		bool IsModal
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		WindowVisualState VisualState
		{
			
			get;
		}

		
		WindowInteractionState InteractionState
		{
			
			get;
		}

		
		bool IsTopmost
		{
			
			[return: MarshalAs(UnmanagedType.Bool)]
			get;
		}

		
		void SetVisualState(WindowVisualState state);

		
		void Close();

		
		[return: MarshalAs(UnmanagedType.Bool)]
		bool WaitForInputIdle(int milliseconds);
	}
}
