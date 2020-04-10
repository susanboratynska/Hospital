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
    public class ParkingController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: Parking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBooking(int parkingID, string bookingDateTime, int hours)
        {
            Debug.WriteLine(bookingDateTime);
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ParkingBooking booking = new ParkingBooking();
                booking.ParkingID = parkingID;
                booking.Hours = hours;
                booking.UserId = User.Identity.GetUserId();
                booking.BookingTime = Convert.ToDateTime(bookingDateTime);

                // Equivalent to SQL insert statement:
                db.ParkingBookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Bookings");
            }
        }

        public ActionResult UpdateBooking(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Parking> parkings = db.Parkings.ToList();
                ParkingUpdate parkingUpdate= new ParkingUpdate();
                SqlParameter[] sqlparams = new SqlParameter[2]; 
                sqlparams[0] = new SqlParameter("@userID", userID);
                sqlparams[1] = new SqlParameter("@id", id);
                ParkingBooking booking = db.ParkingBookings.SqlQuery("select * from ParkingBookings where BookingID = @id AND UserId = @userID", sqlparams).FirstOrDefault();
                parkingUpdate.Parkings = parkings;
                parkingUpdate.ParkingBooking = booking;
                Debug.WriteLine(parkingUpdate.ParkingBooking.BookingTime);
                return View(parkingUpdate);
            }
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
                sqlparams[0] = new SqlParameter("@user_id", userID);
                sqlparams[1] = new SqlParameter("@booking_id", id);
                string query = "select * from ParkingBookings where BookingID = @booking_id AND UserId = @user_id";
                ParkingBooking booking = db.ParkingBookings.SqlQuery(query, sqlparams).FirstOrDefault();
                
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
                sqlparams[0] = new SqlParameter("@user_id", userID);
                sqlparams[1] = new SqlParameter("@booking_id", id);
                string query = "DELETE from ParkingBookings where BookingID = @booking_id AND UserId = @user_id";
                db.Database.ExecuteSqlCommand(query, sqlparams);

                return RedirectToAction("Bookings");
            }
        }

        public ActionResult Book()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Parking> parkings = db.Parkings.ToList();
                return View(parkings);
            }
        }

        public ActionResult Bookings()
        {
            string userID = User.Identity.GetUserId();
            if(string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            } else
            {
                string query = "Select * from ParkingBookings";
                List<SqlParameter> sqlparams = new List<SqlParameter>();
                query = query + " where userId = @userId";
                sqlparams.Add(new SqlParameter("@userId", userID));
                List<ParkingBooking> parkingBookings = db.ParkingBookings.SqlQuery(query, sqlparams.ToArray()).ToList();
                return View(parkingBookings);
            }
        }
    }
}