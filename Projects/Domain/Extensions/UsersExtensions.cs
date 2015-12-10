using CrazyAppsStudio.Delegacje.Domain.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.Entities;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
    public static class UsersExtensions
    {
        public static IQueryable<ApplicationUser> SearchByText(this IQueryable<ApplicationUser> users, string text)
        {
            if (string.IsNullOrEmpty(text))
                return users;

            return users.Where(s => s.UserName.ToLower().Contains(text.Trim().ToLower()));
        }

        public static UserDetailsDTO MapToDetails(this ApplicationUser user)
        {
            UserDetailsDTO userDTO = new UserDetailsDTO()
            {
                //Id = user.Id,
                //Email = user.Email,
                //Username = user.UserName
            };

            return userDTO;
        }
    }
}
