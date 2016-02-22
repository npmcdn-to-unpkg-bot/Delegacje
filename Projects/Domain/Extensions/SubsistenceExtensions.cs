using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using System.Collections.Generic;
using Tools;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
	public static class SubsistenceExtensions
	{
		public static SubsistenceDTO MapToDTO(this Subsistence subsistence)
		{
            SubsistenceDTO result = new SubsistenceDTO();
            result.City = subsistence.City;
            result.CountryId = subsistence.CountryId;
            result.EndDate = subsistence.EndDate.ToAppString();
            result.StartDate = subsistence.StartDate.ToAppString();

            List<SubsistenceDayDTO> days = new List<SubsistenceDayDTO>();
            foreach (SubsistenceDay day in subsistence.Days)
            {
                days.Add(new SubsistenceDayDTO()
                {
                    Amount = day.Amount,
                    AmountPLN = day.AmountPLN,
                    Breakfast = day.Breakfast,
                    Date = day.Date.ToAppString(),
                    Dinner = day.Dinner,
                    Supper = day.Supper,
                    Night = day.Night
                });
            }

            result.Days = days.ToArray();
            return result;
		}		
	}	
}
