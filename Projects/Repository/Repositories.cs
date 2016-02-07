using CrazyAppsStudio.Delegacje.DomainModel;

namespace CrazyAppsStudio.Delegacje.Repository
{
    public class Repositories : IRepositories
    {
        private BusinessTripsContext context;

        public UsersRepository Users { get; set; }
        public DictionariesRepository Dictionaries { get; set; }
		public BusinessTripsRepository BusinessTrips { get; set; }
		public ExpensesRepository Expenses { get; set; }
		public SubsistencesRepository Subsistences { get; set; }
		public MileageAllowancesRepository MileageAllowances { get; set; }

        public Repositories()
        {
            context = new BusinessTripsContext();

            this.Users = new UsersRepository(context);
            this.Dictionaries = new DictionariesRepository(context);
			this.BusinessTrips = new BusinessTripsRepository(context);
			this.Expenses = new ExpensesRepository(context);
			this.Subsistences = new SubsistencesRepository(context);
			this.MileageAllowances = new MileageAllowancesRepository(context);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
