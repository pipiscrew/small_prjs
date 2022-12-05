using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WinApi
{
	
	
	
	public interface IAsyncOperation
	{
		
		void SetAsyncMode([In] int fDoOpAsync);

		
		void GetAsyncMode(out int pfIsOpAsync);

		
		void StartOperation([In] IBindCtx pbcReserved);

		
		void InOperation(out int pfInAsyncOp);

		
		void EndOperation([In] int hResult, [In] IBindCtx pbcReserved, [In] uint dwEffects);
	}
}
