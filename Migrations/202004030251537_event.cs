namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _event : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "EventStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "EventEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "EventDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EventDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "EventEnd");
            DropColumn("dbo.Events", "EventStart");
            DropColumn("dbo.Events", "EventDate");
        }
    }
}
