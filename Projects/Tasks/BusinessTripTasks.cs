using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Tasks
{
	public class BusinessTripsTasks
	{
		private Repositories repo;

		public BusinessTripsTasks()
        {
            repo = new Repositories();
        }

        public void CreateNewBusinessTrip(AddBusinessTripDTO businessTrip)
        {
			repo.BusinessTrips.Create(new BusinessTrip() {
				Title = businessTrip.Title,
				Date = businessTrip.Date,
				BusinessReason = businessTrip.BusinessReason,
				BusinessPurpose = businessTrip.BusinessPurpose,
				Notes = businessTrip.Notes
			});
			this.repo.SaveChanges();
        }

        public IEnumerable<BusinessTripSearchItemDTO> GetForUser(int userId)
        {
            return repo.BusinessTrips.GetForUser(userId);
        }
	}
}
