
using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Repository;

namespace CrazyAppsStudio.Delegacje.Tasks
{
    public class DictionariesTasks
    {
        private Repositories repo;

        public DictionariesTasks()
        {
            repo = new Repositories();
        }

        public DictionariesDTO GetDictionaries()
        {
            DictionariesDTO result = new DictionariesDTO()
            {
                Countries = repo.Dictionaries.GetCountries(),
                ExpenseDocumentTypes = repo.Dictionaries.GetExpenseDocumentTypes(),
                ExpenseTypes = repo.Dictionaries.GetExpenseTypes(),
                MealTypes = repo.Dictionaries.GetMealTypes(),
                VehicleTypes = repo.Dictionaries.GetVehicleTypes()
            };

            return result;
        }
    }
}
