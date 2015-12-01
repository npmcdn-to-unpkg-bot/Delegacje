using CrazyAppsStudio.Delegacje.Domain.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
    public static class ClearingsExtensions
    {
        public static IQueryable<Clearing> SearchByText(this IQueryable<Clearing> clearings, string text)
        {
            if (string.IsNullOrEmpty(text))
                return clearings;

            return clearings.Where(s => s.Title.ToLower().Contains(text.Trim().ToLower()));
        }

        public static ClearingSearchItemDTO MapToSearchItem(this Clearing clearings)
        {
            ClearingSearchItemDTO clearingDTO = new ClearingSearchItemDTO()
            {
                //Id = user.Id,
                //Email = user.Email,
                //Username = user.UserName
            };

            return clearingDTO;
        }
    }
}
