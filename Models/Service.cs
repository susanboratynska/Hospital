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
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}