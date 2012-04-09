namespace VisualFizzler
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdPasteFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.chkFormatHtml = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fizzlerExWebSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._selectorBox = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._matchBox = new System.Windows.Forms.ListBox();
            this._documentBox = new System.Windows.Forms.RichTextBox();
            this._status = new System.Windows.Forms.StatusStrip();
            this.errorText = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this._status.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem1.Text = "&Import from web...";
            toolStripMenuItem1.Click += new System.EventHandler(this.ImportFromWebMenu_Click);
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this._menuStrip.Size = new System.Drawing.Size(658, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            toolStripMenuItem1,
            this.cmdPasteFromClipboard,
            this.toolStripSeparator1,
            this.chkFormatHtml,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // cmdPasteFromClipboard
            // 
            this.cmdPasteFromClipboard.Image = ((System.Drawing.Image)(resources.GetObject("cmdPasteFromClipboard.Image")));
            this.cmdPasteFromClipboard.Name = "cmdPasteFromClipboard";
            this.cmdPasteFromClipboard.Size = new System.Drawing.Size(184, 22);
            this.cmdPasteFromClipboard.Text = "Paste from clipboard";
            this.cmdPasteFromClipboard.Click += new System.EventHandler(this.cmdPasteFromClipboard_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // chkFormatHtml
            // 
            this.chkFormatHtml.Checked = true;
            this.chkFormatHtml.CheckOnClick = true;
            this.chkFormatHtml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFormatHtml.Name = "chkFormatHtml";
            this.chkFormatHtml.Size = new System.Drawing.Size(184, 22);
            this.chkFormatHtml.Text = "Format HTML";
            this.chkFormatHtml.CheckedChanged += new System.EventHandler(this.chkFormatHtml_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.FileExit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fizzlerExWebSiteToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // fizzlerExWebSiteToolStripMenuItem
            // 
            this.fizzlerExWebSiteToolStripMenuItem.Name = "fizzlerExWebSiteToolStripMenuItem";
            this.fizzlerExWebSiteToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.fizzlerExWebSiteToolStripMenuItem.Text = "FizzlerEx web site";
            this.fizzlerExWebSiteToolStripMenuItem.Click += new System.EventHandler(this.fizzlerExWebSiteToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._selectorBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(658, 437);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // _selectorBox
            // 
            this._selectorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectorBox.Location = new System.Drawing.Point(0, 0);
            this._selectorBox.Margin = new System.Windows.Forms.Padding(2);
            this._selectorBox.Name = "_selectorBox";
            this._selectorBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._selectorBox.Size = new System.Drawing.Size(658, 20);
            this._selectorBox.TabIndex = 1;
            this._toolTip.SetToolTip(this._selectorBox, "Enter CSS selector");
            this._selectorBox.TextChanged += new System.EventHandler(this.SelectorBox_TextChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._matchBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._documentBox);
            this.splitContainer2.Size = new System.Drawing.Size(658, 409);
            this.splitContainer2.SplitterDistance = 250;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // _matchBox
            // 
            this._matchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._matchBox.FormattingEnabled = true;
            this._matchBox.Location = new System.Drawing.Point(0, 0);
            this._matchBox.Margin = new System.Windows.Forms.Padding(2);
            this._matchBox.Name = "_matchBox";
            this._matchBox.Size = new System.Drawing.Size(250, 409);
            this._matchBox.TabIndex = 0;
            this._matchBox.SelectedIndexChanged += new System.EventHandler(this._matchBox_SelectedIndexChanged);
            // 
            // _documentBox
            // 
            this._documentBox.BackColor = System.Drawing.SystemColors.Info;
            this._documentBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._documentBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._documentBox.Location = new System.Drawing.Point(0, 0);
            this._documentBox.Margin = new System.Windows.Forms.Padding(2);
            this._documentBox.Name = "_documentBox";
            this._documentBox.ReadOnly = true;
            this._documentBox.Size = new System.Drawing.Size(405, 409);
            this._documentBox.TabIndex = 4;
            this._documentBox.Text = "";
            this._documentBox.WordWrap = false;
            // 
            // _status
            // 
            this._status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorText,
            this._statusLabel});
            this._status.Location = new System.Drawing.Point(0, 461);
            this._status.Name = "_status";
            this._status.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this._status.Size = new System.Drawing.Size(658, 22);
            this._status.TabIndex = 1;
            // 
            // errorText
            // 
            this.errorText.Name = "errorText";
            this.errorText.Size = new System.Drawing.Size(0, 17);
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(647, 17);
            this._statusLabel.Spring = true;
            this._statusLabel.Text = "Ready";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _openFileDialog
            // 
            this._openFileDialog.Filter = "HTML Files|*.htm;*.html|All Files|*.*";
            this._openFileDialog.Title = "Open File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 483);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._status);
            this.Controls.Add(this._menuStrip);
            this.MainMenuStrip = this._menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Visual Fizzler (FizzlerEx)";
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this._status.ResumeLayout(false);
            this._status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip _status;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox _matchBox;
        private System.Windows.Forms.RichTextBox _documentBox;
        private System.Windows.Forms.TextBox _selectorBox;
        private System.Windows.Forms.ToolStripMenuItem fizzlerExWebSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel errorText;
        private System.Windows.Forms.ToolStripMenuItem cmdPasteFromClipboard;
        private System.Windows.Forms.ToolStripMenuItem chkFormatHtml;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

