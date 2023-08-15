using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class TeamsRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();

        public List<Team>GetAllTeams()
        {
            return db.Teams.ToList();
        }
        public void Add(Team t)
        {
            db.Teams.Add(t);
        }
        public void Delete(Team t)
       {
           db.Teams.Remove(t);
       }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
      
    }
}