using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Repository
{
	public class ExpensesRepository
	{
		private BusinessTripsContext context;

		public ExpensesRepository(BusinessTripsContext _context)
        {
            this.context = _context;
        }

        public IQueryable<Expense> ExpensesQueryable
        {
            get
            {
                return this.context.Expenses.AsQueryable<Expense>();
            }
        }

		public Expense Create(Expense expense)
		{
			return this.context.Expenses.Add(expense);
		}

		public IEnumerable<Expense> CreateSet(IEnumerable<Expense> expenses)
		{
			return this.context.Expenses.AddRange(expenses);
		}       
	}
}
