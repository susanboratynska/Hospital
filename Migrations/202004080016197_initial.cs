namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingBookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingTime = c.DateTime(nullable: false),
                        ParkingID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Parkings", t => t.ParkingID, cascadeDelete: true)
                .Index(t => t.ParkingID)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingBookings", "ParkingID", "dbo.Parkings");
            DropForeignKey("dbo.ParkingBookings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ParkingBookings", new[] { "UserId" });
            DropIndex("dbo.ParkingBookings", new[] { "ParkingID" });
            DropTable("dbo.ParkingBookings");
        }
    }
}
