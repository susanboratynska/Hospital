using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class AddEvent
    {
        // Provide a list of Hospital Campuses:
        public virtual List<HospitalCampus> HospitalCampuses { get; set; }

        // Provide a single Event:
        public virtual Event Event { get; set; }

    }
}