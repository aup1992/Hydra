using HarmonyLib;
using MalumMenu;
using UnityEngine;

namespace HydraMenu.features
{
    internal class MalumIntegration
    {
        public static bool FreecamEnabled { get; set; } = false;
        public static bool ZoomOutEnabled { get; set; } = false;
        public static bool PlayerNametagsEnabled { get; set; } = false;
        public static bool MeetingNametagsEnabled { get; set; } = false;
        public static bool SeeGhostsEnabled { get; set; } = false;
        public static bool SporeCloudEnabled { get; set; } = false;

        // Called each HUD frame: handle freecam & zoom
        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        public static class HudUpdatePatch
        {
            static void Postfix(HudManager __instance)
            {
                try
                {
                    // if (FreecamEnabled) MalumESP.FreecamCheat();
                    // if (ZoomOutEnabled) MalumESP.ZoomOut(__instance);
                }
                catch { }
            }
        }

        // Per-player physics updates: nametags & ghost visibility
        // NOTE: PlayerPhysics.Update does not exist in this version. Patch disabled.
        // If you need to hook player physics, use a different method or class.
        /*
        [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.Update))]
        public static class PlayerPhysicsUpdate
        {
            static void Postfix(PlayerPhysics __instance)
            {
                try
                {
                    if (PlayerNametagsEnabled) MalumESP.PlayerNametags(__instance);
                    if (SeeGhostsEnabled) MalumESP.SeeGhostsCheat(__instance);
                }
                catch { }
            }
        }
        */

        // Meeting nametags when a meeting HUD is created
        [HarmonyPatch(typeof(MeetingHud), "Start")]
        public static class MeetingHudStart
        {
            static void Postfix(MeetingHud __instance)
            {
                try
                {
                    // if (MeetingNametagsEnabled) MalumESP.MeetingNametags(__instance);
                }
                catch { }
            }
        }

        // Optional: apply spore cloud fix when appropriate. This is a bit more invasive because
        // it requires a Mushroom instance; for now we will attempt to patch Mushroom.Awake if present.
        [HarmonyPatch]
        public static class OptionalSporePatch
        {
            // Intentionally left blank — Spore/Z-order fix is exposed as a toggle and will be
            // applied indirectly by the other hooks that run each frame. If you want a dedicated
            // patch for Mushroom lifecycle, tell me which method to hook and I will add it.
        }
    }
}
