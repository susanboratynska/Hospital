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
    public class VolunteerController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private HospitalProjectContext db = new HospitalProjectContext();
        public VolunteerController() { }

        // GET: Volunteer
        public ActionResult List(string volunteersearchkey, int pagenum = 0)
        {
            string query = "Select * from Volunteers";
            List<SqlParameter> sqlparams = new List<SqlParameter>();
            if (volunteersearchkey != "")
            {
                query = query + " where FirstName like @searchkey";
                sqlparams.Add(new SqlParameter("@searchkey", "%" + volunteersearchkey + "%"));
            }

            List<Volunteer> volunteers = db.Volunteers.SqlQuery(query, sqlparams.ToArray()).ToList();

            int perpage = 3;
            int volunteercount = volunteers.Count();
            int maxpage = (int)Math.Ceiling((decimal)volunteercount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
           
            return View();
           
        }
    }
}