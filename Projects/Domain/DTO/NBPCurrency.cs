using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
	public class NBPCurrency
	{
		/// <summary>
		/// Nazwa waluty
		/// </summary>
		public string CurrencyName { get; set; }
		/// <summary>
		/// przelicznik
		/// </summary>
		public string Converter { get; set; }
		/// <summary>
		/// kod waluty
		/// </summary>
		public string CurrencyCode { get; set; }
		/// <summary>
		/// kurs średni waluty
		/// </summary>
		public double CurrencyRate { get; set; }
		/// <summary>
		/// data publikacji
		/// </summary>
		public DateTime CurrencyDate { get; set; }

		public NBPCurrency() { }
	}
}
