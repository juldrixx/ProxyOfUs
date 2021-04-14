using HarmonyLib;

namespace ProxyOfUs.GlitchMod
{
    [HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.ShowInfectedMap))]
    class EngineerMapOpen
    {
        static bool Prefix(MapBehaviour __instance)
        {
            return !PlayerControl.LocalPlayer.Is(RoleEnum.Glitch);
        }
    }
}
