namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrencies : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Currencies", "ExchangeRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Currencies", "ExchangeRate", c => c.String(nullable: false));
        }
    }
}
