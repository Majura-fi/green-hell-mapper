using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapperUI;
public partial class LogsForm : Form
{
    readonly List<string> logs = [];

    public LogsForm()
    {
        InitializeComponent();
        listView1.RetrieveVirtualItem += (_, e) => {
            e.Item = e.ItemIndex < logs.Count ? new(logs[e.ItemIndex]) : null;
        };
    }

    public bool CollectLogs { get; internal set; }

    public void AppendLog(string message)
    {
        logs.Add(message);
        listView1.VirtualListSize = logs.Count;
        listView1.Invalidate();
    }

    private void ClearLogToolStripMenuItem_Click(object sender, EventArgs e)
    {
        logs.Clear();
        listView1.VirtualListSize = logs.Count;
        listView1.Invalidate();
    }
}
