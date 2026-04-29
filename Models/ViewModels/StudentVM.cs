using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Models.ViewModels
{
    public class StudentVM
    {
        public StudentVM()
        {
            reservations = new List<Reservation>();
        }

        public List<Reservation> reservations { get; set; }
    }
}