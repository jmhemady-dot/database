using ADNU_CFRS.HelperClass;
using ADNU_CFRS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Repository
{
    public class AccountHelper
    {
        public static bool IsAuthenticated
        {
            get
            {
                try
                {
                    return GetUserDetails != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static void SetUserIdentity(User_Account currUser)
        {
            HttpContext.Current.Session["currUser_appuser"] = currUser;
        }

        public static User_Account GetUserDetails
        {
            get
            {
                return HttpContext.Current.Session["currUser_appuser"] as User_Account;
            }
        }

        public static User_Account GetUserInfo(string username)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT 
                            ua.account_id, ua.username, ua.password, ua.login_status,

                            p.person_id, p.firstname, p.lastname, p.contact_number, p.email_address, p.person_type,

                            ug.group_id, ug.group_name,

                            ur.role_id, ur.role_name

                        FROM user_account ua
                            INNER JOIN person p ON ua.person_id = p.person_id
                            INNER JOIN user_group ug ON ua.group_id = ug.group_id
                            INNER JOIN user_role ur ON ug.role_id = ur.role_id
                        WHERE ua.username = @username;";

                var user = db.connection.Query<User_Account, Person, User_Group, User_Role, User_Account>(
                    sql,
                    (ua, p, ug, ur) =>
                    {
                        ua.person = p;
                        ug.role = ur;
                        ua.group = ug;
                        return ua;
                    },
                    new { username = username },
                    splitOn: "person_id,group_id,role_id",
                    commandType: System.Data.CommandType.Text
                ).FirstOrDefault();

                return user;
            }
            
        }

        public static void ClearSessions()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
        }
    }
}