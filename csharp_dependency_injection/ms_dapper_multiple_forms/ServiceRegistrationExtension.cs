using App.Dialogs;
using App.Helpers;
using App.Interfaces.Repositories;
using App.Interfaces.Services;
using App.Repositories;
using App.Services;
using Infrastructure.Database.Common;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace posokanei2
{
    internal static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IAPIService, APIService>(); //sample standalone service

            return services;
        }

        public static IServiceCollection AddForms(this IServiceCollection services)
        {
            services.AddTransient<MainForm>();
            services.AddTransient<frmCategory>();
            services.AddTransient<frmProduct>();
            services.AddTransient<frmCheckProducts>();
            

            // Manually register Func<T> factories
            services.AddTransient<Func<frmCheckProducts>>(sp => () => sp.GetRequiredService<frmCheckProducts>());
            services.AddTransient<Func<frmCategory>>(sp => () => sp.GetRequiredService<frmCategory>());
            services.AddTransient<Func<frmProduct>>(sp => () => sp.GetRequiredService<frmProduct>());

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // connections
            services.AddKeyedSingleton<IConnectionFactory, ConnectionFactory>("dbase1", (sp, _) =>
            {
                var connectionString = General.LoadSetting("dbase"); //load by app.config
                return new ConnectionFactory(connectionString);
            });

            // repositories
            services.AddSingleton<ICategoryRepository, CategoryRepository>(sp =>
            {
                var connectionFactory = sp.GetRequiredKeyedService<IConnectionFactory>("dbase1");
                return new CategoryRepository(connectionFactory);
            });
            services.AddSingleton<IProductRepository, ProductRepository>(sp =>
            {
                var connectionFactory = sp.GetRequiredKeyedService<IConnectionFactory>("dbase1");
                return new ProductRepository(connectionFactory);
            });

            return services;
        }

        public static IServiceCollection AddSerilog(this IServiceCollection services, string logFilepath)
        {
            var logger = new Serilog.LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File(
                            path: logFilepath,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                        )
                        .CreateLogger();

            /*
             *  is hybrid approach :
             *  Use constructor injection for forms and business logic classes and services,
             *  but you can still use the Log.Logger if you like (ex Log.Logger.Information("Starting executing");).
             */
            Serilog.Log.Logger = logger;

            services.AddSingleton<Serilog.ILogger>(logger);

            return services;
        }
    }
}
