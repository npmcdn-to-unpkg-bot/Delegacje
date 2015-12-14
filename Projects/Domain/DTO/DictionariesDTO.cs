using CrazyAppsStudio.Delegacje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
    public class DictionariesDTO
    {
        public IEnumerable<Country> Countries{ get; set; }

        public IEnumerable<VehicleType> VehicleTypes { get; set; }

        public IEnumerable<ExpenseType> ExpenseTypes { get; set; }

        public IEnumerable<ExpenseDocumentType> ExpenseDocumentTypes { get; set; }

        public IEnumerable<MealType> MealTypes { get; set; }
    }
}
