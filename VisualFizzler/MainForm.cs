using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.VisualBasic;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace VisualFizzler
{
    public partial class MainForm : Form
    {
        private static readonly Regex _tagExpression = new Regex(@"\<(?:(?<t>[a-z]+)(?:""[^""]*""|'[^']*'|[^""'>])*|/(?<t>[a-z]+))\>",
            RegexOptions.IgnoreCase
            | RegexOptions.Singleline
            | RegexOptions.Compiled
            | RegexOptions.CultureInvariant);

        private HtmlDocument _originalDocument;
        private HtmlDocument _document;
        private TagMatch[] _selectorMatches;
        private Uri _lastKnownGoodImportedUrl;

        public MainForm()
        {
            if (!DesignMode)
                this.Font = SystemFonts.MessageBoxFont;

            InitializeComponent();
        }

        private void FileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FileOpen_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            var document = new HtmlDocument();
            document.Load(_openFileDialog.FileName);
            Open(document);
        }

        private void ImportFromWeb(Uri url)
        {

            //
            // Download
            //

            string content;
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            try
            {
                using (CurrentCursorScope.EnterWait())
                    content = wc.DownloadString(url);
            }
            catch (WebException e)
            {
                Program.ShowExceptionDialog(e, "Import Error", this);
                return;
            }

            //
            // Make sure it's HTML otherwise get confirmation to proceed.
            //

            var typeHeader = wc.ResponseHeaders[HttpResponseHeader.ContentType];
            var contentType = new ContentType(typeHeader);
            if (string.IsNullOrEmpty(typeHeader) || !contentType.IsHtml())
            {
                var msg = string.Format(
                    "The downloaded resource is \u201c{0}\u201d rather than HTML, "
                    + "which could produce undesired results. Proceed anyway?",
                    contentType.MediaType);

                if (DialogResult.Yes != MessageBox.Show(this, msg, "Non-HTML Content", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    return;
            }

            //
            // If it's too big (> 512KB), get confirmation to proceed.
            //

            var lengthHeader = wc.ResponseHeaders[HttpResponseHeader.ContentLength];
            long size;
            if (long.TryParse(lengthHeader, NumberStyles.None, CultureInfo.InvariantCulture, out size)
                && size > 512 * 1024)
            {
                var msg = string.Format(
                    "The downloaded resource is rather large ({0} bytes). Proceed anyway?",
                    size.ToString("N0"));

                if (DialogResult.Yes != MessageBox.Show(this, msg, "Large Content", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    return;
            }

            //
            // Load 'er up!
            //

            using (CurrentCursorScope.EnterWait())
            {
                var document = new HtmlDocument();
                document.LoadHtml(content);
                Open(document);
            }

            _lastKnownGoodImportedUrl = url;

        }

        private void ImportFromWebMenu_Click(object sender, EventArgs args)
        {
            Uri url = null;

            var input = _lastKnownGoodImportedUrl != null
                      ? _lastKnownGoodImportedUrl.ToString()
                      : string.Empty;

            do
            {
                input = Interaction.InputBox("Enter URL:", "Import From Web", input,
                    (int)(Location.X + (Size.Height / 10f)),
                    (int)(Location.Y + (Size.Height / 10f))).Trim();

                if (string.IsNullOrEmpty(input))
                    return;

                //
                // If some entered just the DNS name to get the home page 
                // then we prepend "http://" for the user to prevent typing.
                //
                // http://www.shauninman.com/archive/2006/05/08/validating_domain_names
                //

                if (Regex.IsMatch(input, @"^([a-z0-9] ([-a-z0-9]*[a-z0-9])? \.)+ 
                                            ( (a[cdefgilmnoqrstuwxz]|aero|arpa)
                                              |(b[abdefghijmnorstvwyz]|biz)
                                              |(c[acdfghiklmnorsuvxyz]|cat|com|coop)
                                              |d[ejkmoz]
                                              |(e[ceghrstu]|edu)
                                              |f[ijkmor]
                                              |(g[abdefghilmnpqrstuwy]|gov)
                                              |h[kmnrtu]
                                              |(i[delmnoqrst]|info|int)
                                              |(j[emop]|jobs)
                                              |k[eghimnprwyz]
                                              |l[abcikrstuvy]
                                              |(m[acdghklmnopqrstuvwxyz]|mil|mobi|museum)
                                              |(n[acefgilopruz]|name|net)
                                              |(om|org)
                                              |(p[aefghklmnrstwy]|pro)
                                              |qa
                                              |r[eouw]
                                              |s[abcdeghijklmnortvyz]
                                              |(t[cdfghjklmnoprtvwz]|travel)
                                              |u[agkmsyz]
                                              |v[aceginu]
                                              |w[fs]
                                              |y[etu]
                                              |z[amw])", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture))
                {
                    input = "http://" + input;
                }

                if (!Uri.IsWellFormedUriString(input, UriKind.Absolute))
                    MessageBox.Show(this, "The entered URL does not appear to be correctly formatted.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    url = new Uri(input, UriKind.Absolute);
            }
            while (url == null);
            ImportFromWeb(url);
        }


        private static string RemoveClipboardMetadata(string html)
        {
            var header = Regex.Match(html, @"^(\w+:[^\n]*\n)*", RegexOptions.Singleline);
            return html.Substring(header.Value.Length);
        }


        private void cmdPasteFromClipboard_Click(object sender, EventArgs e)
        {
            PasteHtml(Clipboard.GetText(TextDataFormat.Html));
        }

        private void PasteHtml(string html)
        {
            var document = new HtmlDocument();
            if (!string.IsNullOrEmpty(html)) document.LoadHtml(RemoveClipboardMetadata(html));
            else document.LoadHtml(Clipboard.GetText());
            Open(document);
        }

        private void SelectorBox_TextChanged(object sender, EventArgs e)
        {
            Evaluate();
        }


        private static string FormatHtml(ref HtmlDocument document)
        {
            document.OptionOutputAsXml = true;
            var str = document.DocumentNode.WriteTo().Replace("\r", "");
            document.OptionOutputAsXml = false;
            try
            {
                var x = XDocument.Parse(str);
                var formatted = x.ToString(SaveOptions.None).Replace("\r", "");
                document = new HtmlDocument();
                document.LoadHtml(formatted);
                return formatted;
            }
            catch (XmlException)
            {
                return RemoveCarriageReturns(ref document);
            }
        }

        private static string RemoveCarriageReturns(ref HtmlDocument document)
        {
            var str = document.DocumentNode.WriteTo().Replace("\r", "");
            document = new HtmlDocument();
            document.LoadHtml(str);
            return str;
        }


        private void UpdateHtml()
        {
            _document = _originalDocument;
            _documentBox.Clear();
            var html = ShouldFormatHtml ? FormatHtml(ref _document) : RemoveCarriageReturns(ref _document);

            _documentBox.Text = html;

            _oldSelection = null;
            _selectorMatches = null;
            HighlightMarkup(_documentBox, Color.Blue, Color.FromArgb(163, 21, 21), Color.Red);
            Evaluate();
        }


        private void Open(HtmlDocument document)
        {
            _originalDocument = document;
            UpdateHtml();
        }

        private static void HighlightMarkup(RichTextBox rtb, Color tagColor, Color tagNameColor, Color attributeNameColor)
        {
            Debug.Assert(rtb != null);
            rtb.Visible = false;
            var stopwatch = Stopwatch.StartNew();
            foreach (Match tag in _tagExpression.Matches(rtb.Text))
            {
                Highlight(rtb, tag.Index, tag.Length, tagColor, null, null);

                var name = tag.Groups["t"];
                Highlight(rtb, name.Index, name.Length, tagNameColor, null, null);

                var attributes = Regex.Matches(tag.Value,
                    @"\b([a-z]+)\s*=\s*(?:""[^""]*""|'[^']*'|[^'"">\s]+)",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Singleline
                    | RegexOptions.CultureInvariant);

                foreach (var attribute in attributes.Cast<Match>().Select(m => m.Groups[1]))
                    Highlight(rtb, tag.Index + attribute.Index, attribute.Length, attributeNameColor, null, null);


                if (stopwatch.ElapsedMilliseconds > 5000) break;
            }
            rtb.Visible = true;
        }

        private static void Highlight(RichTextBox rtb, IEnumerable<Match> matches, Color? color, Color? backColor, Font font)
        {
            foreach (var match in matches)
                Highlight(rtb, match.Index, match.Length, color, backColor, font);
        }

        private static void Highlight(RichTextBox rtb, IEnumerable<TagMatch> matches, Color? color, Color? backColor, Font font)
        {
            foreach (var match in matches)
                Highlight(rtb, match.StartIndex, match.TagLength, color, backColor, font);
        }

        private static void Highlight(RichTextBox rtb, int start, int length, Color? color, Color? backColor, Font font)
        {
            if (start == -1) return;
            rtb.SelectionStart = start;
            rtb.SelectionLength = length;
            if (color != null) rtb.SelectionColor = color.Value;
            if (backColor != null) rtb.SelectionBackColor = backColor.Value;
            if (font != null) rtb.SelectionFont = font;
        }

        private void Evaluate()
        {
            _selectorMatches = Evaluate(_document, _selectorBox, _matchBox, errorText, _statusLabel, _selectorMatches, _documentBox);
        }

        private TagMatch[] Evaluate(HtmlDocument document, Control tb, ListBox lb, ToolStripStatusLabel hb, ToolStripItem status, IEnumerable<TagMatch> oldMatches, RichTextBox rtb)
        {
            var input = tb.Text.Trim();
            tb.ForeColor = SystemColors.WindowText;

            var elements = new HtmlNode[0];

            if (string.IsNullOrEmpty(input))
            {
                status.Text = "Ready";
                hb.Text = null;
            }
            else
            {
                try
                {
                    //
                    // Simple way to query for elements:
                    //
                    // nodes = document.DocumentNode.QuerySelectorAll(input).ToArray();
                    //
                    // However, we want to generate the human readable text and
                    // the element selector in a single pass so go the bare metal way 
                    // here to make all the parties to talk to each other.
                    //

                    var generator = new SelectorGenerator<HtmlNode>(new HtmlNodeOps());
                    Parser.Parse(input, generator);
                    if (document != null)
                        elements = generator.Selector(Enumerable.Repeat(document.DocumentNode, 1)).ToArray();
                    hb.Text = null;
                    status.Text = "Matches: " + elements.Length.ToString("N0");
                }
                catch (FormatException e)
                {
                    tb.ForeColor = Color.FromKnownColor(KnownColor.Red);
                    status.Text = "Error: " + e.Message;
                    hb.Text = "Oops! " + e.Message;
                }
            }

            ClearOldSelection();

            if (oldMatches != null)
                Highlight(rtb, oldMatches, null, SystemColors.Info, null);


            lb.BeginUpdate();
            try
            {
                lb.Items.Clear();
                if (!elements.Any())
                    return new TagMatch[0];

                var html = rtb.Text;
                var matches = new List<TagMatch>(elements.Length);
                foreach (var element in elements)
                {
                    var match = new TagMatch(element);
                    matches.Add(match);
                }

                Highlight(rtb, matches, null, Color.Yellow, null);

                lb.Items.AddRange(elements.Select(n => n.GetBeginTagString()).ToArray());

                return matches.ToArray();
            }
            finally
            {
                lb.EndUpdate();
            }
        }

        private void fizzlerExWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://fizzlerex.codeplex.com/");
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            cmdPasteFromClipboard.Enabled = Clipboard.ContainsText();
        }

        private void chkFormatHtml_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHtml();
        }

        private bool ShouldFormatHtml
        {
            get
            {
                return chkFormatHtml.Checked;
            }
        }

        private void _matchBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            ClearOldSelection();
            var selected = _matchBox.SelectedIndex;
            if (selected != -1)
            {
                var match = _selectorMatches[selected];
                var line = _documentBox.GetLineFromCharIndex(match.StartIndex);
                var caret = _documentBox.GetFirstCharIndexFromLine(Math.Max(0, line - 4));
                _documentBox.SelectionStart = caret;
                _documentBox.ScrollToCaret();
                Highlight(_documentBox, match.StartIndex, match.FullLength, null, Color.LawnGreen, null);
                _oldSelection = match;
            }
        }

        private void ClearOldSelection()
        {
            if (_oldSelection != null)
            {
                Highlight(_documentBox, _oldSelection.StartIndex, _oldSelection.FullLength, null, SystemColors.Info, null); ;
                Highlight(_documentBox, _oldSelection.StartIndex, _oldSelection.TagLength, null, Color.Yellow, null); ;
                _oldSelection = null;
            }
        }

        private TagMatch _oldSelection;

        private void _selectorBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                _matchBox.SelectedIndex = _matchBox.Items.Count == 0 ? -1 : 0;
                _matchBox_SelectedIndexChanged(sender, EventArgs.Empty);
                _matchBox.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
            }
        }

        private void _matchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && _matchBox.SelectedIndex <= 0)
            {
                e.Handled = true;
                _matchBox.SelectedIndex = -1;
                _selectorBox.Focus();
            }
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.IsInputKey = true;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.V)
            {
                var text = Clipboard.GetText(TextDataFormat.UnicodeText).Trim();
                var html = Regex.Match(Clipboard.GetText(TextDataFormat.Html), @"<!--StartFragment-->(.*)<!--EndFragment-->", RegexOptions.Singleline).Groups[1].Value;

                if (text.StartsWith("http:") || text.StartsWith("https:"))
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    ImportFromWeb(new Uri(text));
                }
                else if (text.StartsWith("<"))
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    PasteHtml(text);
                }
                else if (html.Contains("<div") || html.Contains("<td") || html.Length > 2048)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    PasteHtml(html);
                }
            }

        }

    }
}
