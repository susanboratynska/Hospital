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
    public class Invoice
    {
        /*
            In one invoice there can be one or more services
            In invoice, there must a reference of list of services
        */ 
        [Key]
        public int InvoiceID { get; set; }  
        public double Amount { get; set; }
        public DateTime DateOfService { get; set; }

        public Boolean Status { get; set; }
        //Representing Many in (One Patient to Many Invoices)
        public int PatientID { get; set; }
        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }

        //Representing Many in (Many Invoices to Many Services)
       public ICollection<Service> Services { get; set; }
    }
}