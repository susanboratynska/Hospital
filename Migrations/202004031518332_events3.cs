namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class events3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "EventStart");
            DropColumn("dbo.Events", "EventEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EventEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "EventStart", c => c.DateTime(nullable: false));
        }
    }
}
