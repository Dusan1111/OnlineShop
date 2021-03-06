namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "UnitOfMeasurement", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "UnitOfMeasurement", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
