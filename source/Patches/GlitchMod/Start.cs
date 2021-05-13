using System;
using System.Linq;
using HarmonyLib;
using ProxyOfUs.Roles;

namespace ProxyOfUs.GlitchMod
{
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginCrewmate))]
    class Start
    {
        static void Postfix(IntroCutscene __instance)
        {
            var glitch = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Glitch);
            if (glitch != null)
            {
                ((Glitch)glitch).LastMimic = DateTime.UtcNow;
                ((Glitch)glitch).LastHack = DateTime.UtcNow;
                ((Glitch)glitch).LastKill = DateTime.UtcNow.AddSeconds(CustomGameOptions.InitialGlitchKillCooldown + (CustomGameOptions.GlitchKillCooldown * -1));
            }
        }
    }
}
