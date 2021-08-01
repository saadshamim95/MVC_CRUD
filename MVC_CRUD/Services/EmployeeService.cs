using Microsoft.Extensions.Configuration;
using MVC_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MVC_CRUD.Services
{
    public class EmployeeService
    {
        private readonly IConfiguration _configuration;

        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DBConnection"]))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetEmployees", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee()
                            {
                                EmpId = Convert.ToInt32(reader["empid"]),
                                Name = Convert.ToString(reader["name"]),
                                EmailId = Convert.ToString(reader["email"]),
                                Mobile = Convert.ToString(reader["mobile"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            
            return employees;
        }

        public void AddEmployee(Employee model)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DBConnection"]))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("AddEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Email", model.EmailId);
                    command.Parameters.AddWithValue("@Mobile", model.Mobile);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DBConnection"]))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetEmployeeById", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpId", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employee.EmpId = Convert.ToInt32(reader["empid"]);
                            employee.Name = Convert.ToString(reader["name"]);
                            employee.EmailId = Convert.ToString(reader["email"]);
                            employee.Mobile = Convert.ToString(reader["mobile"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return employee;
        }

        public void UpdateEmployee(Employee model)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DBConnection"]))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpId", model.EmpId);
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Email", model.EmailId);
                    command.Parameters.AddWithValue("@Mobile", model.Mobile);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DBConnection"]))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpId", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
