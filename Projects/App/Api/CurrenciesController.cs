﻿using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrazyAppsStudio.Delegacje.Domain.Extensions;
using Tools;

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
            if (currencyCode == "PLN")
            {
                return new CurrencyRateDTO()
                {
                    Code = "PLN",
                    CurrencyRateId = 0,
                    Date = date.ToAppString(),
                    ExchangeRate = 1,
                    Name = "złoty"
                };
            }

			return tasks.CurrenciesTasks.GetCurrencyRateForDay(currencyCode, date).MapToDTO();
		}

		[Route("forDate")]
		[HttpPost]
		public CurrencyRateDTO[] GetAllCurrencyRatesForDate([FromBody]string date)
		{
			DateTime dateParsed = date.ParseAppString();
			return tasks.CurrenciesTasks.GetAllCurrencyRatesForDay(dateParsed.Date).Select(cr => cr.MapToDTO()).ToArray();			
		}
    }
}
