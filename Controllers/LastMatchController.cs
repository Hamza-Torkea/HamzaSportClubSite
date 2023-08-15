using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SportClubSite.Models;

namespace SportClubSite.Controllers
{
    public class LastMatchController : Controller
    {
        //
        // GET: /LastMatch/
        public ActionResult Index()
        {
            MatchReportRepository mm = new MatchReportRepository();
            var report = mm.GetAllReports().OrderByDescending(k => k.Match.Timestamp).FirstOrDefault();

            return View(report);
        }
	}
}