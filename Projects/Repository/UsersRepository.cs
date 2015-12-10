using CrazyAppsStudio.Delegacje.Domain;
using CrazyAppsStudio.Delegacje.Domain.Extensions;
using CrazyAppsStudio.Delegacje.DomainModel;
using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.Entities;

namespace CrazyAppsStudio.Delegacje.Repository
{
    public class UsersRepository
    {
        private BusinessTripsContext context;

        public UsersRepository(BusinessTripsContext _context)
        {
            this.context = _context;
        }

        public IQueryable<ApplicationUser> UsersQueryable
        {
            get
            {
                return this.context.Users.AsQueryable<ApplicationUser>();
            }
        }
    }
}
