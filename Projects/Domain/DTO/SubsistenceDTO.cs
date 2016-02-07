using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
	public class SubsistenceDTO
	{
		public int? Id { get; set; }
		[Required]
		public DateTime StartDate { get; set; }

		[Required, MaxLength(255)]
		public string DestinationCity { get; set; }

		[Required]
		public int CountryId { get; set; }		

		[Required]
		public DateTime EndDate { get; set; }

		public List<SubsistenceMealDTO> Meals { get; set; }

		/// <summary>
		/// Noclegi
		/// </summary>
		public int AccomodationCount { get; set; }		
	}
}
