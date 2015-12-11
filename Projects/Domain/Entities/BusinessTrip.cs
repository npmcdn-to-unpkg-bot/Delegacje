using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{

    public class BusinessTrip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[MaxLength(100)]
		public string BusinessReason { get; set; }

		[MaxLength(100)]
		public string BusinessPurpose { get; set; }

		[MaxLength(255)]
		public string Notes { get; set; }

        public int UserId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

		public virtual ICollection<Expense> Expenses { get; set; }

		public virtual ICollection<MileageAllowance> MileageAllowances { get; set; }

		public virtual ICollection<Subsistence> Subsistences { get; set; }
    }
}
