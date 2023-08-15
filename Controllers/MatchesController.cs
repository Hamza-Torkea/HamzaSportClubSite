using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportClubSite.Models;


namespace SportClubSite.Controllers
{
    public class MatchesController : Controller
    {
        //
        // GET: /Matches/
        public ActionResult Index()
        {
           MatchesRepository  Matchrep = new MatchesRepository();
            var MyMatches = Matchrep.GetAllMatches();

            if (MyMatches.Count > 0)
            {
                ViewData["MyMatches"] = MyMatches.OrderByDescending(k => k.Timestamp);
            }

            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            List<SelectListItem> teamlist = new List<SelectListItem>();
            TeamsRepository teamrep = new TeamsRepository();
            var MyTeams = teamrep.GetAllTeams().OrderBy(k => k.TeamName);
            if (MyTeams.Count()>0)
            {
                
                foreach(Team t in MyTeams)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = t.Team_id.ToString();
                    sli.Text = t.TeamName;
                    teamlist.Add(sli);
                }
                
            }
            ViewData["MyTeams"] = teamlist;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Match match)
        {
            List<SelectListItem> teamlist = new List<SelectListItem>();
            TeamsRepository teamrep = new TeamsRepository();
            var MyTeams = teamrep.GetAllTeams().OrderBy(k => k.TeamName);
            if (MyTeams.Count() > 0)
            {

                foreach (Team t in MyTeams)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Value = t.Team_id.ToString();
                    sli.Text = t.TeamName;
                    teamlist.Add(sli);
                }

            }
            ViewData["MyTeams"] = teamlist;
            if (match.HomeGoals != null)
            {
                if (match.AwayGoals != null)
                {
                    if (match.Timestamp != null)
                    {
                        if (match.HomeTeam != match.AwayTeam)
                        {
                            MatchesRepository matchrep = new MatchesRepository();
                            matchrep.Add(match);
                            matchrep.SaveChanges();
                            return Redirect("/Matches");
                        }
                        else
                        {
                            ModelState.AddModelError("HomeTeam", "please make sure that the team are diferent for home and away side");
                            ModelState.AddModelError("AwayTeam", "please make sure that the team are diferent for home and away side");

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("TimeStamp", "please make sure you have enterd a valid date for this match");
                    }
                }
                else
                {
                   ModelState.AddModelError("Away goals", "please make sure you have enterd the number of goals that away team scored it");
                }
            }
            else
            {
                ModelState.AddModelError("Home goals", "please make sure you have enterd the number of goals that home team scored it");
            }

            return View(match);
        }
	}
}