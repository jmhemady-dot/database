using ADNU_CFRS.Models.ViewModels;
using ADNU_CFRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADNU_CFRS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ManageReservation()
        {
            var reservations = AdminHelper.GetReservations();
            return View("~/Views/Admin/ManageReservation/ReservationList.cshtml", new AdminVM { reservations = reservations });
        }

        public ActionResult ProcessReservation(int reservation_id, int status)
        {
            AdminHelper.UpdateReservationStatus(reservation_id, status);
            var reservations = AdminHelper.GetReservations();
            return PartialView("~/Views/Admin/ManageReservation/_ReservationTable.cshtml", new AdminVM { reservations = reservations });
        }
    }
}