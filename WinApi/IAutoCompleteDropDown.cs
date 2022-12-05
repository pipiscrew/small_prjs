using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace WinApi
{
	
	
	
	public interface IAutoCompleteDropDown
	{
		
		[PreserveSig]
		int GetDropDownStatus(out uint pdwFlags, [MarshalAs(UnmanagedType.LPWStr)] out StringBuilder ppwszString);

		
		[PreserveSig]
		int ResetEnumerator();
	}
}
