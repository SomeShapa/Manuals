namespace Manuals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteUserProfiletable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Manuals", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Medals", "UserProfile_Id", "dbo.UserProfiles");
            DropIndex("dbo.Manuals", new[] { "UserProfile_Id" });
            DropIndex("dbo.Medals", new[] { "UserProfile_Id" });
            DropColumn("dbo.Manuals", "UserProfile_Id");
            DropColumn("dbo.Medals", "UserProfile_Id");
            DropTable("dbo.UserProfiles");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Medals", "UserProfile_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Manuals", "UserProfile_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Medals", "UserProfile_Id");
            CreateIndex("dbo.Manuals", "UserProfile_Id");
            AddForeignKey("dbo.Medals", "UserProfile_Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Manuals", "UserProfile_Id", "dbo.UserProfiles", "Id");
        }
    }
}
