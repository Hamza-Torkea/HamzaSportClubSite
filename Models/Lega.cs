using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class Lega
    {
        SportSiteEntities40 db = new SportSiteEntities40();


        public List<Cycle1> GetAllTables()
        {
            return db.Cycle1.ToList();
        }
         public void Add(Cycle1 c)
        {
            db.Cycle1.Add(c);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

       
    }
}