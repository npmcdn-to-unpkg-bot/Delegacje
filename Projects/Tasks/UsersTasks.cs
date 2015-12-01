
using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Repository;

namespace CrazyAppsStudio.Delegacje.Tasks
{
    public class UsersTasks
    {
        private RepositoriesFacade repo;

        public UsersTasks()
        {
            repo = new RepositoriesFacade();
        }

        public UserDetailsDTO GetUserData(string username)
        {
            //ApplicationUser user = ReadUserFullData(username);
            //UserDetailsDTO userDTO = user.MapToDetails();
            // return userDTO;
            return null;
        }
    }
}
