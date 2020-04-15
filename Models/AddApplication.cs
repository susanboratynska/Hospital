using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class AddApplication
    {
        //list of volunteers
        public virtual List<Volunteer> Volunteers { get; set; }
        //list of postings
        public virtual List<VolunteerPosting> VolunteerPostings { get; set; }

    }
}