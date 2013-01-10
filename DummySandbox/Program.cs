using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using Fizzler;

namespace DummySandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.RegisterCustomSelector<HtmlNode, string>("where-class-is", @class =>
            {
                return nodes => nodes.Where(x => x.GetAttributeValue("class", null) == @class);
            });
            Parser.RegisterCustomSelector<HtmlNode>("where-is-b", () =>
            {
                return nodes => nodes.Where(x => x.Name == "b");
            });
            Parser.RegisterCustomSelector<HtmlNode, Selector<HtmlNode>>("inception", (selector) =>
            {
                return selector;
            });

            var doc = new HtmlDocument();
            var p = doc.CreateElement("p");
            p.InnerHtml = (@"

<div id=a>
<div class=asdf>
<h1>Music</h1>

<hr>
<b>Kate Havnevik</b>
<i>Grace</i>
<i>Show Me Love</i>
<i>Kaleidoscope</i>

<hr>
<b>Evanescence</b>
<i>My Immortal</i>
<i>Lithium</i>
<i>Tourniquet</i>


<footer>(c) 2011</footer>

</div>
</div>

");

            var result = p.QuerySelectorAll("*:inception(:has(footer))").ToArray();

            var r = p.QuerySelectorAll("#a > .asdf:split-after(hr)").ToArray();
            var s = r[0].ChildNodes[0].ParentNode;

            //  var asd = p.QuerySelectorAll("#a > .asdf:split-after(hr):after(b)").ToArray();
            var sdf = p.QuerySelectorAll(".asdf:between(hr; footer)").ToArray();

            //  var r = p.QuerySelector(".asdf").QuerySelectorAll(":select-parent");

        }
    }
}
