using ADNU_CFRS.Models;
using ADNU_CFRS.Models.ViewModels;
using ADNU_CFRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADNU_CFRS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml", new LoginVM());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UserAuth(LoginVM loginInfo)
        {
            if (ModelState.IsValid)
            {
                User_Account user = AccountHelper.GetUserInfo(loginInfo.username);

                if (user != null)
                {
                    if (user.password.Equals(loginInfo.password))
                    {
                        if (user.login_status != 0)
                        {
                            loginInfo.status = EnumUserStatus.Active;

                            AccountHelper.SetUserIdentity(user);
                            return RedirectToAction("Index", "Account");
                        }
                        else
                        {
                            loginInfo.status = EnumUserStatus.Disabled;
                            loginInfo.message = "Error: Account disabled";
                        }

                    }
                    else
                    {
                        loginInfo.status = EnumUserStatus.InvalidUser;
                        loginInfo.message = "Error: Invalid account password";
                    }
                }
                else
                {
                    loginInfo.status = EnumUserStatus.InvalidUser;
                    loginInfo.message = "Error: Invalid login details.";
                }
            }

            return View("~/Views/Account/Login.cshtml", loginInfo);
        }

        public ActionResult Index()
        {
            if (AccountHelper.IsAuthenticated)
                return View("~/Views/Account/Index.cshtml");
            else
                return RedirectToAction(nameof(Logout));
        }

        public ActionResult Logout()
        {
            AccountHelper.ClearSessions();
            return RedirectToAction("Login", "Account");
        }
    }
}