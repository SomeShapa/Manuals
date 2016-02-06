namespace Manuals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        DateAdded = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ImageLink = c.String(unicode: false),
                        VideoLink = c.String(unicode: false),
                        Body = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, unicode: false),
                        ManualId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manuals", t => t.ManualId)
                .Index(t => t.ManualId);
            
            CreateTable(
                "dbo.RatingComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ManualId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Liked = c.Boolean(),
                        Manual_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManualId, t.UserId })
                .ForeignKey("dbo.Manuals", t => t.Manual_Id)
                .Index(t => t.Manual_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 50, unicode: false),
                        SecondName = c.String(maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        Theme = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        BirthDate = c.DateTime(),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        ImageLink = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BasketTags",
                c => new
                    {
                        ManualId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManualId, t.TagId })
                .ForeignKey("dbo.Manuals", t => t.ManualId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ManualId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.BasketMedals",
                c => new
                    {
                        MedalId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MedalId, t.UserId })
                .ForeignKey("dbo.Medals", t => t.MedalId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MedalId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Manuals", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.BasketMedals", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.BasketMedals", "MedalId", "dbo.Medals");
            DropForeignKey("dbo.Manuals", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.BasketTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.BasketTags", "ManualId", "dbo.Manuals");
            DropForeignKey("dbo.Ratings", "Manual_Id", "dbo.Manuals");
            DropForeignKey("dbo.Comments", "ManualId", "dbo.Manuals");
            DropForeignKey("dbo.RatingComments", "CommentId", "dbo.Comments");
            DropIndex("dbo.BasketMedals", new[] { "UserId" });
            DropIndex("dbo.BasketMedals", new[] { "MedalId" });
            DropIndex("dbo.BasketTags", new[] { "TagId" });
            DropIndex("dbo.BasketTags", new[] { "ManualId" });
            DropIndex("dbo.Ratings", new[] { "Manual_Id" });
            DropIndex("dbo.RatingComments", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "ManualId" });
            DropIndex("dbo.Manuals", new[] { "CategoryId" });
            DropIndex("dbo.Manuals", new[] { "UserId" });
            DropTable("dbo.BasketMedals");
            DropTable("dbo.BasketTags");
            DropTable("dbo.Medals");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Tags");
            DropTable("dbo.Ratings");
            DropTable("dbo.RatingComments");
            DropTable("dbo.Comments");
            DropTable("dbo.Manuals");
            DropTable("dbo.Categories");
        }
    }
}
