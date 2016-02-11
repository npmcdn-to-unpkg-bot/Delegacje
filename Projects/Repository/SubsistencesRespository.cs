using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
	public class SubsistencesRepository
	{
		private BusinessTripsContext context;

		public SubsistencesRepository(BusinessTripsContext _context)
		{
			this.context = _context;
		}

		public IQueryable<Subsistence> SubsistencesQueryable
		{
			get
			{
				return this.context.Subsistences.AsQueryable<Subsistence>();
			}
		}

		public IQueryable<SubsistenceMeal> MealsQueryable
		{
			get
			{
				return this.context.SubsistenceMeals.AsQueryable<SubsistenceMeal>();
			}
		}

		public IEnumerable<Subsistence> CreateSet(IEnumerable<Subsistence> subsistences)
		{
			return this.context.Subsistences.AddRange(subsistences);
		}

		public void RemoveSet(IEnumerable<Subsistence> subsistences)
		{
			this.context.Subsistences.RemoveRange(subsistences);
		}

		public void RemoveMealsForSubsistence(int subsistenceId)
		{
			List<SubsistenceMeal> meals = this.context.SubsistenceMeals.Where(sm => sm.SubsistenceId == subsistenceId).ToList();
			this.context.SubsistenceMeals.RemoveRange(meals);
		}


	}
}
