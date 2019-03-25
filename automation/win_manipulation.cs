class Program
{

	[DllImport("user32.dll", SetLastError = true)]
	private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetForegroundWindow(IntPtr hWnd);

	//mouse click - http://stackoverflow.com/a/2416762
	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

	private const int MOUSEEVENTF_LEFTDOWN = 0x02;
	private const int MOUSEEVENTF_LEFTUP = 0x04;
	private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
	private const int MOUSEEVENTF_RIGHTUP = 0x10;

	//get window top left via hwnd - https://stackoverflow.com/a/1434577
	[DllImport("user32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

	[StructLayout(LayoutKind.Sequential)]
	private struct RECT
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}


	//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-showwindow
	[DllImport("user32.dll")]
	public static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);


	static void Main(string[] args)
	{
		string mobile = "";
		string message = "";
		int left = 0;
		int top = 0;

		if (args.Length == 4)
		{
			left = try_parse_int(args[0].Trim());
			top = try_parse_int(args[1].Trim());

			if (left == 0 || top == 0)
			{
				show_help();
				return;
			}

			mobile = args[2].Trim();
			message = args[3].Trim();
		}
		else
		{
			show_help();
			return;

		}

		//https://stackoverflow.com/a/637680 when minimized cant find window
		//foreach (Process proc in Process.GetProcesses())
		//{
		//    if (proc.MainWindowTitle.Contains("Viber"))
		//    {
		//        hWnd = proc.MainWindowHandle;
		//        break;
		//    }
		//}

		IntPtr hWnd = FindWindow(null, "Viber " + mobile);

		if (!hWnd.Equals(IntPtr.Zero))
		{
			if (ShowWindow(hWnd, 9))
			{
				if (SetForegroundWindow(hWnd))
				{
					//get window top left via hwnd
					RECT rct = new RECT();
					GetWindowRect(hWnd, ref rct);

					//set cursor position
					Cursor.Position = new System.Drawing.Point(rct.Left + left, rct.Top + top);


					//mouse click
					int X = Cursor.Position.X;
					int Y = Cursor.Position.Y;
					mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

					////SendKeys.SendWait("{TAB}");
					SendKeys.SendWait(message);
					SendKeys.SendWait("{ENTER}");


				}
				else
				{
					Console.WriteLine("SetForegroundWindow failed");
				}
			}
			else
			{
				Console.WriteLine("ShowWindow failed");
			}
		}
		else
		{
			Console.WriteLine("FindWindow failed");
		}

	   // Console.ReadLine();
	}

	private static int try_parse_int(string value){

		int number;

		bool result = int.TryParse(value, out number);

		if (result)
			return number;
		else
			return 0;
	}

	private static void show_help(){
					Console.WriteLine(@"Viber Favorite Contact Group - Auto Sender v1.0 
-----------------------------------------------

please use the following syntax : 
viberauto.exe left top mobilephone message

left        = how far from the left side of the window (default : 40)
top         = how far from the top side of the window (default : 230)
mobilephone = your mobile as appear at Viber window title
message     = the message you want to send into double quotes (ex ""hi"")");
			//Console.ReadKey();
	}
}
