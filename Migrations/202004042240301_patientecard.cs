namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientecard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientEcards", "SenderEmail", c => c.String());
            AddColumn("dbo.PatientEcards", "DateSubmitted", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientEcards", "DateDelivered", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientEcards", "CardImage", c => c.String());
            DropColumn("dbo.PatientEcards", "CardSubject");
            DropColumn("dbo.PatientEcards", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientEcards", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientEcards", "CardSubject", c => c.String());
            DropColumn("dbo.PatientEcards", "CardImage");
            DropColumn("dbo.PatientEcards", "DateDelivered");
            DropColumn("dbo.PatientEcards", "DateSubmitted");
            DropColumn("dbo.PatientEcards", "SenderEmail");
        }
    }
}
