using System;
using System.Windows.Forms;

namespace NHibernateByCodeSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmMain());

            //see also
            //Application.Run(new frmMainBinding());
        }
    }
}
