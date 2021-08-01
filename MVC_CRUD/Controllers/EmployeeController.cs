using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.Models;
using MVC_CRUD.Services;
using System.Collections.Generic;

namespace MVC_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult GetEmployees()
        {
            List<Employee> employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _employeeService.AddEmployee(employee);

            return RedirectToAction("GetEmployees");
        }

        public IActionResult UpdateEmployee(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            _employeeService.UpdateEmployee(employee);
            return RedirectToAction("GetEmployees");
        }

        public IActionResult DeleteEmployee(int id)
        {
            _employeeService.DeleteEmployee(id);
            return RedirectToAction("GetEmployees");
        }
    }
}
