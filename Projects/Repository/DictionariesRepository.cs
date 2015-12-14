using CrazyAppsStudio.Delegacje.Domain;
using CrazyAppsStudio.Delegacje.Domain.Extensions;
using CrazyAppsStudio.Delegacje.DomainModel;
using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using System.Collections.Generic;

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
            return this.context.Countries.AsEnumerable();
        }

        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return this.context.VehicleTypes.AsEnumerable();
        }

        public IEnumerable<ExpenseType> GetExpenseTypes()
        {
            return this.context.ExpenseTypes.AsEnumerable();
        }

        public IEnumerable<ExpenseDocumentType> GetExpenseDocumentTypes()
        {
            return this.context.ExpenseDocumentTypes.AsEnumerable();
        }

        public IEnumerable<MealType> GetMealTypes()
        {
            return this.context.MealTypes.AsEnumerable();
        }
    }
}
