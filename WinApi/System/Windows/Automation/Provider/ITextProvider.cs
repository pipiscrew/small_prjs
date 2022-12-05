using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface ITextProvider
	{
		
		ITextRangeProvider[] GetSelection();

		
		ITextRangeProvider[] GetVisibleRanges();

		
		ITextRangeProvider RangeFromChild(IRawElementProviderSimple childElement);

		
		ITextRangeProvider RangeFromPoint(Point screenLocation);

		
		ITextRangeProvider DocumentRange
		{
			
			get;
		}

		
		SupportedTextSelection SupportedTextSelection
		{
			
			get;
		}
	}
}
