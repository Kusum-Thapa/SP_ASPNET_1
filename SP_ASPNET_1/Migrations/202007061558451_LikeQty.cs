namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeQty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPost", "LikeQty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPost", "LikeQty");
        }
    }
}
