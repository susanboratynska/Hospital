﻿namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        EmergencyContactFname = c.String(),
                        EmergencyContactLname = c.String(),
                        EmergencyContactPhone = c.String(),
                        EmergencyContactRelation = c.String(),
                        Allergy = c.Int(nullable: false),
                        BloodGroup = c.Int(nullable: false),
                        MedicalHistory = c.String(),
                        HealthCardNumber = c.String(),
                    })
                .PrimaryKey(t => t.PatientID);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                        Patient_PatientID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Patient_PatientID);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Patient_PatientID", "dbo.Patients");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceInvoices", "Invoice_InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.ServiceInvoices", "Service_ServiceID", "dbo.Services");
            DropForeignKey("dbo.Invoices", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.PatientEcards", "CampusID", "dbo.HospitalCampus");
            DropForeignKey("dbo.Events", "CampusID", "dbo.HospitalCampus");
            DropIndex("dbo.ServiceInvoices", new[] { "Invoice_InvoiceID" });
            DropIndex("dbo.ServiceInvoices", new[] { "Service_ServiceID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Patient_PatientID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ParkingBookings", new[] { "UserId" });
            DropIndex("dbo.ParkingBookings", new[] { "ParkingID" });
            DropIndex("dbo.Invoices", new[] { "PatientID" });
            DropIndex("dbo.PatientEcards", new[] { "CampusID" });
            DropIndex("dbo.Events", new[] { "CampusID" });
            DropTable("dbo.ServiceInvoices");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parkings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ParkingBookings");
            DropTable("dbo.Services");
            DropTable("dbo.Patients");
            DropTable("dbo.Invoices");
            DropTable("dbo.PatientEcards");
            DropTable("dbo.HospitalCampus");
            DropTable("dbo.Events");
        }
    }
}