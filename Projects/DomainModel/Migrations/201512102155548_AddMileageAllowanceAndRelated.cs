namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMileageAllowanceAndRelated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MileageAllowances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleTypeId = c.Int(nullable: false),
                        BusinessTripId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Distance = c.Double(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessTrips", t => t.BusinessTripId)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId)
                .Index(t => t.VehicleTypeId)
                .Index(t => t.BusinessTripId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MileageAllowances", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.MileageAllowances", "BusinessTripId", "dbo.BusinessTrips");
            DropIndex("dbo.MileageAllowances", new[] { "BusinessTripId" });
            DropIndex("dbo.MileageAllowances", new[] { "VehicleTypeId" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.MileageAllowances");
        }
    }
}
