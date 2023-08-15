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
    public class TeamsController : Controller
    {

        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /Teams/
        public ActionResult Index()
        {
            TeamsRepository teamsrep = new TeamsRepository();
            var MyTeams = teamsrep.GetAllTeams();

            if (MyTeams.Count > 0)
            {
                ViewData["MyTeams"] = MyTeams.OrderBy(k => k.TeamName);
            }

            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Team team)
        {
            if (!string.IsNullOrWhiteSpace(team.TeamName))
            {

                TeamsRepository teamsrep = new TeamsRepository();

                if (teamsrep.GetAllTeams().Where(k => k.TeamName.ToLower() == team.TeamName.ToLower()).Count() > 0)
                {
                    ModelState.AddModelError("TeamName", "the team name is alredy in use");
                }
                else
                {
                    teamsrep.Add(team);
                    teamsrep.SaveChanges();
                    return Redirect("/teams");
                }
            }
            else
            {
                ModelState.AddModelError("TeamName", "Please Enter team Name");
            }

            return View(team);
        }
        public ActionResult All()
        {
            TeamsRepository t = new TeamsRepository();
            var myteams = t.GetAllTeams();
            if (myteams.Count > 0 )
            {
                ViewData["Teams"] = myteams.OrderBy(k => k.TeamName);
            }
            return View();
        }
        public ActionResult view(int id)
        {
            TeamsRepository t = new TeamsRepository();

            var Allteams = t.GetAllTeams().Where(k => k.Team_id != id).OrderBy(k => k.TeamName);


            
            
            if(Allteams.Count()>0)
            {
                ViewData["AllTeams"] = Allteams;
            }

            var team = t.GetAllTeams().Where(k => k.Team_id == id).FirstOrDefault();

            if (team != null)
            {
                MatchReportRepository m = new MatchReportRepository();
                var Allreports = m.GetAllReports().Where(k => k.Match.HomeTeam == id || k.Match.AwayTeam == id).OrderByDescending(k => k.Match.Timestamp);


                if (Request.QueryString["opponent"] != null)
                {
                    var opponent = Allteams.Where(k => k.Team_id.ToString() == Request.QueryString["opponent"].ToString()).FirstOrDefault();
                    if (opponent != null)
                    {
                        ViewData["opponentTeamName"] = opponent.TeamName;
                        Allreports = Allreports.Where(k => k.Match.HomeTeam == opponent.Team_id || k.Match.AwayTeam == opponent.Team_id).OrderByDescending(k => k.Match.Timestamp);
                    }
                }
           
                
                
                var Allhomegames = Allreports.Where(k => k.Match.HomeTeam == id);
                var Allawaygames = Allreports.Where(k => k.Match.AwayTeam == id);

                if (Allhomegames.Count() > 0)
                {
                    ViewData["Home Games"] = Allhomegames;
                }
                if (Allawaygames.Count()>0)
                {
                    ViewData["Away Games"] = Allawaygames;

                }
                return View(team);
            }
            else
            {

                return Redirect("/");
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Team t = db.Teams.Find(id);
            if (t == null)
            {
                return HttpNotFound();
            }
            return View(t);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team tt = db.Teams.Find(id);
            db.Teams.Remove(tt);
            db.SaveChanges();
            return Redirect("/teams");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team t = db.Teams.Find(id);
            if (t == null)
            {
                return HttpNotFound();
            }
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Team_id,TeamName,HomeTeamImage,AwayTeamImage")] Team t)
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