using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Install  Entity Framework 6 under Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalProject.Data;

namespace HospitalProject.Models
{
    public class ParkingBooking
    {
        [Key]
        public int BookingID { get; set; }
        public DateTime BookingTime { get; set; }
        public int Hours { get; set; }

        public int ParkingID { get; set; }
        [ForeignKey("ParkingID")]
        public virtual Parking Parking { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}