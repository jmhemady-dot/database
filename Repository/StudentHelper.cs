using ADNU_CFRS.HelperClass;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;


namespace ADNU_CFRS.Repository
{
    public static class StudentHelper
    {
        public static void SaveReservation(DateTime reservation_date, string purpose, string start_time, string end_time, int resource_id, int no_of_attendees, int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO reservation (reservation_date, purpose, no_of_attendees, start_time, end_time, reservation_status, person_id, resource_id) 
                            VALUES (@reservation_date, @purpose, @no_of_attendees, @start_time, @end_time, '0', @person_id, @resource_id);";
                db.connection.Execute(sql, new { reservation_date, purpose, start_time, end_time, resource_id, no_of_attendees, person_id });
            }
        }
    }
}