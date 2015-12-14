namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countriesupdated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Countries", "CultureCodeString");
            DropColumn("dbo.Countries", "CurrencyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "CurrencyName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Countries", "CultureCodeString", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
