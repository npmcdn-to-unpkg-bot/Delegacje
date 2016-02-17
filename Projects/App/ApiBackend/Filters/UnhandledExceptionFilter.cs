using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace CrazyAppsStudio.Delegacje.App.ApiBackend.Filters
{
	public class UnhandledExceptionFilter : ExceptionFilterAttribute
	{
		private readonly ILog logger = LogManager.GetLogger(typeof(UnhandledExceptionFilter));

		public override void OnException(HttpActionExecutedContext context)
		{
			logger.Fatal("Unhandled exception was caught. Description below:", context.Exception);
		}
	}
}