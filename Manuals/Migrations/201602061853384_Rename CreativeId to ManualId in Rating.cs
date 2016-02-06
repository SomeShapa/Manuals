namespace Manuals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCreativeIdtoManualIdinRating : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Ratings", name: "Manual_Id", newName: "ManualId");
            RenameIndex(table: "dbo.Ratings", name: "IX_Manual_Id", newName: "IX_ManualId");
            DropPrimaryKey("dbo.Ratings");
            AddPrimaryKey("dbo.Ratings", new[] { "ManualId", "UserId" });
            DropColumn("dbo.Ratings", "CreativeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "CreativeId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Ratings");
            AddPrimaryKey("dbo.Ratings", new[] { "CreativeId", "UserId" });
            RenameIndex(table: "dbo.Ratings", name: "IX_ManualId", newName: "IX_Manual_Id");
            RenameColumn(table: "dbo.Ratings", name: "ManualId", newName: "Manual_Id");
        }
    }
}
