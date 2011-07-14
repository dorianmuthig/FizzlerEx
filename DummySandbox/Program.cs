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

<div>
<span><b>qui</b></span>
<span><b>quo</b></span>
<span><b>qua</b></span>
</div>

");

            var r = div.QuerySelectorAll("span:contains('quo')");

        }
    }
}
