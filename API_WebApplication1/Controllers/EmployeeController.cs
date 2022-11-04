﻿using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using API_WebApplication1.model;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo_Core_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        SqlConnection con;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("DefaultParkingConnection"));

        }

        [HttpGet]
        public JsonResult GetAllEmployee()
        {
            List<Employeecs> employees = new List<Employeecs>();
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Employeee";
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    employees.Add(new Employeecs
                    {
                        empid = Convert.ToInt32(sdr["empid"]),
                        empname = Convert.ToString(sdr["empname"]),
                        EmailID = Convert.ToString(sdr["EmailID"])
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return new JsonResult(employees);

        }
        //[HttpGet]
        //public JsonResult GetEmployee(int id)
        //{
        //    List<Employee> employees = new List<Employee>();
        //    try
        //    {
        //        SqlCommand cmd = con.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT * FROM tbl_Emp Where(ID="+id+");";
        //        con.Open();
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            employees.Add(new Employee
        //            {
        //                Id = Convert.ToInt32(sdr["Id"]),
        //                Name = Convert.ToString(sdr["Name"]),
        //                Age = Convert.ToInt32(sdr["Age"])
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return new JsonResult(employees);

        //}
        [HttpPost]
        public Boolean AddEmployee(Employeecs employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Employeee(empid,empname,EmailID) values(" + employee.empid +",'" + employee.empname + "','" + employee.EmailID + "')";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpPut]
        public Boolean UpdateEmployee(int id, Employeecs employee)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "Update Employeee Set Name='" + employee.empname + "', Email= '" + employee.EmailID +"'  "Where(ID=" + id + ");";

                 cmd.CommandText = "Update Employeee Set empname='" + employee.empname + "',EmailID= '" + employee.EmailID + "' Where(empid= " + id + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpDelete]
        public Boolean DeleteEmployee(int id)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from Employeee Where(empid=" + id + ");";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

