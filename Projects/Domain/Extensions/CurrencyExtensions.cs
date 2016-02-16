using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
	public static class CurrencyExtensions
	{
		public static CurrencyDTO MapToDetails(this Currency currency)
		{
			CurrencyDTO currencyDTO = new CurrencyDTO()
			{
				Code = currency.Code,
				ExchangeRate = currency.ExchangeRate,
				Name = currency.Name
			};

			return currencyDTO;
		}
	}
}
