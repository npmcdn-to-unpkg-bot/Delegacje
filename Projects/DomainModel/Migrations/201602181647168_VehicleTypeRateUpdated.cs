namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleTypeRateUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleTypes", "Rate", c => c.Decimal(nullable: false, precision: 12, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleTypes", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
