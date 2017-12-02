using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoCrud1.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;

namespace DemoCrud1.Controllers
{
    public class EmployeeController : Controller

    {
        private EmployeesContext dbEmployee = new EmployeesContext();
        // GET: Employee
        public ActionResult Index(string search, int? page)
        {
            return View(dbEmployee.Employees.Where(e =>e.FirstName.StartsWith(search) || search==null).ToList().ToPagedList(page?? 1,5));
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View(dbEmployee.Employees.Where(e=>e.Id==id).SingleOrDefault());
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                dbEmployee.Employees.Add(employee);
                dbEmployee.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dbEmployee.Employees.Where(e=>e.Id==id).SingleOrDefault());
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                dbEmployee.Entry(employee).State = EntityState.Modified;
                dbEmployee.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        //public ActionResult Delete(int id)
        // {
        // return View(dbEmployee.Employees.Where(e => e.Id == id).SingleOrDefault());
        //}

        // POST: Employee/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        // {
        //  try
        // {

        //   return RedirectToAction("Index");
        // }
        // catch
        // {
        // return View();
        // }
        //}

        public ActionResult Delete(int id)
        {
            Employee employee = dbEmployee.Employees.Where(e => e.Id == id).SingleOrDefault();
            dbEmployee.Employees.Remove(employee);
            dbEmployee.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
