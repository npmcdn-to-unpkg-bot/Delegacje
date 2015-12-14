using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class Country
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(100)]
		public string Name { get; set; }

		[Required, MaxLength(10)]
		public string CurrencyCode { get; set; }

		[Required]
		public decimal SubsistenceAllowance { get; set; }

		[Required]
		public decimal AccomodationLimit { get; set; }
	}
}
