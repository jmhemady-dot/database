using ADNU_CFRS.HelperClass;
using ADNU_CFRS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ADNU_CFRS.Repository
{
    public class PersonnelHelper
    {
        public static List<Resource> GetResources(int resource_type)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT * FROM resource WHERE (@resource_type = 0 OR resource_type = @resource_type);";
                var resources = db.connection.Query<Resource>(sql, new { resource_type }, commandType: System.Data.CommandType.Text).ToList();
                return resources;
            }
        }

        public static Facility GetFacility(int resource_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT * FROM facility WHERE resource_id = @resource_id;";
                var facility = db.connection.QueryFirstOrDefault<Facility>(sql, new { resource_id }, commandType: System.Data.CommandType.Text);
                return facility;
            }
        }

        public static Vehicle GetVehicle(int resource_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"SELECT * FROM vehicle WHERE resource_id = @resource_id;";
                var vehicle = db.connection.QueryFirstOrDefault<Vehicle>(sql, new { resource_id }, commandType: System.Data.CommandType.Text);
                return vehicle;
            }
        }

        public static int SaveResource(string resource_name, string resource_description, string resource_condition, int resource_type, int availability_status)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO resource (resource_name, resource_description, resource_condition, resource_type, availability_status) 
                            VALUES (@resource_name, @resource_description, @resource_condition, @resource_type, @availability_status);
                            SELECT LAST_INSERT_ID();";
                var resource_id = db.connection.ExecuteScalar<int>(sql, new { resource_name, resource_description, resource_condition, resource_type, availability_status });
                return resource_id;
            }
        }

        public static void UpdateResource(int resource_id, string resource_name, string resource_description, string resource_condition, int resource_type, int availability_status)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE resource SET resource_name = @resource_name, resource_description = @resource_description, 
                            resource_condition = @resource_condition, resource_type = @resource_type, availability_status = @availability_status 
                            WHERE resource_id = @resource_id;";
                db.connection.Execute(sql, new { resource_id, resource_name, resource_description, resource_condition, resource_type, availability_status });
            }
        }

        public static void SaveFacility(string location, string building, int floor_lvl, int capacity, int resource_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO facility (location, building, floor_lvl, capacity, resource_id) 
                            VALUES (@location, @building, @floor_lvl, @capacity, @resource_id);";
                db.connection.Execute(sql, new { location, building, floor_lvl, capacity, resource_id });
            }
        }

        public static void UpdateFacility(string location, string building, int floor_level, int capacity, int resource_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE facility SET location = @location, building = @building, floor_lvl = @floor_level, capacity = @capacity 
                            WHERE resource_id = @resource_id;";
                db.connection.Execute(sql, new { location, building, floor_level, capacity, resource_id });
            }
        }

        public static void SaveVehicle(string plate_number, int capacity, int resource_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"INSERT INTO vehicle (plate_number, capacity, resource_id) 
                            VALUES (@plate_number, @capacity, @resource_id);";
                db.connection.Execute(sql, new { plate_number, capacity, resource_id });
            }
        }

        public static void UpdateVehicle(string plate_number, int capacity, int resource_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"UPDATE vehicle SET plate_number = @plate_number, capacity = @capacity
                            WHERE resource_id = resource_id;";
                db.connection.Execute(sql, new {plate_number, capacity, resource_id });
            }
        }

        public static void DeleteResource(int resource_id)
        {
            using (DBHelper db = new DBHelper())
            {
                var sql = @"DELETE FROM facility WHERE resource_id = @resource_id;
                            DELETE FROM vehicle WHERE resource_id = @resource_id;
                            DELETE FROM resource WHERE resource_id = @resource_id;";
                db.connection.Execute(sql, new { resource_id });
            }
        }
    }
}