using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class TeamMangmentRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();

        public List<TeamMangment> GetAllTeamMangment()
        {
            return db.TeamMangments.ToList();
        }
        public void Add(TeamMangment t)
        {
            db.TeamMangments.Add(t);
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}