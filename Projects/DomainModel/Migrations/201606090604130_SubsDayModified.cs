namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubsDayModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubsistenceDays", "Diet", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SubsistenceDays", "IsForeign", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubsistenceDays", "IsForeign");
            DropColumn("dbo.SubsistenceDays", "Diet");
        }
    }
}
