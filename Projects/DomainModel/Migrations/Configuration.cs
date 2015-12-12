using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
	public class Configuration : DbMigrationsConfiguration<BusinessTripsContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			AutomaticMigrationDataLossAllowed = false;

			//ContextKey = "CrazyAppsStudio.Delegacje.DomainModel.BusinessTripsContext";
		}

		protected override void Seed(CrazyAppsStudio.Delegacje.DomainModel.BusinessTripsContext context)
		{
			//if (System.Diagnostics.Debugger.IsAttached == false)//Debugging migrations launched from package manager console
			//	System.Diagnostics.Debugger.Launch();
			Seeder seeder = new Seeder();
			seeder.SeedContext(context);
			try
			{
				context.SaveChanges();
			}
			catch (Exception e)
			{
				throw e;
			}

			//  This method will be called after migrating to the latest version.
			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
		}
	}
}
