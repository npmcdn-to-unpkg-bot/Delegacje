﻿//using CrazyAppsStudio.Delegacje.Domain;
//using CrazyAppsStudio.Delegacje.DomainModel;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
//using CrazyAppsStudio.Delegacje.Domain.Entities;
//using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;

//namespace CrazyAppsStudio.Delegacje.App
//{
//	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

//	public class ApplicationUserManager : UserManager<User, int>
//	{
//		public ApplicationUserManager(IUserStore<User, int> store)
//			: base(store)
//		{
//		}

//		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
//		{
//			var manager = new ApplicationUserManager(new UserStore<User, int>(context.Get<BusinessTripsContext>()));
//			// Configure validation logic for usernames
//			manager.UserValidator = new UserValidator<User, int>(manager)
//			{
//				AllowOnlyAlphanumericUserNames = false,
//				RequireUniqueEmail = true
//			};
//			// Configure validation logic for passwords
//			manager.PasswordValidator = new PasswordValidator
//			{
//				RequiredLength = 3,
//				RequireNonLetterOrDigit = false,
//				RequireDigit = false,
//				RequireLowercase = false,
//				RequireUppercase = false,
//			};
//			var dataProtectionProvider = options.DataProtectionProvider;
//			if (dataProtectionProvider != null)
//			{
//				manager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
//			}
//			return manager;
//		}
//	}
//}
