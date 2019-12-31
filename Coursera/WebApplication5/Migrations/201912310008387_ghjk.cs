namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ghjk : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "password", c => c.String());
        }
    }
}
