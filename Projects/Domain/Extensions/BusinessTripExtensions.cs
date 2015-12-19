using CrazyAppsStudio.Delegacje.Domain.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.Entities;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
    public static class BusinessTripExtensions
    {
        public static IQueryable<BusinessTrip> SearchByText(this IQueryable<BusinessTrip> clearings, string text)
        {
            if (string.IsNullOrEmpty(text))
                return clearings;

            return clearings.Where(s => s.Title.ToLower().Contains(text.Trim().ToLower()));
        }

        public static ClearingSearchItemDTO MapToSearchItem(this BusinessTrip clearings)
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
