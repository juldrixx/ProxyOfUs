using System.Linq;
using HarmonyLib;
using Hazel;
using ProxyOfUs.Roles;

namespace ProxyOfUs.GlitchMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.IsGameOverDueToDeath))]
    class GameOverDueToDeathPatch
    {
        public static void Postfix(out bool __result)
        {
            __result = false;
        }
    }
}
