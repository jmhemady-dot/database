using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Models
{
    public class Person
    {
        public int person_id { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string contact_number { get; set; }

        public string email_address { get; set; }

        public int person_type { get; set; }

        public string person_type_desc
        {
            get
            {
                switch (person_type)
                {
                    case 1:
                        return "System Administrator";
                    case 2:
                        return "Student";
                    case 3:
                        return "Employee";
                    case 4:
                        return "External";
                    default:
                        return "Unknown";
                }
            }
        }
    }

    public class Student
    {
        public string course { get; set; }

        public string dept { get; set; }

        public int year_level { get; set; }
    }

    public class Employee
    {
        public string position { get; set; }

        public string dept { get; set; }

        public int employee_type { get; set; }
    }

    public class  External
    {
        public string organization_name { get; set; }

        public string organization_role { get; set; }
    }
}