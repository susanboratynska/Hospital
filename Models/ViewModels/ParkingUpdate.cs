using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ParkingUpdate
    {
        public virtual List<Parking> Parkings { get; set; }
        public virtual ParkingBooking ParkingBooking { get; set; }
    }
}