namespace MapperUI;

partial class LogsForm
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
        listView1 = new ListView();
        colEvents = new ColumnHeader();
        menuStrip1 = new MenuStrip();
        clearLogToolStripMenuItem = new ToolStripMenuItem();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // listView1
        // 
        listView1.Columns.AddRange(new ColumnHeader[] { colEvents });
        listView1.Dock = DockStyle.Fill;
        listView1.Location = new Point(0, 24);
        listView1.Name = "listView1";
        listView1.Size = new Size(800, 426);
        listView1.TabIndex = 1;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = View.Details;
        listView1.VirtualMode = true;
        // 
        // colEvents
        // 
        colEvents.Text = "Events";
        colEvents.Width = 1000;
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { clearLogToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 2;
        menuStrip1.Text = "menuStrip1";
        // 
        // clearLogToolStripMenuItem
        // 
        clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
        clearLogToolStripMenuItem.Size = new Size(66, 20);
        clearLogToolStripMenuItem.Text = "Clear log";
        clearLogToolStripMenuItem.Click += ClearLogToolStripMenuItem_Click;
        // 
        // LogsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(listView1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "LogsForm";
        Text = "CoordinatesLogForm";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ListView listView1;
    private ColumnHeader colEvents;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem clearLogToolStripMenuItem;
}