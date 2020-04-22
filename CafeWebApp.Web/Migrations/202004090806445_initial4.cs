namespace CafeWebApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        FoodId = c.Int(nullable: false),
                        FoodQuantity = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FoodId)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Foods", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Carts", "FoodId", "dbo.Foods");
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.Carts", new[] { "FoodId" });
            DropTable("dbo.Carts");
        }
    }
}
