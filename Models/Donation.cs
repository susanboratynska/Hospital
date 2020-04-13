using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Install  Entity Framework 6 under Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalProject.Data;


namespace HospitalProject.Models
{
    public class Donation
    {
        [Key]
        public int DonorID { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public string DonorEmail { get; set; }
        public string DonorPhone { get; set; }
        public string DonorAddress { get; set; }
        public string DonorCity { get; set; }
        public string DonorProvince { get; set; }
        public int DonationAmount { get; set; }
        public DateTime DonationDate { get; set; }
    }
}