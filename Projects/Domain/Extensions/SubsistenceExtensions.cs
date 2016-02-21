using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
	public static class SubsistenceExtensions
	{
		public static SubsistenceMealDTO MapToDTO(this SubsistenceMeal meal)
		{
			return new SubsistenceMealDTO()
			{
				Id = meal.Id,
				MealTypeId = meal.MealTypeId
			};
		}

		public static SubsistenceDTO MapToDTO(this Subsistence subsistence)
		{
			return new SubsistenceDTO()
			{
				Id = subsistence.Id,
				StartDate = subsistence.StartDate.ToAppString(),
				DestinationCity = subsistence.DestinationCity,
				CountryId = subsistence.CountryId,
				EndDate = subsistence.EndDate.ToAppString(),
				AccomodationCount = subsistence.AccomodationCount,
				Meals = subsistence.Meals.Select(meal => meal.MapToDTO()).ToList()
			};
		}		
	}	
}
