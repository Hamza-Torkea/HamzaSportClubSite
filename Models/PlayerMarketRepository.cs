using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class PlayerMarketRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();

        public List<Market> GetAllPlayerMarketMove()
        {
            return db.Markets.ToList();
        }
        public void Add(Market m)
        {
            db.Markets.Add(m);
        }
        public void Delete(Market m)
        {
            db.Markets.Remove(m);
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}