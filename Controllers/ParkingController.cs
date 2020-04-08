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