using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: System.Reflection.Obfuscation(Feature = "embed satellites", Exclude = false)]
namespace WindowsFormsApplication39
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create a service collection
            var serviceCollection = new ServiceCollection();

            ServiceRegistrationExtension.AddServices(serviceCollection);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Resolve the main form and show it
            var mainForm = serviceProvider.GetRequiredService<Form1>();

            Application.Run(mainForm);
        }
    }
}
