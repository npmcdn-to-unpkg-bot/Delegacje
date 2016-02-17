using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
	public class MileageAllowanceDTO
	{
		public int? id { get; set; }
		public int VehicleTypeId { get; set; }
		public string Date { get; set; }
		public double Distance { get; set; }
		public decimal Amount { get; set; }
		[MaxLength(255)]
		public string Notes { get; set; }
	}
}
