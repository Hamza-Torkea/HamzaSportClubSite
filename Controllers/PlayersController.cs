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
    public class PlayersController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /Players/
        public ActionResult Index()
        {
            PlayersRepository plrep = new PlayersRepository();
            var AllPlayer = plrep.GetAllPlayers();
            if (AllPlayer.Count > 0)
            {
                ViewData["All Player"] = AllPlayer.OrderBy(k => k.PlayerName);
            }
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Player player)
        {
            if (!string.IsNullOrWhiteSpace(player.PlayerName))
            {
                PlayersRepository plarep = new PlayersRepository();

                if (plarep.GetAllPlayers().Where(k => k.PlayerName.ToLower() == player.PlayerName.ToLower()).Count() > 0)
                {
                    ModelState.AddModelError("PlayerName", "the player name is alredy in use");
                }
                else
                {
                    plarep.Add(player);
                    plarep.SaveChanges();
                    return Redirect("/players");
                }
            }
            else
            {
                ModelState.AddModelError("PlayerName", "Please Enter player Name");
            }
           


            return View(player);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player p = db.Players.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Player_id,TeamName,PlayerName,Long,Wieght,Price")] Player p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(p);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Player p = db.Players.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player pp = db.Players.Find(id);
            db.Players.Remove(pp);
            db.SaveChanges();
            return Redirect("/players");
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