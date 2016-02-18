using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace CrazyAppsStudio.Delegacje.Domain.Extensions
{
	public static class ExpenseExtensions
	{
		public static ExpenseDTO MapToDTO(this Expense expense)
		{
			return new ExpenseDTO()
			{
				ExpenseId = expense.Id,
				ExpenseTypeId = expense.ExpenseTypeId,
				Date = expense.Date.ToAppString(),
				City = expense.City,
				Amount = expense.Amount,
				CountryId = expense.CountryId,
				CurrencyCode = expense.CurrencyCode,
				ExchangeRate = expense.ExchangeRate,
				ExchangeRateModifiedByUser = expense.ExchangeRateModifiedByUser,
				VATRate = expense.VATRate,
				Notes = expense.Notes
			};
		}
	}
}
