using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace FIRST
{
    public class EventResultParser
    {
        public EventResultParser()
        {
        }

        public EventResults ParseLink(string url)
        {
            WebClient client = new WebClient();
            string html = client.DownloadString(EventResults.URL);

            HtmlDocument d = new HtmlDocument();
            d.LoadHtml(html);


            List<HtmlNode> tables = d.DocumentNode.GetNodesByClass("table").ToList();
            //we skip first two tables because its junk

            //Qualification Parseing: 
            List<EventResultRow> qualificationRows = new List<EventResultRow>();
            foreach (HtmlNode row in tables[2].GetNodesByClass("tr").Skip(2))
            {
                List<HtmlNode> columns = row.GetNodesByClass("TD").ToList();

                EventResultRow eventRowData = new EventResultRow();

                eventRowData.Time = DateTime.Parse(columns[0].InnerText);
                eventRowData.Description = ""; //note no description for qulifications
                eventRowData.MatchID = int.Parse(columns[1].InnerText);
                eventRowData.RedTeam1ID = int.Parse(columns[2].InnerText);
                eventRowData.RedTeam2ID = int.Parse(columns[3].InnerText);
                eventRowData.RedTeam3ID = int.Parse(columns[4].InnerText);
                eventRowData.BlueTeam1ID = int.Parse(columns[5].InnerText);
                eventRowData.BlueTeam2ID = int.Parse(columns[6].InnerText);
                eventRowData.BlueTeam3ID = int.Parse(columns[7].InnerText);
                eventRowData.RedScore = int.Parse(columns[8].InnerText);
                eventRowData.BlueScore = int.Parse(columns[9].InnerText);
                qualificationRows.Add(eventRowData);
            }

            List<EventResultRow> eliminationRows = new List<EventResultRow>();
            foreach (HtmlNode row in tables[3].GetNodesByClass("tr").Skip(2))
            {
                List<HtmlNode> columns = row.GetNodesByClass("TD").ToList();

                EventResultRow eventRowData = new EventResultRow();

                eventRowData.Time = DateTime.Parse(columns[0].InnerText);
                eventRowData.Description = columns[1].InnerText; //note no description for qulifications
                eventRowData.MatchID = int.Parse(columns[2].InnerText);
                eventRowData.RedTeam1ID = int.Parse(columns[3].InnerText);
                eventRowData.RedTeam2ID = int.Parse(columns[4].InnerText);
                eventRowData.RedTeam3ID = int.Parse(columns[5].InnerText);
                eventRowData.BlueTeam1ID = int.Parse(columns[6].InnerText);
                eventRowData.BlueTeam2ID = int.Parse(columns[7].InnerText);
                eventRowData.BlueTeam3ID = int.Parse(columns[8].InnerText);
                eventRowData.RedScore = int.Parse(columns[9].InnerText);
                eventRowData.BlueScore = int.Parse(columns[10].InnerText);
                eliminationRows.Add(eventRowData);
            }

            return new EventResults() 
            { 
                QualificationMatches = new System.Collections.ObjectModel.ReadOnlyCollection<EventResultRow>(qualificationRows),
                EliminationMatches = new System.Collections.ObjectModel.ReadOnlyCollection<EventResultRow>(eliminationRows) 
            };
        }
    }
}
