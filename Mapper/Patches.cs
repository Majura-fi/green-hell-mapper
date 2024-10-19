using System.Reflection;
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
            __instance.GetWorldPosition(),
            __instance.GetCamTransform().forward
        );
    }
}

[HarmonyPatch]
public static class ReplicatedLogicalPlayer_Update
{
    static MethodInfo TargetMethod()
    {
        return typeof(ReplicatedLogicalPlayer)
            .GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    static void Prefix(ReplicatedLogicalPlayer __instance)
    {
        LocationService.Instance.UpdateLocation(
            __instance.GetInstanceID(),
            __instance.GetWorldPosition(),
            __instance.transform.forward
        );
    }
}