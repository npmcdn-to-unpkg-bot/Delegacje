using log4net;
using log4net.Config;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace CrazyAppsStudio.Delegacje.App
{
    public class WebApiApplication : System.Web.HttpApplication
    {
		private readonly ILog logger = LogManager.GetLogger(typeof(WebApiApplication));

        protected void Application_Start()
        {
			XmlConfigurator.Configure();

			logger.Info("Ichor Client application starting");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            Bootstrapper.Initialise();
        }
    }
}
