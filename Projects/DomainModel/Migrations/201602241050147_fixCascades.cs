namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCascades : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.MileageAllowances", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.BusinessTrips", "UserId", "dbo.Users");
            DropForeignKey("dbo.Expenses", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Expenses", "DocumentTypeId", "dbo.ExpenseDocumentTypes");
            DropForeignKey("dbo.Expenses", "ExpenseTypeId", "dbo.ExpenseTypes");
            DropForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Countries", "LimitCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.MileageAllowances", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Subsistences", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.SubsistenceDays", "SubsistenceId", "dbo.Subsistences");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.CurrencyRates", "CurrencyId", "dbo.Currencies");
            AddForeignKey("dbo.Expenses", "BusinessTripId", "dbo.BusinessTrips", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MileageAllowances", "BusinessTripId", "dbo.BusinessTrips", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BusinessTrips", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Expenses", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Expenses", "DocumentTypeId", "dbo.ExpenseDocumentTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Expenses", "ExpenseTypeId", "dbo.ExpenseTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Countries", "LimitCurrencyId", "dbo.Currencies", "Id", cascadeDelete: false);
            AddForeignKey("dbo.MileageAllowances", "VehicleTypeId", "dbo.VehicleTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subsistences", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubsistenceDays", "SubsistenceId", "dbo.Subsistences", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaims", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLogins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CurrencyRates", "CurrencyId", "dbo.Currencies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrencyRates", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.SubsistenceDays", "SubsistenceId", "dbo.Subsistences");
            DropForeignKey("dbo.Subsistences", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MileageAllowances", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Countries", "LimitCurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Expenses", "ExpenseTypeId", "dbo.ExpenseTypes");
            DropForeignKey("dbo.Expenses", "DocumentTypeId", "dbo.ExpenseDocumentTypes");
            DropForeignKey("dbo.Expenses", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.BusinessTrips", "UserId", "dbo.Users");
            DropForeignKey("dbo.MileageAllowances", "BusinessTripId", "dbo.BusinessTrips");
            DropForeignKey("dbo.Expenses", "BusinessTripId", "dbo.BusinessTrips");
            AddForeignKey("dbo.CurrencyRates", "CurrencyId", "dbo.Currencies", "Id");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id");
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserLogins", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserClaims", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.SubsistenceDays", "SubsistenceId", "dbo.Subsistences", "Id");
            AddForeignKey("dbo.Subsistences", "CountryId", "dbo.Countries", "Id");
            AddForeignKey("dbo.MileageAllowances", "VehicleTypeId", "dbo.VehicleTypes", "Id");
            AddForeignKey("dbo.Countries", "LimitCurrencyId", "dbo.Currencies", "Id");
            AddForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies", "Id");
            AddForeignKey("dbo.Expenses", "ExpenseTypeId", "dbo.ExpenseTypes", "Id");
            AddForeignKey("dbo.Expenses", "DocumentTypeId", "dbo.ExpenseDocumentTypes", "Id");
            AddForeignKey("dbo.Expenses", "CountryId", "dbo.Countries", "Id");
            AddForeignKey("dbo.BusinessTrips", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.MileageAllowances", "BusinessTripId", "dbo.BusinessTrips", "Id");
            AddForeignKey("dbo.Expenses", "BusinessTripId", "dbo.BusinessTrips", "Id");
        }
    }
}
