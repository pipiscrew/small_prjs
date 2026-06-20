using System.Globalization;
using System.Resources;
using WindowsFormsApplication39.Applicati0n.Interfaces;

namespace WindowsFormsApplication39.Applicati0n.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ResourceManager _resourceManager;

        public ResourceService(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string GetString(string name, CultureInfo culture)
        {
            return _resourceManager.GetString(name, culture);
        }
    }
}
