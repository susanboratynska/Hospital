using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ListEvents
    {
        // Provide a list of Hospital Campuses:
        public virtual List<HospitalCampus> HospitalCampuses { get; set; }

        // Provide a list of Events:
        public virtual List<Event> Events { get; set; }

    }
}