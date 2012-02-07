using Fizzler;

namespace Fizzler.Systems.HtmlAgilityPack
{
    #region Imports

    using System;
    using System.Linq;
    using global::HtmlAgilityPack;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// An <see cref="IElementOps{TElement}"/> implementation for <see cref="HtmlNode"/>
    /// from <a href="http://www.codeplex.com/htmlagilitypack">HtmlAgilityPack</a>.
    /// </summary>
    public class HtmlNodeOps : IElementOps<HtmlNode>
    {
        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#type-selectors">type selector</a>,
        /// which represents an instance of the element type in the document tree. 
        /// </summary>
        public virtual Selector<HtmlNode> Type(NamespacePrefix prefix, string type)
        {
            return prefix.IsSpecific
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => nodes.Elements().Where(n => n.Name == type));
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#universal-selector">universal selector</a>,
        /// any single element in the document tree in any namespace 
        /// (including those without a namespace) if no default namespace 
        /// has been specified for selectors. 
        /// </summary>
        public virtual Selector<HtmlNode> Universal(NamespacePrefix prefix)
        {
            return prefix.IsSpecific
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => nodes.Elements());
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#Id-selectors">ID selector</a>,
        /// which represents an element instance that has an identifier that 
        /// matches the identifier in the ID selector.
        /// </summary>
        public virtual Selector<HtmlNode> Id(string id)
        {
            return nodes => nodes.Elements().Where(n => n.Id == id);
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#class-html">class selector</a>,
        /// which is an alternative <see cref="IElementOps{TElement}.AttributeIncludes"/> when 
        /// representing the <c>class</c> attribute. 
        /// </summary>
        public virtual Selector<HtmlNode> Class(string clazz)
        {
            return nodes => nodes.Elements().Where(n => n.GetAttributeValue("class", string.Empty)
                                                         .Split(' ')
                                                         .Contains(clazz));
        }

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the given attribute <paramref name="name"/>
        /// whatever the values of the attribute.
        /// </summary>
        public virtual Selector<HtmlNode> AttributeExists(NamespacePrefix prefix, string name)
        {
            return prefix.IsSpecific
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => nodes.Elements().Where(n => n.Attributes[name] != null));
        }

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the given attribute <paramref name="name"/>
        /// and whose value is exactly <paramref name="value"/>.
        /// </summary>
        public virtual Selector<HtmlNode> AttributeExact(NamespacePrefix prefix, string name, string value)
        {
            var withoutAttribute = string.IsNullOrEmpty(value);

            return prefix.IsSpecific
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => from n in nodes.Elements()
                             let a = n.Attributes[name]
                             where withoutAttribute ? (a == null || string.IsNullOrEmpty(a.Value)) : (a != null && a.Value == value)
                             select n);
        }


        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element without the given attribute <paramref name="name"/>
        /// or with a different value <paramref name="value"/>.
        /// </summary>
        public virtual Selector<HtmlNode> AttributeNotEqual(NamespacePrefix prefix, string name, string value)
        {
            return prefix.IsSpecific
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => from n in nodes.Elements()
                             let a = n.Attributes[name]
                             where a == null || a.Value != value
                             select n);
        }

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the given attribute <paramref name="name"/>
        /// and whose value is a whitespace-separated list of words, one of 
        /// which is exactly <paramref name="value"/>.
        /// </summary>
        public virtual Selector<HtmlNode> AttributeIncludes(NamespacePrefix prefix, string name, string value)
        {
            return prefix.IsSpecific
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => from n in nodes.Elements()
                             let a = n.Attributes[name]
                             where a != null && a.Value.Split(' ').Contains(value)
                             select n);
        }

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the given attribute <paramref name="name"/>,
        /// its value either being exactly <paramref name="value"/> or beginning 
        /// with <paramref name="value"/> immediately followed by "-" (U+002D).
        /// </summary>
        public virtual Selector<HtmlNode> AttributeDashMatch(NamespacePrefix prefix, string name, string value)
        {
            return prefix.IsSpecific || string.IsNullOrEmpty(value)
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => from n in nodes.Elements()
                             let a = n.Attributes[name]
                             where a != null && a.Value.Split('-').Contains(value)
                             select n);
        }

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the attribute <paramref name="name"/> 
        /// whose value begins with the prefix <paramref name="value"/>.
        /// </summary>
        public Selector<HtmlNode> AttributePrefixMatch(NamespacePrefix prefix, string name, string value)
        {
            return prefix.IsSpecific || string.IsNullOrEmpty(value)
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => from n in nodes.Elements()
                             let a = n.Attributes[name]
                             where a != null && a.Value.StartsWith(value)
                             select n);
        }

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the attribute <paramref name="name"/> 
        /// whose value ends with the suffix <paramref name="value"/>.
        /// </summary>
        public Selector<HtmlNode> AttributeSuffixMatch(NamespacePrefix prefix, string name, string value)
        {
            return prefix.IsSpecific || string.IsNullOrEmpty(value)
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => from n in nodes.Elements()
                             let a = n.Attributes[name]
                             where a != null && a.Value.EndsWith(value)
                             select n);
        }

        /// <summary>
        /// Generates an <a href="http://www.w3.org/TR/css3-selectors/#attribute-selectors">attribute selector</a>
        /// that represents an element with the attribute <paramref name="name"/> 
        /// whose value contains at least one instance of the substring <paramref name="value"/>.
        /// </summary>
        public Selector<HtmlNode> AttributeSubstring(NamespacePrefix prefix, string name, string value)
        {
            return prefix.IsSpecific || string.IsNullOrEmpty(value)
                 ? (Selector<HtmlNode>)(nodes => Enumerable.Empty<HtmlNode>())
                 : (nodes => from n in nodes.Elements()
                             let a = n.Attributes[name]
                             where a != null && a.Value.Contains(value)
                             select n);
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
        /// which represents an element that is the first child of some other element.
        /// </summary>
        public virtual Selector<HtmlNode> FirstChild()
        {
            return nodes => nodes.Where(n => !n.ElementsBeforeSelf().Any());
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
        /// which represents an element that is the last child of some other element.
        /// </summary>
        public virtual Selector<HtmlNode> LastChild()
        {
            return nodes => nodes.Where(n => n.ParentNode.NodeType != HtmlNodeType.Document
                                          && !n.ElementsAfterSelf().Any());
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
        /// which represents an element that is the N-th child of some other element.
        /// </summary>
        public virtual Selector<HtmlNode> NthChild(int a, int b)
        {
            if (a != 1)
                throw new NotSupportedException("The nth-child(an+b) selector where a is not 1 is not supported.");

            return nodes => from n in nodes
                            let elements = n.ParentNode.Elements().Take(b).ToArray()
                            where elements.Length == b && elements.Last().Equals(n)
                            select n;
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
        /// which represents an element that has a parent element and whose parent 
        /// element has no other element children.
        /// </summary>
        public virtual Selector<HtmlNode> OnlyChild()
        {
            return nodes => nodes.Where(n => n.ParentNode.NodeType != HtmlNodeType.Document
                                          && !n.ElementsAfterSelf().Concat(n.ElementsBeforeSelf()).Any());
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
        /// which represents an element that has no children at all.
        /// </summary>
        public virtual Selector<HtmlNode> Empty()
        {
            return nodes => nodes.Elements().Where(n => n.ChildNodes.Count == 0);
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#combinators">combinator</a>,
        /// which represents a childhood relationship between two elements.
        /// </summary>
        public virtual Selector<HtmlNode> Child()
        {
            return nodes => nodes.SelectMany(n => n.Elements());
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#combinators">combinator</a>,
        /// which represents a relationship between two elements where one element is an 
        /// arbitrary descendant of some ancestor element.
        /// </summary>
        public virtual Selector<HtmlNode> Descendant()
        {
            return nodes => nodes.SelectMany(n => n.Descendants().Elements());
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#combinators">combinator</a>,
        /// which represents elements that share the same parent in the document tree and 
        /// where the first element immediately precedes the second element.
        /// </summary>
        public virtual Selector<HtmlNode> Adjacent()
        {
            return nodes => nodes.SelectMany(n => n.ElementsAfterSelf().Take(1));
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#combinators">combinator</a>,
        /// which separates two sequences of simple selectors. The elements represented
        /// by the two sequences share the same parent in the document tree and the
        /// element represented by the first sequence precedes (not necessarily
        /// immediately) the element represented by the second one.
        /// </summary>
        public virtual Selector<HtmlNode> GeneralSibling()
        {
            return nodes => nodes.SelectMany(n => n.ElementsAfterSelf());
        }

        /// <summary>
        /// Generates a <a href="http://www.w3.org/TR/css3-selectors/#pseudo-classes">pseudo-class selector</a>,
        /// which represents an element that is the N-th child from bottom up of some other element.
        /// </summary>
        public Selector<HtmlNode> NthLastChild(int a, int b)
        {
            if (a != 1)
                throw new NotSupportedException("The nth-last-child(an+b) selector where a is not 1 is not supported.");

            return nodes => from n in nodes
                            let elements = n.ParentNode.Elements().Skip(Math.Max(0, n.ParentNode.Elements().Count() - b)).Take(b).ToArray()
                            where elements.Length == b && elements.First().Equals(n)
                            select n;
        }


        public Selector<HtmlNode> Eq(int n)
        {
            return nodes =>
            {
                var node = nodes.ElementAtOrDefault(n);
                return node != null ? new[] { node } : Enumerable.Empty<HtmlNode>();
            };
        }


        public Selector<HtmlNode> Has(ISelectorGenerator subgenerator)
        {
            var castedGenerator = (SelectorGenerator<HtmlNode>)subgenerator;

            var compiled = castedGenerator.Selector;

            return nodes => nodes.Where(n => compiled(new[] { n }).Any());
        }

        public Selector<HtmlNode> SplitAfter(ISelectorGenerator subgenerator)
        {
            return nodes => nodes.SelectMany(x => Split(subgenerator, x, false, true));
        }

        public Selector<HtmlNode> SplitBefore(ISelectorGenerator subgenerator)
        {
            return nodes => nodes.SelectMany(x => Split(subgenerator, x, true, false));
        }

        public Selector<HtmlNode> SplitBetween(ISelectorGenerator subgenerator)
        {
            return nodes => nodes.SelectMany(x => Split(subgenerator, x, false, false));
        }

        public Selector<HtmlNode> SplitAll(ISelectorGenerator subgenerator)
        {
            return nodes => nodes.SelectMany(x => Split(subgenerator, x, true, true));
        }

        private Selector<HtmlNode> GetSelector(ISelectorGenerator subgenerator)
        {
            return ((SelectorGenerator<HtmlNode>)subgenerator).Selector;
        }

        private IEnumerable<HtmlNode> Split(ISelectorGenerator subgenerator, HtmlNode parent, bool keepBefore, bool keepAfter)
        {
            var selector = GetSelector(subgenerator);

            var children = parent.ChildNodes.ToArray();
            var splitterPositions = new List<int>();
            var splitterIndex = 0;
            foreach (var splitter in selector(new[] { parent }))
            {
                splitterIndex = Array.IndexOf(children, splitter, splitterIndex);
                if (splitterIndex == -1)
                    throw new FormatException("The node splitter must be a direct child of the context node.");

                splitterPositions.Add(splitterIndex);
            }

            if (splitterPositions.Count == 0)
            {
                if (keepBefore && keepAfter)
                    yield return parent;
                yield break;
            }


            var doc = new HtmlDocument();
            var keepSeparators = keepBefore != keepAfter;


            if (keepBefore)
                yield return CreateNodesGroup(doc, children, 0, splitterPositions[0] + (keepSeparators ? 0 : -1));

            for (int i = 1; i < splitterPositions.Count; i++)
            {

                var indexBegin = splitterPositions[i - 1] + 1;
                var indexEnd = splitterPositions[i] - 1;

                if (keepSeparators)
                {
                    if (keepAfter) indexBegin--;
                    else indexEnd++;
                }

                yield return CreateNodesGroup(doc, children, indexBegin, indexEnd);
            }


            if (keepAfter)
                yield return CreateNodesGroup(doc, children, splitterPositions[splitterPositions.Count - 1] + (keepSeparators ? 0 : 1), children.Length - 1);

        }


        public Selector<HtmlNode> Before(ISelectorGenerator subgenerator)
        {
            var doc = new HtmlDocument();
            return nodes => nodes.SelectNonNull(parent =>
            {
                var end = IndexOfChild(subgenerator, parent, 0);
                return end != null ? CreateNodesGroup(doc, parent.ChildNodes, 0, end.Value - 1) : null;
            });
        }

        public Selector<HtmlNode> After(ISelectorGenerator subgenerator)
        {
            var doc = new HtmlDocument();
            return nodes => nodes.SelectNonNull(parent =>
            {
                var start = IndexOfChild(subgenerator, parent, 0);
                return start != null ? CreateNodesGroup(doc, parent.ChildNodes, start.Value + 1, parent.ChildNodes.Count - 1) : null;
            });
        }

        public Selector<HtmlNode> Between(ISelectorGenerator subgenerator)
        {
            throw new NotImplementedException();
        }

        private int? IndexOfChild(ISelectorGenerator subgenerator, HtmlNode parent, int startIndex)
        {
            var selector = GetSelector(subgenerator);

            var children = parent.ChildNodes;
            var limit = selector(new[] { parent })
                .Select(x => new { Node = x, Position = children.IndexOf(x) })
                .FirstOrDefault(x =>
                {
                    if (x.Position == -1)
                        throw new FormatException("The limit node must be a direct child of the context node.");
                    return x.Position >= startIndex;
                });

            return limit != null ? limit.Position : (int?)null;
        }

        private HtmlNode CreateNodesGroup(HtmlDocument doc, IList<HtmlNode> nodes, int start, int last)
        {
            var group = doc.CreateElement("fizzler_nodes_group");
            for (int i = start; i <= last; i++)
            {
                group.ChildNodes.Add(nodes[i]);
            }
            return group;
        }

        public Selector<HtmlNode> Not(ISelectorGenerator subgenerator)
        {
            var castedGenerator = (SelectorGenerator<HtmlNode>)subgenerator;

            var compiled = castedGenerator.Selector;

            return nodes =>
            {
                var matches = compiled(nodes.Select(x => x.ParentNode)).ToList();
                return nodes.Except(matches);
            };
        }

        public Selector<HtmlNode> SelectParent()
        {
            return nodes => nodes.Select(x => x.ParentNode);
        }

        public Selector<HtmlNode> Contains(string text)
        {
            return nodes => nodes.Where(x => x.InnerText.Contains(text));
        }





    }
}
