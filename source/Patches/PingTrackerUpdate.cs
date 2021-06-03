using System.Linq;
using HarmonyLib;
using ProxyOfUs.CustomHats;
using UnityEngine;

namespace ProxyOfUs {

    //[HarmonyPriority(Priority.VeryHigh)] // to show this message first, or be overrided if any plugins do
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        [HarmonyPrefix]
        public static void Prefix(PingTracker __instance)
        {
            if (!__instance.GetComponentInChildren<SpriteRenderer>())
            {
                GameObject spriteObject = new GameObject("ProxyOfUs Sprite");
                spriteObject.AddComponent<SpriteRenderer>().sprite = ProxyOfUs.ProxyOfUsSprite;
                spriteObject.transform.parent = __instance.transform;
                spriteObject.transform.localPosition = new Vector3(-0.8f, 0.3f, -1);
                spriteObject.transform.localScale *= 0.05f;
            }
        }
        
        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            AspectPosition position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(2.7f, 0.4f, 0);
            position.AdjustPosition();
            
            __instance.text.text = 
                $"<color=#00FF00FF>ProxyOfUs v1.0.4</color>\n" +
                $"Ping: {AmongUsClient.Instance.Ping}ms";
            
        }
    }
}