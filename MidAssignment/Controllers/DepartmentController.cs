using MidAssignment.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidAssignment.Controllers
{
    public class DepartmentController : Controller
    {
        EmployeeDatabaseEntitiesContext _dbContext;
        public DepartmentController()
        {
            _dbContext = new EmployeeDatabaseEntitiesContext();
        }

        // GET: Department
        public ActionResult Index()
        {
            var departmentList = _dbContext.Departments.ToList();
            return View(departmentList);
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
                ModelState.Clear();
                return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Department d) // Action method overloading
        {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    _dbContext.Departments.Add(d); // Save employee
                    _dbContext.SaveChanges();
                    TempData["MsgAdd"] = "Department information added successfully";

                    return RedirectToAction("Index");
                }

            ModelState.Clear();
            return View("AddDepartment");

        }
    }
}