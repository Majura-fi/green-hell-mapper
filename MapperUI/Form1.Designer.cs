namespace MapperUI;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        richTextBox1 = new RichTextBox();
        splitContainer1 = new SplitContainer();
        mapControl1 = new MapControl();
        tableLayoutPanel1 = new TableLayoutPanel();
        label5 = new Label();
        pointSizeInput = new NumericUpDown();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        loadCoordinatesToolStripMenuItem = new ToolStripMenuItem();
        quitToolStripMenuItem = new ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pointSizeInput).BeginInit();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // richTextBox1
        // 
        tableLayoutPanel1.SetColumnSpan(richTextBox1, 2);
        richTextBox1.DetectUrls = false;
        richTextBox1.Dock = DockStyle.Fill;
        richTextBox1.Location = new Point(3, 31);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.ReadOnly = true;
        richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
        richTextBox1.Size = new Size(500, 752);
        richTextBox1.TabIndex = 1;
        richTextBox1.Text = "";
        // 
        // splitContainer1
        // 
        splitContainer1.BorderStyle = BorderStyle.Fixed3D;
        splitContainer1.Dock = DockStyle.Fill;
        splitContainer1.Location = new Point(0, 24);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(mapControl1);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
        splitContainer1.Size = new Size(1263, 790);
        splitContainer1.SplitterDistance = 749;
        splitContainer1.TabIndex = 2;
        // 
        // mapControl1
        // 
        mapControl1.Dock = DockStyle.Fill;
        mapControl1.DrawCurrentLocations = true;
        mapControl1.Location = new Point(0, 0);
        mapControl1.Name = "mapControl1";
        mapControl1.PointSize = 3;
        mapControl1.Size = new Size(745, 786);
        mapControl1.TabIndex = 0;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(richTextBox1, 0, 1);
        tableLayoutPanel1.Controls.Add(label5, 0, 0);
        tableLayoutPanel1.Controls.Add(pointSizeInput, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(506, 786);
        tableLayoutPanel1.TabIndex = 10;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(3, 0);
        label5.Name = "label5";
        label5.Size = new Size(57, 15);
        label5.TabIndex = 10;
        label5.Text = "Point size";
        // 
        // pointSizeInput
        // 
        pointSizeInput.Location = new Point(256, 3);
        pointSizeInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        pointSizeInput.Name = "pointSizeInput";
        pointSizeInput.Size = new Size(120, 23);
        pointSizeInput.TabIndex = 11;
        pointSizeInput.Value = new decimal(new int[] { 3, 0, 0, 0 });
        pointSizeInput.ValueChanged += PointSizeInput_ValueChanged;
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1263, 24);
        menuStrip1.TabIndex = 1;
        menuStrip1.Text = "menuStrip1";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadCoordinatesToolStripMenuItem, quitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "File";
        // 
        // loadCoordinatesToolStripMenuItem
        // 
        loadCoordinatesToolStripMenuItem.Name = "loadCoordinatesToolStripMenuItem";
        loadCoordinatesToolStripMenuItem.Size = new Size(165, 22);
        loadCoordinatesToolStripMenuItem.Text = "Load coordinates";
        loadCoordinatesToolStripMenuItem.Click += LoadCoordinatesMenuClick;
        // 
        // quitToolStripMenuItem
        // 
        quitToolStripMenuItem.Name = "quitToolStripMenuItem";
        quitToolStripMenuItem.Size = new Size(165, 22);
        quitToolStripMenuItem.Text = "Quit";
        quitToolStripMenuItem.Click += QuitMenuClick;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1263, 814);
        Controls.Add(splitContainer1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Green Hell Map";
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pointSizeInput).EndInit();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private RichTextBox richTextBox1;
    private SplitContainer splitContainer1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem loadCoordinatesToolStripMenuItem;
    private ToolStripMenuItem quitToolStripMenuItem;
    private TableLayoutPanel tableLayoutPanel1;
    private Label label5;
    private NumericUpDown pointSizeInput;
    private MapControl mapControl1;
}
