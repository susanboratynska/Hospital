using System;
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
    public class DoctorAppointment
    {
        [Key]
        public int BookingID { get; set; }
        public DateTime BookingDateTime { get; set; }
        public string PatientComments { get; set; }
        public bool Confirmed { get; set; }
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }
        public int DoctorID { get; set; }
        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }
    }
}