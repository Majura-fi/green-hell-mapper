using System.Reflection;
using HarmonyLib;
using MapperSource;
using static UnityModManagerNet.UnityModManager;

namespace Mapper;

public static class Plugin
{
    private static Harmony? harmony;
    public static bool Enabled { get; private set; }

    public static bool Load(ModEntry modEntry)
    {
        harmony = new Harmony(modEntry.Info.Id);
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        modEntry.OnToggle = OnToggle;
        modEntry.Logger.Log("Mapper loaded.");
        return true;
    }

    private static bool OnToggle(ModEntry entry, bool enable)
    {
        if (enable)
        {
            LocationService.Instance.Start();
            entry.Logger.Log("Mapper enabled.");
        }
        else
        {
            LocationService.Instance.Stop();
            entry.Logger.Log("Mapper disabled.");
        }

        Enabled = enable;
        return true;
    }
}