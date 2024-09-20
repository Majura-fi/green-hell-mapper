using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace MapperSource
{
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
}