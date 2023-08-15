using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class MatchReportRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();

        public void Add(MatchReport report)
        {
            db.MatchReports.Add(report);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
        public List<MatchReport>GetAllReports()
        {
            return db.MatchReports.ToList();
        }
    }
}