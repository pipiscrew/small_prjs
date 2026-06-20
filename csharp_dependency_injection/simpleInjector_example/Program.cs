using Serilog;
using SimpleInjector;
using System;
using System.Windows.Forms;

namespace posokanei
{
    static class Program
    {
        static readonly Container container;

        static Program()
        {
            container = new Container();
        }

        [STAThread]
        static void Main()
        {
            //setup serilog
            //https://www.nuget.org/packages/Serilog/2.12.0
            //https://www.nuget.org/packages/Serilog.Sinks.File/2.1.0
            var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(
                path: @"C:\Logs\app-.log",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

            Serilog.Log.Logger = logger;

            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //

            //register for DI see ServicesRegistrationExtension.cs
            container
                .AddApplicationServices()
                .AddDatabase()
                .AddSerilog()
                .AutoRegisterWindowsForms();

            //as per book
            container.Verify();

            Log.Logger.Information("dfgfdG");
            //Log.CloseAndFlush();

            Application.Run(container.GetInstance<Form1>());
        }
    }
}
