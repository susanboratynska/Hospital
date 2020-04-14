using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ShowVolunteer
    {
        //one individual volunteer
        public virtual Volunteer volunteer { get; set; }
        // a list of postings they have applied for
        public List<VolunteerPosting> volunteerpostings { get; set; }

        // a seperate list for representing the ADD of a volunteer to a posting,
        //i.e. show a dropdownlist of all postings, with "Apply for Volunteer Post" on show Volunteer.
        public List<VolunteerPosting> all_volunteerpostings { get; set; }
        public List<Application> applications { get; set; }
    }
}