namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameClearingsToBusinessTrips : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clearings", newName: "BusinessTrips");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BusinessTrips", newName: "Clearings");
        }
    }
}
