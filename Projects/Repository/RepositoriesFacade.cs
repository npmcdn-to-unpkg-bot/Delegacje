using CrazyAppsStudio.Delegacje.DomainModel;

namespace CrazyAppsStudio.Delegacje.Repository
{
    public class RepositoriesFacade
    {
        private BusinessTripsContext swipeContext;

        public UsersRepository Users { get; set; }

        public RepositoriesFacade()
        {
            swipeContext = new BusinessTripsContext();

            this.Users = new UsersRepository(swipeContext);
        }

        public void SaveChanges()
        {
            swipeContext.SaveChanges();
        }

        public void Dispose()
        {
            swipeContext.Dispose();
        }
    }
}
