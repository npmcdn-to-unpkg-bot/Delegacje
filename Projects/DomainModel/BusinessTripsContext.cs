using CrazyAppsStudio.Delegacje.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CrazyAppsStudio.Delegacje.Domain.Entities;

namespace CrazyAppsStudio.Delegacje.DomainModel
{
    public class BusinessTripsContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BusinessTrip> BusinessTrips { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Expense> Expenses { get; set; }
		public DbSet<MileageAllowance> MileageAllowances { get; set; }

		public DbSet<Subsistence> Subsistences { get; set; }

		public DbSet<SubsistenceMeals> SubsistenceMeals { get; set; }

		public DbSet<VehicleType> VehicleTypes { get; set; }

        public BusinessTripsContext()
            : base("BusinessTripsEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            base.OnModelCreating(modelBuilder);
        }

        public static BusinessTripsContext Create()
        {
            return new BusinessTripsContext();
        }
    }
}
