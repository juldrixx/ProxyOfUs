using HarmonyLib;
using UnityEngine;

namespace ProxyOfUs.MorphlingMod
{ 
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {

        public static Sprite SampleSprite => ProxyOfUs.SampleSprite;
        public static Sprite MorphSprite => ProxyOfUs.MorphSprite;
        
        
        
        
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Morphling)) return;
            var role = Roles.Role.GetRole<Roles.Morphling>(PlayerControl.LocalPlayer);
            if (role.MorphButton == null)
            {
                role.MorphButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                role.MorphButton.renderer.enabled = true;
                role.MorphButton.renderer.sprite = SampleSprite;
            }

            if (role.MorphButton.renderer.sprite != SampleSprite && role.MorphButton.renderer.sprite != MorphSprite)
            {
                role.MorphButton.renderer.sprite = SampleSprite;
            }

            role.MorphButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            var position = __instance.KillButton.transform.localPosition;
            role.MorphButton.transform.localPosition = new Vector3(position.x,
                __instance.ReportButton.transform.localPosition.y, position.z);
            if (role.MorphButton.renderer.sprite == SampleSprite)
            {
                role.MorphButton.SetCoolDown(0f, 1f);
                role.closestPlayer = Utils.getClosestPlayer(PlayerControl.LocalPlayer);
                var distance = Utils.getDistBetweenPlayers(PlayerControl.LocalPlayer, role.closestPlayer);
                var flag9 = distance < GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
                if (flag9 && role.MorphButton.isActiveAndEnabled)
                {
                    role.MorphButton.SetTarget(role.closestPlayer);
                    __instance.KillButton.SetTarget(role.closestPlayer);
                }
                else
                {
                    role.MorphButton.SetTarget(null);
                }
            }
            else
            {
                if (role.Morphed)
                {
                    role.MorphButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.MorphlingDuration);
                    return;
                }
                role.MorphButton.SetCoolDown(role.MorphTimer(), CustomGameOptions.MorphlingCd);
                role.MorphButton.renderer.color = Palette.EnabledColor;
                role.MorphButton.renderer.material.SetFloat("_Desat", 0f);
            }
            
            
            
        }

    }
}