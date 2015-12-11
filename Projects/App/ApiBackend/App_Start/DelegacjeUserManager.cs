using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.DomainModel
{
	public class DelegacjeUserManager : UserManager<User, int>
	{
		public DelegacjeUserManager(IUserStore<User, int> store)
			: base(store)
		{
			// Configure validation logic for usernames
            this.UserValidator = new UserValidator<User, int>(this)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = false
			};

			// Configure validation logic for passwords
			this.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true
			};
		}

		public static DelegacjeUserManager Create(BusinessTripsContext context)
		{
			return new DelegacjeUserManager(
                new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>				
				(context));
		}

		public static DelegacjeUserManager Create(IdentityFactoryOptions<DelegacjeUserManager> options,
			IOwinContext context)
		{
			var manager = Create(context.Get<BusinessTripsContext>());

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug in here.
			//manager.RegisterTwoFactorProvider(
			//    "EmailCode",
			//    new EmailTokenProvider<WhitbreadUser, int>
			//    {
			//        Subject = "Security Code",
			//        BodyFormat = "Your security code is: {0}"
			//    });
			//manager.EmailService = new EmailService();
			IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
					new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}        
	}
}
