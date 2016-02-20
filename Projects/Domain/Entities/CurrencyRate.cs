using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class CurrencyRate
	{
		[Key]
		public int Id { get; set; }
		[Required]
		/// <summary>
		/// Exchange rate for 1 PLN
		/// </summary>
		public double ExchangeRate { get; set; }
		[Required]
		public DateTime DateRefreshed { get; set; }
		public int CurrencyId { get; set; }

		public virtual Currency Currency { get; set; }
	}
}
