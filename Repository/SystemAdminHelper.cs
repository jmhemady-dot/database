using ADNU_CFRS.HelperClass;
using ADNU_CFRS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADNU_CFRS.Repository
{
    public class SystemAdminHelper
    {
        public static List<User_Account> GetUsers(int userType)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"
                            SELECT 
                                ua.account_id, ua.username, ua.password, ua.login_status,
                                p.person_id, p.firstname, p.lastname, p.contact_number, p.email_address, p.person_type,
                                ug.group_id, ug.group_name,
                                ur.role_id, ur.role_name
                            FROM user_account ua
                                INNER JOIN person p ON ua.person_id = p.person_id
                                INNER JOIN user_group ug ON ua.group_id = ug.group_id
                                INNER JOIN user_role ur ON ug.role_id = ur.role_id
                            WHERE p.person_type <> 1
                                AND (@person_type = 0 OR p.person_type = @person_type)
                            ORDER BY p.lastname;";

                var users = db.connection.Query<User_Account, Person, User_Group, User_Role, User_Account>(
                    sql,
                    (ua, p, ug, ur) =>
                    {
                        ua.person = p;
                        ug.role = ur;
                        ua.group = ug;
                        return ua;
                    },
                    new { person_type = userType },
                    splitOn: "person_id,group_id,role_id",
                    commandType: System.Data.CommandType.Text
                ).ToList();

                return users;
            }
        }

        public static Student GetStudentDetail(int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT course, dept, year_level FROM student WHERE person_id = @person_id;";
                var student = db.connection.QueryFirstOrDefault<Student>(sql, new { person_id });
                return student;
            }
        }

        public static Employee GetEmployeeDetail(int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT position, dept, employee_type FROM employee WHERE person_id = @person_id;";
                var employee = db.connection.QueryFirstOrDefault<Employee>(sql, new { person_id });
                return employee;
            }
        }

        public static External GetExternalPersonDetail(int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT organization_name, organization_role FROM external WHERE person_id = @person_id;";
                var external = db.connection.QueryFirstOrDefault<External>(sql, new { person_id });
                return external;
            }
        }

        public static int SavePersonDetail(string firstname, string lastname, string contactNum, string email, int person_type)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO person (firstname, lastname, contact_number, email_address, person_type) 
                            VALUES (@firstname, @lastname, @contactNum, @email, @person_type);
                            SELECT LAST_INSERT_ID();";
                var person_id = db.connection.ExecuteScalar<int>(sql, new { firstname, lastname, contactNum, email, person_type });
                return person_id;
            }
        }

        public static void UpdatePersonDetail(int person_id, string firstname, string lastname, string contactNum, string email)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE person 
                            SET firstname = @firstname, lastname = @lastname, contact_number = @contactNum, email_address = @email 
                            WHERE person_id = @person_id;";
                db.connection.Execute(sql, new { person_id, firstname, lastname, contactNum, email });
            }
        }

        public static void SaveUserAccount(string username, string password, int person_id, int group_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO user_account (username, password, login_status, person_id, group_id) 
                            VALUES (@username, @password, 1, @person_id, @group_id);";
                db.connection.Execute(sql, new { username, password, person_id, group_id });
            }
        }

        public static void UpdateUserAccount(string username, int person_id, int group_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE user_account 
                            SET username = @username, group_id = @group_id 
                            WHERE person_id = @person_id;";
                db.connection.Execute(sql, new { username, person_id, group_id });
            }
        }

        public static void SaveStudentDetail(int person_id, string course, string dept, int year_level)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO student (course, dept, year_level, person_id) 
                            VALUES (@course, @dept, @year_level, @person_id);";
                db.connection.Execute(sql, new { course, dept, year_level, person_id });
            }
        }

        public static void UpdateStudentDetail(int person_id, string course, string dept, int year_level)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE student 
                            SET course = @course, dept = @dept, year_level = @year_level 
                            WHERE person_id = @person_id;";
                db.connection.Execute(sql, new { course, dept, year_level, person_id });
            }
        }

        public static void SaveEmployeeDetail(string position, string dept, int employee_type, int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO employee (position, dept, employee_type, person_id) 
                            VALUES (@position, @dept, @employee_type, @person_id);";
                db.connection.Execute(sql, new { position, dept, employee_type, person_id });
            }
        }

        public static void UpdateEmployeeDetail(string position, string dept, int employee_type, int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE employee
                            SET position = @position, dept = @dept, employee_type = @employee_type
                            WHERE person_id = @person_id;";
                db.connection.Execute(sql, new { position, dept, employee_type, person_id });
            }
        }

        public static void SaveExternalPerson(string org_name, string org_role, int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO external (organization_name, organization_role, person_id) 
                            VALUES (@org_name, @org_role, @person_id);";
                db.connection.Execute(sql, new { org_name, org_role, person_id });
            }
        }

        public static void UpdateExternalPersonDetail(string org_name, string org_role, int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE external
                            SET organization_name = @org_name, organization_role = @org_role
                            WHERE person_id = @person_id;";
                db.connection.Execute(sql, new { org_name, org_role, person_id });
            }
        }

        public static void DeleteUserAccount(int person_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"DELETE FROM user_account WHERE person_id = @person_id;
                            DELETE FROM student WHERE person_id = @person_id;
                            DELETE FROM employee WHERE person_id = @person_id;
                            DELETE FROM external WHERE person_id = @person_id;
                            DELETE FROM person WHERE person_id = @person_id;";
                db.connection.Execute(sql, new { person_id });
            }
        }
    }
}