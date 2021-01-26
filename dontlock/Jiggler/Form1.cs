using ArkaneSystems.MouseJiggle;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace dontlockJiggler
{
    public partial class Form1 : Form
    {
        private Timer Timer;

        public Form1()
        {
            InitializeComponent();

            notifyIcon1.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            notifyIcon1.Text = "Jiggler code by Arkane Systems";

            Timer = new Timer() { Interval = 1000 * 40 };

            Timer.Tick += delegate(object sender, EventArgs e)
            {
                Jiggler.Jiggle(0, 0);
            };

            Timer.Start();
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }


}
