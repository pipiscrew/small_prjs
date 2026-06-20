using posokanei.Infrastructure.Database.Factory;
using posokanei.Interfaces.Repositories;
using posokanei.Interfaces.Services;
using posokanei.Services;
using Serilog;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using System.Windows.Forms;

namespace posokanei
{
    public static class ServicesRegistrationExtension
    {
        //https://docs.simpleinjector.org/en/latest/windowsformsintegration.html
        public static Container AutoRegisterWindowsForms(this Container container)
        {
            var types = container.GetTypesToRegister<Form>(typeof(Program).Assembly);

            foreach (var type in types)
            {
                var registration =
                    Lifestyle.Transient.CreateRegistration(type, container);

                registration.SuppressDiagnosticWarning(
                    DiagnosticType.DisposableTransientComponent,
                    "Forms should be disposed by app code; not by the container.");

                container.AddRegistration(type, registration);
            }
            return container;
        }

        public static Container AddApplicationServices(this Container container)
        {
            container.Register<IAPICommands, APICommands>(Lifestyle.Singleton);
            container.Register<IServiceProduct, ServiceProduct>(Lifestyle.Singleton);

            return container;
        }

        public static Container AddDatabase(this Container container)
        {
            string cs = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\dbase.db;Version=3;Pooling=True;";
            container.RegisterInstance<ISqliteConnectionFactory>(new SqliteConnectionFactory(cs));

            // example for 2nd connection
            // container.Register<IMssqlConnectionFactory>(Lifestyle.Singleton);

            return container;
        }

        public static Container AddSerilog(this Container container)
        {
            container.RegisterInstance<Serilog.ILogger>(Log.Logger);

            return container;
        }
    }
}
