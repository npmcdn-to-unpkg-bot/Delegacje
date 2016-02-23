using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Repository;
using CrazyAppsStudio.Delegacje.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace CrazyAppsStudio.Delegacje.Tasks
{
    public class DictionariesTasks
    {
        private Repositories repo;

		private CurrenciesTasks currenciesTasks;

        public DictionariesTasks()
        {
            repo = new Repositories();
			currenciesTasks = new CurrenciesTasks(repo);

        }

        public DictionariesDTO GetDictionaries()
        {
            List<CurrencyRateDTO> currencies = currenciesTasks.GetLatestAndRefreshCurrencyRates().Select(cr => cr.MapToDTO()).ToList();
            currencies.Add(currenciesTasks.GetPLN());

            DictionariesDTO result = new DictionariesDTO()
            {
                Countries = repo.Dictionaries.GetCountries(),
                ExpenseDocumentTypes = repo.Dictionaries.GetExpenseDocumentTypes(),
                ExpenseTypes = repo.Dictionaries.GetExpenseTypes(),
                VehicleTypes = repo.Dictionaries.GetVehicleTypes(),
				Currencies = currencies
            };

            return result;
        }
    }
}
