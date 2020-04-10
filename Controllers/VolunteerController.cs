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
using PetGrooming.Data;
using PetGrooming.Models;
using PetGrooming.Models.ViewModels;
using System.Diagnostics;
using System.IO;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HospitalProject.Data;
using HospitalProject.Models;

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