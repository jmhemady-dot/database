using ADNU_CFRS.Models.ViewModels;
using ADNU_CFRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADNU_CFRS.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult CreateReservation()
        {
            return View("~/Views/Student/CreateReservation.cshtml");
        }

        [HttpPost]
        public ActionResult SaveReservation(DateTime reservation_date, string purpose, string start_time, string end_time,
             int resource_id, int no_of_attendees)
        {
            var userInfo = AccountHelper.GetUserDetails;
            StudentHelper.SaveReservation(reservation_date, purpose, start_time, end_time, resource_id, no_of_attendees, userInfo.person.person_id);

            return ReturnSuccess("Reservation successfully saved", "success");
        }

        public ActionResult MyReservations()
        {
            var userInfo = AccountHelper.GetUserDetails;
            var reservations = AdminHelper.GetReservations().FindAll(x => x.person.person_id == userInfo.person.person_id);

            return View("~/Views/Student/MyReservation.cshtml", new StudentVM { reservations = reservations });
        }

        public JsonResult ReturnSuccess(string msg = "", string result = "", string title = "")
        {
            msg = msg == "" ? "your information has been updated..." : msg;
            result = result == "" ? "success" : result;
            title = title == "" ? "Successfully Saved!" : title;
            return Json(new { msg = msg, result = result, title = title });
        }

        public JsonResult ReturnError(string msg = "", string result = "", string title = "")
        {
            msg = msg == "" ? "please try again later..." : msg;
            result = result == "" ? "error" : result;
            title = title == "" ? "Error on Saving!" : title;
            return Json(new { msg = msg, result = result, title = title });
        }
    }
}