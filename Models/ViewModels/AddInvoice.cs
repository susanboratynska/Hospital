using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class AddInvoice
    {
        //list of patients
        public virtual List<Patient> Patients{ get; set; }
        //list of services
        public virtual List<Service>Services{ get; set; }
    }
}