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

		public IEnumerable<Subsistence> CreateSet(IEnumerable<Subsistence> subsistences)
		{
			return this.context.Subsistences.AddRange(subsistences);
		}  
	}
}
