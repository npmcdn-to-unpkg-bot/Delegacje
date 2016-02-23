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

		public Subsistence Create(Subsistence subsistence)
		{
			return this.context.Subsistences.Add(subsistence);
		}

		public void Remove(Subsistence subsistence)
		{
			this.context.Subsistences.Remove(subsistence);
		}

        public Subsistence GetById(int subId)
        {
            return this.context.Subsistences.Where(s => s.Id == subId).FirstOrDefault();
        }
	}
}
