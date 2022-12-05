using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	[CoClass(typeof(FileOpenDialogRCW)), Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
	
	public interface NativeFileOpenDialog : IFileOpenDialog, IFileDialog, IModalWindow
	{
	}
}
