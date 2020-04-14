using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Data
{
    // To add profile data for the user, add more properties to our ApplicationUser class (REF: https://go.microsoft.com/fwlink/?LinkID=317594)
    // The ApplicationUser IdentityUser is the class used to describe a user who is logged in
    // Leverage this class by associating it with a Doctor, Volunteer, Admin, etc.

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // NOTE: the AuthenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
       // public virtual Volunteer Volunteer { get; set; }
            
       // public virtual Patient Patient { get; set; }

        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Gender UserGender { get; set; }
        public enum Gender
        { 
            Male,
            Female
        }
        
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public Type UserType { get; set; }

        public enum Type
        {
            Admin,
            Doctor,
            Patient,
            Volunteer
        }
        // A logged in user could be a Doctor:
        // public virtual Doctor Doctor { get; set; }

        // A logged in user can be a Patient:
        //public virtual Patient Patient { get; set; }

        // A logged in user could be Volunteer:
        //public virtual Volunteer Volunteer { get; set; }

        //A logged in user could be an Admin
    }


    public class HospitalProjectContext : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten
        // If you want Entity Framework to drop and regenerate your database automatically whenever you change your model schema, use data migrations.
        // For more information refer to the documentation: http://msdn.microsoft.com/en-us/data/jj591621.aspx


        public HospitalProjectContext() : base("name=HospitalProjectContext")
        {
        }
        public static HospitalProjectContext Create()
        {
            return new HospitalProjectContext();
        }

        public System.Data.Entity.DbSet<HospitalProject.Models.Event> Events { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.HospitalCampus> HospitalCampuses { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.PatientEcard> PatientEcards { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Patient> Patients { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Service> Services { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Invoice> Invoices{ get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Parking> Parkings { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.ParkingBooking> ParkingBookings { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Volunteer> Volunteers { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Application> Applications{ get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.VolunteerPosting> VolunteerPostings{ get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.DoctorAppointment> DoctorApointments { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Doctor> Doctors { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.Faq> Faqs { get; set; }
        public System.Data.Entity.DbSet<HospitalProject.Models.HealthLibrary> HealthLibraries { get; set; }

    }
}