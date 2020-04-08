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
    public class Patient

    {
        // Things that describes a Patient:
        [Key]
        public int PatientID { get; set; } 
        public string EmergencyContactFname { get; set; }
        public string EmergencyContactLname{ get; set; }
        public string EmergencyContactPhone { get; set; }
        public string EmergencyContactRelation{ get; set; }
        public Allergies Allergy{ get; set; }
        public BloodGroups BloodGroup{ get; set; }
        public string MedicalHistory{ get; set; }
        public string HealthCardNumber{ get; set; }

    }

    //"https://www.webmd.com/allergies/most-common-drugs-that-cause-allergies"
    public enum Allergies
    { //most common allergies, this list will be displaed as checkbox so if a person has more than one allergy it will easy to mention that
        Antibiotics,
        Aspirin,
        Sulfa,
        Insulin,
        Penicillin,
        Ibuprofen
    }

    // reference from "https://odetocode.com/blogs/scott/archive/2012/09/04/working-with-enums-and-templates-in-asp-net-mvc.aspx"
    public enum BloodGroups
    {
        [Display(Name = "A+")]
        Apositive,

        [Display(Name = "A-")]
        Anegative,

        [Display(Name = "B+")]
        Bpositive,

        [Display(Name = "B-")]
        Bnegative,

        [Display(Name = "O+")]
        Opositive,

        [Display(Name = "O-")]
        Onegative,

        [Display(Name = "AB+")]
        ABpositive,

        [Display(Name = "AB-")]
        ABnegative
    }
}