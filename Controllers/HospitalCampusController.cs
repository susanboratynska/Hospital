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
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class HospitalCampusController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: HospitalCampus
        public ActionResult Index()
        {
            return View();
        }


        // List Hospital Campuses:
        public ActionResult List(string searchkey)
        {
            Debug.WriteLine("The searchkey is " + searchkey);

            if (searchkey != null && searchkey != "")
            {
                List<HospitalCampus> HospitalCampuses = db.HospitalCampuses
                    .Where(hospitalcampus =>
                        hospitalcampus.CampusName.Contains(searchkey) ||
                        hospitalcampus.CampusAddressLine1.Contains(searchkey) ||
                        hospitalcampus.CampusAddressLine2.Contains(searchkey) ||
                        hospitalcampus.CampusPC.Contains(searchkey)
                    ).ToList();
                return View(HospitalCampuses);
            }
            else
            {
                List<HospitalCampus> HospitalCampuses = db.HospitalCampuses.ToList();
                return View(HospitalCampuses);
            }

            
        }

        // Add New Hospital Campus:
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string CampusName, string CampusAddressLine1, string CampusAddressLine2, string CampusCity, string CampusProvince, string CampusPC, string CampusPhone)
        {
            HospitalCampus NewCampus = new HospitalCampus();
            NewCampus.CampusName = CampusName;
            NewCampus.CampusAddressLine1 = CampusAddressLine1;
            NewCampus.CampusAddressLine2 = CampusAddressLine2;
            NewCampus.CampusCity = CampusCity;
            NewCampus.CampusProvince = CampusProvince;
            NewCampus.CampusPC = CampusPC;
            NewCampus.CampusPhone = CampusPhone;

            // Equivalent to SQL insert statement:
            db.HospitalCampuses.Add(NewCampus);

            db.SaveChanges();

            return RedirectToAction("List");
        }




    }

}