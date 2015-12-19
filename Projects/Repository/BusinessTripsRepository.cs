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

        public IEnumerable<BusinessTripSearchItemDTO> GetForUser(int userId)
        {
            return new List<BusinessTripSearchItemDTO>() {
                new BusinessTripSearchItemDTO() { Id = 1, AmountTotal = 100, Date = DateTime.Now, Note = "Note 1", Title = "Title 1" },
                new BusinessTripSearchItemDTO() { Id = 2, AmountTotal = 100, Date = DateTime.Now, Note = "Note 2", Title = "Title 2" },
                new BusinessTripSearchItemDTO() { Id = 3, AmountTotal = 100, Date = DateTime.Now, Note = "Note 3", Title = "Title 3" },
                new BusinessTripSearchItemDTO() { Id = 4, AmountTotal = 100, Date = DateTime.Now, Note = "Note 4", Title = "Title 4" },
                new BusinessTripSearchItemDTO() { Id = 5, AmountTotal = 100, Date = DateTime.Now, Note = "Note 5", Title = "Title 5" },
            };
        }
	}
}
;