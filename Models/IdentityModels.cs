using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalProject.Models
{
    // NOTE: 
    // The structure for this file has been ported over into Data/HospitalProjectContext
    // This file is very similar to a database context file. The difference is that it is of type "IdentityDbContext" instead of type "DbContext"



    /*
    
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    // ApplicationUser decribes the person that's logged in
    // This class is referenced in the class below
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // Christine's example: New thing that describes a user:
        // If you want this applied, you'll need to recompile the db using Add-Migration and Update-Database:
        public string DOB {get; set; }
    }


    // This class holds all the information required to create the db tables and also describes the concept of an <ApplicationUser> on the db
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    // The only difference between these 2 classes is that the ApplicationDbCOntext is a sub-class of the IdentityDbContext
    // The difference btwn the 2 is that the IdentityDbContext understands that there's going to be a logged in user involved.
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

      
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    */
    
}