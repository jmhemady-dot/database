using ADNU_CFRS.HelperClass;
using ADNU_CFRS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.Repository
{
    public static class LookUpHelper
    {
        public static List<User_Group> GetUserGroups()
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT 
                                ug.group_id, ug.group_name,

                                ug.role_id, ur.role_name
                            FROM user_group ug
                                INNER JOIN user_role ur on ug.role_id = ur.role_id
                            WHERE ug.group_id <> 1;";
                var groups = db.connection.Query<User_Group, User_Role, User_Group>
                    (sql,
                     (UG, UR) =>
                     {
                         UG.role = UR;
                         return UG;
                     },
                     splitOn: "role_id",
                     commandType: System.Data.CommandType.Text
                    ).ToList();
                return groups;
            }
        }

        public static int GetEmployeeType(int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT employee_type FROM employee WHERE person_id = @person_id;";
                var employee_type = db.connection.QueryFirstOrDefault<int>(sql, new { person_id });
                return employee_type;
            }

        }

        public static List<Resource> GetResources()
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT resource_id, resource_name FROM resource;";
                var resources = db.connection.Query<Resource>(sql).ToList();
                return resources;
            }
        }
    }
}