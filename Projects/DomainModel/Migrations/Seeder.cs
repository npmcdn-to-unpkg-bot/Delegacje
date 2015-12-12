using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using CrazyAppsStudio.Delegacje.Domain.Entities;

namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
	public class Seeder
	{
		public void SeedContext(BusinessTripsContext context)
		{
			CreateUsers(context);
			CreateCountries(context);
			CreateVehicleTypes(context);
		}

		public void CreateUsers(BusinessTripsContext context)
		{
			//Roles
			Role basicUser = new Role();
			basicUser.Name = "BasicUser";

			User maciej = new User()
			{
				UserName = "Maciej",
				Email = "suruwa@gmail.com",
				EmailConfirmed = true,
				PasswordHash = "AM+qVffwovJO6ncY4wFxZ12RBOlGQ23Z9yyNF95SI6Ij+cW7F0QzPI090Y6FYbimtQ==", //P@ssword1
				SecurityStamp = "ef0ce398-45dd-41a7-ad99-0f39a28c0f32",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				LockoutEndDateUtc = null,
				LockoutEnabled = false,
				AccessFailedCount = 0
			};
			UserRole maciejBasic = new UserRole() { Role = basicUser, User = maciej };			

			User karol = new User()
			{
				UserName = "Karol",
				Email = "karol.barkowski@gmail.com",
				EmailConfirmed = true,
				PasswordHash = "AM+qVffwovJO6ncY4wFxZ12RBOlGQ23Z9yyNF95SI6Ij+cW7F0QzPI090Y6FYbimtQ==", //P@ssword1
				SecurityStamp = "ef0ce398-45dd-41a7-ad99-0f39a28c0f32",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				LockoutEndDateUtc = null,
				LockoutEnabled = false,
				AccessFailedCount = 0
			};
			UserRole karolBasic = new UserRole() { Role = basicUser, User = karol };

			maciej.Roles.Add(maciejBasic);
			karol.Roles.Add(karolBasic);

			context.Roles.AddOrUpdate(r => new { r.Name }, basicUser);
			context.Users.AddOrUpdate(u => new { u.UserName }, maciej);
			context.Users.AddOrUpdate(u => new { u.UserName }, karol);		
		}

		public void CreateCountries(BusinessTripsContext context)
		{
			//Roles
			Country poland = new Country()
			{
				Name = "Polska",
				CultureCodeString = "pl-PL",
				CurrencyName = "Złoty",
				CurrencyCode = "PLN",
				SubsistenceAllowance = 450,
				AccomodationLimit = 300
			};

			Country germany = new Country()
			{
				Name = "Niemcy",
				CultureCodeString = "de-DE",
				CurrencyName = "Euro",
				CurrencyCode = "EUR",
				SubsistenceAllowance = 250,
				AccomodationLimit = 200
			};

			Country uk = new Country()
			{
				Name = "Wielka Brytania",
				CultureCodeString = "en-GB",
				CurrencyName = "Funt brytyjski",
				CurrencyCode = "GBP",
				SubsistenceAllowance = 650,
				AccomodationLimit = 400
			};

			context.Countries.AddOrUpdate(c => c.Name, poland);
			context.Countries.AddOrUpdate(c => c.Name, germany);
			context.Countries.AddOrUpdate(c => c.Name, uk);
		}

		public void CreateVehicleTypes(BusinessTripsContext context)
		{
			VehicleType motorcycle = new VehicleType()
			{
				Name = "Motocykl",
				Rate = 1.50m
			};

			VehicleType carCapacity1 = new VehicleType()
			{
				Name = "Samochód poj. 1",
				Rate = 3.0m
			};

			VehicleType carCapacity2 = new VehicleType()
			{
				Name = "Samochód poj. 2",
				Rate = 5.0m
			};

			context.VehicleTypes.AddOrUpdate(vt => vt.Name, motorcycle);
			context.VehicleTypes.AddOrUpdate(vt => vt.Name, carCapacity1);
			context.VehicleTypes.AddOrUpdate(vt => vt.Name, carCapacity2);
		}
	}
}
