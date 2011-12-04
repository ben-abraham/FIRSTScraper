using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FIRST
{
    public class EventResults
    {
        public readonly static string URL = "http://www2.usfirst.org/2010comp/events/LA/matchresults.html";

        public EventResults()
        {
        }

        public ReadOnlyCollection<EventResultRow> QualificationMatches { get; set; }
        public ReadOnlyCollection<EventResultRow> EliminationMatches { get; set; }

    }

    public class EventResultRow
    {

        public EventResultRow() { }

        public int RedTeam1ID  { get; set; } 
        public int RedTeam2ID  { get; set; }
        public int RedTeam3ID  { get; set; } 
        
        public int BlueTeam1ID { get; set; }
        public int BlueTeam2ID { get; set; } 
        public int BlueTeam3ID { get; set; }

        public DateTime Time { get; set; }
        public int RedScore { get; set; }
        public int BlueScore { get; set; }
        public int MatchID { get; set; }

        // Somtimes blank 
        public string Description { get; set; }
    }

}
