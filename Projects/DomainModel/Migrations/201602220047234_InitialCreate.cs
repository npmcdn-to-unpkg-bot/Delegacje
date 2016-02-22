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
                        ExpenseTypeId = c.Int(nullable: false),
                        DocumentTypeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        City = c.String(nullable: false, maxLength: 255),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountryId = c.Int(nullable: false),
                        CurrencyCode = c.String(nullable: false, maxLength: 10),
                        ExchangeRate = c.Double(nullable: false),
                        ExchangeRateModifiedByUser = c.Boolean(nullable: false),
                        DoNotReturn = c.Boolean(nullable: false),
                        VATRate = c.Double(),
                        Notes = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.ExpenseDocumentTypes", t => t.DocumentTypeId)
                .ForeignKey("dbo.BusinessTrips", t => t.BusinessTripId)
                .ForeignKey("dbo.ExpenseTypes", t => t.ExpenseTypeId)
                .Index(t => t.BusinessTripId)
                .Index(t => t.ExpenseTypeId)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        SubsistenceAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccomodationLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyId = c.Int(nullable: false),
                        LimitCurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Currencies", t => t.LimitCurrencyId)
                .Index(t => t.CurrencyId)
                .Index(t => t.LimitCurrencyId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpenseDocumentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpenseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
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
                        Rate = c.Decimal(nullable: false, precision: 12, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subsistences",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        City = c.String(nullable: false, maxLength: 255),
                        CountryId = c.Int(nullable: false),
                        BusinessTripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.BusinessTrips", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.SubsistenceDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Breakfast = c.Boolean(nullable: false),
                        Dinner = c.Boolean(nullable: false),
                        Supper = c.Boolean(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        UserName = c.String(nullable: false, maxLength: 256),
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
            
            CreateTable(
                "dbo.CurrencyRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExchangeRate = c.Double(nullable: false),
                        DateRefreshed = c.DateTime(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .Index(t => t.CurrencyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrencyRates", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.BusinessTrips", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Subsistences", "Id", "dbo.BusinessTrips");
            DropForeignKey("dbo.SubsistenceDays", "SubsistenceId", "dbo.Subsistences");
            DropForeignKey("dbo.Subsistences", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MileageAllowances", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.MileageAllowances", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.Expenses", "ExpenseTypeId", "dbo.ExpenseTypes");
            DropForeignKey("dbo.Expenses", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.Expenses", "DocumentTypeId", "dbo.ExpenseDocumentTypes");
            DropForeignKey("dbo.Expenses", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "LimitCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.CurrencyRates", new[] { "CurrencyId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.SubsistenceDays", new[] { "SubsistenceId" });
            DropIndex("dbo.Subsistences", new[] { "CountryId" });
            DropIndex("dbo.Subsistences", new[] { "Id" });
            DropIndex("dbo.MileageAllowances", new[] { "BusinessTripId" });
            DropIndex("dbo.MileageAllowances", new[] { "VehicleTypeId" });
            DropIndex("dbo.Countries", new[] { "LimitCurrencyId" });
            DropIndex("dbo.Countries", new[] { "CurrencyId" });
            DropIndex("dbo.Expenses", new[] { "CountryId" });
            DropIndex("dbo.Expenses", new[] { "DocumentTypeId" });
            DropIndex("dbo.Expenses", new[] { "ExpenseTypeId" });
            DropIndex("dbo.Expenses", new[] { "BusinessTripId" });
            DropIndex("dbo.BusinessTrips", new[] { "UserId" });
            DropTable("dbo.CurrencyRates");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.SubsistenceDays");
            DropTable("dbo.Subsistences");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.MileageAllowances");
            DropTable("dbo.ExpenseTypes");
            DropTable("dbo.ExpenseDocumentTypes");
            DropTable("dbo.Currencies");
            DropTable("dbo.Countries");
            DropTable("dbo.Expenses");
            DropTable("dbo.BusinessTrips");
        }
    }
}
