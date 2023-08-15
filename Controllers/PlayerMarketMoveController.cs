using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportClubSite.Models;

namespace SportClubSite.Controllers
{
    public class PlayerMarketMoveController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /PlayerMarketMove/
        public ActionResult Index()
        {
            List<Market> lst = new List<Market>();
            lst.Add(new Market() { MarketId = 1, Teambay = "Bayern Munchen FC", TeamSell = "Real Madrid FC ", HomeTeamImage = "~/image/Bayern muncin.png", AwayTeamImage = "~/image/aaa1.png", PlayerName = "James Rodreqes", Price = 100.2 });
            lst.Add(new Market() { MarketId = 2, Teambay = "Barcalona FC", TeamSell = "Juventus FC", HomeTeamImage = "~/image/barclona.jpg", AwayTeamImage = "~/image/Juventus.png", PlayerName = "Artoro Vidal",Price=60.7 });
            lst.Add(new Market() { MarketId = 3, Teambay = "Juventus FC", TeamSell = "Atletckio Madrid FC", HomeTeamImage = "~/image/Juventus.png", AwayTeamImage = "~/image/Atletko.png", PlayerName = "Mario Manzuketsh" , Price=55.7 });
            lst.Add(new Market() { MarketId = 4, Teambay = "Bayern Munchen FC", TeamSell = "Brosia Durtmund", HomeTeamImage = "~/image/Bayern muncin.png", AwayTeamImage = "~/image/v3.jpg", PlayerName = "Ropert Lefandofski" , Price=122.4 });
            lst.Add(new Market() { MarketId = 5, Teambay = "Barcalona FC", TeamSell = "Atletckio Madrid FC", HomeTeamImage = "~/image/barclona.jpg", AwayTeamImage = "~/image/Atletko.png", PlayerName = "Antoan Grezman" ,Price=92.6 });
            lst.Add(new Market() { MarketId = 6, Teambay = "Juventus FC", TeamSell = "Real Madrid FC", HomeTeamImage = "~/image/Juventus.png", AwayTeamImage = "~/image/aaa1.png", PlayerName = "Cristano Ronaldo" ,Price=128.2 });
            lst.Add(new Market() { MarketId = 7, Teambay = "AC Milan FC", TeamSell = "Real Madrid FC", HomeTeamImage = "~/image/ac millan.jpg", AwayTeamImage = "~/image/aaa1.png", PlayerName = "Theo Hernandes" ,Price=50.2 });
           
            return View(lst);
        }
	}
}