using LCTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace LCTest.Controllers
{
    public class HomeController : Controller
    { 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employee = new List<Employee> { new Employee() };
            IEnumerable<Employee> EmployeeList = GetEmployees(employee, "Hourly");
            ViewData["HourlyEmployee"] = EmployeeList;

            EmployeeList = new List<SalariedEmployee>();
            EmployeeList = GetEmployees(employee, "Salaried");
            ViewData["SalariedEmployee"] = EmployeeList;

            EmployeeList = new List<Manager>();
            EmployeeList = GetEmployees(employee, "Manager");
            ViewData["Manager"] = EmployeeList;
            return View(ViewData);

        }
        private IEnumerable<Employee> GetEmployees(IEnumerable<Employee> EmpList, string EmpType)
        {
            switch (EmpType)
            {
                 case "Hourly":
                    List<HourlyEmployee> EmployeeList = new List<HourlyEmployee>();
                    for (int i = 1; i < 11; i++)
                    {
                        //EmployeeList=new List<Employee>(){
                        HourlyEmployee employeeHourly = new HourlyEmployee
                        {
                            ID = i,
                            FirstName = "Hourly Employee",
                            LastName = i.ToString()
                        };
                        EmployeeList.Add(employeeHourly);
                    }
                    EmpList=EmployeeList;
                    break;
                case "Salaried":
                    List<SalariedEmployee> EmployeeList1 = new List<SalariedEmployee>();
                    for (int i = 101; i < 111; i++)
                    {
                        // EmployeeList=new List<Employee>(){
                        SalariedEmployee employeeHourly = new SalariedEmployee
                        {
                            ID = i,
                            FirstName = "Salaried Employee",
                            LastName = i.ToString()
                        };
                        EmployeeList1.Add(employeeHourly);
                    }
                    EmpList = EmployeeList1;
                    break;
                case "Manager":
                    List<Manager> EmployeeList2 = new List<Manager>();
                    for (int i = 101; i < 111; i++)
                    {
                        // EmployeeList=new List<Employee>(){
                        Manager employeeHourly = new Manager
                        {
                            ID = i,
                            FirstName = "Manager",
                            LastName = i.ToString()
                        };
                        EmployeeList2.Add(employeeHourly);
                    }
                    EmpList = EmployeeList2;
                    break;
            }
            return EmpList;
        }
   

        [HttpPost]
        public JsonResult AddWork(string empJson)
        {
            Employee empSaved = new Employee();
            try { 
                var empData = JsonObject.Parse(empJson);
                if (empData is not null)
                {
                    if (empData[0] is not null)
                    {
                        Employee empUpdate = new Employee
                        {
                            ID = empData[0]["ID"] is not null ? Int32.Parse(empData[0]["ID"].ToString()) : 0,
                            NoOfVacationDays = empData[0]["NoOfVacationDays"] is not null ? Int32.Parse(empData[0]["NoOfVacationDays"].ToString()) : 0,
                            NoOfWorkDays = empData[0]["NoOfWorkDays"] is not null ? Int32.Parse(empData[0]["NoOfWorkDays"].ToString()) : 0,
                            MaxVacationDays = empData[0]["MaxVacationDays"] is not null ? Int32.Parse(empData[0]["MaxVacationDays"].ToString()) : 0
                        };
                        Employee objEmployee = new Employee();
                        switch (Int32.Parse(empData[0]["EmpType"].ToString()))
                        {
                            case 0:
                                objEmployee = new HourlyEmployee();
                                break;
                            case 1:
                                objEmployee = new SalariedEmployee();
                                break;
                            case 2:
                                objEmployee = new Manager();
                                break;
                        }
                        empSaved = objEmployee.AddDaysWork(empUpdate, Single.Parse(empData[0]["NewDays"].ToString()));
                    }
                }
            }
            catch (Exception ex) { }
            return Json(empSaved);
        }

        [HttpPost]
        public JsonResult AddVacation(string empJson)
        {
            Employee empSaved = new Employee();
            try
            {
                var empData = JsonObject.Parse(empJson);
                if (empData is not null)
                {
                    if (empData[0] is not null)
                    {
                        Employee empUpdate = new Employee
                        {
                            ID = empData[0]["ID"] is not null ? Int32.Parse(empData[0]["ID"].ToString()): 0,
                            NoOfVacationDays = empData[0]["NoOfVacationDays"] is not null ? Int32.Parse(empData[0]["NoOfVacationDays"].ToString()) : 0,
                            NoOfWorkDays = empData[0]["NoOfWorkDays"] is not null ? Int32.Parse(empData[0]["NoOfWorkDays"].ToString()) : 0,
                            NoOfVacationDaysAvailed = empData[0]["NoOfVacationDaysAvailed"] is not null ? Int32.Parse(empData[0]["NoOfVacationDaysAvailed"].ToString()) : 0,
                            MaxVacationDays = empData[0]["MaxVacationDays"] is not null ? Int32.Parse(empData[0]["MaxVacationDays"].ToString()) : 0
                        };
                        Employee objEmployee = new Employee();
                        switch (Int32.Parse(empData[0]["EmpType"].ToString()))
                        {
                            case 0:
                                objEmployee = new HourlyEmployee();
                                break;
                            case 1:
                                objEmployee = new SalariedEmployee();
                                break;
                            case 2:
                                objEmployee = new Manager();
                                break;
                        }
                        empSaved = objEmployee.AddVacationTaken(empUpdate, Single.Parse(empData[0]["NewDays"].ToString()));
                    }
                }
            }
            catch (Exception ex) { }
            return Json(empSaved);
        }
       
    }
}
