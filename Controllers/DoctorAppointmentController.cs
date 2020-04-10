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
    public class DoctorAppointmentController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Doctor> doctors = db.Doctors.ToList();
                return View(doctors);
            }
        }

        public ActionResult AddBooking(int doctorId, string bookingDateTime, string comments)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Patient patient = db.Patients.SqlQuery("select * from Patients where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                DoctorAppointment appointment = new DoctorAppointment();
                appointment.DoctorID = doctorId;
                appointment.PatientComments = comments;
                appointment.PatientID = patient.PatientID;
                appointment.BookingDateTime = Convert.ToDateTime(bookingDateTime);

                // Equivalent to SQL insert statement:
                db.DoctorApointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Bookings");
            }
        }

        public ActionResult Bookings()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string query = "Select * from DoctorAppointments";
                List<SqlParameter> sqlparams = new List<SqlParameter>();
                query = query + " where patientId = @patientId";
                Patient patient = db.Patients.SqlQuery("select * from Patients where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                sqlparams.Add(new SqlParameter("@patientId", patient.PatientID));
                List<DoctorAppointment> doctorAppointments = db.DoctorApointments.SqlQuery(query, sqlparams.ToArray()).ToList();
                return View(doctorAppointments);
            }
        }
    }
}