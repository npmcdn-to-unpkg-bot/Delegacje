using System.Web.Http;
using System.Web.Mvc;

namespace CrazyAppsStudio.Delegacje.App
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            Bootstrapper.Initialise();
        }
    }
}
