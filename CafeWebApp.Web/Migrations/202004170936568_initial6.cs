namespace CafeWebApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "FoodName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "FoodName");
        }
    }
}
