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
                query = query + " where PatientID = @patientId";
                Patient patient = db.Patients.SqlQuery("select * from Patients where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                if (patient == null)
                {
                    Doctor doctor = db.Doctors.SqlQuery("select * from Doctors where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                    if (doctor != null)
                    {
                        return RedirectToAction("MyBookings");
                    }
                    else
                    {
                        return RedirectToAction("InvalidUser");
                    }
                }
                sqlparams.Add(new SqlParameter("@patientId", patient.PatientID));
                List<DoctorAppointment> doctorAppointments = db.DoctorApointments.SqlQuery(query, sqlparams.ToArray()).ToList();
                return View(doctorAppointments);
            }
        }

        public ActionResult MyBookings()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Doctor doctor = db.Doctors.SqlQuery("select * from Doctors where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                if (doctor == null)
                {
                    return RedirectToAction("InvalidUser");
                }
                string query = "Select * from DoctorAppointments";
                List<SqlParameter> sqlparams = new List<SqlParameter>();
                query = query + " where DoctorID = @doctorId";
                sqlparams.Add(new SqlParameter("@doctorId", doctor.DoctorID));
                List<DoctorAppointment> doctorAppointments = db.DoctorApointments.SqlQuery(query, sqlparams.ToArray()).ToList();
                return View(doctorAppointments);
            }
        }

        public ActionResult InvalidUser()
        {
            return View();
        }

        public ActionResult ConfirmBooking(int? id)
        {
            DoctorAppointment DoctorAppointment = db.DoctorApointments.Find(id);
            DoctorAppointment.Confirmed = true;
            db.SaveChanges();
            return RedirectToAction("Bookings");
        }

        public ActionResult CancelBooking(int? id)
        {
            DoctorAppointment DoctorAppointment = db.DoctorApointments.Find(id);
            DoctorAppointment.Confirmed = false;
            db.SaveChanges();
            return RedirectToAction("Bookings");
        }

        public ActionResult Update(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Doctor> doctors = db.Doctors.ToList();
                SqlParameter[] sqlparams = new SqlParameter[2];
                Patient patient = db.Patients.SqlQuery("select * from Patients where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                sqlparams[0] = new SqlParameter("@patient_id", patient.PatientID);
                sqlparams[1] = new SqlParameter("@id", id);
                DoctorAppointment booking = db.DoctorApointments.SqlQuery("select * from DoctorAppointments where BookingID = @id AND PatientID = @patient_id", sqlparams).FirstOrDefault();
                DoctorsAppointmentUpdate doctorsAppointmentUpdate = new DoctorsAppointmentUpdate();
                doctorsAppointmentUpdate.Doctors = doctors;
                doctorsAppointmentUpdate.DoctorAppointment= booking;
                return View(doctorsAppointmentUpdate);
            }
        }

        public ActionResult UpdateBooking(int doctorId, int bookingID, string bookingDateTime, string comments)
        {
            DoctorAppointment DoctorAppointment = db.DoctorApointments.Find(bookingID);
            DoctorAppointment.DoctorID = doctorId;
            DoctorAppointment.BookingDateTime = Convert.ToDateTime(bookingDateTime);
            DoctorAppointment.PatientComments = comments;
            DoctorAppointment.Confirmed = false;
            db.SaveChanges();
            return RedirectToAction("Bookings");
        }

        public ActionResult Delete(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                Patient patient = db.Patients.SqlQuery("select * from Patients where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                sqlparams[0] = new SqlParameter("@patient_id", patient.PatientID);
                sqlparams[1] = new SqlParameter("@booking_id", id);
                string query = "select * from DoctorAppointments where BookingID = @booking_id AND PatientID = @patient_id";
                DoctorAppointment booking = db.DoctorApointments.SqlQuery(query, sqlparams).FirstOrDefault();
                return View(booking);
            }
        }

        public ActionResult ConfirmDelete(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                Patient patient = db.Patients.SqlQuery("select * from Patients where UserId = @id", new SqlParameter("@id", userID)).FirstOrDefault();
                sqlparams[0] = new SqlParameter("@patient_id", patient.PatientID);
                sqlparams[1] = new SqlParameter("@booking_id", id);
                string query = "DELETE from DoctorAppointments where BookingID = @booking_id AND PatientID = @patient_id";
                db.Database.ExecuteSqlCommand(query, sqlparams);
                return RedirectToAction("Bookings");
            }
        }
    }    
}