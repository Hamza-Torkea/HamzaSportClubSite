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
    public class TeamMangmentController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /TeamMangment/
        public ActionResult Index()
        {
            TeamMangmentRepository teamrep = new TeamMangmentRepository();
            var AllTeamMangment = teamrep.GetAllTeamMangment();
            if (AllTeamMangment.Count > 0)
            {
                ViewData["All Team Mangment"] = AllTeamMangment.OrderBy(k => k.Owner);
            }
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(TeamMangment team)
        {
            if (!string.IsNullOrWhiteSpace(team.Coatch))
            {

                TeamMangmentRepository tt = new TeamMangmentRepository();

                if (tt.GetAllTeamMangment().Where(k => k.Coatch.ToLower() == team.Coatch.ToLower()).Count() > 0)
                {
                    ModelState.AddModelError("Coatch", "the coatch name is alredy in use");
                }
                else
                {
                    tt.Add(team);
                    tt.SaveChanges();
                    return Redirect("/TeamMangment");
                }
            }
            else
            {
                ModelState.AddModelError("CoatchName", "Please Enter Coatch Name");
            }



            return View(team);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMangment t = db.TeamMangments.Find(id);
            if (t == null)
            {
                return HttpNotFound();
            }
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamMangment_id,Team_id,Owner,Coatch")] TeamMangment t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(t);
        }


    }
}