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
			
		[Required]
		public decimal SubsistenceAllowance { get; set; }

		[Required]
		public decimal AccomodationLimit { get; set; }

		[Required]
		public int CurrencyId { get; set; }

		public virtual Currency Currency { get; set; }
		
		public int LimitCurrencyId { get; set; }

		public virtual Currency LimitCurrency { get; set; }

	}
}
