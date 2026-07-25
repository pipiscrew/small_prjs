using App.Dialogs;
using App.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace posokanei2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create a service collection
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddSerilog(Path.Combine(Application.StartupPath, "logs.txt"))
                .AddDatabase()
                .AddApplicationServices()
                .AddForms();
          

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Log.Logger.Information("Application starting");

            // example use of service - log user IP
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //var logIP = serviceProvider.GetRequiredService<IAPIService>();
            //Task.Run(() => logIP.LogUserIPAsync());

            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //

            var mainForm = serviceProvider.GetRequiredService<MainForm>();

            Application.Run(mainForm);
        }
    }
}
