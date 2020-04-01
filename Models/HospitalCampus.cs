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
    public class HospitalCampus
    {
        // Things that describe a Hospital Campus:
        [Key]
        public int CampusID { get; set; }
        public string CampusName { get; set; }
        public string CampusAddressLine1 {get; set;}
        public string CampusAddressLine2 { get; set; }
        public string CampusCity { get; set; }
        public string CampusProvince { get; set; }
        public string CampusPC { get; set; }
        public string CampusPhone { get; set; }
    }
}