using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace DummySandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new HtmlDocument();
            var div = doc.CreateElement("div");
            div.InnerHtml = (@"

<span></span>
<span><b></b></span>


");

            var r = div.QuerySelectorAll("span:has(b)");

        }
    }
}
