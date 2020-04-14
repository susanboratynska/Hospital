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
    public class VolunteerPosting
    {
        [Key]
        public int VolunteerPostingID { get; set; }
        public string VolunteerPostingTitle { get; set; }
        public string VolunteerPostingDescription { get; set; }
        public DateTime VolunteerPostingDate { get; set; }
        public ICollection<Application>Applications{ get; set; }
        //Representing the "Many" in (Many Volunteers to Many Postings)
        public ICollection<Volunteer> Volunteers { get; set; }
    }
}