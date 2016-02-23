using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
	public class SubsistenceDaysRespository
    {
		private BusinessTripsContext context;

		public SubsistenceDaysRespository(BusinessTripsContext _context)
		{
			this.context = _context;
		}

		public IEnumerable<SubsistenceDay> CreateSet(IEnumerable<SubsistenceDay> days)
		{
			return this.context.SubsistenceDays.AddRange(days);
		}

		public void Remove(IEnumerable<SubsistenceDay> days)
        {
			this.context.SubsistenceDays.RemoveRange(days);
		}

        public IEnumerable<SubsistenceDay> GetForsubsistence(int subsistanceId)
        {
            return this.context.SubsistenceDays.Where(s => s.SubsistenceId == subsistanceId);
        }
	}
}
