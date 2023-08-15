﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class LeagueTableResult
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalsDifference { get; set; }
        public int Points { get; set; }
        

    }
}