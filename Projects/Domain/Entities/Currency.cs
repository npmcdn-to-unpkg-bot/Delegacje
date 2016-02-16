using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class Currency
	{		
		[Required, MaxLength(50)]
		public string Name { get; set; }
		[Key, Required, MaxLength(10)]
		public string Code { get; set; }
		[Required]
		/// <summary>
		/// Exchange rate for 1 PLN
		/// </summary>
		public string ExchangeRate { get; set; }
		[Required]
		public DateTime DateRefreshed { get; set; }
	}
}
