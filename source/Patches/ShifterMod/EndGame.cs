using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Hazel;
using Reactor.Extensions;

namespace ProxyOfUs.ShifterMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.RpcEndGame))]
    public class EndGame
    {
        public static bool Prefix(ShipStatus __instance, [HarmonyArgument(0)] GameOverReason reason)
        {
            if (reason != GameOverReason.HumansByVote && reason != GameOverReason.HumansByTask) return true;

            foreach (var role in Roles.Role.AllRoles)
            {
                if (role.RoleType == RoleEnum.Shifter)
                {
                    ((Roles.Shifter) role).Loses();
                }
            }
            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.ShifterLose,
                SendOption.Reliable, -1);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            return true;
        }
    }
}