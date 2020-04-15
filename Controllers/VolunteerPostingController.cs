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
            //find data about the individual volunteer posting
            string main_query = "Select * from VolunteerPostings where VolunteerPostingID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            VolunteerPosting VolunteerPosting = db.VolunteerPostings.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            return View(VolunteerPosting);
        }

        public ActionResult Update(int id)
        {
            string query = "select * from VolunteerPostings where VolunteerPostingID= @id";
            var parameter = new SqlParameter("@id", id);
            VolunteerPosting volunteerposting= db.VolunteerPostings.SqlQuery(query, parameter).FirstOrDefault();

            return View(volunteerposting);
        }
        [HttpPost]
        public ActionResult Update(int id, string VolunteerPostingDate, string VolunteerPostingTitle, string VolunteerPostingDescription)
        {
            string query = "update VolunteerPostings set VolunteerPostingDate = @VolunteerPostingDate, VolunteerPostingTitle= @VolunteerPostingTitle, VolunteerPostingDescription=@VolunteerPostingDescription where VolunteerPostingID= @id";

            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@VolunteerPostingDate", VolunteerPostingDate);
            sqlparams[2] = new SqlParameter("@VolunteerPostingTitle", VolunteerPostingTitle);
            sqlparams[3] = new SqlParameter("@VolunteerPostingDescription", VolunteerPostingDescription);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult ConfirmDelete(string id)
        {
            string query = "select * from VolunteerPostings where VolunteerPostingID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            VolunteerPosting volunteerposting = db.VolunteerPostings.SqlQuery(query, param).FirstOrDefault();
            return View(volunteerposting);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            string query = "delete from VolunteerPostings where VolunteerPostingID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }

    }
}