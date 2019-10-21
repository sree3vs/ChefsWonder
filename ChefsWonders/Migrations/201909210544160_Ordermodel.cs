namespace ChefsWonders.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ordermodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        FoodItems_FoodItemID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailsID)
                .ForeignKey("dbo.FoodItems", t => t.FoodItems_FoodItemID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.FoodItems_FoodItemID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        statuscode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.StatusMaster", t => t.statuscode, cascadeDelete: true)
                .Index(t => t.statuscode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "statuscode", "dbo.StatusMaster");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "FoodItems_FoodItemID", "dbo.FoodItems");
            DropIndex("dbo.Orders", new[] { "statuscode" });
            DropIndex("dbo.OrderDetails", new[] { "FoodItems_FoodItemID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
