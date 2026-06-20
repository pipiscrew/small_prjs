using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication37
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
