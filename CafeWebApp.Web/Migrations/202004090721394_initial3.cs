namespace CafeWebApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        FoodName = c.String(),
                        FoodPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FoodId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Foods");
        }
    }
}
