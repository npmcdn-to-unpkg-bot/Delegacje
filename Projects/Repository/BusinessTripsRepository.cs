using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
	public class BusinessTripsRepository
	{
		private BusinessTripsContext context;

		public BusinessTripsRepository(BusinessTripsContext _context)
        {
            this.context = _context;
        }

        public IQueryable<BusinessTrip> BusinessTripsQueryable
        {
            get
            {
                return this.context.BusinessTrips.AsQueryable<BusinessTrip>();
            }
        }

		public void Create(BusinessTrip businessTrip)
		{
			this.context.BusinessTrips.Add(businessTrip);
		}
	}
}
;