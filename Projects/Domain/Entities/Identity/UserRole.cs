using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Domain.Entities.Identity
{
	public class UserRole : IdentityUserRole<int>
	{
		public virtual User User { get; set; }
		public virtual Role Role { get; set; }
	}
}
