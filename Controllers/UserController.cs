using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportClubSite.Models;
using System.Data.Entity;

namespace SportClubSite.Controllers
{
    public class UserController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("Login","User");
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            var lg = db.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
            if (lg != null)
            {

                string Nnn = u.UserName;
                Name g = new Name();
                g.Id = 1;

                g.Name1 = Nnn;
                 db.Entry(g).State = EntityState.Modified;
                db.SaveChanges();
               
                Session["UserName"] = u.UserName;
                Session.Timeout = 10;

                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                return RedirectToAction("Login","User");
            }
           
        }
        public ActionResult logoff()
        {
            Session.Remove("UserName");
            return RedirectToAction("Login", "User");
        }
        //
        // GET: /User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
