using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class MatchesRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();
       

        public List < Match > GetAllMatches()
        {
            return db.Matches.ToList();
        }
        public void Add(Match m)
        {
            db.Matches.Add(m);
        }
        
        public void SaveChanges()
        {
            db.SaveChanges();
        }
      
    }
}