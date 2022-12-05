using System;
using System.Drawing;

namespace WinApi
{
	
	public struct RECT
	{
		
		public RECT(Rectangle r)
		{
			this.left = r.Left;
			this.top = r.Top;
            this.right = r.Right;
			this.bottom = r.Bottom;
		}

		
		public RECT(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		
		public Rectangle ToRectangle()
		{
			return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
		}

		
		public Size Size
		{
			
			get
			{
				return new Size(this.right - this.left, this.bottom - this.top);
			}
		}

		
		public int left;

		
		public int top;

		
		public int right;

		
		public int bottom;
	}
}
