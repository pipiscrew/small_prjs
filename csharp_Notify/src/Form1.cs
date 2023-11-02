using Compact_RAM_Cleaner;
using System;
using System.Windows.Forms;

namespace qualcosaNotify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Notify("Eat Rich, Live Long (2018)").Show();
        }
    }
}
