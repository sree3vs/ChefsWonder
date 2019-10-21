namespace ChefsWonders.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelchange250919 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "FoodItems_FoodItemID", "dbo.FoodItems");
            DropIndex("dbo.OrderDetails", new[] { "FoodItems_FoodItemID" });
            RenameColumn(table: "dbo.OrderDetails", name: "FoodItems_FoodItemID", newName: "FoodItemID");
            AlterColumn("dbo.OrderDetails", "FoodItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "FoodItemID");
            AddForeignKey("dbo.OrderDetails", "FoodItemID", "dbo.FoodItems", "FoodItemID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "FoodItemID", "dbo.FoodItems");
            DropIndex("dbo.OrderDetails", new[] { "FoodItemID" });
            AlterColumn("dbo.OrderDetails", "FoodItemID", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "FoodItemID", newName: "FoodItems_FoodItemID");
            CreateIndex("dbo.OrderDetails", "FoodItems_FoodItemID");
            AddForeignKey("dbo.OrderDetails", "FoodItems_FoodItemID", "dbo.FoodItems", "FoodItemID");
        }
    }
}
