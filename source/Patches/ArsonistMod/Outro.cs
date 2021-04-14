using System.Linq;
using HarmonyLib;
using ProxyOfUs.Roles;
using UnityEngine;

namespace ProxyOfUs.ArsonistMod
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
    public class Outro
    {
        public static void Postfix(EndGameManager __instance)
        {
            var role = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Arsonist && ((Arsonist) x).ArsonistWins);
            if (role == null) return;
            if (Roles.Role.GetRoles(RoleEnum.Jester).Any(x => ((Jester) x).VotedOut)) return;
            PoolablePlayer[] array = Object.FindObjectsOfType<PoolablePlayer>();
            array[0].NameText.text = role.ColorString + array[0].NameText.text + "</color>";
            __instance.BackgroundBar.material.color = role.Color;
            var text = Object.Instantiate(__instance.WinText);
            text.text = "Arsonist wins";
            text.color = role.Color;
            var pos = __instance.WinText.transform.localPosition;
            pos.y = 1.5f;
            text.transform.position = pos;
            text.fontSize = 1f;
        }
    }
}