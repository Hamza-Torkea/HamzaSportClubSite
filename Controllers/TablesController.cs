using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportClubSite.Models;


namespace SportClubSite.Controllers
{
    public class TablesController : Controller
    {
        //
        // GET: /Tables/
        public ActionResult Index()
        {
            List<LeagueTableResult> results = new List<LeagueTableResult>();

            MatchesRepository m = new MatchesRepository();
            var allmatches = m.GetAllMatches();

            TeamsRepository t = new TeamsRepository();
            var allteams = t.GetAllTeams();
            if (allteams.Count() > 0)
            {
                foreach (Team e in allteams)
                {
                    LeagueTableResult res = new LeagueTableResult();
                    res.TeamId = e.Team_id;
                    res.TeamName = e.TeamName;

                    int wins = 0;
                    int losses = 0;
                    int draws = 0;

                    int goalsfor = 0;
                    int goalsagainst = 0;

                    int points = 0;
                    
                    var allteammatches = allmatches.Where(k => k.HomeTeam == e.Team_id || k.AwayTeam == e.Team_id);
                    var allteamhomematches = allteammatches.Where(k => k.HomeTeam == e.Team_id);
                    var allteamawaymatches = allteammatches.Where(k => k.AwayTeam == e.Team_id);
                    foreach (Match a in allteamhomematches)
                    {
                        if(a.HomeGoals > a.AwayGoals)
                        {
                            wins++;
                            points = points + 3;
                        }
                        if (a.HomeGoals < a.AwayGoals)
                        {
                            losses++;
                            points = points + 0;
                        }
                        if (a.HomeGoals == a.AwayGoals)
                        {
                            draws++ ;
                            points = points + 1;
                        }
                        goalsfor += (int)a.HomeGoals;
                        goalsagainst += (int)a.AwayGoals;
                    }
                    foreach (Match aa in allteamawaymatches)
                    {
                        if (aa.AwayGoals > aa.HomeGoals)
                        {
                            wins++;
                            points = points + 3;
                        }
                        if (aa.AwayGoals < aa.HomeGoals)
                        {
                            losses++;
                            points = points + 0;
                        }
                        if(aa.AwayGoals == aa.HomeGoals)
                        {
                            draws++;
                            points = points + 1;
                        }
                        goalsfor = (int)aa.AwayGoals;
                        goalsagainst = (int)aa.HomeGoals;
                    }
                    res.Wins = wins;
                    res.Losses = losses;
                    res.Draws = draws;
                    res.GoalsFor = goalsfor;
                    res.GoalsAgainst = goalsagainst;
                    res.GoalsDifference = goalsfor - goalsagainst;
                    res.Points = points;
                    results.Add(res);
                }
            }
            ViewData["Results"] = results.OrderByDescending(k => k.Points).ThenByDescending(k => k.GoalsDifference);

            return View();
        }
	}
}