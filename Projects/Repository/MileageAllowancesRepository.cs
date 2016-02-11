using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
	public class MileageAllowancesRepository
	{
		private BusinessTripsContext context;

		public MileageAllowancesRepository(BusinessTripsContext _context)
		{
			this.context = _context;
		}

		public IQueryable<MileageAllowance> MileageAllowancesQueryable
		{
			get
			{
				return this.context.MileageAllowances.AsQueryable<MileageAllowance>();
			}
		}

		public IEnumerable<MileageAllowance> CreateSet(IEnumerable<MileageAllowance> mileageAllowances)
		{
			return this.context.MileageAllowances.AddRange(mileageAllowances);
		}

		public void RemoveSet(IEnumerable<MileageAllowance> mileageAllowances)
		{
			this.context.MileageAllowances.RemoveRange(mileageAllowances);
		}
	}
}
