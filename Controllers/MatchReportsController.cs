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
    public class MatchReportsController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /MatchReports/
        public ActionResult Index()
        {
            MatchReportRepository mr = new MatchReportRepository();
            var mymatchreports = mr.GetAllReports();
            if (mymatchreports.Count() > 0)
            {
                ViewData["Reports"] = mymatchreports.OrderByDescending(k => k.Match.Timestamp);
            }

            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {

            List<SelectListItem> matcheslist = new List<SelectListItem>();
            MatchesRepository matchrep = new MatchesRepository();
            var mymatches = matchrep.GetAllMatches().OrderByDescending(k => k.Timestamp);
            if (mymatches.Count() > 0)
            {

                foreach (Match m in mymatches)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = m.Match_id.ToString();
                    sli.Text = m.HomeTeam + "(" + m.HomeGoals + " - " + m.AwayGoals + ")" + m.AwayTeam;
                    matcheslist.Add(sli);
                }

            }
            ViewData["Matches"] = matcheslist;
 
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Add(MatchReport report)
        {
            List<SelectListItem> matcheslist = new List<SelectListItem>();
            MatchesRepository matchrep = new MatchesRepository();
            var mymatches = matchrep.GetAllMatches().OrderByDescending(k => k.Timestamp);
            if (mymatches.Count() > 0)
            {

                foreach (Match m in mymatches)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = m.Match_id.ToString();
                    sli.Text = m.HomeTeam + "(" + m.HomeGoals + " - " + m.AwayGoals + ")" + m.AwayTeam;
                    matcheslist.Add(sli);
                }

            }
            ViewData["Matches"] = matcheslist;

            if(!String .IsNullOrEmpty(report.Content))
            {
                MatchReportRepository mm = new MatchReportRepository();
                mm.Add(report);
                mm.SaveChanges();
                return Redirect("/MatchReports");
            }
            else
            {
                ModelState.AddModelError("Contents", " Make you shure enterd Match report to match do it");
            }
            return View(report);
        }
        public ActionResult view(int id)
        {
            MatchReportRepository m = new MatchReportRepository();
            var report = m.GetAllReports().Where(k => k.MatchReport_id == id).FirstOrDefault();

            if (report != null)
            {
                return View(report);
            }
            else
            {

                return Redirect("/");
            }
        }
        public ActionResult All()
        {
            MatchReportRepository mr = new MatchReportRepository();
            var mymatchreports = mr.GetAllReports();
            if (mymatchreports.Count() > 0)
            {
                ViewData["Reports"] = mymatchreports.OrderByDescending(k => k.Match.Timestamp).ThenByDescending(k => k.MatchReport_id);
            }

            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchReport m = db.MatchReports.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatchReport_id,Match_id,Content")] MatchReport m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(m);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            MatchReport mm = db.MatchReports.Find(id);
            if (mm == null)
            {
                return HttpNotFound();
            }
            return View(mm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MatchReport mmm = db.MatchReports.Find(id);
            db.MatchReports.Remove(mmm);
            db.SaveChanges();
            return Redirect("/MatchReports");
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