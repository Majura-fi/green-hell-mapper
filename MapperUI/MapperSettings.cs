using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapperUI;
public class MapperSettings
{
    private string settingsPath = ".\\mapper-settings";

    /// <summary>
    /// How wide path is being drawn on the map.
    /// </summary>
    public float PathWidth { get; set; } = 3f;

    public static MapperSettings Load(string path)
    {
        string content = File.ReadAllText(path, Encoding.UTF8);
        MapperSettings? settings = JsonConvert.DeserializeObject<MapperSettings>(content)
            ?? throw new JsonReaderException("Deserialization returned null.");
        settings.settingsPath = path;

        return settings;
    }

    public void Save()
    {
        Save(settingsPath);
    }

    public void Save(string path)
    {
        string content = JsonConvert.SerializeObject(this);
        File.WriteAllText(path, content, Encoding.UTF8);
    }
}
