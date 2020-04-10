using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ListPatientEcards
    {
        // Provide a list of Hospital Campuses:
        public virtual List<HospitalCampus> HospitalCampuses { get; set; }

        // Provide a list of Patient Ecards:
        public virtual List<PatientEcard> PatientEcards { get; set; }

    }
}