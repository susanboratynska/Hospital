using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateJob
    {
        public virtual List<JobType> JobTypes { get; set; }
        public virtual Job Job { get; set; }
    }
}