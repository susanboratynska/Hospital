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
using System.IO;

namespace HospitalProject.Controllers
{
    public class EventController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: Event
        public ActionResult Index()
        {
            return View();
        }


        // List Events:
        public ActionResult List(string searchkey = "")
        {
            Debug.WriteLine("The searchkey is " + searchkey);

            if (searchkey != null || searchkey != "")
            {
                List<Event> Events = db.Events
                    .Where(Event =>
                        Event.EventTitle.Contains(searchkey) ||
                        Event.EventDescription.Contains(searchkey) ||
                        Event.EventLocation.Contains(searchkey)
                    ).ToList();
                    return View(Events);
            }
            else
            {
            List<Event> Events = db.Events.ToList();
            return View(Events);
            }

        }


        // Display an Event:
        public ActionResult Show(int EventID)
        {
            Event Event = db.Events.Include(c => c.HospitalCampus).FirstOrDefault(c => c.EventID == EventID);

            return View(Event);

        }

        // Add New Event:
        public ActionResult Add()
        {
            List<HospitalCampus> hospitalcampus = db.HospitalCampuses.ToList();   
            return View(hospitalcampus);
        }


    }
}