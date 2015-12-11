namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessTrips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        BusinessReason = c.String(maxLength: 100),
                        BusinessPurpose = c.String(maxLength: 100),
                        Notes = c.String(maxLength: 255),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
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
                        CountryId = c.Int(nullable: false),
                        CurrencyCode = c.String(nullable: false, maxLength: 10),
                        ExchangeRate = c.Double(nullable: false),
                        ExchangeRateModifiedByUser = c.Boolean(nullable: false),
                        VATRate = c.Double(nullable: false),
                        Notes = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.BusinessTrips", t => t.BusinessTripId)
                .Index(t => t.BusinessTripId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CultureCodeString = c.String(nullable: false, maxLength: 10),
                        CurrencyName = c.String(nullable: false, maxLength: 50),
                        CurrencyCode = c.String(nullable: false, maxLength: 10),
                        SubsistenceAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccomodationLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
						UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),                        
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusinessTrips", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Subsistences", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.SubsistenceMeals", "SubsistenceId", "dbo.Subsistences");
            DropForeignKey("dbo.Subsistences", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MileageAllowances", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.MileageAllowances", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.Expenses", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.Expenses", "CountryId", "dbo.Countries");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.SubsistenceMeals", new[] { "SubsistenceId" });
            DropIndex("dbo.Subsistences", new[] { "BusinessTripId" });
            DropIndex("dbo.Subsistences", new[] { "CountryId" });
            DropIndex("dbo.MileageAllowances", new[] { "BusinessTripId" });
            DropIndex("dbo.MileageAllowances", new[] { "VehicleTypeId" });
            DropIndex("dbo.Expenses", new[] { "CountryId" });
            DropIndex("dbo.Expenses", new[] { "BusinessTripId" });
            DropIndex("dbo.BusinessTrips", new[] { "UserId" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.SubsistenceMeals");
            DropTable("dbo.Subsistences");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.MileageAllowances");
            DropTable("dbo.Countries");
            DropTable("dbo.Expenses");
            DropTable("dbo.BusinessTrips");
        }
    }
}
