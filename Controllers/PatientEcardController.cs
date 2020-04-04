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
using System.Globalization;
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
        public ActionResult List(string searchkey = "")
        {
            Debug.WriteLine("The searchkey is " + searchkey);

            if (searchkey != null || searchkey != "")
            {
                List<PatientEcard> PatientEcards = db.PatientEcards
                    .Where(patientecard =>
                        patientecard.SenderFirstname.Contains(searchkey) ||
                        patientecard.SenderLastname.Contains(searchkey) ||
                        patientecard.PatientFirstname.Contains(searchkey) ||
                        patientecard.PatientLastname.Contains(searchkey) ||
                        patientecard.CardSubject.Contains(searchkey) ||
                        patientecard.CardMessage.Contains(searchkey) ||
                        patientecard.PatientRoom.Contains(searchkey)
                    ).ToList();
                return View(PatientEcards);
            }
            else
            {
                List<PatientEcard> PatientEcards = db.PatientEcards.ToList();
                return View(PatientEcards);
            }

        }

        public ActionResult Show(int PatientCardID)
        {
            PatientEcard PatientEcard = db.PatientEcards.Include(c => c.HospitalCampus).FirstOrDefault(c => c.PatientCardID == PatientCardID);

            return View(PatientEcard);

        }

        // List Hospital Campuses for Adding a new PatientEcard:
        public ActionResult Add()
        {
            List<PatientEcard> PatientEcard= db.PatientEcards.ToList();
            return View(PatientEcard);
        }

        [HttpPost]
        public ActionResult Add(string SenderFirstname, string SenderLastname, string PatientFirstname, string PatientLastname, string CardSubject, string CardMessage, string PatientRoom, int CampusID)
        {
            PatientEcard AddCard = new PatientEcard();
            AddCard.SenderFirstname = SenderFirstname;
            AddCard.SenderLastname = SenderLastname;
            AddCard.PatientFirstname = PatientFirstname;
            AddCard.PatientLastname = PatientLastname;
            AddCard.CardSubject = CardSubject;
            AddCard.CardMessage = CardMessage;
            AddCard.PatientRoom = PatientRoom;
            AddCard.CampusID = CampusID;

            // Equivalent to SQL insert statement:
            db.PatientEcards.Add(AddCard);

            db.SaveChanges();

            return RedirectToAction("List");
        }


        // Display Existing Patient Ecard Details:
        public ActionResult Update()
        {
            //AddPatientEcard viewmodel = new AddPatientEcard();
            //viewmodel.HospitalCampuses = db.HospitalCampuses.ToList();
            //viewmodel.PatientEcard = db.PatientEcards.Find(PatientCardID);
            //return View(viewmodel);
            return View();
        }

        // Update Patient Card: 
        [HttpPost]
        public ActionResult Update(int PatientCardID, string SenderFirstname, string SenderLastname, string PatientFirstname, string PatientLastname, string CardSubject, string CardMessage, string PatientRoom, int CampusID)
        {
            PatientEcard UpdateCard = db.PatientEcards.Find(PatientCardID);
            UpdateCard.SenderFirstname = SenderFirstname;
            UpdateCard.SenderLastname = SenderLastname;
            UpdateCard.PatientFirstname = PatientFirstname;
            UpdateCard.PatientLastname = PatientLastname;
            UpdateCard.CardSubject = CardSubject;
            UpdateCard.CardMessage = CardMessage;
            UpdateCard.PatientRoom = PatientRoom;
            UpdateCard.CampusID = CampusID;

            db.SaveChanges();

            return RedirectToAction("Show");
        }


        [HttpPost]
        public ActionResult Delete(int PatientCardID)
        {
            PatientEcard SelectedCard = db.PatientEcards.Find(PatientCardID);
            // Equivalent to SQL delete statement:
            db.PatientEcards.Remove(SelectedCard);
            db.SaveChanges();

            return RedirectToAction("List");
        }







    }
}