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
            var k = HtmlNode.CreateNode(@"

<span><span>
<span><b></b><span>


");

            var r = k.QuerySelector("span:has(b)");

        }
    }
}
