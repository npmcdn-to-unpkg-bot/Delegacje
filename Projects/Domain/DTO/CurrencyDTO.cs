using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
	public class CurrencyRateDTO
	{
		public int CurrencyRateId { get; set; }
		public string Code { get; set; }
		public double ExchangeRate { get; set; }
		public string Name { get; set; }
		public string Date { get; set; }
	}
}
