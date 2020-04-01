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
    public class Event
    {
        // Things that describe an Event:

        [Key]
        public int EventID { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string EventLocation {get; set;}
        public DateTime EventDateTime { get; set; }
        public string EventHostingDepartment { get; set; }

        public int CampusID { get; set; }
        [ForeignKey("CampusID")]
        public virtual HospitalCampus HospitalCampus { get; set; }

    }
}