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
            var p = doc.CreateElement("p");
            p.InnerHtml = (@"

<div class=asdf>
<span><b>qui</b></span>
<span><b>quo</b></span>
<span><b>qua</b></span>
</div>

");

            p.QuerySelectorAll("/ * *");
          //  var r = p.QuerySelector(".asdf").QuerySelectorAll(":select-parent");

        }
    }
}
