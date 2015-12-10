namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCountryKeyInExpenses : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Expenses", name: "Country_Id", newName: "CountryId");
            RenameIndex(table: "dbo.Expenses", name: "IX_Country_Id", newName: "IX_CountryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Expenses", name: "IX_CountryId", newName: "IX_Country_Id");
            RenameColumn(table: "dbo.Expenses", name: "CountryId", newName: "Country_Id");
        }
    }
}
