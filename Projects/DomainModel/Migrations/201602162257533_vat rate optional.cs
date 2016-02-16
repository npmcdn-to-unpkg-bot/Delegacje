namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vatrateoptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Expenses", "VATRate", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Expenses", "VATRate", c => c.Double(nullable: false));
        }
    }
}
