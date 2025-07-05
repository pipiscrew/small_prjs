using Microsoft.Extensions.DependencyInjection;
using System.Resources;
using WindowsFormsApplication39.Applicati0n.Interfaces;
using WindowsFormsApplication39.Applicati0n.Services;

namespace WindowsFormsApplication39
{
    internal static class ServiceRegistrationExtension
    {
        internal static void AddServices(IServiceCollection services)
        {
            // Register your custom services here

            var resourceManager = new ResourceManager(typeof(WindowsFormsApplication39.Properties.Resources));
                
            services.AddSingleton(resourceManager);
            services.AddTransient<IResourceService, ResourceService>();

            services.AddTransient<Form1>();
        }
    }
}
