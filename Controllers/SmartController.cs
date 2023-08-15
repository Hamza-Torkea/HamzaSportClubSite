using SportClubSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportClubSite.Controllers
{
    public class SmartController : Controller
    {
        [HttpGet]
        public ActionResult IndexCycle()
        {
            List<Team> lst = new List<Team>();
            lst.Add(new Team() { Team_id = 1, TeamName = "Real Madrid FC", HomeTeamImage = "~/image/aaa1.png", AwayTeamImage = "~/image/aaa1.png", stadium = "Santiago Bernabeu" });
            lst.Add(new Team() { Team_id = 2, TeamName = "Barcalona FC", HomeTeamImage = "~/image/barclona.jpg", AwayTeamImage = "/image/barclona.jpg", stadium = "Camp Nou" });
            lst.Add(new Team() { Team_id = 3, TeamName = "Juventus FC", HomeTeamImage = "~/image/Juventus.png", AwayTeamImage = "~/image/Juventus.png", stadium = "Alinze Studium" });
            lst.Add(new Team() { Team_id = 4, TeamName = "Bayern Munchen FC", HomeTeamImage = "~/image/Bayern muncin.png", AwayTeamImage = "~/image/Bayern muncin.png", stadium = "Alinze Arena" });
            lst.Add(new Team() { Team_id = 5, TeamName = "Atletckio Madrid FC", HomeTeamImage = "~/image/Atletko.png", AwayTeamImage = "~/image/Atletko.png", stadium = "Vesente Calderon" });
            lst.Add(new Team() { Team_id = 6, TeamName = "Inter Millan FC", HomeTeamImage = "~/image/inter.jpg", AwayTeamImage = "~/image/inter.jpg", stadium = "Josepe Meaza" });
            lst.Add(new Team() { Team_id = 7, TeamName = "AC Milan FC", HomeTeamImage = "~/image/ac millan.jpg", AwayTeamImage = "~/image/ac millan.jpg", stadium = "San Sero" });
            lst.Add(new Team() { Team_id = 8, TeamName = "Liverbool FC", HomeTeamImage = "~/image/liverbol.png", AwayTeamImage = "~/image/liverbol.png", stadium = "Anfelid Studium" });
            lst.Add(new Team() { Team_id = 9, TeamName = "Manchster City FC", HomeTeamImage = "~/image/Manchster city.png", AwayTeamImage = "~/image/Manchster city.png", stadium = "AlEthad Studiium" });
            lst.Add(new Team() { Team_id = 10, TeamName = "Manchster United FC", HomeTeamImage = "~/image/manchster united.jpg", AwayTeamImage = "~/image/manchster united.jpg", stadium = "Old Traford" });
            lst.Add(new Team() { Team_id = 12, TeamName = "Totenham FC", HomeTeamImage = "~/image/totenham.jfif", AwayTeamImage = "~/image/totenham.jfif", stadium = "AlHlam Studium" });
            lst.Add(new Team() { Team_id = 15, TeamName = "Totenham FC", HomeTeamImage = "~/image/napoli.png", AwayTeamImage = "~/image/napoli.png", stadium = "San paolo" });
            List<Match> llst = PredictionModel.generateCycle(lst);
            return View(llst);
        }

        [HttpGet]
        public ActionResult IndexPredictGet()
        {

            

            return View();

        }
        [HttpPost]
        public JsonResult PostMethod(string name)
        {
            PredictModel p = new PredictModel
            {
                Name = name,
                DateTime = DateTime.Now.ToString()
            };
            return Json(p);
        }
       
    }
}