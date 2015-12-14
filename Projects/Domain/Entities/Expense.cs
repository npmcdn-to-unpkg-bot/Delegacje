using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class Expense
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int BusinessTripId { get; set; }

		[Required]
		public virtual BusinessTrip Trip { get; set; }

		[Required]
		public virtual ExpenseType Type { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required, MaxLength(255)]
		public string City { get; set; }

		public decimal Amount { get; set; }

		[Required]
		public int CountryId { get; set; }

		[Required]
		public virtual Country Country { get; set; }

		[Required, MaxLength(10)]
		public string CurrencyCode { get; set; }

		[Required]
		public double ExchangeRate { get; set; }

		[Required]
		public bool ExchangeRateModifiedByUser { get; set; }

		public double VATRate { get; set; }

		[MaxLength(255)]
		public string Notes { get; set; }
	}
}
