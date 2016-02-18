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

		public static BusinessTripDTO MapToDTO(this BusinessTrip trip)
		{
			return new BusinessTripDTO()
			{
				Id = trip.Id,
				Title = trip.Title,
				Date = trip.Date.ToAppString(),
				BusinessPurpose = trip.BusinessPurpose,
				BusinessReason = trip.BusinessReason,
				Notes = trip.Notes,
				Expenses = trip.Expenses.Select(exp => exp.MapToDTO()).ToArray(),
				Subsistences = trip.Subsistences.Select(sub => sub.MapToDTO()).ToArray(),
				MileageAllowances = trip.MileageAllowances.Select(ma => ma.MapToDTO()).ToArray()
			};
		}
    }
}
