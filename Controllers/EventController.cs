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
// Require to use cultrueinfo.invariantculture to parse EventDate and EventTime:
using System.Globalization;

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

        // List Hospital Campuses for Adding a new Event:
        public ActionResult Add()
        {
            List<HospitalCampus> hospitalcampus = db.HospitalCampuses.ToList();   
            return View(hospitalcampus);
        }

        // Add New Event:
        [HttpPost]
        public ActionResult Add(string EventTitle, string EventDescription, string EventLocation, string EventHostingDepartment, string EventDate, string EventTime, int CampusID)
        {
            Event NewEvent = new Event();
            NewEvent.EventTitle = EventTitle;
            NewEvent.EventDescription = EventDescription;
            NewEvent.EventLocation = EventLocation;
            NewEvent.EventHostingDepartment = EventHostingDepartment;
            // SRC: Crhstine Bittle: https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            // PURPOSE: Put both date and time values in one record
            Debug.WriteLine(EventDate + " " + EventTime);
            NewEvent.EventDate = DateTime.ParseExact(EventDate + " " + EventTime, "yyyy-MM-dd Hmm", CultureInfo.InvariantCulture);
      
            NewEvent.CampusID = CampusID;

            // LINQ equivalent to Insert Statement and save to DB:
            db.Events.Add(NewEvent);
            db.SaveChanges();

            return RedirectToAction("List");
        }




        // Display Existing Event Details:
        public ActionResult Update(int EventID)
        {
            Event SelectedEvent = db.Events.Find(EventID);

            return View(EventID);
        }
    }
}