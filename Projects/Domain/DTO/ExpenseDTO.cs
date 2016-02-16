using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
	public class ExpenseDTO
	{
		public int? ExpenseId { get; set; }
		[Required]
		public int ExpenseTypeId { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[Required, MaxLength(255)]
		public string City { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public int CountryId { get; set; }
		[Required, MaxLength(10)]
		public string CurrencyCode { get; set; } //TODO: store currency codes and exchange rates in database, including historical values - MS 07/02/2016
		[Required]
		public double ExchangeRate { get; set; }
		[Required]
		public bool ExchangeRateModifiedByUser { get; set; }

		public double? VATRate { get; set; }

		[MaxLength(255)]
		public string Notes { get; set; }
	}
}
