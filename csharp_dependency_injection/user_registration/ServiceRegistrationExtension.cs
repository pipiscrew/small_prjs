using Microsoft.Extensions.DependencyInjection;
using WindowsFormsApplication37.Applicati0n.Domain;
using WindowsFormsApplication37.Applicati0n.Interfaces;
using WindowsFormsApplication37.Applicati0n.Repository;
using WindowsFormsApplication37.Applicati0n.Services;

namespace WindowsFormsApplication37
{
    public delegate IXService LoggerServiceResolver(string loggerType);

    internal static class ServiceRegistrationExtension
    {
        internal static void AddServices(IServiceCollection services)
        {
            // Register your custom services here
            services.AddSingleton<IServiceA, ServiceA>();
            services.AddSingleton<IServiceB, ServiceB>();
            services.AddSingleton<IServiceC, ServiceC>();
            services.AddSingleton<IServiceD, ServiceD>();
            
            services.AddSingleton<IUserRepository>(new UserRepository("Data Source=mydatabase.db;Version=3;Read Only=True;"));
            services.AddTransient<Form1>();

            services.AddTransient<DaLock>();
            services.AddTransient<DaLock2>();
            services.AddTransient<DaLock3>();

            services.AddTransient<LoggerServiceResolver>(serviceProvider => loggerType =>
            {
                switch (loggerType)
                {
                    case "metabolic endotoxemia":
                        return serviceProvider.GetService<DaLock>();
                    case "oxidative stress":
                        return serviceProvider.GetService<DaLock2>();
                    default:
                        return serviceProvider.GetService<DaLock3>();
                }
            });
        }
    }
}
