using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Diagnostics;
//needed for await
using System.Threading.Tasks;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;


namespace HospitalProject.Controllers
{
    public class VolunteerController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private HospitalProjectContext db = new HospitalProjectContext();
        public VolunteerController() { }

        // GET: Volunteer
        public ActionResult List()
        {
            List<Volunteer> volunteers = db.Volunteers.SqlQuery("Select * from Volunteers").ToList();
            return View(volunteers);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(string FirstName, string LastName, ApplicationUser.Gender UserGender, string Address, string Province, string City, string PostalCode, string Email, string Password, string PhoneNumber)
        {
            
            //before creating volunteer, we will add it as a user.
            //This user will be linked with volunteer
            ApplicationUser NewUser = new ApplicationUser();
            NewUser.UserName = Email;
            NewUser.Email = Email;
            NewUser.FirstName = FirstName;
            NewUser.LastName = LastName;
            NewUser.UserGender = UserGender;
            NewUser.Address = Address;
            NewUser.Province = Province;
            NewUser.City = City;
            NewUser.PostalCode = PostalCode;
            NewUser.PhoneNumber = PhoneNumber;
            IdentityResult result = await UserManager.CreateAsync(NewUser, Password);

            //only create volunteer if account is created
            if (result.Succeeded)
                {
                    string Id = NewUser.Id;
                    string VolunteerID = Id;

                    string query = "Insert into Volunteers (VolunteerID) values (@id)";

                    SqlParameter[] sqlparams = new SqlParameter[1];
                    sqlparams[0] = new SqlParameter("@id", VolunteerID);

                    db.Database.ExecuteSqlCommand(query, sqlparams);
                }
            else
                {
                    //Display errors
                    ViewBag.ErrorMessage = "Something Went Wrong!";
                    ViewBag.Errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        ViewBag.Errors.Add(error);
                    }
                }
            return View("List");
        }

        public ActionResult Show(string id)
        {
            //find data about the individual volunteer
            string main_query = "Select * from Volunteers where VolunteerID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            Volunteer Volunteer = db.Volunteers.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //find data about Applications that this volunteer has applied
            string application_query = "Select * from Applications where Applications.VolunteerID=@id";
            var application_parameter = new SqlParameter("@id", id);
            List<Application> Applications = db.Applications.SqlQuery(application_query, application_parameter).ToList();

            ShowVolunteer viewmodel = new ShowVolunteer();
            viewmodel.volunteer = Volunteer;
            viewmodel.applications = Applications;
            
            return View(viewmodel);
        }

        public ActionResult Update(string id)
        {
            string query = "select * from Volunteers where VolunteerID= @id";
            var parameter = new SqlParameter("@id", id);
            Volunteer volunteer= db.Volunteers.SqlQuery(query, parameter).FirstOrDefault();
            UpdateVolunteer updateVolunteer = new UpdateVolunteer();
            updateVolunteer.Volunteer = volunteer;
            return View(updateVolunteer);
        }


        [HttpPost]
        public ActionResult Update(string id, string VolunteerFname, string VolunteerLname, string VolunteerEmail, string VolunteerPhone, ApplicationUser.Gender UserGender, string VolunteerAddress, string VolunteerProvince, string VolunteerCity, string PostalCode)
        {
            Debug.WriteLine(UserGender);
            Volunteer SelectedVolunteer = db.Volunteers.Find(id);
            var user =  db.Users.Find(SelectedVolunteer.ApplicationUser.Id);
            user.FirstName = VolunteerFname;
            user.LastName = VolunteerLname;
            user.Email = VolunteerEmail;
            user.PhoneNumber = VolunteerPhone;
            user.UserGender = UserGender;
            user.Address = VolunteerAddress;
            user.Province = VolunteerProvince;
            user.City = VolunteerCity;
            user.PostalCode = PostalCode;

            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult ConfirmDelete(string id)
        {
            string query = "select * from Volunteers where VolunteerID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Volunteer volunteer= db.Volunteers.SqlQuery(query, param).FirstOrDefault();
            return View(volunteer);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            string query = "delete from Volunteers where VolunteerID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            //delete associated account
            //(Account has same id -- one to one via fk and primary key).
            string account_query = "delete from AspNetUsers where Id=@id";
            db.Database.ExecuteSqlCommand(account_query, new SqlParameter("@id", id));

            return RedirectToAction("List");
        }


        public VolunteerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}