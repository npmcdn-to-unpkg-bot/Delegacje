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

            //this is needed by the sql spatial types so the DbGeography methods work well on machines without sql sever installed
            SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
        }
    }
}
