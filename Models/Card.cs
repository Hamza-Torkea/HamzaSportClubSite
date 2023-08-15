using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class Card
    {
        public int MatchCardId { get; set; }

        public int Match_Id { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; } 

        public string Studium { get; set; }

        public float Price { get; set; }

        public DateTime Date { get; set; }

        public int RankCard { get; set; }
        public float Bill { get; set; }
        public int chairnumber { get; set; }
        public string KindOfCard { get; set; }

        public Boolean isloked { get; set; }
    }
}