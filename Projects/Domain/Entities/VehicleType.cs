using System.ComponentModel.DataAnnotations;

namespace CrazyAppsStudio.Delegacje.Domain.Entities
{
	public class VehicleType
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
        public decimal Rate { get; set; }
	}
}
