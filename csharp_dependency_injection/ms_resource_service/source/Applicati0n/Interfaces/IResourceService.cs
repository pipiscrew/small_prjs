using System.Globalization;

namespace WindowsFormsApplication39.Applicati0n.Interfaces
{
    public interface IResourceService
    {
        string GetString(string name, CultureInfo culture);
    }
}
