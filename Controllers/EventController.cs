using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
// Required for SqlParameter class
using System.Data.Entity;
using System.Diagnostics;
// Require to use cultrueinfo.invariantculture to parse EventDate and EventTime:
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

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
            // SRC: Christine Bittle: MVC PetGrooming https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            // PURPOSE: Put both date and time values in one record
            // Unable to convert string value less 10:00 to Hmm so need to force a leading 0:
            int EventTimeInt = Int32.Parse(EventTime);
            if(EventTimeInt < 1000)
            {
                EventTime = "0" + EventTime;
            }
            Debug.WriteLine(EventDate + " " + EventTime);
            NewEvent.EventDate = DateTime.ParseExact(EventDate + " " + EventTime, "yyyy-MM-dd Hmm", System.Globalization.CultureInfo.InvariantCulture);
            NewEvent.CampusID = CampusID;


            // LINQ equivalent to Insert Statement and save to DB:
            db.Events.Add(NewEvent);
            db.SaveChanges();

            return RedirectToAction("List");
        }




        // Display Existing Event Details:
        public ActionResult Update(int EventID)
        {
            AddEvent viewmodel = new AddEvent();
            viewmodel.HospitalCampuses = db.HospitalCampuses.ToList();
            viewmodel.Event = db.Events.Find(EventID);
            return View(viewmodel);
        }
        
        // Update Event: 
        [HttpPost]
        public ActionResult Update(int EventID, string EventTitle, string EventDescription, string EventLocation, string EventHostingDepartment, string EventDate, string EventTime, int CampusID)
        {
            Event UpdateEvent = db.Events.Find(EventID);
            UpdateEvent.EventTitle = EventTitle;
            UpdateEvent.EventDescription = EventDescription;
            UpdateEvent.EventLocation = EventLocation;
            UpdateEvent.EventHostingDepartment = EventHostingDepartment;
            // SRC: Crhstine Bittle: https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            // PURPOSE: Put both date and time values in one record
            Debug.WriteLine(EventDate + " " + EventTime);
            UpdateEvent.EventDate = DateTime.ParseExact(EventDate + " " + EventTime, "yyyy-MM-dd Hmm", CultureInfo.InvariantCulture);

            UpdateEvent.CampusID = CampusID;

            // LINQ equivalent to Insert Statement and save to DB:
            db.Events.Add(UpdateEvent);
            db.SaveChanges();

            return RedirectToAction("Show");
        }


        [HttpPost]
        public ActionResult Delete(int EventID)
        {
            Event SelectedEvent = db.Events.Find(EventID);
            // Equivalent to SQL delete statement:
            db.Events.Remove(SelectedEvent);
            db.SaveChanges();

            return RedirectToAction("List");
        }





    }
}