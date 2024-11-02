using MapperUI.Properties;

namespace MapperUI;

internal static class Program
{
    public static MapperSettings? Settings { get; set; }

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        LoadSettings();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }

    private static void LoadSettings()
    {
        do
        {
            try
            {
                string settingsPath = ".\\mapper-settings.json";
                Settings = File.Exists(settingsPath) ? MapperSettings.Load(settingsPath) : new();
            }
            catch (Exception ex)
            {
                string caption = "Failed to load settings!";
                string message = $"Failed to parse settings file due an error: {ex.Message}\n" +
                    $"Try to load settings again? You can also ignore this error and use default settings.";

                var result = MessageBox.Show(message, caption, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);

                switch (result)
                {
                    // Use default settings.
                    case DialogResult.Ignore:
                        Settings = new();
                        break;

                    // Stop the program.
                    case DialogResult.Abort:
                        return;

                    // Loop and attempt to reload settings.
                    case DialogResult.Retry:
                        break;
                }
            }
        } while (Settings == null);
    }
}