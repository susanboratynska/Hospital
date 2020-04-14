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
    public class JobApplication
    {
        [Key]
        public int JobApplciationID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ResumeLink { get; set; }
        public int JobID { get; set; }
        [ForeignKey("JobID")]
        public virtual Job Job { get; set; }
    }
}