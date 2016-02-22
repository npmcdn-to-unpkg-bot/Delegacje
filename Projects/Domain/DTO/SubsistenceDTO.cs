using System.ComponentModel.DataAnnotations;

namespace CrazyAppsStudio.Delegacje.Domain.DTO
{
    public class SubsistenceDTO
    {
        public int? Id { get; set; }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public string City { get; set; }

        public SubsistenceDayDTO[] Days { get; set; }
    }
}
