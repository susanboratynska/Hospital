﻿using System;
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
    public class Volunteer
        //Volunteer must reference a list of postings
    {
        [Key]
        public int VolunteerID { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        //hasFile can be 0 or 1 indicating (1) => has file (0)=> no file
        //Content/Pets/{id}.{FileExtension}
        public int HasFile { get; set; }
        //.pdf, .doc. 
      
        public ICollection<Application>Applications{ get; set; }
        public ICollection<VolunteerPosting> VolunteerPostings { get; set; }

    }
}