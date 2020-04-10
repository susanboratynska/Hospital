using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class DoctorsAppointmentUpdate
    {
        public virtual List<Doctor> Doctors { get; set; }
        public virtual DoctorAppointment DoctorAppointment { get; set; }
    }
}