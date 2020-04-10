using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ListApplications
    {
        //List of volunteers:
        public virtual List<Volunteer> Volunteers { get; set; }

        //List of VolunteerPostings:
        public virtual List<VolunteerPosting> VolunteerPostings { get; set; }
        public virtual List<Application> Applications { get; set; }
    }
}