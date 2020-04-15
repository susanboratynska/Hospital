using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ShowPatient
    {

        //one individual patient
        public virtual Patient patient { get; set; }
        // a list of service they have used
        public List<Service> services { get; set; }

        
        public List<Service> all_services{ get; set; }
        public List<Invoice> invoices{ get; set; }
    }
}