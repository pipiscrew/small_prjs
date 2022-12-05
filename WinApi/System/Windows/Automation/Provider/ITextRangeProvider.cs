using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Automation.Provider.Text;

namespace System.Windows.Automation.Provider
{
	
	
	
	public interface ITextRangeProvider
	{
		
		ITextRangeProvider Clone();

		
		[return: MarshalAs(UnmanagedType.Bool)]
		bool Compare(ITextRangeProvider range);

		
		int CompareEndpoints(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint);

		
		void ExpandToEnclosingUnit(TextUnit unit);

		
		ITextRangeProvider FindAttribute(int attribute, object value, [MarshalAs(UnmanagedType.Bool)] bool backward);

		ITextRangeProvider FindText(string text, [MarshalAs(UnmanagedType.Bool)] bool backward, [MarshalAs(UnmanagedType.Bool)] bool ignoreCase);

		
		object GetAttributeValue(int attribute);

		
		double[] GetBoundingRectangles();

		
		IRawElementProviderSimple GetEnclosingElement();

		
		string GetText(int maxLength);

		
		int Move(TextUnit unit, int count);

		
		int MoveEndpointByUnit(TextPatternRangeEndpoint endpoint, TextUnit unit, int count);

		
		void MoveEndpointByRange(TextPatternRangeEndpoint endpoint, ITextRangeProvider targetRange, TextPatternRangeEndpoint targetEndpoint);

		
		void Select();

		
		void AddToSelection();

		
		void RemoveFromSelection();

		
		void ScrollIntoView([MarshalAs(UnmanagedType.Bool)] bool alignToTop);

		
		IRawElementProviderSimple[] GetChildren();
	}
}
