using ADNU_CFRS.HelperClass;
using ADNU_CFRS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Repository
{
    public static class AdminHelper
    {
        public static List<Reservation> GetReservations()
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT 
                                r.reservation_id, r.reservation_date, r.purpose, r.start_time, r.end_time, r.no_of_attendees, r.reservation_status,
                                p.person_id, p.firstname, p.lastname,
                                res.resource_id, res.resource_name
                            FROM reservation r
                                INNER JOIN person p on r.person_id = p.person_id
                                INNER JOIN resource res on r.resource_id = res.resource_id
                            ORDER BY r.reservation_date DESC;";
                var reservations = db.connection.Query<Reservation, Person, Resource, Reservation>
                    (sql,
                     (R, P, Res) =>
                     {
                         R.person = P;
                         R.resource = Res;
                         return R;
                     },
                     splitOn: "person_id,resource_id",
                     commandType: System.Data.CommandType.Text
                    ).ToList();
                return reservations;
            }
        }

        public static void UpdateReservationStatus(int reservation_id, int reservation_status)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE reservation SET reservation_status = @reservation_status WHERE reservation_id = @reservation_id;";
                db.connection.Execute(sql, new { reservation_id, reservation_status }, commandType: System.Data.CommandType.Text);
            }
        }
    }
}