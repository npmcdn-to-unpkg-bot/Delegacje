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
            result.Id = subsistence.Id;
            result.City = subsistence.City;
            result.CountryId = subsistence.CountryId;
            result.EndDate = subsistence.EndDate.ToAppStringWithTime();
            result.StartDate = subsistence.StartDate.ToAppStringWithTime();

            List<SubsistenceDayDTO> days = new List<SubsistenceDayDTO>();
            foreach (SubsistenceDay day in subsistence.Days)
            {
                days.Add(new SubsistenceDayDTO()
                {
                    Amount = day.Amount,
                    AmountPLN = day.AmountPLN,
                    ExchangeRate = day.ExchangeRate,
                    Breakfast = day.Breakfast,
                    Date = day.Date.ToAppString(),
                    Dinner = day.Dinner,
                    Supper = day.Supper,
                    Night = day.Night,
                    Diet = day.Diet,
                    IsForeign = day.IsForeign
                });
            }

            result.Days = days.ToArray();
            return result;
		}		
	}	
}
