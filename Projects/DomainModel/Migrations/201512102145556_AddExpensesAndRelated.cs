namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpensesAndRelated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessTripId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        City = c.String(nullable: false, maxLength: 255),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyCode = c.String(nullable: false, maxLength: 10),
                        ExchangeRate = c.Double(nullable: false),
                        ExchangeRateModifiedByUser = c.Boolean(nullable: false),
                        VATRate = c.Double(nullable: false),
                        Notes = c.String(maxLength: 255),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.BusinessTrips", t => t.BusinessTripId)
                .Index(t => t.BusinessTripId)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CultureCodeString = c.String(nullable: false, maxLength: 10),
                        CurrencyName = c.String(nullable: false, maxLength: 50),
                        CurrencyCode = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BusinessTrips", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.BusinessTrips", "BusinessReason", c => c.String(maxLength: 100));
            AddColumn("dbo.BusinessTrips", "BusinessPurpose", c => c.String(maxLength: 100));
            AddColumn("dbo.BusinessTrips", "Notes", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.Expenses", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Expenses", new[] { "Country_Id" });
            DropIndex("dbo.Expenses", new[] { "BusinessTripId" });
            DropColumn("dbo.BusinessTrips", "Notes");
            DropColumn("dbo.BusinessTrips", "BusinessPurpose");
            DropColumn("dbo.BusinessTrips", "BusinessReason");
            DropColumn("dbo.BusinessTrips", "Date");
            DropTable("dbo.Countries");
            DropTable("dbo.Expenses");
        }
    }
}
