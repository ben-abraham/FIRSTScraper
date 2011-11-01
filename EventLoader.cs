﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace FIRST
{
    class EventLoader
    {
        public string[] EventTypes = new string[] { "Championship",
                                                    "Regional",
                                                    "MI FRC State Championship",
                                                    "MI District"};

        string LassoLocation = "https://my.usfirst.org/myarea/index.lasso";

        public EventLoader(int year)
        {
            string responseFromServer = "";
            WebRequest request = WebRequest.Create(LassoLocation + "?omit_searchform=1");
            request.Method = "POST";
            string postData = "page=searchresults&skip_events=0&skip_teams=1&programs=FRC&season_FRC=" + year.ToString() + "&reports=events&area=ALL&results_size=250";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseFromServer);
            HtmlNode node = doc.DocumentNode;

            List<Event> events = new List<Event>();
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
                        e.Add("Date", descriptions[4].InnerText);
                        events.Add(e);
                    }
                }
            }
            

            File.WriteAllText("output.html", responseFromServer);
        }

    }

    class Event : Dictionary<string, string>
    {
        public string Name { get { return this["Name"]; } }
        public string Venue { get { return this["Event Venue"]; } }
        public string Location { get { return this["Location"]; } }
    }
}