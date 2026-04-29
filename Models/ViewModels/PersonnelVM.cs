using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Models.ViewModels
{
    public class PersonnelVM
    {
        public PersonnelVM()
        {
            resources = new List<Resource>();
        }

        public List<Resource> resources { get; set; }

        public Resource resource { get; set; }

        public Facility facility { get; set; }

        public Vehicle vehicle { get; set; }
    }
}