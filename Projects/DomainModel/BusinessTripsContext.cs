using CrazyAppsStudio.Delegacje.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CrazyAppsStudio.Delegacje.DomainModel
{
    public class BusinessTripsContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Clearing> Clearings { get; set; }

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
