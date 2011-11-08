using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace FIRST
{
    public class Event : Dictionary<string, string>
    {
        private static WebClient client;

        public string Name { get { return this["Name"]; } }
        public string Venue { get { return this["Event Venue"]; } }
        public string Location { get { return this["Location"]; } }
        public string Date { get { return this["Date"]; } }

        public void LoadEventDetails()
        {
            if (client == null)
                client = new WebClient();

            string result = client.DownloadString(EventLoader.LassoLocation + this["Link"]);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            foreach (HtmlNode row in doc.DocumentNode.GetNodesByClass("tr"))
            {
                Console.WriteLine(row.OuterHtml);
                List<HtmlNode> entries = row.GetNodesByClass("td").ToList();
                if (entries.Count == 2 && entries[1].FirstChild.Name == "a")
                {
                    this.Add(entries[0].InnerText, entries[1].FirstChild.InnerText);
                }
            }
        }
    }
}
