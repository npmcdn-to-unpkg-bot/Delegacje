using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
	public static class CurrencyExtensions
	{
		public static CurrencyRateDTO MapToDetails(this CurrencyRate currencyRate)
		{
			CurrencyRateDTO currencyDTO = new CurrencyRateDTO()
			{
				Code = currencyRate.Currency.Code,
				CurrencyRateId = currencyRate.Id,
				Date = currencyRate.DateRefreshed.ToAppString(),
				ExchangeRate = currencyRate.ExchangeRate,
				Name = currencyRate.Currency.Name				
			};

			return currencyDTO;
		}
	}
}
