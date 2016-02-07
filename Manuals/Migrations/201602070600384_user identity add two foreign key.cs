namespace Manuals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useridentityaddtwoforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketMedals", "MedalId", "dbo.Medals");
            DropForeignKey("dbo.BasketMedals", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.Manuals", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.BasketMedals", new[] { "MedalId" });
            DropIndex("dbo.BasketMedals", new[] { "UserId" });
            CreateTable(
                "dbo.BasketMedal",
                c => new
                    {
                        MedalId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MedalId, t.UserId })
                .ForeignKey("dbo.Medals", t => t.MedalId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MedalId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Manuals", "UserProfile_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Medals", "UserProfile_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Manuals", "UserProfile_Id");
            CreateIndex("dbo.Medals", "UserProfile_Id");
            AddForeignKey("dbo.Manuals", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medals", "UserProfile_Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Manuals", "UserProfile_Id", "dbo.UserProfiles", "Id");
            DropTable("dbo.BasketMedals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BasketMedals",
                c => new
                    {
                        MedalId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MedalId, t.UserId });
            
            DropForeignKey("dbo.Manuals", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Medals", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.BasketMedal", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BasketMedal", "MedalId", "dbo.Medals");
            DropForeignKey("dbo.Manuals", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BasketMedal", new[] { "UserId" });
            DropIndex("dbo.BasketMedal", new[] { "MedalId" });
            DropIndex("dbo.Medals", new[] { "UserProfile_Id" });
            DropIndex("dbo.Manuals", new[] { "UserProfile_Id" });
            DropColumn("dbo.Medals", "UserProfile_Id");
            DropColumn("dbo.Manuals", "UserProfile_Id");
            DropTable("dbo.BasketMedal");
            CreateIndex("dbo.BasketMedals", "UserId");
            CreateIndex("dbo.BasketMedals", "MedalId");
            AddForeignKey("dbo.Manuals", "UserId", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.BasketMedals", "UserId", "dbo.UserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BasketMedals", "MedalId", "dbo.Medals", "Id", cascadeDelete: true);
        }
    }
}
