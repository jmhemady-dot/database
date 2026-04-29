using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ADNU_CFRS.Models
{
    public class Resource
    {
        public int resource_id { get; set; }

        public string resource_name { get; set; }

        public string resource_description { get; set; }

        public string resource_condition { get; set; }

        public int resource_type { get; set; }

        public int availability_status { get; set; }

        public string resource_type_desc
        {
            get
            {
                switch (resource_type)
                {
                    case 1:
                        return "Facility";
                    case 2:
                        return "Room";
                    default:
                        return "Unknown";
                }
            }
        }

        public string status_desc
        {
            get { return availability_status == 1 ? "Available" : "Unavailable"; }
        }

        public string status_badge
        {
            get { return availability_status == 1 ? "bg-success" : "bg-danger"; }
        }
    }

    public class Facility
    {
        public string location { get; set; }

        public string building { get; set; }

        public int floor_lvl { get; set; }

        public int capacity { get; set; }
    }

    public class Vehicle
    {
        public string plate_number { get; set; }

        public int capacity { get; set; }
    }
}