using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication37.Applicati0n.Services
{
    public interface IServiceD
    {
        void Validate();
    }

    public class ServiceD : IServiceD
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern int NtQueryInformationProcess(IntPtr processHandle,
           int processInformationClass, int[] processInformation, uint processInformationLength,
           ref int returnLength);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_CLOSE = 0x0010;

        public ServiceD()
        {
# if !DEBUG
            if (Debugger.IsLogging())
            {
                AutoCloseMessageBox("Debugger detected! Program will exit now!");
                //Environment.Exit(0);
            }
            if (Debugger.IsAttached)
            {
                AutoCloseMessageBox("Debugger detected! Program will exit now!");
                //Environment.Exit(0);
            }
            if (Environment.GetEnvironmentVariable("complus_profapi_profilercompatibilitysetting") != null)
            {
                AutoCloseMessageBox("Profiler detected! Program will exit now!");
                //Environment.Exit(0);
            }
            if (Environment.GetEnvironmentVariable("COR_ENABLE_PROFILING") == "1")
            {
                AutoCloseMessageBox("Profiler detected! Program will exit now!");
                //Environment.Exit(0);
            }
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                int[] numArray = new int[6];
                int num = 0;
                IntPtr handle = Process.GetCurrentProcess().Handle;
                if ((NtQueryInformationProcess(handle, 0x1f, numArray, 4, ref num) == 0) && (numArray[0] != 1))
                {
                    AutoCloseMessageBox("Debugger detected! Program will exit now!");
                    //Environment.Exit(0);
                }
                if ((NtQueryInformationProcess(handle, 30, numArray, 4, ref num) == 0) && (numArray[0] != 0))
                {
                    AutoCloseMessageBox("Debugger detected! Program will exit now!");
                    //Environment.Exit(0);
                }
            }
#endif
        }
    

        internal async static void AutoCloseMessageBox(string message)
        {
            //https://stackoverflow.com/a/74603316
                var owner = new Form { Visible = false };
                // Force the creation of the window handle.
                // Otherwise the BeginInvoke will not work.
                var handle = owner.Handle;
                owner.BeginInvoke((MethodInvoker)delegate 
                {
                    MessageBox.Show(owner,message);
                });
                await Task.Delay(TimeSpan.FromSeconds(1));
                Environment.Exit(-33);
                //owner.Dispose();


        }

        public void Validate()
        {
            throw new Exception("not implemented");
        }
    }
}

