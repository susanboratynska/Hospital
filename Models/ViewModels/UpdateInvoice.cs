using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateInvoice
    {
        //original invoice
        public virtual Invoice Invoice { get; set; }
        //list of patients
        public virtual List<Patient> Patients { get; set; }
        public virtual List<Service> Services { get; set; }
    }
}