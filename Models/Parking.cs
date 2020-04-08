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
    public class Parking
    {
        [Key]
        public int ParkingID { get; set; }
        public string ParkingTitle { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
}