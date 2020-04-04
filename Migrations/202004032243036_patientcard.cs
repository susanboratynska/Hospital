namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientcard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientEcards", "CardDelivered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientEcards", "CardDelivered");
        }
    }
}
