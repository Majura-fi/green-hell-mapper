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
        pictureBox1 = new PictureBox();
        richTextBox1 = new RichTextBox();
        splitContainer1 = new SplitContainer();
        label4 = new Label();
        factorInputY = new NumericUpDown();
        label3 = new Label();
        label2 = new Label();
        label1 = new Label();
        factorInputX = new NumericUpDown();
        offsetInputY = new NumericUpDown();
        offsetInputX = new NumericUpDown();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        loadCoordinatesToolStripMenuItem = new ToolStripMenuItem();
        quitToolStripMenuItem = new ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)factorInputY).BeginInit();
        ((System.ComponentModel.ISupportInitialize)factorInputX).BeginInit();
        ((System.ComponentModel.ISupportInitialize)offsetInputY).BeginInit();
        ((System.ComponentModel.ISupportInitialize)offsetInputX).BeginInit();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.Dock = DockStyle.Fill;
        pictureBox1.Image = Properties.Resources.map;
        pictureBox1.Location = new Point(0, 0);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(745, 786);
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // richTextBox1
        // 
        richTextBox1.DetectUrls = false;
        richTextBox1.Location = new Point(0, 210);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.ReadOnly = true;
        richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
        richTextBox1.Size = new Size(506, 576);
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
        splitContainer1.Panel1.Controls.Add(pictureBox1);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(label4);
        splitContainer1.Panel2.Controls.Add(factorInputY);
        splitContainer1.Panel2.Controls.Add(label3);
        splitContainer1.Panel2.Controls.Add(label2);
        splitContainer1.Panel2.Controls.Add(label1);
        splitContainer1.Panel2.Controls.Add(factorInputX);
        splitContainer1.Panel2.Controls.Add(offsetInputY);
        splitContainer1.Panel2.Controls.Add(offsetInputX);
        splitContainer1.Panel2.Controls.Add(richTextBox1);
        splitContainer1.Size = new Size(1263, 790);
        splitContainer1.SplitterDistance = 749;
        splitContainer1.TabIndex = 2;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(129, 59);
        label4.Name = "label4";
        label4.Size = new Size(50, 15);
        label4.TabIndex = 9;
        label4.Text = "Factor Y";
        // 
        // factorInputY
        // 
        factorInputY.DecimalPlaces = 2;
        factorInputY.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        factorInputY.Location = new Point(129, 77);
        factorInputY.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        factorInputY.Name = "factorInputY";
        factorInputY.Size = new Size(120, 23);
        factorInputY.TabIndex = 8;
        factorInputY.Value = new decimal(new int[] { 274, 0, 0, -2147352576 });
        factorInputY.ValueChanged += TweakValueChanged;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(3, 59);
        label3.Name = "label3";
        label3.Size = new Size(50, 15);
        label3.TabIndex = 7;
        label3.Text = "Factor X";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(129, 0);
        label2.Name = "label2";
        label2.Size = new Size(14, 15);
        label2.TabIndex = 6;
        label2.Text = "Y";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 0);
        label1.Name = "label1";
        label1.Size = new Size(14, 15);
        label1.TabIndex = 5;
        label1.Text = "X";
        // 
        // factorInputX
        // 
        factorInputX.DecimalPlaces = 2;
        factorInputX.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
        factorInputX.Location = new Point(3, 77);
        factorInputX.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
        factorInputX.Name = "factorInputX";
        factorInputX.Size = new Size(120, 23);
        factorInputX.TabIndex = 4;
        factorInputX.Value = new decimal(new int[] { 253, 0, 0, 131072 });
        factorInputX.ValueChanged += TweakValueChanged;
        // 
        // offsetInputY
        // 
        offsetInputY.Location = new Point(129, 18);
        offsetInputY.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        offsetInputY.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
        offsetInputY.Name = "offsetInputY";
        offsetInputY.Size = new Size(120, 23);
        offsetInputY.TabIndex = 3;
        offsetInputY.Value = new decimal(new int[] { 5310, 0, 0, 0 });
        offsetInputY.ValueChanged += TweakValueChanged;
        // 
        // offsetInputX
        // 
        offsetInputX.Location = new Point(3, 18);
        offsetInputX.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        offsetInputX.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
        offsetInputX.Name = "offsetInputX";
        offsetInputX.Size = new Size(120, 23);
        offsetInputX.TabIndex = 2;
        offsetInputX.Value = new decimal(new int[] { 108, 0, 0, int.MinValue });
        offsetInputX.ValueChanged += TweakValueChanged;
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
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        splitContainer1.Panel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)factorInputY).EndInit();
        ((System.ComponentModel.ISupportInitialize)factorInputX).EndInit();
        ((System.ComponentModel.ISupportInitialize)offsetInputY).EndInit();
        ((System.ComponentModel.ISupportInitialize)offsetInputX).EndInit();
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private RichTextBox richTextBox1;
    private SplitContainer splitContainer1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem loadCoordinatesToolStripMenuItem;
    private ToolStripMenuItem quitToolStripMenuItem;
    private Label label3;
    private Label label2;
    private Label label1;
    private NumericUpDown factorInputX;
    private NumericUpDown offsetInputY;
    private NumericUpDown offsetInputX;
    private Label label4;
    private NumericUpDown factorInputY;
}
