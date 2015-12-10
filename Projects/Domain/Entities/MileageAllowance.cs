using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class MileageAllowance
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int VehicleTypeId { get; set; }

		public virtual VehicleType Type { get; set; }

		[Required]
		public int BusinessTripId { get; set; }

		[Required]
		public virtual BusinessTrip Trip { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public double Distance { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[MaxLength(255)]
		public string Notes { get; set; }		
	}
}
