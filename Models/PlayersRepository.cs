using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class PlayersRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();

        public List<Player> GetAllPlayers()
        {
            return db.Players.ToList();
        }
        public void Add(Player p)
        {
            db.Players.Add(p);
        }
        public void Delete(Player p)
        {
            db.Players.Remove(p);
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}