namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientecard2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientEcards", "DateDelivered", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientEcards", "DateDelivered", c => c.DateTime(nullable: false));
        }
    }
}
