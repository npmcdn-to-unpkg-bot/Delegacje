using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CrazyAppsStudio.Delegacje.App.Startup))]

namespace CrazyAppsStudio.Delegacje.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
