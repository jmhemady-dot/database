using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Models
{
    public class User_Account
    {
        public int account_id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public int login_status { get; set; }

        public Person person { get; set; }

        public User_Group group { get; set; }

        public string status_desc
        {
            get { return login_status == 1 ? "Active" : "Inactive"; }
        }

        public string status_badge
        {
            get { return login_status == 1 ? "bg-success" : "bg-danger"; }
        }
    }
}