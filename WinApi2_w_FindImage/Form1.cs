using System;
using System.Windows.Forms;

namespace FindImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindByImage x = new FindByImage(Application.StartupPath + "\\win10start.png");
            //x.ForegroundWindowOnly = false;
            //x.Similarity = 0;
            x.ProcessCommand();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //can be also for specific pixel
            //if (9488341 == Extensions.SwitchColorFormatBGRRGB(WinApi.GetPixelUintColor(this.X, this.Y)))

            FindByPixel x = new FindByPixel();
            x.ForegroundWindowOnly = false;
            x.SearchInRect = false;
            //x.RectangleToSearchIn;
            x.Color = 9488341;  //http://www.shodor.org/~efarrow/trunk/html/rgbint.html
            x.ProcessCommand();
        }
    }
}
