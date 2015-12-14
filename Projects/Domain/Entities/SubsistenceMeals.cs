using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class SubsistenceMeals
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public virtual MealType Type { get; set; }

		[Required]
		public int SubsistenceId { get; set; }

		[Required]
		public virtual Subsistence Subsistence { get; set; }
	}
}
