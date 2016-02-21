using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
	public class CurrenciesRepository
	{
		private BusinessTripsContext context;

		public CurrenciesRepository(BusinessTripsContext _context)
		{
			this.context = _context;
		}

		public CurrencyRate GetCurrencyRate(string code, DateTime date)
		{
			return this.context.CurrencyRates.Where(cr => cr.Currency.Code == code && cr.DateRefreshed == date).FirstOrDefault();
		}

		public CurrencyRate GetLastCurrencyRate(string code)
		{
			return this.context.CurrencyRates.Where(cr => cr.Currency.Code == code).OrderByDescending(cr => cr.DateRefreshed).FirstOrDefault();
		}

		public IEnumerable<CurrencyRate> GetAllRatesForDay(DateTime date)
		{
			return this.context.CurrencyRates.Where(cr => cr.DateRefreshed == date);
		}

		public void AddCurrencyRates(IEnumerable<CurrencyRate> currencyRates)
		{
			this.context.CurrencyRates.AddRange(currencyRates);
		}		
	}
}
