using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Models
{
    public class User_Group
    {
        public int group_id { get; set; }

        public string group_name { get; set; }

        public User_Role role { get; set; }
    }
}