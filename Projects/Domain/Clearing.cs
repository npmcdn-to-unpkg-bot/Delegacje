using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrazyAppsStudio.Delegacje.Domain
{

    public class Clearing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string UserId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
