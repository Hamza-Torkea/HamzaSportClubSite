using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;



using SportClubSite.Models;


namespace SportClubSite.Controllers
{
    public class EmployeeController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            EmployeeRepository e = new EmployeeRepository();
            var AllEmployee = e.GetAllEmployees();
            if (AllEmployee.Count > 0)
            {
                ViewData["All Employee"] = AllEmployee.OrderBy(k => k.EmployeeName);
            }
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Employee employee)
        {
            if (!string.IsNullOrWhiteSpace(employee.EmployeeName))
            {
                EmployeeRepository ee = new EmployeeRepository();

                if (ee.GetAllEmployees().Where(k => k.EmployeeName.ToLower() == employee.EmployeeName.ToLower()).Count() > 0)
                {
                    ModelState.AddModelError("EmployeeName", "the employee name is alredy in use");
                }
                else
                {
                    ee.Add(employee);
                    ee.SaveChanges();
                    return Redirect("/Employee");
                }
            }
            else
            {
                ModelState.AddModelError("EmployeeName", "Please Enter Employee Name");
            }



            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee e = db.Employees.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee_id,EmployeeName,Email,Adress")] Employee e)
        {
            if (ModelState.IsValid)
            {
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(e);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Employee e = db.Employees.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee ee = db.Employees.Find(id);
            db.Employees.Remove(ee);
            db.SaveChanges();
            return Redirect("/Employee");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}