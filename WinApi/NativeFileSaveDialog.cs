using System;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	[CoClass(typeof(FileSaveDialogRCW)), Guid("84bccd23-5fde-4cdb-aea4-af64b83d78ab")]
	
	public interface NativeFileSaveDialog : IFileSaveDialog, IFileDialog, IModalWindow
	{
	}
}
