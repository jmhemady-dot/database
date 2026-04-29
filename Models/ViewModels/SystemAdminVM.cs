using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Models.ViewModels
{
    public class SystemAdminVM
    {
        public SystemAdminVM()
        {
            users = new List<User_Account>();
        }

        public List<User_Account> users { get; set; }

        public User_Account user_account { get; set; }

        public Student student { get; set; }

        public Employee employee { get; set; }

        public External external { get; set; }
    }
}