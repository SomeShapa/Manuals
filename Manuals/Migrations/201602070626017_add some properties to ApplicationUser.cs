namespace Manuals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsomepropertiestoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecondName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Language", c => c.String(nullable: false, defaultValue: "en"));
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Description");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Language");
            DropColumn("dbo.AspNetUsers", "SecondName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
