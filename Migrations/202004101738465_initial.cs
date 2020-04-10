namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationID = c.Int(nullable: false, identity: true),
                        VolunteerID = c.Int(nullable: false),
                        Education = c.String(),
                        CurrentEducation = c.String(),
                        Experience = c.Int(nullable: false),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.ApplicationID)
                .ForeignKey("dbo.Volunteers", t => t.VolunteerID, cascadeDelete: true)
                .Index(t => t.VolunteerID);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        HasFile = c.Int(nullable: false),
                        FileExtension = c.String(),
                    })
                .PrimaryKey(t => t.VolunteerID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserGender = c.Int(nullable: false),
                        Address = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                        UserType = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.DoctorAppointments",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingDateTime = c.DateTime(nullable: false),
                        PatientComments = c.String(),
                        Confirmed = c.Boolean(nullable: false),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        Specialization = c.String(),
                        Qualification = c.String(),
                        Contact = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        EmergencyContactFname = c.String(),
                        EmergencyContactLname = c.String(),
                        EmergencyContactPhone = c.String(),
                        EmergencyContactRelation = c.String(),
                        Allergy = c.Int(nullable: false),
                        BloodGroup = c.Int(nullable: false),
                        MedicalHistory = c.String(),
                        HealthCardNumber = c.String(),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(),
                        EventDescription = c.String(),
                        EventLocation = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        EventHostingDepartment = c.String(),
                        CampusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.HospitalCampus", t => t.CampusID, cascadeDelete: true)
                .Index(t => t.CampusID);
            
            CreateTable(
                "dbo.HospitalCampus",
                c => new
                    {
                        CampusID = c.Int(nullable: false, identity: true),
                        CampusName = c.String(),
                        CampusAddressLine1 = c.String(),
                        CampusAddressLine2 = c.String(),
                        CampusCity = c.String(),
                        CampusProvince = c.String(),
                        CampusPC = c.String(),
                        CampusPhone = c.String(),
                    })
                .PrimaryKey(t => t.CampusID);
            
            CreateTable(
                "dbo.PatientEcards",
                c => new
                    {
                        PatientCardID = c.Int(nullable: false, identity: true),
                        SenderFirstname = c.String(),
                        SenderLastname = c.String(),
                        SenderEmail = c.String(),
                        PatientFirstname = c.String(),
                        PatientLastname = c.String(),
                        CardMessage = c.String(),
                        DateSubmitted = c.DateTime(nullable: false),
                        PatientRoom = c.String(),
                        CardDelivered = c.Boolean(nullable: false),
                        DateDelivered = c.DateTime(),
                        CardImage = c.String(),
                        CampusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientCardID)
                .ForeignKey("dbo.HospitalCampus", t => t.CampusID, cascadeDelete: true)
                .Index(t => t.CampusID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        DateOfService = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        PatientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceID = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                    })
                .PrimaryKey(t => t.ServiceID);
            
            CreateTable(
                "dbo.ParkingBookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingTime = c.DateTime(nullable: false),
                        Hours = c.Int(nullable: false),
                        ParkingID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Parkings", t => t.ParkingID, cascadeDelete: true)
                .Index(t => t.ParkingID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        ParkingID = c.Int(nullable: false, identity: true),
                        ParkingTitle = c.String(),
                        Location = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ParkingID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.VolunteerPostings",
                c => new
                    {
                        VolunteerPostingID = c.Int(nullable: false, identity: true),
                        VolunteerPostingTitle = c.String(),
                        VolunteerPostingDescription = c.String(),
                        VolunteerPostingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VolunteerPostingID);
            
            CreateTable(
                "dbo.ServiceInvoices",
                c => new
                    {
                        Service_ServiceID = c.Int(nullable: false),
                        Invoice_InvoiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_ServiceID, t.Invoice_InvoiceID })
                .ForeignKey("dbo.Services", t => t.Service_ServiceID, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceID, cascadeDelete: true)
                .Index(t => t.Service_ServiceID)
                .Index(t => t.Invoice_InvoiceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ParkingBookings", "ParkingID", "dbo.Parkings");
            DropForeignKey("dbo.ParkingBookings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceInvoices", "Invoice_InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.ServiceInvoices", "Service_ServiceID", "dbo.Services");
            DropForeignKey("dbo.Invoices", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.PatientEcards", "CampusID", "dbo.HospitalCampus");
            DropForeignKey("dbo.Events", "CampusID", "dbo.HospitalCampus");
            DropForeignKey("dbo.DoctorAppointments", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Patients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DoctorAppointments", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applications", "VolunteerID", "dbo.Volunteers");
            DropForeignKey("dbo.Volunteers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ServiceInvoices", new[] { "Invoice_InvoiceID" });
            DropIndex("dbo.ServiceInvoices", new[] { "Service_ServiceID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ParkingBookings", new[] { "UserId" });
            DropIndex("dbo.ParkingBookings", new[] { "ParkingID" });
            DropIndex("dbo.Invoices", new[] { "PatientID" });
            DropIndex("dbo.PatientEcards", new[] { "CampusID" });
            DropIndex("dbo.Events", new[] { "CampusID" });
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.Doctors", new[] { "UserId" });
            DropIndex("dbo.DoctorAppointments", new[] { "DoctorID" });
            DropIndex("dbo.DoctorAppointments", new[] { "PatientID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Volunteers", new[] { "UserId" });
            DropIndex("dbo.Applications", new[] { "VolunteerID" });
            DropTable("dbo.ServiceInvoices");
            DropTable("dbo.VolunteerPostings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parkings");
            DropTable("dbo.ParkingBookings");
            DropTable("dbo.Services");
            DropTable("dbo.Invoices");
            DropTable("dbo.PatientEcards");
            DropTable("dbo.HospitalCampus");
            DropTable("dbo.Events");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.DoctorAppointments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Volunteers");
            DropTable("dbo.Applications");
        }
    }
}
