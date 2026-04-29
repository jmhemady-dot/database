using ADNU_CFRS.Models.ViewModels;
using ADNU_CFRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADNU_CFRS.Controllers
{
    public class SystemAdminController : Controller
    {
        // GET: SystemAdmin
        public ActionResult ManageUsers()
        {
            return View("~/Views/SystemAdmin/ManageUser/UserList.cshtml", new SystemAdminVM());
        }

        public ActionResult LoadUserList(int userType)
        {
            var users = SystemAdminHelper.GetUsers(userType);
            return PartialView("~/Views/SystemAdmin/ManageUser/_UserAccountTable.cshtml", new SystemAdminVM { users = users });
        }

        public ActionResult AddUser()
        {
            return View("~/Views/SystemAdmin/ManageUser/SelectType.cshtml");
        }

        public ActionResult SelectUserType(int user_type)
        {
            if (user_type == 2)
            {
                return View("~/Views/SystemAdmin/ManageUser/AddStudent.cshtml");
            }
            else if (user_type == 3)
            {
                return View("~/Views/SystemAdmin/ManageUser/AddEmployee.cshtml");
            }
            else
            {
                return View("~/Views/SystemAdmin/ManageUser/AddExternal.cshtml");
            }
        }

        public ActionResult EditUser(int person_id)
        {
            var user_account = SystemAdminHelper.GetUsers(0).Where(x => x.person.person_id == person_id).FirstOrDefault();

            if (user_account.person.person_type == 2)
            {
                var student = SystemAdminHelper.GetStudentDetail(person_id);
                return View("~/Views/SystemAdmin/ManageUser/EditStudent.cshtml", new SystemAdminVM { user_account = user_account, student = student });
            }
            else if (user_account.person.person_type == 3)
            {
                var employee = SystemAdminHelper.GetEmployeeDetail(person_id);
                return View("~/Views/SystemAdmin/ManageUser/EditEmployee.cshtml", new SystemAdminVM { user_account = user_account,  employee = employee });
            }
            else
            {
                var external = SystemAdminHelper.GetExternalPersonDetail(person_id);
                return View("~/Views/SystemAdmin/ManageUser/EditExternal.cshtml", new SystemAdminVM { user_account = user_account, external = external });
            }
        }

        public ActionResult DeleteUser(int person_id, int userType)
        {
            SystemAdminHelper.DeleteUserAccount(person_id);

            var users = SystemAdminHelper.GetUsers(userType);
            return PartialView("~/Views/SystemAdmin/ManageUser/_UserAccountTable.cshtml", new SystemAdminVM { users = users });
        }

        [HttpPost]
        public ActionResult SaveStudent(string firstname, string lastname, string contactNum, string email, string course, string dept, int year_level,
            string username, string password, int group_id)
        {
            //SAVE PERSON DETAIL
            int person_id = SystemAdminHelper.SavePersonDetail(firstname, lastname, contactNum, email, 2);

            //SAVE USER ACCOUNT
            SystemAdminHelper.SaveUserAccount(username, password, person_id, group_id);

            //SAVE STUDENT DETAIL
            SystemAdminHelper.SaveStudentDetail(person_id, course, dept, year_level);


            return ReturnSuccess("New user successfully created", "success", "Successfully Saved!");
        }

        [HttpPost]
        public ActionResult UpdateStudent(int person_id, string firstname, string lastname, string contactNum, string email, string course, string dept, int year_level,
            string username, int group_id)
        {
            //UPDATE PERSON DETAIL
            SystemAdminHelper.UpdatePersonDetail(person_id, firstname, lastname, contactNum, email);

            //UPDATE USER ACCOUNT
            SystemAdminHelper.UpdateUserAccount(username, person_id, group_id);

            //UPDATE STUDENT DETAIL
            SystemAdminHelper.UpdateStudentDetail(person_id, course, dept, year_level);

            return ReturnSuccess("Student details successfully updated", "success", "Successfully Saved!");
        }


        [HttpPost]
        public ActionResult SaveEmployee(string firstname, string lastname, string contactNum, string email,  string position, string dept, int employee_type,
            string username, string password, int group_id)
        {
            //SAVE PERSON DETAIL
            int person_id = SystemAdminHelper.SavePersonDetail(firstname, lastname, contactNum, email, 3);
            
            //SAVE USER ACCOUNT
            SystemAdminHelper.SaveUserAccount(username, password, person_id, group_id);

            //SAVE EMPLOYEE DETAIL
            SystemAdminHelper.SaveEmployeeDetail(position, dept, employee_type, person_id);

            return ReturnSuccess("New user successfully created", "success", "Successfully Saved!");
        }

        [HttpPost]
        public ActionResult UpdateEmployee(int person_id, string firstname, string lastname, string contactNum, string email, string position, string dept, int employee_type,
            string username, int group_id)
        {
            //UPDATE PERSON DETAIL
            SystemAdminHelper.UpdatePersonDetail(person_id, firstname, lastname, contactNum, email);

            //UPDATE USER ACCOUNT
            SystemAdminHelper.UpdateUserAccount(username, person_id, group_id);

            //UPDATE EMPLOYEE DETAIL
            SystemAdminHelper.UpdateEmployeeDetail(position, dept, employee_type, person_id);

            return ReturnSuccess("Employee details successfully updated", "success", "Successfully Saved!");
        }

        [HttpPost]
        public ActionResult SaveExternal(string firstname, string lastname, string contactNum, string email, string org_name, string org_role,
            string username, string password, int group_id)
        {
            //SAVE PERSON DETAIL
            int person_id = SystemAdminHelper.SavePersonDetail(firstname, lastname, contactNum, email, 4);

            //SAVE USER ACCOUNT
            SystemAdminHelper.SaveUserAccount(username, password, person_id, group_id);

            //SAVE EXTERNAL DETAIL
            SystemAdminHelper.SaveExternalPerson(org_name, org_role, person_id);

            return ReturnSuccess("New user successfully created", "success", "Successfully Saved!");
        }

        [HttpPost]
        public ActionResult UpdateExternal(int person_id, string firstname, string lastname, string contactNum, string email, string org_name, string org_role,
            string username, int group_id)
        {
            //UPDATE PERSON DETAIL
            SystemAdminHelper.UpdatePersonDetail(person_id, firstname, lastname, contactNum, email);

            //UPDATE USER ACCOUNT
            SystemAdminHelper.UpdateUserAccount(username, person_id, group_id);

            //UPDATE EXTERNAL PERSON DETAIL
            SystemAdminHelper.UpdateExternalPersonDetail(org_name, org_role, person_id);

            return ReturnSuccess("External Person person successfully updated", "success", "Successfully Saved!");
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