using HarmonyLib;

namespace MapperSource;

[HarmonyPatch(typeof(Player), nameof(Player.UpdateMe))]
public static class Player_UpdateMe_Prefix
{
    [HarmonyPrefix]
    static void Prefix(Player __instance)
    {
        LocationService.Instance.UpdateLocation(
            __instance.GetInstanceID(),
            __instance.GetWorldPosition()
        );
    }
}