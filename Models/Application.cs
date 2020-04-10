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
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }
        public int VolunteerID { get; set; }
        [ForeignKey("VolunteerID")]
        public virtual Volunteer Volunteer { get; set; }
        public int VolunteerPostingID { get; set; }
        [ForeignKey("VolunteerPostingID")]
        public virtual VolunteerPosting VolunteerPosting { get; set; }
        
        public string Education { get; set; }
        public string CurrentOccupation { get; set; }
        public int Experience { get; set; }

     
        
    }
}