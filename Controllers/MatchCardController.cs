using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportClubSite.Models;
using System.Data.Entity;

namespace SportClubSite.Controllers
{
    public class MatchCardController : Controller
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        //
        // GET: /MatchCard/

        public ActionResult Index1()
        {

            return View();
        }

        public ActionResult Index()
        {
            List<Name> hh = db.Names.ToList();
            string ii = "hh";
            foreach (Name i in hh)
            {
                ii = i.Name1;

            }
            List<User> hh1 = db.Users.ToList();
            int i1 = 777777;
            foreach (User i in hh1)
            {
                if (i.UserName.Equals(ii))
                {
                    i1 = i.UserId;
                }

            }
            Session["UserId"] = i1;
            
            if (TempData["card"] != null)
            {
                float x = 0;
                List<Card> li2 = TempData["card"] as List<Card>;
                foreach (var item in li2)
                {
                    x += item.Bill;

                }

                TempData["total"] = x;
            }
            TempData.Keep();
            return View(db.MatchCards.OrderByDescending(x => x.MatchCard_id).ToList());
        }
        public ActionResult AddToCard(int? Id)
        {


            MatchCard m = db.MatchCards.Where(x => x.MatchCard_id == Id).SingleOrDefault();
            if(m.IsLocked == true)
            {
                TempData["msg1"] = "This Card Is Alocated By Anthoer User";
                return RedirectToAction("Index");
            }
            return View(m);
        }

        List<Card> li = new List<Card>();
        [HttpPost]
        public ActionResult AddToCard(MatchCard mc, string rankcard, int Id)
        {
            MatchCard m = db.MatchCards.Where(x => x.MatchCard_id == Id).SingleOrDefault();

            Card c = new Card();
            m.IsLocked = true;
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();
            c.MatchCardId = m.MatchCard_id;
            c.Match_Id = m.MatchCard_id;
            c.Price = (float)m.Price;
            c.RankCard = Convert.ToInt32(rankcard);
            c.Bill = c.Price * c.RankCard;
            c.HomeTeam = m.HomeTeam;
            m.IsLocked = true;
            c.AwayTeam = m.AwayTeam;
            c.Studium = m.Studium;
            c.KindOfCard = m.KindOfCard;
            c.chairnumber = m.ChairNumber;
            if (TempData["card"] == null)
            {
                li.Add(c);
                TempData["card"] = li;
               
            }
            else
            {
                List<Card> li2 = TempData["card"] as List<Card>;

                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.MatchCardId == c.MatchCardId)
                    {
                        item.RankCard += c.RankCard;
                        item.Bill += c.Bill;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    li2.Add(c);

                }
                TempData["card"] = li2;
            }

            TempData.Keep();




            return RedirectToAction("Index");
        }
        public ActionResult remove(int? id)
        {
            List<Card> li2 = TempData["card"] as List<Card>;
            Card c = li2.Where(x => x.MatchCardId == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.Bill;
            }
            TempData["total"] = h;
            return RedirectToAction("Index");
        }
        public ActionResult checkout()
        {
            TempData.Keep();


            return View();
        }
        [HttpPost]
        public ActionResult checkout(MatchCardOrder o)
        {
            List<Card> li = TempData["card"] as List<Card>;
            
            BookMatchCard b = new BookMatchCard();

            b.UserId = (int)Session["UserId"];
            b.BookDate = System.DateTime.Now;
            b.totalbill = (float)TempData["total"];
            db.BookMatchCards.Add(b);
            db.SaveChanges();

            foreach (var item in li)
            {
                MatchCardOrder od = new MatchCardOrder();
                od.MOId = item.MatchCardId;
                od.MatchCardId = item.MatchCardId;
                od.UserId = (int)Session["UserId"];
                od.BookMatchCardId = b.BookId;
                od.MatchOrderDate = System.DateTime.Now;
                od.UnitPrice = (int)item.Price;
                od.Bill = item.Bill;
                db.MatchCardOrders.Add(od);
                db.SaveChanges();
            }
            TempData.Remove("total");
            TempData.Remove("card");
            TempData["msg"] = "Transation Completed ...........";
            TempData.Keep();
            return RedirectToAction("Index");
        }
	}
}