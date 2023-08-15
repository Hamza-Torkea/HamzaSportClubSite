using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClubSite.Models
{
    public class PredictionModel
    {
        private static string QUERY_CONST = @"select predict([Team Won])
                                            from
                                            [Predict Match Score]
                                            natural prediction join
                                            (
                                                select ""{0}"" as [Home Team],
	                                              ""{1}"" as [away Team],
	                                            ""{2}"" as [Match Time],
	                                            ""{3}"" as [Studium],
	                                            ""{4}"" as [Weather],
	                                              {5} as [Total Point Home Team],
	                                              {6} as [Total Point Away Team],
	                                              {7} as [Rank Home Team],
	                                              {8} as [Rank Away Team]
                                            ) as results";


        private static string CONNECTION_STRING_CONST = "Provider=MSOLAP; Initial Catalog={0}; Connect Timeout=30; Data Source=.;";
        private static string RESULT_COLUMN_CONST = "Expression";







        public static string Predict(PredictModel model)
        {
            string connectionString = String.Format(CONNECTION_STRING_CONST, "SportClubSite_AS");
            string query = String.Format(QUERY_CONST, model.homeTeam,model.awayTeam,model.matchTime,model.studium,model.weather,model.totalPointHomeTeam,model.totalPointAwayTeam,model.rankHomeTeam,model.rankAwayTeam);
            using (var conn = new AdomdConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var command = new AdomdCommand(query, conn);
                    command.CommandTimeout = 10000;
                    AdomdDataReader result = command.ExecuteReader();
                    if (result.Read())
                    {
                        return result[RESULT_COLUMN_CONST].ToString();
                    }
                    return "";
                }
                catch (Exception e)
                {
                    conn.Close();
                }
                return "";
            }
            
        }
        public static List<Match> generateCycle(List<Team> teams)
        {
            List<Match> matches = new List<Match>();

            int hour = 1;
            foreach(var homeTeam in teams)
            {
                DateTime timeNow = DateTime.Now.AddHours(hour);
                DateTime date = timeNow;

                foreach (var awayTeam in teams)
                {
                    
                    if (homeTeam.Team_id != awayTeam.Team_id)
                    {
                        Match match = new Match();
                        match.HomeTeam = homeTeam.Team_id;
                        match.AwayTeam = awayTeam.Team_id;
                        match.Timestamp = date;
                        match.HomeTeamImage = homeTeam.HomeTeamImage;
                        match.AwayTeamImage = awayTeam.AwayTeamImage;
                        match.stadium = homeTeam.stadium;
                         matches.Add(match);
                         date = date.AddDays(5);
                    }

                }
                hour += 1;
            }

            return matches;
        }
    }


    public class PredictModel
    {
        public int predictID { set; get; }
        public string homeTeam { set; get; }
        public string awayTeam { set; get; }
        public string matchTime { set; get; }
        public string studium { set; get; }
        public string weather { set; get; }
        public int rankHomeTeam { set; get; }
        public int rankAwayTeam { set; get; }
        public int totalPointHomeTeam { set; get; }
        public int totalPointAwayTeam { set; get; }
        public int FTHG { set; get; }
        public int FTAG { set; get; }
        public int HST { get; set; }
        public int AST { set; get; }
        public int HTGDIFF { set; get; }
        public int ATGDIFF { get; set; }
        public string powerofPlayer { set; get; }
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string result { set; get; }

    }
}