[DllImport("wininet.dll", SetLastError = true)]
private static extern long DeleteUrlCacheEntry(string lpszUrlName);

[DllImport("wininet.dll", SetLastError = true)]
private static extern long DeleteUrlCacheEntry2(IntPtr lpszUrlName);


//mouse click
[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

private const int MOUSEEVENTF_LEFTDOWN = 0x02;
private const int MOUSEEVENTF_LEFTUP = 0x04;
private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
private const int MOUSEEVENTF_RIGHTUP = 0x10;

public frmUnassigned()
{
	InitializeComponent();
}

private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
{
	Console.WriteLine(e.Url.ToString());
	DeleteUrlCacheEntry(e.Url.ToString());
}

private void frmTickets_Load(object sender, EventArgs e)
{
	//WebBrowserHelper.ClearCache();
	DeleteUrlCacheEntry("http://x.com/default.aspx");
	this.Enabled = false;
	webBrowser1.Navigate("http://x.com/default.aspx");
	webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
	timer1.Interval = 100;

}

private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
{
	//http://stackoverflow.com/a/7801574
	//inject JS to current page, and execute it!!
	var jsCode = "$(\"#chkbxIncQueue_UnassignedOnly\").prop('checked',true);";
	webBrowser1.Document.InvokeScript("execScript", new Object[] { jsCode, "JavaScript" });

	timer1.Enabled = true;
}

private void timer1_Tick(object sender, EventArgs e)
{
	//example1
	timer1.Enabled = false;
	Cursor.Position = new System.Drawing.Point(webBrowser1.Location.X + this.Location.X + 570, webBrowser1.Location.Y + this.Location.Y + 300);

	//http://stackoverflow.com/a/2416762
	int X = Cursor.Position.X;
	int Y = Cursor.Position.Y;
	mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

   // this.WindowState = FormWindowState.Maximized;
	this.Enabled = true;
	
	//example2
	//find assignee textbox and set username
	//var elem = webBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_txbAssignee");
	//
	//if (elem != null)
	//{
	//    Console.WriteLine("SUBMIT");
	//    elem.InnerText = General.userName;
	//    timer1.Enabled = false;
	//
	//    //submit
	//    HtmlElement loBtn = webBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_btnSearch");
	//    loBtn.InvokeMember("click");
	//}
}

private void button5_Click_1(object sender, EventArgs e)
{
	General.Copy2Clipboard(webBrowser1.Url.ToString());
}
