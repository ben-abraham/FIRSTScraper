using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace FIRST
{
    class EventPage
    {
        public EventPage(string url)
        {
            WebClient cli = new WebClient();
            string html = cli.DownloadString(url);
            HtmlDocument d = new HtmlDocument();
            d.LoadHtml(html);
            List<HtmlNode> tables = d.DocumentNode.GetNodesByClass("table").ToList();
            foreach (HtmlNode table in tables)
            {
                List<HtmlNode> nodes = table.GetNodesByClass("TR").ToList();
                Console.WriteLine("Table Found with {0} rows", nodes.Count);
            }
        }
    }
}
