using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ADNU_CFRS.Models
{
    public class Reservation
    {
        public int reservation_id{ get; set; }

        public DateTime reservation_date { get; set; }

        public string purpose { get; set; }

        public string start_time { get; set; }

        public string end_time { get; set; }

        public int no_of_attendees { get; set; }

        public int reservation_status { get; set; }

        public Person person { get; set; }

        public Resource resource { get; set; }

        public string status_desc
        {
            get 
            { 
                if (reservation_status == 0)
                {
                    return "Pending";
                }
                else if (reservation_status == 1)
                {
                    return "Approved";
                }
                else if (reservation_status == 2)
                {
                    return "Rejected";
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        public string status_badge
        {
            get 
            { 
                if (reservation_status == 0)
                {
                    return "bg-warning";
                }
                else if (reservation_status == 1)
                {
                    return "bg-success";
                }
                else if (reservation_status == 2)
                {
                    return "bg-danger";
                }
                else
                {
                    return "bg-secondary";
                }
            }
        }
    }
}