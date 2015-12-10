namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubsistenceAndRelated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subsistences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        DestinationCity = c.String(nullable: false, maxLength: 255),
                        CountryId = c.Int(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        AccomodationCount = c.Int(nullable: false),
                        BusinessTripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.BusinessTrips", t => t.BusinessTripId)
                .Index(t => t.CountryId)
                .Index(t => t.BusinessTripId);
            
            CreateTable(
                "dbo.SubsistenceMeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        SubsistenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subsistences", t => t.SubsistenceId)
                .Index(t => t.SubsistenceId);
            
            AddColumn("dbo.Countries", "SubsistenceAllowance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Countries", "AccomodationLimit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subsistences", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.SubsistenceMeals", "SubsistenceId", "dbo.Subsistences");
            DropForeignKey("dbo.Subsistences", "CountryId", "dbo.Countries");
            DropIndex("dbo.SubsistenceMeals", new[] { "SubsistenceId" });
            DropIndex("dbo.Subsistences", new[] { "BusinessTripId" });
            DropIndex("dbo.Subsistences", new[] { "CountryId" });
            DropColumn("dbo.Countries", "AccomodationLimit");
            DropColumn("dbo.Countries", "SubsistenceAllowance");
            DropTable("dbo.SubsistenceMeals");
            DropTable("dbo.Subsistences");
        }
    }
}
