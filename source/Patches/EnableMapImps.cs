using HarmonyLib;
using UnhollowerBaseLib;
using UnityEngine;

namespace ProxyOfUs
{
    [HarmonyPatch(typeof(GameSettingMenu), nameof(GameSettingMenu.OnEnable))]
    public class EnableMapImps
    {
        static void Prefix(ref GameSettingMenu __instance)
        {
            __instance.HideForOnline = new Il2CppReferenceArray<Transform>(0);
        }
    }
}