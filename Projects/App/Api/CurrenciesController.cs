using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrazyAppsStudio.Delegacje.Domain.Extensions;

namespace CrazyAppsStudio.Delegacje.App.Api
{
	[RoutePrefix("api/currencies")]
	[Authorize]
	public class CurrenciesController : BaseProfileController
    {
		private readonly ITasksRepository tasks;

		public CurrenciesController(ITasksRepository tasks)
        {
            this.tasks = tasks;
        }

		[Route("{currencyCode}/{date}")]
		[HttpGet]
		public CurrencyRateDTO GetCurrencyRateForDate(string currencyCode, DateTime date)
		{
			return tasks.CurrenciesTasks.GetCurrencyRateForDay(currencyCode, date).MapToDTO();
		}

		[Route("{date}")]
		[HttpGet]
		public CurrencyRateDTO[] GetAllCurrencyRatesForDate(DateTime date)
		{
			return tasks.CurrenciesTasks.GetAllCurrencyRatesForDay(date).Select(cr => cr.MapToDTO()).ToArray();
		}
    }
}
