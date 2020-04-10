using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using HospitalProject.Models;
using HospitalProject.Data;
using System.Diagnostics;

namespace HospitalProject
{
    public partial class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        private string userid
        {
            get { return HttpContext.Current.User.Identity.GetUserId() != null ? HttpContext.Current.User.Identity.GetUserId() : ""; }
        }

        private bool isLoggedIn
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated ? true : false;  }
        }
        //public property accessors
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
        }

        public ApplicationUser GetUser()
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            if (userid == "") return null;
            return (manager.FindById(userid));
        }

        public bool IsUserAdmin()
        {
            ApplicationUser user = GetUser();
            if (user == null) return false;
            if (user.IsAdmin) return true;
            return false;
        }

        public bool IsUserVolunteer()
        {
            ApplicationUser user = GetUser();
            if (user == null) return false;
            if (user.Volunteer != null) return true;
            return false;
        }

        public bool IsUserPatient()
        {
            ApplicationUser user = GetUser();
            if (user == null) return false;
            if (user.Patient != null) return true;
            return false;
        }
    }
        
    public class HospitalAuthentication
    {
    }
}