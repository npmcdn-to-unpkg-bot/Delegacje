using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.DomainModel
{
    public class DbInitializer : MigrateDatabaseToLatestVersion<BusinessTripsContext, Migrations.Configuration>
    {
    }

}
