using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using VisualFizzler;
using HtmlAgilityPack;
using System.IO;

[assembly: DebuggerVisualizer(typeof(HtmlNodeDebuggerVisualizer), typeof(HtmlNodeVisualizerSource), Target = typeof(HtmlAgilityPack.HtmlNode))]
[assembly: DebuggerVisualizer(typeof(HtmlNodeDebuggerVisualizer), typeof(HtmlNodeVisualizerSource), Target = typeof(HtmlAgilityPack.HtmlDocument))]

namespace VisualFizzler
{


    public class HtmlNodeDebuggerVisualizer : DialogDebuggerVisualizer
    {

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            string html;
            using (var s = new StreamReader(objectProvider.GetData(), Encoding.UTF8))
            {
                html = s.ReadToEnd();
            }
            var form = new MainForm();
            form.CloseOnEsc = true;
            form.LoadHtml(html);
            windowService.ShowDialog(form);
        }
    }


    public class HtmlNodeVisualizerSource : VisualizerObjectSource
    {

        public override void GetData(object target, Stream outgoingData)
        {
            var sw = new StreamWriter(outgoingData, Encoding.UTF8);

            if (target is HtmlNode) ((HtmlNode)target).WriteTo(sw);
            else if (target is HtmlDocument) ((HtmlDocument)target).DocumentNode.WriteTo(sw);
            else throw new ArgumentException("Visualized object must be either HtmlNode or HtmlDocument.");

            sw.Flush();
        }
    }
}
