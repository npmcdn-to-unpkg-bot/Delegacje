using CrazyAppsStudio.Delegacje.App.ApiBackend.Filters;
using CrazyAppsStudio.Delegacje.Domain.Utils;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CrazyAppsStudio.Delegacje.App
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //cors enabled
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Enforce HTTPS            
            if (Settings.EnforceHTTPS)
                config.Filters.Add(new CrazyAppsStudio.Delegacje.App.Filters.RequireHttpsAttribute());
			config.Filters.Add(new UnhandledExceptionFilter());
        }
    }
}
