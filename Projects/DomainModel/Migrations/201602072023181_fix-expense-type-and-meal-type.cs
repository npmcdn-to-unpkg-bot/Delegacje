namespace CrazyAppsStudio.Delegacje.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixexpensetypeandmealtype : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Expenses", name: "Type_Id", newName: "ExpenseTypeId");
            RenameColumn(table: "dbo.SubsistenceMeals", name: "Type_Id", newName: "MealTypeId");
            RenameIndex(table: "dbo.Expenses", name: "IX_Type_Id", newName: "IX_ExpenseTypeId");
            RenameIndex(table: "dbo.SubsistenceMeals", name: "IX_Type_Id", newName: "IX_MealTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SubsistenceMeals", name: "IX_MealTypeId", newName: "IX_Type_Id");
            RenameIndex(table: "dbo.Expenses", name: "IX_ExpenseTypeId", newName: "IX_Type_Id");
            RenameColumn(table: "dbo.SubsistenceMeals", name: "MealTypeId", newName: "Type_Id");
            RenameColumn(table: "dbo.Expenses", name: "ExpenseTypeId", newName: "Type_Id");
        }
    }
}
