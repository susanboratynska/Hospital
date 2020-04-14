using System;
using System.Collections.Generic;
using System.Data;
// Required for SqlParameter class
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
using System.IO;
using Microsoft.AspNet.Identity;

namespace HospitalProject.Controllers
{
    public class ApplicationController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Application> applications= db.Applications.SqlQuery("Select * from Applications").ToList();
            return View(applications);
        }
        public ActionResult Add()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                AddApplication viewmodel = new AddApplication();
                viewmodel.Volunteers = db.Volunteers.ToList();
                viewmodel.VolunteerPostings = db.VolunteerPostings.ToList();
                return View(viewmodel);
            }
        }

        [HttpPost]
        public ActionResult Add(int VolunteerID, int VolunteerPostingID, string VolunteerEducation, string VolunteerOccupation, int VolunteerExperience)
        {
            Application newapplication = new Application();
            newapplication.VolunteerID = VolunteerID;
            newapplication.VolunteerPostingID = VolunteerPostingID;
            newapplication.Education = VolunteerEducation;
            newapplication.CurrentOccupation = VolunteerOccupation;
            newapplication.Experience = VolunteerExperience;

            //first add application 
            db.Applications.Add(newapplication);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Show(string id)
        {
            //find data about individual application
            string main_query = "Select * from Applications where ApplicationID=@id";
            var pk_parameter = new SqlParameter("@id",id);
            Application Application = db.Applications.SqlQuery(main_query, pk_parameter).FirstOrDefault();
            return View(Application);
        }
    }
}