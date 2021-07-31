namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimaryKeyForProductTableFix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.Products", "Id");
        }
    }
}
