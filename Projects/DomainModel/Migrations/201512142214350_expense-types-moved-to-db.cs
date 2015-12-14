namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expensetypesmovedtodb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Expenses", "Type_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Expenses", "Type_Id");
            AddForeignKey("dbo.Expenses", "Type_Id", "dbo.ExpenseTypes", "Id");
            DropColumn("dbo.Expenses", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.Expenses", "Type_Id", "dbo.ExpenseTypes");
            DropIndex("dbo.Expenses", new[] { "Type_Id" });
            DropColumn("dbo.Expenses", "Type_Id");
            DropTable("dbo.ExpenseTypes");
        }
    }
}
