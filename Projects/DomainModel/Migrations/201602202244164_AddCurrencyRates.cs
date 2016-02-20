namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrencyRates : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Currencies");
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
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .Index(t => t.CurrencyId);
            
            AddColumn("dbo.Countries", "CurrencyId", c => c.Int(nullable: false));
            AddColumn("dbo.Currencies", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Currencies", "Id");
            CreateIndex("dbo.Countries", "CurrencyId");
            AddForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies", "Id", cascadeDelete: true);
            DropColumn("dbo.Currencies", "ExchangeRate");
            DropColumn("dbo.Currencies", "DateRefreshed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Currencies", "DateRefreshed", c => c.DateTime(nullable: false));
            AddColumn("dbo.Currencies", "ExchangeRate", c => c.Double(nullable: false));
            DropForeignKey("dbo.CurrencyRates", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.CurrencyRates", new[] { "CurrencyId" });
            DropIndex("dbo.Countries", new[] { "CurrencyId" });
            DropPrimaryKey("dbo.Currencies");
            DropColumn("dbo.Currencies", "Id");
            DropColumn("dbo.Countries", "CurrencyId");
            DropTable("dbo.CurrencyRates");
            AddPrimaryKey("dbo.Currencies", "Code");
        }
    }
}
