using CrazyAppsStudio.Delegacje.Domain;
using CrazyAppsStudio.Delegacje.Domain.Extensions;
using CrazyAppsStudio.Delegacje.DomainModel;
using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using System.Collections.Generic;
using System;

namespace CrazyAppsStudio.Delegacje.Repository
{
    public class DictionariesRepository
    {
        private BusinessTripsContext context;

        public DictionariesRepository(BusinessTripsContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<Country> GetCountries()
        {
            List<Country> countries = this.context.Countries
                .OrderBy(c => c.Name)
                .ToList();

            var pl = countries.Where(c => c.Name == "Polska").First();
            countries.Remove(pl);
            countries.Insert(0, pl);

            return countries.AsEnumerable();
        }

		public Country GetCountry(int countryId)
		{
			return this.context.Countries.FirstOrDefault(c => c.Id == countryId);
		}

        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return this.context.VehicleTypes.AsEnumerable();
        }

		public VehicleType GetVehicleType(int vehicleTypeId)
		{
			return this.context.VehicleTypes.FirstOrDefault(v => v.Id == vehicleTypeId);
		}

        public IEnumerable<ExpenseType> GetExpenseTypes()
        {
            return this.context.ExpenseTypes.AsEnumerable();
        }

		public ExpenseType GetExpenseType(int expenseTypeId)
		{
			return this.context.ExpenseTypes.FirstOrDefault(e => e.Id == expenseTypeId);
		}

        public IEnumerable<ExpenseDocumentType> GetExpenseDocumentTypes()
        {
            return this.context.ExpenseDocumentTypes.AsEnumerable();
        }

        public ExpenseDocumentType GetExpenseDocumentType(int expenseDocumentTypeId)
        {
            return this.context.ExpenseDocumentTypes.FirstOrDefault(e => e.Id == expenseDocumentTypeId);
        }

        public IEnumerable<MealType> GetMealTypes()
        {
            return this.context.MealTypes.AsEnumerable();
        }

		public MealType GetMealType(int mealTypeId)
		{
			return this.context.MealTypes.FirstOrDefault(m => m.Id == mealTypeId);
		}

		public IEnumerable<Currency> GetCurrencies()
		{
			return this.context.Currencies.AsEnumerable();
		}

		public void AddCurrencies(IEnumerable<Currency> currencies)
		{
			this.context.Currencies.AddRange(currencies);
		}		
    }
}
