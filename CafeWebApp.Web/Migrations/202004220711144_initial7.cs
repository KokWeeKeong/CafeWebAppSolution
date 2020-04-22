namespace CafeWebApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tables", "UserId", "dbo.Users");
            DropIndex("dbo.Tables", new[] { "UserId" });
            AlterColumn("dbo.Tables", "UserId", c => c.Int());
            CreateIndex("dbo.Tables", "UserId");
            AddForeignKey("dbo.Tables", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tables", "UserId", "dbo.Users");
            DropIndex("dbo.Tables", new[] { "UserId" });
            AlterColumn("dbo.Tables", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tables", "UserId");
            AddForeignKey("dbo.Tables", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
