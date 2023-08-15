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
    public class LegaTableController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /LegaTable/
        public ActionResult Index()
        {
            Lega l = new Lega();
            var MyLega = l.GetAllTables();

            if (MyLega.Count > 0)
            {
                ViewData["MyLega"] = MyLega.OrderByDescending(k => k.CycleName);
            }

            return View();

        }
        
        public ActionResult AddNotice(int ?id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cycle1 c = db.Cycle1.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNotice([Bind(Include = "cause")] Cycle1 c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
                
                return RedirectToAction("index");
            }
            return View(c);
                


            
        }

	}
}