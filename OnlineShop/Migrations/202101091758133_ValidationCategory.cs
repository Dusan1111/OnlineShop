namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategories", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductCategories", "Name", c => c.String());
        }
    }
}
