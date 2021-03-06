﻿using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
// Required for SqlParameter class
using System.Data.Entity;
using System.Diagnostics;
// Require to use cultrueinfo.invariantculture to parse Date:
using System.Linq;
using System.Web.Mvc;


namespace HospitalProject.Controllers
{
    public class PatientEcardController : Controller
    {

        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: PatientEcard
        public ActionResult Index()
        {
            return View();
        }

        // List Patient Ecards:
        public ActionResult List(string searchkey = "", string delivered = "", string campus = "", int pagenum = 0)
        {
            ListPatientEcards viewmodel = new ListPatientEcards();
            viewmodel.HospitalCampuses = db.HospitalCampuses.ToList();

            Debug.WriteLine("The searchkey is " + searchkey);

            if (searchkey != "")
            {
                List<PatientEcard> PatientEcards = db.PatientEcards
                    .Where(patientecard =>
                        patientecard.SenderFirstname.Contains(searchkey) ||
                        patientecard.SenderLastname.Contains(searchkey) ||
                        patientecard.PatientFirstname.Contains(searchkey) ||
                        patientecard.PatientLastname.Contains(searchkey) ||
                        patientecard.CardMessage.Contains(searchkey) ||
                        patientecard.PatientRoom.Contains(searchkey)
                    ).OrderByDescending(p => p.DateSubmitted).ThenBy(d => d.CardDelivered).ToList();
                viewmodel.PatientEcards = PatientEcards;
            }
            else if (delivered != "")
            {
                bool deliveredbool = bool.Parse(delivered);
                List<PatientEcard> PatientEcards = db.PatientEcards.Where(patientcard => patientcard.CardDelivered == deliveredbool).OrderByDescending(p => p.DateSubmitted).ThenBy(d => d.CardDelivered).ToList();
                viewmodel.PatientEcards = PatientEcards;
            }
            else if (campus != "")
            {
                int campusid = int.Parse(campus);
                List<PatientEcard> PatientEcards = db.PatientEcards.Where(patientcard => patientcard.CampusID == campusid).OrderByDescending(p => p.DateSubmitted).ThenBy(d => d.CardDelivered).ToList();
                viewmodel.PatientEcards = PatientEcards;
            }
            else
            {
                List<PatientEcard> PatientEcards = db.PatientEcards.OrderByDescending(p => p.DateSubmitted).ThenBy(d => d.CardDelivered).ToList();
                viewmodel.PatientEcards = PatientEcards;
            }


       

            // Pagination for List of Patient eCards:

            // "perpage": number of records per page
            int perpage = 3;
            int eventcount = viewmodel.PatientEcards.Count();
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
            if (maxpage > 1)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                if (searchkey != "")
                {
                    List<PatientEcard> PatientEcards = db.PatientEcards
                        .Where(patientecard =>
                            patientecard.SenderFirstname.Contains(searchkey) ||
                            patientecard.SenderLastname.Contains(searchkey) ||
                            patientecard.PatientFirstname.Contains(searchkey) ||
                            patientecard.PatientLastname.Contains(searchkey) ||
                            patientecard.CardMessage.Contains(searchkey) ||
                            patientecard.PatientRoom.Contains(searchkey)
                        ).OrderBy(p => p.DateSubmitted).ThenBy(d => d.CardDelivered)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
                    viewmodel.PatientEcards = PatientEcards;
                }
                else if (delivered != "")
                {
                    bool deliveredbool = bool.Parse(delivered);
                    List<PatientEcard> PatientEcards = db.PatientEcards.Where(patientcard => patientcard.CardDelivered == deliveredbool).OrderBy(d => d.CardDelivered).ThenByDescending(p => p.DateSubmitted)
                        .Skip(start)
                        .Take(perpage)
                        .ToList();
                    viewmodel.PatientEcards = PatientEcards;
                }
                else if (campus != "")
                {
                    int campusid = int.Parse(campus);
                    List<PatientEcard> PatientEcards = db.PatientEcards.Where(patientcard => patientcard.CampusID == campusid).OrderBy(d => d.CardDelivered).ThenByDescending(p => p.DateSubmitted)
                        .Skip(start)
                        .Take(perpage)
                        .ToList();
                    viewmodel.PatientEcards = PatientEcards;
                }
                else
                {
                    List<PatientEcard> PatientEcards = db.PatientEcards.OrderBy(d => d.CardDelivered).ThenByDescending(p => p.DateSubmitted)
                        .Skip(start)
                        .Take(perpage)
                        .ToList();
                    viewmodel.PatientEcards = PatientEcards;
                }
                // End of LINQ pagination algorithm

            }

            return View(viewmodel);
        }



        // Show single Patient Ecard (Admin View):
        public ActionResult Show(int PatientCardID)
        {
            PatientEcard PatientEcard = db.PatientEcards.Include(c => c.HospitalCampus).FirstOrDefault(c => c.PatientCardID == PatientCardID);

            return View(PatientEcard);

        }

        // List Hospital Campuses for Adding a new PatientEcard:
        public ActionResult Add()
        {
            ListPatientEcards viewmodel = new ListPatientEcards();
            viewmodel.HospitalCampuses = db.HospitalCampuses.ToList();
            return View(viewmodel);
        }

        // Public View: Add new Patient eCard
        [HttpPost]
        public ActionResult Add(string SenderFirstname, string SenderLastname, string SenderEmail, string PatientFirstname, string PatientLastname, string CardMessage, string PatientRoom, int CampusID, string CardImage)
        {

            PatientEcard patientecard = new PatientEcard();
            patientecard.SenderFirstname = SenderFirstname;
            patientecard.SenderLastname = SenderLastname;
            patientecard.SenderEmail = SenderEmail;
            patientecard.PatientFirstname = PatientFirstname;
            patientecard.PatientLastname = PatientLastname;
            patientecard.CardMessage = CardMessage;
            patientecard.PatientRoom = PatientRoom;
            patientecard.CampusID = CampusID;
            patientecard.CardImage = CardImage;
            patientecard.DateSubmitted = DateTime.Now;
            patientecard.DateDelivered = null;


            // Equivalent to SQL insert statement:
            db.PatientEcards.Add(patientecard);

            db.SaveChanges();

            //string redirectstring = "?PatientCardID=" + patientecard.PatientCardID;

            return RedirectToAction("Confirm/", new { id = patientecard.PatientCardID });
        }

        // Public View: Show card to user after submit

        public ActionResult Confirm(int id)
        {
            PatientEcard confirmsubmission = db.PatientEcards.Find(id);

            return View(confirmsubmission);

        }


        // Display Existing Patient eCard Details:
        public ActionResult Update()
        {
            //AddPatientEcard viewmodel = new AddPatientEcard();
            //viewmodel.HospitalCampuses = db.HospitalCampuses.ToList();
            //viewmodel.PatientEcard = db.PatientEcards.Find(PatientCardID);
            //return View(viewmodel);
            return View();
        }

        // Update Patient eCard: 
        [HttpPost]
        public ActionResult Update(int PatientCardID, string SenderFirstname, string SenderLastname, string PatientFirstname, string PatientLastname, string CardMessage, string PatientRoom, int CampusID)
        {
            PatientEcard UpdateCard = db.PatientEcards.Find(PatientCardID);
            UpdateCard.SenderFirstname = SenderFirstname;
            UpdateCard.SenderLastname = SenderLastname;
            UpdateCard.PatientFirstname = PatientFirstname;
            UpdateCard.PatientLastname = PatientLastname;
            UpdateCard.CardMessage = CardMessage;
            UpdateCard.PatientRoom = PatientRoom;
            UpdateCard.CampusID = CampusID;

            db.SaveChanges();

            return RedirectToAction("Show");
        }


        // No delete option. Keep records of all card submissions:

        //[HttpPost]
        //public ActionResult Delete(int PatientCardID)
        //{
        //    PatientEcard SelectedCard = db.PatientEcards.Find(PatientCardID);
        //    // Equivalent to SQL delete statement:
        //    db.PatientEcards.Remove(SelectedCard);
        //    db.SaveChanges();

        //    return RedirectToAction("List");
        //}



        // Change Delivery Status:
        [HttpPost]
        public ActionResult Deliver(int PatientCardID)
        {
            PatientEcard UpdateDelivery = db.PatientEcards.Find(PatientCardID);
            UpdateDelivery.CardDelivered = true;
            UpdateDelivery.DateDelivered = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("List");

        }






    }
}