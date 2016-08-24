
using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using CrazyAppsStudio.Delegacje.Repository;

namespace CrazyAppsStudio.Delegacje.Tasks
{
    public class UsersTasks
    {
        private Repositories repo;

        public UsersTasks()
        {
            repo = new Repositories();
        }

        public UserDetailsDTO GetUserData(string username)
        {
            //ApplicationUser user = ReadUserFullData(username);
            //UserDetailsDTO userDTO = user.MapToDetails();
            // return userDTO;
            return null;
        }

        public User GetUserByActivationCode(string activationCode)
        {
            return repo.Users.FindUserByActivationCode(activationCode);
        }

        public void ActivateUser(int userId)
        {
            repo.Users.ActivateUser(userId);
        }
    }
}
