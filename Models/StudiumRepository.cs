using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class StudiumRepository
    {
        SportSiteEntities40 db = new SportSiteEntities40();
        public List<Studium> GetAllStudium()
        {
            return db.Studiums.ToList();
        }
        public void Add(Studium s)
        {
            db.Studiums.Add(s);
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}