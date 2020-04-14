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
using System.Web.UI.WebControls.WebParts;
using System.Globalization;

namespace HospitalProject.Controllers
{
    public class VolunteerPostingController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        public VolunteerPostingController() { }

        // GET: VolunteerPosting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<VolunteerPosting> volunteerpostings = db.VolunteerPostings.SqlQuery("Select * from VolunteerPostings").ToList();
            return View(volunteerpostings);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string VolunteerPostingDate, string VolunteerPostingTitle, string VolunteerPostingDescription)
        {
            VolunteerPosting newposting = new VolunteerPosting();
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            Debug.WriteLine(VolunteerPostingDate);
            newposting.VolunteerPostingDate = Convert.ToDateTime(VolunteerPostingDate);
            newposting.VolunteerPostingTitle = VolunteerPostingTitle;
            newposting.VolunteerPostingDescription = VolunteerPostingDescription;

            //first add posting
            db.VolunteerPostings.Add(newposting);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Show(string id)
        {
            //find data about the individual volunteer
            string main_query = "Select * from VolunteerPostings where VolunteerPostingID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            VolunteerPosting VolunteerPosting = db.VolunteerPostings.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            return View(VolunteerPosting);
        }
    }
}