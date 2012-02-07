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

            var r = p.QuerySelectorAll("#a > .asdf:split-after(hr)").ToArray();
            var s = r[0].ChildNodes[0].ParentNode;

            var asd = p.QuerySelectorAll("#a > .asdf:split-after(hr):before(b)").ToArray();

          //  var r = p.QuerySelector(".asdf").QuerySelectorAll(":select-parent");

        }
    }
}
