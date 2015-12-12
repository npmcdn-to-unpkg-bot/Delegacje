using CrazyAppsStudio.Delegacje.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;

namespace CrazyAppsStudio.Delegacje.DomainModel
{
	public class BusinessTripsContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public DbSet<BusinessTrip> BusinessTrips { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Expense> Expenses { get; set; }
		public DbSet<MileageAllowance> MileageAllowances { get; set; }

		public DbSet<Subsistence> Subsistences { get; set; }

		public DbSet<SubsistenceMeals> SubsistenceMeals { get; set; }

		public DbSet<VehicleType> VehicleTypes { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }		

        public BusinessTripsContext()
            : base("BusinessTripsEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Role>().ToTable("Roles", "dbo");
			modelBuilder.Entity<User>().ToTable("Users", "dbo");
			modelBuilder.Entity<UserRole>().ToTable("UserRoles", "dbo");
			modelBuilder.Entity<UserLogin>().ToTable("UserLogins", "dbo");
			modelBuilder.Entity<UserClaim>().ToTable("UserClaims", "dbo");

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();                        
        }

        public static BusinessTripsContext Create()
        {
            return new BusinessTripsContext();
        }
    }
}
