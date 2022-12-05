using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace WinApi
{
	
	
	
	public interface IContextMenu
	{
		
		void QueryContextMenu(IntPtr hMenu, uint indexMenu, uint idCmdFirst, uint idCmdLast, uint uFlags);

		
		void InvokeCommand(ref CMINVOKECOMMANDINFO pici);

		
		void GetCommandString(int idcmd, uint uflags, int reserved, StringBuilder commandstring, int cch);
	}
}
