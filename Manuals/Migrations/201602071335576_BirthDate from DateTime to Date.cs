namespace Manuals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BirthDatefromDateTimetoDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
        }
    }
}
