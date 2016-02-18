using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
	public class CurrenciesRepository
	{
		private BusinessTripsContext context;

		public CurrenciesRepository(BusinessTripsContext _context)
		{
			this.context = _context;
		}
	}
}
