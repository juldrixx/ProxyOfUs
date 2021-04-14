using System.Linq;
using HarmonyLib;
using ProxyOfUs.Roles;

namespace ProxyOfUs.InvestigatorMod
{
    public class EndGame
    {
        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.ExitGame))]
        public static class EndGamePatch {

            public static void Prefix(AmongUsClient __instance) {
                Reset();
            }
        }

        [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
        public static class EndGameManagerPatch {
            public static bool Prefix(EndGameManager __instance) {
                Reset();

                return true;
            }
        }

        public static void Reset()
        {
            foreach (var role in Roles.Role.GetRoles(RoleEnum.Investigator))
            {
                ((Investigator)role).AllPrints.Clear();
            }
        }
    }
}