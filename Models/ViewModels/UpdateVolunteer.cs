using HospitalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateVolunteer
    {
        public virtual Volunteer Volunteer { get; set; }
        
        public ApplicationUser.Gender Female = ApplicationUser.Gender.Female;
        public ApplicationUser.Gender Male = ApplicationUser.Gender.Male;
    }
}