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
    public class PatientEcard
    {
        [Key]

        // Things that describe a Patient Ecard:

        public int PatientCardID { get; set; }
        public string SenderFirstname { get; set; }
        public string SenderLastname { get; set; }
        public string PatientFirstname { get; set; }
        public string PatientLastname { get; set; }
        public string CardSubject { get; set; }
        public string CardMessage { get; set; }
        public DateTime Date { get; set; }
        public string PatientRoom { get; set; }

        // Set default value to FALSE:
        public bool CardDelivered { get; set; }
        public PatientEcard()
        {
            CardDelivered = false;
        }

        // Representing the Many in (One HospitalCampus to Many PatientEcards)
        public int CampusID { get; set; }
        [ForeignKey("CampusID")]
        public virtual HospitalCampus HospitalCampus { get; set; }
    }
}