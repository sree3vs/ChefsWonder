namespace ChefsWonders.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordermodelchange2709201901 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ChefID", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "statuscode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "statuscode");
            DropColumn("dbo.OrderDetails", "ChefID");
        }
    }
}
