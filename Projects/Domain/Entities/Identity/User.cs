using CrazyAppsStudio.Delegacje.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities.Identity
{	
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {	
        [MaxLength(255)]        
        public string CompanyName { get; set; }

        [MaxLength(50)]
        public string ActivationCode { get; set; }

        public bool IsActive { get; set; }

        public virtual List<BusinessTrip> BusinessTrips { get; set; }

		public virtual ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            this.BusinessTrips = new List<BusinessTrip>();
        }		
    }
}