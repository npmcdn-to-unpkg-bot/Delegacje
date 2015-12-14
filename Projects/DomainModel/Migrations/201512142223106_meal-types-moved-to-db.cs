namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mealtypesmovedtodb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SubsistenceMeals", "Type_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.SubsistenceMeals", "Type_Id");
            AddForeignKey("dbo.SubsistenceMeals", "Type_Id", "dbo.MealTypes", "Id");
            DropColumn("dbo.SubsistenceMeals", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubsistenceMeals", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.SubsistenceMeals", "Type_Id", "dbo.MealTypes");
            DropIndex("dbo.SubsistenceMeals", new[] { "Type_Id" });
            DropColumn("dbo.SubsistenceMeals", "Type_Id");
            DropTable("dbo.MealTypes");
        }
    }
}
