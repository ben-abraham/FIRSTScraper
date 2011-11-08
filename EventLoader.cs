using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace FIRST
{
    public delegate void ProgressEventHandler(object sender, UploadProgressChangedEventArgs e);
    public delegate void EventsUpdatedHandler(object sender, EventArgs e);

    class EventLoader
    {
        public event ProgressEventHandler DownloadProgressChanged;
        public event EventsUpdatedHandler EventsUpdated;

        public string[] EventTypes = new string[] { "Championship",
                                                    "Regional",
                                                    "MI FRC State Championship",
                                                    "MI District"};

        public static string LassoLocation = "https://my.usfirst.org/myarea/index.lasso";

        public List<Event> events;
        private WebClient client;
        private NameValueCollection values;

        public EventLoader()
        {
            events = new List<Event>();
            client = new WebClient();

            values = new NameValueCollection();
            values.Add("page", "searchresults");
            values.Add("skip_events", "0");
            values.Add("skip_teams", "1");
            values.Add("programs", "FRC");
            values.Add("season_FRC", "2011");
            values.Add("reports", "events");
            values.Add("area", "ALL");
            values.Add("results_size", "250");
            values.Add("year", "2011");

            client.UploadProgressChanged += new UploadProgressChangedEventHandler(client_UploadProgressChanged);
            client.UploadValuesCompleted += new UploadValuesCompletedEventHandler(client_UploadValuesCompleted);
        }

        void client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
                DownloadProgressChanged(this, e);
        }

        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            ParseResults(e.Result);

            if (EventsUpdated != null)
                EventsUpdated(this, new EventArgs());
        }

        public void GetEvents(int year)
        {
            values["year"] = year.ToString();
            client.UploadValuesAsync(new Uri(LassoLocation), values);
        }

        public void ParseResults(byte[] response)
        {
            string responseFromServer = "";
            responseFromServer = Encoding.ASCII.GetString(response);
           
            HtmlDocument doc = new HtmlDocument();
            
            doc.LoadHtml(responseFromServer);
            HtmlNode node = doc.DocumentNode;

            events = new List<Event>();
            foreach (HtmlNode row in node.GetNodesByClass("tr"))
            {
                HtmlNode temp = row.FirstChildWithClass("td");
                if (temp != null)
                {
                    if (EventTypes.Contains(temp.InnerText))
                    {
                        Event e = new Event();
                        List<HtmlNode> descriptions = row.GetNodesByClass("td").ToList();
                        HtmlNode linker = descriptions[1].FirstChildWithClass("a");
                        string link = linker.Attributes["href"].Value;


                        e.Add("Type", descriptions[0].InnerText);
                        e.Add("Name", linker.InnerText);
                        e.Add("Link", link);
                        e.Add("Event Venue", descriptions[2].InnerText);
                        e.Add("Location", descriptions[3].InnerText);
                        e.Add("Date", descriptions[4].InnerText.Trim());
                        events.Add(e);
                    }
                }
            }
        }
    }
}
