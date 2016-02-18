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
	public static class MileageExtensions
	{		
		public static MileageAllowanceDTO MapToDTO(this MileageAllowance allowance)
		{
			return new MileageAllowanceDTO()
			{
				id = allowance.Id,
				VehicleTypeId = allowance.VehicleTypeId,
				Date = allowance.Date.ToAppString(),
				Distance = allowance.Distance,
				Amount = allowance.Amount,
				Notes = allowance.Notes
			};
		}
	}
}
