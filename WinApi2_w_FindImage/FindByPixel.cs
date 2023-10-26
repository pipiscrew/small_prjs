using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FindImage
{
    class FindByPixel
    {
        public FindByPixel()
        {
            this.RectangleToSearchIn = default(Rectangle);
        }

        public static Point? FindPixel(bool foregroundWindowOnly, bool searchInRect, Rectangle rectangleToSearchIn, uint color)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            int num;
            int num2;
            int num3;
            int num4;
            if (foregroundWindowOnly)
            {
                WinApi.RECT rECT;
                WinApi.GetWindowRect(WinApi.GetForegroundWindow(), out rECT);
                num = rECT.Left;
                num2 = rECT.Top;
                num3 = rECT.Right;
                num4 = rECT.Bottom;
            }
            else if (searchInRect)
            {
                num = rectangleToSearchIn.Left;
                num2 = rectangleToSearchIn.Top;
                num3 = rectangleToSearchIn.Right;
                num4 = rectangleToSearchIn.Bottom;
            }
            else
            {
                num = 0;
                num2 = 0;
                num3 = width - 1;
                num4 = height - 1;
            }
            if (num < 0)
            {
                num = 0;
            }
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (num3 >= width)
            {
                num3 = width - 1;
            }
            if (num4 >= height)
            {
                num4 = height - 1;
            }
            bool flag = false;
            byte[] screenshotRgbBytes = FindByPixel.GetScreenshotRgbBytes();
            int i = 0;
            int j;
            for (j = num2; j <= num4; j++)
            {
                for (i = num; i <= num3; i++)
                {
                    int num5 = (j * width + i) * 3;
                    uint arg_F3_0 = FindByPixel.RGB(screenshotRgbBytes[num5 + 2], screenshotRgbBytes[num5 + 1], screenshotRgbBytes[num5]);
                    Application.DoEvents();
                    if (arg_F3_0 == color)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            if (flag)
            {
                return new Point?(new Point(i, j));
            }
            return null;
        }


        private static byte[] GetScreenshotRgbBytes()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            IntPtr arg_9C_0 = bitmapData.Scan0;
            int num = bitmapData.Stride * bitmap.Height;
            byte[] array = new byte[num];
            Marshal.Copy(arg_9C_0, array, 0, num);
            bitmap.UnlockBits(bitmapData);
            return array;
        }

        public int? ProcessCommand()
        {
            Point? point = FindByPixel.FindPixel(this.ForegroundWindowOnly, this.SearchInRect, this.RectangleToSearchIn, this.Color);
            if (point.HasValue)
            {
                WinApi.SetCursorPos(point.Value.X, point.Value.Y);
            }
            return null;
        }

        private static uint RGB(byte red, byte green, byte blue)
        {
            return (uint)blue + 256u * (uint)green + 65536u * (uint)red;
        }

        public override string ToString()
        {
            return string.Concat(new string[]
			{
				"FIND PIXEL : ",
				this.Color.ToString(),
				" : ",
				this.ForegroundWindowOnly ? "1" : "0",
				" : ",
				this.SearchInRect ? "1" : "0",
				" : ",
				this.RectangleToSearchIn.Top.ToString(),
				" : ",
				this.RectangleToSearchIn.Left.ToString(),
				" : ",
				this.RectangleToSearchIn.Bottom.ToString(),
				" : ",
				this.RectangleToSearchIn.Right.ToString()
			});
        }

        public uint Color
        {
            get;
            set;
        }


        public bool ForegroundWindowOnly
        {        
            get;
            set;
        }

        public Rectangle RectangleToSearchIn
        {
            get;
            set;
        }

        public bool SearchInRect
        {
            get;
            set;
        }
    }
}

