using CrazyAppsStudio.Delegacje.Domain.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using Tools;

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

        public static List<BusinessTripSearchItemDTO> MapToSearchItem(this IQueryable<BusinessTrip> clearings)
        {
            return clearings.ToList().Select(bt => new BusinessTripSearchItemDTO()
            {
                Id = bt.Id,
                Date = bt.Date.ToAppString(),
                Note = bt.Notes,
                Title = bt.Title,
                Reason = bt.BusinessReason,
                Purpose = bt.BusinessPurpose
            }).ToList();
        }
    }
}
