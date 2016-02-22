using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class Subsistence
	{
		[Key]
		public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, MaxLength(255)]
        public string City { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public virtual Country Country { get; set; }

  //      [Required]
		//public int BusinessTripId { get; set; }

		//[Required]
		//public virtual BusinessTrip BusinessTrip { get; set; }

        public virtual ICollection<SubsistenceDay> Days { get; set; }        
    }
}
