using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinApi
{
	
	public sealed class UnmanagedPointer : IDisposable
	{
		
		public UnmanagedPointer(IntPtr ptr, AllocMethod method)
		{
			this.m_meth = method;
			this.m_ptr = ptr;
		}

		
		public void Dispose()
		{
			this.Dispose(true);
		}

		
		private void Dispose(bool disposing)
		{
			if (this.m_ptr != IntPtr.Zero)
			{
				if (this.m_meth == AllocMethod.HGlobal)
				{
					Marshal.FreeHGlobal(this.m_ptr);
				}
				else if (this.m_meth == AllocMethod.CoTaskMem)
				{
					Marshal.FreeCoTaskMem(this.m_ptr);
				}
				this.m_ptr = IntPtr.Zero;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		
		~UnmanagedPointer()
		{
			this.Dispose(false);
		}

		
		
		public static implicit operator IntPtr(UnmanagedPointer ptr)
		{
			return ptr.m_ptr;
		}

		
		private AllocMethod m_meth;

		
		private IntPtr m_ptr;
	}
}
