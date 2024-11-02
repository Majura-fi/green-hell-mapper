using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using MapperUI.Properties;
using Newtonsoft.Json;

namespace MapperUI;

public partial class Form1 : Form
{
    private readonly LocationServer locationServer = new();
    private SettingsForm? settingsForm;
    private LogsForm? logsForm;

    public Form1()
    {
        InitializeComponent();
        locationServer.LocationReceived += OnLocationReceived;
        locationServer.LogProduced += OnLogProduced;
        locationServer.Start();

        FormClosing += (_, _) => {
            locationServer.Stop();
            logsForm?.Close();
            settingsForm?.Close();
        };

        //LoadPreviousCoordinates();
    }

    private void OnLogProduced(object? sender, string e)
    {
        SafeInvoke(() => logsForm?.AppendLog(e));
    }

    private void OnLocationReceived(object? sender, PlayerInfo e)
    {
        SafeInvoke(() => { mapControl1.DrawMark(e, true); });
    }

    private void LoadCoordinatesFromFile(string path)
    {
        using FileStream fs = new(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using StreamReader sr = new(fs);

        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            try
            {
                PlayerInfo? info = JsonConvert.DeserializeObject<PlayerInfo>(line);

                if (info == null)
                {
                    logsForm?.AppendLog($"Failed to parse json: \"{line}\"");
                    continue;
                }

                locationServer.AddLocation(info.Value);
            }
            catch (Exception ex)
            {
                logsForm?.AppendLog($"[{path}] {ex.Message} - Line: \"{line}\"");
            }
        }

        logsForm?.AppendLog($"Imported {path}");
    }

    private void SafeInvoke(Action action)
    {
        try
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        catch (Exception)
        {
        }
    }

    private void LoadPreviousCoordinates()
    {
        DirectoryInfo srcDir = new(@"C:\Program Files (x86)\Steam\steamapps\common\Green Hell\SavedCoordinates");
        IEnumerable<FileInfo> files = srcDir.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);

        foreach (FileInfo file in files)
        {
            LoadCoordinatesFromFile(file.FullName);
        }
    }

    private void LoadCoordinatesMenuClick(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new() {
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = true
        };

        if (
            !string.IsNullOrWhiteSpace(Settings.Default.RecentCoordinatesListPath)
            && Directory.Exists(Settings.Default.RecentCoordinatesListPath)
        )
        {
            dialog.InitialDirectory = Settings.Default.RecentCoordinatesListPath;
        }

        if (dialog.ShowDialog() != DialogResult.OK || dialog.FileNames.Length == 0)
        {
            return;
        }

        Settings.Default.RecentCoordinatesListPath = Path.GetDirectoryName(dialog.FileNames[0]);
        Settings.Default.Save();

        foreach (string path in dialog.FileNames)
        {
            LoadCoordinatesFromFile(path);
        }
    }

    private void QuitMenuClick(object sender, EventArgs e)
    {
        Close();
    }

    private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        settingsForm ??= new(Program.Settings!);
        settingsForm.FormClosing += SettingsForm_FormClosing;
        settingsForm.Show();
    }

    private void SettingsForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        settingsForm!.FormClosing -= SettingsForm_FormClosing;
        settingsForm = null;
    }

    private void ShowLogsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        logsForm ??= new();
        logsForm.FormClosing += LogsForm_FormClosing;
        logsForm.Show();
    }

    private void LogsForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        logsForm!.FormClosing -= LogsForm_FormClosing;
        logsForm = null;
    }
}
