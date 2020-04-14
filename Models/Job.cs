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
    public class Job
    {
        [Key]
        public int JobID { get; set; }
        public string Position { get; set; }
        public int JobTypeID { get; set; }
        [ForeignKey("JobTypeID")]
        public virtual JobType JobType { get; set; }
        public int Experience { get; set; }
        public string Qualification { get; set; }

    }
}