using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CrazyAppsStudio.Delegacje.DomainModel
{
	public class BusinessTripsContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public DbSet<BusinessTrip> BusinessTrips { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Expense> Expenses { get; set; }
		public DbSet<MileageAllowance> MileageAllowances { get; set; }

		public DbSet<Subsistence> Subsistences { get; set; }
        public DbSet<SubsistenceDay> SubsistenceDays { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseDocumentType> ExpenseDocumentTypes { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

		public DbSet<Currency> Currencies { get; set; }

		public DbSet<CurrencyRate> CurrencyRates { get; set; }

        public BusinessTripsContext()
            : base("BusinessTripsEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleType>().Property(v => v.Rate).HasPrecision(12, 4);

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
