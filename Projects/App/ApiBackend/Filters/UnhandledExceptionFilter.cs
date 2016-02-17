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
		public override void OnException(HttpActionExecutedContext context)
		{
            System.Diagnostics.Trace.TraceError("Błąd aplikacji", context.Exception);
        }
	}
}