using CrazyAppsStudio.Delegacje.Domain.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
    public static class UsersExtensions
    {
        public static IQueryable<User> SearchByText(this IQueryable<User> users, string text)
        {
            if (string.IsNullOrEmpty(text))
                return users;

            return users.Where(s => s.UserName.ToLower().Contains(text.Trim().ToLower()));
        }

        public static UserDetailsDTO MapToDetails(this User user)
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
