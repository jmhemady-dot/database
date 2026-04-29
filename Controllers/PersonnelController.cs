using ADNU_CFRS.Models.ViewModels;
using ADNU_CFRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADNU_CFRS.Controllers
{
    public class PersonnelController : Controller
    {
        // GET: Personnel
        public ActionResult ManageResources()
        {
            return View("~/Views/Personnel/ManageResource/ResourceList.cshtml", new PersonnelVM());
        }

        public ActionResult LoadResourceList(int resource_type)
        {
            var resources = PersonnelHelper.GetResources(resource_type);

            return PartialView("~/Views/Personnel/ManageResource/_ResourcesTable.cshtml", new PersonnelVM { resources = resources });
        }

        public ActionResult AddResource()
        {
            return View("~/Views/Personnel/ManageResource/SelectType.cshtml");
        }

        public ActionResult SelectResourceType(int resource_type)
        {
            if (resource_type == 1)
            {
                return View("~/Views/Personnel/ManageResource/AddFacility.cshtml");
            }
            else
            {
                return View("~/Views/Personnel/ManageResource/AddVehicle.cshtml");
            }
        }

        public ActionResult EditResource(int resource_id)
        {
            var resource = PersonnelHelper.GetResources(0).Where(x => x.resource_id == resource_id).FirstOrDefault();

            if (resource.resource_type == 1)
            {
                var facility = PersonnelHelper.GetFacility(resource_id);
                return View("~/Views/Personnel/ManageResource/EditFacility.cshtml", new PersonnelVM { resource = resource, facility = facility });
            }
            else
            {
                var vehicle = PersonnelHelper.GetVehicle(resource_id);
                return View("~/Views/Personnel/ManageResource/EditVehicle.cshtml", new PersonnelVM { resource = resource, vehicle = vehicle });
            }
        }

        public ActionResult DeleteResource(int resource_id, int resource_type)
        {
            PersonnelHelper.DeleteResource(resource_id);

            var resources = PersonnelHelper.GetResources(resource_type);

            return PartialView("~/Views/Personnel/ManageResource/_ResourcesTable.cshtml", new PersonnelVM { resources = resources });
        }

        [HttpPost]
        public ActionResult SaveFacility(string resource_name, string resource_description, string resource_condition, int resource_type,
            int availability_status, string location, string building, int floor_lvl, int capacity)
        {
            //SAVE RESOURCE DETAIL
            int resource_id = PersonnelHelper.SaveResource(resource_name, resource_description, resource_condition, resource_type, availability_status);

            //SAVE FACILITY DETAIL
            PersonnelHelper.SaveFacility(location, building, floor_lvl, capacity, resource_id);

            return ReturnSuccess("New Facility successfully saved", "success");
        }

        [HttpPost]
        public ActionResult UpdateFacility(int resource_id, string resource_name, string resource_description, string resource_condition, int resource_type,
            int availability_status, string location, string building, int floor_lvl, int capacity)
        {
            //UPDATE RESOURCE DETAIL
            PersonnelHelper.UpdateResource(resource_id, resource_name, resource_description, resource_condition, resource_type, availability_status);

            //UPDATE FACILITY DETAIL
            PersonnelHelper.UpdateFacility(location, building, floor_lvl, capacity, resource_id);

            return ReturnSuccess("Facility successfully updated", "success");
        }

        [HttpPost]
        public ActionResult SaveVehicle(string resource_name, string resource_description, string resource_condition, int resource_type,
            int availability_status, string plate_number, int capacity)
        {
            //SAVE RESOURCE DETAIL
            int resource_id = PersonnelHelper.SaveResource(resource_name, resource_description, resource_condition, resource_type, availability_status);

            //SAVE VEHICLE DETAIL
            PersonnelHelper.SaveVehicle(plate_number, capacity, resource_id);

            return ReturnSuccess("New Vehicle successfully saved", "success");
        }

        [HttpPost]
        public ActionResult UpdateVehicle(int resource_id, string resource_name, string resource_description, string resource_condition, int resource_type,
            int availability_status, string plate_number, int capacity)
        {
            //UDPATE RESOURCE DETAIL
            PersonnelHelper.UpdateResource(resource_id, resource_name, resource_description, resource_condition, resource_type, availability_status);

            //UPDATE VEHICLE DETAIL
            PersonnelHelper.UpdateVehicle(plate_number, capacity, resource_id);

            return ReturnSuccess("Vehicle successfully updated", "success");
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