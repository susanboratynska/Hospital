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
    public class DonationController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: Donation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult DonationSuccess()
        {
            return View();
        }

        public ActionResult AddDonation(string DonorFirstName, string DonorLastName, string DonorEmail, string DonorPhone, string DonorAddress, string DonorCity, string DonorProvince, int DonationAmount)
        {
            Donation Donation = new Donation();
            Donation.DonorFirstName = DonorFirstName;
            Donation.DonorLastName = DonorLastName;
            Donation.DonorEmail = DonorEmail;
            Donation.DonorPhone = DonorPhone;
            Donation.DonorAddress = DonorAddress;
            Donation.DonorCity = DonorCity;
            Donation.DonorProvince= DonorProvince;
            Donation.DonationAmount = DonationAmount;
            Donation.DonationDate = DateTime.Now;
            db.Donations.Add(Donation);
            db.SaveChanges();
            return RedirectToAction("DonationSuccess");
        }

        public ActionResult List()
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string query = "Select * from Donations";
                List<Donation> donations = db.Donations.SqlQuery(query).ToList();
                return View(donations);
            }
        }

        public ActionResult View(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string query = "Select * from Donations where DonorID = @donorID";
                Donation donation = db.Donations.SqlQuery(query, new SqlParameter("@donorID", id)).FirstOrDefault();
                return View(donation);
            }
        }
    }
}