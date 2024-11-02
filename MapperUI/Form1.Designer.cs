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
        mapControl1 = new MapControl();
        menuStrip1 = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        loadCoordinatesToolStripMenuItem = new ToolStripMenuItem();
        showLogToolStripMenuItem = new ToolStripMenuItem();
        settingsToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        quitToolStripMenuItem = new ToolStripMenuItem();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // mapControl1
        // 
        mapControl1.Dock = DockStyle.Fill;
        mapControl1.DrawCurrentLocations = true;
        mapControl1.Location = new Point(0, 24);
        mapControl1.Name = "mapControl1";
        mapControl1.PointSize = 3;
        mapControl1.Size = new Size(1263, 790);
        mapControl1.TabIndex = 0;
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
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadCoordinatesToolStripMenuItem, showLogToolStripMenuItem, settingsToolStripMenuItem, toolStripSeparator1, quitToolStripMenuItem });
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
        // showLogToolStripMenuItem
        // 
        showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
        showLogToolStripMenuItem.Size = new Size(165, 22);
        showLogToolStripMenuItem.Text = "Show Log";
        showLogToolStripMenuItem.Click += ShowLogsToolStripMenuItem_Click;
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        settingsToolStripMenuItem.Size = new Size(165, 22);
        settingsToolStripMenuItem.Text = "Settings";
        settingsToolStripMenuItem.Click += SettingsToolStripMenuItem_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(162, 6);
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
        Controls.Add(mapControl1);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Green Hell Map";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem loadCoordinatesToolStripMenuItem;
    private ToolStripMenuItem quitToolStripMenuItem;
    private MapControl mapControl1;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem showLogToolStripMenuItem;
}
