using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalProject.Data;

namespace HospitalProject.Models
{
    public class Faq
    {
        [Key]
        public int FaqID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool Published { get; set; }
    }
}