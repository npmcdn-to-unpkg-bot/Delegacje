using System;
using System.ComponentModel.DataAnnotations;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class SubsistenceDay
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime Date { get; set; }

        /// <summary>
        /// Było śniadanko
        /// </summary>
        public bool Breakfast { get; set; }

        /// <summary>
        /// Był obiadek
        /// </summary>
        public bool Dinner { get; set; }

        /// <summary>
        /// Była kolacyjka
        /// </summary>
        public bool Supper { get; set; }

        /// <summary>
        /// Spanko we własnym zakresie
        /// </summary>
        public bool Night { get; set; }

        public decimal Amount { get; set; }
        public decimal AmountPLN { get; set; }

        [Required]
		public int SubsistenceId { get; set; }

		[Required]
		public virtual Subsistence Subsistence { get; set; }
	}
}
