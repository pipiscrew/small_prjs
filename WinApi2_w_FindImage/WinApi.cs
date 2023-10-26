using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public static class WinApi
{		
	[DllImport("user32.dll")]
	private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
	
	[DllImport("user32.dll")]
	private static extern bool BringWindowToTop(IntPtr hWnd);
	
	public static void CloseWindowByCaption(string caption, bool treatAsRegex)
	{
		int hWndByCaption = WinApi.GetHWndByCaption(caption, treatAsRegex);
		if (hWndByCaption != 0)
		{
			WinApi.PostMessage(new IntPtr(hWndByCaption), 16u, IntPtr.Zero, IntPtr.Zero);
		}
	}
	
	[DllImport("gdi32.dll")]
	public static extern int DeleteDC(IntPtr hdc);
	
	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern bool EnumDesktopWindows(IntPtr hDesktop, WinApi.EnumDelegate lpEnumCallbackFunction, IntPtr lParam);
	
	private static bool EnumRegexWindowsProc(IntPtr hWnd, int lParam)
	{
		if (WinApi.IsWindowVisible(hWnd))
		{
			int num = WinApi.FindWindow("Shell_TrayWnd", null);
			int desktopWindow = WinApi.GetDesktopWindow();
			int num2 = WinApi.FindWindow("Progman", "Program Manager");
			int windowLong = WinApi.GetWindowLong(hWnd, -8);
			if (hWnd.ToInt32() != num2 && hWnd.ToInt32() != desktopWindow && windowLong != num)
			{
				string windowText = WinApi.GetWindowText(hWnd);
				if (windowText != "" && ((!WinApi._treatAsRegex && (windowText == WinApi._caption || windowText.Like(WinApi._caption))) || (WinApi._treatAsRegex && Regex.IsMatch(windowText, WinApi._caption, RegexOptions.IgnoreCase))))
				{
					WinApi._foundHwd = hWnd.ToInt32();
				}
			}
		}
		return true;
	}
	
	private static bool EnumWindowsProc(IntPtr hWnd, int lParam)
	{
		if (WinApi.IsWindowVisible(hWnd))
		{
			int num = WinApi.FindWindow("Shell_TrayWnd", null);
			int desktopWindow = WinApi.GetDesktopWindow();
			int num2 = WinApi.FindWindow("Progman", "Program Manager");
			int windowLong = WinApi.GetWindowLong(hWnd, -8);
			if (hWnd.ToInt32() != num2 && hWnd.ToInt32() != desktopWindow && windowLong != num)
			{
				string windowText = WinApi.GetWindowText(hWnd);
				if (windowText != "")
				{
					WinApi.lstTitles.Add(windowText);
				}
			}
		}
		return true;
	}
	
	[DllImport("user32.dll")]
	public static extern int FindWindow(string className, string windowText);
	
	[DllImport("user32.dll")]
	public static extern bool GetCursorPos(out Point pt);
	
	[DllImport("user32.dll")]
	public static extern IntPtr GetDC(IntPtr hwnd);
	
	[DllImport("user32.dll")]
	public static extern int GetDesktopWindow();
	
	public static List<string> GetDesktopWindowsTitles()
	{
		WinApi.lstTitles = new List<string>();
		WinApi.EnumDelegate lpEnumCallbackFunction = new WinApi.EnumDelegate(WinApi.EnumWindowsProc);
		if (WinApi.EnumDesktopWindows(IntPtr.Zero, lpEnumCallbackFunction, IntPtr.Zero))
		{
			return WinApi.lstTitles;
		}
		int lastWin32Error = Marshal.GetLastWin32Error();
		throw new Exception(string.Format("EnumDesktopWindows failed with code {0}.", lastWin32Error));
	}
	
	[DllImport("user32.dll")]
	public static extern int GetForegroundWindow();
	
	public static int GetHWndByCaption(string caption, bool treatAsRegex)
	{
		WinApi._foundHwd = 0;
		WinApi._caption = caption;
		WinApi._treatAsRegex = treatAsRegex;
		WinApi.EnumDelegate lpEnumCallbackFunction = new WinApi.EnumDelegate(WinApi.EnumRegexWindowsProc);
		WinApi.EnumDesktopWindows(IntPtr.Zero, lpEnumCallbackFunction, IntPtr.Zero);
		return WinApi._foundHwd;
	}
	
	[DllImport("gdi32.dll")]
	private static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
	
	public static Color GetPixelColor(int x, int y)
	{
		uint pixelUintColor = WinApi.GetPixelUintColor(x, y);
		return Color.FromArgb((int)(pixelUintColor & 255u), (int)(pixelUintColor & 65280u) >> 8, (int)(pixelUintColor & 16711680u) >> 16);
	}
	
	public static uint GetPixelUintColor(int x, int y)
	{
		IntPtr dC = WinApi.GetDC(IntPtr.Zero);
		uint arg_1F_0 = WinApi.GetPixel(dC, x, y);
		WinApi.ReleaseDC(IntPtr.Zero, dC);
		return arg_1F_0;
	}
	
	[DllImport("kernel32.dll")]
	public static extern int GetTickCount();
	
	[DllImport("user32.dll", SetLastError = true)]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
	
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowPlacement(IntPtr hWnd, ref WinApi.WINDOWPLACEMENT lpwndpl);
	
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetWindowRect(int hWnd, out WinApi.RECT lpRect);
	
	public static FormWindowState GetWindowState(IntPtr hWnd)
	{
		WinApi.WINDOWPLACEMENT wINDOWPLACEMENT = default(WinApi.WINDOWPLACEMENT);
        //wINDOWPLACEMENT.length = Marshal.SizeOf<WinApi.WINDOWPLACEMENT>(wINDOWPLACEMENT);
		WinApi.GetWindowPlacement(hWnd, ref wINDOWPLACEMENT);
		if ((long)wINDOWPLACEMENT.showCmd == 3L)
		{
			return FormWindowState.Maximized;
		}
		if ((long)wINDOWPLACEMENT.showCmd == 2L)
		{
			return FormWindowState.Minimized;
		}
		return FormWindowState.Normal;
	}
	
	public static string GetWindowText(IntPtr hWnd)
	{
		StringBuilder stringBuilder = new StringBuilder(255);
		int length = WinApi._GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity + 1);
		stringBuilder.Length = length;
		return stringBuilder.ToString();
	}
	
	[DllImport("user32.dll")]
	private static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);
	
	public static Rectangle GetWndPosition(int hWnd)
	{
		WinApi.RECT rECT;
		if (WinApi.GetWindowRect(hWnd, out rECT))
		{
			return new Rectangle(rECT.Left, rECT.Top, rECT.Right - rECT.Left, rECT.Bottom - rECT.Top);
		}
		return Rectangle.Empty;
	}
	
	public static void HideMacroRecorder(IntPtr mainFormHwnd)
	{
		WinApi.SetWindowLong(mainFormHwnd, -20, WinApi.GetWindowLong(mainFormHwnd, -20) | 524288);
		WinApi.SetLayeredWindowAttributes(mainFormHwnd, 0u, 0, 2u);
	}
	
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool IsIconic(IntPtr hWnd);
	
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool IsWindowVisible(IntPtr hWnd);
	
	[DllImport("user32.dll")]
	public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
	
	[DllImport("user32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
	
	[DllImport("user32")]
	public static extern uint RegisterWindowMessage(string message);
	
	[DllImport("user32.dll")]
	public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
	
	public static void ResizeWindowByCaption(string caption, bool treatAsRegex, Rectangle rect)
	{
		int hWndByCaption = WinApi.GetHWndByCaption(caption, treatAsRegex);
		if (hWndByCaption != 0)
		{
			WinApi.SetWindowPos(hWndByCaption, 0, rect.Left, rect.Top, rect.Width, rect.Height, 64u);
		}
	}
	
	[DllImport("user32.dll")]
	public static extern bool SetCursorPos(int X, int Y);
	
	[DllImport("user32.dll")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);
	
	[DllImport("user32.dll")]
	private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
	
	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
	
	[DllImport("user32.dll")]
	public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
	
	public static void SetWindowStateWindowByCaption(string caption, bool treatAsRegex, FormWindowState state)
	{
		int hWndByCaption = WinApi.GetHWndByCaption(caption, treatAsRegex);
		int nCmdShow = 1;
		if (hWndByCaption != 0)
		{
			if (state == FormWindowState.Maximized)
			{
				nCmdShow = 3;
			}
			else if (state == FormWindowState.Minimized)
			{
				nCmdShow = 2;
			}
			WinApi.ShowWindow(new IntPtr(hWndByCaption), nCmdShow);
		}
	}
	
	public static void ShowMacroRecorder(IntPtr mainFormHwnd)
	{
		WinApi.SetLayeredWindowAttributes(mainFormHwnd, 0u, 255, 2u);
		WinApi.SetWindowLong(mainFormHwnd, -20, WinApi.GetWindowLong(mainFormHwnd, -20) & -524289);
	}
	
	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
	
	[DllImport("user32.dll")]
	private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
	
	private static void SwitchToWindow(int hwnd)
	{
		IntPtr intPtr = new IntPtr(hwnd);
		if (WinApi.IsIconic(intPtr))
		{
			WinApi.ShowWindow(intPtr, 9);
		}
		WinApi.SetForegroundWindow(intPtr);
		WinApi.ShowWindowAsync(intPtr, 5);
		WinApi.SetForegroundWindow(intPtr);
		IntPtr arg_40_0 = new IntPtr(WinApi.GetForegroundWindow());
		IntPtr zero = IntPtr.Zero;
		int windowThreadProcessId = WinApi.GetWindowThreadProcessId(arg_40_0, zero);
		int windowThreadProcessId2 = WinApi.GetWindowThreadProcessId(intPtr, zero);
		if (WinApi.AttachThreadInput((uint)windowThreadProcessId2, (uint)windowThreadProcessId, true))
		{
			WinApi.BringWindowToTop(intPtr);
			WinApi.SetForegroundWindow(intPtr);
			WinApi.AttachThreadInput((uint)windowThreadProcessId2, (uint)windowThreadProcessId, false);
		}
		if (WinApi.GetForegroundWindow() != hwnd)
		{
			IntPtr zero2 = IntPtr.Zero;
			WinApi.SystemParametersInfo(8192u, 0u, zero2, 0u);
			WinApi.SystemParametersInfo(8193u, 0u, zero, 2u);
			WinApi.BringWindowToTop(intPtr);
			WinApi.SetForegroundWindow(intPtr);
			WinApi.SystemParametersInfo(8193u, 0u, zero2, 2u);
		}
	}
	
	public static void SwitchToWndByCaption(string caption, bool treatAsRegex)
	{
		int hWndByCaption = WinApi.GetHWndByCaption(caption, treatAsRegex);
		if (hWndByCaption != 0)
		{
			WinApi.SwitchToWindow(hWndByCaption);
		}
	}
	
	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);
	
	[DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
	public static extern uint TimeBeginPeriod(uint uMilliseconds);
	
	[DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
	public static extern uint TimeEndPeriod(uint uMilliseconds);
	
	private static void UnitTest()
	{
		WinApi.GetWindowText(new IntPtr(23487623));
	}
	
	public static bool WindowExists(string caption, bool treatAsRegex)
	{
		List<string> desktopWindowsTitles = WinApi.GetDesktopWindowsTitles();
		Regex regex = null;
		if (treatAsRegex)
		{
			try
			{
				regex = new Regex(caption, RegexOptions.IgnoreCase);
			}
			catch
			{
				bool result = false;
				return result;
			}
		}
		foreach (string current in desktopWindowsTitles)
		{
			if (!treatAsRegex && (current.Trim() == caption.Trim() || current.Like(caption)))
			{
				bool result = true;
				return result;
			}
			if (treatAsRegex && regex.IsMatch(current))
			{
				bool result = true;
				return result;
			}
		}
		return false;
	}
	
	[DllImport("user32.dll")]
	public static extern IntPtr WindowFromPoint(Point pnt);
	
	[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowText", SetLastError = true)]
	private static extern int _GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);
	
	private const int GWL_EXSTYLE = -20;
	
	private const int GWL_HWNDPARENT = -8;
	
	public const int HWND_BROADCAST = 65535;
	
	public const int HWND_TOPMOST = -1;
	
	public const uint INPUT_KEYBOARD = 1u;
	
	public const uint KEYEVENTF_EXTENDEDKEY = 1u;
	
	public const uint KEYEVENTF_KEYUP = 2u;
	
	private static List<string> lstTitles;
	
	private const int LWA_ALPHA = 2;
	
	private const int MAXTITLE = 255;
	
	private const int SPIF_SENDCHANGE = 2;
	
	private const uint SPI_GETFOREGROUNDLOCKTIMEOUT = 8192u;
	
	private const uint SPI_SETFOREGROUNDLOCKTIMEOUT = 8193u;
	
	public const uint SWP_NOACTIVATE = 16u;
	
	private const uint SWP_SHOWWINDOW = 64u;
	
	public const int SW_RESTORE = 9;
	
	public const int SW_SHOW = 5;
	
	private const uint SW_SHOWMAXIMIZED = 3u;
	
	private const uint SW_SHOWMINIMIZED = 2u;
	
	public const int SW_SHOWNOACTIVATE = 4;
	
	private const uint SW_SHOWNORMAL = 1u;
	
	private const uint WM_CLOSE = 16u;
	
	public static readonly uint WM_SHOWME = WinApi.RegisterWindowMessage("WM_SHOWME");
	
	public static readonly uint WM_SHOWME2 = WinApi.RegisterWindowMessage("WM_SHOWME2");
	
	private const int WS_EX_LAYERED = 524288;
	
	private static string _caption;
	
	private static int _foundHwd;
	
	private static bool _treatAsRegex;
	
	
	private delegate bool EnumDelegate(IntPtr hWnd, int lParam);
	
	public struct RECT
	{
		
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}
	
	private struct WINDOWPLACEMENT
	{
		
		public int length;
		public int flags;
		public int showCmd;
		public Point ptMinPosition;
		public Point ptMaxPosition;
		public Rectangle rcNormalPosition;
	}
}
