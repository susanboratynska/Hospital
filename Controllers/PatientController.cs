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
    public class PatientController : Controller
    {
        // GET: Patient
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private HospitalProjectContext db = new HospitalProjectContext();
        public PatientController() { }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult List()
        {
            List<Patient> patients= db.Patients.SqlQuery("Select * from Patients").ToList();
            return View(patients);
        }
       
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(string FirstName, string LastName, ApplicationUser.Gender UserGender, string Address, string Province, string City, string PostalCode, string Email, string Password, string PhoneNumber)
        {

            //before creating patient, we will add it as a user.
            //This user will be linked with patient
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

            //only create patient if account is created
            if (result.Succeeded)
            {
                string Id = NewUser.Id;
                string PatientID = Id;

                string query = "Insert into Patients (PatientID) values (@id)";

                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@id", PatientID);

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
            //find data about the individual patient
            string main_query = "Select * from Patients where PatientID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            Patient Patient = db.Patients.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //find data about invoice that this patient has
            string invoice_query = "Select * from Invoices where Invoices.PatientID=@id";
            var invoice_parameter = new SqlParameter("@id", id);
            List<Invoice> Invoices= db.Invoices.SqlQuery(invoice_query, invoice_parameter).ToList();

            ShowPatient viewmodel = new ShowPatient();
            viewmodel.patient = Patient;
            viewmodel.invoices = Invoices;


            return View(viewmodel);
        }

        public ActionResult ConfirmDelete(string id)
        {
            string query = "select * from Patients where PatientID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Patient patient = db.Patients.SqlQuery(query, param).FirstOrDefault();
            return View(patient);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            string query = "delete from Patients where PatientID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            //delete associated account
            //(Account has same id -- one to one via fk and primary key).
            string account_query = "delete from AspNetUsers where Id=@id";
            db.Database.ExecuteSqlCommand(account_query, new SqlParameter("@id", id));

            return RedirectToAction("List");
        }


        public PatientController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
