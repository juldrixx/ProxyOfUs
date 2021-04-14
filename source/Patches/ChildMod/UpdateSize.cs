using HarmonyLib;
using ProxyOfUs.Roles;
using UnityEngine;

namespace ProxyOfUs.ChildMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class UpdateSize
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (player.Is(RoleEnum.Child))
                {
                    player.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                    return;
                }

                if (player.Is(RoleEnum.Morphling))
                {
                    var role = Roles.Role.GetRole<Morphling>(player);
                    if (role.MorphedPlayer != null && role.MorphedPlayer.Is(RoleEnum.Child))
                    {
                        player.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                        return;
                    }
                    
                }

                if (player.Is(RoleEnum.Glitch))
                {
                    var role = Roles.Role.GetRole<Glitch>(player);
                    if (role.MimicTarget != null && role.MimicTarget.Is(RoleEnum.Child))
                    {
                        player.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                        return;
                    }
                }
                
                if (player.Is(ModifierEnum.BigBoi))
                {
                    player.transform.localScale = new Vector3(1f, 1f, 1f);
                    return;
                }
                player.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            }
        }
    }
}