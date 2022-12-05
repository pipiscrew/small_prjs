using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	
	
	public interface IFileDialogCustomize
	{
		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnableOpenDropDown([In] int dwIDCtl);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddMenu([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddPushButton([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddComboBox([In] int dwIDCtl);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddRadioButtonList([In] int dwIDCtl);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddCheckButton([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel, [In] bool bChecked);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddEditBox([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddSeparator([In] int dwIDCtl);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddText([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetControlLabel([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetControlState([In] int dwIDCtl, out Win32.CDCONTROLSTATE pdwState);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetControlState([In] int dwIDCtl, [In] Win32.CDCONTROLSTATE dwState);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetEditBoxText([In] int dwIDCtl, [Out] IntPtr ppszText);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetEditBoxText([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCheckButtonState([In] int dwIDCtl, out bool pbChecked);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCheckButtonState([In] int dwIDCtl, [In] bool bChecked);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AddControlItem([In] int dwIDCtl, [In] int dwIDItem, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveControlItem([In] int dwIDCtl, [In] int dwIDItem);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RemoveAllControlItems([In] int dwIDCtl);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetControlItemState([In] int dwIDCtl, [In] int dwIDItem, out Win32.CDCONTROLSTATE pdwState);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetControlItemState([In] int dwIDCtl, [In] int dwIDItem, [In] Win32.CDCONTROLSTATE dwState);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetSelectedControlItem([In] int dwIDCtl, out int pdwIDItem);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetSelectedControlItem([In] int dwIDCtl, [In] int dwIDItem);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void StartVisualGroup([In] int dwIDCtl, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EndVisualGroup();

		
		[MethodImpl(MethodImplOptions.InternalCall)]
		void MakeProminent([In] int dwIDCtl);
	}
}
