using CrazyAppsStudio.Delegacje.DomainModel;
using log4net;
using log4net.Config;
using System;
using System.Data.Entity;
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

            System.Diagnostics.Trace.TraceInformation("Delegacje startują");            

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Database.SetInitializer<BusinessTripsContext>(new DbInitializer());
            //Database.SetInitializer<BusinessTripsContext>(new DropCreateDatabaseAlways<BusinessTripsContext>());

            Bootstrapper.Initialise();
        }
    }
}
