namespace MalumMenu
{
    // Compatibility wrapper so existing code that references `CheatToggles` keeps compiling.
    // This forwards selected toggles to the existing MalumSettings class in this repo.
    public static class CheatToggles
    {
        public static bool freecam
        {
            get => MalumSettings.freecam;
            set => MalumSettings.freecam = value;
        }

        public static bool zoomOut
        {
            get => MalumSettings.zoomOut;
            set => MalumSettings.zoomOut = value;
        }

        public static bool noShadows
        {
            get => MalumSettings.noShadows;
            set => MalumSettings.noShadows = value;
        }

        public static bool seeRoles
        {
            get => MalumSettings.seeRoles;
            set => MalumSettings.seeRoles = value;
        }

        public static bool seePlayerInfo
        {
            get => MalumSettings.seePlayerInfo;
            set => MalumSettings.seePlayerInfo = value;
        }

        public static bool seeGhosts
        {
            get => MalumSettings.seeGhosts;
            set => MalumSettings.seeGhosts = value;
        }

        public static bool sporeCloudFix
        {
            get => MalumSettings.sporeCloudFix;
            set => MalumSettings.sporeCloudFix = value;
        }
    }
}
