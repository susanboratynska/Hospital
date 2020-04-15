using HospitalProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace HospitalProject.Models
{
    public class AddPatient
    {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.")]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.")]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public ApplicationUser.Gender UserGender { get; set; }

            public enum Gender
            {
                Male,
                Female
            }

            [Required]
            [DataType(DataType.MultilineText)]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Province")]
            public string Province { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required]
            [DataType(DataType.PostalCode)]
            [Display(Name = "Postal Code")]
            public string PostalCode { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "User Type")]
            public ApplicationUser.Type UserType { get; set; }

            public enum Type
            {
                Admin,
                Doctor,
                Patient,
                Volunteer
            }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency First Name")]
            public string EmergencyContactFname { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Last Name")]
            public string EmergencyContactLname { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Emergency Phone")]
            public string EmergencyContactPhone{ get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Emergency Relation")]
            public string EmergencyContactRelation{ get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Allergy")]
            public Allergies Allergy { get; set; }

            public enum Allergies
            {
                Antibiotics,
                Aspirin,
                Sulfa,
                Insulin,
                Penicillin,
                Ibuprofen
            }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Blood Group")]
        public BloodGroups BloodGroup{ get; set; }

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

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "MedicalHistory")]
        public string MedicalHistory { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "HealthCardNumber")]
        public string HealthCardNumber { get; set; }

    }
}
