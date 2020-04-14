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
        public ActionResult List(string searchkey = "", string campus = "", int pagenum = 0)
        {
            ListEvents viewmodel = new ListEvents();
            viewmodel.HospitalCampuses = db.HospitalCampuses.ToList();

            Debug.WriteLine("The searchkey is " + searchkey);

            if (searchkey != "")
            {
                List<Event> Events = db.Events
                    .Where(Event =>
                        Event.EventTitle.Contains(searchkey) ||
                        Event.EventDescription.Contains(searchkey) ||
                        Event.EventLocation.Contains(searchkey)
                    ).ToList();
                viewmodel.Events = Events;

            }
            else if (campus != "")
            {
                // Must parse to int value to check against DB values:
                int campusid = int.Parse(campus);
                List<Event> Events = db.Events.Where(h => h.CampusID == campusid).ToList();
                viewmodel.Events = Events;

            }
            else
            {
                List<Event> Events = db.Events.ToList();
                viewmodel.Events = Events;
            }


            // Pagination for List of Events:

            // "perpage": number of records per page
            int perpage = 3;
            int eventcount = viewmodel.Events.Count();
            // "maxpage": -1 to offset index starting at 0
            int maxpage = (int)Math.Ceiling((decimal)eventcount / perpage) - 1;
            // "maxpage": max number of pages. Obtained by taking total number of records divided by the number of records perpage (round up)
            if (maxpage < 0) maxpage = 0;
            // "pagenum": current page
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            // "start": describes which record we start on. Obtained by multiplying the number of records per page by the total number of pages:
            int start = (int)(perpage * pagenum);

            // Get maxpage value so to only display pagination if there are more than 3 records in the PatientEcards table:
            ViewData["maxpage"] = maxpage;
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                if (searchkey != "")
                {
                    viewmodel.Events = db.Events
                    .Where(Event =>
                        Event.EventTitle.Contains(searchkey) ||
                        Event.EventDescription.Contains(searchkey) ||
                        Event.EventLocation.Contains(searchkey))
                    .OrderBy(Event => Event.EventDate)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
                }
                else if (campus != "")
                {

                    int campusid = int.Parse(campus);
                    List<Event> Events = db.Events.Where(h => h.CampusID == campusid)
                        .OrderBy(Event => Event.EventDate)
                        .Skip(start)
                        .Take(perpage)
                        .ToList();
                    viewmodel.Events = Events;

                }
                else
                {
                    List<Event> Events = db.Events
                        .OrderBy(Event => Event.EventDate)
                        .Skip(start)
                        .Take(perpage)
                        .ToList();
                    viewmodel.Events = Events;
                }
                // End of LINQ pagination algorithm

            }


            return View(viewmodel);

        }


        // Show a single Event:
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
            if (EventTimeInt < 1000)
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
            // SRC: Christine Bittle: https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
            // PURPOSE: Put both date and time values in one record
            int EventTimeInt = Int32.Parse(EventTime);
            if (EventTimeInt < 1000)
            {
                EventTime = "0" + EventTime;
            }
            Debug.WriteLine(EventDate + " " + EventTime);
            UpdateEvent.EventDate = DateTime.ParseExact(EventDate + " " + EventTime, "yyyy-MM-dd Hmm", System.Globalization.CultureInfo.InvariantCulture);

            UpdateEvent.CampusID = CampusID;


            db.SaveChanges();

            return RedirectToAction("List");
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