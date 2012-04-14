using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace VisualFizzler
{
    public class TagMatch
    {

        [CLSCompliant(false)]
        public TagMatch(HtmlNode node)
        {
            StartIndex = GetStartIndex(node);
            TagLength = GetTagLength(node);
            EndIndex = GetEndIndex(node);
        }


        public int StartIndex { get; private set; }
        public int TagLength { get; private set; }
        public int EndIndex { get; private set; }
        public int FullLength { get { return EndIndex != -1 ? EndIndex - StartIndex : 0; } }




        private static HtmlNode GetFirstActualNode(HtmlNode node)
        {
            if (node == null) return null;
            if (node.Name == "fizzler_nodes_group") return GetFirstActualNode(node.FirstChild);
            return node;
        }

        private static HtmlNode GetLastActualNode(HtmlNode node)
        {
            if (node == null) return null;
            if (node.Name == "fizzler_nodes_group") return GetLastActualNode(node.LastChild);
            return node;
        }




        private static int GetStartIndex(HtmlNode node)
        {
            node = GetFirstActualNode(node);
            return node != null ? node.StreamPosition : -1;
        }

        private static int GetEndIndex(HtmlNode node)
        {
            node = GetLastActualNode(node);
            if (node == null) return -1;
            var next = node.NextSibling;
            return next != null ? next.StreamPosition : (node.StreamPosition + node.OuterHtml.Length);
        }

        private static int GetTagLength(HtmlNode node)
        {
            var first = GetFirstActualNode(node);

            if (first == null) return 0;

            if (first.NodeType == HtmlNodeType.Element)
                return first.Name.Length + (first.HasAttributes ? 1 : 2);

            var length = first.OuterHtml.Length;
            if (first != node && node.ChildNodes.Count >= 2)
                length += GetTagLength(node.ChildNodes[1]);

            return length;

        }
    }
}
