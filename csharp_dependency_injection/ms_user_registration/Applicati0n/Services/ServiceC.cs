using System;
using System.Diagnostics;
using WindowsFormsApplication37.Applicati0n.Interfaces;

namespace WindowsFormsApplication37.Applicati0n.Services
{
    public interface IServiceC
    {
        void Validate();
    }

    public class ServiceC : IServiceC
    {
        private readonly IUserRepository _u;

        public ServiceC(IUserRepository u)
        {
            _u = u;

            Process currentProcess = Process.GetCurrentProcess();
# if !DEBUG
            if ((currentProcess.ProcessName + ".exe").ToLower() != "windowsformsapplication37.exe")
                Environment.Exit(-9999);
#endif
        }

        public void Validate()
        {
            throw new Exception("not implemented");
        }
    }
}
