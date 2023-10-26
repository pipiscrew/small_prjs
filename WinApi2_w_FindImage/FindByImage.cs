using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class FindByImage
{
    public FindByImage() { }

    //public FindByImage(string base64Image)
    //{
    //    if (!string.IsNullOrEmpty(base64Image))
    //    {
    //        using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(base64Image)))
    //        {
    //            this.ImageToFind = (Bitmap)Image.FromStream(memoryStream);
    //        }
    //    }
    //}

    public FindByImage(string imagePath)
    {
        this.ImageToFind = new Bitmap(imagePath);
    }

    public static string GetBitmapAsBase64(Bitmap bmp)
    {
        if (bmp == null)
        {
            return "";
        }
        Image arg_1B_0 = new Bitmap(bmp);
        MemoryStream memoryStream = new MemoryStream();
        arg_1B_0.Save(memoryStream, ImageFormat.Bmp);
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static Bitmap GetScreenshotFromRect(Rectangle rectangle)
    {
        Point pt;
        WinApi.GetCursorPos(out pt);
        if (FindByImage.IsPointWihtinRect(pt, rectangle))
        {
            int num = rectangle.X - 100;
            int num2 = rectangle.Y;
            if (num < 0)
            {
                num = rectangle.Right + 100;
            }
            if (num2 < 0)
            {
                num2 = rectangle.Bottom / 2;
            }
            WinApi.SetCursorPos(num, num2);
        }
        Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format24bppRgb);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
        }
        WinApi.SetCursorPos(pt.X, pt.Y);
        return bitmap;
    }
    private static bool IsPointWihtinRect(Point pt, Rectangle rect)
    {
        return pt.X >= rect.Left && pt.Y >= rect.Top && pt.X <= rect.Right && pt.Y <= rect.Bottom;
    }

    public int? ProcessCommand()
    {
        if (this.ImageToFind == null)
        {
            return null;
        }
        Rectangle rectangle;
        if (this.ForegroundWindowOnly)
        {
            WinApi.RECT rECT;
            WinApi.GetWindowRect(WinApi.GetForegroundWindow(), out rECT);
            rectangle = new Rectangle(rECT.Left, rECT.Top, rECT.Right - rECT.Left + 1, rECT.Bottom - rECT.Top + 1);
        }
        else
        {
            rectangle = (this.SearchRegion.HasValue ? this.SearchRegion.Value : new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
        }
        using (Bitmap screenshotFromRect = FindByImage.GetScreenshotFromRect(rectangle))
        {
            Rectangle left = FindByImage.SearchBitmap(this.ImageToFind, screenshotFromRect, this.Similarity);
            if (left != Rectangle.Empty)
            {
                WinApi.SetCursorPos(left.X + left.Width / 2 + rectangle.X, left.Y + left.Height / 2 + rectangle.Y);
            }
        }
        return null;
    }
    public static Rectangle SearchBitmap(Bitmap smallBmp, Bitmap bigBmp, int similarity = 0)
    {
        int width = bigBmp.Width;
        int num = bigBmp.Height - smallBmp.Height + 1;
        int num2 = smallBmp.Width * 3;
        int height = smallBmp.Height;
        Rectangle empty = Rectangle.Empty;
        object lockObject = FindByImage._lockObject;
        lock (lockObject)
        {
            BitmapData bitmapData = smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bitmapData2 = bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int stride = bitmapData.Stride;
            int stride2 = bitmapData2.Stride;
            byte[] array = new byte[bitmapData2.Stride * bitmapData2.Height];
            byte[] array2 = new byte[bitmapData.Stride * bitmapData.Height];
            Marshal.Copy(bitmapData2.Scan0, array, 0, bitmapData2.Stride * bitmapData2.Height);
            Marshal.Copy(bitmapData.Scan0, array2, 0, bitmapData.Stride * bitmapData.Height);
            int num3 = 0;
            int arg_109_0 = smallBmp.Width;
            int num4 = stride2 - bigBmp.Width * 3;
            bool flag2 = true;
            for (int i = 0; i < num; i++)
            {
                Application.DoEvents();

                for (int j = 0; j < width; j++)
                {
                    int num5 = num3;
                    int num6 = 0;
                    flag2 = true;
                    for (int k = 0; k < height; k++)
                    {
                        for (int l = 0; l < num2; l++)
                        {
                            int num7 = (int)(array2[num6] - array[num3]);
                            int num8 = 1 + similarity;
                            if (num7 > num8 || num7 < -num8)
                            {
                                flag2 = false;
                                break;
                            }
                            num3++;
                            num6++;
                        }
                        if (!flag2)
                        {
                            break;
                        }
                        num6 = stride * (1 + k);
                        num3 = num5 + stride2 * (1 + k);
                    }
                    if (flag2)
                    {
                        empty.X = j;
                        empty.Y = i;
                        empty.Width = smallBmp.Width;
                        empty.Height = smallBmp.Height;
                        break;
                    }
                    num3 = num5;
                    num3 += 3;
                }
                if (flag2)
                {
                    break;
                }
                num3 += num4;
            }
            bigBmp.UnlockBits(bitmapData2);
            smallBmp.UnlockBits(bitmapData);
        }
        return empty;
    }

    public override string ToString()
    {
        return string.Concat(new string[]
			{
				"FIND IMAGE : ",
				FindByImage.GetBitmapAsBase64(this.ImageToFind),
				" : ",
				this.ForegroundWindowOnly ? "1" : "0",
				" : ",
				this.Similarity.ToString(),
				" : ",
				this.SearchRegion.HasValue ? string.Concat(new string[]
				{
					this.SearchRegion.Value.Left.ToString(),
					";",
					this.SearchRegion.Value.Top.ToString(),
					";",
					this.SearchRegion.Value.Width.ToString(),
					";",
					this.SearchRegion.Value.Height.ToString()
				}) : ""
			});
    }


    public bool ForegroundWindowOnly
    {
        get;
        set;
    }
    public Bitmap ImageToFind
    {
        get;
        set;
    }
    public Rectangle? SearchRegion
    {
        get;
        set;
    }
    public int Similarity
    {
        get;
        set;
    }
    private static readonly object _lockObject = new object();
}

