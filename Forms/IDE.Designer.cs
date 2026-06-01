namespace Decorator.Forms
{
    partial class IDE
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDE));
            fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            menuStrip2 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            abrirArchivoToolStripMenuItem = new ToolStripMenuItem();
            abrirCarpetaToolStripMenuItem = new ToolStripMenuItem();
            verToolStripMenuItem = new ToolStripMenuItem();
            terminalToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            treeView1 = new TreeView();
            ((System.ComponentModel.ISupportInitialize)fastColoredTextBox1).BeginInit();
            menuStrip2.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // fastColoredTextBox1
            // 
            fastColoredTextBox1.AutoCompleteBracketsList = new char[]
    {
    '(',
    ')',
    '{',
    '}',
    '[',
    ']',
    '"',
    '"',
    '\'',
    '\''
    };
            fastColoredTextBox1.AutoScrollMinSize = new Size(27, 14);
            fastColoredTextBox1.BackBrush = null;
            fastColoredTextBox1.BackColor = Color.SlateBlue;
            fastColoredTextBox1.CharHeight = 14;
            fastColoredTextBox1.CharWidth = 8;
            fastColoredTextBox1.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            fastColoredTextBox1.Dock = DockStyle.Fill;
            fastColoredTextBox1.Font = new Font("Courier New", 9.75F);
            fastColoredTextBox1.ForeColor = SystemColors.ControlText;
            fastColoredTextBox1.IsReplaceMode = false;
            fastColoredTextBox1.Location = new Point(0, 0);
            fastColoredTextBox1.Name = "fastColoredTextBox1";
            fastColoredTextBox1.Paddings = new Padding(0);
            fastColoredTextBox1.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            fastColoredTextBox1.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fastColoredTextBox1.ServiceColors");
            fastColoredTextBox1.Size = new Size(839, 401);
            fastColoredTextBox1.TabIndex = 1;
            fastColoredTextBox1.Zoom = 100;
            fastColoredTextBox1.Load += fastColoredTextBox1_Load;
            // 
            // menuStrip2
            // 
            menuStrip2.AutoSize = false;
            menuStrip2.BackColor = Color.DarkSlateBlue;
            menuStrip2.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, verToolStripMenuItem, terminalToolStripMenuItem, toolStripMenuItem1 });
            menuStrip2.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(1084, 24);
            menuStrip2.Stretch = false;
            menuStrip2.TabIndex = 3;
            menuStrip2.Text = "menuStrip2";
            menuStrip2.ItemClicked += menuStrip2_ItemClicked;
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { abrirArchivoToolStripMenuItem, abrirCarpetaToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(60, 20);
            archivoToolStripMenuItem.Text = "Archivo";
            archivoToolStripMenuItem.Click += archivoToolStripMenuItem_Click;
            // 
            // abrirArchivoToolStripMenuItem
            // 
            abrirArchivoToolStripMenuItem.Name = "abrirArchivoToolStripMenuItem";
            abrirArchivoToolStripMenuItem.Size = new Size(180, 22);
            abrirArchivoToolStripMenuItem.Text = "Abrir Archivo";
            abrirArchivoToolStripMenuItem.Click += abrirArchivoToolStripMenuItem_Click;
            // 
            // abrirCarpetaToolStripMenuItem
            // 
            abrirCarpetaToolStripMenuItem.Name = "abrirCarpetaToolStripMenuItem";
            abrirCarpetaToolStripMenuItem.Size = new Size(180, 22);
            abrirCarpetaToolStripMenuItem.Text = "Abrir Carpeta";
            abrirCarpetaToolStripMenuItem.Click += abrirCarpetaToolStripMenuItem_Click;
            // 
            // verToolStripMenuItem
            // 
            verToolStripMenuItem.Name = "verToolStripMenuItem";
            verToolStripMenuItem.Size = new Size(35, 20);
            verToolStripMenuItem.Text = "Ver";
            // 
            // terminalToolStripMenuItem
            // 
            terminalToolStripMenuItem.Name = "terminalToolStripMenuItem";
            terminalToolStripMenuItem.Size = new Size(64, 20);
            terminalToolStripMenuItem.Text = "Terminal";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(12, 20);
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.Transparent;
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripLabel1 });
            toolStrip1.Location = new Point(188, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(87, 25);
            toolStrip1.TabIndex = 4;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.BackColor = Color.IndianRed;
            toolStripLabel1.ImageTransparentColor = Color.IndianRed;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(29, 22);
            toolStripLabel1.Text = "Salir";
            toolStripLabel1.Click += toolStripLabel1_Click_1;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.MenuText;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.ForeColor = SystemColors.Window;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(839, 162);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = ">>> ";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(550, 5);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 6;
            label1.Text = "Default1.py";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(fastColoredTextBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(richTextBox1);
            splitContainer1.Size = new Size(839, 567);
            splitContainer1.SplitterDistance = 401;
            splitContainer1.TabIndex = 7;
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 24);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(splitContainer1);
            splitContainer2.Size = new Size(1084, 567);
            splitContainer2.SplitterDistance = 241;
            splitContainer2.TabIndex = 8;
            // 
            // treeView1
            // 
            treeView1.BackColor = Color.Lavender;
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(241, 567);
            treeView1.TabIndex = 0;
            treeView1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;
            // 
            // IDE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(1084, 591);
            Controls.Add(splitContainer2);
            Controls.Add(label1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip2);
            Name = "IDE";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)fastColoredTextBox1).EndInit();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem verToolStripMenuItem;
        private ToolStripMenuItem terminalToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private RichTextBox richTextBox1;
        private ToolStripMenuItem abrirArchivoToolStripMenuItem;
        private Label label1;
        private ToolStripMenuItem toolStripMenuItem1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TreeView treeView1;
        private ToolStripMenuItem abrirCarpetaToolStripMenuItem;
        private ToolStripLabel toolStripLabel1;
    }
}