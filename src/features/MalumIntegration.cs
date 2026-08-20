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
                    // synchronize settings used by MalumESP
                    MalumSettings.freecam = FreecamEnabled;
                    MalumSettings.zoomOut = ZoomOutEnabled;
                    MalumSettings.noShadows = SporeCloudEnabled;
                    MalumSettings.seeGhosts = SeeGhostsEnabled;
                    MalumSettings.seeRoles = PlayerNametagsEnabled;
                    MalumSettings.seePlayerInfo = PlayerNametagsEnabled;

                    if (FreecamEnabled) MalumESP.FreecamCheat();
                    if (ZoomOutEnabled) MalumESP.ZoomOut(__instance);
                }
                catch { }
            }
        }

        // Per-player updates: nametags & ghost visibility (hook PlayerControl.Update to reach per-player physics)
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Update))]
        public static class PlayerControlUpdate
        {
            static void Postfix(PlayerControl __instance)
            {
                try
                {
                    // only run for local player instances
                    if (__instance == null) return;

                    var phys = __instance.MyPhysics;
                    if (phys == null) return;

                    if (PlayerNametagsEnabled) MalumESP.PlayerNametags(phys);
                    if (SeeGhostsEnabled) MalumESP.SeeGhostsCheat(phys);
                }
                catch { }
            }
        }

        // Meeting nametags when a meeting HUD is created
        [HarmonyPatch(typeof(MeetingHud), "Start")]
        public static class MeetingHudStart
        {
            static void Postfix(MeetingHud __instance)
            {
                try
                {
                    MalumSettings.seeRoles = MeetingNametagsEnabled;
                    MalumSettings.seePlayerInfo = MeetingNametagsEnabled;

                    if (MeetingNametagsEnabled) MalumESP.MeetingNametags(__instance);
                }
                catch { }
            }
        }
    }
}
