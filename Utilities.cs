using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace FIRST
{
    static class Utilities
    {
        public static IEnumerable<HtmlNode> GetNodesByCssClass(this HtmlNode parent, string cssClass)
        {
            foreach (HtmlNode n in parent.DescendantNodes())
            {
                if (n.Attributes["class"] != null && n.Attributes["class"].Value == cssClass)
                {
                    yield return n;
                }
            }
        }
        public static IEnumerable<HtmlNode> GetNodesByClass(this HtmlNode parent, string Class)
        {
            foreach (HtmlNode n in parent.DescendantNodes())
            {
                if (n.OriginalName.Equals(Class, StringComparison.CurrentCultureIgnoreCase))
                {
                    yield return n;
                }
            }
        }
        public static bool EqualsAttributes(this HtmlAttributeCollection collection, HtmlAttributeCollection second)
        {
            if(collection.Count != second.Count)
                return false;

            bool all = true;
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i] != second[i])
                {
                    all = false;
                }
            }

            return all;
        }
    }
}
