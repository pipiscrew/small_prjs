using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace dontlock
{
    public partial class Form1 : Form
    {
        //src - https://stackoverflow.com/a/11870224
        //https://dlaa.me/blog/post/9901642 (didnt work)
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        public Form1()
        {
            InitializeComponent();

            notifyIcon1.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate

            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }
   
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }


}
