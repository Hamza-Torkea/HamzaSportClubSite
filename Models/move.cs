using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class move
    {
        SportSiteEntities40 db = new SportSiteEntities40();


        public List<PlayerMarketMove> GetAllMove()
        {
            return db.PlayerMarketMoves.ToList();
        }
        public void Add(PlayerMarketMove p)
        {
            db.PlayerMarketMoves.Add(p);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}