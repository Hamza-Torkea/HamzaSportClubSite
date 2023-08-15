using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportClubSite.Models;

namespace SportClubSite.Controllers
{
    public class PlayerMoveController : Controller
    {
        //
        // GET: /PlayerMove/
        public ActionResult Index()
        {
            move m = new move();
            var MyMove = m.GetAllMove();

            if (MyMove.Count > 0)
            {
                ViewData["MyMove"] = MyMove.OrderByDescending(k => k.playername);
            }

            return View();


        }
	}
}