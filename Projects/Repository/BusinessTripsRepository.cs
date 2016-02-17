using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrazyAppsStudio.Delegacje.Domain.Extensions;

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

		public BusinessTrip Create(BusinessTrip businessTrip)
		{
			return this.context.BusinessTrips.Add(businessTrip);
		}

		public BusinessTrip GetById(int businessTripId)
		{
			return this.context.BusinessTrips.FirstOrDefault(bt => bt.Id == businessTripId);
		}	

        public IEnumerable<BusinessTripSearchItemDTO> GetForUser(string user)
        {
            return this.context.BusinessTrips.Where(b => b.User.UserName == user).MapToSearchItem();
        }

		public void Remove(int businessTripId)
		{
			BusinessTrip trip = this.context.BusinessTrips.First(bt => bt.Id == businessTripId);
			this.context.BusinessTrips.Remove(trip);			
		}
	}
}
;