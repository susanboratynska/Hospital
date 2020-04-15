using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateApplication
    {
        //original application
        public virtual Application Application { get; set; }
        //list of volunteers
        public virtual List<Volunteer> Volunteers { get; set; }
        public virtual List<VolunteerPosting> VolunteerPostings { get; set; }

    }
}