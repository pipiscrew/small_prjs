public class ExplorerSend
{   
	// source - https://learn.microsoft.com/en-us/answers/questions/1162850/opening-the-default-e-email-app
	/*
		Send to > Mail Recipient - is using sendmail.dll
		https://www.codeproject.com/articles/SendTo-Mail-Recipient
		https://www.mvps.org/emorcillo/en/code/vb6/sendmail.shtml
		misc - use of ShellLink - https://github.com/sybil-sink/megadesktop/blob/master/MegaSync/shellink/ShellLink.cs
	*/
	
	public enum HRESULT : int
	{
		S_OK = 0,
		S_FALSE = 1,
		E_NOINTERFACE = unchecked((int)0x80004002),
		E_NOTIMPL = unchecked((int)0x80004001),
		E_FAIL = unchecked((int)0x80004005),
	}
	[ComImport]
	[Guid("00000122-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDropTarget
	{
		HRESULT DragEnter(
		[In] System.Runtime.InteropServices.ComTypes.IDataObject pDataObj,
		[In] int grfKeyState,
		[In] Point pt,
		[In, Out] ref int pdwEffect);
		HRESULT DragOver(
		[In] int grfKeyState,
		[In] Point pt,
		[In, Out] ref int pdwEffect);
		HRESULT DragLeave();
		HRESULT Drop(
		[In] System.Runtime.InteropServices.ComTypes.IDataObject pDataObj,
		[In] int grfKeyState,
		[In] Point pt,
		[In, Out] ref int pdwEffect);
	}
	public const int DROPEFFECT_NONE = (0);
	[DllImport("Shell32.dll", CharSet = CharSet.Unicode, SetLastError = true, EntryPoint = "#740")]
	public static extern HRESULT SHCreateFileDataObject(IntPtr pidlFolder, uint cidl, IntPtr[] apidl, System.Runtime.InteropServices.ComTypes.IDataObject pdtInner, out System.Runtime.InteropServices.ComTypes.IDataObject ppdtobj);
	[DllImport("Shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern HRESULT SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);
	[DllImport("Shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern IntPtr ILFindLastID(IntPtr pidl);
	[DllImport("Shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern IntPtr ILClone(IntPtr pidl);
	[DllImport("Shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern Boolean ILRemoveLastID(IntPtr pidl);
	[DllImport("Shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern void ILFree(IntPtr pidl);
	Guid CLSID_MapiMail = new Guid("9E56BE60-C50F-11CF-9A2C-00A0C90A90CE");

	public void SendMail(string filePath)
	{
		HRESULT hr = HRESULT.E_FAIL;
		uint rgflnOut = 0;
		System.Runtime.InteropServices.ComTypes.IDataObject pDataObject;
		IntPtr pidlParent = IntPtr.Zero, pidlFull = IntPtr.Zero, pidlItem = IntPtr.Zero;
		var aPidl = new IntPtr[255];
		hr = SHILCreateFromPath(filePath, out pidlFull, ref rgflnOut);
		if (hr == HRESULT.S_OK)
		{
			pidlItem = ILFindLastID(pidlFull);
			aPidl[0] = ILClone(pidlItem);
			ILRemoveLastID(pidlFull);
			pidlParent = ILClone(pidlFull);
			ILFree(pidlFull);
			hr = SHCreateFileDataObject(pidlParent, 1, aPidl, null, out pDataObject);
			if (hr == HRESULT.S_OK)
			{
				Type DropTargetType = Type.GetTypeFromCLSID(CLSID_MapiMail, true);
				object DropTarget = Activator.CreateInstance(DropTargetType);
				IDropTarget pDropTarget = (IDropTarget)DropTarget;
				int pdwEffect = DROPEFFECT_NONE;
				Point pt = new Point();
				pt.X = 0;
				pt.Y = 0;
				hr = pDropTarget.Drop(pDataObject, 0, pt, pdwEffect);
			}
			if (pidlParent != IntPtr.Zero)
				ILFree(pidlParent);
			if (aPidl[0] != IntPtr.Zero)
				ILFree(aPidl[0]);
		}

	}
}