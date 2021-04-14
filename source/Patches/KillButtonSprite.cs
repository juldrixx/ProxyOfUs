using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace ProxyOfUs
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class KillButtonSprite
    {
        private static Sprite Shift => ProxyOfUs.Shift;
        private static Sprite Rewind => ProxyOfUs.Rewind;
        private static Sprite Medic => ProxyOfUs.MedicSprite;
        private static Sprite Seer => ProxyOfUs.SeerSprite;
        private static Sprite Douse => ProxyOfUs.DouseSprite;

        private static Sprite Revive => ProxyOfUs.ReviveSprite;

        private static Sprite Kill => ProxyOfUs.Kill;
        private static Sprite Button => ProxyOfUs.ButtonSprite;


        
        public static void Postfix(HudManager __instance)
        {
    
            if (__instance.KillButton == null) return;
            var flag = false;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Shifter))
            {
                __instance.KillButton.renderer.sprite = Shift;
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.TimeLord))
            {
                __instance.KillButton.renderer.sprite = Rewind;
                flag = true;
            } 
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Seer))
            {
                __instance.KillButton.renderer.sprite = Seer;
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Medic))
            {
                __instance.KillButton.renderer.sprite = Medic;
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist))
            {
                __instance.KillButton.renderer.sprite = Douse;
                flag = true;
            }
            else if (PlayerControl.LocalPlayer.Is(RoleEnum.Altruist))
            {
                __instance.KillButton.renderer.sprite = Revive;
                flag = true;
            }
            else
            {
                __instance.KillButton.renderer.sprite = Kill;
                flag = PlayerControl.LocalPlayer.Is(RoleEnum.Sheriff);
            }

            var keyInt = Input.GetKeyInt(KeyCode.Q);
            var controller = ConsoleJoystick.player.GetButtonDown(8);
            if ((keyInt | controller) && __instance.KillButton != null && flag && !PlayerControl.LocalPlayer.Data.IsDead)
            {
                __instance.KillButton.PerformKill();
            }
        }
    }
}