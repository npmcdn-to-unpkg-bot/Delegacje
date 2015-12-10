using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class Subsistence
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required, MaxLength(255)]
		public string DestinationCity { get; set; }

		[Required]
		public int CountryId { get; set; }

		[Required]
		public virtual Country Country { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		public virtual ICollection<SubsistenceMeals> Meals { get; set; }

		/// <summary>
		/// Noclegi
		/// </summary>
		public int AccomodationCount { get; set; }

		[Required]
		public int BusinessTripId { get; set; }

		[Required]
		public virtual BusinessTrip Trip { get; set; }
	}
}
