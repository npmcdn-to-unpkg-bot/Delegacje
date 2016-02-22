using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
	public class BusinessTripDTO
	{
		public int? Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Date { get; set; }
		[MaxLength(100, ErrorMessage = "Maksymalna długość: 100 znaków")]
		public string BusinessReason { get; set; }
		[MaxLength(100, ErrorMessage = "Maksymalna długość: 100 znaków")]
		public string BusinessPurpose { get; set; }
		[MaxLength(255, ErrorMessage = "Maksymalna długość: 255 znaków")]
		public string Notes { get; set; }

		public ExpenseDTO[] Expenses { get; set; }
		public MileageAllowanceDTO[] MileageAllowances { get; set; }
		public SubsistenceDTO Subsistence { get; set; }
	}
}
