using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WinApi
{
	
	
	
	public interface IAutoComplete
	{
		
		[PreserveSig]
		int Init(HandleRef hwndEdit, IEnumString punkACL, [MarshalAs(UnmanagedType.LPWStr)] string pwszRegKeyPath, [MarshalAs(UnmanagedType.LPWStr)] string pwszQuickComplete);

		
		[PreserveSig]
		int Enable(int fEnable);
	}
}
