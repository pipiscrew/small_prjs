using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(north.Startup))]
namespace north
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
