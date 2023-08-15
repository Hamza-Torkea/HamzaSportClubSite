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
    public class StudiumController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();

        //
        // GET: /Studium/
        public ActionResult Index()
        {
            StudiumRepository ss = new StudiumRepository();
            var AllStudium = ss.GetAllStudium();
            if (AllStudium.Count > 0)
            {
                ViewData["All Studium"] = AllStudium.OrderBy(k => k.Studium_name);
            }
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Studium studium)
        {
            if (!string.IsNullOrWhiteSpace(studium.Studium_name))
            {
                StudiumRepository ss = new StudiumRepository();


                if (ss.GetAllStudium().Where(k => k.Studium_name.ToLower() == studium.Studium_name.ToLower()).Count() > 0)
                {
                    ModelState.AddModelError("StudiumName", "the studium name is alredy in use");
                }
                else
                {
                    ss.Add(studium);
                    ss.SaveChanges();
                    return Redirect("/Studium");
                }
            }
            else
            {
                ModelState.AddModelError("studiumName", "Please Enter studium Name");
            }



            return View(studium);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studium s = db.Studiums.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Studium_id,Team_id,Studium_name,capacity")] Studium s)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(s);
        }


    }
}