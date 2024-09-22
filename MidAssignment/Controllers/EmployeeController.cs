using MidAssignment.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidAssignment.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDatabaseEntitiesContext _dbContext;
        public EmployeeController()
        {
            _dbContext = new EmployeeDatabaseEntitiesContext();
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employeeList = _dbContext.Employees.ToList();
            return View(employeeList);
        }

        [HttpGet]
        public ActionResult AddEmployee(Employee e)
        {
            if (e.Id > 0)
            {
                return View(e);
            }
            else
            {
                ModelState.Clear();
                ViewBag.NoData = 0;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee e)
        {
            if (e.Id <= 0)
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    _dbContext.Employees.Add(e); // Save employee
                    _dbContext.SaveChanges();
                    TempData["MsgAdd"] = "Employee information added successfully";

                    return RedirectToAction("Index");
                }
            }
            else
            {
                _dbContext.Entry(e).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["MsgMod"] = "Employee information modified successfully";

                return RedirectToAction("Index");


            }

            ModelState.Clear();
            return View("AddEmployee");

        }

        public ActionResult DeleteEmployee(int id)
        {

            var delete_employee = _dbContext.Employees.Where(x => x.Id == id).First();
            _dbContext.Employees.Remove(delete_employee);
            _dbContext.SaveChanges();
            TempData["MsgRem"] = "Employee information removed successfully";

            return RedirectToAction("Index");
        }
    }
}