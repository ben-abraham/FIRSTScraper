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
            HtmlNode table = d.DocumentNode.GetNodesByClass("table").First();
            List<HtmlNode> nodes = table.GetNodesByClass("TR").ToList();
            HtmlNode TemplateRow = nodes.Last();
            Console.WriteLine(TemplateRow.OuterHtml);
        }
    }
}
