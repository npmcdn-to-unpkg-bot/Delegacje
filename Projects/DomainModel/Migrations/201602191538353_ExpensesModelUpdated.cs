namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpensesModelUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "DocumentTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Expenses", "DoNotReturn", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Expenses", "DocumentTypeId");
            AddForeignKey("dbo.Expenses", "DocumentTypeId", "dbo.ExpenseDocumentTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "DocumentTypeId", "dbo.ExpenseDocumentTypes");
            DropIndex("dbo.Expenses", new[] { "DocumentTypeId" });
            DropColumn("dbo.Expenses", "DoNotReturn");
            DropColumn("dbo.Expenses", "DocumentTypeId");
        }
    }
}
