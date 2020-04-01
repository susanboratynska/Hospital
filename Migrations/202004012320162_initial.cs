namespace HospitalProject.Migrations
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
                        EventDateTime = c.DateTime(nullable: false),
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
                        PatientFirstname = c.String(),
                        PatientLastname = c.String(),
                        CardSubject = c.String(),
                        CardMessage = c.String(),
                        Date = c.DateTime(nullable: false),
                        PatientRoom = c.String(),
                        CampusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientCardID)
                .ForeignKey("dbo.HospitalCampus", t => t.CampusID, cascadeDelete: true)
                .Index(t => t.CampusID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PatientEcards", "CampusID", "dbo.HospitalCampus");
            DropForeignKey("dbo.Events", "CampusID", "dbo.HospitalCampus");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PatientEcards", new[] { "CampusID" });
            DropIndex("dbo.Events", new[] { "CampusID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PatientEcards");
            DropTable("dbo.HospitalCampus");
            DropTable("dbo.Events");
        }
    }
}
